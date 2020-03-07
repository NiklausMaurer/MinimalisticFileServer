using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MinimalisticFileServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private ILogger<FilesController> Logger { get; }
        private IConfiguration Config { get; }

        public FilesController(ILogger<FilesController> logger, IConfiguration config)
        {
            Logger = logger;
            Config = config;
        }

        [HttpGet]
        public IEnumerable<FileDTO> Get()
        {
            Logger.Log(LogLevel.Trace, "Serving files");
            
            var files = Directory.GetFiles(Config[EnvironmentVariables.PATH]);

            foreach (var file in files)
            {
                yield return new FileDTO {Url = $"/files/{Path.GetFileName(file)}"};
            }
        }
    }
}