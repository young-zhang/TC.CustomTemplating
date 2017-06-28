using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateArgumentDirectiveProcessorSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_constructing
    {
        /// <summary />
        [Test]
        public void should_succeed()
        {
            new TemplateArgumentDirectiveProcessor();
        }
    }
}