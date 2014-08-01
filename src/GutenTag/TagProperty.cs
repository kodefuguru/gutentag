namespace Guten
{
    using System;
    
    internal class TagProperty
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
}