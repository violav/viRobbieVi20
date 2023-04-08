using System;
namespace Repositories.NestedDocuments
{
	public class Deductible
	{
        // FRANCHIGIA RICERCA DEL GUASTO    
        public int? SeekingDamage { get; set; }

        // FRANCHIGIA ACQUA CONDOTTA
        public int? Water { get; set; }

        // FRANCHIGIA DANNI ELETTRICI 
        public int? Electricity { get; set; }

        // FRANCHIGIA EVENTI ATMOSFERICI
        public int? Atmosphere { get; set; }

        // Email Assicuratore Acqua condotta
        public string? EmailWaterAssurance { get; set; }

        // Email Assicuratore DANNI ELETTRICI 
        public string? EmailElectricityAssurance { get; set; }

        // Email Assicuratore EVENTI ATMOSFERICI
        public string? EmailAtmosphereAssurance { get; set; }

        // Email Assicuratore  RICERCA DEL GUASTO   
        public string? EmailSeekingDamageAssurance { get; set; }
    }
}

