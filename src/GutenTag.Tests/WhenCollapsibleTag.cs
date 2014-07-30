namespace Guten
{
    using MbUnit.Framework;

    public class WhenCollapsibleTag
    {       
        [Test]
        public void IsEmpty()
        {
            var tag = new CollapsibleTag("foo");
            Assert.AreEqual("<foo />", tag.ToString());
        }
    }
}