using System;
using System.Diagnostics.Metrics;
using Core.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;
using Repositories;
using Repositories.MongoConfiguration;
using Repositories.NestedDocuments;
using Services.Models.Contracts;
using Services.Models.DTOs;

namespace Services.Classes
{
	public static class SearchBusinessModuleRequest<T> where T : CheckTicketDocument
    {
		public static FilterDefinition<T> CommonSearch(BusinessDocumentRequest businessDocumentRequest)
		{
            var builder = Builders<T>.Filter;

            var filter = builder.In(x => x.SupplierMaintenance.Id, businessDocumentRequest.SupplierIds);
            filter |= builder.In(x => x.SupplierCheck.Id, businessDocumentRequest.SupplierIds);
            if (businessDocumentRequest.FromDate != null)
                filter &= builder.Gte(x => x.LastUpdate, businessDocumentRequest.FromDate);
            if (businessDocumentRequest.ToDate != null)
                filter &= builder.Lte(x => x.LastUpdate, businessDocumentRequest.ToDate);
            if (businessDocumentRequest.IsOpened != null)
                filter &= builder.Eq(x => x.IsOpened, businessDocumentRequest.IsOpened);

            return filter;
        }
    }


    public static class SearchCoreModuleRequest
    {
        public static FilterDefinition<Condominium> SearchCondominiumTickets(string tenantId) =>
            Builders<Condominium>.Filter.Eq(x => x.TenantId, tenantId);
    }
}

