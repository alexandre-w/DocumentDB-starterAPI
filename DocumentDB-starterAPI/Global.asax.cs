using DocumentDB_starterAPI.DAL.Repository;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace DocumentDB_starterAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            DocumentDBRepository<JObject>.Initialize();
        }
    }
}
