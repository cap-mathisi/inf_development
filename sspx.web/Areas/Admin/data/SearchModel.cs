using sspx.core.entities;
using System.Collections.Generic;

namespace sspx.Areas.Admin.data
{
    public class SearchModel
    {
        #region public properties

        public string ItemsName { get; set; } = string.Empty;
        public string SessionVarName { get; set; }
        public string PageUrl { get; set; } = string.Empty;
        public List<SearchItem> SearchItems { get; set; }

        #endregion

        public SearchModel(string itemsName, string pageUrl)
        {
            ItemsName = itemsName;
            SessionVarName = $"{itemsName.Replace(" ", string.Empty)}SearchTerm";
            PageUrl = pageUrl;
            SearchItems = new List<SearchItem>();
        }

        #region static methods

        public static SearchModel FromProtocolsWithGroups(string pageUrl, List<ProtocolWithGroup> protocolsWithGroups)
        {
            SearchModel searchModel = new SearchModel("Protocols", pageUrl);
            foreach (ProtocolWithGroup protocolWithGroup in protocolsWithGroups)
            {
                searchModel.SearchItems.Add(new SearchItem
                {
                    Id = protocolWithGroup.ProtocolKey,
                    Title = protocolWithGroup.ProtocolName,
                    SubTitle = protocolWithGroup.ProtocolGroupName,
                    Active = protocolWithGroup.ProtocolActive
                });
            }

            return searchModel;
        }

        public static SearchModel FromProtocolGroups(string pageUrl, List<ProtocolGroup> protocolGroups)
        {
            SearchModel searchModel = new SearchModel("Protocol Groups", pageUrl);
            foreach (ProtocolGroup protocolGroup in protocolGroups)
            {
                searchModel.SearchItems.Add(new SearchItem
                {
                    Id = protocolGroup.ProtocolGroupKey,
                    Title = protocolGroup.ProtocolGroupName,
                    SubTitle = protocolGroup.ProtocolGroupName, // because there's not another field to use for subtitle
                    Active = protocolGroup.Active
                });
            }

            return searchModel;
        }

        public static SearchModel FromQualifications(string pageUrl, List<Qualification> qualifications)
        {
            SearchModel searchModel = new SearchModel("Qualifications", pageUrl);
            foreach (Qualification qualification in qualifications)
            {
                searchModel.SearchItems.Add(new SearchItem
                {
                    Id = qualification.QualificationKey,
                    Title = qualification.QualificationTxt,
                    SubTitle = qualification.Description,
                    Active = qualification.Active
                });
            }

            return searchModel;
        }

        public static SearchModel FromStandards(string pageUrl, List<Standard> standards)
        {
            SearchModel searchModel = new SearchModel("Standards", pageUrl);
            foreach (Standard standard in standards)
            {
                searchModel.SearchItems.Add(new SearchItem
                {
                    Id = (int)standard.BasedOnCkey, // TODO CS2:
                    Title = standard.BasedOn,
                    SubTitle = standard.Description,
                    Active = standard.Active
                });
            }

            return searchModel;
        }

        #endregion
    }

    public class SearchItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public bool Active { get; set; }
    }
}
