using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using sspx.infra.config;
using sspx.infra.data;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Models;
using sspx.web.Helpers;
using sspx.web.Services;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;

namespace sspx.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISSPxConfig _config;
        private readonly IProtocolPermissions _protocolPermissions;
        private readonly IHostingEnvironment _hostingEnvironment;
        List<ChecklistItem> results = new List<ChecklistItem>();
        //int firstTime = 0;
        List<ExplanatoryNote> notes = new List<ExplanatoryNote>();
        private String _userKey;
        private String _fullName;

        public HomeController(ISSPxConfig config, IProtocolPermissions protocolPermissions, IHostingEnvironment hostingEnvironment)
        {
            _config = config;
            _protocolPermissions = protocolPermissions;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Error()
        {
            return View();
        }

        #region Output

        public IActionResult SSPxBuildPdf(String pid)
        {
            return View("SSPxBuildPdf", pid);
        }

        public IActionResult SSPxBuildMSWord(String pid)
        {
            return View("SSPxBuildMSWord", pid);
        }
        /// <summary>
        /// Create PDF File to download
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="base64"></param>
        /// <returns></returns>
        // based on https://docs.telerik.com/kendo-ui/framework/save-files/introduction
        [HttpPost]
        public IActionResult SSPxOutputPdf(String contentType, String base64)
        {
            var fileContents = Convert.FromBase64String(base64);
            var response = new FileContentResult(fileContents, "application/octet-stream");
            response.FileDownloadName = "Protocol.pdf";
            return response;
        }

        public IActionResult SSPxOutputWord1(String contentType, String base64)
        {
            var fileContents = Encoding.ASCII.GetBytes(contentType);
            return File(fileContents, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Protocoldoc.docx");
        }
        /// <summary>
        /// Create MS Word File to download
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="base64"></param>
        /// <returns></returns>
        public IActionResult SSPxOutputWord(String contentType, String base64)
        {
            MemoryStream ms;
            MainDocumentPart mainPart;
            Body body;
            Document doc;
            AlternativeFormatImportPart chunk;
            AltChunk altChunk;
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string basedirectory = AppContext.BaseDirectory;
            string documentname = "\\Protocolword" + ".docx";

            string altChunkID = "AltChunkId";
            ms = new MemoryStream();
            using (WordprocessingDocument myDoc = WordprocessingDocument.Create(webRootPath + "\\Protocolword.docx", WordprocessingDocumentType.Document))
            {
                mainPart = myDoc.MainDocumentPart;
                if (mainPart == null)
                {
                    mainPart = myDoc.AddMainDocumentPart();
                    body = new Body();
                    doc = new Document(body);
                    doc.Save(mainPart);
                }
                chunk = mainPart.AddAlternativeFormatImportPart(AlternativeFormatImportPartType.Xhtml, altChunkID);
                using (Stream chunkStream = chunk.GetStream(FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter stringStream = new StreamWriter(chunkStream))
                    {
                        stringStream.Write(contentType);
                    }
                }
                altChunk = new AltChunk();
                altChunk.Id = altChunkID;
                mainPart.Document.Body.InsertAt(altChunk, 0);
                mainPart.Document.Save();
            }
            return PhysicalFile(webRootPath + "\\Protocolword.docx", "application/ms-word", "Protocolword.docx");

        }

        public IActionResult SSPxDownloadPdf(String contentType, String base64)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(base64);
            var response = new FileContentResult(fileBytes, "application/octet-stream");
            response.FileDownloadName = "Protocol.pdf";
            return response;
        }


        #endregion

        #region Protocol Detail

        [Authorize]
        public IActionResult SSPxProtocol(String pid = "7")
        {
            int commentCount = 0;
            // using local DBHelper calls
            GetUserData();

            bool hasEditPermission = _protocolPermissions.HasPermission(Convert.ToInt32(_userKey), Convert.ToInt32(pid), ProtocolPermissionTypes.EditProtocol);
            if (hasEditPermission == false)
            {
                return RedirectToPage("/Index", new { area = "Dashboard" });
            }
            Int32 protocolVersionKey = DBHelper.GetProtocolVersion(_config.SSPxConnectionString, pid);
            var resultSet = DBHelper.GetProtocolVersionCommentsForAll(_config.SSPxConnectionString, protocolVersionKey.ToString());
            // commentsCalculation(resultSet);
            //SSP 136 - Get totoal number of count for a protocol
            for (int i = 0; i < resultSet.Count; i++)
            {
                if (resultSet[i].roleKey == Convert.ToInt32(RoleTypes.Author))
                    commentCount += 1;
                else if (resultSet[i].roleKey == Convert.ToInt32(RoleTypes.Reviewer))
                    commentCount += 1;
            }

            //SSP 136 - Get totoal number of count for a protocol
            ChecklistNoteViewModel protocol = new ChecklistNoteViewModel
            {
                protocolCkey = pid,
                comments = DBHelper.GetProtocolVersionCommentsForAll(_config.SSPxConnectionString, protocolVersionKey.ToString()),
                commentsCount = commentCount,
                notes = DBHelper.GetNotes(_config.SSPxConnectionString, pid),
                allUsers = DBHelper.GetAllUsers(_config.SSPxConnectionString),
                standards = DBHelper.GetStandards(_config.SSPxConnectionString),
                dataTypes = AnswerDataType,
                dataUnits = AnswerUnit,
                protocolVersionKey = protocolVersionKey
            };
            return View(protocol);
        }

        [Authorize]
        public IActionResult SSPxProtocol_responsive(String pid = "7")
        {
            // using local DBHelper calls
            GetUserData();

            bool hasEditPermission = _protocolPermissions.HasPermission(Convert.ToInt32(_userKey), Convert.ToInt32(pid), ProtocolPermissionTypes.EditProtocol);
            if (hasEditPermission == false)
            {
                return RedirectToPage("/Index", new { area = "Dashboard" });
            }
            Int32 protocolVersionKey = DBHelper.GetProtocolVersion(_config.SSPxConnectionString, pid);
            ChecklistNoteViewModel protocol = new ChecklistNoteViewModel
            {
                protocolCkey = pid,
                comments = DBHelper.GetProtocolVersionComments(_config.SSPxConnectionString, protocolVersionKey.ToString(), "1_F"),
                notes = DBHelper.GetNotes(_config.SSPxConnectionString, pid),
                allUsers = DBHelper.GetAllUsers(_config.SSPxConnectionString),
                standards = DBHelper.GetStandards(_config.SSPxConnectionString),
                dataTypes = AnswerDataType,
                dataUnits = AnswerUnit,
                protocolVersionKey = protocolVersionKey
            };
            return View(protocol);
        }

        [Authorize]
        public IActionResult SSPxCompareVersions(String pid)
        {
            return View();
        }

        [Authorize]
        public IActionResult SSPxImportItems(String pid)
        {
            return View();
        }

        //public JsonResult SSPxItem(String id)
        //{
        //    ChecklistItem item = DBHelper.GetChecklistItem(_config.SSPxConnectionString, id);
        //    item.notes = DBHelper.GetNoteKeys(_config.SSPxConnectionString, id);
        //    return Json(item);
        //}

        //Zira Id SSP-99
        /// <summary>
        /// Get Case Summary child and sub child items
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult SSPxItem(String id, String pid)
        {
            ChecklistItem item = new ChecklistItem();
            List<ChecklistItem> items = new List<ChecklistItem>();
            string treeViewData = "";
            if (id.IndexOf("_C") > 1)
            {
                var versions = DBHelper.GetCheckVersion(_config.SSPxConnectionString, id);
                treeViewData = Treeview(id, "", 0, 0, 0);
                item.Items = JsonConvert.DeserializeObject<List<ChecklistItem>>(treeViewData);
                item.notes = notes;
                item.protocolName = versions[0].protocolName;

            }
            else if (id.IndexOf("_I") > 1)
            {
                item.Items = DBHelper.GetAllNodes(_config.SSPxConnectionString, id);
                item.notes = DBHelper.GetNoteKeys(_config.SSPxConnectionString, id);
            }
            else if (id.IndexOf("_F") > 0)
            {
                var versions = DBHelper.GetChecklistVersion(_config.SSPxConnectionString, pid);
                foreach (var protocolVersion in versions)
                {
                    item = new ChecklistItem();
                    treeViewData = Treeview(protocolVersion.key + "_C", "", 0, 0, 0);
                    item.Items = JsonConvert.DeserializeObject<List<ChecklistItem>>(treeViewData);
                    item.notes = notes;
                    item.protocolName = protocolVersion.protocolName;
                    items.Add(item);
                }
                return Json(items);
            }
            else
            {
                item.Items = JsonConvert.DeserializeObject<List<ChecklistItem>>(treeViewData);
                item.notes = DBHelper.GetNoteKeys(_config.SSPxConnectionString, id);
            }
            return Json(item);
        }
        //Zira Id SSP-99

        [HttpPost]
        public JsonResult SSPxItem(String id, [FromBody] ChecklistItem item)
        {
            try
            {
                item.key = id.Replace("_I", string.Empty);
                String result = DBHelper.UpdateChecklistItem(_config.SSPxConnectionString, item);
                result += DBHelper.UpdateChecklistItemNotes(_config.SSPxConnectionString, item.key, item.noteKeys);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} update item':'success'}}", item.key));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        // GET: SSPX ChecklistItem Children
        public JsonResult SSPxItemChildren(String pid, String view = "protocol", String parentId = "", String id = "")
        {
            try
            {
                String nodeId = String.IsNullOrWhiteSpace(parentId) ? id : parentId;
                if (nodeId == "")
                {
                    var versions = DBHelper.GetChecklistVersion(_config.SSPxConnectionString, pid);
                    //Added the below lines to Show Title, Cover and Author comments count in badge icon
                    var versionsCommentsSummary = DBHelper.GetChecklistVersionComments(_config.SSPxConnectionString, pid);
                    StringBuilder nodes = new StringBuilder("[{\"id\":\"1_F\",\"parentId\":\"\",\"value\":\"TITLE\",\"titleHtml\":\"TITLE\",\"hasItems\":false},{\"id\":\"2_F\",\"parentId\":\"\",\"value\":\"COVER\",\"titleHtml\":\"COVER\",\"hasItems\":false},{\"id\":\"3_F\",\"parentId\":\"\",\"value\":\"AUTHORS\",\"titleHtml\":\"AUTHORS\",\"hasItems\":false},");
                    if (versions.Count == 1)
                    {
                        nodes.AppendFormat("{{\"id\":\"{0}_C\",\"parentId\":\"\",\"value\":\"CASE SUMMARY ({1})\",\"titleHtml\":\"CASE SUMMARY ({1})\",\"hasItems\":true}},", versions[0].key, versions[0].protocolName);
                    }
                    else
                    {
                        nodes.Append("{\"id\":\"4_F\",\"parentId\":\"\",\"value\":\"CASE SUMMARY\",\"titleHtml\":\"CASE SUMMARY\",\"hasItems\":true},");
                    }
                    if (view == "full")
                    {
                        nodes.Append("{\"id\":\"5_F\",\"parentId\":\"\",\"value\":\"NOTES\",\"titleHtml\":\"NOTES\",\"hasItems\":false},{\"id\":\"6_F\",\"parentId\":\"\",\"value\":\"REFERENCES\",\"titleHtml\":\"REFERENCES\",\"hasItems\":false}]");
                    }
                    //Added the below lines to Show Title, Cover and Author comments count in badge icon
                    if (versionsCommentsSummary.Count > 0)
                    {
                        foreach (var comment in versionsCommentsSummary)
                        {
                            if (comment.Commenttype.TrimEnd() == "Title")
                            {
                                nodes.Replace("\"TITLE\",\"hasItems\":false", "\"TITLE\",\"hasItems\":false,\"commentCount\":" + comment.CommentCount);
                            }
                            else if (comment.Commenttype.TrimEnd() == "Cover")
                            {
                                nodes.Replace("\"COVER\",\"hasItems\":false", "\"COVER\",\"hasItems\":false,\"commentCount\":" + comment.CommentCount);
                            }
                            else if (comment.Commenttype.TrimEnd() == "Authors")
                            {
                                nodes.Replace("\"AUTHORS\",\"hasItems\":false", "\"AUTHORS\",\"hasItems\":false,\"commentCount\":" + comment.CommentCount);
                            }
                            //else if (comment.Commenttype.TrimEnd() == "Case Summary")
                            //{
                            //    nodes.Replace(",\"hasItems\":true},", ",\"hasItems\":true},\"commentCount\":" + comment.CommentCount+",");
                            //}
                        }
                    }
                    return Json(JsonConvert.DeserializeObject(nodes.ToString()));
                }
                else if (nodeId == "4_F")
                {
                    var versions = DBHelper.GetChecklistVersion(_config.SSPxConnectionString, pid);
                    StringBuilder nodes = new StringBuilder("[");
                    foreach (var version in versions)
                    {
                        nodes.AppendFormat("{{\"id\":\"{0}_C\",\"parentId\":\"4_F\",\"value\":\"CASE SUMMARY ({1})\",\"titleHtml\":\"CASE SUMMARY ({1})\",\"hasItems\":true}},", version.key, version.protocolName);
                    }
                    nodes.Append("]");
                    return Json(JsonConvert.DeserializeObject(nodes.ToString()));
                }
                var results = DBHelper.GetItemChildren(_config.SSPxConnectionString, nodeId);
                var items = new List<ChecklistItemNode>();
                foreach (var item in results)
                {
                    items.Add(item.ToChecklistItemNode(nodeId));
                }
                return Json(items);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

        }

        [Authorize]
        public ActionResult SSPxProtocolNote(String pid = "27", String noteNo = "")
        {
            GetUserData();
            var noteData = noteNo == String.Empty ? DBHelper.GetNoteTitles(_config.SSPxConnectionString, pid)
                : DBHelper.GetNotes(_config.SSPxConnectionString, pid);
            ChecklistNoteViewModel notes = new ChecklistNoteViewModel
            {
                protocolCkey = pid,
                notes = noteData,
                nodeCkey = noteNo
            };
            return View(notes);
        }

        [Authorize]
        public ActionResult SSPxProtocolReader(String vid = "7")
        {
            int commentCount = 0;
            GetUserData();
            bool hasViewPermission = _protocolPermissions.HasPermission(Convert.ToInt32(_userKey), Convert.ToInt32(vid), ProtocolPermissionTypes.View);
            if (hasViewPermission == false)
            {
                return RedirectToPage("/Index", new { area = "Dashboard" });
            }
            Int32 protocolVersionKey = DBHelper.GetProtocolVersion(_config.SSPxConnectionString, vid);
            //SSP 136 - Get totoal number of count for a protocol
            var resultSet = DBHelper.GetProtocolVersionCommentsForAll(_config.SSPxConnectionString, protocolVersionKey.ToString());
            for (int i = 0; i < resultSet.Count; i++)
            {
                if (resultSet[i].roleKey == Convert.ToInt32(RoleTypes.Author))
                    commentCount += 1;
                else if (resultSet[i].roleKey == Convert.ToInt32(RoleTypes.Reviewer))
                    commentCount += 1;
            }
            //SSP 136 - Get totoal number of count for a protocol
            ChecklistNoteViewModel protocolReader = new ChecklistNoteViewModel
            {
                protocolCkey = vid,
                //comments = DBHelper.GetProtocolVersionComments(_config.SSPxConnectionString, protocolVersionKey.ToString(), "1_F"),
                comments = DBHelper.GetProtocolVersionCommentsForAll(_config.SSPxConnectionString, protocolVersionKey.ToString()),
                commentsCount = commentCount,
                protocolVersionKey = protocolVersionKey
            };
            return View(protocolReader);
        }

        public ActionResult SSPxProtocolPreview(String nid, String view = "casesummary")
        {
            GetUserData();
            string[] views = view.Split("/");
            string[] protocolId = nid.Split("_");

            ChecklistPreviewViewModel preview = new ChecklistPreviewViewModel
            {
                nodeCkey = nid,
                view = view.ToLower()
            };
            if (nid.EndsWith("_P"))
            {
                preview.version = DBHelper.GetFullChecklistVersion(_config.SSPxConnectionString, nid.Replace("_P", String.Empty), 0);
            }
            else
            {
                preview.version = DBHelper.GetFullChecklistVersion(_config.SSPxConnectionString, preview.versionCkey, 1);
            }
            if (views[0].Equals("full", StringComparison.OrdinalIgnoreCase))
            {
                ChecklistItemNode itemNode;
                var versions = DBHelper.GetChecklistVersion(_config.SSPxConnectionString, protocolId[0] + "_C");
                preview.items = DBHelper.GetChecklistVersionItems(_config.SSPxConnectionString, nid);
                foreach (var data in versions)
                {
                    itemNode = new ChecklistItemNode();
                    itemNode.titleHtml = "<span><b>" + "CASE SUMMARY:(" + data.protocolName + ")" + "</b></span>";
                    int index = preview.items.FindIndex(a => a.version == data.key);
                    itemNode.@checked = preview.items[index].@checked;
                    itemNode.hasItems = preview.items[index].hasItems;
                    itemNode.hidden = preview.items[index].hidden;
                    itemNode.required = preview.items[index].required;
                    itemNode.version = preview.items[index].version;
                    itemNode.parentId = preview.items[index].parentId;

                    preview.items.Insert(index, itemNode);
                }
                if (preview.items.Count > 0)
                {
                    preview.versionCkey = preview.items[0].version;
                }
                if (nid.EndsWith("_P"))
                {
                    preview.notes = DBHelper.GetNotesForVersion(_config.SSPxConnectionString, preview.version.key);
                    preview.references = DBHelper.GetNoteReferencesForVersion(_config.SSPxConnectionString, preview.version.key);
                }
                else
                {
                    preview.notes = DBHelper.GetNotesForVersion(_config.SSPxConnectionString, preview.versionCkey);
                    preview.references = DBHelper.GetNoteReferencesForVersion(_config.SSPxConnectionString, preview.versionCkey);
                }
            }
            if (views.Count() > 1) 
            {
                //if (views[1] == "pdf")
                //{
                ViewBag.ProtocolView = views[1];
                //}

            }
            ViewBag.ProtocolId = protocolId[0];
            return View(preview);
        }

        public JsonResult SSPxNote(String pid, String noteid)
        {
            // using local DBHelper calls
            ChecklistNoteViewModel notes = new ChecklistNoteViewModel
            {
                protocolCkey = pid,
                notes = DBHelper.GetNotes(_config.SSPxConnectionString, pid, noteid),
                comments = DBHelper.GetNoteComments(_config.SSPxConnectionString, pid, noteid),
                references = DBHelper.GetNoteReferences(_config.SSPxConnectionString, pid, noteid)
            };
            return Json(notes);
        }

        public JsonResult SSPxVersion(String pid)
        {
            // using local DBHelper calls
            ChecklistVersion version = DBHelper.GetFullChecklistVersion(_config.SSPxConnectionString, pid, 0);
            return Json(version);
        }

        // POST: Save note
        [HttpPost]
        public JsonResult SSPxProtocolSaveNote(String id, String note, String noteTitle, String protocolCkey, String refOrder)
        {
            try
            {
                GetUserData();
                String result = DBHelper.SaveNotes(_config.SSPxConnectionString, id, note, noteTitle, protocolCkey, refOrder, _userKey);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} insert item':'success'}}", id));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult SSPxProtocolInsertItem(String id, [FromQuery] Int32 insertType,
            [FromQuery] Int32 itemTypeKey, [FromQuery] Int32 itemCount)
        {
            try
            {
                GetUserData();
                String result = DBHelper.InsertChecklistItem(_config.SSPxConnectionString, id, insertType, itemTypeKey, itemCount);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} insert item':'success'}}", id));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult SSPxProtocolSwitchItem(String id, [FromQuery] String withNodeId)
        {
            try
            {
                String result = DBHelper.SwitchChecklistItem(_config.SSPxConnectionString, id, withNodeId);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} switch item':'success'}}", id));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult SSPxProtocolDeleteItem(String id)
        {
            try
            {
                String result = DBHelper.DeleteChecklistItem(_config.SSPxConnectionString, id);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} delete item':'success'}}", id));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult SSPxProtocolDeleteItemComment(Int32 id, String itemKey)
        {
            try
            {
                String result = DBHelper.DeleteProtocolComment(_config.SSPxConnectionString, id, itemKey);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} delete item comment':'success'}}", id));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult SSPxProtocolGetComment(String pid, String id)
        {
            var comments = DBHelper.GetProtocolVersionComments(_config.SSPxConnectionString, pid, id);
            return Json(comments);
        }

        public JsonResult SSPxCopyItemNode(String id, String pid, Boolean isCut, Boolean itemOnly, Boolean asChild)
        {
            try
            {
                String result = DBHelper.CopyItemNode(_config.SSPxConnectionString, id, pid, isCut, itemOnly, asChild);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} copy':'success'}}", id));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult SSPxRemoveNoteReference(String id)
        {
            try
            {
                GetUserData();
                String result = DBHelper.RemoveNoteReference(_config.SSPxConnectionString, id, _userKey);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} removed':'success'}}", id));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        // POST: Save note reference
        [HttpPost]
        public JsonResult SSPxSaveNoteReference(String id, String noteCkey, String reference)
        {
            try
            {
                GetUserData();
                String result = DBHelper.SaveNoteReference(_config.SSPxConnectionString, id, noteCkey, reference, _userKey);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} saved':'success'}}", id));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        // POST: Save note comment
        [HttpPost]
        public JsonResult SSPxSaveNoteComment(String id, String noteCkey, String comment)
        {
            try
            {
                GetUserData();
                String result = DBHelper.SaveNoteComment(_config.SSPxConnectionString, id, noteCkey, comment, _userKey);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} saved':'success'}}", id));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult SSPxRemoveNoteComment(String id)
        {
            try
            {
                GetUserData();
                String result = DBHelper.RemoveNoteComment(_config.SSPxConnectionString, id, _userKey);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} removed':'success'}}", id));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        // POST: Save protocol comment
        [HttpPost]
        public JsonResult SSPxProtocolSaveComment(String id, String comment, Int32 commentId, String protocolVersionkey)
        {
            try
            {
                GetUserData();
                String result = DBHelper.SaveProtocolComment(_config.SSPxConnectionString, commentId, comment, id, protocolVersionkey, _userKey);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} saved':'success'}}", id));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        // POST: Save protocol version
        [HttpPost]
        public JsonResult SSPxSaveVersion(String id, String name, String description, String basedOnKey)
        {
            try
            {
                GetUserData();
                String result = DBHelper.SaveProtocolVersion(_config.SSPxConnectionString, name, description, basedOnKey, id, _userKey);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} saved':'success'}}", id));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        // POST: Save protocol version cover
        [HttpPost]
        public JsonResult SSPxSaveCover(String id, String detail)
        {
            try
            {
                GetUserData();
                String result = DBHelper.SaveCover(_config.SSPxConnectionString, detail, id, _userKey);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'{0} saved':'success'}}", id));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        // POST: Save protocol version authors
        [HttpPost]
        public JsonResult SSPxSaveAuthors(String id, String role, String userIds)
        {
            try
            {
                GetUserData();
                String result = DBHelper.SaveAuthors(_config.SSPxConnectionString, id, role, userIds, _userKey);
                if (result == String.Empty)
                {
                    return Json(String.Format("{{'saved':'{0} success'}}", role));
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        // POST: Save item hidden flags
        [HttpPost]
        public JsonResult SSPxSaveItemHiddenFlag(String id, String hideIds, String showIds)
        {
            try
            {
                String result = DBHelper.SaveItemHiddenFlag(_config.SSPxConnectionString, id, hideIds, showIds);
                if (result == String.Empty)
                {
                    return Json("{'flags saved':'success'}");
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        [HttpGet]
        public ActionResult ImportCommentVersions(int ProtocolsKey, int WorkingProtocolVersionKey)
        {
            List<VersionComments> commentVersions = DBHelper.GetAllVersionsComment(_config.SSPxConnectionString, ProtocolsKey, WorkingProtocolVersionKey);
            List<string> protocolversions = commentVersions.Select(X => X.Version).Distinct().ToList();


            //var myList = protocolversions.Where(item => idsOnly.Contains(item.ID.Value))
            //         .Select(a => a.Title).ToList();

            List<ImportComments> lstImportcomments = new List<ImportComments>();
            foreach (var j in protocolversions)
            {
                ImportComments objImportComments = new ImportComments();
                List<Models.Reviewer> lstReviewers = new List<Models.Reviewer>();
                //Models.Reviewer objreviewer1 = new Models.Reviewer();
                //objreviewer1.ReviewerID = 000;
                //objreviewer1.ReviewerName = "All";
                //lstReviewers.Add(objreviewer1);
                foreach (var i in commentVersions)
                {
                    if (j == i.Version)
                    {
                        objImportComments.Version = i.Version;
                        objImportComments.ProtocolVersionsKey = i.ProtocolVersionsKey;
                        Models.Reviewer objreviewer = new Models.Reviewer();
                        objreviewer.ReviewerID = i.ReviewerID;
                        objreviewer.ReviewerName = i.ReviewerName;
                        objreviewer.ReviewerComments = i.CommentsCount;
                        objImportComments.Comments = objImportComments.Comments + i.CommentsCount;
                        lstReviewers.Add(objreviewer);
                    }
                }
                objImportComments.Reviewers = lstReviewers;
                lstImportcomments.Add(objImportComments);
            }
            ViewBag.WorkingProtocolVersionKey = WorkingProtocolVersionKey;
            return PartialView("_ImportComments", lstImportcomments);
        }

        [HttpPost]
        public JsonResult ImportCommentToCurrentVersion(int protocolVersionKey, String userID, int WorkingProtocolVersionKey)
        {
            string result = null;
            DBHelper.ImportCommentToCurrentVersion(_config.SSPxConnectionString, protocolVersionKey, userID, WorkingProtocolVersionKey);
            if (result == String.Empty)
            {
                return Json("{'flags saved':'success'}");
            }
            return Json(result);
        }



        #endregion

        #region private method

        private void GetUserData()
        {
            if (HttpContext.Session.Get<int?>("userKey") != null)
            {
                _userKey = HttpContext.Session.Get<int>("userKey").ToString();
                _fullName = HttpContext.Session.Get<string>("userFullName");
            }
            ViewBag.UserFullName = _fullName;
        }

        #endregion

        #region private variable

        private IProtocolObject DBHelper = new SSPxEditorDBHelper();

        #endregion

        #region private constants

        private string[,] AnswerUnit =
        {
            { "0", "" },
            { "2", "T" },
            { "3", "mm^2" },
            { "4", "mm" },
            { "5", "signals/cell" },
            { "6", "signals" },
            { "7", "cm" },
            { "8", "cells" },
            { "10", "mitotic figures" },
            { "11", "kbp" },
            { "12", "%" },
            { "13", "ng/mL" },
            { "14", "IU" },
            { "15", "g" },
            { "16", "hours" },
            { "17", "milligram/liter" },
            { "18", "copies/cell" },
            { "19", "kg" },
            { "20", "min" },
            { "21", "grams" },
            { "22", "ml" },
            { "23", "per mm2" }
        };

        private string[,] AnswerDataType =
        {
            { "0", "" },
            { "2", "Numeric-Integer" },
            { "4", "String" },
            { "9", "Numeric" },
            { "16", "DateTime" },
            { "17", "Custom" }
        };

        #endregion

        #region Case Summary Tree View Binding
        //Zira Id SSP-98
        /// <summary>
        /// Case Summary Tree View Binding
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="mystr"></param>
        /// <param name="j"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string Treeview(string itemID, string mystr, int j, int flag, int first)
        {
            List<ChecklistItem> querylist = new List<ChecklistItem>();

            int mainNode = 0;
            int childquantity = 0;
            int myflag;
            // var ctx = new TreeviewEntities();
            if (first == 0)
            {
                results = new List<ChecklistItem>();
                notes = new List<ExplanatoryNote>();
                if (itemID.IndexOf("_C") > 1)
                {
                    results = DBHelper.GetAllNodes(_config.SSPxConnectionString, itemID).ToList();
                }
                else if (itemID.IndexOf("_I") > 1)
                {
                    results = DBHelper.GetAllNodes(_config.SSPxConnectionString, itemID).ToList();
                }
            }

            first++;
            if (flag == 0)
            {
                querylist = (from m in results
                             where string.IsNullOrEmpty(m.parentid)
                             select m).ToList();
                mainNode = querylist.Count;

                mystr += "[";
            }
            if (flag == 1)
            {

                querylist = (from m in results
                             where m.parentid == itemID
                             select m).ToList();
                mainNode = querylist.Count;
                mystr += ",items:[";
            }

            //Below line shows an example of how to make parent node with his child
            //[{ id: "1", text: "P1", items: [{ id: "5", text: "P2" }] }]

            int i = 1;
            foreach (var item in querylist)
            {
                myflag = 0;
                foreach (var note in item.notes)
                {
                    notes.Add(note);
                }

                mystr += "{key:\"" + item.key +
                "\",text:\"" + item.text.Replace('"', ' ').Trim() + "\",hasItems:\"" + item.hasItems + "\",parentId:\"" + item.parentid + "\",itemType:\"" + item.itemType + "\", longText:\"" + item.longText.Replace('"', ' ').Trim() + "\",itemType:\"" + item.itemType +
                "\",comments:\"" + item.comments.Replace('"', ' ').Trim() + "\",condition:\"" + item.condition + "\",required:\"" + item.required + "\",reportText:\"" + item.reportText +
                "\",answerDataTypeKey:\"" + item.answerDataTypeKey + "\",answerUnits:\"" + item.answerUnits + "\",answerMaxValue:\"" + item.answerMaxValue +
                "\",answerMinValue:\"" + item.answerMinValue + "\",answerMaxRepsv:\"" + item.answerUnits + "\",answerMinReps:\"" + item.answerMinReps + "\" ";
                List<ChecklistItem> querylistParent = new List<ChecklistItem>();
                //Check this parent has child or not , if yes how many?
                querylistParent = (from m in results
                                   where m.parentid == item.key
                                   select m).ToList();
                childquantity = querylistParent.Count;
                //If Parent Has Child again call Treeview with new parameter
                if (childquantity > 0)
                {
                    mystr = Treeview(item.key, mystr, i, 1, first);
                }
                //No Child and No Last Node
                else if (childquantity == 0 && i < querylist.Count)
                {
                    mystr += "},";
                }
                //No Child and Last Node
                else if (childquantity == 0 && i == querylist.Count)
                {
                    int fcheck2 = 0;
                    int fcheck3 = 0;
                    int counter = 0;
                    int flagbreak = 0;

                    string currentparent;
                    List<ChecklistItem> parentquery;
                    List<ChecklistItem> childlistquery;
                    TempData["counter"] = 0;
                    currentparent = item.parentid;
                    string coun;
                    while (!string.IsNullOrEmpty(currentparent))
                    {
                        //count parent of parent

                        fcheck2 = 0;
                        fcheck3 = 0;
                        parentquery = new List<ChecklistItem>();
                        parentquery = (from m in results
                                       where m.key == currentparent
                                       select m).ToList();

                        var rep2 = (from h in parentquery
                                    select new { h.parentid }).First();

                        //put {[ up to end

                        //list of child
                        childlistquery = new List<ChecklistItem>();
                        childlistquery = (from m in results
                                          where m.parentid == currentparent
                                          select m).ToList();

                        foreach (var item22 in childlistquery)
                        {
                            if (mystr.Contains(item22.key.ToString()))
                            {

                                if (item22.parentid == currentparent)
                                {
                                    fcheck3 += 1;
                                    if (fcheck3 == 1)
                                    {
                                        counter += 1;
                                    }
                                }
                            }
                            else
                            {
                                myflag = 1;
                                if (item22.parentid == currentparent)
                                {
                                    fcheck2 += 1;
                                    if (fcheck2 == 1)
                                    {
                                        counter -= 1;
                                        flagbreak = 1;
                                    }
                                }
                            }
                        }

                        var result55 =
                        (from h in parentquery select new { h.key }).First();
                        coun = result55.key;
                        TempData["coun"] = Convert.ToString(coun);
                        currentparent = rep2.parentid;
                        if (flagbreak == 1)
                        {
                            break;
                        }
                    }

                    for (int i2 = 0; i2 < counter; i2++)
                    {
                        mystr += "}]";
                    }

                    List<ChecklistItem> lastchild = new List<ChecklistItem>();
                    lastchild = (from m in results
                                 where m.parentid == item.parentid
                                 select m).ToList();

                    List<ChecklistItem> lastparent = new List<ChecklistItem>();
                    lastparent = (from m in results
                                  where m.parentid == null
                                  select m).ToList();

                    if (lastchild.Count > 0)
                    {
                        var result_lastchild =
                        (from h in lastchild select new { h.key }).Last();
                        var result_lastparent =
                        (from h in lastparent select new { h.key }).Last();
                        string mycount = Convert.ToString(TempData["coun"]);
                        if (item.key == result_lastchild.key &&
                        mycount == result_lastparent.key && myflag == 0)
                        {
                            mystr += "}]";
                        }
                        else if (item.key == result_lastchild.key &&
                        mycount == result_lastparent.key && myflag == 1)
                        {
                            mystr += "},";
                        }
                        else if (item.key == result_lastchild.key &&
                        mycount != result_lastparent.key)
                        {
                            mystr += "},";
                        }
                    }
                    //  finish }]
                    else if (lastchild.Count == 0 && item.parentid == null)
                    {
                        mystr += "}]";
                    }
                }
                i++;
            }

            return mystr;
        }
        //Jira Id SSP-98
        #endregion
    }
}
