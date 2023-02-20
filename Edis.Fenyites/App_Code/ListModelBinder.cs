using System;
using System.Globalization;
using System.Web.Mvc;

namespace Edis.Fenyites
{
    public class ListModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (string.IsNullOrWhiteSpace(valueResult?.AttemptedValue))
                return null;
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}