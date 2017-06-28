using System.IO;
using MbUnit.Framework;

namespace TC.CustomTemplating.IntegrationTests
{
    [TestFixture]
    public class When_transforming_a_nested_template_runner : When_transforming_runner
    {
        private string _file;

        public override void Arrange(ITextTransformer transformer)
        {
            var folder = Path.GetTempPath();
            _file = Path.Combine(folder, "inner.tt");

            File.WriteAllText(_file, "This is the inner template!");
            transformer.Path = folder;
        }

        public override string Act(ITextTransformer transformer, string template)
        {
            return transformer.Transform(template);
        }

        public override void Cleanup()
        {
            File.Delete(_file);
        }
    }
}