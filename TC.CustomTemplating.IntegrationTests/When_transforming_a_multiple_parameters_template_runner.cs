using MbUnit.Framework;

namespace TC.CustomTemplating.IntegrationTests
{
    [TestFixture]
    public class When_transforming_a_multiple_parameters_template_runner : When_transforming_runner
    {
        public override string Act(ITextTransformer transformer, string template)
        {
            var arguments = new TemplateArgumentCollection
                                {
                                    new TemplateArgument("Argument1", "This is argument1 Value!"),
                                    new TemplateArgument{Name = "Argument2", Value= "This is argument2 Value!"},
                                    new TemplateArgument("Argument3", "This is argument3 Value!")
                                };
            return transformer.Transform(template, arguments);
        }
    }
}