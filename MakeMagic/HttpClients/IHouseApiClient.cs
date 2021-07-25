using MakeMagic.Models;
using System.Threading.Tasks;

namespace MakeMagic.HttpClients
{
    public interface IHouseApiClient
    {
        Task<Houses> SelectAllAsync();
    }
}