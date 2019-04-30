using Microsoft.AspNetCore.Identity;
using sspx.web.Models;
using System.Threading.Tasks;

namespace sspx.web.Helpers
{
    public class NoAsteriskPasswordValidator<TUser> : IPasswordValidator<TUser>
        where TUser : IdentitySSPxUser
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            if (password.Contains("*"))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "AsteriskIdInPassword",
                    Description = "Passwords must not contain \"*\"."
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
