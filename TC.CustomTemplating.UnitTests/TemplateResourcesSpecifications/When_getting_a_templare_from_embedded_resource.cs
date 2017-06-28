using System;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateResourcesSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_getting_a_template_from_embedded_resource
    {
        /// <summary />
        [Test]
        public void the_template_should_be_returned()
        {
            const string expected = "Example";
            var actual = TemplateResources.Get("TC.CustomTemplating.UnitTests.TemplateResourcesSpecifications.Example.tt", GetType());
            Assert.AreEqual(expected, actual);
        }

        /// <summary />
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void without_providing_a_type_should_throw_an_ArgumentNullException()
        {
            var actual = TemplateResources.Get("TC.CustomTemplating.UnitTests.TemplateResourcesSpecifications.Example.tt", null);
        }


        /// <summary />
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void with_invalid_resource_string_should_throw_an_InvalidOperationException()
        {
            TemplateResources.Get("blablablab", GetType());
        }

        /// <summary />
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void without_resource_string_should_throw_an_ArgumentNullException()
        {
            TemplateResources.Get(null, GetType());
        }
    }
}