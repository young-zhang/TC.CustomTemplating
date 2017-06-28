using System;
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.ServiceLocatorSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_resolving_host
    {
        private ITextTransformerHost _textTransformerHost;
        private Host _host;
        private Mock<ITextTransformer> _transformer;

        /// <summary />
        [FixtureInitializer]
        public void Initialize()
        {
            _transformer = new Mock<ITextTransformer>();
            _textTransformerHost = ServiceLocator.Resolve<ITextTransformerHost, ITextTransformer>(_transformer.Object);
            _host = _textTransformerHost as Host;
        }

        /// <summary />
        [Test]
        public void the_host_should_contain_the_transformer()
        {
            Assert.AreEqual(_host.Transformer, _transformer.Object);
        }

        /// <summary />
        [Test]
        public void the_host_should_contain_a_file_loader()
        {
            Assert.IsNotNull(_host.FileLoader);
        }

        /// <summary />
        [Test]
        public void the_host_should_contain_a_path_resolver()
        {
            Assert.IsNotNull(_host.PathResolver);
        }

        /// <summary />
        [Test]
        public void the_path_resolver_should_be_not_null()
        {
            Assert.IsNotNull(_host.PathResolver as PathResolver);
        }

        /// <summary />
        [Test]
        public void the_path_resolver_should_contain_a_file_system()
        {
            var resolver = _host.PathResolver as PathResolver;
            Assert.IsNotNull(resolver);
            Assert.IsNotNull(resolver.FileSystem);
        }

        /// <summary />
        [Test]
        public void the_file_loader_should_be_not_null()
        {
            Assert.IsNotNull(_host.FileLoader as FileLoader);
        }

        /// <summary />
        [Test]
        public void the_file_loader_should_contain_a_file_system()
        {
            var loader = _host.FileLoader as FileLoader;
            Assert.IsNotNull(loader);
            Assert.IsNotNull(loader.FileSystem);
        }
    }
}