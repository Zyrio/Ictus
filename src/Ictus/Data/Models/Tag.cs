using System;

namespace Ictus.Data.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int FileCount { get; set; }
    }
}