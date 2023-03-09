using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnweWeatherApp.Model;
using UnweWeatherApp.Service;
using UnweWeatherApp.Util;
using Xamarin.Forms;

namespace UnweWeatherApp
{
    public partial class MainPage : ContentPage
    {
        private OpenWeatherService _WeatherService;
        public MainPage()
        {
            InitializeComponent();
            _WeatherService = new OpenWeatherService();
            if (Device.RuntimePlatform == Device.Android)
            {
                searchLabel.Text = "Running on Android";
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                searchLabel.Text = "Running on iOS";
            }
        }


        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={_cityEntry.Text}";
            requestUri += "&units=metric"; // or units = metric
            requestUri += $"&appid={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }

        public async void OnGetWeatherButtonClicked(object sender, EventArgs args)
        {
            WeatherData weatherData = null;

            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                imageIcon.IsVisible = false;
                cachedResultsLabel.IsVisible = false;

                WeatherModel cachedResult = await _WeatherService.GetWeatherByLocation(_cityEntry.Text);


                if (cachedResult != null)
                {
                    weatherData = WeatherMapper.MapToData(cachedResult);

                    BindingContext = weatherData;
                    ConstructImageURL(weatherData.Weather[0].Icon);

                    cachedResultsLabel.Text = $"This data was cached on {cachedResult.Time} UTC";
                    cachedResultsLabel.IsVisible = true;
                }
                else
                {

                    try
                    {
                        weatherData = await _WeatherService.GetWeatherData(
                            GenerateRequestUri(Constants.OpenWeatherMapEndpoint));

                        BindingContext = weatherData;
                        ConstructImageURL(weatherData.Weather[0].Icon);

                        CacheResult(weatherData);
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Alert", "Not a valid location!", "OK");
                    }
                }
            }
            else
            {
                await DisplayAlert("Alert", "Please enter a location.", "OK");
            }

        }


        private void ConstructImageURL(string iconCode)
        {
            imageIcon.IsVisible = true;
            string imageUrl = $"https://openweathermap.org/img/wn/{iconCode}@2x.png";
            imageIcon.Source = imageUrl;
            imageIcon.Focus();
        }

        private async void CacheResult(WeatherData weatherData)
        {
            WeatherModel entity = WeatherMapper.MapToModel(weatherData);
            await App.WeatherRepository.Save(entity);
        }
    }
}
