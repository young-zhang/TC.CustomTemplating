using System;
using MbUnit.Framework;
using TC.CustomTemplating.IntegrationTests.Model;

namespace TC.CustomTemplating.IntegrationTests
{
    [TestFixture]
    public class When_transforming_invalid_template_runner : When_transforming_runner
    {
        public override string Act(ITextTransformer transformer, string template)
        {
            return transformer.Transform(template);
        }
    }
}