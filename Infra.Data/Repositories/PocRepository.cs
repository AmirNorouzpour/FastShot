using Domain.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infra.Data.Repositories
{
    public class PocRepository : IPocRepository
    {
        public IEnumerable<Poc> GetPocList()
        {
            throw new NotImplementedException();
        }
    }
}
