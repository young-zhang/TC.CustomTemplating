using System;
using System.Diagnostics.CodeAnalysis;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests
{
    ///<summary>
    /// Provides the methods to enforce Arrange Act Assert tests.
    ///</summary>
    public abstract class TestBase
    {
        /// <summary>
        /// Gets the exception thrown by Act(), if any.
        /// </summary>
        protected Exception Exception { get; private set; }

        /// <summary>
        /// Initializes the context of the test and calls the Act method.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "All exceptions are catched and stored in the Exception member to get a better AAA experience.")]
        [FixtureInitializer]
        public void Setup()
        {
            Arrange();

            try
            {
                Act();
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
        }

        /// <summary>
        /// Cleans up any resources that used in the test.
        /// </summary>
        [FixtureTearDown]
        public virtual void CleanUp() { }

        /// <summary>
        /// Arranges the context of you test.
        /// </summary>
        protected virtual void Arrange() { }

        /// <summary>
        /// Performs actions on the subject under test.
        /// </summary>
        protected abstract void Act();
    }
}