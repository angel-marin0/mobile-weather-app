using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UnweWeatherApp
{
    public partial class MainPage : ContentPage
    {
        private OpenWeatherService _WeatherWeatherService;
        public MainPage()
        {
            InitializeComponent();
            _WeatherWeatherService= new OpenWeatherService();
        }


        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={_cityEntry.Text }";
            requestUri += "&units=metric"; // or units = metric
            requestUri += $"&appid={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }

        public async void OnGetWeatherButtonClicked(object sender, EventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                WeatherData weatherData = await _WeatherWeatherService.GetWeatherData(
                    GenerateRequestUri(Constants.OpenWeatherMapEndpoint));
                BindingContext = weatherData;
            }

        }
    }
}
