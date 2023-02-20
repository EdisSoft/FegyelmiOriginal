using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Edis.Functions.Base
{
    public class ValidationException : Exception
    {
        public List<ValidationItem> Errors { get; set; }
        public bool IsReloadPage { get; set; }

        public object AdditionalData { get; set; }
        public ValidationException()
        {
            Errors = new List<ValidationItem>();
        }

        public ValidationException(string id, string text, object additionalData, bool isReloadPage = false) : this(id, text, isReloadPage)
        {
            AdditionalData = additionalData;
        }

        public ValidationException(string id, string text, bool isReloadPage = false)
        {
            Errors = new List<ValidationItem>
            {
                new ValidationItem
                {
                    Id = id,
                    Text = text
                }
            };
            IsReloadPage = isReloadPage;
        }

        public ValidationException(List<ValidationItem> errors, bool isReloadPage = false)
        {
            Errors = new List<ValidationItem>();
            Errors.AddRange(errors);
            IsReloadPage = isReloadPage;
        }

        public ValidationException(List<string> properties, string text, bool isReloadPage = false)
        {
            Errors = new List<ValidationItem>();
            foreach (var property in properties)
            {
                Errors.Add(new ValidationItem
                {
                    Id = property,
                    Text = text
                });

            }

            IsReloadPage = isReloadPage;
        }
    }

    public class ValidationItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
