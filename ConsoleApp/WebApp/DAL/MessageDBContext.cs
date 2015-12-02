using System.Data.Entity;
using WebApp.Models;

namespace WebApp.DAL
{
    public class MessageDBContext : DbContext
    {
        public MessageDBContext() : base("DefaultConnection")
        {
            Database.SetInitializer<MessageDBContext>
                (new DropCreateDatabaseIfModelChanges<MessageDBContext>());
        }

        public DbSet<MessageDBModel> Messages { get; set; }
    }
}