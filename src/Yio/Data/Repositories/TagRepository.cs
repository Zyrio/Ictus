using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yio.Data.Repositories.Interfaces;
using Yio.Data.Models;

namespace Yio.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TagRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tag> GetTagByName(string name)
        {
            return await _dbContext
                .Tags
                .Where(t => t.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Tag>> GetTags()
        {
            return await _dbContext
                .Tags
                .ToListAsync();
        }

        public async Task<List<Tag>> GetTagsOrdered()
        {
            return await _dbContext
                .Tags
                .OrderBy(r => r.Name)
                .ToListAsync();
        }
    }
}