using System;
using System.IO;
using System.Reflection;

namespace TC.CustomTemplating.UnitTests.TemplateArgumentDirectiveProcessorSpecifications
{
    public class When_processing_Base
    {
        protected string GetResourceCode(string resource)
        {
            Type type = GetType();
            Assembly assembly = type.Assembly;
            string full = "{0}.{1}_{2}.code".FormatInvariant(
                type.Namespace,
                type.Name,
                resource);
            using (Stream stream = assembly.GetManifestResourceStream(full))
            {
                if (stream == null) throw new InvalidOperationException();
                return new StreamReader(stream).ReadToEnd();
            }
        }
    }
}