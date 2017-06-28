using System.CodeDom.Compiler;
using Microsoft.CSharp;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateArgumentDirectiveProcessorSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_processing_without_argument
    {
        private TemplateArgumentDirectiveProcessor _processor;

        /// <summary />
        [FixtureInitializer]
        public void TestInitialize()
        {
            _processor = new TemplateArgumentDirectiveProcessor();
            var codeProvider = new CSharpCodeProvider();
            const string template = "this is a template";
            var errors = new CompilerErrorCollection();

            _processor.StartProcessingRun(codeProvider, template, errors);
        }

        /// <summary />
        [Test]
        public void GetClassCodeForProcessingRun_should_return_empty_string()
        {
            Assert.AreEqual(_processor.GetClassCodeForProcessingRun(), string.Empty);
        }

        /// <summary />
        [Test]
        public void GetPostInitializationCodeForProcessingRun_should_return_empty_string()
        {
            Assert.AreEqual(_processor.GetPostInitializationCodeForProcessingRun(), string.Empty);
        }
    }
}