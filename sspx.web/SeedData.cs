using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sspx.core.entities;
using sspx.infra.data;

namespace sspx.web
{
    public static class SeedData
    {
        public static void LoadData(AppDbContext dbContext)
        {
            LoadUsers(dbContext);
            LoadProtocols(dbContext);
        }

        #region
        public static void LoadUsers(AppDbContext dbContext)
        {
            var users = dbContext.Users;
            foreach (var user in users)
            {
                dbContext.Remove(user);
            }
            dbContext.SaveChanges();
            dbContext.Users.Add(new User()
            {
                UserId = "123",
                FirstName = "Mary"
            });
            dbContext.Users.Add(new User()
            {
                UserId = "124",
                FirstName = "Harry"
            });
            dbContext.SaveChanges();
            dbContext.Users.Add(new User()
            {
                UserId = "125",
                FirstName = "Maria"
            });
            dbContext.SaveChanges();
        }
        #endregion

        #region
        public static void LoadProtocols(AppDbContext dbContext)
        {
            var protocols = dbContext.Protocols;
            foreach (var protocol in protocols)
            {
                dbContext.Remove(protocol);
            }
            dbContext.SaveChanges();
            dbContext.Protocols.Add(new Protocol()
            {
                ProtocolId = 100.123m,
                ProtocolName = "Breast, Template 123"
            });
            dbContext.Protocols.Add(new Protocol()
            {
                ProtocolId = 100.124m,
                ProtocolName = "Colon, Template 124"
            });
            dbContext.SaveChanges();
            dbContext.Protocols.Add(new Protocol()
            {
                ProtocolId = 100.125m,
                ProtocolName = "Prostate, Template 125"
            });
            dbContext.SaveChanges();
        }
        #endregion
    }
}