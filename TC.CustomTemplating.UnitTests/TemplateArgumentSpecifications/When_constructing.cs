using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateArgumentSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_constructing
    {
        /// <summary />
        [Test]
        public void with_default_constructor_Name_should_be_null()
        {
            var argument = new TemplateArgument();
            Assert.IsNull(argument.Name);
        }

        /// <summary />
        [Test]
        public void with_default_constructor_Value_should_be_null()
        {
            var argument = new TemplateArgument();
            Assert.IsNull(argument.Value);
        }

        /// <summary />
        [Test]
        public void with_argumented_constructor_Name_should_be_assigned()
        {
            var actual = "Name";
            var argument = new TemplateArgument(actual, new object());
            Assert.AreEqual(argument.Name, actual);
        }

        /// <summary />
        [Test]
        public void with_argumented_constructor_Value_should_be_assigned()
        {
            var actual = new object();
            var argument = new TemplateArgument("Name", actual);
            Assert.AreSame(argument.Value, actual);
        }
    }
}