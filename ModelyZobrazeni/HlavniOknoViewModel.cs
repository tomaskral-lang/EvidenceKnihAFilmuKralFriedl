using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using Avalonia_MVVM_Aplikace_Kral_Friedl.Modely;
using ReactiveUI;

namespace Avalonia_MVVM_Aplikace_Kral_Friedl.ModelyZobrazeni
{
    public class HlavniOknoViewModel : ReactiveObject
    {
        private string _searchQuery = string.Empty;
        private string _selectedGenreFilter = "Vše";
        private MediaObjekt? _selectedItem;

        public ObservableCollection<MediaObjekt> VsechnyPolozky { get; set; } = new();
        public ObservableCollection<MediaObjekt> FiltrovanePolozky { get; set; } = new();
        public ObservableCollection<string> Zanry { get; } = new() { "Vše", "Sci-Fi", "Fantasy", "Drama", "Komedie", "Horor" };

        private readonly string _appDataSlozka;
        private readonly string _souborDatabaze;

        public HlavniOknoViewModel()
        {
            string systemAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _appDataSlozka = Path.Combine(systemAppData, "EvidenceKnihAFilmu_Kral_Friedl");
            _souborDatabaze = Path.Combine(_appDataSlozka, "databaze.txt");

            if (!Directory.Exists(_appDataSlozka))
            {
                Directory.CreateDirectory(_appDataSlozka);
            }

            NactiDataZAppData();
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                this.RaiseAndSetIfChanged(ref _searchQuery, value);
                AplikujFiltr();
            }
        }

        public string SelectedGenreFilter
        {
            get => _selectedGenreFilter;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedGenreFilter, value);
                AplikujFiltr();
            }
        }

        public MediaObjekt? SelectedItem
        {
            get => _selectedItem;
            set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
        }

        public void AplikujFiltr()
        {
            var vysledek = VsechnyPolozky.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                vysledek = vysledek.Where(i => i.Nazev.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            if (SelectedGenreFilter != "Vše")
            {
                vysledek = vysledek.Where(i => i.Zanr == SelectedGenreFilter);
            }

            FiltrovanePolozky.Clear();
            foreach (var polozka in vysledek)
            {
                FiltrovanePolozky.Add(polozka);
            }
        }

        public void SmazatPolozku()
        {
            if (SelectedItem != null)
            {
                VsechnyPolozky.Remove(SelectedItem);
                UlozDataDoAppData();
                AplikujFiltr();
            }
        }

        public void UlozDataDoAppData()
        {
            try
            {
                string json = JsonSerializer.Serialize(VsechnyPolozky);
                File.WriteAllText(_souborDatabaze, json);
            }
            catch { }
        }

        private void NactiDataZAppData()
        {
            if (File.Exists(_souborDatabaze))
            {
                try
                {
                    string json = File.ReadAllText(_souborDatabaze);
                    var nactene = JsonSerializer.Deserialize<ObservableCollection<MediaObjekt>>(json);
                    if (nactene != null)
                    {
                        VsechnyPolozky = nactene;
                    }
                }
                catch 
                {
                    VsechnyPolozky = new ObservableCollection<MediaObjekt>();
                }
            }
            AplikujFiltr();
        }
    }
}