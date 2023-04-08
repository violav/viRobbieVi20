using System;
using System.Text.RegularExpressions;
using Core.Contracts;
using Repositories;
using Repositories.NestedDocuments;
using Robbie.Services;

namespace Services.Classes
{
	public static class SupplierTools
	{
        public static Bank ComposeBank(SupplierFile i_supplierFile)
        {
            string NameChuck = "";
            string IbanChuck = "";

            if (i_supplierFile.Banca != "")
            {
                if (i_supplierFile.Banca.Contains("-"))
                {
                    NameChuck = i_supplierFile.Banca.Split("-")[0];
                    IbanChuck = i_supplierFile.Banca.Split("-")[1].Split("IBAN")[1];
                }
                else if (i_supplierFile.Banca.Contains("IBAN"))
                {
                    IbanChuck = i_supplierFile.Banca.Split("IBAN")[1];
                }
                else
                {
                    IbanChuck = i_supplierFile.Banca;
                }
            }

            return new Bank()
            {
                Iban = IbanChuck,
                Name = NameChuck
            };
        }

        public static Supplier GetSupplierFromFiles(SupplierFileQuery supplierFileQuery)
        {
            SupplierFile supplierFile = supplierFileQuery.Document;
            return new Supplier()
            {
                Email = supplierFile.Email,
                Name = supplierFile.Denominazione,
                FiscalCode = supplierFile.CodFisc,
                VatCode = supplierFile.PartIva,
                Address = new Address()
                {
                    City = supplierFile.Città,
                    Country = supplierFile.Nazione,
                    Street = supplierFile.Indirizzo,
                    PostalCode = supplierFile.CAP,
                    Province = supplierFile.Prov
                },
                Bank = SupplierTools.ComposeBank(supplierFile),
                CreatedAt = new DateTime().Date,
                LastUpdate = new DateTime().Date,
                Fax = supplierFile.Fax,
                Owner = new Person()
                {
                    Alias = $"{supplierFile.Cognome}, {supplierFile.Nome}",
                    Email = ""
                },
                Pec = supplierFile.Pec,
                Telephone = supplierFile.Tel1,
                Note = supplierFile.Note,
                TenantId = supplierFileQuery.TenantId
            };
        }
    }
}

