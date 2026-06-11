using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia_MVVM_Aplikace_Kral_Friedl.ModelyZobrazeni;

namespace Avalonia_MVVM_Aplikace_Kral_Friedl.Zobrazeni
{
    public partial class HlavniOkno : Window
    {
        public HlavniOkno()
        {
            InitializeComponent();
            DataContext = new HlavniOknoViewModel();
        }

        private async void Pridat_Click(object? sender, RoutedEventArgs e)
        {
            var dialog = new PridatOkno();
            var vm = new PridatOknoViewModel();
            dialog.DataContext = vm;
            
            vm.ZavritOkno = () => dialog.Close();

            await dialog.ShowDialog(this);

            if (vm.NovyObjekt != null && DataContext is HlavniOknoViewModel hlavniVm)
            {
                hlavniVm.VsechnyPolozky.Add(vm.NovyObjekt);
                hlavniVm.UlozDataDoAppData();
                hlavniVm.AplikujFiltr();
            }
        }

        private async void Detail_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is HlavniOknoViewModel hlavniVm && hlavniVm.SelectedItem != null)
            {
                var detailOkno = new DetailOkno();
                detailOkno.DataContext = hlavniVm.SelectedItem;
                await detailOkno.ShowDialog(this);
            }
        }

        private void Export_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is HlavniOknoViewModel hlavniVm)
            {
                try
                {
                    string plocha = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string souborExportu = Path.Combine(plocha, "export_evidence.txt");

                    using (StreamWriter writer = new StreamWriter(souborExportu))
                    {
                        writer.WriteLine("=== EXPORT EVIDENCE ===");
                        writer.WriteLine($"Vygenerováno: {DateTime.Now}");
                        writer.WriteLine("=======================");
                        
                        foreach (var polozka in hlavniVm.FiltrovanePolozky)
                        {
                            string stav = polozka.JePrecteno ? "ANO" : "NE";
                            writer.WriteLine($"Název: {polozka.Nazev} | Žánr: {polozka.Zanr}");
                            writer.WriteLine($"Délka: {polozka.Delka} str/min | Hodnocení: {polozka.Hodnoceni}/5");
                            writer.WriteLine($"Přečteno/Viděno: {stav}");
                            writer.WriteLine($"Popis: {polozka.Popis}");
                            writer.WriteLine("-------------------------------------------");
                        }
                    }
                }
                catch { }
            }
        }
    }
}