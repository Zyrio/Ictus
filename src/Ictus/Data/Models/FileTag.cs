using System;

namespace Ictus.Data.Models
{
    public class FileTag
    {
        public Guid Id { get; set; }
        public int Hits { get; set; }
        public bool InUse { get; set; }

        public virtual File File { get; set; }
        public virtual Tag Tag { get; set; }
    }
}