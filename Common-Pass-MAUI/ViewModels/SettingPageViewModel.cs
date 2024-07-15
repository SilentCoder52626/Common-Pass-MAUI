using Common_Pass_MAUI.Enums;
using Common_Pass_MAUI.Pages;
using Common_Pass_MAUI.Services;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UraniumUI.Dialogs;

namespace Common_Pass_MAUI.ViewModels
{
    public partial class SettingPageViewModel : ObservableObject
    {
        ISettingService _settingService;
        public Action ClosePopupAction { get; set; }


        public SettingPageViewModel(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [ObservableProperty]
        string _encryptionKey;
        [ObservableProperty]
        string _exportPin;
        [ObservableProperty]
        bool _isBusy;

        public async Task LoadSettingData()
        {

            IsBusy = true;
            var setting = await _settingService.GetSetting();
            if (setting.Any(a => a.Key == SettingKey.EncryptionKey.ToString()))
            {

                this.EncryptionKey = setting.FirstOrDefault(a => a.Key == SettingKey.EncryptionKey.ToString()).Value;
            }
            if (setting.Any(a => a.Key == SettingKey.ExportPin.ToString()))
            {

                this.ExportPin = setting.FirstOrDefault(a => a.Key == SettingKey.ExportPin.ToString()).Value;
            }
            IsBusy = false;
        }
        [RelayCommand]
        public async Task SaveSetting()
        {
            try
            {
                IsBusy = true;
                await _settingService.SaveSetting(new List<Models.SettingModel>()
                    {
                new Models.SettingModel(){Key = SettingKey.EncryptionKey.ToString(),Value = EncryptionKey },
                new Models.SettingModel(){Key = SettingKey.ExportPin.ToString(),Value = ExportPin },
                    });
                IsBusy = false;

                await Shell.Current.DisplayAlert("Success!", "Settings updated successfully!", "Ok");
                ClosePopupAction?.Invoke();

            }
            catch (Exception ex)
            {
                IsBusy = false;
                await Shell.Current.DisplayAlert("Error!", $"Something Went Wrong: {ex.Message}", "Ok");


            }


        }
    }
}
