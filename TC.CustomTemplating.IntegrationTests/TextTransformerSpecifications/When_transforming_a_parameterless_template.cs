﻿using MbUnit.Framework;

namespace TC.CustomTemplating.IntegrationTests.TextTransformerSpecifications
{
    [TestFixture]
    public class When_transforming_a_parameterless_template : When_transforming_Base
    {
        public When_transforming_a_parameterless_template() :
            base(new When_transforming_a_parameterless_template_runner())
        {
        }

        [Test]
        public void the_transformation_should_match_expected_result()
        {
            Assert.AreEqual(ExpectedResult, ActualResult);
        }
    }
}