using System;
using System.Collections.Generic;
using System.Reflection;
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.HostSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_getting_StandardAssemblyReferences
    {
        private IList<string> _locations;

        /// <summary />
        [FixtureInitializer]
        public void TestInitialize()
        {
            var transformer = new Mock<ITextTransformer>();
            var fileLoader = new Mock<IFileLoader>();
            var pathResolver = new Mock<IPathResolver>();
            var assemblies = new List<Assembly>();
            assemblies.Add(typeof(Guid).Assembly);
            transformer.Setup(t => t.AssemblyReferences).Returns(assemblies);

            var host = new Host(transformer.Object, pathResolver.Object, fileLoader.Object);
            _locations = host.StandardAssemblyReferences;
        }

        /// <summary />
        [Test]
        public void locations_of_assemblies_returned_by_should_be_returned()
        {
            Assert.AreEqual(_locations.Count, 1);
        }
    }
}
