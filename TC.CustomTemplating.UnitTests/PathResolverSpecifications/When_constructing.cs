using System;
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.PathResolverSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_constructing
    {
        /// <summary />
        [Test]
        public void should_succeed_when_filsystem_is_given()
        {
            var fileSystem = new Mock<IFileSystem>();
            new PathResolver(fileSystem.Object);
        }

        /// <summary />
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void should_throw_ArgumentNullException_when_null_is_given()
        {
            new PathResolver(null);
        }
    }
}