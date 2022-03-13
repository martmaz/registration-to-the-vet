﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebApplication5.App_Start
{
    public static class Utilities
    {
        public static string IsActive(this HtmlHelper html,
                                      string control,
                                      string action)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            // both must match
            var returnActive = control.ToLower() == routeControl.ToLower() &&
                               action.ToLower() == routeAction.ToLower();

            return returnActive ? "active" : "";
        }
    }
}