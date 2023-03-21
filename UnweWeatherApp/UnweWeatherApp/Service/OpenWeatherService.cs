using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnweWeatherApp.Data;
using UnweWeatherApp.Model;

namespace UnweWeatherApp.Service
{
    public class OpenWeatherService
    {
        HttpClient _client;
        public OpenWeatherService()
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
