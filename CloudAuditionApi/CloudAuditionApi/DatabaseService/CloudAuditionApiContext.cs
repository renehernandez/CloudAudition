using CloudAuditionApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudAuditionApi.DatabaseService
{
    public class CloudAuditionApiContext : DbContext
    {
        public CloudAuditionApiContext(DbContextOptions<CloudAuditionApiContext> options) 
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }

    public class CloudAuditionApiDbOptions 
    {
        public CloudAuditionApiDbOptions()
        {
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string DatabaseName { get; set; }
    }
}