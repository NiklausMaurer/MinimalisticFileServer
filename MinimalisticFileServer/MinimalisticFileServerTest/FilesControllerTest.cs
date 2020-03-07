using MinimalisticFileServer.Controllers;
using MinimalisticFileServerTest.TestDoubles;
using Xunit;

namespace MinimalisticFileServerTest
{
    public class FilesControllerTest
    {
        private FilesController FilesController { get; }

        public FilesControllerTest()
        {
            FilesController = new FilesController(new LoggerDummy<FilesController>());
        }

        [Fact]
        public void TestGet()
        {
        }
    }
}