using System;
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.FileSystemSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_constructing
    {
        /// <summary />
        [Test]
        public void should_succeed()
        {
            new FileSystem();
        }
    }
}