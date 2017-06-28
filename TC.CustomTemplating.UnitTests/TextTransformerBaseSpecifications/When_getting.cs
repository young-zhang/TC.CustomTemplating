using System.Linq;
using System.Reflection;
using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.TextTransformerBaseSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_getting
    {
        /// <summary />
        [Test]
        public void AssemblyReference_it_should_return_assemblies_of_current_AppDomain()
        {
            var host = new Mock<ITextTransformerHost>();
            var engine = new Mock<ITextTemplatingEngine>();

            var transformer = new TextTransformerImplementation(host.Object, engine.Object);

            Assembly[] actual = transformer.AssemblyReferences.ToArray();
            Assembly[] expected = transformer.AppDomain.GetAssemblies();

            Assert.AreEqual(actual.Length, expected.Length);
        }

        [Test]
        public void Path_the_assigned_should_be_returned()
        {
            var host = new Mock<ITextTransformerHost>();
            var engine = new Mock<ITextTemplatingEngine>();

            var transformer = new TextTransformerImplementation(host.Object, engine.Object);

            var expected = "this is a new path";
            transformer.Path = expected;
            var actual = transformer.Path;

            Assert.AreEqual(actual, expected);
        }
    }
}
