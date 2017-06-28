using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;
using System;

namespace TC.CustomTemplating.UnitTests.TextTransformerBaseSpecifications
{
    [TestFixture]
    public class When_transforming_a_single_parameter_template__with_null
    {
        private const string Template = "this is a template";

        private Mock<ITextTransformerHost> _host = new Mock<ITextTransformerHost>();
        private Mock<ITextTemplatingEngine> _engine = new Mock<ITextTemplatingEngine>();

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void should_throw_NullArgumentException()
        {
            var host = _host.Object;
            var engine = _engine.Object;
            var transformer = new TextTransformerImplementation(host, engine);
            transformer.Transform(Template, null, new object());
        }
    }
}
