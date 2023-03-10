using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.Serialization;

namespace PortStanleyRun.Api.Models
{
    public class PortStanleyUser
    {
        [SwaggerSchema(ReadOnly = true)]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "userName")]
        public string userName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Pace { get; set; } = string.Empty;
    }
}
