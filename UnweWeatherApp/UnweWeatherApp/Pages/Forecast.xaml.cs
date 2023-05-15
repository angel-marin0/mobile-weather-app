using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnweWeatherApp.Data;
using UnweWeatherApp.Model;
using UnweWeatherApp.Service;
using UnweWeatherApp.Util;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace UnweWeatherApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Forecast : ContentPage
    {
        private OpenWeatherService _WeatherService;

        public ObservableCollection<List> Forecasts { get; }
        public Forecast()
        {
            InitializeComponent();

            _WeatherService = OpenWeatherService.Instance;

            listView.IsVisible = false;
        }

        private async void GetForecastButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                if (picker.SelectedIndex == -1)
                {
                    await DisplayAlert("Warning", "Please select a time.", "OK");
                    return;
                }


                listView.ItemsSource = null;
                listView.HeightRequest = 0;
                listView.IsVisible = false;
                listView.IsEnabled = false;
                indic.IsRunning = true;
                indic.IsVisible = true;
                indic.HeightRequest = 60;

                ForecastData forecastData = null;

                try
                {
                    forecastData = await GetForeCastData();
                }
                catch {
                    noDataLayout.IsVisible = true;
                    listView.IsVisible = false;
                    indic.IsRunning = false;
                    indic.IsVisible = false;
                    indic.HeightRequest = 0;
                    listView.IsVisible = false;
                listView.IsEnabled = false; 
                    return;
                }

                FilterForecastData(forecastData);


                List<ForecastModel> models = new List<ForecastModel>();
                foreach (var item in forecastData.List)
                {
                    ForecastModel forecastModel = ForecastMapper.MapToModel(item);
                    models.Add(forecastModel);
                }

                listView.ItemsSource = models;
                listView.IsVisible = true;
                indic.IsRunning = false;
                indic.IsVisible = false;
                indic.HeightRequest = 0;
                listView.IsVisible = true;
                listView.IsEnabled = true;

                Preferences.Set("last_location_key", _cityEntry.Text);
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

            int selectedHour = picker.SelectedIndex * 3; 
            forecastData.List = list.Where(item => DateTime.Parse(item.Date).Hour == selectedHour).ToArray();
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var myValue = Preferences.Get("last_location_key", "");
            _cityEntry.Text = myValue;
        }
    }
}
