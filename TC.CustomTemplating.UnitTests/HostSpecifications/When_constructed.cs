using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.HostSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_constructed
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
        public void Tranformer_should_not_be_null()
        {
            Assert.IsNotNull(_host.Transformer);
        }

        /// <summary />
        [Test]
        public void PathResolver_should_not_be_null()
        {
            Assert.IsNotNull(_host.PathResolver);
        }

        /// <summary />
        [Test]
        public void FielLoader_should_not_be_null()
        {
            Assert.IsNotNull(_host.FileLoader);
        }

        /// <summary />
        [Test]
        public void StandardImports_should_contain_3_namespaces()
        {
            Assert.AreEqual(_host.StandardImports.Count, 3);
        }

        /// <summary />
        [Test]
        public void StandardImports_should_contain_system()
        {
            Assert.IsTrue(_host.StandardImports.Contains("System"));
        }

        /// <summary />
        [Test]
        public void StandardImports_should_contain_system_codedom()
        {
            Assert.IsTrue(_host.StandardImports.Contains("System.CodeDom"));
        }

        /// <summary />
        [Test]
        public void StandardImports_should_contain_templatingNamespace()
        {
            Assert.IsTrue(_host.StandardImports.Contains("TC.CustomTemplating"));
        }

        /// <summary />
        [Test]
        public void TemplateFile_should_be_an_empty_string()
        {
            Assert.AreEqual(_host.TemplateFile, string.Empty);
        }
    }
}