using System;
using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.TextTransformerBaseSpecifications
{
    [TestFixture]
    public class When_transforming_multiple_parameter_template_with_null
    {
        private const string Template = "this is a template";

        private Mock<ITextTransformerHost> _host = new Mock<ITextTransformerHost>();
        private Mock<ITextTemplatingEngine> _engine = new Mock<ITextTemplatingEngine>();

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_NullArgumentException()
        {
            var host = _host.Object;
            var engine = _engine.Object;
            var transformer = new TextTransformerImplementation(host, engine);
            transformer.Transform(Template, null);
        }
    }
}
