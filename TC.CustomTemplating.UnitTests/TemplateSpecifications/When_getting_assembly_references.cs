using System.Collections.Generic;
using System.Reflection;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_getting_assembly_references : TemplateTest
    {
        private readonly IList<Assembly> _references = new List<Assembly>
                               { typeof(When_getting_assembly_references).Assembly };
        private IList<Assembly> _result;

        protected override void Arrange()
        {
            base.Arrange();

            TextTransformer.SetupGet(t => t.AssemblyReferences).Returns(_references);
        }

        protected override void Act()
        {
            _result = Template.AssemblyReferences;
        }

        /// <summary />
        [Test]
        public void TextTranformer_should_be_called()
        {
            TextTransformer.VerifyGet(t => t.AssemblyReferences);
        }

        /// <summary />
        [Test]
        public void result_returned_by_the_TextTranformer_should_be_returned()
        {
            Assert.AreEqual(_references, _result);
        }
    }
}