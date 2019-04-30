using Microsoft.AspNetCore.Identity;
using sspx.web.Models;
using System;
using System.Threading.Tasks;

namespace sspx.web.Helpers
{
    public class UsernameInPasswordValidator<TUser> : IPasswordValidator<TUser>
        where TUser : IdentitySSPxUser
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            if(password.Contains(user.UserName, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "UserIdInPassword",
                    Description = "Passwords must not match or contain User ID."
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
