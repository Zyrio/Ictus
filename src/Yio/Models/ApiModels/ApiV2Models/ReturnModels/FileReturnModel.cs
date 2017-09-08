using System;
using System.Collections.Generic;
using Yio.Data.Models;

namespace Yio.Models.ApiModels.ApiV2Models.ReturnModels {
    public class FileReturnModel
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public File File { get; set; }
        public List<FileTag> Tags { get; set; }
    }
}