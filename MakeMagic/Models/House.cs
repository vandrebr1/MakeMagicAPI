using Newtonsoft.Json;
using System.Collections.Generic;

namespace MakeMagic.Models
{

    /// <summary>
    /// This classe's using for mapping object from HTTPClient - potterApi/houses
    /// </summary>
    public class House
    {
        [JsonProperty("id")]
        public string Id { get; set; }


        [JsonProperty("name")]
        public string Name { get; set; }


        [JsonProperty("headOfHouse")]
        public string HeadOfHouse { get; set; }


        [JsonProperty("values")]
        public List<string> Values { get; set; }


        [JsonProperty("colors")]
        public List<string> Colors { get; set; }


        [JsonProperty("mascot")]
        public string Mascot { get; set; }


        [JsonProperty("houseGhost")]
        public string HouseGhost { get; set; }


        [JsonProperty("founder")]
        public string Founder { get; set; }


        [JsonProperty("school")]
        public string School { get; set; }
    }
}
