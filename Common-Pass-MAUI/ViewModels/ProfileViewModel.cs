using Common_Pass_MAUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Pass_MAUI.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly IAccountService _accountService;

        public ProfileViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            LoadProfile();
        }
        public async void LoadProfile()
        {
            var profileData = await _accountService.GetProfile();

            this.Name = profileData?.Name ?? String.Empty;
            this.Email = profileData?.EmailAddress ?? String.Empty;
        }
        [ObservableProperty]
        string _name;
        [ObservableProperty]
        string _email;

    }
}
