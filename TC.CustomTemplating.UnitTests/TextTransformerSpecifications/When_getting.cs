using System;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TextTransformerSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_getting
    {
        /// <summary />
        [Test]
        public void AppDomain_current_AppDomain_should_be_returned()
        {
            var transformer = new TextTransformer();
            Assert.AreEqual(transformer.AppDomain, AppDomain.CurrentDomain);
        }
    }
}