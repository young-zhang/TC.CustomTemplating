using System;
using System.CodeDom;
using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.DomainTextTransformerSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_transforming_a_single_null_value_parameter_template
    {
        private DomainTextTransformer _transformer;
        private AppDomain _appDomain;
        private readonly Mock<IAppDomainManager> _appDomainManager = new Mock<IAppDomainManager>();
        private readonly Mock<ITextTransformerHost> _host = new Mock<ITextTransformerHost>();
        private readonly Mock<ITextTemplatingEngine> _engine = new Mock<ITextTemplatingEngine>();
        private string _result;
        private int _initialAssemblies;

        const string Template = "this is a template";
        const string Result = "this is a result";
        const string ArgumentName = "ArgumentName";

        /// <summary />
        [FixtureInitializer]
        public void TestInitialize()
        {
            //Arrange
            _appDomain = AppDomain.CreateDomain(new Guid().ToString());
            _appDomainManager.Setup(a => a.Create(It.IsAny<string>())).Returns(_appDomain);
            _engine.Setup(e => e.ProcessTemplate(Template, _host.Object)).Returns(Result);
            
            _transformer = new DomainTextTransformer(_appDomainManager.Object, _host.Object, _engine.Object);
            _initialAssemblies = _transformer.AssemblyReferences.Count;
            
            //Act
            _result = _transformer.Transform(Template, ArgumentName, null);
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

        /// <summary />
        [Test]
        public void host_should_be_initialized()
        {
            _host.Verify(h => h.Initialize(It.Is<TemplateArgumentCollection>(c => c.Count > 0 && c[0].Name == ArgumentName && c[0].Value == null)));
        }

        /// <summary />
        [Test]
        public void host_should_be_finished()
        {
            _host.Verify(h => h.Finish(It.Is<TemplateArgumentCollection>(c => c.Count > 0 && c[0].Name == ArgumentName && c[0].Value == null)));
        }

        /// <summary />
        [Test]
        public void AssemblyReferences_should_contain_same_number_of_assemblies()
        {
            Assert.AreEqual(_transformer.AssemblyReferences.Count, _initialAssemblies);
        }
    }
}