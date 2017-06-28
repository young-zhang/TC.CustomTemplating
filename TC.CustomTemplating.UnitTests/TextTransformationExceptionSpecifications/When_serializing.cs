using Moq;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TextTransformationExceptionSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_serializing
    {
        private const string TemplateClass = "this is a test";
        private const string Message = "this is a message";
        private SerializationInfo _info;
        private CompilerErrorCollection _errors;

        /// <summary />
        [FixtureInitializer]
        public void TestInitialized()
        {
            var converter = new Mock<IFormatterConverter>();
            _info = new SerializationInfo(typeof(TextTransformationException), converter.Object);

            _errors = new CompilerErrorCollection
                             {
                                 new CompilerError("bla,", 0, 1, "1234", "BlabBlablABl"),
                                 new CompilerError("bla,", 0, 2, "1234", "BlabBlablABl"),
                                 new CompilerError("bla,", 0, 3, "1234", "BlabBlablABl"),
                                 new CompilerError("bla,", 0, 4, "1234", "BlabBlablABl")
                             };

            var ex = new TextTransformationException(Message, _errors, TemplateClass);
            ex.GetObjectData(_info, new StreamingContext());
        }

        /// <summary />
        [Test]
        public void SerializationInfo_should_contain_the_compilation_errors()
        {
            var serializedErrors = _info.GetValue("compilationError", typeof(CompilerErrorCollection));
            Assert.AreEqual(_errors, serializedErrors);
        }
        /// <summary />
        [Test]
        public void SerializationInfo_should_contain_the_message()
        {
            var message = _info.GetString("Message");
            Assert.AreEqual(message, message);
        }
        /// <summary />
        [Test]
        public void SerializationInfo_should_contain_the_templateClass()
        {
            var serializedTemplateClass = _info.GetString("templateClass");
            Assert.AreEqual(TemplateClass, serializedTemplateClass);
        }
    }
}