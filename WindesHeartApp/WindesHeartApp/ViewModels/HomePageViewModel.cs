﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WindesHeartApp.Pages;
using WindesHeartApp.Resources;
using WindesHeartApp.Views;
using WindesHeartSDK;
using WindesHeartSDK.Models;
using Xamarin.Forms;

namespace WindesHeartApp.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        private int _battery;
        private int _heartrate;
        private string _batteryImage = "";
        private bool _isLoading;
        public bool toggle;
        private string _bandnameLabel;
        public event PropertyChangedEventHandler PropertyChanged;

        public HomePageViewModel()
        {
            if (Windesheart.ConnectedDevice != null)
            {
                ReadCurrentBattery();
                BandNameLabel = Windesheart.ConnectedDevice.Device.Name;
            }

            toggle = false;
        }

        public async Task ReadCurrentBattery()
        {
            //catch!!
            var battery = await Windesheart.ConnectedDevice.GetBattery();
            UpdateBattery(battery);
        }

        public void UpdateBattery(Battery battery)
        {
            Battery = battery.BatteryPercentage;
            if (battery.Status == StatusEnum.Charging)
            {
                BatteryImage = "BatteryCharging.png";
                return;
            }

            if (battery.BatteryPercentage >= 0 && battery.BatteryPercentage < 26)
            {
                BatteryImage = "BatteryQuart.png";
            }
            else if (battery.BatteryPercentage >= 26 && battery.BatteryPercentage < 51)
            {
                BatteryImage = "BatteryHalf.png";
            }
            else if (battery.BatteryPercentage >= 51 && battery.BatteryPercentage < 76)
            {
                BatteryImage = "BatteryThreeQuarts.png";
            }
            else if (battery.BatteryPercentage >= 76)
            {
                BatteryImage = "BatteryFull.png";
            }
        }


        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string BatteryImage
        {
            get => _batteryImage;
            set
            {
                _batteryImage = value;
                OnPropertyChanged();
            }
        }

        public int Battery
        {
            get => _battery;
            set
            {
                _battery = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayBattery));
            }
        }
        public int Heartrate
        {
            get => _heartrate;
            set
            {
                _heartrate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayHeartRate));
            }
        }

        public string BandNameLabel
        {
            get => _bandnameLabel;
            set
            {
                _bandnameLabel = value;
                OnPropertyChanged();
            }
        }
        public string DisplayHeartRate => Heartrate != 0 ? $"Last Heartbeat: {Heartrate.ToString()}" : "";

        public string DisplayBattery => Battery != 0 ? $"{Battery.ToString()}%" : "";

        public async void AboutButtonClicked(object sender, EventArgs args)
        {
            ToggleEnableButtons();

            IsLoading = true;
            await Application.Current.MainPage.Navigation.PushAsync(new AboutPage());
            IsLoading = false;
            ToggleEnableButtons();

        }
        public async void SettingsButtonClicked(object sender, EventArgs args)
        {
            ToggleEnableButtons();
            IsLoading = true;
            await Application.Current.MainPage.Navigation.PushAsync(new SettingsPage()
            {
                BindingContext = Globals.SettingsPageViewModel
            });
            IsLoading = false;
            ToggleEnableButtons();
        }

        public async void StepsButtonClicked(object sender, EventArgs args)
        {
            ToggleEnableButtons();
            IsLoading = true;
            await Application.Current.MainPage.Navigation.PushAsync(new StepsPage()
            {
                BindingContext = Globals.StepsPageViewModel
            });
            IsLoading = false;
            ToggleEnableButtons();
        }

        public async void HeartrateButtonClicked(object sender, EventArgs args)
        {
            ToggleEnableButtons();
            IsLoading = true;
            await Application.Current.MainPage.Navigation.PushAsync(new HeartratePage()
            {
                BindingContext = Globals.HeartratePageViewModel
            });
            IsLoading = false;
            ToggleEnableButtons();


        }

        public async void SleepButtonClicked(object sender, EventArgs args)
        {
            ToggleEnableButtons();

            IsLoading = true;
            await Application.Current.MainPage.Navigation.PushAsync(new SleepPage()
            {
                BindingContext = Globals.SleepPageViewModel
            });
            IsLoading = false;
            ToggleEnableButtons();

        }

        public async void DeviceButtonClicked(object sender, EventArgs args)
        {
            ToggleEnableButtons();

            IsLoading = true;
            await Application.Current.MainPage.Navigation.PushAsync(new DevicePage()
            {
                BindingContext = Globals.DevicePageViewModel
            });
            IsLoading = false;
            ToggleEnableButtons();


        }

        public void ToggleEnableButtons()
        {
            HomePage.SleepButton.IsEnabled = toggle;
            HomePage.AboutButton.IsEnabled = toggle;
            HomePage.SettingsButton.IsEnabled = toggle;
            HomePage.StepsButton.IsEnabled = toggle;
            HomePage.HeartrateButton.IsEnabled = toggle;
            HomePage.DeviceButton.IsEnabled = toggle;
            toggle = !toggle;
        }
    }
}
