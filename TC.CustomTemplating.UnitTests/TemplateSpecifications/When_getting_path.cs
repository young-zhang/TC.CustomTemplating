using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_getting_path : TemplateTest
    {
        const string Path = "this is a path";
        private string _result;

        protected override void Arrange()
        {
            base.Arrange();

            TextTransformer.SetupGet(t => t.Path).Returns(Path);
        }
        
        protected override void Act()
        {
            _result = Template.Path;
        }

        /// <summary />
        [Test]
        public void TextTranformer_should_be_called()
        {
            TextTransformer.VerifyGet(t => t.Path);
        }

        /// <summary />
        [Test]
        public void result_returned_by_the_TextTranformer_should_be_returned()
        {
            Assert.AreEqual(Path, _result);
        }
    }
}