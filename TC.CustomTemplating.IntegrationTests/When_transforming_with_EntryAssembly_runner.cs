using System.IO;
using System.Reflection;
using MbUnit.Framework;

namespace TC.CustomTemplating.IntegrationTests
{
    //Silly trick to ensure there is a EntryAssembly available
    //(only once possible per test project)
    public class When_transforming_with_EntryAssembly_runner
    {
        private static string _template;
        private static string _expectedResult;
        private static string _actualResult;

        public static void Main()
        {
            VerifyEntryAssembly();
            VerifyTransformationWithInnerTemplate();
        }

        private static void VerifyTransformationWithInnerTemplate()
        {
            var assembly = Assembly.GetEntryAssembly();
            var folder = Path.GetDirectoryName(assembly.Location);
            var file = Path.Combine(folder, "inner.tt");

            File.WriteAllText(file, "This is the inner template!");

            try
            {
                var type = typeof(When_transforming_with_EntryAssembly);

                _template = Templates.Get(type, "tt");
                _expectedResult = Templates.Get(type, "result");

                ITextTransformer transformer = new TextTransformer();
                _actualResult = transformer.Transform(_template);

                Assert.AreEqual(_expectedResult, _actualResult);
            }
            finally
            {
                File.Delete(file);
            }
        }

        private static void VerifyEntryAssembly()
        {
            var assembly = Assembly.GetEntryAssembly();
            Assert.IsNotNull(assembly);
            Assert.IsNotNull(assembly.Location);
        }
    }
}