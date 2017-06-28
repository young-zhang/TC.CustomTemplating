using System;
using System.Reflection;

namespace TC.CustomTemplating.IntegrationTests
{
    internal class Templates
    {
        internal static string Get(Type type, string resource)
        {
            Assembly assembly = type.Assembly;
            string full = String.Format("{0}.{1}.{2}",
                typeof(Templates).Namespace,
                type.Name,
                resource);
            return TemplateResources.Get(full, type);
        }
    }
}