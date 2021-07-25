using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MakeMagic.Models
{
    /// <summary>
    /// This classe's using for mapping list os houses from HTTPClient - potterApi/houses
    /// </summary>
    public class Houses
    {
        /// <summary>
        /// List of houses from HTTPClient - potterApi/houses
        /// </summary>
        [JsonProperty("houses")]
        public List<House> houses { get; set; }

        /// <summary>
        /// Verify if <see cref="House.Id"/> from parameter exists in collection <see cref="houses"/>
        /// </summary>
        /// <param name="houseid"></param>
        /// <returns>return true if exists</returns>
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
