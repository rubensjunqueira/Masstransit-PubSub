using Microsoft.EntityFrameworkCore;
using Publisher.Models;
using Publisher.Repositories;

namespace Publisher.AppDbContext
{
    public class PublisherContext : DbContext, IUnitOfWork
    {
        public PublisherContext(DbContextOptions<PublisherContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }

        public bool Save()
        {
            return base.SaveChanges() > 0;
        }
    }
}
