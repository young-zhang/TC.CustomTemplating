using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.TextTransformerBaseSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_constructing
    {
        /// <summary />
        [Test]
        public void with_default_constructor_Host_should_not_be_emtpy()
        {
            var transformer = new TextTransformerImplementation();

            Assert.IsNotNull(transformer.Host);
        }

        /// <summary />
        [Test]
        public void with_argumented_constructor_Host_should_not_be_emtpy()
        {
            Mock<ITextTransformerHost> _host = new Mock<ITextTransformerHost>();
            Mock<ITextTemplatingEngine> _engine = new Mock<ITextTemplatingEngine>();
            var host = _host.Object;
            var engine = _engine.Object;

            var transformer = new TextTransformerImplementation(host, engine);

            Assert.IsNotNull(transformer.Host);
        }

        /// <summary />
        [Test]
        public void AppDomain_should_not_be_emtpy()
        {
            Mock<ITextTransformerHost> _host = new Mock<ITextTransformerHost>();
            Mock<ITextTemplatingEngine> _engine = new Mock<ITextTemplatingEngine>();
            var host = _host.Object;
            var engine = _engine.Object;

            var transformer = new TextTransformerImplementation(host, engine);

            Assert.IsNotNull(transformer.AppDomain);
        }
    }
}
