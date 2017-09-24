using System;
using System.Collections.Generic;
using Ictus.Data.Models;
using Ictus.Models.ApiModels;

namespace Ictus.Models.ApiModels.ApiV2Models.ReturnModels {
    public class FileReturnModel : ErrorApiModel
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public File File { get; set; }
        public List<FileTag> Tags { get; set; }
    }
}