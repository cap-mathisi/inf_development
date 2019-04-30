using Microsoft.AspNetCore.Mvc;
using sspx.core.entities;
using sspx.infra.config;
using sspx.infra.data;
using System.Collections.Generic;

namespace sspx.web.Services
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private ISSPxConfig _config;

        public UserTypeRepository([FromServices] ISSPxConfig config)
        {
            _config = config;
        }

        public List<UserType> List()
        {
            var userTypes = SSPxDBHelper.GetUserTypesActive(_config.SSPxConnectionString);
            if (userTypes == null)
            {
                userTypes = new List<UserType>();
            }

            return userTypes;
        }
    }
}
