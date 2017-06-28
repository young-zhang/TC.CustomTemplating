using System;
using System.CodeDom;
using MbUnit.Framework;
using Microsoft.VisualStudio.TextTemplating;
using Moq;

namespace TC.CustomTemplating.UnitTests.DomainTextTransformerSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_transforming_a_single_parameter_template
    {
        private class ArgumentValue
        {
            private string part1;
            private string part2;

            public ArgumentValue(string part1, string part2)
            {
                this.part1 = part1;
                this.part2 = part2;
            }

            #region equality members

            public bool Equals(ArgumentValue other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Equals(other.part1, part1) && Equals(other.part2, part2);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof (ArgumentValue)) return false;
                return Equals((ArgumentValue) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((part1 != null ? part1.GetHashCode() : 0)*397) ^ (part2 != null ? part2.GetHashCode() : 0);
                }
            }

            #endregion
        }

        private DomainTextTransformer _transformer;
        private AppDomain _appDomain;
        private readonly Mock<IAppDomainManager> _appDomainManager = new Mock<IAppDomainManager>();
        private readonly Mock<ITextTransformerHost> _host = new Mock<ITextTransformerHost>();
        private readonly Mock<ITextTemplatingEngine> _engine = new Mock<ITextTemplatingEngine>();
        private ArgumentValue _argumentValue = new ArgumentValue("value1", "value2");
        private string _result;
        private int _initialAssemblies;

        const string Template = "this is a template";
        const string Result = "this is a result";
        const string ArgumentName = "ArgumentName";

        /// <summary />
        [FixtureInitializer]
        public void with_arguments_it_should_call_appDomainManager()
        {
            //Arrange
            _appDomain = AppDomain.CreateDomain(new Guid().ToString());
            _appDomainManager.Setup(a => a.Create(It.IsAny<string>())).Returns(_appDomain);
            _engine.Setup(e => e.ProcessTemplate(Template, _host.Object)).Returns(Result);
            
            _transformer = new DomainTextTransformer(_appDomainManager.Object, _host.Object, _engine.Object);
            _initialAssemblies = _transformer.AssemblyReferences.Count;
            
            //Act
            _result = _transformer.Transform(Template, ArgumentName, _argumentValue);
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
            _host.Verify(h => h.Initialize(It.Is<TemplateArgumentCollection>(c => c.Count > 0 && c[0].Name == ArgumentName && c[0].Value == _argumentValue)));
        }

        /// <summary />
        [Test]
        public void host_should_be_finished()
        {
            _host.Verify(h => h.Finish(It.Is<TemplateArgumentCollection>(c => c.Count > 0 && c[0].Name == ArgumentName && c[0].Value == _argumentValue)));
        }

        /// <summary />
        [Test]
        public void AssemblyReferences_should_contain_test_dll()
        {
            Assert.IsTrue(_transformer.AssemblyReferences.Contains(typeof(ArgumentValue).Assembly));
        }

        /// <summary />
        [Test]
        public void AssemblyReferences_should_contain_one_more_assembly()
        {
            Assert.AreEqual(_transformer.AssemblyReferences.Count, _initialAssemblies + 1);
        }
    }
}