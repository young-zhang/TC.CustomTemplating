using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using Microsoft.CSharp;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateArgumentDirectiveProcessorSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_processing
    {
        /// <summary />
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void without_name_parameter_should_fail()
        {
            var processor = new TemplateArgumentDirectiveProcessor();
            var codeProvider = new CSharpCodeProvider();
            const string template = "this is a template";
            var errors = new CompilerErrorCollection();

            processor.StartProcessingRun(codeProvider, template, errors);
            var dictionary = new Dictionary<string, string>
                                 {
                                     {"type", "MyCustomType"},
                                     {"converter", "MyCustomConverter"},
                                     {"editor", "MyCustomEditor"}
                                 };
            processor.ProcessDirective("argument1", dictionary);
        }

        /// <summary />
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void without_type_parameter_should_fail()
        {
            var processor = new TemplateArgumentDirectiveProcessor();
            var codeProvider = new CSharpCodeProvider();
            const string template = "this is a template";
            var errors = new CompilerErrorCollection();

            processor.StartProcessingRun(codeProvider, template, errors);
            var dictionary = new Dictionary<string, string>
                                 {
                                     {"name", "MyCustomName"},
                                     {"converter", "MyCustomConverter"},
                                     {"editor", "MyCustomEditor"}
                                 };
            processor.ProcessDirective("argument1", dictionary);
        }


        /// <summary />
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void with_duplicated_argument_it_should_fail()
        {
            var processor = new TemplateArgumentDirectiveProcessor();
            var codeProvider = new CSharpCodeProvider();
            const string template = "this is a template";
            var errors = new CompilerErrorCollection();

            processor.StartProcessingRun(codeProvider, template, errors);
            var dictionary = new Dictionary<string, string>
                                 {
                                     {"type", "MyCustomType"},
                                     {"name", "MyCustomName"},
                                     {"converter", "MyCustomConverter"},
                                     {"editor", "MyCustomEditor"}
                                 };
            processor.ProcessDirective("argument1", dictionary);
            processor.ProcessDirective("argument1", dictionary);
        }
    }
}