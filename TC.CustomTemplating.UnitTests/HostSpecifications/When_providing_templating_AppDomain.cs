
using System;
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.HostSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_providing_templating_AppDomain
    {
        private const string Content = "this is the content";

        private readonly Mock<ITextTransformer> _transformer = new Mock<ITextTransformer>();
        private readonly Mock<IFileLoader> _fileLoader = new Mock<IFileLoader>();
        private Host _host;
        private AppDomain _result;
        private AppDomain _appDomain;
        private ClassDefinitionEventArgs _classDefinitionEventArgs;

        /// <summary />
        [FixtureInitializer]
        public void TestInitialize()
        {
            _appDomain = AppDomain.CreateDomain("test");
            _transformer.Setup(t => t.AppDomain).Returns(_appDomain).Verifiable();

            var pathResolver = new Mock<IPathResolver>();
            _host = new Host(_transformer.Object, pathResolver.Object, _fileLoader.Object);
            _host.ClassDefinitionGenerated += ((sender, e) => _classDefinitionEventArgs = e);

            _result = _host.ProvideTemplatingAppDomain(Content);
        }

        /// <summary />
        [FixtureTearDown]
        public void TestCleanup()
        {
            AppDomain.Unload(_appDomain);
        }

        /// <summary />
        [Test]
        public void transformer_should_provide_path()
        {
            _transformer.Verify();
        }

        /// <summary />
        [Test]
        public void result_should_equal_result_returned_by_the_transformer()
        {
            Assert.AreEqual(_result, _appDomain);
        }

        /// <summary />
        [Test]
        public void ClassDefinitionGenerated_should_be_raised()
        {
            Assert.IsNotNull(_classDefinitionEventArgs);
        }

        /// <summary />
        [Test]
        public void ClassDefinitionGeneratedEventArg_should_contain_contents()
        {
            Assert.AreEqual(_classDefinitionEventArgs.ClassDefinition, Content);
        }
    }
}