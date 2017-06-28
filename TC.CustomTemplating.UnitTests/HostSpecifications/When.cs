using System.CodeDom.Compiler;
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.HostSpecifications
{
    /// <summary />
    [TestFixture]
    public class When
    {
        private Host _host;

        /// <summary />
        [FixtureInitializer]
        public void TestInitialize()
        {
            var transformer = new Mock<ITextTransformer>();
            var fileLoader = new Mock<IFileLoader>();
            var pathResolver = new Mock<IPathResolver>();
            _host = new Host(transformer.Object, pathResolver.Object, fileLoader.Object);
        }

        /// <summary />
        [Test]
        public void resolving_parameter_value_should_return_null()
        {
            Assert.IsNull(_host.ResolveParameterValue("", "", ""));
        }

        /// <summary />
        [Test]
        public void setting_file_extension_should_succeed_without_doing_anything()
        {
            _host.SetFileExtension("jkl");
        }

        /// <summary />
        [Test]
        public void setting_output_encoding_should_succeed_without_doing_anything()
        {
            _host.SetOutputEncoding(null, true);
        }

        /// <summary />
        [Test]
        public void loggin_errors_they_should_be_returned()
        {
            var errors = new CompilerErrorCollection
                             {
                                 new CompilerError("f1", 5, 6, "9", "lklk"),
                                 new CompilerError("f2", 5, 6, "9", "lklk"),
                                 new CompilerError("f3", 5, 6, "9", "lklk"),
                                 new CompilerError("f4", 5, 6, "9", "lklk")
                             };

            _host.LogErrors(errors);

            Assert.AreEqual(_host.Errors, errors);
        }

        /// <summary />
        [Test]
        public void logging_no_errors_null_should_be_returned()
        {
            var errors = new CompilerErrorCollection
                             {
                                 new CompilerError("f1", 5, 6, "9", "lklk"),
                                 new CompilerError("f2", 6, 5, "4", "lklk"),
                                 new CompilerError("f3", 7, 7, "5", "lklk"),
                                 new CompilerError("f4", 8, 3, "1", "lklk")
                             };

            _host.LogErrors(errors);

            Assert.AreEqual(_host.Errors, errors); _host.LogErrors(null);

            Assert.AreEqual(_host.Errors, null);
        }
    }
}