using System.Web;
using System.Web.Mvc;

namespace _4_FileManagementProject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // Add global authorization if needed
            // filters.Add(new RoleAuthorizeAttribute { Roles = new[] { "Admin", "User" } });

        }
    }
}
