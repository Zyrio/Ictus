using System;
using Ictus.Data.Enums;

namespace Ictus.Models.ApiModels.ApiV2Models {
    public class PostVoteApiModel
    {
        public Guid FileId { get; set; }
        public Guid TagId { get; set; }
        public Vote Vote { get; set; }
    }
}