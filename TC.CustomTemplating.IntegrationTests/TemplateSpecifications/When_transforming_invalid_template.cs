using MbUnit.Framework;

namespace TC.CustomTemplating.IntegrationTests.TemplateSpecifications
{
    [TestFixture]
    public class When_transforming_invalid_template : When_transforming_Base
    {
        public When_transforming_invalid_template() :
            base(new When_transforming_invalid_template_runner())
        {
        }

        [Test]
        public void with_the_text_transformer_the_transformation_should_match_expected_result()
        {
            Assert.IsInstanceOfType<TextTransformationException>(Exception);
        }
    }
}