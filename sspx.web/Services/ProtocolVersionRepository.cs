using sspx.core.entities;
using sspx.core.interfaces;
using sspx.infra.config;
using sspx.infra.data;
using sspx.web.Models;
using System;
using System.Collections.Generic;

namespace sspx.web.Services
{
    public class ProtocolVersionRepository : IProtocolVersionRepository
    {
        private ISSPxConfig _config;
        private IProtocolObject DBHelper = new SSPxEditorDBHelper();
        public ProtocolVersionRepository(ISSPxConfig config)
        {
            _config = config;
        }
        /// <summary>
        /// SSP-137 Validate Users
        /// </summary>
        /// <returns></returns>
        public List<User> ValidUserID()
        {
            return SSPxDBHelper.ValidUserID(_config.SSPxConnectionString);
        }

        public ProtocolVersion Add(ProtocolVersion protocolVersion)
        {
            return SSPxDBHelper.AddProtocolVersion(_config.SSPxConnectionString, protocolVersion);
        }

        public string Delete(ProtocolVersion protocolVersion)
        {
            // TODO CS2
            throw new NotImplementedException();
        }

        public ProtocolVersion GetByKey(int key)
        {
            // TODO CS2
            throw new NotImplementedException();
        }

        public ProtocolVersion GetLatestVersionForProtocol(int protocolKey)
        {
            return SSPxDBHelper.GetProtocolVersionLatestForProtocol(_config.SSPxConnectionString, protocolKey);
        }

        public ProtocolVersion GetLatestVersionForProtocolIncludingInactive(int protocolKey)
        {
            return SSPxDBHelper.GetProtocolVersionLatestForProtocolIncludingInactive(_config.SSPxConnectionString, protocolKey);
        }

        public List<ProtocolVersion> List()
        {
            // TODO CS2
            throw new NotImplementedException();
        }

        public List<ProtocolVersion> ListForProtocol(int protocolKey)
        {
            var versions = SSPxDBHelper.GetProtocolVersionsForProtocol(_config.SSPxConnectionString, protocolKey);

            return versions ?? new List<ProtocolVersion>();
        }

        public string Update(ProtocolVersion protocolVersion)
        {
            // TODO CS2
            //throw new NotImplementedException();
            return SSPxDBHelper.SaveProtocalVersion(_config.SSPxConnectionString,protocolVersion);
        }


        public List<ChecklistReviewers> GetReviewersbyProtocalVersion(int protocolVersionsKey)
        {
            return SSPxDBHelper.GetReviewersbyProtocalVersion(_config.SSPxConnectionString, protocolVersionsKey);
        }

        public List<LablelistAuthors> GetAuthorsbyProtocalVersion(int protocolVersionsKey)
        {
            return SSPxDBHelper.GetAuthorsbyProtocalVersion(_config.SSPxConnectionString, protocolVersionsKey);
        }
        public List<ProtocolVersion> GetAssignReviewerById(int protocolVersionsKey)
        {
            return SSPxDBHelper.GetAssignReviewerById(_config.SSPxConnectionString, protocolVersionsKey);
        }

        public List<ChecklistItem> GetCaseSummary(List<string> templateVersion)
        {
            string templateVersions = null;
            if (templateVersion != null)
            {
                for (int i = 0; i < templateVersion.Count; i++)
                {
                    templateVersions += templateVersion[i] + ",";
                }
                templateVersions = templateVersions.TrimEnd(templateVersions[templateVersions.Length - 1]);
                return DBHelper.GetItemChildren(_config.SSPxConnectionString, templateVersions);
            }
            return new List<ChecklistItem>();
        }

        public List<string> GetTemplateVersion(int protocolVersionsKey)
        {
            return SSPxDBHelper.GetTemplateVersion(_config.SSPxConnectionString, protocolVersionsKey); 
        }

        public String InsertProtocolCaseSummarylistItem(Int32 FromProtocolVersionsKey, Int32 ToProtocolVersionsKey, Int32 FromParentItemid, Int32 TemplateVersionsKey)
        {
            return DBHelper.InsertProtocolCaseSummarylistItem(_config.SSPxConnectionString, FromProtocolVersionsKey, ToProtocolVersionsKey, FromParentItemid, TemplateVersionsKey);
        }

        public List<ChecklistVersion> GetChecklistVersion(String ProtocolKey)
        {
            return DBHelper.GetChecklistVersion(_config.SSPxConnectionString, ProtocolKey);
        }

        public List<ChecklistItem> GetItemChildren(String NodeId)
        {
            return DBHelper.GetItemChildren(_config.SSPxConnectionString, NodeId);
        }

        //public List<ChecklistItem> GetItemChildren(String NodeId)
        //{
        //    return DBHelper.GetItemChildren(_config.SSPxConnectionString, NodeId);
        //}

        public List<ChecklistItem> GetAllNodes(String id)
        {
            return DBHelper.GetAllNodes(_config.SSPxConnectionString, id);
        }

    }
}
