using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.ArgumentInfoSpecifications.cs
{
    /// <summary />
    public class When_setting
    {
        /// <summary />
        [Test]
        public void ConverterType_the_value_should_be_returned()
        {
            ArgumentInfo argumentInfo = new ArgumentInfo();
            string actual = "actual";
            argumentInfo.ConverterType = actual;

            Assert.AreEqual(actual, argumentInfo.ConverterType);
        }

        /// <summary />
        [Test]
        public void EditorType_the_value_should_be_returned()
        {
            ArgumentInfo argumentInfo = new ArgumentInfo();
            string actual = "actual";
            argumentInfo.EditorType = actual;

            Assert.AreEqual(actual, argumentInfo.EditorType);
        }

        /// <summary />
        [Test]
        public void Type_the_value_should_be_returned()
        {
            ArgumentInfo argumentInfo = new ArgumentInfo();
            string actual = "actual";
            argumentInfo.Type = actual;

            Assert.AreEqual(actual, argumentInfo.Type);
        }
    }
}