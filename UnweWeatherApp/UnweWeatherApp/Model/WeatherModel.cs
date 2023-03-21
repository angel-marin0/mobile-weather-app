using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace UnweWeatherApp.Model
{
    [Table("WeatherModel")]
    public class WeatherModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public double Temperature { get; set; }
        public double Feels_Like { get; set; }
        public double WindSpeed { get; set; }
        public long Humidity { get; set; }
        public string Visibility { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
        public string Icon { get; set; }
        public DateTime Time { get; set; }
    }
}
