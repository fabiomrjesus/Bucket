using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AccountServiceContext : IdentityDbContext<AccountServiceUser, IdentityRole<long>, long>
    {
        public AccountServiceContext(DbContextOptions<AccountServiceContext> opts) : base(opts){}
    }
}