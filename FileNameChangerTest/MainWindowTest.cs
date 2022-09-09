using FileNameChanger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FileNameChangerTest
{
    [TestClass]
    public class MainWindowTest
    {
        [TestMethod]
        public void ConstantsTest()
        {
            Assert.IsTrue(Constants.ProhibitedCharacters.Contains('/'));
            Assert.IsTrue(Constants.ProhibitedCharacters.Contains('<'));
        }
    }
}
