using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ictus.BulkImport.Data.Constants;
using Ictus.BulkImport.Data;
using Ictus.Data.Models;

namespace Ictus.BulkImport.Business
{
    public class Import : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public Import(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            var allFiles = Directory.GetFiles(ArgConstants.MoveFrom, "*.*", SearchOption.AllDirectories);

            foreach(var item in allFiles) {
                var itemArray = item.Replace("\\", "/").Split("/");

                var name = itemArray[itemArray.Length-1];
                var type = name.Substring(name.LastIndexOf('.') + 1);
                var tag = itemArray[itemArray.Length-3];
                var source = itemArray[itemArray.Length-2];

                if(name.ToLower().Contains(".ds_store") || name.ToLower().Contains("thumbs.db")) {
                    // ignore these files
                } else {
                    if(type == "jpg" || type == "jpeg" || type == "png" || type == "gif") {
                        //add file
                        Ictus.Data.Models.File file = new Ictus.Data.Models.File {
                            Filename = name,
                            Type = Ictus.Data.Enums.Filetype.Image,
                            OriginalFilename = name,
                            DateUploaded = DateTime.Now,
                            Source = source
                        };

                        var newFile = _dbContext
                            .Files
                            .Add(file)
                            .Entity;

                        //add tag
                        var newTag = _dbContext
                            .Tags
                            .Where(t => t.Name == tag)
                            .SingleOrDefault();

                        if(newTag == null) {
                            newTag = new Tag {
                                Name = name
                            };

                            _dbContext
                                .Tags
                                .Add(newTag);
                        }

                        FileTag newFileTag = new FileTag {
                            File = newFile,
                            Tag = newTag,
                            InUse = true,
                            Hits = 0
                        };

                        _dbContext
                            .FileTags
                            .Add(newFileTag);

                        newTag.FileCount = newTag.FileCount + 1;

                        // move file
                        var from = ArgConstants.MoveFrom + tag + "/" + source + "/" + name;
                        var to = ArgConstants.MoveTo + source + "/" + name;
                        try {
                            System.IO.File.Move(from, to);

                            _dbContext.SaveChanges();
                            Console.WriteLine(newFile.Id + ":" + name + ":" + tag + "/" + source);
                        } catch(System.IO.DirectoryNotFoundException) {
                            Directory.CreateDirectory(ArgConstants.MoveTo + source);

                            System.IO.File.Move(from, to);

                            _dbContext.SaveChanges();
                            Console.WriteLine(newFile.Id + ":" + name + ":" + tag + "/" + source);
                        } catch(System.IO.IOException) {
                            System.IO.File.Delete(from);
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}