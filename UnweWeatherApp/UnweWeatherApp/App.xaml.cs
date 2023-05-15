using System;
using System.IO;
using System.Threading;
using UnweWeatherApp.Repository;
using UnweWeatherApp.Util;
using Xamarin.Essentials;
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

            try {
                bool reportsToggle = (bool)Application.Current.Properties["Auto_Reports"];

                Properties["Auto_Reports"] = reportsToggle;
            } catch (Exception ex)
            {
                Properties["Auto_Reports"] = true;
            }
            
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
