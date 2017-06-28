using MbUnit.Framework;

namespace TC.CustomTemplating.IntegrationTests.TextTransformerSpecifications
{
    [TestFixture]
    public class When_transforming_a_multiple_parameters_template : DomainTextTransformerSpecifications.When_transforming_Base
    {
        public When_transforming_a_multiple_parameters_template() : 
            base(new When_transforming_a_multiple_parameters_template_runner())
        {
        }

        [Test]
        public void the_transformation_should_match_expected_result()
        {
            Assert.AreEqual(ExpectedResult, ActualResult);
        }
    }
}