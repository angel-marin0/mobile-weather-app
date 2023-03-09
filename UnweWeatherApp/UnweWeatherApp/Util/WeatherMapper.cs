using System;
using System.Collections.Generic;
using System.Text;
using UnweWeatherApp.Model;

namespace UnweWeatherApp.Util
{
    internal class WeatherMapper
    {
        public WeatherMapper() { }

        public static WeatherData MapToData(WeatherModel weatherModel)
        {
            WeatherData weatherData = new WeatherData
            {
                Title = weatherModel.Title,
                Main = new Main
                {
                    Temperature = weatherModel.Temperature,
                    Feels_like = weatherModel.Feels_Like,
                    Humidity = weatherModel.Humidity
                },
                Wind = new Wind
                {
                    Speed = weatherModel.WindSpeed
                },
                Sys = new Sys
                {
                    Sunrise = weatherModel.Sunrise,
                    Sunset = weatherModel.Sunset
                },
                Weather = new Weather[]
                {
                    new Weather
                    {
                        Visibility = weatherModel.Visibility,
                        Icon = weatherModel.Icon
                    }
                }
            };


            return weatherData;
        }

        public static WeatherModel MapToModel(WeatherData weatherData)
        {
            WeatherModel weatherModel = new WeatherModel();
            weatherModel.Title = weatherData.Title;
            weatherModel.Temperature = weatherData.Main.Temperature;
            weatherModel.Feels_Like = weatherData.Main.Feels_like;
            weatherModel.WindSpeed = weatherData.Wind.Speed;
            weatherModel.Humidity = weatherData.Main.Humidity;
            weatherModel.Visibility = weatherData.Weather[0].Visibility;
            weatherModel.Sunrise = weatherData.Sys.Sunrise;
            weatherModel.Sunset = weatherData.Sys.Sunset;
            weatherModel.Icon = weatherData.Weather[0].Icon;
            weatherModel.Time = DateTime.Now;

            return weatherModel;
        }
    }
}
