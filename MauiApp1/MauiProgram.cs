using Drastic.AudioRecorder;
using MauiApp1.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace MauiApp1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMudServices();
        AudioRecorderService recorder = new()
        {
            StopRecordingOnSilence = false, //will stop recording after 2 seconds (default)
            StopRecordingAfterTimeout = false,  //stop recording after a max timeout (defined below)
                                                //TotalAudioTimeout = TimeSpan.FromSeconds(15) //audio will stop recording after 15 seconds
            PreferredSampleRate = 16000
        };
        builder.Services.AddSingleton(recorder);
        builder.Services.AddSingleton<IDialogService, DialogService>();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
