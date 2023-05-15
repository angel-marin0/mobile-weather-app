using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UnweWeatherApp.Model
{
    public class GoogleFindPlaceData
    {
        [JsonProperty("candidates")]
        public Candidates[] candidates { get; set; }
    }

    public class Candidates
    {
        [JsonProperty("photos")]
        public Photos[] photos { get; set; }
    }

    public class Photos
    {
        [JsonProperty("photo_reference")]
        public string PhotoReference { get; set; }
    }
}
