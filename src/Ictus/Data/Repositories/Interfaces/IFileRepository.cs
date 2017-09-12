using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yio.Data.Enums;
using Yio.Data.Models;

namespace Yio.Data.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task<File> AddFile(String name,
            Filetype type,
            string originalFilename,
            DateTime dateUploaded,
            string source);
        Task<File> GetFileById(Guid id);
        Task<List<File>> GetFiles();
        Task<File> GetRandomFile();
        Task HitFile(File file);
    }
}