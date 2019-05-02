using System;
using System.Collections.Generic;
using System.Text;
using sspx.core.entities;
using sspx.core.interfaces;

namespace sspx.infra.data
{
    public class SSPxEditorDBHelper : IProtocolObject
    {
        #region public methods

        public List<ChecklistItem> GetItemChildren(String connection, String id)
        {
            Console.WriteLine("getting item children for {0}", id);
            if (id == "0")
            {
                id = "0_T";
            }
            var ids = id.Split('_');
            if (ids.Length == 2)
            {
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "ItemId", Value = ids[0] });
                param.Add(new DBHelper.Parameter { Name = "Type", Value = ids[1] });
                var items = DBHelper.ExecuteQueryObjectWithMapper(connection, _getItemChildrenById, SSPxEditorObjects.CaseSummaryItemMapper, param);
                var notes = DBHelper.ExecuteQueryObjectWithMapper(connection, _getNoteKeyForItems, SSPxEditorObjects.CaseSummaryNoteMapper, param);
                foreach (var note in notes)
                {
                    int index = items.FindIndex(a => a.key == note.title);
                    var item = items.Find(i => i.key == note.title);
                    if (item != null)
                    {
                        items[index].notes.Add(note);
                    }
                }
                return items;
            }
            return new List<ChecklistItem>();
        }

        //Zira Id SSP-98
        public List<ChecklistItem> GetAllNodes(String connection, String id)
        {
            Console.WriteLine("getting item children for {0}", id);
            if (id == "0")
            {
                id = "0_T";
            }
            var ids = id.Split('_');
            if (ids.Length == 2)
            {
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "ItemId", Value = ids[0] });
                param.Add(new DBHelper.Parameter { Name = "Type", Value = ids[1] });
                var items = DBHelper.ExecuteQueryObjectWithMapper(connection, _getAllItemChildrenById, SSPxEditorObjects.CaseSummaryItemMapper, param);
                var notes = DBHelper.ExecuteQueryObjectWithMapper(connection, _getNoteKeyForItems, SSPxEditorObjects.CaseSummaryNoteMapper, param);
                foreach (var note in notes)
                {
                    int index = items.FindIndex(a => a.key == note.title);
                    var item = items.Find(i => i.key == note.title);
                    if (item != null)
                    {
                        items[index].notes.Add(note);
                    }
                }
                return items;
            }
            return new List<ChecklistItem>();
        }
        //Zira Id SSP-98
        public ChecklistItem GetChecklistItem(String connection, String itemId)
        {
            Console.WriteLine("getting checklist item {0}", itemId);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "ItemId", Value = itemId.Replace("_I", String.Empty) });
            var items = DBHelper.ExecuteQueryObjectWithMapper(connection, _getTemplateItem, SSPxEditorObjects.FullItemMapper, param);
            if (items.Count > 0)
            {
                return items[0];
            }
            return new ChecklistItem();
        }

        public String UpdateChecklistItem(String connection, ChecklistItem item)
        {
            try
            {
                Console.WriteLine("saving item for {0}", item.key);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "ItemId", Value = item.key });
                param.Add(new DBHelper.Parameter { Name = "VisibleText", Value = item.longText });
                param.Add(new DBHelper.Parameter { Name = "ItemTypeId", Value = item.itemType });
                param.Add(new DBHelper.Parameter { Name = "Required", Value = item.required });
                param.Add(new DBHelper.Parameter { Name = "Condition", Value = item.condition });
                param.Add(new DBHelper.Parameter { Name = "Comments", Value = item.comments });
                param.Add(new DBHelper.Parameter { Name = "DataTypeId", Value = item.answerDataTypeKey });
                param.Add(new DBHelper.Parameter { Name = "UnitId", Value = item.answerUnits });
                param.Add(new DBHelper.Parameter { Name = "ReportText", Value = item.reportText });
                param.Add(new DBHelper.Parameter { Name = "MaxValue", Value = item.answerMaxValue });
                param.Add(new DBHelper.Parameter { Name = "MinValue", Value = item.answerMinValue });
                param.Add(new DBHelper.Parameter { Name = "MaxReps", Value = item.answerMaxReps });
                param.Add(new DBHelper.Parameter { Name = "MinReps", Value = item.answerMinReps });
                DBHelper.ExecuteNonQuery(connection, _updateTemplateItem, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String UpdateChecklistItemNotes(String connection, String itemId, String noteKeys)
        {
            try
            {
                Console.WriteLine("saving item notes for {0}", itemId);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "ItemId", Value = itemId });
                param.Add(new DBHelper.Parameter { Name = "NoteKeys", Value = noteKeys });
                DBHelper.ExecuteNonQuery(connection, _saveItemNotes, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public List<ExplanatoryNote> GetNoteKeys(String connection, String itemId)
        {
            Console.WriteLine("getting notes for checklist item {0}", itemId);
            if (itemId == "0")
            {
                itemId = "0_T";
            }
            var ids = itemId.Split('_');
            if (ids.Length == 2)
            {
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "ItemId", Value = ids[0] });
                param.Add(new DBHelper.Parameter { Name = "Type", Value = ids[1] });
                var notes = DBHelper.ExecuteQueryObjectWithMapper(connection, _getNoteKeys, SSPxEditorObjects.CaseSummaryNoteMapper, param);
                return notes;
            }
            return new List<ExplanatoryNote>();
        }

        public List<ExplanatoryNote> GetNotes(String connection, String id, String noteId = "")
        {
            Console.WriteLine("getting notes for checklist version {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "Protocolskey", Value = id.Replace("_C", String.Empty) });
            param.Add(new DBHelper.Parameter { Name = "NoteId", Value = noteId });
            var notes = DBHelper.ExecuteQueryObjectWithMapper(connection, _getNotes, SSPxEditorObjects.NoteMapper, param);
            return notes;
        }

        public List<ExplanatoryNote> GetNoteTitles(String connection, String id)
        {
            Console.WriteLine("getting note titles for checklist version {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "Protocolskey", Value = id.Replace("_C", String.Empty) });
            var notes = DBHelper.ExecuteQueryObjectWithMapper(connection, _getNoteTitles, SSPxEditorObjects.NoteMapper, param);
            return notes;
        }

        public List<ExplanatoryNote> GetNotesForVersion(String connection, String id)
        {
            Console.WriteLine("getting notes for checklist version {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "VersionCkey", Value = id.Replace("_C", String.Empty) });
            var notes = DBHelper.ExecuteQueryObjectWithMapper(connection, _getNotesForVersion, SSPxEditorObjects.NoteMapper, param);
            return notes;
        }

        public List<ItemComment> GetNoteComments(String connection, String id, String noteId)
        {
            Console.WriteLine("getting note comments for checklist version {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "ProtocolCkey", Value = id.Replace("_C", String.Empty) });
            param.Add(new DBHelper.Parameter { Name = "NoteId", Value = noteId });
            var comments = DBHelper.ExecuteQueryObjectWithMapper(connection, _getNoteComments, SSPxEditorObjects.CommentMapper, param);
            return comments;
        }
        /// <summary>
        /// Added the below Method to Show Title, Cover and Author comments count in badge icon
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public List<ItemComment> GetProtocolVersionComments(String connection, String id, String itemKey)
        {
            Console.WriteLine("getting protocol version item comments {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "ProtocolVersionKey", Value = id });
            param.Add(new DBHelper.Parameter { Name = "ItemKey", Value = itemKey.Replace("_I", String.Empty) });
            var comments = DBHelper.ExecuteQueryObjectWithMapper(connection, _getProtocolVersionComments, SSPxEditorObjects.CommentMapper, param);
            return comments;
        }
        /// <summary>
        /// SSP 136 - Get total number of Comments count for a Protocol
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ItemComment> GetProtocolVersionCommentsForAll(String connection, String id)
        {
            Console.WriteLine("getting protocol version item comments {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "ProtocolVersionKey", Value = id });
            //      param.Add(new DBHelper.Parameter { Name = "ItemKey", Value = itemKey.Replace("_I", String.Empty) });
            var comments = DBHelper.ExecuteQueryObjectWithMapper(connection, _getProtocolVersionCommentsForAll, SSPxEditorObjects.CommentMapperForAll, param);

            return comments;
        }

        public Int32 GetProtocolVersion(String connection, String id)
        {
            Console.WriteLine("getting protocol comments {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "ProtocolsKey", Value = id.Replace("_C", String.Empty) });
            var key = DBHelper.ExecuteQueryObjectWithMapper(connection, _getProtocolVersion, SSPxEditorObjects.KeyValueMapper, param);
            if (key.Count > 0)
            {
                return Convert.ToInt32(key[0].Value);
            }
            return 0;
        }

        public Int32 GetPreviousProtocolVersion(String connection, String id)
        {
            Console.WriteLine("getting protocol comments {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "ProtocolsKey", Value = id.Replace("_C", String.Empty) });
            var key = DBHelper.ExecuteQueryObjectWithMapper1(connection, "SSPx_GetPreviousProtocolversion", SSPxEditorObjects.KeyValueMapper, param);
            if (key.Count > 0)
            {
                return Convert.ToInt32(key[0].Value);
            }
            return 0;
        }

        public List<ItemReference> GetNoteReferences(String connection, String id, String noteId)
        {
            Console.WriteLine("getting note references for checklist version {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "ProtocolCkey", Value = id.Replace("_C", String.Empty) });
            param.Add(new DBHelper.Parameter { Name = "NoteId", Value = noteId });
            var references = DBHelper.ExecuteQueryObjectWithMapper(connection, _getNoteReferences, SSPxEditorObjects.ReferenceMapper, param);
            return references;
        }

        public List<ItemReference> GetNoteReferencesForVersion(String connection, String id)
        {
            Console.WriteLine("getting note references for checklist version {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "VersionCkey", Value = id.Replace("_C", String.Empty) });
            var references = DBHelper.ExecuteQueryObjectWithMapper(connection, _getNoteReferencesForVersion, SSPxEditorObjects.ReferenceMapper, param);
            return references;
        }

        public List<ChecklistVersion> GetChecklistVersion(String connection, String id)
        {
            Console.WriteLine("getting template version for protocol {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "Protocolskey", Value = id.Replace("_C", String.Empty) });
            var versions = DBHelper.ExecuteQueryObjectWithMapper(connection, _getTemplateVersion, SSPxEditorObjects.VersionMapper, param);
            return versions;
        }


        public List<ChecklistVersion> GetCheckVersion(String connection, String id)
        {
            Console.WriteLine("getting template version for protocol {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "TemplateVersionsKey", Value = id.Replace("_C", String.Empty) });
            var versions = DBHelper.ExecuteQueryObjectWithMapper(connection, _getTemplateVersionName, SSPxEditorObjects.VersionMapper, param);
            return versions;
        }
        /// <summary>
        /// Comments for Author, Title and Cover
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ChecklistVersionCommentsSummary> GetChecklistVersionComments(String connection, String id)
        {
            Console.WriteLine("getting Comments for protocol editor screen {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "ProtocolsKey", Value = id.Replace("_C", String.Empty) });
            var versions = DBHelper.ExecuteQueryObjectWithMapper(connection, _getChecklistVersionComments, SSPxEditorObjects.ChecklistVersionCommentsSummaryMapper, param);
            return versions;
        }

        public ChecklistVersion GetFullChecklistVersion(String connection, String id, Int32 idType)
        {
            Console.WriteLine("getting template version for {0}", id);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "VersionCkey", Value = (idType == 1) ? id.Replace("_C", String.Empty) : String.Empty });
            param.Add(new DBHelper.Parameter { Name = "ProtocolCkey", Value = (idType == 1) ? String.Empty : id.Replace("_C", String.Empty) });
            var versions = DBHelper.ExecuteQueryObjectWithMapper(connection, _getFullTemplateVersion, SSPxEditorObjects.VersionMapper, param);
            if (versions.Count > 0)
            {
                var version = versions[0];
                param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "VersionCkey", Value = version.key });
                version.users = DBHelper.ExecuteQueryObjectWithMapper(connection, _getChecklistAuthors, SSPxEditorObjects.UserMapper, param);
                var external = DBHelper.ExecuteQueryObjectWithMapper(connection, _getChecklistExternalVersions, SSPxEditorObjects.KeyValueMapper, param);
                foreach (var ver in external)
                {
                    version.basedOnKey.Add(Convert.ToInt32(ver.Value));
                }
                return version;
            }
            return new ChecklistVersion();
        }

        public List<UserSimple> GetAllUsers(String connection)
        {
            Console.WriteLine("getting all users");
            var users = DBHelper.ExecuteQueryObjectWithMapper(connection, _getAllUsers, SSPxEditorObjects.UserMapper);
            var simpleUsers = new List<UserSimple>();
            foreach (var user in users)
            {
                simpleUsers.Add(user.ToUserSimple());
            }
            return simpleUsers;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public List<KeyValuePair<String, String>> GetStandards(String connection)
        {
            Console.WriteLine("getting all standards");
            var standards = DBHelper.ExecuteQueryObjectWithMapper(connection, _getAllBasedOn, SSPxEditorObjects.KeyValueMapper);
            return standards;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ChecklistItemNode> GetChecklistVersionItems(String connection, String id)
        {
            Console.WriteLine("getting all items for checklist template item {0}", id);
            var item = id.Split('_');
            var itemType = "I";
            if (item.Length > 1)
            {
                itemType = item[1].TrimStart('_');
            }
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "ItemId", Value = item[0] });
            param.Add(new DBHelper.Parameter { Name = "ItemType", Value = itemType });
            var items = DBHelper.ExecuteQueryObjectWithMapper(connection, _getTemplateVersionItems, SSPxEditorObjects.ChecklistItemNodeMapper, param);
            return items;
        }



        public List<VersionComments> GetAllVersionsComment(String connection, int ProtocolKey, int WorkingProtocolVersionKey)
        {
            Console.WriteLine("getting all the version and comment on the Protocol {0}", ProtocolKey);
            List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
            param.Add(new DBHelper.Parameter { Name = "ProtocolKey", Value = ProtocolKey });
            param.Add(new DBHelper.Parameter { Name = "WorkingProtocolVersionKey", Value = WorkingProtocolVersionKey });
            var versionComments = DBHelper.ExecuteStoredProcedureObjectWithMapper(connection, "SSPX_GetAllVersionComments", SSPxEditorObjects.VersionCommentMapper, param);
            return versionComments;
        }

        public String ImportCommentToCurrentVersion(String connection, Int32 protocolVersionKey, String userID, Int32 ToProtocolVersionKey = 300)
        {
            try
            {
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "FromProtocolVersionKey", Value = protocolVersionKey });
                param.Add(new DBHelper.Parameter { Name = "ReviwerList", Value = userID });
                param.Add(new DBHelper.Parameter { Name = "ToProtocolVersionKey", Value = ToProtocolVersionKey });


                DBHelper.ExecuteNonQueryStoredProcedure(connection, "SSPX_ImportCommentToCurrentVersion", param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String SaveNotes(String connection, String noteCKey, String note, String noteTitle, String protocolCkey, String refOrder, String personCkey = "350.100004300")
        {
            try
            {
                Console.WriteLine("saving content for {0}", noteCKey);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "ProtocolCkey", Value = protocolCkey });
                param.Add(new DBHelper.Parameter { Name = "NoteCKey", Value = noteCKey });
                param.Add(new DBHelper.Parameter { Name = "NoteTitle", Value = noteTitle });
                param.Add(new DBHelper.Parameter { Name = "Note", Value = note ?? String.Empty });
                param.Add(new DBHelper.Parameter { Name = "RefOrder", Value = refOrder ?? String.Empty });
                param.Add(new DBHelper.Parameter { Name = "PersonCkey", Value = personCkey });
                DBHelper.ExecuteNonQuery(connection, _saveNote, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String SaveProtocolVersion(String connection, String name, String description, String basedOnKey, String versionCkey, String personCkey = "350.100004300")
        {
            try
            {
                Console.WriteLine("saving cover for version {0}", versionCkey);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "VersionCkey", Value = versionCkey });
                param.Add(new DBHelper.Parameter { Name = "ProtocolName", Value = name });
                param.Add(new DBHelper.Parameter { Name = "Description", Value = description });
                param.Add(new DBHelper.Parameter { Name = "BasedOnKey", Value = basedOnKey });
                param.Add(new DBHelper.Parameter { Name = "PersonCkey", Value = personCkey });
                DBHelper.ExecuteNonQuery(connection, _saveProtocolVersion, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String SaveCover(String connection, String detail, String versionCkey, String personCkey = "350.100004300")
        {
            try
            {
                Console.WriteLine("saving cover for version {0}", versionCkey);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "VersionCkey", Value = versionCkey });
                param.Add(new DBHelper.Parameter { Name = "Detail", Value = detail });
                param.Add(new DBHelper.Parameter { Name = "PersonCkey", Value = personCkey });
                DBHelper.ExecuteNonQuery(connection, _saveCover, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String SaveProtocolComment(String connection, Int32 commentId, String comment, String itemKey, String protocolVersionKey, String personCkey = "350")
        {
            try
            {
                Console.WriteLine("saving comment for protocol version item {0}", commentId);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "CommentId", Value = commentId });
                param.Add(new DBHelper.Parameter { Name = "ProtocolVersionKey", Value = protocolVersionKey });
                param.Add(new DBHelper.Parameter { Name = "ItemKey", Value = itemKey.Replace("_I", String.Empty) });
                param.Add(new DBHelper.Parameter { Name = "Comment", Value = comment ?? String.Empty });
                param.Add(new DBHelper.Parameter { Name = "Personkey", Value = personCkey });
                DBHelper.ExecuteNonQuery(connection, _saveProtocolVersionComment, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String DeleteProtocolComment(String connection, Int32 commentId, String itemKey)
        {
            try
            {
                Console.WriteLine("delete comment from protocol {0}", commentId);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                var isItem = itemKey.EndsWith("_I") ? 1 : 0;
                param.Add(new DBHelper.Parameter { Name = "CommentId", Value = commentId });
                param.Add(new DBHelper.Parameter { Name = "IsItem", Value = isItem });
                DBHelper.ExecuteNonQuery(connection, _deleteProtocolComment, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String InsertChecklistItem(String connection, String itemId, Int32 insertType, Int32 itemTypeKey, Int32 itemCount)
        {
            try
            {
                Console.WriteLine("inserting template item for {0}", itemId);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "ItemId", Value = itemId.Replace("_I", String.Empty) });
                param.Add(new DBHelper.Parameter { Name = "InsertType", Value = insertType });
                param.Add(new DBHelper.Parameter { Name = "ItemTypesKey", Value = itemTypeKey });
                param.Add(new DBHelper.Parameter { Name = "itemCount", Value = itemCount });
                DBHelper.ExecuteNonQuery(connection, _insertTemplateItem, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String InsertProtocolCaseSummarylistItem(String connection, Int32 FromProtocolVersionsKey, Int32 ToProtocolVersionsKey, Int32 FromParentItemid, Int32 TemplateVersionsKey)
        {
            try
            {
                Console.WriteLine("inserting template item for {0}", FromProtocolVersionsKey);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "FromProtocolVersionsKey", Value = FromProtocolVersionsKey });
                param.Add(new DBHelper.Parameter { Name = "ToProtocolVersionsKey", Value = ToProtocolVersionsKey });
                param.Add(new DBHelper.Parameter { Name = "FromParentItemid", Value = FromParentItemid });
                param.Add(new DBHelper.Parameter { Name = "TemplateVersionsKey", Value = TemplateVersionsKey });
                DBHelper.ExecuteNonQuery(connection, "SSPx_InsertProtocolCaseSummary", param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String SwitchChecklistItem(String connection, String itemId, String withItemId)
        {
            try
            {
                Console.WriteLine("switching template items for {0} and {1}", itemId, withItemId);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "ItemId", Value = itemId.Replace("_I", String.Empty) });
                param.Add(new DBHelper.Parameter { Name = "ItemId2", Value = withItemId.Replace("_I", String.Empty) });
                DBHelper.ExecuteNonQuery(connection, _switchTemplateItems, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String DeleteChecklistItem(String connection, String itemId)
        {
            try
            {
                Console.WriteLine("deleting template item {0}", itemId);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "ItemId", Value = itemId.Replace("_I", String.Empty) });
                DBHelper.ExecuteNonQuery(connection, _deleteTemplateItem, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String CopyItemNode(String connection, String nodeIdToCopy, String nodeIdToPaste, Boolean isCut = false, Boolean itemOnly = false, Boolean asChild = false)
        {
            try
            {
                Int32 isMove = isCut ? 1 : 0;
                Int32 isItemOnly = itemOnly ? 1 : 0;
                Int32 pasteAsChild = asChild ? 1 : 0;
                Console.WriteLine("copying node and children for {0} to after {1}", nodeIdToCopy, nodeIdToPaste);
                String sql = $"EXEC dbo.SSPx_CopyItemNode '{nodeIdToCopy.Replace("_I", String.Empty)}', '{nodeIdToPaste.Replace("_I", String.Empty)}', {isMove}, {isItemOnly}, {pasteAsChild}";
                DBHelper.ExecuteNonQuery(connection, sql);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String SaveNoteReference(String connection, String referencesKey, String noteskey,
            String reference, String userskey = "350")
        {
            try
            {
                Console.WriteLine("saving referece for {0}", referencesKey);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "ReferencesKey", Value = referencesKey });
                param.Add(new DBHelper.Parameter { Name = "Noteskey", Value = noteskey });
                param.Add(new DBHelper.Parameter { Name = "Reference", Value = reference });
                param.Add(new DBHelper.Parameter { Name = "Userskey", Value = userskey });
                DBHelper.ExecuteNonQuery(connection, _saveNoteReference, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String SaveNoteComment(String connection, String commentKey, String noteskey, String comment, String userskey = "350")
        {
            try
            {
                Console.WriteLine("adding new comment for note {0}", noteskey);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "CommentKey", Value = commentKey });
                param.Add(new DBHelper.Parameter { Name = "Noteskey", Value = noteskey });
                param.Add(new DBHelper.Parameter { Name = "Comment", Value = comment });
                param.Add(new DBHelper.Parameter { Name = "Userskey", Value = userskey });
                DBHelper.ExecuteNonQuery(connection, _saveNoteComment, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String RemoveNoteComment(String connection, String commentKey, String userskey = "350")
        {
            try
            {
                Console.WriteLine("remove comment for key {0}", commentKey);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "CommentKey", Value = commentKey });
                param.Add(new DBHelper.Parameter { Name = "Userskey", Value = userskey });
                DBHelper.ExecuteNonQuery(connection, _deleteNoteComment, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String RemoveNoteReference(String connection, String referencesKey, String userskey = "350")
        {
            try
            {
                Console.WriteLine("remove note reference for {0}", referencesKey);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "ReferencesKey", Value = referencesKey });
                param.Add(new DBHelper.Parameter { Name = "Userskey", Value = userskey });
                DBHelper.ExecuteNonQuery(connection, _removeNoteReference, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String SaveAuthors(String connection, String versionCkey, String role, String userIds, String personCkey = "350")
        {
            try
            {
                Console.WriteLine("saving authors for version {0}", versionCkey);
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "VersionCkey", Value = versionCkey });
                param.Add(new DBHelper.Parameter { Name = "Role", Value = role });
                param.Add(new DBHelper.Parameter { Name = "UserIds", Value = userIds });
                param.Add(new DBHelper.Parameter { Name = "PersonCkey", Value = personCkey });
                if (String.IsNullOrWhiteSpace(userIds))
                {
                    DBHelper.ExecuteNonQuery(connection, _deleteAllAuthors, param);
                }
                else
                {
                    DBHelper.ExecuteNonQuery(connection, _saveAuthors, param);
                }
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String SaveItemHiddenFlag(String connection, String versionCkey, String hideItemIds, String showItemIds)
        {
            try
            {
                Console.WriteLine("saving item hidden flags");
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "HideItemId", Value = (hideItemIds ?? String.Empty).Replace("_I", String.Empty) });
                param.Add(new DBHelper.Parameter { Name = "ShowItemId", Value = (showItemIds ?? String.Empty).Replace("_I", String.Empty) });
                DBHelper.ExecuteNonQuery(connection, _saveItemVisibleFlag, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String AddNote(String connection, String nid, String userid)
        {
            try
            {
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "NoteKey", Value = nid });
                param.Add(new DBHelper.Parameter { Name = "UsersKey", Value = userid });
                DBHelper.ExecuteNonQuery(connection, _addNote, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String MoveNote(String connection, String id, String nid, String userid)
        {
            try
            {
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "NoteKey1", Value = id });
                param.Add(new DBHelper.Parameter { Name = "NoteKey2", Value = nid });
                param.Add(new DBHelper.Parameter { Name = "UsersKey", Value = userid });
                DBHelper.ExecuteNonQuery(connection, _moveNote, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String DeleteNote(String connection, String nid, String userid)
        {
            try
            {
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "NoteKey", Value = nid });
                param.Add(new DBHelper.Parameter { Name = "UsersKey", Value = userid });
                DBHelper.ExecuteNonQuery(connection, _removeNote, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public String CopyNote(String connection, String nid, String userid)
        {
            try
            {
                List<DBHelper.Parameter> param = new List<DBHelper.Parameter>();
                param.Add(new DBHelper.Parameter { Name = "NoteKey", Value = nid });
                param.Add(new DBHelper.Parameter { Name = "UsersKey", Value = userid });
                DBHelper.ExecuteNonQuery(connection, _copyNote, param);
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        #endregion

        #region SQL Statements

        private static string _saveItemVisibleFlag = @"
            UPDATE i SET Visible = 0
            FROM dbo.SSPX_TemplateVersionItems i (NOLOCK)
            INNER JOIN dbo.StringSplit(@HideItemId, ',', 500) h ON i.TemplateVersionItemsKey = h.variablestring

            UPDATE i SET Visible = 1
            FROM dbo.SSPX_TemplateVersionItems i (NOLOCK)
            INNER JOIN dbo.StringSplit(@ShowItemId, ',', 500) s ON i.TemplateVersionItemsKey = s.variablestring
        ";

        private static string _updateTemplateItem = @"
            UPDATE dbo.SSPX_TemplateVersionItems SET VisibleText = @VisibleText, ItemTypesKey = @ItemTypeId, MustImplement = @Required,
	            Condition = @Condition, EditorComment = @Comments, ReportText = @ReportText, 
	            DataTypeKey = NULLIF(@DataTypeId, 0), AnswerUnitsKey = NULLIF(@UnitId, 0), AnswerMaxValue = NULLIF(@MaxValue, -1),
	            AnswerMinValue = NULLIF(@MinValue, -1), MaxRepetitions = NULLIF(@MaxReps, -1), MinRepetitions = NULLIF(@MinReps, -1)
            WHERE TemplateVersionItemsKey = @ItemId
        ";

        private static string _getTemplateItem = @"
            SELECT CONVERT(VARCHAR, i.TemplateVersionItemsKey) AS ItemKey, LEFT(COALESCE(NULLIF(i.VisibleText, ''), i.LongText), 150) AS ItemText, 
                COALESCE(NULLIF(i.VisibleText, ''), i.LongText) AS ItemLongText, 
	            CASE WHEN EXISTS (SELECT * FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE ParentTemplateVersionItemsKey = @ItemId) THEN 1 ELSE 0 END AS HasItems, 
	            0 AS CommentCount, COALESCE(i.MustImplement, CONVERT(BIT, 0)) AS Required,
	            COALESCE(i.Condition, '') AS Condition, i.ItemTypesKey, COALESCE(i.EditorComment, '') AS Comments, 
	            CONVERT(VARCHAR, i.TemplateVersionsKey) AS VersionKey,
	            i.ReportText, i.DataTypeKey, i.MaxRepetitions AS AnswerMaxReps, i.AnswerMaxValue, i.AnswerMinValue, i.AnswerUnitsKey, 
	            i.MinRepetitions AS AnswerMinReps, CASE WHEN i.visible = 1 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END AS Hidden
            FROM dbo.SSPX_TemplateVersionItems i (NOLOCK)
            WHERE i.TemplateVersionItemsKey = @ItemId
        ";

        private static string _saveNoteComment = @"
            If @CommentKey > 0
                UPDATE dbo.SSPX_NoteComments SET Comment = @Comment, UsersKey = @Userskey, LastUpdated = GETDATE()
                WHERE NoteCommentsKey = @CommentKey
            ELSE
                INSERT INTO dbo.SSPX_NoteComments (ProtocolNotesKey, Comment, UsersKey, DateCreated, Active) 
                VALUES (@Noteskey, @Comment, @Userskey, GETDATE(), 1)
        ";

        private static string _deleteNoteComment = @"
            UPDATE dbo.SSPX_NoteComments SET Active = 0, UsersKey = @Userskey, LastUpdated = GETDATE()
            WHERE NoteCommentsKey = @CommentKey
        ";

        private static string _saveNoteReference = @"
            IF (@Referenceskey > 0)
	            UPDATE dbo.SSPX_NoteReferences
	            SET ReferenceTitle = @Reference, UsersKey = @Userskey, LastUpdatedDt = GETDATE(), Active = 1
	            WHERE NoteReferenceskey = @Referenceskey
            ELSE
            BEGIN
	            DECLARE @ReferenceNumber INT = (SELECT MAX(ReferenceNumber) FROM dbo.SSPX_NoteReferences (NOLOCK)
		            WHERE ProtocolVersionNotesKey = @Noteskey)
	            SET @ReferenceNumber = COALESCE(@ReferenceNumber, 0)

	            INSERT INTO dbo.SSPX_NoteReferences (ProtocolVersionNotesKey, ReferenceNumber, 
		            ReferenceTitle, UsersKey, DateCreated, LastUpdatedDt, Active)
	            VALUES (@Noteskey, @ReferenceNumber + 1, 
		            @Reference, @Userskey, GETDATE(), GETDATE(), 1)
            END
        ";

        private static string _removeNoteReference = @"
            UPDATE dbo.SSPX_NoteReferences SET UsersKey = @Userskey, LastUpdatedDt = GETDATE(), Active = 0, ReferenceNumber = 0
            WHERE NoteReferenceskey = @Referenceskey
        ";

        private static string _switchTemplateItems = @"
            DECLARE @SortOrder INT = (SELECT SortOrder FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE TemplateVersionItemsKey = @ItemId)
            UPDATE dbo.SSPX_TemplateVersionItems SET SortOrder = (SELECT SortOrder FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE TemplateVersionItemsKey = @ItemId2)
                WHERE TemplateVersionItemsKey = @ItemId
            UPDATE dbo.SSPX_TemplateVersionItems SET SortOrder = @SortOrder WHERE TemplateVersionItemsKey = @ItemId2
        ";

        private static string _deleteTemplateItem = @"
            UPDATE dbo.SSPX_TemplateVersionItems SET Active = 0 WHERE TemplateVersionItemsKey IN (SELECT ItemId FROM dbo.ItemChildrenId(@ItemId, 1))
        ";

        private static string _insertTemplateItem = @"
            DECLARE @Versionskey INT, @SortOrder INT, @Parentkey INT, @NewItemKey INT, @NextSortOrder INT

            SELECT @Versionskey = TemplateVersionsKey, @SortOrder = SortOrder, @Parentkey = ParentTemplateVersionItemsKey
            FROM dbo.SSPX_TemplateVersionItems (NOLOCK)
            WHERE TemplateVersionItemsKey = @ItemId

            IF @InsertType = 1
	            SET @Parentkey = @ItemId
            
            SET @NextSortOrder = (SELECT TOP 1 SortOrder FROM dbo.SSPX_TemplateVersionItems (NOLOCK)
                WHERE TemplateVersionsKey = @Versionskey AND SortOrder > @SortOrder 
                ORDER BY SortOrder)
            SET @NextSortOrder = COALESCE(@NextSortOrder, 100)

            select @NextSortOrder, @SortOrder

            IF (((@NextSortOrder - @SortOrder) / @ItemCount) < 5)
                EXEC dbo.SSPx_ResortTemplateVersionItems @Versionskey

            INSERT INTO dbo.SSPX_TemplateVersionItems (VisibleText, RequiredLegacy, ItemTypesKey, SortOrder, 
	            DeprecatedFlag, Active, Hidden, ParentTemplateVersionItemsKey, TemplateVersionsKey, 
	            ChecklistTemplateItemCkey, enabled, visible, status, DefaultValue, AuthorityValue, SelectionDisablesChildren,
	            SelectionDisablesSiblings, Locked, type, styleClass, responseRequired, omitWhenSelected, 
	            colTextDelimiter, numCols, storedCol, minSelections, maxSelections, ordered, selected, 
	            popUpText, linkText, U_ChecklistTemplateItemKey, LongText,
	            U_DefaultDisabled, L_linkText2, EditorComment)
            SELECT '[' + TypeShortName + ' ' + CONVERT(VARCHAR, n.TempId) + ']', 0, ItemTypesKey, @SortOrder + (n.TempId * 5), 
	            0, 1, 0, @Parentkey, @Versionskey, 
	            0, 1, 1, 1, '', '', 0, 
	            0, 0, '', '', 0, 0,
	            '|', 0, 0, 0, 0, 1, 0,
	            '', '', 0, '[' + TypeShortName + ' ' + CONVERT(VARCHAR, n.TempId) + ']',
	            0, '', ''
            FROM dbo.ListOfItemTypes t (NOLOCK)
	            INNER JOIN dbo.GenerateNumber(@ItemCount) n ON n.TempId > 0
            WHERE ItemTypesKey = @ItemTypesKey
        ";

        private static string _getChecklistVersionComments = @"
            SELECT
	            pv.[ProtocolsKey],pvc.ProtocolVersionsKey, pvc.Commenttype, count(1) as commentCount
            FROM [dbo].[SSPX_ProtocolVersions] pv (NOLOCK)
            INNER JOIN (
	            -- latest active version of each protocol
	            SELECT pv2.[ProtocolsKey], [ProtocolVersion] = MAX(pv2.[ProtocolVersion])
	            FROM [dbo].[SSPX_ProtocolVersions] pv2 (NOLOCK)
	            WHERE pv2.[Active] = 1
	            GROUP BY pv2.[ProtocolsKey]
	            ) mostRecentVersions ON pv.ProtocolsKey = mostRecentVersions.ProtocolsKey 
	            AND pv.ProtocolVersion = mostRecentVersions.ProtocolVersion
            INNER JOIN SSPX_ProtocolVersionComments pvc (NOLOCK)
            on pvc.ProtocolVersionsKey = pv.ProtocolVersionsKey
            WHERE pv.[ProtocolsKey] = @ProtocolsKey AND pv.[Active] = 1 AND pvc.Active = 1
            group by pv.[ProtocolsKey],pvc.ProtocolVersionsKey, pvc.Commenttype
        ";

        private static string _getTemplateVersion = @"
            SELECT CONVERT(VARCHAR, v.TemplateVersionsKey) AS Versionskey, p.ProtocolVersion, 
                COALESCE(p.WebPostingDate, GETDATE()) AS WebPostingDate, 
                t.Lineage AS ProtocolName, '' AS Title, '' AS GroupCkey, '' AS ProtocolGroup, 
	            ''  as Cover, p.ProtocolVersionsKey
            FROM dbo.SSPX_ProtocolVersions p (NOLOCK)
            INNER JOIN
			    (-- latest active version of each protocol
	            SELECT pv2.[ProtocolsKey], [ProtocolVersion] = MAX(pv2.[ProtocolVersion])
	            FROM [dbo].[SSPX_ProtocolVersions] pv2 (NOLOCK)
	            WHERE pv2.[Active] = 1
	            GROUP BY pv2.[ProtocolsKey]
	            ) mostRecentVersions 
				ON p.ProtocolsKey = mostRecentVersions.ProtocolsKey 
	            AND p.ProtocolVersion = mostRecentVersions.ProtocolVersion
            INNER JOIN dbo.SSPX_TemplateVersions v (NOLOCK) ON p.ProtocolVersionsKey = v.ProtocolVersionsKey 
            INNER JOIN dbo.SSPX_ProtocolTemplates t (NOLOCK) ON v.ProtocolTemplatesKey = t.ProtocolTemplatesKey AND t.Active = 1
            WHERE p.Protocolskey = @ProtocolsKey AND v.Active = 1
        ";


        private static string _getTemplateVersionName = @"
            SELECT CONVERT(VARCHAR, v.TemplateVersionsKey) AS Versionskey, p.ProtocolVersion, 
                COALESCE(p.WebPostingDate, GETDATE()) AS WebPostingDate, 
                t.Lineage AS ProtocolName, '' AS Title, '' AS GroupCkey, '' AS ProtocolGroup, 
	            ''  as Cover, p.ProtocolVersionsKey
            FROM dbo.SSPX_ProtocolVersions p (NOLOCK)
            INNER JOIN
			    (-- latest active version of each protocol
	            SELECT pv2.[ProtocolsKey], [ProtocolVersion] = MAX(pv2.[ProtocolVersion])
	            FROM [dbo].[SSPX_ProtocolVersions] pv2 (NOLOCK)
	            WHERE pv2.[Active] = 1
	            GROUP BY pv2.[ProtocolsKey]
	            ) mostRecentVersions 
				ON p.ProtocolsKey = mostRecentVersions.ProtocolsKey 
	            AND p.ProtocolVersion = mostRecentVersions.ProtocolVersion
            INNER JOIN dbo.SSPX_TemplateVersions v (NOLOCK) ON p.ProtocolVersionsKey = v.ProtocolVersionsKey 
            INNER JOIN dbo.SSPX_ProtocolTemplates t (NOLOCK) ON v.ProtocolTemplatesKey = t.ProtocolTemplatesKey AND t.Active = 1
            WHERE v.TemplateVersionsKey = @TemplateVersionsKey AND v.Active = 1
        ";

        private static string _getFullTemplateVersion = @"
            IF (@VersionCkey = '')
                SET @VersionCkey = (
	                SELECT pv.[ProtocolVersionsKey]
	                FROM SSPX_ProtocolVersions pv (NOLOCK)
	                INNER JOIN (
		                -- most recent active version
		                SELECT pv2.[ProtocolsKey], MAX(pv2.[ProtocolVersion]) AS [MaxProtocolVersion]
		                FROM [dbo].[SSPX_ProtocolVersions] pv2 (NOLOCK)
		                WHERE pv2.[ProtocolsKey] = @ProtocolCkey AND pv2.Active = 1
		                GROUP BY pv2.[ProtocolsKey]
	                ) pv2 ON pv.[ProtocolsKey] = pv2.[ProtocolsKey]	AND	pv.[ProtocolVersion] = pv2.[MaxProtocolVersion]
                )
            
            SELECT CONVERT(VARCHAR, v.ProtocolVersionskey) AS VersionCkey, v.ProtocolVersion, 
                COALESCE(v.WebPostingDate, GETDATE()) AS WebPostingDate, 
                COALESCE(v.ProtocolVersionName, p.ProtocolName) AS ProtocolName, v.Title, 
                CONVERT(VARCHAR, l.ProtocolGroupKey) AS GroupCkey, l.ProtocolGroup, 
                COALESCE(v.CoverDocRaw, '')  as Cover
            FROM dbo.SSPX_ProtocolVersions v (NOLOCK)
            INNER JOIN dbo.SSPX_Protocols p (NOLOCK) ON v.Protocolskey = p.Protocolskey
            INNER JOIN dbo.SSPX_ProtocolGroupsMM g (NOLOCK) ON p.ProtocolsKey = g.ProtocolsKey
            INNER JOIN dbo.SSPX_ListOfProtocolGroups l (NOLOCK) ON g.ProtocolGroupKey = l.ProtocolGroupKey
            WHERE v.ProtocolVersionskey = @VersionCkey
        ";

        private static string _saveProtocolVersion = @"
            UPDATE dbo.SSPX_ProtocolVersions SET ProtocolVersionName = @ProtocolName, Title = @Description, 
                UsersKey = @PersonCkey, LastUpdatedDt = GETDATE()
            WHERE ProtocolVersionskey = @VersionCkey

            DECLARE @BaseOn TABLE (EVKey INT, Active BIT)
            INSERT INTO @BaseOn (EVKey, Active)
            SELECT CONVERT(INT, VariableString), 0
            FROM dbo.StringSplit(@BasedOnKey, ',', 100)
    
            UPDATE @BaseON SET Active = 1 
            WHERE EVKey IN (SELECT ExternalVersionKey FROM dbo.SSPX_ProtocolVersionsExternalVersionsMM (NOLOCK)
                WHERE ProtocolVersionsKey =  @VersionCkey)

            DELETE FROM dbo.SSPX_ProtocolVersionsExternalVersionsMM WHERE ProtocolVersionsKey =  @VersionCkey
            AND ExternalVersionKey NOT IN (SELECT EVKey FROM @BaseOn)

            INSERT INTO dbo.SSPX_ProtocolVersionsExternalVersionsMM (ExternalVersionKey, ProtocolVersionsKey)
            SELECT EVKey, @VersionCkey
            FROM @BaseOn WHERE Active = 0
        ";

        private static string _saveCover = @"
            UPDATE dbo.SSPX_ProtocolVersions SET CoverDocRaw = @Detail, UsersKey = @PersonCkey, LastUpdatedDt = GETDATE()
            WHERE ProtocolVersionskey = @VersionCkey
        ";

        //Zira Id SSP-66
        private static string _saveNote = @"
            IF (@NoteCKey > 0)
            BEGIN
                   UPDATE dbo.SSPX_ProtocolVersionNotes SET NoteDetails_html = @Note, NoteTitle = @NoteTitle,
                    UsersKey = @PersonCkey, LastUpdated = GETDATE()
                WHERE ProtocolVersionNotesKey = @NoteCKey
    
                UPDATE r SET ReferenceNumber = o.tempid, UsersKey = @PersonCkey, LastUpdatedDt = GETDATE()
                FROM dbo.SSPX_NoteReferences r (NOLOCK)
                       INNER JOIN dbo.StringSplit(@RefOrder, ',', 100) o ON o.variablestring = r.NoteReferencesKey
                WHERE r.ProtocolVersionNotesKey = @NoteCKey AND COALESCE(r.Active, 1) = 1
            END
            ELSE
            BEGIN
                DECLARE @VersionCkey INT = (
                       SELECT pv.[ProtocolVersionsKey]
                       FROM SSPX_ProtocolVersions pv (NOLOCK)
                       INNER JOIN (
                              -- most recent active version
                              SELECT pv2.[ProtocolsKey], MAX(pv2.[ProtocolVersion]) AS [MaxProtocolVersion]
                              FROM [dbo].[SSPX_ProtocolVersions] pv2 (NOLOCK)
                              WHERE pv2.[ProtocolsKey] = @ProtocolCkey AND pv2.Active = 1
                              GROUP BY pv2.[ProtocolsKey]
                       ) pv2 ON pv.[ProtocolsKey] = pv2.[ProtocolsKey]      AND       pv.[ProtocolVersion] = pv2.[MaxProtocolVersion]
                )
                   DECLARE @NoteNumber VARCHAR(2) = (SELECT TOP 1 NoteID FROM dbo.SSPX_ProtocolVersionNotes (NOLOCK)
                       WHERE ProtocolVersionskey = @VersionCkey and Active=1 ORDER BY LEN(NoteID) DESC, NoteID DESC)
                   SET @NoteNumber = dbo.GetNextNoteNumber(@NoteNumber)

                   INSERT INTO dbo.SSPX_ProtocolVersionNotes (NoteID, NoteTitle, 
                          NoteDetails_html, ProtocolVersionsKey, UsersKey, DateCreated, LastUpdated, Active)
                   VALUES (@NoteNumber, @NoteTitle,  
                          @Note, @VersionCkey, @PersonCkey, GETDATE(), GETDATE(), 1)
            END
        ";
        //Zira Id SSP-66

        private static string _saveProtocolVersionComment = @"
            IF @CommentId > 0
            BEGIN
	            IF ISNUMERIC(@ItemKey) = 0
		            UPDATE dbo.SSPX_ProtocolVersionComments SET Comment = @Comment, LastUpdated = GETDATE() WHERE ProtocolVersionCommentsKey = @CommentId
	            ELSE
		            UPDATE dbo.SSPX_TemplateVersionItemsReviewComments SET Comment = @Comment WHERE TemplateVersionItemsReviewCommentsKey = @CommentId
            END
            ELSE
            BEGIN
	            IF ISNUMERIC(@ItemKey) = 0
	            BEGIN
		            DECLARE @CommentType VARCHAR(25) = ''
		            IF (@ItemKey = '1_F')
			            SET @CommentType = 'Title'
		            ELSE IF (@ItemKey = '2_F')
			            SET @CommentType = 'Cover'
		            ELSE IF (@ItemKey = '3_F')
			            SET @CommentType = 'Authors'
		            ELSE IF (@ItemKey = '5_F')
			            SET @CommentType = 'Explanatory Notes'
		            ELSE IF (@ItemKey = '6_F')
			            SET @CommentType = 'References'

		            INSERT INTO dbo.SSPX_ProtocolVersionComments (ProtocolVersionsKey, Comment, CommentType, Userskey, DateCreated)
		            VALUES (@ProtocolVersionKey, @Comment, @CommentType, @Personkey, GETDATE())
	            END
	            ELSE
		            INSERT INTO dbo.SSPX_TemplateVersionItemsReviewComments (TemplateVersionItemsKey, Comment, UsersKey, DateCreated)
		            VALUES (@ItemKey, @Comment, @PersonKey, GETDATE())
            END
        ";

        private static string _deleteProtocolComment = @"
            IF @IsItem = 1
                UPDATE dbo.SSPX_TemplateVersionItemsReviewComments SET Active = 0 WHERE TemplateVersionItemsReviewCommentsKey = @CommentId
            ELSE
	            UPDATE dbo.SSPX_ProtocolVersionComments SET Active = 0 WHERE ProtocolVersionCommentsKey = @CommentId
        ";

        private static string _getNotesForVersion = @"
            SELECT CONVERT(VARCHAR, e.ProtocolVersionNotesKey) AS NoteCkey, e.NoteID, e.NoteTitle, 
	            COALESCE(e.NoteDetails_html, '') AS NoteDetails_html, 0 AS CommentCount
            FROM dbo.SSPX_ProtocolVersionNotes e (NOLOCK)  
            WHERE e.ProtocolVersionsKey = @VersionCkey AND e.Active = 1
            ORDER BY LEN(e.NoteID), e.NoteID
        ";

        private static string _getNoteReferencesForVersion = @"
            SELECT r.ProtocolVersionNotesKey, CONVERT(VARCHAR, r.NoteReferencesKey) AS NotesReferenceCkey, 
	            COALESCE(r.ReferenceNumber, 1) AS ReferenceNumber, r.ReferenceTitle, '' AS NoteTitletitle
            FROM dbo.SSPX_NoteReferences r (NOLOCK) 
	            INNER JOIN dbo.SSPX_ProtocolVersionNotes n (NOLOCK) ON r.ProtocolVersionNotesKey = n.ProtocolVersionNotesKey
            WHERE COALESCE(r.Active, 1) = 1 AND n.ProtocolVersionsKey = @VersionCkey
            ORDER BY r.ProtocolVersionNotesKey, r.ReferenceNumber
        ";

        private static string _getNoteTitles = @"
            DECLARE @VersionKey INT = (SELECT TOP 1 ProtocolVersionsKey FROM dbo.SSPX_ProtocolVersions (NOLOCK)
                WHERE Active = 1 AND Protocolskey = @ProtocolsKey ORDER BY ProtocolVersion DESC)

            SELECT CONVERT(VARCHAR, e.ProtocolVersionNotesKey) AS NoteCkey, e.NoteID, e.NoteTitle, 
	            '' AS NoteDetails_html, COUNT(n.NoteCommentsKey) AS CommentCount
            FROM dbo.SSPX_ProtocolVersionNotes e (NOLOCK)
		        INNER JOIN dbo.SSPX_ProtocolVersions v (NOLOCK) ON e.ProtocolVersionsKey = v.ProtocolVersionsKey
	            LEFT OUTER JOIN dbo.SSPX_NoteComments n (NOLOCK) ON e.ProtocolVersionNotesKey = n.ProtocolNotesKey AND n.Active = 1
            WHERE v.ProtocolVersionsKey = @VersionKey AND e.Active = 1
            GROUP BY e.ProtocolVersionNotesKey, e.NoteID, e.NoteTitle, e.ProtocolVersionsKey
            ORDER BY e.ProtocolVersionsKey, LEN(e.NoteID), e.NoteID
        ";

        private static string _removeNote = @"
            DECLARE @ProtocolVersionKey INT = (SELECT ProtocolVersionsKey FROM dbo.SSPX_ProtocolVersionNotes (NOLOCK) 
                WHERE ProtocolVersionNotesKey = @NoteKey)

            UPDATE dbo.SSPX_ProtocolVersionNotes SET Active = 0, UsersKey = @UsersKey WHERE ProtocolVersionNotesKey = @NoteKey

            EXEC dbo.SSPx_NoteRenumber @ProtocolVersionKey
        ";

        private static string _moveNote = @"
            DECLARE @NoteID VARCHAR(2) = (SELECT NoteID FROM dbo.SSPX_ProtocolVersionNotes (NOLOCK)
                WHERE ProtocolVersionNotesKey = @NoteKey1)
            
            UPDATE dbo.SSPX_ProtocolVersionNotes SET NoteID = (SELECT NoteID FROM dbo.SSPX_ProtocolVersionNotes (NOLOCK)
                WHERE ProtocolVersionNotesKey = @NoteKey2) WHERE ProtocolVersionNotesKey = @NoteKey1

            UPDATE dbo.SSPX_ProtocolVersionNotes SET NoteID = @NoteID WHERE ProtocolVersionNotesKey = @NoteKey2
        ";

        private static string _addNote = @"
            DECLARE @ProtocolVersionKey INT = (SELECT ProtocolVersionsKey FROM dbo.SSPX_ProtocolVersionNotes (NOLOCK) 
                WHERE ProtocolVersionNotesKey = @NoteKey)

            INSERT INTO dbo.SSPX_ProtocolVersionNotes (NoteID, NoteTitle, ProtocolVersionsKey, UsersKey, Active, DateCreated)
            SELECT NoteID, 'New Note', ProtocolVersionsKey, @UsersKey, 1, GETDATE()
            FROM dbo.SSPX_ProtocolVersionNotes (NOLOCK)
            WHERE ProtocolVersionNotesKey = @NoteKey
    
            EXEC dbo.SSPx_NoteRenumber @ProtocolVersionKey
        ";

        private static string _copyNote = @"
            DECLARE @ProtocolVersionKey INT = (SELECT ProtocolVersionsKey FROM dbo.SSPX_ProtocolVersionNotes (NOLOCK) 
                WHERE ProtocolVersionNotesKey = @NoteKey)

            INSERT INTO dbo.SSPX_ProtocolVersionNotes (NoteID, NoteTitle, NoteRaw, NoteDetails_html, ProtocolVersionsKey, UsersKey, Active, DateCreated)
            SELECT NoteID, NoteTitle, NoteRaw, NoteDetails_html, ProtocolVersionsKey, @UsersKey, 1, GETDATE()
            FROM dbo.SSPX_ProtocolVersionNotes (NOLOCK)
            WHERE ProtocolVersionNotesKey = @NoteKey
    
            EXEC dbo.SSPx_NoteRenumber @ProtocolVersionKey
        ";

        private static string _getProtocolVersion = @"
            SELECT TOP 1 CONVERT(VARCHAR, @ProtocolsKey) AS ProtocolsKey, 
                CONVERT(VARCHAR, ProtocolVersionsKey) AS ProtocolVersionsKey FROM dbo.SSPX_ProtocolVersions (NOLOCK)
            WHERE Active = 1 AND Protocolskey = @ProtocolsKey ORDER BY ProtocolVersion DESC
        ";

        private static string _getNotes = @"
            IF @NoteId = ''
            BEGIN
                DECLARE @VersionKey INT = (SELECT TOP 1 ProtocolVersionsKey FROM dbo.SSPX_ProtocolVersions (NOLOCK)
                    WHERE Active = 1 AND Protocolskey = @ProtocolsKey ORDER BY ProtocolVersion DESC)

                SELECT CONVERT(VARCHAR, e.ProtocolVersionNotesKey) AS NoteCkey, e.NoteID, e.NoteTitle, 
	                COALESCE(e.NoteDetails_html, '') AS NoteDetails_html, COUNT(n.NoteCommentsKey) AS CommentCount
                FROM dbo.SSPX_ProtocolVersionNotes e (NOLOCK)
		            INNER JOIN dbo.SSPX_ProtocolVersions v (NOLOCK) ON e.ProtocolVersionsKey = v.ProtocolVersionsKey
	                LEFT OUTER JOIN dbo.SSPX_NoteComments n (NOLOCK) ON e.ProtocolVersionNotesKey = n.ProtocolNotesKey AND n.Active = 1
                WHERE v.ProtocolVersionsKey = @VersionKey AND e.Active = 1
                GROUP BY e.ProtocolVersionNotesKey, e.NoteID, e.NoteTitle, e.ProtocolVersionsKey, e.NoteDetails_html
                ORDER BY e.ProtocolVersionsKey, LEN(e.NoteID), e.NoteID
            END
            ELSE
	            SELECT CONVERT(VARCHAR, e.ProtocolVersionNotesKey) AS NoteCkey, e.NoteID, e.NoteTitle, 
	                COALESCE(e.NoteDetails_html, '') AS NoteDetails_html, COUNT(n.NoteCommentsKey) AS CommentCount
                FROM dbo.SSPX_ProtocolVersionNotes e (NOLOCK)
	                LEFT OUTER JOIN dbo.SSPX_NoteComments n (NOLOCK) ON e.ProtocolVersionNotesKey = n.ProtocolNotesKey AND n.Active = 1
                WHERE e.Active = 1 AND e.ProtocolVersionNotesKey = @NoteId
                GROUP BY e.ProtocolVersionNotesKey, e.NoteID, e.NoteTitle, e.ProtocolVersionsKey, e.NoteDetails_html
                ORDER BY e.ProtocolVersionsKey, LEN(e.NoteID), e.NoteID
        ";

        private static string _getNoteKeys = @"
            IF @Type = 'C'
	            SELECT CONVERT(VARCHAR, e.ProtocolVersionNotesKey) AS NoteCkey, e.NoteID, CONVERT(VARCHAR, n.TemplateVersionItemsKey) + '_I' AS Title,e.NoteDetails_html AS Note
	            FROM dbo.SSPX_ItemNotesMM n (NOLOCK)
		            INNER JOIN dbo.SSPX_ProtocolVersionNotes e (NOLOCK) ON n.ProtocolVersionNotesKey = e.ProtocolVersionNotesKey
		            INNER JOIN dbo.SSPX_TemplateVersionItems i (NOLOCK) ON n.TemplateVersionItemsKey = i.TemplateVersionItemsKey
	            WHERE i.Active = 1 AND i.VisibleText <> '' AND i.TemplateVersionsKey = @ItemId AND n.Active = 1 
	            ORDER BY n.TemplateVersionItemsKey, e.NoteID 
            ELSE IF @Type = 'I'
	           SELECT CONVERT(VARCHAR, e.ProtocolVersionNotesKey) AS NoteCkey, e.NoteID, CONVERT(VARCHAR, n.TemplateVersionItemsKey) + '_I' AS Title,e.NoteDetails_html AS Note
            FROM dbo.SSPX_ItemNotesMM n (NOLOCK)
	            INNER JOIN dbo.SSPX_ProtocolVersionNotes e (NOLOCK) ON n.ProtocolVersionNotesKey = e.ProtocolVersionNotesKey
            WHERE n.TemplateVersionItemsKey = @ItemId AND n.Active = 1
            ORDER BY e.NoteID
        ";

        private static string _getNoteKeyForItems = @"
            IF @Type = 'C'
	            SELECT CONVERT(VARCHAR, e.ProtocolVersionNotesKey) AS NoteCkey, e.NoteID, CONVERT(VARCHAR, n.TemplateVersionItemsKey) + '_I' AS Title,e.NoteDetails_html AS Note
	            FROM dbo.SSPX_ItemNotesMM n (NOLOCK)
		            INNER JOIN dbo.SSPX_ProtocolVersionNotes e (NOLOCK) ON n.ProtocolVersionNotesKey = e.ProtocolVersionNotesKey
		            INNER JOIN dbo.SSPX_TemplateVersionItems i (NOLOCK) ON n.TemplateVersionItemsKey = i.TemplateVersionItemsKey
	            WHERE i.Active = 1 AND i.VisibleText <> '' AND i.TemplateVersionsKey = @ItemId AND n.Active = 1
	            ORDER BY n.TemplateVersionItemsKey, e.NoteID 
            ELSE IF @Type = 'I'
	            SELECT CONVERT(VARCHAR, e.ProtocolVersionNotesKey) AS NoteCkey, e.NoteID, CONVERT(VARCHAR, n.TemplateVersionItemsKey) + '_I' AS Title,e.NoteDetails_html AS Note
	            FROM dbo.SSPX_ItemNotesMM n (NOLOCK)
		            INNER JOIN dbo.SSPX_ProtocolVersionNotes e (NOLOCK) ON n.ProtocolVersionNotesKey = e.ProtocolVersionNotesKey
		            INNER JOIN dbo.SSPX_TemplateVersionItems i (NOLOCK) ON n.TemplateVersionItemsKey = i.TemplateVersionItemsKey
	            WHERE i.Active = 1 AND i.VisibleText <> '' AND n.Active = 1 AND i.ParentTemplateVersionItemsKey = @ItemId
	            ORDER BY n.TemplateVersionItemsKey, e.NoteID
        ";

        private static string _saveItemNotes = @"
            DECLARE @NoteCKey TABLE (
	            NoteCkey INT,
	            Existed BIT
            )

            INSERT INTO @NoteCKey (NoteCkey, Existed)
            SELECT CONVERT(INT, VariableString), 0
            FROM StringSplit(@NoteKeys, ',', 200)

            UPDATE @NoteCKey SET Existed = 1 FROM @NoteCKey c
            INNER JOIN dbo.SSPX_ItemNotesMM n (NOLOCK) ON c.NoteCkey = n.ProtocolVersionNotesKey
            WHERE n.TemplateVersionItemsKey = @ItemId

            INSERT INTO dbo.SSPX_ItemNotesMM (TemplateVersionItemsKey, ProtocolVersionNotesKey, Active, DateCreated, LastUpdated)
            SELECT @ItemId, NoteCkey, 1, GETDATE(), GETDATE()
            FROM @NoteCKey WHERE Existed = 0

            UPDATE n SET Active = 1, LastUpdated = GETDATE()
            FROM dbo.SSPX_ItemNotesMM n (NOLOCK)
            INNER JOIN @NoteCKey c ON c.NoteCkey = n.ProtocolVersionNotesKey
            WHERE n.TemplateVersionItemsKey = @ItemId AND c.Existed = 1 AND n.Active = 0

            UPDATE n SET Active = 0, LastUpdated = GETDATE()
            FROM dbo.SSPX_ItemNotesMM n (NOLOCK)
            WHERE n.TemplateVersionItemsKey = @ItemId AND n.ProtocolVersionNotesKey NOT IN (SELECT NoteCkey FROM @NoteCkey)
        ";

        private static string _getNoteReferences = @"
            SELECT r.ProtocolVersionNotesKey, CONVERT(VARCHAR, r.NoteReferencesKey) AS NotesReferenceCkey, 
	            COALESCE(r.ReferenceNumber, 1) AS ReferenceNumber, r.ReferenceTitle, n.NoteTitle
            FROM dbo.SSPX_NoteReferences r (NOLOCK)
                INNER JOIN dbo.SSPX_ProtocolVersionNotes n (NOLOCK) ON r.ProtocolVersionNotesKey = n.ProtocolVersionNotesKey
            WHERE COALESCE(r.Active, 1) = 1 AND r.ProtocolVersionNotesKey = @NoteId
            ORDER BY r.ProtocolVersionNotesKey, r.ReferenceNumber
        ";

        private static string _getNoteComments = @"
            SELECT c.ProtocolNotesKey, c.NoteCommentsKey, c.Comment, n.NoteTitle, u.FirstName, u.LastName, c.DateCreated, 6 AS CommentType
            FROM dbo.SSPX_NoteComments c (NOLOCK)
                INNER JOIN dbo.SSPX_Users u (NOLOCK) ON c.UsersKey = u.Userskey
                INNER JOIN dbo.SSPX_ProtocolVersionNotes n (NOLOCK) ON c.ProtocolNotesKey = n.ProtocolVersionNotesKey 
            WHERE c.ProtocolNotesKey = @NoteId AND c.Active = 1
            ORDER BY DateCreated DESC
        ";

        private static string _getProtocolVersionComments = @"
            DECLARE @CommentType VARCHAR(25) = ''
            IF (@ItemKey = '1_F')
	            SET @CommentType = 'Title'
            ELSE IF (@ItemKey = '2_F')
	            SET @CommentType = 'Cover'
            ELSE IF (@ItemKey = '3_F')
	            SET @CommentType = 'Authors'
            ELSE IF (@ItemKey = '5_F')
	            SET @CommentType = 'Explanatory Notes'
            ELSE IF (@ItemKey = '6_F')
	            SET @CommentType = 'References'

            IF (@CommentType = '')
	            SELECT i.TemplateVersionItemsKey AS ItemKey, 
		            c.TemplateVersionItemsReviewCommentsKey, c.Comment, i.VisibleText, 
		            u.FirstName, u.LastName, c.DateCreated, 0 AS CommentType
	            FROM dbo.SSPX_TemplateVersionItemsReviewComments c (NOLOCK)
		            INNER JOIN dbo.SSPX_Users u (NOLOCK) ON c.UsersKey = u.Userskey
		            INNER JOIN dbo.SSPX_TemplateVersionItems i (NOLOCK) ON c.TemplateVersionItemsKey = i.TemplateVersionItemsKey
	            WHERE c.Active = 1 AND i.TemplateVersionItemsKey = @ItemKey
	            ORDER BY c.DateCreated DESC
            ELSE 
	            SELECT 0 AS ItemKey, 
		            c.ProtocolVersionCommentsKey, c.Comment, c.CommentType AS VisibleText, 
		            u.FirstName, u.LastName, c.DateCreated, 0 AS CommentType
	            FROM dbo.SSPX_ProtocolVersionComments c (NOLOCK)
		            INNER JOIN dbo.SSPX_Users u (NOLOCK) ON c.UsersKey = u.Userskey
	            WHERE c.Active = 1 AND c.ProtocolVersionsKey = @ProtocolVersionKey AND CommentType = @CommentType
	            ORDER BY c.DateCreated DESC
        ";

        //SSP 136 - Get total number of Comments count for a Protocol
        private static string _getProtocolVersionCommentsForAll = @"
               SELECT 0 AS ItemKey,
	                c.TemplateVersionItemsReviewCommentsKey as VersionCommentsKey, c.Comment,
	                u.FirstName, u.LastName, c.CopiedDateTime, 0 AS CommentType ,LR.RolesKey
	                FROM dbo.TemplateVersionItemsReviewComments c (NOLOCK)
		                inner join TemplateVersionItems TVI on TvI.TemplateVersionItemsKey = c.TemplateVersionItemsKey
		                inner join TemplateVersions TV on tv.TemplateVersionsKey = TVI.TemplateVersionsKey
		                INNER JOIN dbo.Users u (NOLOCK) ON c.CopiedByUsersKey = u.Userskey 
						Inner join dbo.ProtocolVersionsUserRoles PVUR on PVUR.UsersKey = u.Userskey 
						Inner JOin dbo.ListOfRoles LR ON LR.RolesKey = PVUR.RolesKey
	                WHERE c.Active = 1 AND TV.ProtocolVersionsKey = @ProtocolVersionKey  and PVUR.ProtocolVersionsKey = @ProtocolVersionKey
                union
                SELECT 0 AS ItemKey,
	                c.ProtocolVersionsCommentsKey as VersionCommentsKey, c.Comment,
	                u.FirstName, u.LastName, c.CopiedDateTime, 0 AS CommentType, LR.RolesKey
	                FROM dbo.ProtocolVersionsComments c (NOLOCK)
		                INNER JOIN dbo.Users u (NOLOCK) ON c.CopiedByUsersKey = u.Userskey
						Inner join dbo.ProtocolVersionsUserRoles PVUR on PVUR.UsersKey = u.Userskey 
						Inner JOin dbo.ListOfRoles LR ON LR.RolesKey = PVUR.RolesKey
	                WHERE c.Active = 1 AND c.ProtocolVersionsKey = @ProtocolVersionKey and PVUR.ProtocolVersionsKey = @ProtocolVersionKey
	                ORDER BY c.CopiedDateTime DESC
        ";
        //Zira Id SSP-99
        private static string _getItemChildrenById = @"
                IF @Type = 'C'
        				SELECT CONVERT(VARCHAR, i.TemplateVersionItemsKey)+ '_I' AS ItemKey, LEFT(COALESCE(NULLIF(i.VisibleText, ''), i.LongText), 150) AS ItemText, 
                        COALESCE(NULLIF(i.VisibleText, ''), i.LongText) AS ItemLongText, 
                           CASE WHEN EXISTS (SELECT ParentTemplateVersionItemsKey FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE ParentTemplateVersionItemsKey = i.TemplateVersionItemsKey) THEN 1 ELSE 0 END AS HasItems,  
                           COUNT(c.TemplateVersionItemsReviewCommentsKey)  AS CommentCount, COALESCE(i.MustImplement, CONVERT(BIT, 0)) AS Required,
                           COALESCE(i.Condition, '') AS Condition, i.ItemTypesKey, COALESCE(i.EditorComment, '') AS Comments, 
                           CONVERT(VARCHAR, i.TemplateVersionsKey) AS VersionKey,
                           i.ReportText, i.DataTypeKey, i.MaxRepetitions AS AnswerMaxReps, i.AnswerMaxValue, i.AnswerMinValue, i.AnswerUnitsKey, 
                           i.MinRepetitions AS AnswerMinReps, CASE WHEN i.visible = 1 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END AS Hidden,i.SortOrder,
        CAST(i.SortOrder AS varbinary(max))  AS Sortkey, 0 AS Indent,
        	CONVERT(VARCHAR, i.ParentTemplateVersionItemsKey) + '_I'  AS ParentId 
        FROM dbo.SSPX_TemplateVersionItems i (NOLOCK)
        	INNER JOIN dbo.ListOfItemTypes t (NOLOCK) ON i.ItemTypesKey = t.ItemTypesKey
        	LEFT OUTER JOIN dbo.SSPX_TemplateVersionItemsReviewComments c (NOLOCK)
        	 ON i.TemplateVersionItemsKey = c.TemplateVersionItemsKey AND c.Active = 1
        WHERE i.TemplateVersionsKey = @ItemId AND i.Active = 1 AND i.ParentTemplateVersionItemsKey IS NULL
        GROUP BY i.TemplateVersionItemsKey, i.VisibleText, i.ItemTypesKey, i.SortOrder, i.MustImplement, 
        	i.TemplateVersionsKey, i.visible, i.LongText, i.Condition,i.ParentTemplateVersionItemsKey,i.EditorComment,i.SortOrder,
			 i.ReportText, i.DataTypeKey, i.MaxRepetitions, i.AnswerMaxValue, i.AnswerMinValue, i.AnswerUnitsKey,  i.MinRepetitions
        ORDER BY i.SortOrder  
                ELSE IF @Type = 'I'
         SELECT CONVERT(VARCHAR, i.TemplateVersionItemsKey)+ '_I' AS ItemKey, LEFT(COALESCE(NULLIF(i.VisibleText, ''), i.LongText), 150) AS ItemText, 
                        COALESCE(NULLIF(i.VisibleText, ''), i.LongText) AS ItemLongText, 
                           CASE WHEN EXISTS (SELECT ParentTemplateVersionItemsKey FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE ParentTemplateVersionItemsKey = i.TemplateVersionItemsKey) THEN 1 ELSE 0 END AS HasItems,  
                           COUNT(c.TemplateVersionItemsReviewCommentsKey) AS CommentCount, COALESCE(i.MustImplement, CONVERT(BIT, 0)) AS Required,
                           COALESCE(i.Condition, '') AS Condition, i.ItemTypesKey, COALESCE(i.EditorComment, '') AS Comments, 
                           CONVERT(VARCHAR, i.TemplateVersionsKey) AS VersionKey,
                           i.ReportText, i.DataTypeKey, i.MaxRepetitions AS AnswerMaxReps, i.AnswerMaxValue, i.AnswerMinValue, i.AnswerUnitsKey, 
                           i.MinRepetitions AS AnswerMinReps, CASE WHEN i.visible = 1 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END AS Hidden,i.SortOrder,
       CAST(i.SortOrder AS varbinary(max))  AS Sortkey, 0 AS Indent,
        	CONVERT(VARCHAR, i.ParentTemplateVersionItemsKey) + '_I'  AS ParentId 
        FROM dbo.SSPX_TemplateVersionItems i (NOLOCK)
        	INNER JOIN dbo.ListOfItemTypes t (NOLOCK) ON i.ItemTypesKey = t.ItemTypesKey
        	LEFT OUTER JOIN dbo.SSPX_TemplateVersionItemsReviewComments c (NOLOCK)
        	 ON i.TemplateVersionItemsKey = c.TemplateVersionItemsKey AND c.Active = 1
        WHERE i.Active = 1 AND i.ParentTemplateVersionItemsKey =  @ItemId
        GROUP BY i.TemplateVersionItemsKey, i.VisibleText, i.ItemTypesKey, i.SortOrder, i.MustImplement, 
        	i.TemplateVersionsKey, i.visible, i.LongText, i.Condition,i.ParentTemplateVersionItemsKey,i.EditorComment,i.SortOrder,
			 i.ReportText, i.DataTypeKey, i.MaxRepetitions, i.AnswerMaxValue, i.AnswerMinValue, i.AnswerUnitsKey,  i.MinRepetitions
        ORDER BY i.SortOrder  
            ";
        //Zira Id SSP-99

        //Zira Id SSP-98
        private static string _getAllItemChildrenById = @"
            IF @Type = 'C'
				SELECT CONVERT(VARCHAR, i.TemplateVersionItemsKey)+ '_I' AS ItemKey, LEFT(COALESCE(NULLIF(i.VisibleText, ''), i.LongText), 150) AS ItemText, 
                        COALESCE(NULLIF(i.VisibleText, ''), i.LongText) AS ItemLongText, 
                           CASE WHEN EXISTS (SELECT ParentTemplateVersionItemsKey FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE ParentTemplateVersionItemsKey = i.TemplateVersionItemsKey) THEN 1 ELSE 0 END AS HasItems,  
                           0 AS CommentCount, COALESCE(i.MustImplement, CONVERT(BIT, 0)) AS Required,
                           COALESCE(i.Condition, '') AS Condition, i.ItemTypesKey, COALESCE(i.EditorComment, '') AS Comments, 
                           CONVERT(VARCHAR, i.TemplateVersionsKey) AS VersionKey,
                           i.ReportText, i.DataTypeKey, i.MaxRepetitions AS AnswerMaxReps, i.AnswerMaxValue, i.AnswerMinValue, i.AnswerUnitsKey, 
                           i.MinRepetitions AS AnswerMinReps, CASE WHEN i.visible = 1 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END AS Hidden,i.SortOrder,
        CAST(i.SortOrder AS varbinary(max))  AS Sortkey, 0 AS Indent,
        	CONVERT(VARCHAR, i.ParentTemplateVersionItemsKey) + '_I'  AS ParentId 
        FROM dbo.SSPX_TemplateVersionItems i (NOLOCK)
        	INNER JOIN dbo.ListOfItemTypes t (NOLOCK) ON i.ItemTypesKey = t.ItemTypesKey
        	LEFT OUTER JOIN dbo.SSPX_TemplateVersionItemsReviewComments c (NOLOCK)
        	 ON i.TemplateVersionItemsKey = c.TemplateVersionItemsKey AND c.Active = 1
        WHERE i.TemplateVersionsKey = @ItemId AND i.Active = 1 
        GROUP BY i.TemplateVersionItemsKey, i.VisibleText, i.ItemTypesKey, i.SortOrder, i.MustImplement, 
        	i.TemplateVersionsKey, i.visible, i.LongText, i.Condition,i.ParentTemplateVersionItemsKey,i.EditorComment,i.SortOrder,
			 i.ReportText, i.DataTypeKey, i.MaxRepetitions, i.AnswerMaxValue, i.AnswerMinValue, i.AnswerUnitsKey,  i.MinRepetitions
        ORDER BY i.SortOrder   
            ELSE IF @Type = 'I'
				  WITH Casesummary

        AS

        (

  SELECT CONVERT(VARCHAR, parent.TemplateVersionItemsKey)+ '_I'  AS ItemKey, LEFT(COALESCE(NULLIF(parent.VisibleText, ''), parent.LongText), 150) AS ItemText, 
                        COALESCE(NULLIF(parent.VisibleText, ''), parent.LongText) AS ItemLongText, 
                           CASE WHEN EXISTS (SELECT ParentTemplateVersionItemsKey FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE ParentTemplateVersionItemsKey =@ItemId) THEN 1 ELSE 0 END AS HasItems,  
                           0 AS CommentCount, COALESCE(parent.MustImplement, CONVERT(BIT, 0)) AS Required,
                           COALESCE(parent.Condition, '') AS Condition, parent.ItemTypesKey, COALESCE(parent.EditorComment, '') AS Comments, 
                           CONVERT(VARCHAR, parent.TemplateVersionsKey) AS VersionKey,
                           parent.ReportText, parent.DataTypeKey, parent.MaxRepetitions AS AnswerMaxReps, parent.AnswerMaxValue, parent.AnswerMinValue, parent.AnswerUnitsKey, 
                           parent.MinRepetitions AS AnswerMinReps, CASE WHEN parent.visible = 1 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END AS Hidden,parent.SortOrder,
      CAST(parent.SortOrder AS VARBINARY(MAX)) AS Sortkey, 0 AS Indent,
        	CONVERT(VARCHAR, parent.ParentTemplateVersionItemsKey) + '_I'  AS ParentId 

        FROM SSPX_TemplateVersionItems  parent(NOLOCK)
		INNER JOIN dbo.ListOfItemTypes t (NOLOCK) ON parent.ItemTypesKey = t.ItemTypesKey

        WHERE parent.TemplateVersionItemsKey =@ItemId
        UNION ALL

    SELECT CONVERT(VARCHAR, parent.TemplateVersionItemsKey)+'_I'  AS ItemKey, LEFT(COALESCE(NULLIF(parent.VisibleText, ''), parent.LongText), 150) AS ItemText, 
                        COALESCE(NULLIF(parent.VisibleText, ''), parent.LongText) AS ItemLongText, 
                           CASE WHEN EXISTS (SELECT ParentTemplateVersionItemsKey FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE ParentTemplateVersionItemsKey =@ItemId) THEN 1 ELSE 0 END AS HasItems,  
                           0 AS CommentCount, COALESCE(parent.MustImplement, CONVERT(BIT, 0)) AS Required,
                           COALESCE(parent.Condition, '') AS Condition, parent.ItemTypesKey, COALESCE(parent.EditorComment, '') AS Comments, 
                           CONVERT(VARCHAR, parent.TemplateVersionsKey) AS VersionKey,
                           parent.ReportText, parent.DataTypeKey, parent.MaxRepetitions AS AnswerMaxReps, parent.AnswerMaxValue, parent.AnswerMinValue, parent.AnswerUnitsKey, 
                           parent.MinRepetitions AS AnswerMinReps, CASE WHEN parent.visible = 1 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END AS Hidden,parent.SortOrder,
      CAST(parent.SortOrder AS VARBINARY(MAX)) AS Sortkey, 0 AS Indent,
        	CONVERT(VARCHAR, parent.ParentTemplateVersionItemsKey) + '_I'  AS ParentId 
        FROM SSPX_TemplateVersionItems parent (NOLOCK)
		INNER JOIN dbo.ListOfItemTypes t (NOLOCK) ON parent.ItemTypesKey = t.ItemTypesKey

        WHERE parent.ParentTemplateVersionItemsKey =@ItemId

        UNION ALL

         SELECT CONVERT(VARCHAR, child.TemplateVersionItemsKey)+'_I' AS ItemKey, LEFT(COALESCE(NULLIF(child.VisibleText, ''), child.LongText), 150) AS ItemText, 
                        COALESCE(NULLIF(child.VisibleText, ''), child.LongText) AS ItemLongText, 
                           CASE WHEN EXISTS (SELECT ParentTemplateVersionItemsKey FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE ParentTemplateVersionItemsKey =@ItemId) THEN 1 ELSE 0 END AS HasItems,  
                           0 AS CommentCount, COALESCE(child.MustImplement, CONVERT(BIT, 0)) AS Required,
                           COALESCE(child.Condition, '') AS Condition, child.ItemTypesKey, COALESCE(child.EditorComment, '') AS Comments, 
                           CONVERT(VARCHAR, child.TemplateVersionsKey) AS VersionKey,
                           child.ReportText, child.DataTypeKey, child.MaxRepetitions AS AnswerMaxReps, child.AnswerMaxValue, child.AnswerMinValue, child.AnswerUnitsKey, 
                           child.MinRepetitions AS AnswerMinReps, CASE WHEN child.visible = 1 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END AS Hidden,child.SortOrder,
      CL.Sortkey + CAST(child.SortOrder AS varbinary(max))  AS Sortkey, CL.Indent + 1 AS Indent,
        	CONVERT(VARCHAR, child.ParentTemplateVersionItemsKey) + '_I'  AS ParentId 
        FROM SSPX_TemplateVersionItems as child (NOLOCK)
		INNER JOIN dbo.ListOfItemTypes t (NOLOCK) ON child.ItemTypesKey = t.ItemTypesKey

        INNER JOIN Casesummary as CL ON CONVERT(VARCHAR, child.ParentTemplateVersionItemsKey) + '_I' = CL.ItemKey

        WHERE child.ParentTemplateVersionItemsKey !=@ItemId

        )

        SELECT* FROM Casesummary
       ORDER BY Sortkey
        ";
        //Zira Id SSP-98

        private static String _getTemplateVersionItems = @"            
		DECLARE @Versionskey INT
--DECLARE @ItemType varchar(20)
--DECLARE @ItemId varchar(20)

        IF @ItemType = 'I'
              BEGIN
            SET @Versionskey = (SELECT TemplateVersionsKey 
            FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE TemplateVersionItemsKey = @ItemId)

                     SELECT CONVERT(VARCHAR, i.TemplateVersionItemsKey) AS Id, 
                    CONVERT(VARCHAR, COALESCE(i.ParentTemplateVersionItemsKey, 0)) AS ParentId, 
                    0 AS CommentCount, i.ItemTypesKey AS ItemType,
                    COALESCE(i.MustImplement, CONVERT(BIT, 0)) AS Required, 
                     COALESCE(NULLIF(i.VisibleText, ''), i.LongText) AS TitleHtml, 
                                         CASE WHEN EXISTS (SELECT * FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE ParentTemplateVersionItemsKey = i.TemplateVersionItemsKey) 
                                         THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END AS HasItems, 
                                         CONVERT(VARCHAR, @Versionskey) AS VersionKey, CASE WHEN i.visible = 1 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END AS Hidden,
                     COALESCE(i.Condition, '') AS Condition
                FROM dbo.SSPX_TemplateVersionItems i (NOLOCK)
                    INNER JOIN dbo.ListOfItemTypes t (NOLOCK) ON i.ItemTypesKey = t.ItemTypesKey
                WHERE i.Active = 1 AND i.TemplateVersionsKey = @Versionskey and i.DeprecatedFlag =0
                ORDER BY COALESCE(i.ParentTemplateVersionItemsKey, 0), i.SortOrder
              END
        ELSE IF @ItemType = 'C'
              BEGIN
            SET @Versionskey = @ItemId

                     SELECT CONVERT(VARCHAR, i.TemplateVersionItemsKey) AS Id, 
                    CONVERT(VARCHAR, COALESCE(i.ParentTemplateVersionItemsKey, 0)) AS ParentId, 
                    0 AS CommentCount, i.ItemTypesKey AS ItemType,
                    COALESCE(i.MustImplement, CONVERT(BIT, 0)) AS Required, 
                     COALESCE(NULLIF(i.VisibleText, ''), i.LongText) AS TitleHtml, 
                                         CASE WHEN EXISTS (SELECT * FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE ParentTemplateVersionItemsKey = i.TemplateVersionItemsKey) 
                                         THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END AS HasItems, 
                                         CONVERT(VARCHAR, @Versionskey) AS VersionKey, CASE WHEN i.visible = 1 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END AS Hidden,
                     COALESCE(i.Condition, '') AS Condition
                FROM dbo.SSPX_TemplateVersionItems i (NOLOCK)
                    INNER JOIN dbo.ListOfItemTypes t (NOLOCK) ON i.ItemTypesKey = t.ItemTypesKey
                WHERE i.Active = 1 AND i.TemplateVersionsKey = @Versionskey and i.DeprecatedFlag =0
                ORDER BY COALESCE(i.ParentTemplateVersionItemsKey, 0), i.SortOrder
              END
        ELSE IF @ItemType = 'P'
              BEGIN
                     SELECT CONVERT(VARCHAR, i.TemplateVersionItemsKey) AS Id, 
                    CONVERT(VARCHAR, COALESCE(i.ParentTemplateVersionItemsKey, 0)) AS ParentId, 
                    0 AS CommentCount, i.ItemTypesKey AS ItemType,
                    COALESCE(i.MustImplement, CONVERT(BIT, 0)) AS Required, 
                     COALESCE(NULLIF(i.VisibleText, ''), i.LongText) AS TitleHtml, 
                                         CASE WHEN EXISTS (SELECT * FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE ParentTemplateVersionItemsKey = i.TemplateVersionItemsKey) 
                                         THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END AS HasItems, 
                                         CONVERT(VARCHAR,  i.TemplateVersionsKey) AS VersionKey, CASE WHEN i.visible = 1 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END AS Hidden,
                     COALESCE(i.Condition, '') AS Condition
                FROM dbo.SSPX_TemplateVersionItems i (NOLOCK)
                    INNER JOIN dbo.ListOfItemTypes t (NOLOCK) ON i.ItemTypesKey = t.ItemTypesKey
                WHERE i.Active = 1 AND i.TemplateVersionsKey in (SELECT v.TemplateVersionsKey 
            FROM dbo.SSPX_ProtocolVersions p (NOLOCK)
            INNER JOIN dbo.SSPX_TemplateVersions v (NOLOCK) ON p.ProtocolVersionsKey = v.ProtocolVersionsKey 
            WHERE p.Protocolskey = @ItemId AND v.Active = 1) and i.DeprecatedFlag =0
                ORDER BY i.TemplateVersionsKey,COALESCE(i.ParentTemplateVersionItemsKey, 0), i.SortOrder            
              END

        ";

        private static String _getChecklistAuthors = @"
            SELECT u.UsersKey, u.FirstName, COALESCE(u.MiddleName, '') AS MiddleName, u.LastName, COALESCE(u.Qualifications, '') AS Qualification, l.Role
            FROM dbo.SSPX_ProtocolVersionUserRoles a (NOLOCK)
	            INNER JOIN dbo.SSPX_Users u (NOLOCK) on a.UsersKey = u.UsersKey
	            INNER JOIN dbo.SSPX_ListOfRoles l (NOLOCK) ON a.RolesKey = l.Roleskey
            WHERE a.ProtocolVersionsKey = @VersionCkey AND u.Active = 1
            ORDER BY l.RolesKey, u.LastName
        ";

        private static String _getChecklistExternalVersions = @"
            SELECT CONVERT(VARCHAR, ExternalVersionKey) AS EVKey, CONVERT(VARCHAR, ExternalVersionKey) AS ExternalVersion
            FROM dbo.SSPX_ProtocolVersionsExternalVersionsMM (NOLOCK)
            WHERE ProtocolVersionsKey = @VersionCkey
        ";

        private static String _getAllUsers = @"
            SELECT u.UsersKey, u.FirstName, COALESCE(u.MiddleName, '') AS MiddleName, u.LastName, COALESCE(u.Qualifications, '') AS Qualification, '' AS Role
            FROM dbo.SSPX_Users u (NOLOCK)
            WHERE u.Active = 1
            ORDER BY u.LastName
        ";

        private static String _getAllBasedOn = @"
            SELECT CONVERT(VARCHAR, ExternalVersionKey) AS EVKey, CASE WHEN OrganizationName = 'FIGO' THEN ExternalVersion 
	            ELSE OrganizationName + ' ' + ExternalVersion END AS ExternalVersion
            FROM dbo.ListOfExternalVersions (NOLOCK)
            WHERE Active = 1
            ORDER BY OrganizationName, ExternalVersion
        ";

        private static String _deleteAllAuthors = @"
            DECLARE @Roleskey INT = (SELECT Roleskey FROM dbo.SSPX_ListOfRoles (NOLOCK) WHERE Role = @Role)

            DELETE FROM dbo.SSPX_ProtocolVersionUserRoles 
            WHERE RolesKey = @Roleskey AND ProtocolVersionsKey = @VersionCkey
        ";

        private static String _saveAuthors = @"
            DECLARE @Roleskey INT = (SELECT Roleskey FROM dbo.SSPX_ListOfRoles (NOLOCK) WHERE Role = @Role)
            DECLARE @UserInRole TABLE (
	            Authorskey INT,
	            ProtocolAuthorKey INT,
	            Action INT
            )
            DECLARE @User TABLE (
	            AuthorCkey INT
            )

            INSERT INTO @User (AuthorCkey)
            SELECT variablestring FROM dbo.StringSplit(@UserIds, ',', 100)

            INSERT INTO @UserInRole (Authorskey, ProtocolAuthorKey, Action)
            SELECT a.UsersKey, a.ProtocolVersionUserRoleKey, 2
            FROM dbo.SSPX_ProtocolVersionUserRoles a (NOLOCK)
            WHERE a.RolesKey = @Roleskey AND a.ProtocolVersionsKey = @VersionCkey

            UPDATE u SET Action = 1
            FROM @UserInRole u WHERE Authorskey IN (SELECT AuthorCkey FROM @User)

            -- add users
            INSERT INTO dbo.SSPX_ProtocolVersionUserRoles (ProtocolVersionsKey, RolesKey, UsersKey, CreatedBy, CreatedDt, LastUpdated, LastUpdatedDt)
            SELECT @VersionCkey, @Roleskey, AuthorCkey, @PersonCkey, GETDATE(), @PersonCkey, GETDATE()
            FROM @User WHERE AuthorCkey NOT IN (SELECT Authorskey FROM @UserInRole)

            -- remove users
            DELETE FROM dbo.SSPX_ProtocolVersionUserRoles 
            WHERE ProtocolVersionUserRoleKey IN (SELECT ProtocolAuthorKey FROM @UserInRole WHERE Action = 2)
        ";

        #endregion
    }
}