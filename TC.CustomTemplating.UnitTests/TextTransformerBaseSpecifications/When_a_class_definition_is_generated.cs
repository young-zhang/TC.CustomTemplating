using System;
using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.TextTransformerBaseSpecifications
{
    [TestFixture]
    public class When_a_class_definition_is_generated
    {
        private const string _template = "this is a template";
        private ClassDefinitionEventArgs _result;

        private Mock<ITextTransformerHost> _host = new Mock<ITextTransformerHost>();
        private Mock<ITextTemplatingEngine> _engine = new Mock<ITextTemplatingEngine>();

        [FixtureInitializer]
        public void Initialize()
        {
            var host = _host.Object;
            var engine = _engine.Object;

            var transformer = new TextTransformerImplementation(host, engine);
            EventHandler<ClassDefinitionEventArgs> generated = (o, e) => _result = e;
            transformer.ClassDefinitionGenerated += generated;

            var handler = _host.CreateEventHandler<ClassDefinitionEventArgs>();
            _host.Object.ClassDefinitionGenerated += handler;
            handler.Raise(new ClassDefinitionEventArgs(_template));

            transformer.ClassDefinitionGenerated -= generated;
        }

        [Test]
        public void event_should_be_raised()
        {
            Assert.IsNotNull(_result);
        }

        [Test]
        public void event_argument_should_contain_template()
        {
            Assert.AreEqual(_result.ClassDefinition, _template);
        }
    }
}
