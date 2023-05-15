using System;
using System.Collections.Generic;
using System.Text;

namespace UnweWeatherApp.Util
{
    public static class Constants
    {
        private static string BaseUrl = "https://api.openweathermap.org";

        public static string OpenWeatherMapEndpoint = $"{BaseUrl}/data/2.5";
        public static string OpenWeatherGeolocationEndpoint = $"{BaseUrl}/geo/1.0";
        public static string OpenWeatherMapAPIKey = "2c24436d9d9a44bc6d9eae99d7835bb9";

        public static string GoogleBaseUrl = "https://maps.googleapis.com/maps/api/place/";
        public static string FindPlaceUrl = "findplacefromtext/json?input=";
        public static string GetPhotoReferenceUrl = $"&inputtype=textquery&fields=photo{GoogleAPIKey}";
        public static string Photo = "photo?maxwidth=400&photo_reference=";
        public static string GoogleAPIKey = "placeholder";


        public static string OpenWeatherIconBase = "https://openweathermap.org/img/wn/";
        public static string OpenWeatherIconExtension = "@2x.png";

        public static double KelvinFactor = 273.15;
    }
}

