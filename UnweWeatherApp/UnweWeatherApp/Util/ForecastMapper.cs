using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnweWeatherApp.Data;
using UnweWeatherApp.Model;

namespace UnweWeatherApp.Util
{
    public class ForecastMapper
    {
        public ForecastMapper() { }

        public static ForecastModel MapToModel (List forecastData)
        {
            ForecastModel model = new ForecastModel();
            model.Temperature = forecastData.Main.Temperature.ToString();
            model.MinTemperature = forecastData.Main.TempMin.ToString();
            model.MaxTemperature = forecastData.Main.TempMax.ToString();
            model.IconUrl = $"{Constants.OpenWeatherIconBase}{forecastData.Weather[0].Icon}{Constants.OpenWeatherIconExtension}";

            DateTime date = DateTime.ParseExact(forecastData.Date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            model.DayOfWeek = date.ToString("dddd");
            model.Date = date.ToString("d") + "/" + date.ToString("M");

            return model;
        }

    }
}
