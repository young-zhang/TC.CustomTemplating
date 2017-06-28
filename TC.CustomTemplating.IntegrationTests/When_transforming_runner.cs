namespace TC.CustomTemplating.IntegrationTests
{
    public abstract class When_transforming_runner
    {
        public virtual void Arrange(ITextTransformer transformer)
        {
        }
        public virtual void Cleanup()
        {
        }

        public abstract string Act(ITextTransformer transformer, string template);
    }
}