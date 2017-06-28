using System;
using System.CodeDom.Compiler;
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.HostSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_resolving_directive_processor
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
        public void with_name_PropertyProcessor_should_return_TemplateArgumentDirectiveProcessor()
        {
            Assert.AreEqual(typeof(TemplateArgumentDirectiveProcessor),
                _host.ResolveDirectiveProcessor("PropertyProcessor"));

        }

        /// <summary />
        [Test]
        public void with_name_propertyProcessor_should_return_TemplateArgumentDirectiveProcessor()
        {
            Assert.AreEqual(typeof(TemplateArgumentDirectiveProcessor),
                _host.ResolveDirectiveProcessor("propertyProcessor"));

        }

        /// <summary />
        [Test]
        public void with_name_PROPERTYPROCESSOR_should_return_TemplateArgumentDirectiveProcessor()
        {
            Assert.AreEqual(typeof(TemplateArgumentDirectiveProcessor),
                _host.ResolveDirectiveProcessor("PROPERTYPROCESSOR"));

        }

        /// <summary />
        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void with_invalid_name_should_throw_NotSupportedException()
        {
            Assert.AreEqual(typeof(TemplateArgumentDirectiveProcessor),
                _host.ResolveDirectiveProcessor("invalid"));

        }

        /// <summary />
        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void with_null_name_should_throw_NotSupportedException()
        {
            Assert.AreEqual(typeof(TemplateArgumentDirectiveProcessor),
                _host.ResolveDirectiveProcessor(null));

        }


        /// <summary />
        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void with_empty_name_should_throw_NotSupportedException()
        {
            Assert.AreEqual(typeof(TemplateArgumentDirectiveProcessor),
                _host.ResolveDirectiveProcessor(string.Empty));

        }
    }
}