using System;
using System.Web.Mvc;


namespace Edis.Fenyites.Controllers.Base
{

    public static class ControllerExtensions
    {
        public static string ControllerName(this Type controllerType)
        {
            var baseType = typeof(Controller);
            if (baseType.IsAssignableFrom(controllerType))
            {
                var lastControllerIndex = controllerType.Name.LastIndexOf("Controller");
                if (lastControllerIndex > 0)
                {
                    return controllerType.Name.Substring(0, lastControllerIndex);
                }
            }

            return controllerType.Name;
        }
    }
}