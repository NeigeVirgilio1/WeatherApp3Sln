using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Datas();
        }
        private async Task Datas()
        {
            var data = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (data != PermissionStatus.Granted)
            {
                var newdata = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
            var location = await Geolocation.GetLocationAsync();
            var latitude = location.Latitude;
            var longitude = location.Longitude;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&units=metric&appid=da840b5eb676f8e617682e856ef9485b";
            var response = await client.GetStringAsync(url);
            var weathersData = JsonConvert.DeserializeObject<Data>(response);
            BindingContext = weathersData;
        }
    }
}

