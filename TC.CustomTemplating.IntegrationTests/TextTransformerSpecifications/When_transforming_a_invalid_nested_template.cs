using MbUnit.Framework;

namespace TC.CustomTemplating.IntegrationTests.TextTransformerSpecifications
{
    [TestFixture]
    public class When_transforming_an_invalid_nested_template : When_transforming_Base
    {
        public When_transforming_an_invalid_nested_template() :
            base(new When_transforming_an_invalid_nested_template_runner())
        {
        }

        [Test]
        public void the_transformation_should_throw_an_exception()
        {
            Assert.IsInstanceOfType<TextTransformationException>(Exception);
        }
    }
}