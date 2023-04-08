using CsvHelper.Configuration.Attributes;

namespace Core.Contracts
{
    public class SupplierFile
    {
        public string? Denominazione { get; set; }
        public string? Titolo { get; set; }
        public string? Indirizzo { get; set; }
        public string? CAP { get; set; }
        public string? Città { get; set; }
        public string? Prov { get; set; }
        public string? Nazione { get; set; }
        public string? CodFisc { get; set; }
        public string? PartIva { get; set; }
        public string? Tel1 { get; set; }
        public string? Tel2 { get; set; }
        public string? Tel3 { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Pec { get; set; }
        [Name("Spedizioni: nome")]
        public string? SpedizioniNome { get; set; }
        [Name("Spedizioni: Indirizzo")]
        public string? SpedizioniIndirizzo { get; set; }
        [Name("Spedizioni: CAP")]
        public string? SpedizioniCAP { get; set; }
        [Name("Spedizioni: Città")]
        public string? SpedizioniCittà { get; set; }
        [Name("Spedizioni: Prov")]
        public string? SpedizioniProv { get; set; }
        [Name("Spedizioni: Nazione")]
        public string? SpedizioniNazione { get; set; }
        public string? Note { get; set; }
        public string? Attività { get; set; }
        public string? Pagamento { get; set; }
        public string? Banca { get; set; }
        [Name("Anagrafe Tributaria")]
        public string? AnagrafeTributaria { get; set; }
        public string? Cognome { get; set; }
        public string? Nome { get; set; }
        [Name("Comune di nascita")]
        public string? ComuneDiNascita { get; set; }
        [Name("Provincia di nascita")]
        public string? ProvinciaDiNascita { get; set; }
        [Name("Data di nascita")]
        public string? DataDiNascita { get; set; }
    }
}


