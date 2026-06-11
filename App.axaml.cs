using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia_MVVM_Aplikace_Kral_Friedl.Zobrazeni;

namespace Avalonia_MVVM_Aplikace_Kral_Friedl
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new HlavniOkno();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}