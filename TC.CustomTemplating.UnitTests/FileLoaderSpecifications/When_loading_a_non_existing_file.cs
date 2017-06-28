using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.FileLoaderSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_loading_a_non_existing_file
    {
        const string FileContents = "this is the content of the file";
        const string ResolvedPath = "this is the resolved path";
        const string RequestedFile = "this is the requested file";

        private bool _result;
        private string _content;
        private string _location;
        private Mock<IPathResolver> _resolver = new Mock<IPathResolver>();
        private Mock<IFileSystem>  _fileSystem = new Mock<IFileSystem>();

        /// <summary />
        [FixtureInitializer]
        public void Initialize()
        {
            var resolver = new Mock<IPathResolver>();
            var fileSystem = new Mock<IFileSystem>();

            const string transformerpath = "this is this transformerpath";

            resolver.Setup(r => r.ResolvePath(RequestedFile, transformerpath)).Returns(ResolvedPath).Verifiable();
            fileSystem.Setup(r => r.FileExists(RequestedFile)).Returns(false).Verifiable();
            
            var fileLoader = new FileLoader(resolver.Object, fileSystem.Object);

            _result = fileLoader.Load(RequestedFile, transformerpath, out _content, out _location);
        }

        /// <summary />
        [Test]
        public void it_should_return_false()
        {
            Assert.AreEqual(_result, false);
        }

        /// <summary />
        [Test]
        public void it_should_return_null_as_the_requested_path()
        {
            Assert.AreEqual(_location, null);
        }

        /// <summary />
        [Test]
        public void it_should_return_null_as_the_content_of_the_file()
        {
            Assert.AreEqual(_content, null);
        }

        /// <summary />
        [Test]
        public void it_should_call_file_system()
        {
            _fileSystem.Verify();
        }

        /// <summary />
        [Test]
        public void it_should_call_path_resolver()
        {
            _resolver.Verify();
        }
    }
 }