using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakeMagic.HttpClients
{
    public interface IApiConfig
    {
        string BaseUrl { get; set; }
        string Token { get; set; }
    }

    public class ApiConfig : IApiConfig
    {

        public string BaseUrl { get; set; }
        public string Token { get; set; }
    }
}
