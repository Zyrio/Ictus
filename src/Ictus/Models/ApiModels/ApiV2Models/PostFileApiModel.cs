using Ictus.Data.Enums;

namespace Ictus.Models.ApiModels.ApiV2Models {
    public class PostFileApiModel
    {
        public string Name { get; set; }
        public Filetype Type { get; set; }
        public string Source { get; set; }
    }
}