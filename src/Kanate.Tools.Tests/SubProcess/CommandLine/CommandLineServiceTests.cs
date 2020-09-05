using Kanate.Tools.IO;
using Kanate.Tools.SubProcess.CommandLine;
using NUnit.Framework;
using System;

namespace Kanate.Tools.Tests.SubProcess.CommandLine
{
    [TestFixture]
    public class CommandLineServiceTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("1:\\cmd.exe")]
        public void CreateForPath_Throws_Exception(string path)
        {
            KanateFile.DefaultExistsFunc = (filePath) => false;

            Assert.Throws<ArgumentException>(() => CommandLineService.CreateForCommandLinePath(path));
        }
    }
}
