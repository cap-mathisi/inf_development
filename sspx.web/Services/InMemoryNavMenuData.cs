using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sspx.core.entities;
using sspx.infra.data;
using sspx.web.Models;

namespace sspx.web.Services
{
    public class InMemoryNavMenuData : INavMenuData
    {
        private List<ProtocolNavMenuData> _protocolNavMenuDataList;

        // TODO CS2:
        private List<ProtocolVersionNavMenuData> _protocolVersionMenuDataList;

        public InMemoryNavMenuData()
        {
            _protocolNavMenuDataList = new List<ProtocolNavMenuData>
            {
                new ProtocolNavMenuData {
                    ProtocolGroupKey = 1,
                    ProtocolGroupName = "Breast",
                    ProtocolGroupSortName = "Breast",
                    ProtocolKey = 5,
                    ProtocolName = "Invasive Breast",
                    ProtocolSortName = "Invasive Breast",
                    UserRoles = new List<RoleTypes>{ RoleTypes.Author, RoleTypes.Reviewer }
                },
                new ProtocolNavMenuData {
                    ProtocolGroupKey = 8,
                    ProtocolGroupName = "Gastrointestinal",
                    ProtocolGroupSortName = "Gastrointestinal",
                    ProtocolKey = 7,
                    ProtocolName = "Colon Res",
                    ProtocolSortName = "Colon Res",
                    UserRoles = new List<RoleTypes>{ RoleTypes.Author }
                },
                new ProtocolNavMenuData {
                    ProtocolGroupKey = 11,
                    ProtocolGroupName = "Genitourinary",
                    ProtocolGroupSortName = "Genitourinary",
                    ProtocolKey = 52,
                    ProtocolName = "Urethra",
                    ProtocolSortName = "Urethra",
                    UserRoles = new List<RoleTypes>{ RoleTypes.Reviewer }
                },
                new ProtocolNavMenuData {
                    ProtocolGroupKey = 5,
                    ProtocolGroupName = "Head and Neck",
                    ProtocolGroupSortName = "Head and Neck",
                    ProtocolKey = 27,
                    ProtocolName = "Nasal Cavity and Paranasal Sinuses",
                    ProtocolSortName = "Nasal Cavity and Paranasal Sinuses",
                    UserRoles = new List<RoleTypes>()
                },
                new ProtocolNavMenuData {
                    ProtocolGroupKey = 5,
                    ProtocolGroupName = "Head and Neck",
                    ProtocolGroupSortName = "Head and Neck",
                    ProtocolKey = 36,
                    ProtocolName = "Pharynx",
                    ProtocolSortName = "Pharynx",
                    UserRoles = new List<RoleTypes>{ RoleTypes.Author }
                }
            };

            _protocolVersionMenuDataList = new List<ProtocolVersionNavMenuData>
            {
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 5,
                    ProtocolVersionKey = 205,
                    ProtocolVersion = "3.3.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-11-03 15:36:01.400")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 5,
                    ProtocolVersionKey = 89,
                    ProtocolVersion = "3.1.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 5,
                    ProtocolVersionKey = 154,
                    ProtocolVersion = "3.1.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 5,
                    ProtocolVersionKey = 24,
                    ProtocolVersion = "3.0.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 7,
                    ProtocolVersionKey = 207,
                    ProtocolVersion = "3.4.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-11-03 15:36:01.400")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 7,
                    ProtocolVersionKey = 74,
                    ProtocolVersion = "3.3.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 7,
                    ProtocolVersionKey = 139,
                    ProtocolVersion = "3.2.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 7,
                    ProtocolVersionKey = 9,
                    ProtocolVersion = "3.1.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 27,
                    ProtocolVersionKey = 227,
                    ProtocolVersion = "3.2.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-11-03 15:36:01.407")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 27,
                    ProtocolVersionKey = 97,
                    ProtocolVersion = "3.2.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 27,
                    ProtocolVersionKey = 162,
                    ProtocolVersion = "3.1.0.1",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 27,
                    ProtocolVersionKey = 32,
                    ProtocolVersion = "3.1.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 36,
                    ProtocolVersionKey = 107,
                    ProtocolVersion = "3.3.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 36,
                    ProtocolVersionKey = 236,
                    ProtocolVersion = "3.3.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-11-03 15:36:01.410")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 36,
                    ProtocolVersionKey = 172,
                    ProtocolVersion = "3.2.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 36,
                    ProtocolVersionKey = 42,
                    ProtocolVersion = "3.1.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 52,
                    ProtocolVersionKey = 252,
                    ProtocolVersion = "3.2.1.0",
                    LastUpdatedDate = DateTime.Parse("2016-11-03 15:36:01.413")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 52,
                    ProtocolVersionKey = 124,
                    ProtocolVersion = "3.2.1.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 52,
                    ProtocolVersionKey = 189,
                    ProtocolVersion = "3.2.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                },
                new ProtocolVersionNavMenuData{
                    ProtocolKey = 52,
                    ProtocolVersionKey = 59,
                    ProtocolVersion = "3.1.0.0",
                    LastUpdatedDate = DateTime.Parse("2016-10-18 12:25:46.490")
                }
            };
        }

        public Task<List<ProtocolGroupMenuItem>> GetProtocolGroupMenuItems()
        {
            var protocolGroupMenuDataList = _protocolNavMenuDataList
                .GroupBy(m => new { m.ProtocolGroupKey })
                .Select(m => m.FirstOrDefault())
                .OrderBy(m => m.ProtocolGroupSortName);

            return Task.FromResult(
                protocolGroupMenuDataList
                    .Select(m => new ProtocolGroupMenuItem(m.ProtocolGroupKey, m.ProtocolGroupName))
                    .ToList()
                );
        }

        public Task<List<ProtocolMenuItem>> GetProtocolMenuItems(int userKey)
        {
            // returning the same items for all users, since this is just fake data
            return Task.FromResult(
                _protocolNavMenuDataList
                    .OrderBy(m => m.ProtocolGroupSortName)
                    .ThenBy(m => m.ProtocolSortName)
                    .Select(m => ProtocolMenuItem.FromNavMenuData(m))
                    .ToList()
                );
        }

        public Task<List<ProtocolVersionMenuItem>> GetProtocolVersions(int protocolKey)
        {
            return Task.FromResult(
                _protocolVersionMenuDataList
                    .Where(m => m.ProtocolKey == protocolKey)
                    .OrderByDescending(m => m.ProtocolVersion)
                    .Select(m => ProtocolVersionMenuItem.FromProtocolVersionNavMenuData(m))
                    .ToList()
                );
        }
    }
}
