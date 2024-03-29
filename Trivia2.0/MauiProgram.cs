﻿using Microsoft.Extensions.Logging;
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
            builder.Services.AddTransient<EditQuestionsPage>();
            builder.Services.AddTransient<ApproveQuestionsPage>();
            builder.Services.AddTransient<UserAdminPage>();
            builder.Services.AddTransient<AddUserPage>();

            //ViewModels
            builder.Services.AddTransient<LogInViewModel>();
            builder.Services.AddTransient<BestScoresPageViewModel>();
            builder.Services.AddTransient<UserQuestionsPageViewModel>();
            builder.Services.AddTransient<EditQuestionsPageViewModel>();
            builder.Services.AddTransient<ApproveQuestionsPageViewModel>();
            builder.Services.AddTransient<UserAdminPageViewModel>();
            builder.Services.AddTransient<AddUserPageViewModel>();


#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}