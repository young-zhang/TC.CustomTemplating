using Moq;

namespace TC.CustomTemplating.UnitTests.TemplateSpecifications
{
    public abstract class TemplateTest : TestBase
    {
        protected Mock<ITextTransformer> TextTransformer { get; private set; }
        protected ITextTransformer _previousTextTransformer;

        protected override void Arrange()
        {
            TextTransformer = new Mock<ITextTransformer>();

            _previousTextTransformer = Template.TextTransformer;
            Template.TextTransformer = TextTransformer.Object;
        }

        public override void CleanUp()
        {
            base.CleanUp();

            Template.TextTransformer = _previousTextTransformer;
        }
    }
}
