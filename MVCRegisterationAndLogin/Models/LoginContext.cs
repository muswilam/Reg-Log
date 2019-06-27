using System.Data.Entity;

namespace MVCRegisterationAndLogin.Models
{
    public class LoginContext : DbContext
    {
        public LoginContext()
            : base("name=LoginContext")
        {

        }

        public DbSet<User> Users { get; set; }

    }
}