using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnweWeatherApp.Data
{
    public class GeoData
    {
        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("lon")]
        public string Longitude { get; set; }
    }
}
