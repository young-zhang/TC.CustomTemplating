using System;
using System.CodeDom;
using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.DomainTextTransformerSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_constructing_with_arguments
    {
        private DomainTextTransformer _transformer;
        private AppDomain _appDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString());
        private Mock<IAppDomainManager> _appDomainManager = new Mock<IAppDomainManager>();

        /// <summary />
        [FixtureInitializer]
        public void Initialize()
        {
            var host = new Mock<ITextTransformerHost>();
            var engine = new Mock<ITextTemplatingEngine>();

            _appDomainManager.Setup(a => a.Create(It.IsAny<string>())).Returns(_appDomain).Verifiable();

            _transformer = new DomainTextTransformer(_appDomainManager.Object, host.Object, engine.Object);
        }
        /// <summary />
        [FixtureTearDown]
        public void CleanUp()
        {
            _transformer.Dispose();
            AppDomain.Unload(_appDomain);
        }

        /// <summary />
        [Test]
        public void AppDomainManager_should_be_called_to_Create_appDomain()
        {
            _appDomainManager.Verify();
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