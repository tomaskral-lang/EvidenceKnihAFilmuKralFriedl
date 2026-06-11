using System;

namespace Avalonia_MVVM_Aplikace_Kral_Friedl.Modely
{
    public class MediaObjekt
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string Nazev { get; set; } = string.Empty;
        public string Zanr { get; set; } = "Neznámý";
        public string Popis { get; set; } = string.Empty;
        public int Delka { get; set; } 
        public int Hodnoceni { get; set; } // Očekává 1 až 5
        public bool JePrecteno { get; set; } 

        public bool IsValid(out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(Nazev))
            {
                errorMessage = "Název nesmí být prázdný.";
                return false;
            }
            if (Delka <= 0)
            {
                errorMessage = "Délka musí být větší než 0.";
                return false;
            }
            if (Hodnoceni < 1 || Hodnoceni > 5)
            {
                errorMessage = "Hodnocení musí být mezi 1 a 5.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }
    }
}