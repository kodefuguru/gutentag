using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GutenTag.Tests
{
    [TestClass]
    public class WhenVoidTag
    {
        [TestMethod]
        public void IsEmpty()
        {
            var tag = new VoidTag("foo");
            Assert.AreEqual("<foo>", tag.ToString());
        }
    }
}