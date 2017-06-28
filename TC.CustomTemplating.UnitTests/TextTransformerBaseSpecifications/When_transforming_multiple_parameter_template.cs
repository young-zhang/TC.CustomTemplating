using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.TextTransformerBaseSpecifications
{
    [TestFixture]
    public class When_transforming_multiple_parameter_template
    {
        const string Template = "this is a template";
        private const string ResultActual = "this is a result";

        private string _result;

        private Mock<ITextTransformerHost> _host = new Mock<ITextTransformerHost>();
        private Mock<ITextTemplatingEngine> _engine = new Mock<ITextTemplatingEngine>();

        private TemplateArgumentCollection _arguments = new TemplateArgumentCollection()
                                                            {
                                                                new TemplateArgument("this is argument 0", new object()),
                                                                new TemplateArgument("this is argument 1", new object()),
                                                                new TemplateArgument("this is argument 2", new object()),
                                                            };

        [FixtureInitializer]
        public void Initialize()
        {
            var host = _host.Object;
            var engine = _engine.Object;

            _engine.Setup(e => e.ProcessTemplate(Template, host)).Returns(ResultActual).Verifiable();
            var transformer = new TextTransformerImplementation(host, engine);
            
            _result = transformer.Transform(Template, _arguments);
        }

        [Test]
        public void result_of_engine_should_be_returned()
        {
            Assert.AreEqual(_result, ResultActual);
        }

        [Test]
        public void host_should_be_initialized()
        {
            _host.Verify(h => h.Initialize(_arguments));
        }

        [Test]
        public void engine_should_be_called()
        {
            _engine.Verify();
        }

        [Test]
        public void host_should_be_finished()
        {
            _host.Verify(h => h.Finish(_arguments));
        }
    }
}
