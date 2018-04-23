using System.Web.Mvc;

namespace WebApiTest.Areas.StuManager
{
    public class StuManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "StuManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "StuManager_default",
                "StuManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}