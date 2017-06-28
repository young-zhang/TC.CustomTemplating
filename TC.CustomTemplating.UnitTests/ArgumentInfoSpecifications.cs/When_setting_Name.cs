using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.ArgumentInfoSpecifications.cs
{
    /// <summary />
    [TestFixture]
    public class When_setting_Name
    {
        private ArgumentInfo _argumentInfo;

        /// <summary />
        [FixtureInitializer]
        public void Initialize()
        {
            _argumentInfo = new ArgumentInfo();
            _argumentInfo.Name = "actual";
        }

        /// <summary />
        [Test]
        public void Name_should_return_the_same_value()
        {
            Assert.AreEqual(_argumentInfo.Name, "actual");
        }

        /// <summary />
        [Test]
        public void FieldName_should_return_the_needed_FieldName()
        {
            Assert.AreEqual(_argumentInfo.FieldName, "_actual");
        }
    }
}