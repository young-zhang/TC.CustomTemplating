using MbUnit.Framework;
using System;

namespace TC.CustomTemplating.UnitTests.TemplateArgumentDirectiveProcessorSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_normalizing
    {
        /// <summary />
        [Test]
        public void a_simple_type_it_should_be_returned()
        {
            const string type = "System.Collections.Hashtable";
            const string expected = "System.Collections.Hashtable";

            var actual = TemplateArgumentDirectiveProcessor.NormalizeType(type);

            Assert.AreEqual(expected, actual);
        }

        /// <summary />
        [Test]
        public void null_it_should_return_null()
        {
            const string type = null;
            const string expected = null;

            var actual = TemplateArgumentDirectiveProcessor.NormalizeType(type);

            Assert.AreEqual(expected, actual);
        }

        /// <summary />
        [Test]
        public void an_empty_string_it_should_return_null()
        {
            const string type = "";
            const string expected = null;

            var actual = TemplateArgumentDirectiveProcessor.NormalizeType(type);

            Assert.AreEqual(expected, actual);
        }

        /// <summary />
        [Test]
        public void a_fully_qualified_type_its_type_should_be_returned()
        {
            const string type = "System.Collections.Hashtable, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            const string expected = "System.Collections.Hashtable";

            var actual = TemplateArgumentDirectiveProcessor.NormalizeType(type);

            Assert.AreEqual(expected, actual);
        }

        /// <summary />
        [Test]
        public void a_generic_type_it_should_be_returned()
        {
            const string type = "System.Collections.Generic.Dictionary<string, int>";
            const string expected = "System.Collections.Generic.Dictionary<string, int>";

            var actual = TemplateArgumentDirectiveProcessor.NormalizeType(type);

            Assert.AreEqual(expected, actual);
        }

        /// <summary />
        [Test]
        public void a_fully_qualified_generic_type_its_generic_type_should_be_returned()
        {
            const string type = "System.Collections.Generic.Dictionary<string, int>, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            const string expected = "System.Collections.Generic.Dictionary<string, int>";

            var actual = TemplateArgumentDirectiveProcessor.NormalizeType(type);

            Assert.AreEqual(expected, actual);
        }

        /// <summary />
        [Test]
        public void a_nested_generic_type_it_should_be_returned()
        {
            const string type = "System.Collections.Generic.Dictionary<string, System.Generic.Dictionary<int, System.Collections.Generic.List<double>>>";
            const string expected = "System.Collections.Generic.Dictionary<string, System.Generic.Dictionary<int, System.Collections.Generic.List<double>>>";

            var actual = TemplateArgumentDirectiveProcessor.NormalizeType(type);

            Assert.AreEqual(expected, actual);
        }

        /// <summary />
        [Test]
        public void a_fully_qualified_nested_generic_type_its_generic_type_should_be_returned()
        {
            const string type = "System.Collections.Generic.Dictionary<string, System.Generic.Dictionary<int, System.Collections.Generic.List<double>>>, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            const string expected = "System.Collections.Generic.Dictionary<string, System.Generic.Dictionary<int, System.Collections.Generic.List<double>>>";

            var actual = TemplateArgumentDirectiveProcessor.NormalizeType(type);

            Assert.AreEqual(expected, actual);
        }

        /// <summary />
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void an_invalid_generic_type_it_should_throw_an_invalid_operation_exception()
        {
            const string type = "System.Collections.Generic.Dictionary<string, System.Generic.Dictionary<int, System.Collections.Generic.List<double>>>>, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

            TemplateArgumentDirectiveProcessor.NormalizeType(type);
        }
    }
}