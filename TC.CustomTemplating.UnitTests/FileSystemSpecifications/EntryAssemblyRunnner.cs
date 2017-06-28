using System.Reflection;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.FileSystemSpecifications
{
    public class EntryAssemblyRunnner
    {
        public static void Main()
        {
            var fileSystem = new FileSystem();
            var assembly = Assembly.GetEntryAssembly();  //Not available during unit testing

            Assert.IsNotNull(assembly);
            Assert.AreEqual(fileSystem.EntryAssemblyLocation, assembly.Location);
        }
    }
}