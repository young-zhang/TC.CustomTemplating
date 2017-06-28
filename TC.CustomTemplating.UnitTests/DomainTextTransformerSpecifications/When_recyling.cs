using System;
using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.DomainTextTransformerSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_recyling
    {
        /// <summary />
        [Test]
        public void with_arguments_it_should_call_appDomainManager()
        {
            var appDomainManager = new Mock<IAppDomainManager>();
            var host = new Mock<ITextTransformerHost>();
            var engine = new Mock<ITextTemplatingEngine>();

            var appDomain = AppDomain.CreateDomain(new Guid().ToString());
            try
            {
                appDomainManager.Setup(a => a.Create(It.IsAny<string>())).Returns(appDomain);

                var transformer = new DomainTextTransformer(appDomainManager.Object, host.Object, engine.Object);
                transformer.Recycle();

                appDomainManager.Verify(a => a.Create(It.IsAny<string>()), Times.Exactly(2));
            }
            finally
            {
                AppDomain.Unload(appDomain);
            }
        }
    }
}