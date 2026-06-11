using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Avalonia_MVVM_Aplikace_Kral_Friedl.Zobrazeni
{
    public partial class DetailOkno : Window
    {
        public DetailOkno()
        {
            InitializeComponent();
        }

        private void Zavrit_Click(object? sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}