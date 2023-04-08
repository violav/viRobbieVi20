using System;
using Core.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;
using Repositories;
using Repositories.NestedDocuments;
using Services.Models.Contracts;
using Services.Models.DTOs;

namespace Services.Classes
{
	public static class TicketTools
	{
        public static IEnumerable<Tickets> FromGroundToTicket(IEnumerable<Ground> docs)
        {
            List<Tickets> tickets = new();
            foreach (Ground doc in docs)
                tickets.Add(new Tickets()
                {
                    Guid = doc.Id,
                    Label = $"{doc.Condominium.Name} - {doc.DateDeadline}",
                    BusinessModule = Models.Contracts.BusinessModule.Ground
                });
            return tickets;
        }

        public static IEnumerable<Tickets> FromCondominiumToTicket(IEnumerable<Condominium> docs)
        {
            List<Tickets> tickets = new();
            foreach (Condominium doc in docs)
                tickets.Add(new Tickets()
                {
                    Guid = doc.Id,
                    Label = doc.Name,
                    BusinessModule = Models.Contracts.BusinessModule.Condominium
                });
            return tickets;
        }
    }
}

