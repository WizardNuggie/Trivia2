using Microsoft.Extensions.Logging;
using Trivia2._0.Services;
using Trivia2._0.ViewModels;
using Trivia2._0.Views;

namespace Trivia2._0
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Services
            builder.Services.AddSingleton<Service>();

            //Views
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<BestScoresPage>();
            builder.Services.AddTransient<UserQuestionsPage>();

            //ViewModels
            builder.Services.AddTransient<LogInViewModel>();
            builder.Services.AddTransient<BestScoresPageViewModel>();
            builder.Services.AddTransient<UserQuestionsPageViewModel>();


#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}