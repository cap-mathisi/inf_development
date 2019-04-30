using System.Collections.Generic;
using System.Linq;
using sspx.core.entities;
using sspx.core.interfaces;

namespace sspx.web.Services
{
    public class InMemoryStandardRepository : IAdminRepository<Standard>
    {
        private Dictionary<decimal, Standard> _standard;

        public InMemoryStandardRepository()
        {
            _standard = new Dictionary<decimal, Standard>
            {
                {
                    12.100004300m,
                    new Standard{
                        BasedOnCkey = 12.100004300m,
                        BasedOnKey = 12,
                        Namespace = 1000043,
                        BasedOn = "Includes pTNM requirements from the 8th Edition, AJCC Staging Manual",
                        Description = "Includes pTNM requirements from the 8th Edition, AJCC Staging Manual",
                        SortOrder = 10,
                        Active = true
                    }
                },
                {
                    6.100004300m,
                    new Standard{
                        BasedOnCkey = 6.100004300m,
                        BasedOnKey = 6,
                        Namespace = 1000043,
                        BasedOn = "AJCC/UICC TNM, 8th edition",
                        Description = "AJCC/UICC TNM, 8th edition",
                        SortOrder = 20,
                        Active = true
                    }
                },
                {
                    9.100004300m,
                    new Standard{
                        BasedOnCkey = 9.100004300m,
                        BasedOnKey = 9,
                        Namespace = 1000043,
                        BasedOn = "No AJCC/UICC TNM Staging System",
                        Description = "No AJCC/UICC TNM Staging System",
                        SortOrder = 30,
                        Active = true
                    }
                },
                {
                    11.100004300m,
                    new Standard{
                        BasedOnCkey = 11.100004300m,
                        BasedOnKey = 11,
                        Namespace = 1000043,
                        BasedOn = "FIGO 2015 Cancer Report",
                        Description = "FIGO 2015 Cancer Report",
                        SortOrder = 40,
                        Active = true
                    }
                },
                {
                    5.100004300m,
                    new Standard{
                        BasedOnCkey = 5.100004300m,
                        BasedOnKey = 5,
                        Namespace = 1000043,
                        BasedOn = "FIGO 2014 Annual Report",
                        Description = "FIGO 2014 Annual Report",
                        SortOrder = 45,
                        Active = true
                    }
                },
                {
                    10.100004300m,
                    new Standard{
                        BasedOnCkey = 10.100004300m,
                        BasedOnKey = 10,
                        Namespace = 1000043,
                        BasedOn = "Other",
                        Description = "Other",
                        SortOrder = 50,
                        Active = false
                    }
                },
                {
                    2.100004300m,
                    new Standard{
                        BasedOnCkey = 2.100004300m,
                        BasedOnKey = 2,
                        Namespace = 1000043,
                        BasedOn = "AJCC/UICC TNM, 7th edition",
                        Description = "AJCC/UICC TNM, 7th edition",
                        SortOrder = 70,
                        Active = true
                    }
                }
            };
        }

        public Standard Add(Standard standard)
        {
            decimal cKey = 1.0m;
            if (_standard.Values.Any())
            {
                cKey = _standard.Values.Max(q => q.BasedOnCkey) + 1.0m;
            }
            standard.BasedOnCkey = cKey;
            standard.SortOrder = _standard.Values.Select(s => s.SortOrder).DefaultIfEmpty().Max() + 10;

            _standard.Add(cKey, standard);

            return standard;
        }

        public string Delete(Standard standard)
        {
            var standardToUpdate = _standard[standard.BasedOnCkey];
            standardToUpdate.Active = false;
            standardToUpdate.LastUpdated = standard.LastUpdated;
            return string.Empty;
        }

        // TODO CS2:
        public Standard GetByCkey(decimal cKey)
        {
            return _standard[cKey];
        }

        public Standard GetByKey(int key)
        {
            throw new System.NotImplementedException();
        }

        public List<Standard> List()
        {
            return _standard.Values.OrderBy(q => q.SortOrder).ToList();
        }

        public string Update(Standard standard)
        {
            var standardToUpdate = _standard[standard.BasedOnCkey];

            standardToUpdate.BasedOn = standard.BasedOn;
            standardToUpdate.Description = standard.Description;
            standardToUpdate.LastUpdated = standard.LastUpdated;
            standardToUpdate.Active = standard.Active;

            return string.Empty;
        }
    }
}
