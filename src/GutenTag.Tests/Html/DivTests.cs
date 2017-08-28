using GutenTag.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GutenTag.Tests.Html
{
    [TestClass]
    public class DivTests
    {
        [TestMethod]
        public void Empty()
        {
            const string expected = "<div></div>";
            var tag = new Div();
            var actual = tag.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}