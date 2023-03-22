using System;
using System.Collections.Generic;
using System.Text;

namespace UnweWeatherApp.Model
{
    public class ForecastModel
    {
        public string Temperature { get; set; }

        public string MaxTemperature { get; set;}

        public string MinTemperature { get; set; }

        public string IconUrl { get; set; }
        public string DayOfWeek { get; set; }

        public string Date { get; set; }

    }
}
