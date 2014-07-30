namespace Guten
{
    using MbUnit.Framework;

    [TestFixture]
    public class WhenVoidTag
    {
        [Test]
        public void IsEmpty()
        {
            var tag = new VoidTag("foo");
            Assert.AreEqual("<foo>", tag.ToString());
        }
    }
}