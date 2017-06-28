using System.IO;
using MbUnit.Framework;

namespace TC.CustomTemplating.IntegrationTests
{
    [TestFixture]
    public class When_transforming_an_invalid_nested_template_runner : When_transforming_runner
    {
        public override string Act(ITextTransformer transformer, string template)
        {
            return transformer.Transform(template);
        }
    }
}