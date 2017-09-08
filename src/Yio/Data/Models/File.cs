using System;
using Yio.Data.Enums;

namespace Yio.Data.Models
{
    public class File
    {
        public Guid Id { get; set; }
        public string Filename { get; set; }
        public Filetype Type { get; set; }
        public string OriginalFilename { get; set; }
        public DateTime DateUploaded { get; set; }
        public string Source { get; set; }
    }
}