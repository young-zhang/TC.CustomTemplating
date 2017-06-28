using System;
using System.CodeDom.Compiler;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TextTransformationExceptionSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_constructing_with_errors_and_template_class
    {
        private TextTransformationException _exception;
        private const string TemplateClass = "this is a test";
        private const string Message = "this is the message";
        private CompilerErrorCollection _errors;

        /// <summary />
        [FixtureInitializer]
        public void TestInitialize()
        {
            _errors = new CompilerErrorCollection
                             {
                                 new CompilerError("bla,", 0, 1, "1234", "BlabBlablABl"),
                                 new CompilerError("bla,", 0, 2, "1234", "BlabBlablABl"),
                                 new CompilerError("bla,", 0, 3, "1234", "BlabBlablABl"),
                                 new CompilerError("bla,", 0, 4, "1234", "BlabBlablABl")
                             };
            _exception = new TextTransformationException(Message, _errors, TemplateClass);
        }

        /// <summary />
        [Test]
        public void errors_should_be_the_same()
        {
            Assert.AreSame(_errors, _exception.CompilationErrors);
        }

        /// <summary />
        [Test]
        public void message_should_be_the_same()
        {
            Assert.AreSame(Message, _exception.Message);
        }

        /// <summary />
        [Test]
        public void TemplateClass_should_be_the_same()
        {
            Assert.AreSame(TemplateClass, _exception.TemplateClass);
        }
    }
}