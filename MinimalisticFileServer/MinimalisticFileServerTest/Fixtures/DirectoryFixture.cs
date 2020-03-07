using System;
using System.IO;

namespace MinimalisticFileServerTest.Fixtures
{
    public class DirectoryFixture : IDisposable
    {
        public string TempDirectory { get; }
        
        public DirectoryFixture()
        {
            TempDirectory = Path.Join(Path.GetTempPath(), "MinimalisticFileServer");
            
            Directory.CreateDirectory(TempDirectory);

            foreach(var file in Directory.GetFiles("Assets/TestFiles"))
            {
                File.Copy(file, Path.Combine(this.TempDirectory, Path.GetFileName(file)));
            }
        }
        
        public void Dispose()
        {
            Directory.Delete(TempDirectory, true);
        }
    }
}