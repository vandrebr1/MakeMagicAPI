using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MakeMagic.Models
{
    public class Houses
    {
        [JsonProperty("houses")]
        public List<House> houses { get; set; }

        internal bool Exists(string houseid)
        {
            if (houses.FirstOrDefault(h => h.Id == houseid) == null)
            {
                return false;
            }

            return true;
        }
    }
}
