using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using UnweWeatherApp.Data;
using Xamarin.Forms;

namespace UnweWeatherApp.ViewModel
{
    public class DayDetailViewModel
    {
        public DayDetailViewModel() { }

        public ImageSource Image { get; set; }

        public string Icon { get; set; }

        public string Location { get; set; }

        public Main Main { get; set; }

        
        public Weather[] Weather { get; set; }

        
        public Clouds Clouds { get; set; }

        
        public Wind Wind { get; set; }

        
        public long Visibility { get; set; }

        
        public string Date { get; set; }

        public string DayOfWeek { get; set; }


        public GeoSys GeoSys { get; set; }
    }
}
