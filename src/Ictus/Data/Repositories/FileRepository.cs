using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ictus.Data.Repositories.Interfaces;
using Ictus.Data.Enums;
using Ictus.Data.Models;

namespace Ictus.Data.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<File> AddFile(String name,
            Filetype type,
            string originalFilename,
            DateTime dateUploaded,
            string source)
        {
            File file = new File {
                Filename = name,
                Type = type,
                OriginalFilename = originalFilename,
                DateUploaded = dateUploaded,
                Source = source
            };

            var newFile = _dbContext
                .Files
                .Add(file)
                .Entity;

            await _dbContext.SaveChangesAsync();

            return newFile;
        }

        public async Task<File> GetFileById(Guid id)
        {
            return await _dbContext
                .Files
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<File>> GetFiles()
        {
            return await _dbContext
                .Files
                .ToListAsync();
        }

        public async Task<File> GetRandomFile()
        {
            return await _dbContext
                .Files
                .OrderBy(f => Guid.NewGuid()) // NOTE: I have no idea how this works, it just does
                .FirstOrDefaultAsync();
        }

        public async Task HitFile(File file)
        {
            file.Hits = file.Hits + 1;
            
            _dbContext
                .Files
                .Update(file);

            await _dbContext.SaveChangesAsync();
        }
    }
}