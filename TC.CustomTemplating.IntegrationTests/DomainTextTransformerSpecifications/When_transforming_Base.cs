
namespace TC.CustomTemplating.IntegrationTests.DomainTextTransformerSpecifications
{
    public abstract class When_transforming_Base : When_transforming
    {
        protected When_transforming_Base(When_transforming_runner runner)
            : base(runner)
        {
        }

        protected override ITextTransformer CreateTransformer()
        {
            return new DomainTextTransformer();
        }
    }
}