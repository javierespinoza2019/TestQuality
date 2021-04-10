namespace FileManager.Framework.Common.Interfaces
{
    using FileManager.Entities.Files;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface para controlador de archivos
    /// </summary>
    public interface IFileLogicActions
    {
        Task<bool> Upload(FileModel item);
        Task<FileModel> Download(FileModel item);
        Task<IEnumerable<FileModel>> Get();
        Task<bool> Delete(FileModel item);
    }
}
