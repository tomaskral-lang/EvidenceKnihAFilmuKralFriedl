using System;
using System.Collections.ObjectModel;
using Avalonia_MVVM_Aplikace_Kral_Friedl.Modely;
using ReactiveUI;

namespace Avalonia_MVVM_Aplikace_Kral_Friedl.ModelyZobrazeni
{
    public class PridatOknoViewModel : ReactiveObject
    {
        private string _nazev = string.Empty;
        private string _vybranyZanr = "Sci-Fi";
        private string _delkaText = "";
        private string _hodnoceniText = "";
        private bool _jePrecteno;
        private string _popis = string.Empty;
        private string _chybaValidace = string.Empty;

        public ObservableCollection<string> Zanry { get; } = new() { "Sci-Fi", "Fantasy", "Drama", "Komedie", "Horor" };

        public Action? ZavritOkno { get; set; }
        public MediaObjekt? NovyObjekt { get; private set; }

        public string Nazev
        {
            get => _nazev;
            set => this.RaiseAndSetIfChanged(ref _nazev, value);
        }

        public string VybranyZanr
        {
            get => _vybranyZanr;
            set => this.RaiseAndSetIfChanged(ref _vybranyZanr, value);
        }

        public string DelkaText
        {
            get => _delkaText;
            set => this.RaiseAndSetIfChanged(ref _delkaText, value);
        }

        public string HodnoceniText
        {
            get => _hodnoceniText;
            set => this.RaiseAndSetIfChanged(ref _hodnoceniText, value);
        }

        public bool JePrecteno
        {
            get => _jePrecteno;
            set => this.RaiseAndSetIfChanged(ref _jePrecteno, value);
        }

        public string Popis
        {
            get => _popis;
            set => this.RaiseAndSetIfChanged(ref _popis, value);
        }

        public string ChybaValidace
        {
            get => _chybaValidace;
            set => this.RaiseAndSetIfChanged(ref _chybaValidace, value);
        }

        public void UlozitCommand()
        {
            if (!int.TryParse(DelkaText, out int delka) || delka <= 0)
            {
                ChybaValidace = "Délka musí být číslo větší než 0!";
                return;
            }

            if (!int.TryParse(HodnoceniText, out int hodnoceni) || hodnoceni < 1 || hodnoceni > 5)
            {
                ChybaValidace = "Hodnocení musí být číslo od 1 do 5!";
                return;
            }

            var objekt = new MediaObjekt
            {
                Nazev = this.Nazev,
                Zanr = this.VybranyZanr,
                Delka = delka,
                Hodnoceni = hodnoceni,
                JePrecteno = this.JePrecteno,
                Popis = this.Popis
            };

            if (!objekt.IsValid(out string error))
            {
                ChybaValidace = error;
                return;
            }

            NovyObjekt = objekt;
            ZavritOkno?.Invoke();
        }
    }
}