using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.TemplateSpecifications
{
    [TestFixture]
    public class When_transforming_multiple_parameter_template : TemplateTest
    {
        const string TemplateActual = "this is a template";
        private const string ResultActual = "this is a result";

        private string _result;

        private TemplateArgumentCollection _arguments = new TemplateArgumentCollection()
                                                            {
                                                                new TemplateArgument("this is argument 0", new object()),
                                                                new TemplateArgument("this is argument 1", new object()),
                                                                new TemplateArgument("this is argument 2", new object()),
                                                            };

        protected override void Arrange()
        {
            base.Arrange();
            TextTransformer.Setup(t => t.Transform(TemplateActual, _arguments)).Returns(ResultActual);
        }

        protected override void Act()
        {
            _result = Template.Transform(TemplateActual, _arguments);
        }

        [Test]
        public void result_should_be_returned()
        {
            Assert.AreEqual(_result, ResultActual);
        }

        [Test]
        public void tranform_of_text_transformer_should_be_called()
        {
            TextTransformer.Verify(t => t.Transform(TemplateActual, _arguments));
        }
    }
}