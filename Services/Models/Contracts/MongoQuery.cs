

namespace Services.Models.Contracts
{
	public class IMongoQuery<T>
	{
		public T Document { get; set; } = default!;
		public string TenantId { get; set; } = default!;
	}

}

