namespace FileManager.Controllers
{
    using FileManager.Entities.Files;
    using FileManager.Framework.Common.Interfaces;
    using FileManager.Framework.Common.Resources;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController, EnableCors("AllowOrigin")]   
    public class FileManagerController : ControllerBase
    {
        private readonly IFileLogicActions fileActions = null;
        public FileManagerController(IFileLogicActions fileActions)
        {
            this.fileActions = fileActions;
        }
        /// <summary>
        /// Metodo que carga un archivo al servidor
        /// </summary>
        /// <returns>Retorna resultado de la carga</returns>
        [Route("Upload")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.FirstOrDefault();
            if (file == null) return StatusCode(412, MessageResource.FileRequired);
               
            var result = await fileActions.Upload(new FileModel()
            {
                FileName = file.FileName,
                Data = Request.Form.Files
            });  
                
            return new JsonResult(result)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        /// <summary>
        /// Metodo que descarga un archivo al servidor
        /// </summary>
        /// <returns>Retorna resultado de la carga</returns>
        [Route("Download/{fileName}")]
        [HttpGet, DisableRequestSizeLimit]
        public async Task<IActionResult> Download(string fileName)
        {   
            FileModel file = await fileActions.Download(new FileModel() { FileName = fileName });
            return File((FileStream)file.Data, "application/octet-stream", file.FileName);
        }

        /// <summary>
        /// Metodo que elimina un archivo al servidor
        /// </summary>
        /// <returns>Retorna resultado de la carga</returns>
        [Route("Delete/{fileName}")]
        [HttpGet]
        public async Task<IActionResult> Delete(string fileName)
        {
            var result = await fileActions.Delete(new FileModel() { FileName = fileName });
            return new JsonResult(result)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        /// <summary>
        /// Metodo que obtiene listado de archivos del servidor
        /// </summary>
        /// <returns>Retorna resultado de la carga</returns>
        [Route("Get")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await fileActions.Get();
            return Ok(result);            
        }

    }
}
