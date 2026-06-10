using System;
using Avalonia;
using Avalonia.ReactiveUI;

namespace Avalonia_MVVM_Aplikace_Kral_Friedl
{
    internal class Program
    {

        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);


        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace()
                .UseReactiveUI(); 
    }
}