using System;

namespace TC.CustomTemplating.Example
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Transform a very simple template\n\r");
            SimpleTemplate.Transform();

            Console.WriteLine("Transform examples from documentation\n\r");
            DocumentationExamples.Transform();

            Console.WriteLine("\n\r\n\rTransform the ClassDefinition template in current app-domain\n\r");
            ClassTemplate.Transform(new TextTransformer());

            Console.WriteLine("\n\r\n\rTransform the ClassDefinition template in new app-domain\n\r");
            using (var domain = new DomainTextTransformer())
            {
                ClassTemplate.Transform(domain);
            }

            Console.WriteLine("\n\r\n\rMeasure the transformation of the ClassDefinition template\n\r");
            ClassTemplate.Measure();

            Console.WriteLine("Transform a template that includes a template from EntryAssembly path\n\r");
            IncludeTemplate.Transform();

            Console.ReadKey();
        }  
    }
}
