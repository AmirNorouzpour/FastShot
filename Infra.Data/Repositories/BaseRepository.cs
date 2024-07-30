using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.Repositories
{
    public abstract class BaseRepository
    {
        public SqlConnection _Connection { get; set; }
        public BaseRepository(IConfiguration configuration)
        {
            _Connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}