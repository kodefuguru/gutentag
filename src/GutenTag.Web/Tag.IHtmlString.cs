namespace GutenTag
{
    using System.Web;

    public partial class Tag : IHtmlString
    {
        public string ToHtmlString()
        {
            return new HtmlString(ToString()).ToHtmlString();
        }
    }
}
