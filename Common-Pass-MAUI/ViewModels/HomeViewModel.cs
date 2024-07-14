using Common_Pass_MAUI.Models;
using Common_Pass_MAUI.Pages;
using Common_Pass_MAUI.Services;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Pass_MAUI.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IAccountDetailsService _accountDetailsService;
        public HomeViewModel(IAccountDetailsService accountDetailsService)
        {
            _accountDetailsService = accountDetailsService;
            LoadAccounts();
        }
        [RelayCommand]
        public async void LoadAccounts()
        {
            var Accs = await _accountDetailsService.GetAccountDetails();
            foreach(var data in Accs.Details)
            {
                Accounts.Add(new AccountDetailsDto()
                {
                    Account = data.Account,
                    Id = data.Id,
                    Pass = data.Pass,
                    UserName = data.UserName,
                });
            }
        }
        [RelayCommand]
        public void GoToDetailsPageCommand(AccountDetailsDto dto)
        {
             var viewModel = Application.Current.Handler.MauiContext.Services.GetService<SettingPageViewModel>();

        var settingPage = new SettingPage(viewModel);
            Shell.Current.CurrentPage.ShowPopup(settingPage);
        }

        public ObservableCollection<AccountDetailsDto> Accounts { get; set; } = new ObservableCollection<AccountDetailsDto>();


        
    }
   
}
