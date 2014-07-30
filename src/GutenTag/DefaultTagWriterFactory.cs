namespace Guten
{
    using System;
    using System.Collections.Generic;

    public class DefaultTagWriterFactory : TagWriterFactory
    {
        private static readonly Dictionary<Type, Func<TagWriter, TagWriter>> writers = new Dictionary<Type, Func<TagWriter, TagWriter>>();

        public static void Register(Type type, Func<TagWriter, TagWriter> decorator)
        {
            if (decorator != null)
            { 
                if (writers.ContainsKey(type))
                {
                    writers[type] = writer => decorator(writers[type](writer));
                }
                else
                {
                    writers[type] = decorator;
                }
            }
        }

        public static void Register<T>(Func<TagWriter, TagWriter> decorator)
        {
            Register(typeof(T), decorator);
        }

        public static void Unregister(Type type)
        {
            if (writers.ContainsKey(type))
            {
                writers.Remove(type);   
            }
        }

        static DefaultTagWriterFactory()
        {
            Register<VoidAttribute>(t => new VoidWriter(t));
            Register<CollapsibleAttribute>(t => new CollapsibleWriter(t));
        }

        private TagWriter Decorate(TagWriter writer, Type type)
        {
            if (writers.ContainsKey(type))
            {
                writer = writers[type](writer);
            }
            return writer;
        }

        private TagWriter CreateWriter(Type type)
        {
            TagWriter writer = new DefaultWriter();
            
            foreach (var attribute in type.GetCustomAttributes(true))
            {
                writer = Decorate(writer, attribute.GetType());
            }

            return writer;
        }

        public override TagWriter CreateWriter(Tag tag)
        {
            return tag == null ? new NullWriter() : CreateWriter(tag.GetType());
        }

        private class DefaultWriter : TagWriter
        {
            public DefaultWriter(TagWriter next = null)
                : base(next)
            {
            }

            public override void OpenStartTag(string name)
            {
                Output.Append("<" + name);
            }

            public override void CloseStartTag(string name)
            {
                Output.Append(">");
            }

            public override void Attribute(string name, string value)
            {
                if (name == null)
                {
                    return;
                }
                if (value == null)
                {
                    Output.Append(" " + name);
                }
                Output.AppendFormat(" {0}=\"{1}\"", name, value);
            }

            public override void Contents(string contents)
            {
                Output.Append(contents);
            }

            public override void OpenEndTag(string name)
            {
                Output.Append("</" + name);
            }
            public override void CloseEndTag(string name)
            {
                Output.Append(">");
            }

            public override void Text(string text)
            {
                Output.Append(text);
            }
        }

        private class NullWriter : TagWriter
        {

        }

        private class VoidWriter : TagWriter
        {
            public VoidWriter(TagWriter next)
                : base(next)
            {
            }
            public override void Contents(string contents)
            {
            }
            public override void OpenEndTag(string name)
            {
            }
            public override void CloseEndTag(string name)
            {
            }
        }

        private class CollapsibleWriter : TagWriter
        {
            private bool contentsWritten = false;
            
            public CollapsibleWriter(TagWriter next)
                : base(next)
            {
            }
            public override void Contents(string contents)
            {
                contentsWritten = true;
                base.Contents(contents);
            }
            public override void OpenEndTag(string name)
            {
                if (contentsWritten)
                {
                    base.OpenEndTag(name);
                }
                else
                {
                    Output.Insert(Output.Length - 1, " /");
                }
            }
            public override void CloseEndTag(string name)
            {
                if (contentsWritten)
                {
                    base.CloseEndTag(name);
                }
            }
        }
    }
}