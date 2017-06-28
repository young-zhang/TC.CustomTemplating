using System;
using System.CodeDom.Compiler;
using System.Runtime.Remoting.Messaging;
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.HostSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_finished
    {
        private Host _host;
        private readonly string _argumentValue1 = "qsdqsdqsd";
        private readonly object _argumentValue2 = new Object();
        private readonly SystemException _argumentValue3 = new SystemException();

        private const string ArgumentName1 = "TestArgument1";
        private const string ArgumentName2 = "TestArgument2";
        private const string ArgumentName3 = "TestArgument3";
            
        /// <summary />
        [FixtureInitializer]
        public void TestInitialize()
        {
            var transformer = new Mock<ITextTransformer>();
            var fileLoader = new Mock<IFileLoader>();
            var pathResolver = new Mock<IPathResolver>();
            _host = new Host(transformer.Object, pathResolver.Object, fileLoader.Object);

            var arguments = new TemplateArgumentCollection
                                {
                                    new TemplateArgument(ArgumentName1, _argumentValue1),
                                    new TemplateArgument(ArgumentName2, _argumentValue2),
                                    new TemplateArgument(ArgumentName3, _argumentValue3)
                                };
            CallContext.SetData(ArgumentName1, _argumentValue1);
            CallContext.SetData(ArgumentName2, _argumentValue2);
            CallContext.SetData(ArgumentName3, _argumentValue3);

            _host.Finish(arguments);
        }

        /// <summary />
        [Test]
        public void CallContext_should_contain_TestArgument1()
        {
            Assert.AreSame(CallContext.GetData(ArgumentName1), null);
        }

        /// <summary />
        [Test]
        public void CallContext_should_contain_TestArgument2()
        {
            Assert.AreSame(CallContext.GetData(ArgumentName2), null);
        }

        /// <summary />
        [Test]
        public void CallContext_should_contain_TestArgument3()
        {
            Assert.AreSame(CallContext.GetData(ArgumentName3), null);
        }
    }
}