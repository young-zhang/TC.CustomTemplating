﻿using MbUnit.Framework;

namespace TC.CustomTemplating.IntegrationTests.DomainTextTransformerSpecifications
{
    public class When_transforming_a_multiple_parameters_template : When_transforming_Base
    {
        public When_transforming_a_multiple_parameters_template() : 
            base(new When_transforming_a_multiple_parameters_template_runner())
        {
        }

        [Test]
        public void the_transformation_should_match_expected_result()
        {
            Assert.AreEqual(ExpectedResult, ActualResult);
        }
    }
}