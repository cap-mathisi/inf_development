using System;
using System.Collections.Generic;
using sspx.core.entities;
using sspx.infra.config;
using sspx.infra.data;

namespace sspx.web.Services
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private ISSPxConfig _config;

        public SpecialtyRepository(ISSPxConfig config)
        {
            _config = config;
        }

        public List<Specialty> List()
        {
            var specialties = SSPxDBHelper.GetSpecialtiesActive(_config.SSPxConnectionString);
            return specialties ?? new List<Specialty>();
        }
    }
}
