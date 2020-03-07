using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MinimalisticFileServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<FilesController> _logger;

        public FilesController(ILogger<FilesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<File> Get()
        {
            _logger.Log(LogLevel.Trace, "Serving files");

            return new[]
            {
                new File {Url = "http://somewhere:2222/files/file1.pdf"},
                new File {Url = "http://somewhere:2222/files/file2.pdf"},
                new File {Url = "http://somewhere:2222/files/file13pdf"}
            };
        }
    }
}