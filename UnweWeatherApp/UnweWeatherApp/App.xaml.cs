using System;
using System.IO;
using UnweWeatherApp.Repository;
using UnweWeatherApp.Util;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnweWeatherApp
{
    public partial class App : Application
    {
        static WeatherRepository weatherRepository;
        public static WeatherRepository WeatherRepository
        {
            get
            {
                if (weatherRepository == null)
                {
                    weatherRepository = new WeatherRepository(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                        "WeatherRepository.db3"));
                }
                return weatherRepository;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
