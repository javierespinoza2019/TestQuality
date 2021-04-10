namespace FileManager.Business.Files
{
    using FileManager.Entities.Files;
    using FileManager.Framework.Common.Interfaces;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// Capa de logica de negocio para gestion de archivos
    /// </summary>
    public class FileManagerBusiness : IFileLogicActions
    {
        public FileManagerBusiness()
        {
            
        }
        public async Task<bool> Delete(FileModel item)
        {
            return await Task.Run(() => {
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fullPath = Path.Combine(pathToSave, item.FileName);              
                File.Delete(fullPath);
                return true;
            });
        }

        public async Task<FileModel> Download(FileModel item)
        {
            return await Task.Run(() => {
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fullPath = Path.Combine(pathToSave, item.FileName);
                var file = new FileModel()
                {
                    FileName = item.FileName,
                    Data = new FileStream(fullPath, FileMode.Open),
                };
                return file;
            });
        }

        public async Task<IEnumerable<FileModel>> Get()
        {
            return await Task.Run(() => 
            {
                var list = new List<FileModel>();
                var folderName = Path.Combine("Resources", "Images");
                var pathToSearch = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                foreach (string filePath in Directory.EnumerateFiles(pathToSearch, "*",SearchOption.AllDirectories))
                {
                    var file = new FileInfo(filePath);
                    list.Add(new FileModel() { 
                        FileName = file.Name,
                        URI = string.Format("http://localhost:5000/{0}/{1}", folderName.Replace('\\','/'), file.Name),
                        Extension = file.Extension
                    });
                }
                return list;             
            });
        }

        public async Task<bool> Upload(FileModel item)
        {
            return await Task.Run(() =>
            {
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var file = ((IFormFileCollection)item.Data)[0];
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }               
                return true;
            });
        }
    }
}
