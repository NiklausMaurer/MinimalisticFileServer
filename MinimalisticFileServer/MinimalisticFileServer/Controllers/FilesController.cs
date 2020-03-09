using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MinimalisticFileServer.DataTransferObjects;

namespace MinimalisticFileServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private IConfiguration Config { get; }
        private string Directory { get; }

        public FilesController(IConfiguration config)
        {
            Config = config;
            Directory = Config[EnvironmentVariables.Path];
        }

        [HttpGet("/files/{filename?}")]
        public async Task<IActionResult> Get(string filename)
        {
            if (!string.IsNullOrEmpty(filename)) return ServeFile(filename);
            
            var files = await Task.Run(() => System.IO.Directory.GetFiles(Directory));
            
            return Ok(files.Select(f => new FileDto(GenerateDownloadUrl(f))));
        }

        [HttpDelete("/files/{filename}")]
        public async Task<IActionResult> Delete(string filename)
        {
            var file = GetFilePath(filename);
            if (!System.IO.File.Exists(file)) return NotFound();
            
            await Task.Run(() => System.IO.File.Delete(file));

            return Ok();
        }

        private string GenerateDownloadUrl(string file)
        {
            return $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/files/{Path.GetFileName(file)}";
        }

        private IActionResult ServeFile(string filename)
        {
            var file = GetFilePath(filename);

            if (!System.IO.File.Exists(file)) return NotFound();

            var stream = new FileStream(file, FileMode.Open);
            
            return File(stream, "application/octet-stream");
        }

        private string GetFilePath(string filename)
        {
            return Path.Combine(Directory, filename);
        }
    }
}