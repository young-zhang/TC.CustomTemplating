using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateSpecifications
{
    [TestFixture]
    public class When_transforming_a_parameterless_template : TemplateTest
    {
        private const string _template = "this is a template";
        private const string _resultActual = "this is a result";
        private string _result;

        protected override void Arrange()
        {
            base.Arrange();
            TextTransformer.Setup(t => t.Transform(_template)).Returns(_resultActual);
        }

        protected override void Act()
        {
            _result = Template.Transform(_template);
        }

        [Test]
        public void result_should_be_returned()
        {
            Assert.AreEqual(_result, _resultActual);
        }

        [Test]
        public void tranform_of_text_transformer_should_be_called()
        {
            TextTransformer.Verify(t => t.Transform(_template));
        }
    }
}