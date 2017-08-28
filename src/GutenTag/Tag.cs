using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GutenTag
{
    public class Tag : IEnumerable<Tag>, IEnumerable<KeyValuePair<string, string>>
    {
        private readonly Dictionary<string, TagProperty> attributes = new Dictionary<string, TagProperty>();

        private readonly List<Tag> children = new List<Tag>();
        private readonly TagPropertyFactory property;

        public Tag(string tagName)
        {
            Name = tagName;
            property = new TagPropertyFactory();
            RegisterProperties(property);
        }

        protected string Name { get; }

        public string this[string name]
        {
            get => attributes[name].Get();
            set => attributes?[name].Set(value);
        }

        public Tag this[int position] => children.ElementAtOrDefault(position);

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            return attributes
                .Select(attribute => new KeyValuePair<string, string>(attribute.Key, attribute.Value.Get()))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Tag> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        internal virtual void RegisterProperties(TagPropertyFactory factory)
        {
            factory.Default<TagProperty>();
            factory.Register<ListTagProperty>("class");
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
            foreach (var pair in attributes)
            {
                Add(pair.Key, pair.Value);
            }
        }

        public void Add(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

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
            var writer = factory.CreateWriter(this);

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
                : base(string.Empty)
            {
                this.text = text;
            }

            public override string ToString()
            {
                return text;
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