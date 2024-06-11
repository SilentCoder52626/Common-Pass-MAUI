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
    public class HomeViewModel : ObservableObject
    {
        private readonly IAccountService _accountService;
        public HomeViewModel(IAccountService accountService)
        {
            _accountService = accountService;

            LoadUsers();
            
        }

        private async void LoadUsers()
        {
           
        }

        public ObservableCollection<UserModel> Users { get; set; } = new ObservableCollection<UserModel>();


        
    }
    public class UserModel
    {
        public int SN { get; set; }
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNumber { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
    }
}
