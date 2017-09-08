using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Yio.Data.Constants;
using Yio.Data.Models;
using Yio.Data.Repositories.Interfaces;

// Please don't judge me.
// I was either drunk or tired when I wrote this. Probably both.
//  -- Ducky

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
        public async Task<String> GetRandom(String repo)
        {
            Response.Headers.Add("Content-Type", "application/json");
            Response.Headers.Add("Access-Control-Allow-Headers", "Authorization, Content-Type");

            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            if (String.IsNullOrEmpty(repo)){
                Response.StatusCode = 400;
                return "{ \"message\": \"" + "Supplied parameters are incorrect" + "\", \"status\": 400 }";
            }

            if(!String.IsNullOrEmpty("repo")) {
                Tag tag = await _tagRepository.GetTagByName(repo);

                try {
                    FileTag fileTag = await _fileTagRepository.GetRandomFileTagById(tag.Id);
                    File file = fileTag.File;
                    List<FileTag> tagsForFile = await _fileTagRepository.GetFileTagsForFile(file.Id);

                    var randomFile = AppSettingsConstant.FileEndpoint + file.Source + "/" + file.Filename;
                
                    randomFile = randomFile
                        .Replace("https://", "//")
                        .Replace("http://", "//");

                    var returnObject = "{ ";
                    returnObject += "\"url\": \"" + randomFile + "\" ";
                    returnObject += "}";

                    return returnObject;
                } catch {
                    Response.StatusCode = 404;
                    return "{ \"message\": \"" + "Repository does not exist" + "\", \"status\": 404 }";
                }
            }

            Response.StatusCode = 500;
            return "{ \"message\": \"" + "He'd dead, Jim!" + "\", \"status\": 500 }";
        }

        // /api/v1/site
        
        [HttpGet]
        [Route("site")]
        public async Task<String> GetSite()
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

            var returnObject = "{ ";
            returnObject += "\"name\": \"" + "Yiff.co" + "\",";
            returnObject += "\"default\": \"" + "furry" + "\",";
            returnObject += "\"showNav\": \"" + "true" + "\",";
            returnObject += "\"repos\": \"" + repos + "\"";
            returnObject += "}";

            return returnObject;
        }
    }
}