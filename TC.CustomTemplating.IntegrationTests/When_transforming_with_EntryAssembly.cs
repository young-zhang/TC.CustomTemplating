using System;
using System.IO;
using MbUnit.Framework;

namespace TC.CustomTemplating.IntegrationTests
{
    [TestFixture]
    [Serializable]
    public class When_transforming_with_EntryAssembly
    {
        //WARNING When test fails in the ReSharper test runner disable the "Shadow-copy assemblies being tested" feature
        [Test]
        public void the_include_form_the_EntryAssembly_location_should_be_included()
        {
            //Silly trick to ensure there is a EntryAssembly available
            var folder = Path.GetDirectoryName(typeof(TextTransformer).Assembly.Location);
            var domain = AppDomain.CreateDomain("test", AppDomain.CurrentDomain.Evidence, folder, folder, false);
            try
            {
                domain.ExecuteAssembly(typeof(When_transforming_with_EntryAssembly_runner).Assembly.Location);
            }         
            finally
            {
                AppDomain.Unload(domain);
            }
        }
    }
}