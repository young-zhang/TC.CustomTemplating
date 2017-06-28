using System;
using System.CodeDom;
using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.DomainTextTransformerSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_transforming_with_auto_recycle
    {
        private DomainTextTransformer _transformer;
        private AppDomain _appDomain;
        private readonly Mock<IAppDomainManager> _appDomainManager = new Mock<IAppDomainManager>();
        private readonly Mock<ITextTransformerHost> _host = new Mock<ITextTransformerHost>();
        private readonly Mock<ITextTemplatingEngine> _engine = new Mock<ITextTemplatingEngine>();
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
            _transformer = new DomainTextTransformer(_appDomainManager.Object, _host.Object, _engine.Object);
            _transformer.AutoRecycle = true;
            _transformer.RecycleThreshold = 5;
                        
            //Act
            _transformer.Transform(Template);
            _appDomainManager.Verify(a => a.Create(It.IsAny<string>()), Times.Once());
            _transformer.Transform(Template);
            _appDomainManager.Verify(a => a.Create(It.IsAny<string>()), Times.Once());
            _transformer.Transform(Template);
            _appDomainManager.Verify(a => a.Create(It.IsAny<string>()), Times.Once());
            _transformer.Transform(Template);
            _appDomainManager.Verify(a => a.Create(It.IsAny<string>()), Times.Once());
            _transformer.Transform(Template);           
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
        public void it_should_create_two_appDomains_after_the_Threshold_is_reached()
        {
            _appDomainManager.Verify(a => a.Create(It.IsAny<string>()), Times.Exactly(2));
        }

        /// <summary />
        [Test]
        public void host_should_be_initialized()
        {
            _host.Verify(h => h.Initialize(null));
        }

        /// <summary />
        [Test]
        public void host_should_be_finished()
        {
            _host.Verify(h => h.Finish(null));
        }
    }
}