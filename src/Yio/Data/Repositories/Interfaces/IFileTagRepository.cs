using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yio.Data.Models;

namespace Yio.Data.Repositories.Interfaces
{
    public interface IFileTagRepository
    {
        Task AddFileTagByFileId(Guid fileId, string name);
        Task<List<FileTag>> GetFileTags();
        Task<List<FileTag>> GetFileTagsForFile(Guid fileId);
        Task<FileTag> GetRandomFileTagById(Guid tagId);
        Task IncreaseTagVote(Guid fileId, Guid tagId);
        Task DecreaseTagVote(Guid fileId, Guid tagId);
    }
}