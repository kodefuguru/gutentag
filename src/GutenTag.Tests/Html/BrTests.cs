namespace Guten.Html
{
    using System;
    using System.Linq;
    using MbUnit.Framework;

    [TestFixture]
    public class BrTests
    {
        [Test]
        public void Empty()
        {
            var expected = "<br>";
            var tag = new Br { };
            var actual = tag.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
