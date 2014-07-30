namespace Guten
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    
    public class Tag : IEnumerable<Tag>, IEnumerable<KeyValuePair<string, string>>
    {
        protected string Name { get; private set; }

        private readonly List<Tag> children = new List<Tag>();
        private readonly Dictionary<string, TagProperty> attributes = new Dictionary<string, TagProperty>();
        private readonly TagPropertyFactory property;

        public Tag(string tagName)
        {
            Name = tagName;
            this.property = new TagPropertyFactory();
            RegisterProperties(this.property);
        }

        protected virtual void RegisterProperties(TagPropertyFactory factory)
        {
            factory.Default<TagProperty>();
            factory.Register<ListTagProperty>("class");    
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<Tag> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            return attributes.Select(attribute => new KeyValuePair<string, string>(attribute.Key, attribute.Value.Get())).GetEnumerator();
        }

        public string this[string name]
        {
            get
            {
                return attributes[name].Get();
            }
        }

        public Tag this[int position]
        {
            get 
            {
                return children.ElementAtOrDefault(position);
            }
        }

        public void Add(object obj)
        {
            Add(StringPairs.FromObject(obj));
        }

        public void Add(string text)
        {
            children.Add(new Text(text));
        }

        public void Add(Tag tag)
        {
            children.Add(tag);
        }

        public void Add(IEnumerable<KeyValuePair<string, string>> attributes)
        {
            foreach(var pair in attributes)
            {
                this.Add(pair.Key, pair.Value);
            }
        }

        public void Add(string name, string value)
        {
            if (String.IsNullOrWhiteSpace(name))
                return;

            name = name.ToLower();
            if (attributes.ContainsKey(name))
            {
                attributes[name].Add(value);
            }            
            else
            {
                var attribute = property.Create(name);
                if (attribute != null)
                {
                    attribute.Set(value);
                    attributes.Add(name, attribute);
                }
            }
        }

        public override string ToString()
        {
            return ToString(new DefaultTagWriterFactory());
        }

        public virtual string ToString(TagWriterFactory factory)
        {
            TagWriter writer = factory.CreateWriter(this);

            writer.OpenStartTag(Name);
            foreach (var attribute in attributes)
            {
                writer.Attribute(attribute.Key, attribute.Value.Get());
            }
            writer.CloseStartTag(Name);
            foreach (var child in children)
            {
                writer.Contents(child.ToString(factory));
            }
            writer.OpenEndTag(Name);
            writer.CloseEndTag(Name);

            return writer.GetOutput();
        }

        private class Text : Tag
        {
            private readonly string text;

            public Text(string text)
                : base(String.Empty)
            {
                this.text = text;
            }

            public override string ToString()
            {
                return this.text;
            }

            public override string ToString(TagWriterFactory factory)
            {
                var writer = factory.CreateWriter(this);
                writer.Text(text);
                return writer.GetOutput();
            }
        }

    }

}
