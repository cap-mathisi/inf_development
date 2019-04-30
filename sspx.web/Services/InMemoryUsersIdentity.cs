using Microsoft.AspNetCore.Identity;
using sspx.web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace sspx.web.Services
{
    // CS2. Note that Create/Update/Delete methods are the only ones that PERSIST the user. The rest should just update the POCO.
    // https://github.com/aspnet/Identity/issues/1865#issuecomment-404647834

    public class InMemoryUsersIdentity : IUserStore<IdentitySSPxUser>, IUserPasswordStore<IdentitySSPxUser>, IUserEmailStore<IdentitySSPxUser>
    {
        private List<IdentitySSPxUser> _identitySSPxUsers;

        public InMemoryUsersIdentity()
        {
            _identitySSPxUsers = new List<IdentitySSPxUser>
            {
                new IdentitySSPxUser{
                    Id = "520edabe-8e00-43bf-af43-41ff2ce98cf5",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "test_admin@cap.org",
                    NormalizedEmail = "TEST_ADMIN@CAP.ORG",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEEbg7aDuaU/YnWYNO1uHdpnAPheyCw2da4oPhC0FLTE0/5JtadVtqYXgNPLAtkg1NA==",
                    SecurityStamp = "O6NJVHGR2SOJWVQSX6GNJCYVDQNGDP4O",
                    ConcurrencyStamp = "b708e173-fcb0-44a0-be17-96dfc230015d",
	                // PhoneNumber = "",
	                PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
	                // LockoutEnd = "",
	                LockoutEnabled = true,
                    AccessFailedCount = 0,
                    SSPxUserKey = 194
                },
                new IdentitySSPxUser{
                    Id = "1b793e10-4217-46da-b4f7-f978066da691",
                    UserName = "test",
                    NormalizedUserName = "TEST",
                    Email = "test@cap.org",
                    NormalizedEmail = "TEST@CAP.ORG",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEEbg7aDuaU/YnWYNO1uHdpnAPheyCw2da4oPhC0FLTE0/5JtadVtqYXgNPLAtkg1NA==",
                    SecurityStamp = "O6NJVHGR2SOJWVQSX6GNJCYVDQNGDP4O",
                    ConcurrencyStamp = "b708e173-fcb0-44a0-be17-96dfc230015d",
	                // PhoneNumber = "",
	                PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
	                // LockoutEnd = "",
	                LockoutEnabled = true,
                    AccessFailedCount = 0,
                    SSPxUserKey = 116
                },
                new IdentitySSPxUser{
                    Id = "7c860f16-9714-4037-a46c-0c363d5db6e7",
                    UserName = "staff",
                    NormalizedUserName = "STAFF",
                    Email = "test_staff@cap.org",
                    NormalizedEmail = "TEST_STAFF@CAP.ORG",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEEbg7aDuaU/YnWYNO1uHdpnAPheyCw2da4oPhC0FLTE0/5JtadVtqYXgNPLAtkg1NA==",
                    SecurityStamp = "O6NJVHGR2SOJWVQSX6GNJCYVDQNGDP4O",
                    ConcurrencyStamp = "b708e173-fcb0-44a0-be17-96dfc230015d",
	                // PhoneNumber = "",
	                PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
	                // LockoutEnd = "",
	                LockoutEnabled = true,
                    AccessFailedCount = 0,
                    SSPxUserKey = 203
                }
            };
        }

        public Task<IdentityResult> CreateAsync(IdentitySSPxUser user, CancellationToken cancellationToken)
        {
            _identitySSPxUsers.Add(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(IdentitySSPxUser user, CancellationToken cancellationToken)
        {
            _identitySSPxUsers.Remove(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public Task<IdentitySSPxUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.FromResult(_identitySSPxUsers.FirstOrDefault(u => u.NormalizedEmail == normalizedEmail));
        }

        public Task<IdentitySSPxUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_identitySSPxUsers.FirstOrDefault(u => u.Id == userId));
        }

        public Task<IdentitySSPxUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task.FromResult(_identitySSPxUsers.FirstOrDefault(u => u.NormalizedUserName == normalizedUserName));
        }

        public Task<string> GetEmailAsync(IdentitySSPxUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentitySSPxUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(_identitySSPxUsers.FirstOrDefault(u => u.Id == user.Id).EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(IdentitySSPxUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(_identitySSPxUsers.FirstOrDefault(u => u.Id == user.Id).NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(IdentitySSPxUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(_identitySSPxUsers.FirstOrDefault(u => u.Id == user.Id).NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(IdentitySSPxUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(_identitySSPxUsers.FirstOrDefault(u => u.Id == user.Id).PasswordHash);
        }

        public Task<string> GetUserIdAsync(IdentitySSPxUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(IdentitySSPxUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(IdentitySSPxUser user, CancellationToken cancellationToken)
        {
            bool hasPassword = (string.IsNullOrEmpty(user.PasswordHash) == false);
            return Task.FromResult(hasPassword);
        }

        public Task SetEmailAsync(IdentitySSPxUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(IdentitySSPxUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(IdentitySSPxUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(IdentitySSPxUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(IdentitySSPxUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(IdentitySSPxUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(IdentitySSPxUser user, CancellationToken cancellationToken)
        {
            var userToUpdate = _identitySSPxUsers.FirstOrDefault(u => u.Id == user.Id);
            userToUpdate.AccessFailedCount = user.AccessFailedCount;
            userToUpdate.ConcurrencyStamp = user.ConcurrencyStamp;
            userToUpdate.Email = user.Email;
            userToUpdate.EmailConfirmed = user.EmailConfirmed;
            userToUpdate.LockoutEnabled = user.LockoutEnabled;
            userToUpdate.LockoutEnd = user.LockoutEnd;
            userToUpdate.NormalizedEmail = user.NormalizedEmail;
            userToUpdate.NormalizedUserName = user.NormalizedUserName;
            userToUpdate.PasswordHash = user.PasswordHash;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            userToUpdate.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            userToUpdate.SecurityStamp = user.SecurityStamp;
            userToUpdate.SSPxUserKey = user.SSPxUserKey;
            userToUpdate.TwoFactorEnabled = user.TwoFactorEnabled;
            userToUpdate.UserName = user.UserName;

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
