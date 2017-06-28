using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.FileLoaderSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_loading_an_existing_file
    {
        const string FileContents = "this is the content of the file";
        const string ResolvedPath = "this is the resolved path";
        
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

            const string requestedFile = "this is the requested file";
            const string transformerpath = "this is this transformerpath";
            const string fileContents = "this is the content of the file";

            resolver.Setup(r => r.ResolvePath(requestedFile, transformerpath)).Returns(ResolvedPath).Verifiable();
            fileSystem.Setup(r => r.FileExists(ResolvedPath)).Returns(true).Verifiable();
            fileSystem.Setup(r => r.ReadAllText(ResolvedPath)).Returns(fileContents).Verifiable();
            
            var fileLoader = new FileLoader(resolver.Object, fileSystem.Object);

            _result = fileLoader.Load(requestedFile, transformerpath, out _content, out _location);
        }

        /// <summary />
        [Test]
        public void it_should_return_true()
        {
            Assert.AreEqual(_result, true);
        }

        /// <summary />
        [Test]
        public void it_should_return_the_resolved_path()
        {
            Assert.AreEqual(_location, ResolvedPath);
        }

        /// <summary />
        [Test]
        public void it_should_return_the_content_of_the_file()
        {
            Assert.AreEqual(_content, FileContents);
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