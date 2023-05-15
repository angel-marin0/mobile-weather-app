using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnweWeatherApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
		public Settings ()
		{
			InitializeComponent ();
		}

        private async void autoReportSwitcCell_OnChanged(object sender, ToggledEventArgs e)
        {
            SwitchCell switchCell = (SwitchCell) sender;
            bool isEnabled = switchCell.On;
            autoReportSwitcCell.Text = isEnabled ? "On" : "Off";

            Application.Current.Properties["Auto_Reports"] = isEnabled;
            await Application.Current.SavePropertiesAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            bool areGeoReportsEnabled = (bool)Application.Current.Properties["Auto_Reports"];

            bool isEnabled = areGeoReportsEnabled;
            autoReportSwitcCell.On = isEnabled;
            autoReportSwitcCell.Text = isEnabled ? "On" : "Off";
        }
    }
}