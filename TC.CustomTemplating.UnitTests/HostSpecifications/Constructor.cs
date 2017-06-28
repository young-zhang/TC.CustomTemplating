using System;
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.HostSpecifications
{
    /// <summary />
    [TestFixture]
    public class Constructor_
    {
        /// <summary />
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_ArgumentNullException_when_null_is_given_as_transformer()
        {
            var fileLoader = new Mock<IFileLoader>();
            var pathResolver = new Mock<IPathResolver>();
            new Host(null, pathResolver.Object, fileLoader.Object);
        }

        /// <summary />
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_ArgumentNullException_when_null_is_given_as_file_loader()
        {
            var transformer = new Mock<ITextTransformer>();
            var pathResolver = new Mock<IPathResolver>();
            new Host(transformer.Object, pathResolver.Object, null);
        }

        /// <summary />
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_ArgumentNullException_when_null_is_given_as_path_resolver()
        {
            var transformer = new Mock<ITextTransformer>();
            var fileLoader = new Mock<IFileLoader>();
            new Host(transformer.Object, null, fileLoader.Object);
        }

        /// <summary />
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_ArgumentNullException_when_null_is_given()
        {
            new Host(null, null, null);
        }
    }
}