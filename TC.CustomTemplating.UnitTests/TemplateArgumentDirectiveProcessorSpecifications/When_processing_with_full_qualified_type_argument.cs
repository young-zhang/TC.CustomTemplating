using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CSharp;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateArgumentDirectiveProcessorSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_processing_with_full_qualified_type_argument :  When_processing_Base
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
            var dictionary = new Dictionary<string, string>
                                 {
                                     {"type", "MyCustomType, assembly, some other shit"},
                                     {"name", "MyCustomName"},
                                     {"converter", "MyCustomConverter"},
                                     {"editor", "MyCustomEditor"}
                                 };
            _processor.ProcessDirective("argument1", dictionary);
        }

        /// <summary />
        [Test]
        public void GetClassCodeForProcessingRun_should_return_empty_string()
        {
            string expected = GetResourceCode("Class");
            string actual = _processor.GetClassCodeForProcessingRun();

            Assert.AreEqual(expected, actual);
        }

        /// <summary />
        [Test]
        public void GetPostInitializationCodeForProcessingRun_should_return_empty_string()
        {
            string expected = GetResourceCode("Initialization");
            string actual = _processor.GetPostInitializationCodeForProcessingRun();

            Assert.AreEqual(expected, actual);
        }
    }
}