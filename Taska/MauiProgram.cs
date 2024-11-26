using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Taska.Views;
using Taska.ModelView;

namespace Taska
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<TasksPage>();
            builder.Services.AddTransient<TaskPageModelView>();

            builder.Services.AddTransient<TaskForm>();
            builder.Services.AddTransient<TaskFormViewModel>();

            // Register DatabaseHelper as a singleton
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, "tasks.db");
            builder.Services.AddSingleton<DatabaseHelper>(s => new DatabaseHelper(databasePath));

            return builder.Build();
        }
    }
}
