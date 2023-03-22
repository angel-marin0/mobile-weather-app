﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnweWeatherApp.Data;
using UnweWeatherApp.Service;
using UnweWeatherApp.Util;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace UnweWeatherApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Forecast : ContentPage
    {
        private OpenWeatherService _WeatherService;
        public Forecast()
        {
            InitializeComponent();

            _WeatherService = OpenWeatherService.Instance;

            if (Device.RuntimePlatform == Device.Android)
            {
                searchLabel.Text = "Running on Android";
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                searchLabel.Text = "Running on iOS";
            }
        }

        private async void GetForecastButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                ForecastData forecastData = null;

                try
                {
                    forecastData = await GetForeCastData();
                }
                catch {
                    noDataLayout.IsVisible = true;
                    return;
                }

                FilterForecastData(forecastData);

                BindingContext = forecastData;

            }
            else
            {
                await DisplayAlert("Warning", "Please enter a location.", "OK");
            }
        }

        private async Task<ForecastData> GetForeCastData()
        {
            noDataLayout.IsVisible = false;
            List<GeoData> geoData = null;
            try
            {
                geoData = await _WeatherService.GetGeoData(
                    GenerateGeolocationRequestUri(Constants.OpenWeatherGeolocationEndpoint));
            }
            catch (Exception)
            {
                await DisplayAlert("Warning", "Not a valid location!", "OK");
            }

            string latitude = geoData[0].Latitude;
            string longitude = geoData[0].Longitude;

            ForecastData forecastData = null;

            try
            {
                forecastData = await _WeatherService.GetForecastData(
                    GenerateForecastRequestUri(Constants.OpenWeatherMapEndpoint, latitude, longitude));
            }
            catch (Exception)
            {
                await DisplayAlert("Warning", "Not a valid location!", "OK");
            }
            return forecastData;
        }

        private void FilterForecastData(ForecastData forecastData)
        {
            List[] list = forecastData.List;

            forecastData.List = list.Where(item => DateTime.Parse(item.Date).Hour == 12).ToArray();
        }

        string GenerateGeolocationRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += "/direct";
            requestUri += $"?q={_cityEntry.Text}";
            requestUri += "&limit=1";
            requestUri += $"&appid={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }

        string GenerateForecastRequestUri(string endpoint, string lat, string lon)
        {
            string requestUri = endpoint;
            requestUri += "/forecast";
            requestUri += $"?appid={Constants.OpenWeatherMapAPIKey}";

            requestUri += $"&lat={lat}";
            requestUri += $"&lon={lon}";
            return requestUri;
        }
    }
}