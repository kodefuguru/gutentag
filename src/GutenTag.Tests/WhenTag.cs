using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GutenTag.Tests
{
    [TestClass]
    public class WhenTag
    {
        [TestMethod]
        public void IsEmpty()
        {
            var tag = new Tag("div");
            Assert.IsNull(tag[0]);
            Assert.AreEqual("<div></div>", tag.ToString());
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void HasText()
        {
            var tag = new Tag("p")
            {
                "Hello World"
            };
            Assert.IsInstanceOfType(tag[0], typeof(Tag));
            Assert.AreEqual("<p>Hello World</p>", tag.ToString());
        }

        [TestMethod]
        public void HasChild()
        {
            var tag = new Tag("div")
            {
                new Tag("p")
                {
                    "Hello World"
                }
            };
            Assert.IsInstanceOfType(tag[0], typeof(Tag));

            Assert.AreEqual("<div><p>Hello World</p></div>", tag.ToString());
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        [Description("When a tag is initialized with a dictionary, it should use its items as attributes.")]
        public void UsesDictionary()
        {
            var attributes = new Dictionary<string, string>
            {
                {"id", "foo"},
                {"class", "bar"}
            };

            var tag = new Tag("div")
            {
                attributes
            };

            Assert.AreEqual("foo", tag["id"]);
            Assert.AreEqual("bar", tag["class"]);
        }

        [TestMethod]
        [Description(
            "When a tag is initialized with an anonymouse type, it should use its string properties as attributes.")]
        public void UsesAnonymousType()
        {
            var tag = new Tag("div")
            {
                new {id = "foo"}
            };

            Assert.AreEqual("foo", tag["id"]);
        }

        [TestMethod]
        [Description(
            "Attributes are not case sensitive. Out of convention, they are lowercased. Great news for C# enthusiasts, since some lowercase attribute names are keywords.")]
        public void UsesAnonymousTypeWithCasing()
        {
            var tag = new Tag("div")
            {
                new {Id = "foo", Class = "bar"}
            };

            Assert.AreEqual("foo", tag["id"]);
            Assert.AreEqual("bar", tag["class"]);
        }

        [TestMethod]
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

        [TestMethod]
        public void HasListAttributesWithSameName()
        {
            var tag = new Tag("div")
            {
                {"class", "foo"},
                {"class", "bar"}
            };
            Assert.AreEqual("foo bar", tag["class"]);
        }

        [TestMethod]
        public void AttributeIsChanged()
        {
            var tag = new Tag("div")
            {
                {"class", "foo"}
            };

            tag["class"] = "bar";

            Assert.AreEqual("bar", tag["class"]);
        }
    }
}