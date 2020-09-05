using Kanate.Tools.IO;
using NUnit.Framework;
using System.IO;

namespace Kanate.Tools.Tests.IO
{
    [TestFixture]
    public class KanateFileTests
    {
        [TearDown]
        public void After_Each_Test()
        {
            KanateFile.DefaultExistsFunc = File.Exists;
        }

        [TestCase(null)]
        [TestCase("")]
        public void Exists_Returns_False_For_Undefined_Path_Tests(string filePath)
        {
            bool subject = KanateFile.Exists(filePath);

            Assert.IsFalse(subject);
        }

        [Test]
        public void Exist_Returns_False_For_Not_Existing_Path()
        {
            KanateFile.DefaultExistsFunc = (filePath) => false;

            bool subject = KanateFile.Exists("C:\\Some\\Directory\\file.txt");

            Assert.IsFalse(subject);
        }

        [Test]
        public void Exist_Returns_True_For_Existing_Path()
        {
            KanateFile.DefaultExistsFunc = (filePath) => true;

            bool subject = KanateFile.Exists("C:\\Some\\Directory\\file.txt");

            Assert.IsTrue(subject);
        }
    }
}
