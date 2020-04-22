using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //This will make the home page also not accessible.
            //To make it accessible go to HomeContoller.
            filters.Add(new AuthorizeAttribute());

            //Using this no one can access our home page using non-secure server.
            //Application end points will no longer be availabe on any http channel.
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
