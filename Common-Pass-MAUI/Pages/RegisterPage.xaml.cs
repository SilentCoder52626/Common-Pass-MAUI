using Common_Pass_MAUI.ViewModels;

namespace Common_Pass_MAUI.Pages;

public partial class RegisterPage : ContentPage
{
	private readonly RegisterViewModel _vm;
    public RegisterPage(RegisterViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }



}