namespace FileManager.Data.Files
{
    using FileManager.Entities.Files;
    using FileManager.Framework.Common.Interfaces;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Capa de acceso a de datos
    /// </summary>
    public class FileManagerData : IFileLogicActions
    {
        private readonly IConfiguration config = null;
        public FileManagerData(IConfiguration config)
        {
            this.config = config;
        }
        public async Task<bool> Delete(FileModel item)
        {
            bool result = false;
            using (var conn = new SqlConnection(config.GetConnectionString("FileDBConnection")))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandText = $"DELETE TOP(1) FROM FileManager WHERE Id = {item.Id}";
                    cmd.CommandType = CommandType.Text;
                    result = await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
            return result;
        }

        public async Task<bool> Download(FileModel item)
        {           
            return await Task.Run(() => { return false; });
        }

        public async Task<IEnumerable<FileModel>> Get(FileModel item)
        {
            List<FileModel> list = new List<FileModel>();
            using (var conn = new SqlConnection(config.GetConnectionString("FileDBConnection")))
            {
                using (var cmd = new SqlCommand())
                {
                    var reader = await cmd.ExecuteReaderAsync();
                    while(await reader.ReadAsync())
                    {
                        list.Add(new FileModel()
                        {
                            Id = reader.GetFieldValue<long>(reader.GetOrdinal("Id")),
                            FileName = reader.GetFieldValue<string>(reader.GetOrdinal("FileName")),
                            URI = reader.GetFieldValue<string>(reader.GetOrdinal("URI")),
                            Path = reader.GetFieldValue<string>(reader.GetOrdinal("Path"))
                        });
                    }
                }
            }
            return list;
        }

        public async Task<bool> Upload(FileModel item)
        {
            bool result = false;
            using (var conn = new SqlConnection(config.GetConnectionString("FileDBConnection")))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandText = $"INSERT INTO FileManager (FileName,URI,Path) VALUES ('{item.FileName}','{item.URI}','{item.Path}')";
                    cmd.CommandType = CommandType.Text;
                    result = await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
            return result;
        }
    }
}
