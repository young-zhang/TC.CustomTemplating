using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.TemplateSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_setting_path : TemplateTest
    {
        const string Path = "this is the path";

        protected override void Act()
        {
            Template.Path = Path;
        }

        /// <summary />
        [Test]
        public void TextTranformer_should_be_called()
        {
            TextTransformer.VerifySet(t => t.Path, Path);
        }
    }
}