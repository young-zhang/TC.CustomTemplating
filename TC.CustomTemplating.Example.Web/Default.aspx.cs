using System;
using System.Drawing;
using System.Text;

namespace TC.CustomTemplating.Example.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            _template.Text =
                   @"<#@ property name=""Name"" type=""System.String"" processor=""PropertyProcessor"" #>" + Environment.NewLine +
                   @"This is a template displaying one argument: <#= Name #> - " + Environment.NewLine +
                   @"<# for (int i = 0 ; i < 10 ; i++) { Write(i + "" ""); } #>";
        }

        protected void _transform_Click(object sender, EventArgs e)
        {
            var transformer = Application["Transformer"] as ITextTransformer;
            try
            {
                var result = transformer.Transform(_template.Text, _argumentName.Text, _argumentValue.Text);
                _result.Text = Server.HtmlEncode(result);
                _result.ForeColor = Color.Black;
            }
            catch (TextTransformationException ex)
            {
                _result.ForeColor = Color.Red;
                _result.Text = ex.Message + Environment.NewLine
                    + "Template: " + Environment.NewLine
                    + ex.TemplateClass;
            }
        }
    }
}

