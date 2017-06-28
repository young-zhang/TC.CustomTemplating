using System;
using MbUnit.Framework;

namespace TC.CustomTemplating.IntegrationTests
{
    public abstract class When_transforming
    {
        private string _template;
        private string _expectedResult;
        private string _actualResult;
        private Exception _exception;
        private readonly When_transforming_runner _runner;

        protected When_transforming(When_transforming_runner runner)
        {
            _runner = runner;
        }

        protected string Template
        {
            get { return _template; }
        }

        protected string ExpectedResult
        {
            get { return _expectedResult; }
        }

        public string ActualResult
        {
            get { return _actualResult; }
        }

        public Exception Exception
        {
            get { return _exception; }
        }

        [FixtureInitializer]
        public void TestInitialize()
        {
            _template = Templates.Get(GetType(), "tt");
            _expectedResult = Templates.Get(GetType(), "result");

            ITextTransformer transformer = CreateTransformer();
            _runner.Arrange(transformer);
            BefortAct(transformer);

            try
            {
                _actualResult = _runner.Act(transformer, Template);
            }
            catch (Exception exception)
            {
                _exception = exception;
            }
            var disposable = transformer as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        protected virtual void BefortAct(ITextTransformer transformer)
        {
        }

        [FixtureTearDown]
        public void TestCleanup()
        {
            _runner.Cleanup();
        }

        protected abstract ITextTransformer CreateTransformer();
    }
}