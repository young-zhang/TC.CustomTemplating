using System;
using System.CodeDom.Compiler;
using System.Runtime.Remoting.Messaging;
using MbUnit.Framework;
using Moq;

namespace TC.CustomTemplating.UnitTests.HostSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_initialized
    {
        private Host _host;
        private readonly string _argumentValue1 = "qsdqsdqsd";
        private readonly object _argumentValue2 = new Object();
        private readonly SystemException _argumentValue3 = new SystemException();

        private const string ArgumentName1 = "TestArgument1";
        private const string ArgumentName2 = "TestArgument2";
        private const string ArgumentName3 = "TestArgument3";
            
        /// <summary />
        public void TestInitialize()
        {
            var transformer = new Mock<ITextTransformer>();
            var fileLoader = new Mock<IFileLoader>();
            var pathResolver = new Mock<IPathResolver>();
            _host = new Host(transformer.Object, pathResolver.Object, fileLoader.Object);
            _host.LogErrors(new CompilerErrorCollection()); ///Ensure there are errors

            var arguments = new TemplateArgumentCollection
                                {
                                    new TemplateArgument(ArgumentName1, _argumentValue1),
                                    new TemplateArgument(ArgumentName2, _argumentValue2),
                                    new TemplateArgument(ArgumentName3, _argumentValue3)
                                };

            _host.Initialize(arguments);
        }

        /// <summary />
        [Test]
        public void errors_should_be_null()
        {
            TestInitialize();
            Assert.IsNull(_host.Errors);
        }

        /// <summary />
        [Test]
        public void CallContext_should_contain_TestArgument1()
        {
            TestInitialize();
            Assert.AreSame(CallContext.GetData(ArgumentName1), _argumentValue1);
        }

        /// <summary />
        [Test]
        public void CallContext_should_contain_TestArgument2()
        {
            TestInitialize();
            Assert.AreSame(CallContext.GetData(ArgumentName2), _argumentValue2);
        }

        /// <summary />
        [Test]
        public void CallContext_should_contain_TestArgument3()
        {
            TestInitialize();
            Assert.AreSame(CallContext.GetData(ArgumentName3), _argumentValue3);
        }
    }
}