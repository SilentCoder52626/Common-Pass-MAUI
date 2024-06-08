using Common_Pass_MAUI.Pages;
using Common_Pass_MAUI.Services;
using Common_Pass_MAUI.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using UraniumUI;

namespace Common_Pass_MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("Common_Pass_MAUI.appsettings.json");

            var _config = new ConfigurationBuilder().AddJsonStream(stream).Build();

            builder.Configuration.AddConfiguration(_config);

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddMaterialIconFonts();
                });

            builder.Services.AddHttpClient("Pass_Client", config =>
            {
                var url = _config["BaseUrl"];
                config.BaseAddress = new Uri(url);
            });

            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<RegisterPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
