using System;
using System.Collections.Generic;
using System.Linq;
using sspx.core.entities;

namespace sspx.web.Services
{
    public class InMemoryQualificationRepository : IQualificationRepository
    {
        private List<Qualification> _qualifications;

        public InMemoryQualificationRepository()
        {
            _qualifications = new List<Qualification>
            {
                    new Qualification{
                        QualificationKey = 1,
                        QualificationTxt = "MD",
                        Description = "Doctor of Medicine",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 3,
                        QualificationTxt = "MD PHD",
                        Description = "Doctor of Medicine and Doctor of Philosophy",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 2,
                        QualificationTxt = "PHD",
                        Description = "Doctor of Philosophy",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 13,
                        QualificationTxt = "FCAP",
                        Description = "FCAP",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 5,
                        QualificationTxt = "DO PhD",
                        Description = "DO AND PhD",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 11,
                        QualificationTxt = "FRACP",
                        Description = "FRACP",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                     },
                    new Qualification{
                        QualificationKey = 6,
                        QualificationTxt = "CTR CCRP",
                        Description = "CTR AND CCRP",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 12,
                        QualificationTxt = "FRCPA",
                        Description = "FRCPA",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 8,
                        QualificationTxt = "Engineer",
                        Description = "Engineer",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 9,
                        QualificationTxt = "MB",
                        Description = "MB",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 10,
                        QualificationTxt = "BS",
                        Description = "BS",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 7,
                        QualificationTxt = "BSc",
                        Description = "BSc",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 14,
                        QualificationTxt = "MBA",
                        Description = "MBA",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 15,
                        QualificationTxt = "MT(ASCP)",
                        Description = "MT(ASCP)",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 4,
                        QualificationTxt = "Other",
                        Description = "Software engineer",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = true
                    },
                    new Qualification{
                        QualificationKey = 100,
                        QualificationTxt = "Disabled",
                        Description = "A Disabled Qualification",
                        CreatedBy = 1.100004300m,
                        LastUpdated = 1.100004300m,
                        Active = false
                    }
                };
        }

        public Qualification Add(Qualification qualification)
        {
            int newKey = 1;

            if (_qualifications.Any())
            {
                newKey = _qualifications.Max(q => q.QualificationKey) + 1;
            }

            var qualificationToAdd = qualification;
            qualificationToAdd.QualificationKey = newKey;
            _qualifications.Add(qualificationToAdd);

            return qualificationToAdd;
        }

        public string Delete(Qualification qualification)
        {
            // TODO CS2:
            // note: LastUpdated should be set to user who deleted it; soft deletes?
            throw new NotImplementedException();
        }

        public Qualification GetByKey(int key)
        {
            return _qualifications.FirstOrDefault(q => q.QualificationKey == key);
        }

        public List<Qualification> List()
        {
            return _qualifications;
        }

        public List<Qualification> ListActive()
        {
            return _qualifications.Where(q => q.Active == true).ToList();
        }

        public string Update(Qualification qualification)
        {
            var qualificationToUpdate = _qualifications.FirstOrDefault(
                q => q.QualificationKey == qualification.QualificationKey
            );

            qualificationToUpdate.QualificationTxt = qualification.QualificationTxt;
            qualificationToUpdate.Description = qualification.Description;
            qualificationToUpdate.CreatedBy = qualification.CreatedBy;
            qualificationToUpdate.LastUpdated = qualification.LastUpdated;
            qualificationToUpdate.Active = qualification.Active;

            return string.Empty;
        }
    }
}
