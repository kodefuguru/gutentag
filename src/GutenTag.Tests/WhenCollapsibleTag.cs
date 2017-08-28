using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GutenTag.Tests
{
    [TestClass]
    public class WhenCollapsibleTag
    {
        [TestMethod]
        public void IsEmpty()
        {
            var tag = new CollapsibleTag("foo");
            Assert.AreEqual("<foo />", tag.ToString());
        }
    }
}