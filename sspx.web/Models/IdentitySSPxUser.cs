using Microsoft.AspNetCore.Identity;

namespace sspx.web.Models
{
    public class IdentitySSPxUser : IdentityUser
    {
        public int? SSPxUserKey { get; set; }
    }
}
