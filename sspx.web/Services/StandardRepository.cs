using Microsoft.AspNetCore.Mvc;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.infra.config;
using sspx.infra.data;
using System.Collections.Generic;

namespace sspx.web.Services
{
    public class StandardRepository : IAdminRepository<Standard>
    {
        private ISSPxConfig _config;

        public StandardRepository([FromServices] ISSPxConfig config)
        {
            _config = config;
        }

        public Standard Add(Standard standard)
        {
            return SSPxDBHelper.AddStandard(_config.SSPxConnectionString, standard);
        }

        public string Delete(Standard standard)
        {
            return SSPxDBHelper.DeleteStandard(_config.SSPxConnectionString, standard.BasedOnCkey, standard.LastUpdated);
        }

        // TODO CS2:
        public Standard GetByCkey(decimal cKey)
        {
            return SSPxDBHelper.GetStandard(_config.SSPxConnectionString, cKey);
        }

        public Standard GetByKey(int key)
        {
            throw new System.NotImplementedException();
        }

        public List<Standard> List()
        {
            var standards = SSPxDBHelper.GetStandards(_config.SSPxConnectionString);
            if (standards == null)
            {
                standards = new List<Standard>();
            }

            return standards;
        }

        public string Update(Standard standard)
        {
            return SSPxDBHelper.SaveStandard(_config.SSPxConnectionString, standard);
        }
    }
}
