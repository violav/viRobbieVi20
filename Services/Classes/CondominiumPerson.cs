using System;
using Core.Contracts;
using Repositories.NestedDocuments;

namespace Services.Classes
{
	public static class CondominiumPerson
	{
		public static CondominiumPeople AddNewPerson(CondominiumPeopleFileQuery condominiumPeopleQuery)
		{
            CondominiumPeopleFile condominiumPeople = condominiumPeopleQuery.Document;
            return new CondominiumPeople()
            {
                Email = condominiumPeople.Email,
                Alias = condominiumPeople.Denominazione,
                Title = condominiumPeople.Titolo,
                Note = condominiumPeople.Note,
                Telephone = condominiumPeople.Tel1,
                TenantId = condominiumPeopleQuery.TenantId,
                CreatedAt = new DateTime(),
                LastUpdate = new DateTime()
            };
        }

        public static CondominiumPeople UpdatePerson(CondominiumPeopleQuery condominiumPeopleQuery)
        {
            CondominiumPeople condominiumPeople = condominiumPeopleQuery.Document;
            return new CondominiumPeople()
            {
                Email = condominiumPeople.Email,
                Alias = condominiumPeople.Alias,
                Title = condominiumPeople.Title,
                Note = condominiumPeople.Note,
                Telephone = condominiumPeople.Telephone,
                TenantId = condominiumPeopleQuery.TenantId,
                CreatedAt = condominiumPeople.CreatedAt,
                LastUpdate = new DateTime()
            };
        }
    }
}

