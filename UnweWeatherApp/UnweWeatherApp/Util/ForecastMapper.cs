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
            model.Temperature = Math.Ceiling(forecastData.Main.Temperature - Constants.KelvinFactor).ToString() + "°";
            model.MinTemperature = "Minimum: " + Math.Ceiling(forecastData.Main.TempMin - Constants.KelvinFactor).ToString() + "°";
            model.MaxTemperature = "Maximum: " + Math.Ceiling(forecastData.Main.TempMax - Constants.KelvinFactor).ToString() + "°";
            model.IconUrl = $"{Constants.OpenWeatherIconBase}{forecastData.Weather[0].Icon}{Constants.OpenWeatherIconExtension}";

            DateTime date = DateTime.ParseExact(forecastData.Date, "yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"));

            model.DayOfWeek = date.ToString("dddd", new CultureInfo("en-gb"));
            model.Date = date.ToString("m", new CultureInfo("en-gb"));

            return model;
        }

    }
}
