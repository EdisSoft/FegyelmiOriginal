using Edis.Functions.Fany;
using Edis.IoC.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Edis.Functions.JFK.FENY
{
    public static class EmailTemplateNames
    {
        public const string EmailTemplatePath = "~/EmailTemplates/";
        public const string FelkeresSzakterVelemenyre = "FelkeresSzakterVelemenyTemplate.html";
    }
    //public class FegyelmiEmailFunctions : IFegyelmiEmailFunctions
    //{
    //    //public void FelkeresSzakteruletiVelemenyreEmail(Dictionary<string, string> parameterek, string cimzett)
    //    //{
    //    //    string templateName = EmailTemplateNames.FelkeresSzakterVelemenyre;
    //    //    var filename = HttpContext.Current.Server.MapPath(EmailTemplateNames.EmailTemplatePath + templateName);
    //    //    string[] content = File.ReadAllLines(filename);
    //    //    string subject = content[0].Replace("<!--", "").Replace("-->", "");
    //    //    string body = string.Join("\r\n", content.Skip(1).ToArray());
    //    //    foreach (var key in parameterek.Keys)
    //    //    {
    //    //        body = body.Replace(key, parameterek[key]);
    //    //    }
    //    //    EmailFunctions.SendEmailHTML(cimzett, subject, body);
    //    //}
    //}
}
