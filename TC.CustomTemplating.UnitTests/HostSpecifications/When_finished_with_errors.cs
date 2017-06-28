using System.CodeDom.Compiler;
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.HostSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_When_finished_with_errors
    {
        /// <summary />
        [Test]
        [ExpectedException(typeof(TextTransformationException))]
        public void a_TextTransformationException_should_be_thrown()
        {
            var transformer = new Mock<ITextTransformer>();
            var fileLoader = new Mock<IFileLoader>();
            var pathResolver = new Mock<IPathResolver>();
            var host = new Host(transformer.Object, pathResolver.Object, fileLoader.Object);
            var errors = new CompilerErrorCollection
                         {
                             new CompilerError("f1", 5, 6, "9", "lklk"),
                             new CompilerError("f2", 5, 6, "9", "lklk"),
                             new CompilerError("f3", 5, 6, "9", "lklk"),
                             new CompilerError("f4", 5, 6, "9", "lklk")
                         };
            host.LogErrors(errors);

            host.Finish(null);
        }
    }
}