
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.HostSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_loading_include_text
    {
        private const bool Result = true;

        private const string RequestedFile = "RequestedFile";
        private const string TransformerPath = "TransformerPath";
        private const string Content = "this is the file Content";
        private const string Location = "this is the file Location";

        private readonly Mock<IFileLoader> _fileLoader = new Mock<IFileLoader>();
        private readonly Mock<ITextTransformer> _transformer = new Mock<ITextTransformer>();
        private Host _host;
        private bool _result;
        private string _content;
        private string _location;

        /// <summary />
        [FixtureInitializer]
        public void TestInitialize()
        {
            var content = Content;
            var location = Location;
            _transformer.Setup(t => t.Path).Returns(TransformerPath).Verifiable();
            _fileLoader.Setup(f => f.Load(RequestedFile, TransformerPath, out content, out location)).Returns(Result).Verifiable();

            var pathResolver = new Mock<IPathResolver>();
            _host = new Host(_transformer.Object, pathResolver.Object, _fileLoader.Object);
            _result = _host.LoadIncludeText(RequestedFile, out _content, out _location);
        }

        /// <summary />
        [Test]
        public void transformer_should_provide_path()
        {
            _fileLoader.Verify();
        }

        /// <summary />
        [Test]
        public void fileLoader_should_load_the_file()
        {
            _fileLoader.Verify();
        }

        /// <summary />
        [Test]
        public void result_should_equal_result_returned_by_the_fileLoader()
        {
            Assert.AreEqual(_result, Result);
        }

        /// <summary />
        [Test]
        public void location_should_equal_location_returned_by_the_fileLoader()
        {
            Assert.AreEqual(_location, Location);
        }

        /// <summary />
        [Test]
        public void content_should_equal_content_returned_by_the_fileLoader()
        {
            Assert.AreEqual(_content, Content);
        }
    }
}