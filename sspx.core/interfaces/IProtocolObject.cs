using System;
using System.Collections.Generic;
using System.Text;
using sspx.core.entities;

namespace sspx.core.interfaces
{
    public interface IProtocolObject
    {
        /// <summary>
        /// SSp-136 - Get comments for a protocol
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        List<ItemComment> GetProtocolVersionCommentsForAll(String connection, String id);
        List<ChecklistItem> GetItemChildren(String connection, String id);

        ChecklistItem GetChecklistItem(String connection, String itemId);

        String UpdateChecklistItem(String connection, ChecklistItem item);

        String UpdateChecklistItemNotes(String connection, String itemId, String noteKeys);

        List<ExplanatoryNote> GetNoteKeys(String connection, String itemId);

        List<ExplanatoryNote> GetNotes(String connection, String id, String noteId = "");

        List<ExplanatoryNote> GetNoteTitles(String connection, String id);

        List<ItemComment> GetNoteComments(String connection, String id, String noteId);

        List<ItemComment> GetProtocolVersionComments(String connection, String id, String itemKey);

        List<ItemReference> GetNoteReferences(String connection, String id, String noteId = "");

        List<ChecklistVersion> GetChecklistVersion(String connection, String id);
        /// <summary>
        /// Added the below lines to Show Title, Cover and Author comments count in badge icon
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        List<ChecklistVersionCommentsSummary> GetChecklistVersionComments(String connection, String id);

        List<ChecklistItemNode> GetChecklistVersionItems(String connection, String id);

        String SaveNotes(String connection, String noteCKey, String note, String noteTitle, String protocolCkey, String refOrder, String personCkey = "350");

        String InsertChecklistItem(String connection, String itemId, Int32 insertType, Int32 itemTypeKey, Int32 itemCount);

        String SwitchChecklistItem(String connection, String itemId, String withItemId);

        String DeleteChecklistItem(String connection, String itemId);

        String CopyItemNode(String connection, String nodeIdToCopy, String nodeIdToPaste, Boolean isCut, Boolean itemOnly, Boolean asChild);

        List<VersionComments> GetAllVersionsComment(String connection, int ProtocolKey, int WorkingProtocolVersionKey);

        String SaveNoteReference(String connection, String referenceCKey, String noteCkey, String reference, String personCkey = "350");

        String SaveNoteComment(String connection, String commentKey, String noteCkey, String comment, String personCkey = "350");

        String RemoveNoteComment(String connection, String commentKey, String personCkey = "350");

        String RemoveNoteReference(String connection, String referenceCKey, String personCkey = "350");

        String SaveProtocolComment(String connection, Int32 commentId, String comment, String itemKey, String protocolVersionKey, String personCkey = "350");
        String ImportCommentToCurrentVersion(String connection, Int32 protocolVersionKey, String userID, Int32 ToProtocolVersionKey);
        String DeleteProtocolComment(String connection, Int32 commentId, String itemKey);

        String CopyNote(String connection, String nid, String personCkey = "350");

        ChecklistVersion GetFullChecklistVersion(String connection, String id, Int32 idType = 0);

        List<ExplanatoryNote> GetNotesForVersion(String connection, String id);

        List<ItemReference> GetNoteReferencesForVersion(String connection, String id);

        List<UserSimple> GetAllUsers(String connection);

        List<KeyValuePair<String, String>> GetStandards(String connection);

        String SaveProtocolVersion(String connection, String name, String description, String basedOnKey, String versionCkey, String personCkey = "350");

        String SaveCover(String connection, String detail, String versionCkey, String personCkey = "350");

        String SaveAuthors(String connection, String versionCkey, String role, String userIds, String personCkey = "350");

        String SaveItemHiddenFlag(String connection, String id, String hideItemIds, String showItemIds);

        String AddNote(String connection, String nid, String personCkey = "350");

        String MoveNote(String connection, String id, String nid, String personCkey = "350");

        String DeleteNote(String connection, String nid, String personCkey = "350");



        Int32 GetProtocolVersion(String connection, String id);

        Int32 GetPreviousProtocolVersion(String connection, String id);

        String InsertProtocolCaseSummarylistItem(String connection, Int32 FromProtocolVersionsKey, Int32 ToProtocolVersionsKey, Int32 FromParentItemid, Int32 TemplateVersionsKey);


        //JIRA Id SSP-98
        List<ChecklistItem> GetAllNodes(String connection, String id);
        //JIRA Id SSP-98

        List<ChecklistVersion> GetCheckVersion(String connection, String id);
    }
}
