namespace Guten.Html
{
    using MbUnit.Framework;

    [TestFixture]
    public class DivTests
    {
        [Test]
        public void Empty()
        {
            var expected = "<div></div>";
            var tag = new Div { };
            var actual = tag.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}