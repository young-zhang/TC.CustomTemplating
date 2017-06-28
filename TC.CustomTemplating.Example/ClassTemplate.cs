using System;
using System.Diagnostics;
using TC.CustomTemplating.Example.Domain;

namespace TC.CustomTemplating.Example
{
    internal static class ClassTemplate
    {
        internal static void Transform(TextTransformerBase transformer)
        {
            //Prepare the argument that will be passes to the template
            var class1 = new ClassDefinition
            {
                Name = "TestClass",
                Namespace = "TC.CustomTemplating.Example.Generated"
            };
            class1.Properties.Add(new Property("Name", typeof(string)));
            class1.Properties.Add(new Property("Lenght", typeof(int)));
            class1.Properties.Add(new Property("L", typeof(int)));

            //Get template from the embedded resources
            string template = TemplateResources.Get("TC.CustomTemplating.Example.Class.tt", typeof(ClassTemplate));

            //Allows us to show the compiled class
            transformer.ClassDefinitionGenerated += Host_ClassDefinitionGenerated;

            //start the tranformation
            var output = transformer.Transform(template, "Class", class1);

            Console.WriteLine("--BEGIN OUTPUT--");
            Console.WriteLine(output);
            Console.WriteLine("--END OUTPUT--");

            transformer.ClassDefinitionGenerated -= Host_ClassDefinitionGenerated;
        }

        internal static void Measure()
        {
            //Prepare the argument that will be passes to the template
            var class1 = new ClassDefinition
                             {
                                 Name = "TestClass",
                                 Namespace = "TC.CustomTemplating.Example.Generated"
                             };
            class1.Properties.Add(new Property("Name", typeof(string)));
            class1.Properties.Add(new Property("Lenght", typeof(int)));
            class1.Properties.Add(new Property("L", typeof(int)));

            double min = double.MaxValue;
            double max = 0;
            double first = double.MinValue;
            const int numberOfTransformations = 100;

            //Get template from the embedded resources
            string template = TemplateResources.Get("TC.CustomTemplating.Example.Class.tt", typeof(ClassTemplate));

            //Create a new domain wherein the transformation will
            //take place
            using (var transformer = new DomainTextTransformer())
            {
                //We try to transform it n times
                Stopwatch watch = new Stopwatch();
                for (int i = 0; i < numberOfTransformations; i++)
                {
                    //start the tranformation
                    watch.Reset();
                    watch.Start();
                    transformer.Transform(template, "Class", class1);
                    watch.Stop();

                    //Measure
                    var milliseconds = watch.ElapsedMilliseconds;
                    if (first == double.MinValue)
                    {
                        first = milliseconds;
                    }
                    else
                    {
                        max = Math.Max(max, milliseconds);
                    }
                    min = Math.Min(min, milliseconds);
                }
            }

            Console.WriteLine("--BEGIN MEASUREMENT RESULTS--");
            Console.WriteLine("Number Of Transformations: {0}", numberOfTransformations);
            Console.WriteLine("First Time: {0}ms", first);
            Console.WriteLine("Min Time: {0}ms", min);
            Console.WriteLine("Max Time: {0}ms  (without first time)", max);
            Console.WriteLine("--END MEASUREMENT RESULTS--");
        }

        static void Host_ClassDefinitionGenerated(object sender, ClassDefinitionEventArgs e)
        {
            Console.WriteLine("--BEGIN COMPILED CLASS--");
            Console.WriteLine(e.ClassDefinition);
            Console.WriteLine("--END COMPILED CLASS--");
        }
    }
}



