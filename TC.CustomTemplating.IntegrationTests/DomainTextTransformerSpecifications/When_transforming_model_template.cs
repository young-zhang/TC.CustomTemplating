using MbUnit.Framework;
using TC.CustomTemplating.IntegrationTests.TextTransformerSpecifications;

namespace TC.CustomTemplating.IntegrationTests.DomainTextTransformerSpecifications
{
    [TestFixture]
    public class When_transforming_model_template : When_transforming_Base
    {
        private bool _classDefinitionGeneratedRaised;

        public When_transforming_model_template() :
            base(new When_transforming_model_template_runner())
        {
        }

        protected override void BefortAct(ITextTransformer transformer)
        {
            transformer.ClassDefinitionGenerated += (a, e) => _classDefinitionGeneratedRaised = true;
            base.BefortAct(transformer);
        }

        [Test]
        public void the_transformation_should_match_expected_result()
        {
            Assert.AreEqual(ExpectedResult, ActualResult);
        }

        [Test]
        public void with_the_text_transformer_the_ClassDefinitionGenerated_event_should_be_raised()
        {
            Assert.IsTrue(_classDefinitionGeneratedRaised);
        }
    }
}