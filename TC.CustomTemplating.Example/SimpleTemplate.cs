using System;

namespace TC.CustomTemplating.Example
{
    internal static class SimpleTemplate
    {
        internal static void Transform()
        {
            //Get template from the embedded resources
            string template = TemplateResources.Get("TC.CustomTemplating.Example.ClassFeatures.tt", typeof(ClassTemplate));

            //Allows us to show the generated class
            var transformer = new TextTransformer();
            transformer.ClassDefinitionGenerated += Host_ClassDefinitionGenerated;

            //start the tranformation in th current appdomain
            var output = transformer.Transform(template);

            Console.WriteLine("--BEGIN OUTPUT--");
            Console.WriteLine(output);
            Console.WriteLine("--END OUTPUT--");

            transformer.ClassDefinitionGenerated -= Host_ClassDefinitionGenerated;
        }

        static void Host_ClassDefinitionGenerated(object sender, ClassDefinitionEventArgs e)
        {
            Console.WriteLine("--BEGIN COMPILED CLASS--");
            Console.WriteLine(e.ClassDefinition);
            Console.WriteLine("--END COMPILED CLASS--");
        }
    }
}
