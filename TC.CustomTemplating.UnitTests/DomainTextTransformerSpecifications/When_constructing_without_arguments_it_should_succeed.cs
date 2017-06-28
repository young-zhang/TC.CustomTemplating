using System.CodeDom;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.DomainTextTransformerSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_constructing_without_arguments_it_should_succeed
    {
        private DomainTextTransformer _transformer;

        /// <summary />
        [FixtureInitializer]
        public void Initialize()
        {
            _transformer = new DomainTextTransformer();
        }

        /// <summary />
        [FixtureTearDown]
        public void CleanUp()
        {
            _transformer.Dispose();
        }

        /// <summary />
        [Test]
        public void appDomain_should_be_initialized()
        {
            Assert.IsNotNull(_transformer.AppDomain);
        }

        /// <summary />
        [Test]
        public void AutoRecycle_should_be_false()
        {
            Assert.IsFalse(_transformer.AutoRecycle);
        }

        /// <summary />
        [Test]
        public void RecycleThreshold_should_be_25()
        {
            Assert.AreEqual(25, _transformer.RecycleThreshold);
        }

        /// <summary />
        [Test]
        public void AssemblyReferences_should_contain_system_dll()
        {
            Assert.IsTrue(_transformer.AssemblyReferences.Contains(typeof(CodeCompileUnit).Assembly));
        }

        /// <summary />
        [Test]
        public void AssemblyReferences_should_contain_mscorlib()
        {
            Assert.IsTrue(_transformer.AssemblyReferences.Contains(typeof(CodeCompileUnit).Assembly));
        }

        /// <summary />
        [Test]
        public void AssemblyReferences_should_contain_tc_customTemplating_dll()
        {
            Assert.IsTrue(_transformer.AssemblyReferences.Contains(typeof(TemplateArgument).Assembly));
        }

        /// <summary />
        [Test]
        public void AssemblyReferences_should_contain_3_assemblies()
        {
            Assert.AreEqual(_transformer.AssemblyReferences.Count, 3);
        }
    }
}