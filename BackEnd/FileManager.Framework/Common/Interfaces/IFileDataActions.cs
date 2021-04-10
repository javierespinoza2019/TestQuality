namespace FileManager.Framework.Common.Interfaces
{
    using FileManager.Entities.Files;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface para controlador de archivos
    /// </summary>
    public interface IFileDataActions
    {
        Task<bool> Upload(FileModel item);
        Task<bool> Download(FileModel item);
        Task<IEnumerable<FileModel>> Get(FileModel item);
        Task<bool> Delete(FileModel item);
    }
}
