using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Services.Description;
using Unity;
using Unity.AspNet.Mvc;
using WeatherApp.Service.Country;
using WeatherApp.Service.Weather;
using WeatherApp.Services;

namespace WeatherApp
{
    public static class Bootstrapper
    {
        #region Properties

        public static UnityContainer Container
        {
            get { return (UnityContainer)BuildUnityContainer(); }
        }

        #endregion Properties

        #region (private) Methods

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<ICountryService, CountryService>();
            container.RegisterType<IWeatherService, WeatherService>(); 
            container.RegisterType<IHttpRequestHandler, HttpRequestHandler>();
			container.RegisterType<IHttpContentWrapper, HttpContentWrapper>();

			return container;
        }

        private static void RegisterTypes(Dictionary<Type, Type> containerTypes, UnityContainer container)
        {
            foreach (var item in containerTypes)
            {
                container.RegisterType(item.Key, item.Value);
            }
        }

        #endregion (private) Methods

        #region (public) Methods

        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            //var unityServiceLocator = new UnityServiceLocator(container);
            //ServiceLocator.SetLocatorProvider(() => unityServiceLocator);

            return container;
        }

        #endregion (public) Methods
    }
}