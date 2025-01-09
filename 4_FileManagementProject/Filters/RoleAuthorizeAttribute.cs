using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4_FileManagementProject.Filters
{
    public class RoleAuthorizeAttribute:AuthorizeAttribute
    {
        public string[] Roles { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userRole = httpContext.Session["UserRole"]?.ToString();
            return Roles != null && Roles.Contains(userRole);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!AuthorizeCore(filterContext.HttpContext))
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    { "controller", "Account" },
                    { "action", "Unauthorized" }
                });
            }
        }
    }
}