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
        }
        [ObservableProperty]
        private bool _isBusy;

        [RelayCommand]
        public async Task LoadAccounts()
        {
            IsBusy = true;
            Accounts.Clear();

            var Accs = await _accountDetailsService.GetAccountDetails();
            foreach (var data in Accs.Details)
            {
                Accounts.Add(new AccountDetailsDto()
                {
                    Account = data.Account,
                    Id = data.Id,
                    Pass = data.Pass,
                    UserName = data.UserName,
                });
            }
            IsBusy = false;
        }
        [RelayCommand]
        public async void GoToDetailsPage(AccountDetailsDto dto)
        {
            try
            {
                var viewModel = Application.Current.Handler.MauiContext.Services.GetService<DetailsPageViewModel>();
                viewModel.Id = dto.Id;
                var detailsPage = new DetailsPage(viewModel);
                Shell.Current.CurrentPage.ShowPopup(detailsPage);
            }catch(Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Something Went Wrong: {ex.Message}", "Ok");

            }
        }

        public ObservableCollection<AccountDetailsDto> Accounts { get; set; } = [];



    }

}
