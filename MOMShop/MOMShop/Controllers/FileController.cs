using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Net.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using MOMShop.Entites;

namespace MOMShop.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public FileController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("get")]
        public IActionResult GetFile(string folder, string fileName)
        {
            string folderPath = $"\\MOMShop\\images\\{folder}";
            var baseDir = Directory.GetParent(Directory.GetParent(_hostEnvironment.ContentRootPath).FullName).FullName;
            string filePath = Path.Combine(baseDir + folderPath, fileName);
            var fileByte = System.IO.File.ReadAllBytes(filePath);
            return File(fileByte, "application/octet-stream", fileName);
        }

    }
}
