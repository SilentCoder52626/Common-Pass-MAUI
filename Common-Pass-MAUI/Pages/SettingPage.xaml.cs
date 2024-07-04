using CommunityToolkit.Maui.Views;

namespace Common_Pass_MAUI.Pages;

public partial class SettingPage : Popup
{
	public SettingPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Close();
    }
}