using System.CodeDom.Compiler;
using Microsoft.CSharp;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateArgumentDirectiveProcessorSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_finishing_processing_run
    {
        /// <summary />
        [Test]
        public void codeProvider_should_do_nothing()
        {
            var processor = new TemplateArgumentDirectiveProcessor();
            processor.FinishProcessingRun();
        }
    }
}