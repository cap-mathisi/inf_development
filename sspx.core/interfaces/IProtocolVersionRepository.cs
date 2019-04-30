using sspx.core.entities;
using System.Collections.Generic;

namespace sspx.core.interfaces
{
    public interface IProtocolVersionRepository
    {
        ProtocolVersion GetByKey(int key);
        List<ChecklistVersion> GetChecklistVersion(string id);
        ProtocolVersion GetLatestVersionForProtocol(int protocolKey);
        List<ChecklistItem> GetItemChildren(string nodeId);
        ProtocolVersion GetLatestVersionForProtocolIncludingInactive(int protocolKey);
        List<ProtocolVersion> List();
        List<ProtocolVersion> ListForProtocol(int protocolKey);
        ProtocolVersion Add(ProtocolVersion protocolVersion);
        string Update(ProtocolVersion protocolVersion);
        string Delete(ProtocolVersion protocolVersion);

        List<ChecklistReviewers> GetReviewersbyProtocalVersion(int protocolVersionsKey);

        List<LablelistAuthors> GetAuthorsbyProtocalVersion(int protocolVersionsKey);
        List<ProtocolVersion> GetAssignReviewerById(int protocolVersionsKey);

        List<string> GetTemplateVersion(int protocolVersionsKey);

        List<ChecklistItem> GetCaseSummary(List<string> templateVersion);

        string InsertProtocolCaseSummarylistItem(int FromProtocolVersionsKey, int ToProtocolVersionsKey, int FromParentItemid, int TemplateVersionsKey);

        List<ChecklistItem> GetAllNodes(string id);
        /// <summary>
        /// SSP-137 - Getting valid users
        /// </summary>
        /// <returns></returns>
        List<User> ValidUserID();
    }
}
