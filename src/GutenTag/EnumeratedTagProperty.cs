namespace Guten
{
    using System;

    internal class EnumeratedTagProperty<TEnum> : TagProperty where TEnum : struct, IConvertible
    {
        public override string Set(string value)
        {
            value = value.Trim();
            if (String.IsNullOrEmpty(value))
            {
                return base.Set(value);
            }
            else
            {
                TEnum e;
                if (Enum.TryParse<TEnum>(value, true, out e))
                {
                    return base.Set(e.ToString());
                }
            }
            return base.Set(null);
        }
    }
}