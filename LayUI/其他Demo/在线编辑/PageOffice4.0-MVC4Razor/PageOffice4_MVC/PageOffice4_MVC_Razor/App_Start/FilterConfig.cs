using System.Web;
using System.Web.Mvc;

namespace PageOffice4_MVC_Razor
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
