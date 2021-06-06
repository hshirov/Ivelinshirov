using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace Ivelinshirov.Common
{
    public static class ActiveClassHelper
    {
        public static string ActiveClass(this IHtmlHelper htmlHelper, string controllers = null, string actions = null, string routeId = null, string cssClass = "active")
        {
            var currentController = htmlHelper?.ViewContext.RouteData.Values["controller"] as string;
            var currentAction = htmlHelper?.ViewContext.RouteData.Values["action"] as string;
            var currentRouteId = htmlHelper?.ViewContext.RouteData.Values["id"] as string;

            var acceptedControllers = (controllers ?? currentController ?? "").Split(',');
            var acceptedActions = (actions ?? currentAction ?? "").Split(',');
            var acceptedRouteId = (routeId ?? currentRouteId ?? "").Split(',');

            if(routeId == null)
            {
                return acceptedControllers.Contains(currentController) && acceptedActions.Contains(currentAction)
                ? cssClass
                : "";
            }
            return acceptedControllers.Contains(currentController) && acceptedActions.Contains(currentAction)
                && acceptedRouteId.Contains(currentRouteId)
                ? cssClass
                : "";
        }
    }
}
