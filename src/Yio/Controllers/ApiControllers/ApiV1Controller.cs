using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Yio.Data.Constants;
using Yio.Data.Models;
using Yio.Data.Repositories.Interfaces;
using Yio.Models.ApiModels;
using Yio.Models.ApiModels.ApiV1Models;

// Kill me.

namespace Yio.Controllers.ApiControllers
{
    [Route("/api/v1")]
    public class ApiV1Controller : Controller
    {
        public ITagRepository _tagRepository { get; }
        public IFileRepository _fileRepository { get; }
        public IFileTagRepository _fileTagRepository { get; }

        public ApiV1Controller(
            ITagRepository tagRepository,
            IFileRepository fileRepository,
            IFileTagRepository fileTagRepository
        )
        {
            _tagRepository = tagRepository;
            _fileRepository = fileRepository;
            _fileTagRepository = fileTagRepository;
        }

        // /api/v1/random

        [HttpGet]
        [Route("random/{repo}")]
        public async Task<GetRandomApiModel> GetRandom(String repo)
        {
            Response.Headers.Add("Content-Type", "application/json");
            Response.Headers.Add("Access-Control-Allow-Headers", "Authorization, Content-Type");

            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            if (String.IsNullOrEmpty(repo)){
                Response.StatusCode = 400;

                GetRandomApiModel am = new GetRandomApiModel
                {
                    Message = "Supplied parameters are incorrect",
                    Status = 400
                };

                return am;
            }

            if(!String.IsNullOrEmpty("repo")) {
                Tag tag = await _tagRepository.GetTagByName(repo);

                try {
                    FileTag fileTag = await _fileTagRepository.GetRandomFileTagById(tag.Id);
                    File file = fileTag.File;
                    List<FileTag> tagsForFile = await _fileTagRepository.GetFileTagsForFile(file.Id);

                    var randomFile = "/" + file.Source + "/" + file.Filename;
                    var randomFileWithEndpoint = AppSettingsConstant.FileEndpoint + file.Source + "/" + file.Filename;

                    randomFileWithEndpoint = randomFileWithEndpoint
                        .Replace("https://", "//")
                        .Replace("http://", "//");

                    GetRandomApiModel am = new GetRandomApiModel
                    {
                        Url = randomFileWithEndpoint,
                        File = randomFile,
                        Message = "API V1 will soon be depricated. Please migrate to V2.",
                        Status = 200
                    };
                    
                    await _fileRepository.HitFile(file);

                    return am;
                } catch {
                    Response.StatusCode = 404;

                    GetRandomApiModel am = new GetRandomApiModel
                    {
                        Message = "Repository does not exist",
                        Status = 400
                    };

                    return am;
                }
            }

            Response.StatusCode = 500;

            return new GetRandomApiModel {};
        }

        // /api/v1/site
        
        [HttpGet]
        [Route("site")]
        public async Task<GetSiteApiModel> GetSite()
        {
            List<Tag> tags = await _tagRepository.GetTagsOrdered();
            string repos = null;

            foreach(var tag in tags) {
                if(String.IsNullOrEmpty(repos)) {
                    repos += tag.Name;
                } else {
                    repos += "|" + tag.Name;
                }
            }

            Response.Headers.Add("Content-Type", "application/json");
            Response.Headers.Add("Access-Control-Allow-Headers", "Authorization, Content-Type");

            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            GetSiteApiModel am = new GetSiteApiModel
            {
                Name = "Yiff",
                Default = "furry",
                ShowNav = "true",
                Repos = repos,
                Endpoint = AppSettingsConstant.FileEndpoint,
                Message = "API V1 will soon be depricated. Please migrate to V2.",
                Status = 200
            };

            return am;
        }
    }
}