using System;
using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.DomainTextTransformerSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_disposing
    {
        private AppDomain _appDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString());
        private Mock<IAppDomainManager> _appDomainManager = new Mock<IAppDomainManager>();

        /// <summary />
        [FixtureInitializer]
        public void Initialize()
        {
            var host = new Mock<ITextTransformerHost>();
            var engine = new Mock<ITextTemplatingEngine>();

            _appDomainManager.Setup(a => a.Create(It.IsAny<string>())).Returns(_appDomain);
            _appDomainManager.Setup(a => a.Unload(_appDomain)).Verifiable(); //Should be called

            var transformer = new DomainTextTransformer(_appDomainManager.Object, host.Object, engine.Object);
            transformer.Dispose();
        }

        /// <summary />
        [Test]
        public void AppDomainManager_should_be_called_to_Unload_appDomain()
        {
            _appDomainManager.Verify();
        }
    }
}