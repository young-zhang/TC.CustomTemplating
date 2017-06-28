using System;
using System.CodeDom;
using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.DomainTextTransformerSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_transforming_a_parameterless_template
    {
        private DomainTextTransformer _transformer;
        private AppDomain _appDomain;
        private readonly Mock<IAppDomainManager> _appDomainManager = new Mock<IAppDomainManager>();
        private readonly Mock<ITextTransformerHost> _host = new Mock<ITextTransformerHost>();
        private readonly Mock<ITextTemplatingEngine> _engine = new Mock<ITextTemplatingEngine>();
        private string _result;
        const string Template = "this is a template";
        const string Result = "this is a result";

        /// <summary />
        [FixtureInitializer]
        public void with_arguments_it_should_call_appDomainManager()
        {
            //Arrange
            _appDomain = AppDomain.CreateDomain(new Guid().ToString());
            _appDomainManager.Setup(a => a.Create(It.IsAny<string>())).Returns(_appDomain);
            _engine.Setup(e => e.ProcessTemplate(Template, _host.Object)).Returns(Result);

            //Act
            _transformer = new DomainTextTransformer(_appDomainManager.Object, _host.Object, _engine.Object);
            _result = _transformer.Transform(Template);
        }

        /// <summary />
        [FixtureTearDown]
        public void CleanUp()
        {
            AppDomain.Unload(_appDomain);
        }

        //Assert

        /// <summary />
        [Test]
        public void it_should_create_an_appDomain()
        {
            _appDomainManager.Verify(a => a.Create(It.IsAny<string>()), Times.Once());
        }

        /// <summary />
        [Test]
        public void it_should_call_ProcessTemplate_of_the_engine()
        {
            _engine.Verify(e => e.ProcessTemplate(Template, _host.Object), Times.Once());
        }

        /// <summary />
        [Test]
        public void it_should_return_the_result_returned_by_the_engine()
        {
            Assert.AreEqual(_result, Result);
        }

        [Test]
        public void host_should_be_initialized()
        {
            _host.Verify(h => h.Initialize(null));
        }

        [Test]
        public void host_should_be_finished()
        {
            _host.Verify(h => h.Finish(null));
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