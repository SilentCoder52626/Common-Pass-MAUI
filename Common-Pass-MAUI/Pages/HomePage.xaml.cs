
using Common_Pass_MAUI.ViewModels;

namespace Common_Pass_MAUI.Pages;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _vm;
    public HomePage(HomeViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAccounts();
    }


}