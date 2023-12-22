using E_Commerce_Assignment.Services;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce_Assignment
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RequiresUserIdAuthorizationFilter());
        }
    }
}
