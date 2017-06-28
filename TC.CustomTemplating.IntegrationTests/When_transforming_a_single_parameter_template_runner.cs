using MbUnit.Framework;

namespace TC.CustomTemplating.IntegrationTests
{
    [TestFixture]
    public class When_transforming_a_single_parameter_template_runner : When_transforming_runner
    {
        public override string Act(ITextTransformer transformer, string template)
        {
            return transformer.Transform(template, "Argument", "This is the argument value");
        }
    }
}