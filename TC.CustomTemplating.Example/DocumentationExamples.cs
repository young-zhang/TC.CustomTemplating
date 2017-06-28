using System;
using System.Drawing;

namespace TC.CustomTemplating.Example
{
    internal static class DocumentationExamples
    {
        internal static void Transform()
        {

//Start transformation without argument
string template1 = "This is a template displaying the current date & time: <#= System.DateTime.Now #>";
string output = Template.Transform(template1);

string template2 = @"<#@ property name=""Name"" type=""System.String""" +
                   @"    processor=""PropertyProcessor"" #>" +
                   @"This is a template displaying one argument: <#= Name #>";
//Start transformation with a single argument
string output2 = Template.Transform(template2, "Name", "Homer Simpson");

//Start transformation with multiple arguments
var arguments = new TemplateArgumentCollection
{    
   //Argument             Name     &  Value
   new TemplateArgument("FirstName", "Homer"),
   new TemplateArgument("LastName", "Simpson"),
   new TemplateArgument("Color", Color.Yellow)
};
string template3 = @"<#@ property name=""FirstName"" type=""System.String""" +
                   @"    processor=""PropertyProcessor"" #>" +
                   @"<#@ property name=""LastName"" type=""System.String""" +
                   @"    processor=""PropertyProcessor"" #>" +
                   @"<#@ property name=""Color"" type=""System.Drawing.Color""" +
                   @"    processor=""PropertyProcessor"" #>" +
                   @"This is a template displaying multiple arguments: "+
                   @"<#= FirstName #> <#= LastName #> is <#= Color.Name #>";
string output3 = Template.Transform(template3, arguments);
        }
    }
}
