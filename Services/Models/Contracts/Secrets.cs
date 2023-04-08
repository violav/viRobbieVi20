using System;
namespace Services.Models.Contracts
{
	public class MongoSecrets
	{
        public const string MongoDB = "MongoDB";

        public string Uri { get; set; } = null!;
    }
}

