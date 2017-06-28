using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.TextTransformerBaseSpecifications
{
    [TestFixture]
    public class When_transforming_a_single_parameter_template
    {
        private const string Template = "this is a template";
        private const string ResultActual = "this is a result";
        private const string ArgumentName = "this is an argument";

        private readonly object _argument = new object();
        private string _result;

        private Mock<ITextTransformerHost> _host = new Mock<ITextTransformerHost>();
        private Mock<ITextTemplatingEngine> _engine = new Mock<ITextTemplatingEngine>();

        [FixtureInitializer]
        public void Initialize()
        {
            var host = _host.Object;
            var engine = _engine.Object;

            _engine.Setup(e => e.ProcessTemplate(Template, host)).Returns(ResultActual).Verifiable();
            var transformer = new TextTransformerImplementation(host, engine);
            _result = transformer.Transform(Template, ArgumentName, _argument);
        }

        [Test]
        public void result_should_be_returned()
        {
            Assert.AreEqual(_result, ResultActual);
        }

        [Test]
        public void host_should_be_initialized()
        {
            _host.Verify(h => h.Initialize(It.Is<TemplateArgumentCollection>(c => c.Count > 0 && c[0].Name == ArgumentName && c[0].Value == _argument)));
        }

        [Test]
        public void engine_should_be_called()
        {
            _engine.Verify();
        }

        [Test]
        public void host_should_be_finished()
        {
            _host.Verify(h => h.Finish(It.Is<TemplateArgumentCollection>(c => c.Count > 0 && c[0].Name == ArgumentName && c[0].Value == _argument)));
        }
    }
}
