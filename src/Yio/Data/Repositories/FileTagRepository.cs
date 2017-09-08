using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yio.Data.Repositories.Interfaces;
using Yio.Data.Models;

namespace Yio.Data.Repositories
{
    public class FileTagRepository : IFileTagRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FileTagRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddFileTagByFileId(Guid fileId,
            string name)
        {
            var fileTag = await _dbContext
                .FileTags
                .Where(ft => ft.File.Id == fileId &&
                    ft.Tag.Name == name)
                .SingleOrDefaultAsync();

            if(fileTag == null) {
                var file = await _dbContext
                    .Files
                    .Where(f => f.Id == fileId)
                    .SingleOrDefaultAsync();

                var tag = await _dbContext
                    .Tags
                    .Where(t => t.Name == name)
                    .SingleOrDefaultAsync();

                if(tag == null) {
                    tag = new Tag {
                        Name = name
                    };

                    _dbContext
                        .Tags
                        .Add(tag);
                }

                FileTag newFileTag = new FileTag {
                    File = file,
                    Tag = tag,
                    InUse = true,
                    Hits = 0
                };

                _dbContext
                    .FileTags
                    .Add(newFileTag);
            } else {
                fileTag.Hits = fileTag.Hits + 1;

                _dbContext
                    .FileTags
                    .Update(fileTag);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<FileTag>> GetFileTags()
        {
            return await _dbContext
                .FileTags
                .ToListAsync();
        }

        public async Task<List<FileTag>> GetFileTagsForFile(Guid fileId)
        {
            return await _dbContext
                .FileTags
                .Where(ft => ft.File.Id == fileId)
                .ToListAsync();
        }

        public async Task<FileTag> GetRandomFileTagById(Guid tagId)
        {
            return await _dbContext
                .FileTags
                .Include(ft => ft.File)
                .Where(ft => ft.Tag.Id == tagId)
                .OrderBy(ft => Guid.NewGuid())
                .FirstOrDefaultAsync();
        }

        public async Task IncreaseTagVote(Guid fileId,
            Guid tagId)
        {
            var fileTag = await _dbContext
                .FileTags
                .Where(ft => ft.File.Id == fileId &&
                    ft.Tag.Id == tagId)
                .SingleOrDefaultAsync();

            fileTag.Hits = fileTag.Hits + 1;

            _dbContext
                .FileTags
                .Update(fileTag);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DecreaseTagVote(Guid fileId,
            Guid tagId)
        {
            var fileTag = await _dbContext
                .FileTags
                .Where(ft => ft.File.Id == fileId &&
                    ft.Tag.Id == tagId)
                .SingleOrDefaultAsync();

            fileTag.Hits = fileTag.Hits - 1;

            _dbContext
                .FileTags
                .Update(fileTag);

            await _dbContext.SaveChangesAsync();
        }
    }
}