
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.HostSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_resolving_assembly
    {
        private const string Result = "this is the Result";
        private const string Path = "this is the Path";
        private const string TransformerPath = "TransformerPath";

        private readonly Mock<IPathResolver> _pathResolver = new Mock<IPathResolver>();
        private readonly Mock<ITextTransformer> _transformer = new Mock<ITextTransformer>();
        private readonly Mock<IFileLoader> _fileLoader = new Mock<IFileLoader>();
        private Host _host;
        private string _result;

        /// <summary />
        [FixtureInitializer]
        public void TestInitialize()
        {
            _transformer.Setup(t => t.Path).Returns(TransformerPath).Verifiable();
            _pathResolver.Setup(p => p.ResolvePath(Path, TransformerPath)).Returns(Result).Verifiable();

            _host = new Host(_transformer.Object, _pathResolver.Object, _fileLoader.Object);
            _result = _host.ResolveAssemblyReference(Path);
        }

        /// <summary />
        [Test]
        public void transformer_should_provide_path()
        {
            _transformer.Verify();
        }

        /// <summary />
        [Test]
        public void pathResolver_should_be_called()
        {
            _pathResolver.Verify();
        }

        /// <summary />
        [Test]
        public void result_should_equal_result_returned_by_the_fileLoader()
        {
            Assert.AreEqual(_result, Result);
        }
    }
}