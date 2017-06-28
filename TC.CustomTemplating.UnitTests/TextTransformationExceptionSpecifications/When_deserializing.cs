using Moq;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TextTransformationExceptionSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_deserializing
    {
        public class DeserializableTextTransformationException : TextTransformationException
        {
            public DeserializableTextTransformationException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }

        private CompilerErrorCollection _errors;
        private const string Template = "this is a template";
        private TextTransformationException _result;

        /// <summary />
        [FixtureInitializer]
        public void TestInitialize()
        {
            var converter = new Mock<IFormatterConverter>();
            SerializationInfo info = new SerializationInfo(typeof(DeserializableTextTransformationException), converter.Object);

            _errors = new CompilerErrorCollection
                             {
                                 new CompilerError("bla,", 0, 1, "1234", "BlabBlablABl"),
                                 new CompilerError("bla,", 0, 2, "1234", "BlabBlablABl"),
                                 new CompilerError("bla,", 0, 3, "1234", "BlabBlablABl"),
                                 new CompilerError("bla,", 0, 4, "1234", "BlabBlablABl")
                             };

            info.AddValue("compilationError", _errors);
            info.AddValue("templateClass", Template);

            //next values are neccessary for Exception base class
            info.AddValue("ClassName", "ThisIsAClass");
            info.AddValue("Message", "ThisIsAMessage");
            info.AddValue("InnerException", null);
            info.AddValue("HelpURL", string.Empty);
            info.AddValue("StackTraceString", string.Empty);
            info.AddValue("RemoteStackTraceString", string.Empty);
            info.AddValue("RemoteStackIndex", 6);
            info.AddValue("ExceptionMethod", string.Empty);
            info.AddValue("Source", string.Empty);
            info.AddValue("HResult", 1234);

            _result = new DeserializableTextTransformationException(info, new StreamingContext());
        }

        [Test]
        public void CompilationErrors_should_be_equal()
        {
            Assert.AreEqual(_errors, _result.CompilationErrors);
        }

        [Test]
        public void TemplateClass_should_be_equal()
        {
            Assert.AreEqual(Template, _result.TemplateClass);
        }
    }
}