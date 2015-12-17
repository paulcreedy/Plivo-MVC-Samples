using System.Web;
using System.Web.Mvc;

namespace Plivo_MVC_Samples
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}