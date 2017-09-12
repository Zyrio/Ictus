
namespace Ictus.Models.ApiModels.ApiV1Models {
    public class GetSiteApiModel : ErrorApiModel
    {
        public string Name { get; set; }
        public string Default { get; set; }
        public string ShowNav { get; set; }
        public string Repos { get; set; }
        public string Endpoint { get; set; }
    }
}