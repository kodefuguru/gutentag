namespace Guten
{
    using System;
    
    public class TagProperty
    {
        private string value;

        public TagProperty()
        {
            Modifier = new TagPropertyModifier();
        }

        protected TagPropertyModifier Modifier { get; private set; }

        protected virtual string CoreSet(string value)
        {
            return this.value = value;
        }

        public virtual string Set(string value)
        {
            var current = Get();
            value = Modifier.ModifyReceived(value, current);
            value = Modifier.ModifyBeforeSet(value, current);
            return CoreSet(value);
        }

        public virtual string Get()
        {
            return this.value;
        }

        public virtual string CoreAdd(string value)
        {
            return CoreSet(value);
        }

        public virtual string Add(string value)
        {
            var current = Get();
            value = Modifier.ModifyReceived(value, current);
            value = Modifier.ModifyBeforeAdd(value, current);
            return CoreAdd(value);
        }

        public virtual string Remove()
        {
            return this.value = null;
        }

        public virtual string Remove(string value)
        {
            return Remove();
        }

        public void AddModifier(TagPropertyModifier modifier)
        {
            Modifier.Add(modifier);
        }
    }

    public class EnumeratedTagProperty<TEnum> : TagProperty where TEnum : struct, IConvertible
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