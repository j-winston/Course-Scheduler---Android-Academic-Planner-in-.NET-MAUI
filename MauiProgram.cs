using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;

namespace c971
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp( )
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseLocalNotification();

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping( nameof( Entry ), ( handler, view ) =>
            {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            } );
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}