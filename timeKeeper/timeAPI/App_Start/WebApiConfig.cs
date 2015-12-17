using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace timeAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            config.Routes.MapHttpRoute(
                name: "PersonalReport",
                routeTemplate: "api/personal/{id}/{year}/{month}",
                defaults: new { controller = "Personal", year = RouteParameter.Optional, month = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "MonthlyReport",
                routeTemplate: "api/month/{year}/{month}",
                defaults: new { controller = "Month", year = RouteParameter.Optional, month = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AnnualReport",
                routeTemplate: "api/annual/{year}",
                defaults: new { controller = "Annual", year = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Pagination",
                routeTemplate: "api/{controller}/page/{page}",
                defaults: new { page = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var json = GlobalConfiguration.Configuration;
            json.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            json.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            json.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
        }
    }
}
