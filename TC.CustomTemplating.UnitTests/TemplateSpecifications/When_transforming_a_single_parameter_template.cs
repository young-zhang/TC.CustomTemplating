using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateSpecifications
{
    [TestFixture]
    public class When_transforming_a_single_parameter_template : TemplateTest
    {
        private const string TemplateActual = "this is a template";
        private const string ResultActual = "this is a result";
        private const string ArgumentName = "this is an argument";

        private readonly object _argument = new object();
        private string _result;

        protected override void Arrange()
        {
            base.Arrange();
            TextTransformer.Setup(t => t.Transform(TemplateActual, ArgumentName, _argument)).Returns(ResultActual);
        }

        protected override void Act()
        {
            _result = Template.Transform(TemplateActual, ArgumentName, _argument);
        }

        [Test]
        public void result_should_be_returned()
        {
            Assert.AreEqual(_result, ResultActual);
        }

        [Test]
        public void tranform_of_text_transformer_should_be_called()
        {
            TextTransformer.Verify(t => t.Transform(TemplateActual, ArgumentName, _argument));
        }
    }
}