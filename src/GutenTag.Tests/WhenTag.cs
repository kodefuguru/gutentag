namespace Guten
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MbUnit.Framework;

    [TestFixture]
    public class WhenTag
    {
        [Test]
        public void IsEmpty()
        {
            var tag = new Tag("div");
            Assert.IsNull(tag[0]); 
            Assert.AreEqual("<div></div>", tag.ToString());
        }

        [Test]
        public void HasAttribute()
        {
            var tag = new Tag("div")
            {
                {"id", "foo"}
            };
            Assert.AreEqual("foo", tag["id"]);
            Assert.IsNull(tag[0]); 
            Assert.AreEqual("<div id=\"foo\"></div>", tag.ToString());
        }

        [Test]
        public void HasAttributes()
        {
            var tag = new Tag("div")
            {
                {"id", "foo"},
                {"class", "bar"}
            };
            Assert.AreEqual("foo", tag["id"]);
            Assert.AreEqual("bar", tag["class"]);
            Assert.IsNull(tag[0]);
            Assert.AreEqual("<div id=\"foo\" class=\"bar\"></div>", tag.ToString());
        }

        [Test]
        public void HasText()
        {
            var tag = new Tag("p")
            {
                "Hello World"
            };
            Assert.IsInstanceOfType<Tag>(tag[0]);
            Assert.AreEqual("<p>Hello World</p>", tag.ToString());
        }

        [Test]
        public void HasChild()
        {
            var tag = new Tag("div")
            {
                new Tag("p") 
                {
                    "Hello World"
                }
            };
            Assert.IsInstanceOfType<Tag>(tag[0]);
            Assert.AreEqual("<div><p>Hello World</p></div>", tag.ToString());
        }

        [Test]
        public void HasAttributeAndChild()
        {
            var tag = new Tag("div")
            {
                {"id", "foo"},
                new Tag("p") 
                {
                    "Hello World"
                }
            };
            Assert.AreEqual("foo", tag["id"]);            
            Assert.AreEqual("<div id=\"foo\"><p>Hello World</p></div>", tag.ToString());
        }

        [Test]
        public void HasAttributesAndChild()
        {
            var tag = new Tag("div")
            {
                {"id", "foo"},
                {"class", "bar"},
                new Tag("p") 
                {
                    "Hello World"
                }
            };
            Assert.AreEqual("foo", tag["id"]);
            Assert.AreEqual("bar", tag["class"]); 
            Assert.AreEqual("<div id=\"foo\" class=\"bar\"><p>Hello World</p></div>", tag.ToString());
        }

        [Test]
        [Description("When a tag is initialized with a dictionary, it should use its items as attributes.")]
        public void UsesDictionary()
        {
            var attributes = new Dictionary<string, string>{
                {"id", "foo"}, {"class", "bar"}
            };

            var tag = new Tag("div")
            {
                attributes
            };

            Assert.AreEqual("foo", tag["id"]);
            Assert.AreEqual("bar", tag["class"]);             
        }

        [Test]
        [Description("When a tag is initialized with an anonymouse type, it should use its string properties as attributes.")]
        public void UsesAnonymousType()
        {
            var tag = new Tag("div")
            {
                new  { id = "foo" }
            };

            Assert.AreEqual("foo", tag["id"]);
        }

        [Test]
        [Description("Attributes are not case sensitive. Out of convention, they are lowercased. Great news for C# enthusiasts, since some lowercase attribute names are keywords.")]
        public void UsesAnonymousTypeWithCasing()
        {
            var tag = new Tag("div")
            {
                new { Id = "foo", Class = "bar" }
            };

            Assert.AreEqual("foo", tag["id"]);
            Assert.AreEqual("bar", tag["class"]);
        }

        [Test]
        [Description("Unless the attribute is a list type, such as class, previous values will be overwritten.")]
        public void HasNormalAttributesWithSameName()
        {
            var tag = new Tag("div")
            {
                {"id", "foo"},
                {"id", "bar"}
            };
            Assert.AreEqual("bar", tag["id"]);
        }

        [Test]
        public void HasListAttributesWithSameName()
        {
            var tag = new Tag("div")
            {
                {"class", "foo"},
                {"class", "bar"}
            };
            Assert.AreEqual("foo bar", tag["class"]);
        }
    }
}
