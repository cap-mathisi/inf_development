using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sspx.web.Models;

namespace sspx.web.data
{
    class ApplicationDbContext : IdentityDbContext<IdentitySSPxUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
