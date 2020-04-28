using NUnit.Framework;

namespace GoogleDriveTreeView.Tests
{
    public class DirectoryItemTest
    {
        private DirectoryItem dirItem;
        private DirectoryItem dirItemArg;
        private string name = "test";
        private string id = "rootTest";
        private DirectoryItemType type = DirectoryItemType.File;

        [SetUp]
        public void Setup()
        {
            dirItemArg = new DirectoryItem(name, id, type);
            dirItem = new DirectoryItem();
            dirItem.Name = name;
            dirItem.Id = id;
            dirItem.Type = type;
        }

        [Test]
        public void DirectoryItem_DefConstr_test()
        {
            Assert.AreEqual(dirItem.Name, name);
            Assert.AreEqual(dirItem.Id, id);
            Assert.AreEqual(dirItem.Type, type);
        }

        [Test]
        public void DirectoryItem_ArgConstr_test()
        {
            Assert.AreEqual(dirItemArg.Name, name);
            Assert.AreEqual(dirItemArg.Id, id);
            Assert.AreEqual(dirItemArg.Type, type);
        }
    }
}
