namespace FileManager.Entities.Files
{
    /// <summary>
    /// Modelo de datos para controlar datos de archivos
    /// </summary>
    public class FileModel
    {
        /// <summary>
        /// Identificador de archivo
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Nombre del archivo
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Extension del archivo
        /// </summary>
        public string Extension { get; set; }       
        /// <summary>
        /// Uri de descarga
        /// </summary>
        public string URI { get; set; }
        /// <summary>
        /// Archivo binario
        /// </summary>
        public object Data { get; set; }
    }
}
