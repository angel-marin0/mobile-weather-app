using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnweWeatherApp.Data
{
    public class ForecastData : WeatherData
    {
        [JsonProperty("list")]
        public List[] List { get; set; }
    }

    public class List
    {
        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("visibility")]
        public long Visibility { get; set; }

        [JsonProperty("dt_txt")]
        public string Date { get; set; }

        [JsonProperty("sys")]
        public GeoSys GeoSys { get; set; }
    }

    public class GeoSys
    {
        [JsonProperty("pod")]
        public string TimeOfDay { get; set; }
    }
}
