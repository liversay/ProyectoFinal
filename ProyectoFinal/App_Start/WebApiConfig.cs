﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ProyectoFinal
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Habilitar respuestas JSON
            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}
