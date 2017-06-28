using System;

namespace TC.CustomTemplating.Example.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var textTransformer = new DomainTextTransformer {AutoRecycle = true, RecycleThreshold = 30};
            Application["Transformer"] = textTransformer;
        }

        protected void Application_End(object sender, EventArgs e)
        {
            var transformer = Application["Transformer"] as DomainTextTransformer;
            if (transformer != null)
            {
                transformer.Dispose();
            }
        }
    }
}