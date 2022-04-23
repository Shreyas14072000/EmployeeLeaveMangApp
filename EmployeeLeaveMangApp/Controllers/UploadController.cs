using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Infrastructure;

namespace StudentManagementSystem.Controllers
{
    public class UploadController : Controller
    {
        public UploadFile _uploadFileToBlob;
        public UploadController(UploadFile uploadFileToBlob)
        {
            _uploadFileToBlob = uploadFileToBlob;
        }

        [Route("Upload/UploadFileAsync")]
        public IActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFileAsync1()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fileURL = await _uploadFileToBlob.UploadFileToBlobAsync(file.OpenReadStream(), fileName, file.ContentType);
                    ViewBag.Message = string.Format("File Uploaded Successfully");
                    return View("UploadFile");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
