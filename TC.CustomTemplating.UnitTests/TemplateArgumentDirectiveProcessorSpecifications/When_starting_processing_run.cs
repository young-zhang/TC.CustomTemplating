using System.CodeDom.Compiler;
using Microsoft.CSharp;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateArgumentDirectiveProcessorSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_starting_processing_run
    {
        /// <summary />
        [Test]
        public void codeProvider_should_be_assigned()
        {
            var processor = new TemplateArgumentDirectiveProcessor();
            var codeProvider = new CSharpCodeProvider();
            const string template = "this is a template";
            var errors = new CompilerErrorCollection();

            processor.StartProcessingRun(codeProvider, template,  errors);

            Assert.AreEqual(codeProvider, processor.CodeProvider);
        }
    }
}