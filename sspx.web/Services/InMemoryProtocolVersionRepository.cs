using sspx.core.entities;
using sspx.core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sspx.web.Services
{
    public class InMemoryProtocolVersionRepository : IProtocolVersionRepository
    {
        private List<ProtocolVersion> _protocolVersions;

        public InMemoryProtocolVersionRepository()
        {
            _protocolVersions = new List<ProtocolVersion>
            {
                new ProtocolVersion{
                    ProtocolVersionKey = 24,
                    NamespaceKey =  null,
                    ProtocolKey = 5,
                    ProtocolVersionText = "3.0.0.0",
                    TestVersion = true,
                    Title = "Breast Invasive",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2011-02-01"),
                    WebPostingDate = Convert.ToDateTime("2011-02-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 154,
                    NamespaceKey =  null,
                    ProtocolKey = 5,
                    ProtocolVersionText = "3.1.0.0",
                    TestVersion = true,
                    Title = "Breast Invasive",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2013-01-01"),
                    WebPostingDate = Convert.ToDateTime("2013-01-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 205,
                    NamespaceKey =  null,
                    ProtocolKey = 5,
                    ProtocolVersionText = "3.3.0.0",
                    TestVersion = true,
                    Title = "Breast Invasive",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2016-08-01"),
                    WebPostingDate = Convert.ToDateTime("2016-08-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-11-03 15:36:01.400"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 9,
                    NamespaceKey =  null,
                    ProtocolKey = 7,
                    ProtocolVersionText = "3.1.0.0",
                    TestVersion = true,
                    Title = "Colon and Rectum",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2011-02-01"),
                    WebPostingDate = Convert.ToDateTime("2011-02-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 139,
                    NamespaceKey =  null,
                    ProtocolKey = 7,
                    ProtocolVersionText = "3.2.0.0",
                    TestVersion = true,
                    Title = "Colon and Rectum",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2013-01-01"),
                    WebPostingDate = Convert.ToDateTime("2013-01-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 74,
                    NamespaceKey =  null,
                    ProtocolKey = 7,
                    ProtocolVersionText = "3.3.0.0",
                    TestVersion = true,
                    Title = "Colon and Rectum",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2013-10-01"),
                    WebPostingDate = Convert.ToDateTime("2013-10-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 207,
                    NamespaceKey =  null,
                    ProtocolKey = 7,
                    ProtocolVersionText = "3.4.0.0",
                    TestVersion = true,
                    Title = "Colon and Rectum",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2016-08-01"),
                    WebPostingDate = Convert.ToDateTime("2016-08-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-11-03 15:36:01.400"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 32,
                    NamespaceKey =  null,
                    ProtocolKey = 27,
                    ProtocolVersionText = "3.1.0.0",
                    TestVersion = true,
                    Title = "Nasal Cavity and Paranasal Sinuses",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2011-02-01"),
                    WebPostingDate = Convert.ToDateTime("2011-02-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 162,
                    NamespaceKey =  null,
                    ProtocolKey = 27,
                    ProtocolVersionText = "3.1.0.1",
                    TestVersion = true,
                    Title = "Nasal Cavity and Paranasal Sinuses",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2013-01-01"),
                    WebPostingDate = Convert.ToDateTime("2013-01-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 97,
                    NamespaceKey =  null,
                    ProtocolKey = 27,
                    ProtocolVersionText = "3.2.0.0",
                    TestVersion = true,
                    Title = "Nasal Cavity and Paranasal Sinuses",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2013-10-01"),
                    WebPostingDate = Convert.ToDateTime("2013-10-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 42,
                    NamespaceKey =  null,
                    ProtocolKey = 36,
                    ProtocolVersionText = "3.1.0.0",
                    TestVersion = true,
                    Title = "Pharynx",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2011-02-01"),
                    WebPostingDate = Convert.ToDateTime("2011-02-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 172,
                    NamespaceKey =  null,
                    ProtocolKey = 36,
                    ProtocolVersionText = "3.2.0.0",
                    TestVersion = true,
                    Title = "Pharynx",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2013-01-01"),
                    WebPostingDate = Convert.ToDateTime("2013-01-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 107,
                    NamespaceKey =  null,
                    ProtocolKey = 36,
                    ProtocolVersionText = "3.3.0.0",
                    TestVersion = true,
                    Title = "Pharynx",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2013-10-01"),
                    WebPostingDate = Convert.ToDateTime("2013-10-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 59,
                    NamespaceKey =  null,
                    ProtocolKey = 52,
                    ProtocolVersionText = "3.1.0.0",
                    TestVersion = true,
                    Title = "Urethra",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2011-02-01"),
                    WebPostingDate = Convert.ToDateTime("2011-02-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 189,
                    NamespaceKey =  null,
                    ProtocolKey = 52,
                    ProtocolVersionText = "3.2.0.0",
                    TestVersion = true,
                    Title = "Urethra",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2013-01-01"),
                    WebPostingDate = Convert.ToDateTime("2013-01-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                },
                new ProtocolVersion{
                    ProtocolVersionKey = 124,
                    NamespaceKey =  null,
                    ProtocolKey = 52,
                    ProtocolVersionText = "3.2.1.0",
                    TestVersion = true,
                    Title = "Urethra",
                    SubTitle = string.Empty,
                    ReleaseDate = Convert.ToDateTime("2013-10-01"),
                    WebPostingDate = Convert.ToDateTime("2013-10-01"),
                    UserKey = 194,
                    LastUpdated = 1,
                    LastUpdatedDt = Convert.ToDateTime("2016-10-18 12:25:46.490"),
                    Active = true
                }
            };
        }

        public ProtocolVersion Add(ProtocolVersion protocolVersion)
        {
            int newKey = 1;
            if (_protocolVersions.Any())
            {
                newKey = _protocolVersions.Max(pv => pv.ProtocolVersionKey) + 1;
            }

            var protocolVersionToAdd = protocolVersion;
            protocolVersionToAdd.ProtocolVersionKey = newKey;

            _protocolVersions.Add(protocolVersionToAdd);

            return protocolVersionToAdd;
        }

        public string Delete(ProtocolVersion protocolVersion)
        {
            var protocolVersionToUpdate = _protocolVersions.FirstOrDefault(pv => pv.ProtocolVersionKey == protocolVersion.ProtocolVersionKey);
            protocolVersionToUpdate.Active = false;
            return string.Empty;
        }

        public ProtocolVersion GetByKey(int key)
        {
            return _protocolVersions.FirstOrDefault(pv => pv.ProtocolVersionKey == key);
        }

        public ProtocolVersion GetLatestVersionForProtocol(int protocolKey)
        {
            var versionsForProtocol = _protocolVersions.Where(pv => pv.ProtocolKey == protocolKey && pv.Active == true);
            if (versionsForProtocol.Any() == false)
            {
                return null;
            }

            return versionsForProtocol.OrderByDescending(pv => pv.ProtocolVersionText).First();
        }

        public ProtocolVersion GetLatestVersionForProtocolIncludingInactive(int protocolKey)
        {
            var versionsForProtocol = _protocolVersions.Where(pv => pv.ProtocolKey == protocolKey);
            if (versionsForProtocol.Any() == false)
            {
                return null;
            }

            return versionsForProtocol.OrderByDescending(pv => pv.ProtocolVersionText).First();
        }

        public List<ProtocolVersion> List()
        {
            return _protocolVersions;
        }

        public List<ProtocolVersion> ListForProtocol(int protocolKey)
        {
            return _protocolVersions.Where(pv => pv.ProtocolKey == protocolKey).ToList();
        }

        public string Update(ProtocolVersion protocolVersion)
        {
            var protocolVersionToUpdate = _protocolVersions.FirstOrDefault(pv => pv.ProtocolVersionKey == protocolVersion.ProtocolVersionKey);

            protocolVersionToUpdate.ProtocolVersionKey = protocolVersion.ProtocolVersionKey;
            protocolVersionToUpdate.NamespaceKey = protocolVersion.NamespaceKey;
            protocolVersionToUpdate.ProtocolKey = protocolVersion.ProtocolKey;
            protocolVersionToUpdate.ProtocolVersionText = protocolVersion.ProtocolVersionText;
            protocolVersionToUpdate.TestVersion = protocolVersion.TestVersion;
            protocolVersionToUpdate.Title = protocolVersion.Title;
            protocolVersionToUpdate.SubTitle = protocolVersion.SubTitle;
            protocolVersionToUpdate.ReleaseDate = protocolVersion.ReleaseDate;
            protocolVersionToUpdate.WebPostingDate = protocolVersion.WebPostingDate;
            protocolVersionToUpdate.UserKey = protocolVersion.UserKey;
            protocolVersionToUpdate.LastUpdated = protocolVersion.LastUpdated;
            protocolVersionToUpdate.LastUpdatedDt = protocolVersion.LastUpdatedDt;
            protocolVersionToUpdate.Active = protocolVersion.Active;

            return string.Empty;
        }

        public List<ChecklistReviewers> GetReviewersbyProtocalVersion(int protocolKey)
        {
            return new List<ChecklistReviewers>();
        }

        public List<LablelistAuthors> GetAuthorsbyProtocalVersion(int protocolKey)
        {
            return new List<LablelistAuthors>();
        }
        public List<ProtocolVersion> GetAssignReviewerById(int protocolVersionsKey)
        {
            return new List<ProtocolVersion>();
        }

        public List<string> GetTemplateVersion(int protocolVersionsKey)
        {
            return new List<string>();
        }

        public List<ChecklistItem> GetCaseSummary(List<string> templateVersion)
        {
            return new List<ChecklistItem>();
        }
        public String InsertProtocolCaseSummarylistItem(Int32 FromProtocolVersionsKey, Int32 ToProtocolVersionsKey, Int32 FromParentItemid, Int32 TemplateVersionsKey)
        {
            return String.Empty;
        }
        public List<ChecklistVersion> GetChecklistVersion(String ProtocolKey)
        {
            return new List<ChecklistVersion>();
        }

        public List<ChecklistItem> GetItemChildren(String NodeId)
        {
            return new List<ChecklistItem>();
        }
        public List<ChecklistItem> GetAllNodes(String id)
        {
            return new List<ChecklistItem>();
        }

        public List<User> ValidUserID()
        {
            return new List<User>();
        }

    }
}


