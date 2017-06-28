using System;
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.HostSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_getting_host_options
    {
        /// <summary />
        [Test]
        public void should_return_true_when_CacheAssemblies_is_given()
        {
            var transformer = new Mock<ITextTransformer>();
            var fileLoader = new Mock<IFileLoader>();
            var pathResolver = new Mock<IPathResolver>();
            var host = new Host(transformer.Object, pathResolver.Object, fileLoader.Object);
            
            Assert.IsTrue((bool)host.GetHostOption("CacheAssemblies"));
        }
        
        /// <summary />
        [Test]
        public void should_return_null_when_unknown_option_is_given()
        {
            var transformer = new Mock<ITextTransformer>();
            var fileLoader = new Mock<IFileLoader>();
            var pathResolver = new Mock<IPathResolver>();
            var host = new Host(transformer.Object, pathResolver.Object, fileLoader.Object);
            
            Assert.IsNull(host.GetHostOption("bnlbalbla"));
        }
        
        /// <summary />
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_ArgumentNullException_when_null_is_given()
        {
            var transformer = new Mock<ITextTransformer>();
            var fileLoader = new Mock<IFileLoader>();
            var pathResolver = new Mock<IPathResolver>();
            var host = new Host(transformer.Object, pathResolver.Object, fileLoader.Object);
            
            Assert.IsNull(host.GetHostOption(null));
        }
    }
}