using System.Web.Mvc;

namespace ibrasOneriAnket.Areas.Anket
{
    public class AnketAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Anket";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Anket_default",
                "Anket/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}