using GutenTag.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GutenTag.Tests.Html
{
    [TestClass]
    public class BrTests
    {
        [TestMethod]
        public void Empty()
        {
            const string expected = "<br>";
            var tag = new Br();
            var actual = tag.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}