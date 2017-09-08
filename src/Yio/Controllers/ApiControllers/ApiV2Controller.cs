using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Yio.Data;
using Yio.Data.Constants;
using Yio.Data.Enums;
using Yio.Data.Models;
using Yio.Data.Repositories.Interfaces;
using Yio.Models.ApiModels.ApiV2Models;
using Yio.Models.ApiModels.ApiV2Models.ReturnModels;
using Yio.Utilities.Interfaces;

namespace Yio.Controllers.ApiControllers
{
    [Route("/api/v2")]
    public class ApiV2Controller : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFileRepository _fileRepository;
        private readonly IRandomGeneratorUtilities _randomGeneratorUtilities;
        private readonly IFileTagRepository _fileTagRepository;
        private readonly ITagRepository _tagRepository;

        private static string _userUploadSource = "usup";

        public ApiV2Controller(
            ApplicationDbContext dbContext,
            IFileRepository fileRepository,
            IRandomGeneratorUtilities randomGeneratorUtilities,
            IFileTagRepository fileTagRepository,
            ITagRepository tagRepository
        ) {
            _dbContext = dbContext;
            _fileRepository = fileRepository;
            _randomGeneratorUtilities = randomGeneratorUtilities;
            _fileTagRepository = fileTagRepository;
            _tagRepository = tagRepository;
        }

        // /api/v2/files

        [HttpPost]
        [Route("files")]
        public async Task<IActionResult> PostFile([FromBody] PostFileApiModel item)
        {
            if (item == null){
                return BadRequest();
            }

            var addFile = await _fileRepository.AddFile(
                item.Name,
                item.Type,
                item.Name,
                DateTime.Now,
                item.Source
            );

            return Ok();
        }

        // /api/v2/files/{fileId}

        [HttpGet]
        [Route("files/{fileId}")]
        public async Task<FileReturnModel> GetFileById(Guid fileId)
        {
            Data.Models.File file = await _fileRepository.GetFileById(fileId);
            List<FileTag> tagsForFile = await _fileTagRepository.GetFileTagsForFile(file.Id);

            FileReturnModel returnItem = new FileReturnModel {
                Id = file.Id,
                Location = AppSettingsConstant.FileEndpoint + file.Source + "/" + file.Filename,
                File = file,
                Tags = tagsForFile
            };

            return returnItem;
        }

        // /api/v2/files/uploads

        [HttpPost]
        [Route("files/uploads")]
        public async Task<UploadReturnModel> PostUpload(List<IFormFile> files)
        {
            long filesSize = files.Sum(f => f.Length);

            var tmpPath = Path.GetTempFileName();

            Data.Models.File newFile = null;

            foreach (var file in files)
            {
                if (file.Length > 0) {
                    using (var stream = new FileStream(tmpPath, FileMode.Create)) {
                        await file.CopyToAsync(stream);

                        var fileExtension = Path.GetExtension(file.FileName).ToLower();
                        var fileName = file.FileName;
                        var newFileName =  _randomGeneratorUtilities.GetRandomLowercaseAlphanumericString(8) + fileExtension;

                        System.IO.File.Move(tmpPath, AppSettingsConstant.FileStorage + _userUploadSource +  newFileName);

                        Filetype fileType = 0;

                        switch(fileExtension) {
                            case "jpg":
                            case "jpeg":
                            case "png":
                                fileType = Filetype.Image;
                                break;
                            case "gif":
                                fileType = Filetype.Animated;
                                break;
                            case "mp4":
                            case "webm":
                                fileType = Filetype.Video;
                                break;
                            default:
                                fileType = 0;
                                break;
                        }

                        newFile = await _fileRepository.AddFile(
                            newFileName,
                            fileType,
                            fileName,
                            DateTime.Now,
                            _userUploadSource
                        );
                    }
                }
            }

            UploadReturnModel returnItem = new UploadReturnModel {
                Id = newFile.Id
            };

            return returnItem;
        }

        // /api/v2/random/{tagName}

        [HttpGet]
        [Route("random/{tagName}")]
        public async Task<FileReturnModel> GetRandomByTag(string tagName)
        {
            Tag tag = await _tagRepository.GetTagByName(tagName);
            FileTag fileTag = await _fileTagRepository.GetRandomFileTagById(tag.Id);
            Data.Models.File file = fileTag.File;
            List<FileTag> tagsForFile = await _fileTagRepository.GetFileTagsForFile(file.Id);

            FileReturnModel returnItem = new FileReturnModel {
                Id = file.Id,
                Location = AppSettingsConstant.FileEndpoint + file.Source + "/" + file.Filename,
                File = file,
                Tags = tagsForFile
            };

            return returnItem;
        }

        // /api/v2/tags

        [HttpGet]
        [Route("tags")]
        public async Task<List<Tag>> GetTags()
        {
            var tags = await _tagRepository.GetTagsOrderedByCount();

            return tags;
        }

        [HttpPost]
        [Route("tags")]
        public async Task<IActionResult> PostTag([FromBody] PostTagApiModel item)
        {
            if (item == null){
                return BadRequest();
            }

            await _fileTagRepository.AddFileTagByFileId(
                item.FileId,
                item.Name
            );

            return Ok();
        }

        // /api/v2/tags/default

        [HttpGet]
        [Route("tags/default")]
        public async Task<Tag> GetDefaultTag()
        {
            Tag tag = await _tagRepository.GetTagByName("furry");

            return tag;
        }

        // /api/v2/votes

        [HttpPost]
        [Route("votes")]
        public async Task<IActionResult> PostVote([FromBody] PostVoteApiModel item)
        {
            if (item == null){
                return BadRequest();
            }

            if(item.Vote == Vote.Up) {
                await _fileTagRepository.IncreaseTagVote(
                    item.FileId,
                    item.TagId
                );
            } else if (item.Vote == Vote.Down) {
                await _fileTagRepository.DecreaseTagVote(
                    item.FileId,
                    item.TagId
                );
            }

            return Ok();
        }

        //// /api/v2/mergefiles/{repo}
        //
        //[HttpGet]
        //[Route("mergefiles/{repo}/{source}")]
        //public async Task GetMergeFiles(string repo, string source)
        //{
        //    string[] files = Directory.GetFiles("/web/data/Yiff.co/pub/" + repo + "/" + source);
        //
        //    foreach(var file in files) {
        //        var newFile = await _fileRepository.AddFile(
        //            Path.GetFileName(file),
        //            Filetype.Image,
        //            Path.GetFileName(file),
        //            DateTime.Now,
        //            source
        //        );
        //
        //        await _fileTagRepository.AddFileTagByFileId(
        //            newFile.Id,
        //            repo
        //        );
        //    }
        //}
    }
}