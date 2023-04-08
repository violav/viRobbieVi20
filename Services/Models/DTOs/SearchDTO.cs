using System.Text.Json.Serialization;
using Services.Models.Contracts;

namespace Services.Models.DTOs
{
    public class Tickets
    {
        public string Guid { set; get; }
        public string Label { set; get; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BusinessModule BusinessModule { get; set; }
    }
}
