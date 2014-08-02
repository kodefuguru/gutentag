namespace Guten
{
    using System.Web;

    public partial class Tag : IHtmlString
    {
        public string ToHtmlString()
        {
            return ToString();
        }
    }
}
