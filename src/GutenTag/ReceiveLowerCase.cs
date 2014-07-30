namespace Guten
{
    public class ReceiveLowerCase : TagPropertyModifier
    {
        protected override string OnReceive(string value, string current)
        {
            return value == null ? null : value.ToLower();
        }
    }
}