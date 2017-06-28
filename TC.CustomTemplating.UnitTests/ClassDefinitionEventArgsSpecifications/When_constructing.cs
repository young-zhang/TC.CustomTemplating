using System;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.ClassDefinitionEventArgsSpecifications
{
    /// <summary />
    [TestFixture]
    public class When_constructing
    {
        /// <summary />
        [Test]
        public void classdefinition_should_be_returned_when_value_was_given()
        {
            string actual = "this is a class definition";
            ClassDefinitionEventArgs eventArgs = new ClassDefinitionEventArgs(actual);
            Assert.AreEqual(actual, eventArgs.ClassDefinition);
        }

        /// <summary />
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void an_ArgumentNullException_is_thrown_when_null_was_given()
        {
            string actual = null;
            new ClassDefinitionEventArgs(actual);
        }
    }
}