using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UploadFileExample.Models;

namespace UploadFileExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        const string FILE_PATH = @"D:\Proyectos\C#\UploadFile\UploadFileExample\";
        public FileUploadController()
        {

        }
        [HttpPost]
        public IActionResult Post([FromBody] FileToUpload theFile)
        {
            try
            {
                var filePathName = FILE_PATH + Path.GetFileNameWithoutExtension(theFile.FileName) + "-" +
                    DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") +
                    Path.GetExtension(theFile.FileName);
                if (theFile.FileAsBase64.Contains(","))
                {
                    theFile.FileAsBase64 = theFile.FileAsBase64.Substring(theFile.FileAsBase64.IndexOf(",") + 1);
                }
                theFile.FileAsByteArray = Convert.FromBase64String(theFile.FileAsBase64);
                using (var fs = new FileStream(filePathName, FileMode.CreateNew))
                {
                    fs.Write(theFile.FileAsByteArray, 0, theFile.FileAsByteArray.Length);
                }
                return Ok(new { messege = $"Imagen {filePathName} guardada" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

}
