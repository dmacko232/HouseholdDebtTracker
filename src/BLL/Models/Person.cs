using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HouseholdDebtTracker.BLL.Models
{
    /// <summary>
    /// Enum used to represent business logic person
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Person
    {
        public int? ID { get; set; }
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string NickName { get; set; }
        public List<Debt> Debts { get; set; }
    }
}