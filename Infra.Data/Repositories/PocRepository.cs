using Domain.Interfaces;
using Models;


namespace Infra.Data.Repositories
{
    public class PocRepository : IPocRepository
    {
        public IEnumerable<Poc> GetPocList()
        {
            return new List<Poc>
            {
                new Poc {
                    Id = Guid.NewGuid(),
                    Username = "Elham"
                }
            };
        }
    }
}
