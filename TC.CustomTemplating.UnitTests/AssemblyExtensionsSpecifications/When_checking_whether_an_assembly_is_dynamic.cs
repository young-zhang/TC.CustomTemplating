using System;
using System.Reflection.Emit;
using MbUnit.Framework;
using System.Reflection;

namespace TC.CustomTemplating.UnitTests.AssemblyExtensionsSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_checking_whether_an_assembly_is_dynamic
    {
        /// <summary />
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgrumentNullException_should_be_thrown_when_assembly_is_null()
        {
            AssemblyExtensions.IsDynamic(null);
        }

        /// <summary />
        [Test]
        public void false_should_be_returned_when_assembly_static()
        {
            Assembly assembly = typeof(Guid).Assembly;

            Assert.IsFalse(AssemblyExtensions.IsDynamic(assembly));
        }

        /// <summary />
        [Test]
        public void true_should_be_returned_when_assembly_dynamic()
        {
            var assemblyName = new AssemblyName("dynamic");var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            var assembly = assemblyBuilder.DefineDynamicModule("test").Assembly;

            Assert.IsTrue(AssemblyExtensions.IsDynamic(assembly));
        }
    }
}