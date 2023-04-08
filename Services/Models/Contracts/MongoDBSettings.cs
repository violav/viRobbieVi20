namespace Services.Models.Contracts
{
    public class MongoDBSettings
    {
        public const string MongoDB = "MongoDB";

        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;

        #region Collections
        public string CondominiumCollection { get; set; } = null!;
        public string SupplierCollection { get; set; } = null!;
        public string UserCollection { get; set; } = null!;
        public string FireCollection { get; set; } = null!;
        public string LiftCollection { get; set; } = null!;
        public string GroundCollection { get; set; } = null!;
        public string AccidentCollection { get; set; } = null!;
        public string CondominiumPeopleCollection { get; set; } = null!;
        public string GreenPrunningCollection { get; set; } = null!;
        public string AssembleeCollection { get; set; } = null!;
        #endregion
    }
}
