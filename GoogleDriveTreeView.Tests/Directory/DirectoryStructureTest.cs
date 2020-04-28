using NUnit.Framework;

namespace GoogleDriveTreeView.Tests
{
    public class DirectoryStructureTest
    {
        private string fullpath = @"C:\Documents\Newsletters\Summer2018.pdf";
        private string name = "Summer2018.pdf";

        [Test]
        public void GetFileFolderName_test()
        {
            Assert.AreEqual(DirectoryStructure.GetFileFolderName(fullpath), name); 
        }
    }
}
