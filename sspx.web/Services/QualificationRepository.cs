using System;
using System.Collections.Generic;
using sspx.core.entities;
using sspx.infra.config;
using sspx.infra.data;

namespace sspx.web.Services
{
    public class QualificationRepository : IQualificationRepository
    {
        private ISSPxConfig _config;

        public QualificationRepository(ISSPxConfig config)
        {
            _config = config;
        }

        public Qualification Add(Qualification qualification)
        {
            return SSPxDBHelper.AddQualification(_config.SSPxConnectionString, qualification);
        }

        public string Delete(Qualification qualification)
        {
            // TODO: CS2:
            // note: LastUpdated should be set to user who deleted it; Soft Delete?
            throw new NotImplementedException();
        }

        public Qualification GetByKey(int key)
        {
            return SSPxDBHelper.GetQualification(_config.SSPxConnectionString, key);
        }

        public List<Qualification> List()
        {
            var qualifications = SSPxDBHelper.GetQualifications(_config.SSPxConnectionString);
            return qualifications ?? new List<Qualification>();
        }

        public List<Qualification> ListActive()
        {
            var qualifications = SSPxDBHelper.GetQualificationsActive(_config.SSPxConnectionString);
            return qualifications ?? new List<Qualification>();
        }

        public string Update(Qualification qualification)
        {
            return SSPxDBHelper.SaveQualification(_config.SSPxConnectionString, qualification);
        }
    }
}
