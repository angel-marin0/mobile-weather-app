using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnweWeatherApp.Data;
using UnweWeatherApp.Model;

namespace UnweWeatherApp.Service
{
    public sealed class OpenWeatherService
    {
        private static OpenWeatherService instance = null;
        private static object locker = new object();
        private HttpClient _client;

        public static OpenWeatherService Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new OpenWeatherService();
                    }
                    return instance;
                }
            }
        }

        private OpenWeatherService()
        {
            _client = new HttpClient();
        }

        public async Task<WeatherData> GetWeatherData(string query)
        {
            WeatherData weatherData = null;


            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }


            return weatherData;
        }

        public async Task<List<GeoData>> GetGeoData(string query)
        {
            List<GeoData> geoData = null;

            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    geoData = JsonConvert.DeserializeObject<List<GeoData>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }


            return geoData;
        }

        public async Task<ForecastData> GetForecastData(string query)
        {
            ForecastData forecastData = null;

            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    forecastData = JsonConvert.DeserializeObject<ForecastData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }


            return forecastData;
        }

        public async Task<WeatherModel> GetWeatherByLocation(string location)
        {
            WeatherModel result = await App.WeatherRepository.GetByLocationTitle(location);
            return result;
        }

        public async Task<int> CacheWeather(WeatherModel weatherModel)
        {
            int result = await App.WeatherRepository.Save(weatherModel);
            return result;
        }

        public void Delete()
        {
            App.WeatherRepository.RemoveOlderEntries();
        }
    }
}
