using System;
using Ictus.Data.Enums;

namespace Ictus.Data.Models
{
    public class File
    {
        public Guid Id { get; set; }
        public string Filename { get; set; }
        public Filetype Type { get; set; }
        public string OriginalFilename { get; set; }
        public DateTime DateUploaded { get; set; }
        public string Source { get; set; }
        public int Hits { get; set; }
    }
}