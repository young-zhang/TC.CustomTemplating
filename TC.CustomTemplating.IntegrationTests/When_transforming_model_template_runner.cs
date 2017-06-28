using MbUnit.Framework;
using TC.CustomTemplating.IntegrationTests.Model;

namespace TC.CustomTemplating.IntegrationTests.TextTransformerSpecifications
{
    [TestFixture]
    public class When_transforming_model_template_runner : When_transforming_runner
    {
        public override void Arrange(ITextTransformer transformer)
        {
            base.Arrange(transformer);
        }

        public override string Act(ITextTransformer transformer, string template)
        {
            //Prepare the argument that will be passes to the template
            var classDefinition = new ClassDefinition
            {
                Name = "TestClass",
                Namespace = "TC.CustomTemplating.Example.Generated"
            };
            classDefinition.Properties.Add(new Property("Name", typeof(string)));
            classDefinition.Properties.Add(new Property("Lenght", typeof(int)));
            classDefinition.Properties.Add(new Property("L", typeof(int)));

            return transformer.Transform(template, "Class", classDefinition);
        }
    }
}