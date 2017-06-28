using System.Collections.Generic;
using MbUnit.Framework;

namespace TC.CustomTemplating.IntegrationTests
{
    [TestFixture]
    public class When_transforming_a_generic_parameter_template_runner : When_transforming_runner
    {
        public override string Act(ITextTransformer transformer, string template)
        {
            var dictionary = new Dictionary<int, List<string>>
                                 {
                                     { 0, new List<string> { "one", "two", "three" }},
                                     { 4, new List<string> { "four", "five", "six" }},
                                     { 7, new List<string> { "seven", "eight", "nine" }}
                                 };
            return transformer.Transform(template, "Argument", dictionary);
        }
    }
}