using System.Collections.Generic;
using System.Threading.Tasks;
using Yio.Data.Models;

namespace Yio.Data.Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task<Tag> GetTagByName(string name);
        Task<List<Tag>> GetTags();
        Task<List<Tag>> GetTagsOrdered();
    }
}