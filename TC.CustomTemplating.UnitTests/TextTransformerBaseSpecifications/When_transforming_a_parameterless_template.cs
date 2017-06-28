using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.TextTransformerBaseSpecifications
{
    [TestFixture]
    public class When_transforming_a_parameterless_template
    {
        private const string _template = "this is a template";
        private const string _resultActual = "this is a result";
        private string _result;

        private Mock<ITextTransformerHost> _host = new Mock<ITextTransformerHost>();
        private Mock<ITextTemplatingEngine> _engine = new Mock<ITextTemplatingEngine>();

        [FixtureInitializer]
        public void Initialize()
        {
            var host = _host.Object;
            var engine = _engine.Object;

            _engine.Setup(e => e.ProcessTemplate(_template, host)).Returns(_resultActual).Verifiable();

            var transformer = new TextTransformerImplementation(host, engine);
            _result = transformer.Transform(_template);
        }

        [Test]
        public void result_should_be_returned()
        {
            Assert.AreEqual(_result, _resultActual);
        }

        [Test]
        public void host_should_be_initialized()
        {
            _host.Verify(h => h.Initialize(null));
        }

        [Test]
        public void engine_should_be_called()
        {
            _engine.Verify();
        }

        [Test]
        public void host_should_be_finished()
        {
            _host.Verify(h => h.Finish(null));
        }
    }
}
