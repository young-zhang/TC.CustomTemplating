using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_constructing
    {
        /// <summary />
        [Test]
        public void with_static_constructor_transformer_should_a_text_transformer()
        {
            Assert.IsInstanceOfType<TextTransformer>(Template.TextTransformer);
        }
    }
}