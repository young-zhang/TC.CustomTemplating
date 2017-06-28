using System.Collections.Generic;
using System.Drawing.Design;
using MbUnit.Framework;

namespace TC.CustomTemplating.UnitTests.TemplateArgumentDirectiveProcessorSpecifications
{
    /// <summary />
    [TestFixture]
    public class When
    {
        /// <summary />
        [Test]
        public void GetImportsForProcessingRun_is_called_null_should_be_returned()
        {
            var processor = new TemplateArgumentDirectiveProcessor();
            Assert.IsNull(processor.GetImportsForProcessingRun());
        }

        /// <summary />
        [Test]
        public void GetPreInitializationCodeForProcessingRun_is_called_null_should_be_returned()
        {
            var processor = new TemplateArgumentDirectiveProcessor();
            Assert.IsNull(processor.GetPreInitializationCodeForProcessingRun());
        }

        /// <summary />
        [Test]
        public void GetReferencesForProcessingRun_is_called_null_should_be_returned()
        {
            var processor = new TemplateArgumentDirectiveProcessor();
            Assert.IsNull(processor.GetReferencesForProcessingRun());
        }

        /// <summary />
        [Test]
        public void GetReferencesForProcessingRun_is_called_system_drawing_should_be_returned_when_convertor_was_used()
        {
            var processor = new TemplateArgumentDirectiveProcessor();
            
            var dictionary = new Dictionary<string, string>
                                 {
                                     {"type", "MyCustomType, assembly, some other shit"},
                                     {"name", "MyCustomName"},
                                     {"converter", "MyCustomConverter"},
                                     {"editor", "MyCustomEditor"}
                                 };
            processor.ProcessDirective("argument1", dictionary);

            var strings = processor.GetReferencesForProcessingRun();

            Assert.AreEqual(strings.Length, 1);
            Assert.AreEqual(strings[0], typeof(UITypeEditor).Assembly.Location);
        }

        /// <summary />
        [Test]
        public void IsDirectiveSupported_is_called_true_should_be_returned()
        {
            var processor = new TemplateArgumentDirectiveProcessor();
            Assert.IsTrue(processor.IsDirectiveSupported("processorName"));
        }
    }
}