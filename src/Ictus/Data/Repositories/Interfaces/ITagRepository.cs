using System.Collections.Generic;
using System.Threading.Tasks;
using Ictus.Data.Models;

namespace Ictus.Data.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task<Tag> GetTagByName(string name);
        Task<List<Tag>> GetTags();
        Task<List<Tag>> GetTagsOrdered();
        Task<List<Tag>> GetTagsOrderedByCount();
    }
}