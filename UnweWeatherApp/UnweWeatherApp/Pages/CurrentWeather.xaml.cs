﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnweWeatherApp.Data;
using UnweWeatherApp.Model;
using UnweWeatherApp.Service;
using UnweWeatherApp.Util;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnweWeatherApp.Pages
{
    public partial class CurrentWeather : ContentPage
    {
        private OpenWeatherService _WeatherService;
        public CurrentWeather()
        {
            InitializeComponent();

            _WeatherService = OpenWeatherService.Instance;

            ClearCacheJob();
        }


        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += "/weather";
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

                WeatherModel cachedResult = await _WeatherService.GetWeatherByLocation(_cityEntry.Text.Trim());


                if (cachedResult != null)
                {
                    weatherData = WeatherMapper.MapToData(cachedResult);

                    BindingContext = weatherData;
                    ConstructImageURL(weatherData.Weather[0].Icon);

                    cachedResultsLabel.Text = $"*This data was cached on {cachedResult.Time} UTC";
                    cachedResultsLabel.IsVisible = true;
                }
                else
                {

                    try
                    {
                        weatherData = await _WeatherService.GetWeatherData(
                            GenerateRequestUri(Constants.OpenWeatherMapEndpoint));
                        weatherData.Title = _cityEntry.Text;

                        BindingContext = weatherData;
                        ConstructImageURL(weatherData.Weather[0].Icon);

                        CacheResult(weatherData);
                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Warning", "Not a valid location!", "OK");
                    }
                }

                Preferences.Set("last_location_key", _cityEntry.Text);
            }
            else
            {
                await DisplayAlert("Warning", "Please enter a location.", "OK");
            }

        }


        private void ConstructImageURL(string iconCode)
        {
            imageIcon.IsVisible = true;
            string imageUrl = $"{Constants.OpenWeatherIconBase}{iconCode}{Constants.OpenWeatherIconExtension}";
            imageIcon.Source = imageUrl;
            imageIcon.Focus();
        }

        private async void CacheResult(WeatherData weatherData)
        {
            WeatherModel entity = WeatherMapper.MapToModel(weatherData);
            await App.WeatherRepository.Save(entity);
        }

        private void ClearCacheJob()
        {
            _WeatherService.Delete();

            // Begin a timer and repeat the deletion every "n" given minutes
            var minutes = TimeSpan.FromMinutes(2);

            Device.StartTimer(minutes, () => {

                _WeatherService.Delete();
                return true;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var myValue = Preferences.Get("last_location_key", "");
            _cityEntry.Text = myValue;
        }
    }
}