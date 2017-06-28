using System;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.ServiceLocatorSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_resolving_unknown_service
    {
        /// <summary />
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InvalidOperationException_should_be_thrown()
        {
            ServiceLocator.Resolve<ICustomFormatter, object>(new object());
        }
    }
}