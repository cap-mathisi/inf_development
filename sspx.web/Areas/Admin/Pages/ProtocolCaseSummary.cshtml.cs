using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.Areas.Admin.data;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using sspx.web.Services;

namespace sspx.Areas.Admin.Pages
{
    public class ProtocolCaseSummaryModel : PageModel
    {
        private IProtocolWithGroupRepository _protocolWithGroupRepository;
        private IProtocolVersionRepository _protocolVersionRepository;
        //private IProtocolObject _DBHelper;
        private String _userKey;
        private String _fullName;
        List<ChecklistItem> results = new List<ChecklistItem>();
        int firstTime = 0;

        public string Title { get; set; } = "Create Protocol - Case Summary";
        public SearchModel ProtocolsWithGroupsSearchModel { get; set; }

        [BindProperty]
        public int ProtocolVersionKey { get; set; } = DefaultValue.Key;
        [BindProperty]
        public int ProtocolKeyForCurrentVersion { get; set; } = DefaultValue.Key;
        [BindProperty]
        public int FromProtocol { get; set; } = DefaultValue.Key;
        [BindProperty]
        public int CopyProtocolKey { get; set; } = DefaultValue.Key;
        [BindProperty]
        public int CopyProtocolParentItemid { get; set; } = DefaultValue.Key;
        [BindProperty]
        public string version { get; set; } 

        [BindProperty]
        public int TemplateVersionsKey { get; set; } = DefaultValue.Key;
        
        public bool EditMode { get; set; } = false;

        #region User Inputs

        [BindProperty]
        [Display(Name = "Protocol")]
        public int ProtocolKey { get; set; }

        [BindProperty]
        public string CaseSummaryIDs { get; set; }

        [BindProperty]
        public string ProtocolName { get; set; }
        [BindProperty]
        [Display(Name = "Protocol Name")]
        [Range(1, int.MaxValue, ErrorMessage = "Protocol is required")]
        public int ProtocolGroupKey { get; set; }

        #endregion

        [BindProperty]
        public List<ProtocolWithGroup> ProtocolsWithGroupsForDropDown { get; set; }

        [BindProperty]
        public List<ChecklistItem> ProtocolCheckList { get; set; }
        [BindProperty]

        [TempData]
        public string StatusMessage { get; set; }

        public ProtocolCaseSummaryModel(IProtocolWithGroupRepository protocolWithGroupRepository, IProtocolVersionRepository protocolVersionRepository)
        {
            _protocolWithGroupRepository = protocolWithGroupRepository;
            _protocolVersionRepository = protocolVersionRepository;
            //_DBHelper = DBHelper;
        }


        public IActionResult OnGet(int protocolVersionKey)
        {
            SetUpPage(protocolVersionKey);

            var protocolsWithGroups = _protocolWithGroupRepository.List();
            var pageUrl = Url.Page("Protocol", new { protocolKey = "" });
            ProtocolKeyForCurrentVersion = protocolVersionKey;
            ProtocolsWithGroupsSearchModel = SearchModel.FromProtocolsWithGroups(pageUrl, protocolsWithGroups);
            HttpContext.Session.Set("protocolsWithGroupsSearchModel", ProtocolsWithGroupsSearchModel);


            var protocolWithGroup = new ProtocolWithGroup()
            {
                ProtocolKey = this.ProtocolKey
            };
            //var latestProtocolVersion = _protocolVersionRepository.GetLatestVersionForProtocol(protocolWithGroup.ProtocolKey);
            var latestProtocolVersion = _protocolVersionRepository.GetLatestVersionForProtocol(52);
            var sample = _protocolVersionRepository.GetTemplateVersion(latestProtocolVersion.ProtocolVersionKey);
            ProtocolCheckList = _protocolVersionRepository.GetCaseSummary(sample);

            // TODO CS2:

            return Page();
        }

        //public JsonResult OnGet()
        //{
        //    return new JsonResult(_carService.ReadAll());
        //}   

        public IActionResult OnPostSave()
        {
            SetUpPage(ProtocolVersionKey);

            if (ModelState.IsValid == false)
            {
                return Page();
            }

            // TODO CS2: 
            // save data (ProtocolVersionKey and CaseSummaryIDs)

            // TODO CS2:
            // return RedirectToPage("ProtocolNotes", new { protocolVersionKey = ProtocolVersionKey });
            return Page();
        }


        public IActionResult OnPostSaveCaseSummary([FromServices] IHostingEnvironment hostingEnvironment)
        {
            try
            {
                GetUserData();
                //String result = _protocolVersionRepository.InsertProtocolCaseSummarylistItem(CopyProtocolKey, ProtocolKeyForCurrentVersion, CopyProtocolParentItemid=0, TemplateVersionsKey, version="");
                String result = _protocolVersionRepository.InsertProtocolCaseSummarylistItem(CopyProtocolKey, ProtocolKeyForCurrentVersion, CopyProtocolParentItemid = 0, TemplateVersionsKey);
                if (result == String.Empty)
                {
                    return new JsonResult(String.Format("{{'{0} insert item':'success'}}", CopyProtocolParentItemid));
                }
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
            // return new JsonResult("test");
        }


        public JsonResult OnGetDropdownChange(string ProtocolKey)
        {
            try
            {
                var jsonItemCaseSummary = "";
                var treeViewData = "";
                List<ChecklistItem> items = new List<ChecklistItem>();
                var versions = _protocolVersionRepository.GetChecklistVersion(ProtocolKey);               
                if (versions.Count == 1)
                {                    
                    foreach (var itemsummary in versions)
                    {
                        firstTime = 0;
                        jsonItemCaseSummary = "[{ \"id\":\"itemparentid\",\"parentId\":\"\",\"value\":\"CASE SUMMARY (" + itemsummary.protocolName + ")\",\"titleHtml\":\"CASE SUMMARY (" + itemsummary.protocolName + ")\",\"hasItems\":true,\"items\": [";
                        //    var LatestProtocolVersion = _protocolVersionRepository.GetLatestVersionForProtocol(Convert.ToInt32(ProtocolKey));
                        //var ProtocolTemplateVersion = _protocolVersionRepository.GetTemplateVersion(LatestProtocolVersion.ProtocolVersionKey);
                        //var ProtocolText = (LatestProtocolVersion != null) ? LatestProtocolVersion.ProtocolVersionText : string.Empty;
                        treeViewData = Treeview(itemsummary.key+"_C", "", 0, 0);
                        jsonItemCaseSummary = jsonItemCaseSummary + treeViewData + "}]";
                    }
                        //ProtocolCheckList = _protocolVersionRepository.GetCaseSummary(ProtocolTemplateVersion);
                        var JsonList = new JsonResult(jsonItemCaseSummary);
                        return JsonList;
                    
                    

                    //if (ProtocolCheckList == null)
                    //{
                    //    jsonItemCaseSummary = "[{ \"id\":\"itemparentid\",\"parentId\":\"\",\"value\":\"CASE SUMMARY (" + ProtocolText + ")\",\"titleHtml\":\"CASE SUMMARY (" + ProtocolText + ")\",\"hasItems\":true}]";
                    //    var JsonList = new JsonResult(jsonItemCaseSummary);
                    //}
                    //else
                    //{
                    //    jsonItemCaseSummary = "[{ \"id\":\"itemparentid\",\"parentId\":\"\",\"value\":\"CASE SUMMARY (" + ProtocolText + ")\",\"titleHtml\":\"CASE SUMMARY (" + ProtocolText + ")\",\"hasItems\":true,\"items\": [";
                    //    jsonItemCaseSummary = jsonItemCaseSummary.Replace("itemparentid", ProtocolCheckList[0].versionKey);
                    //    foreach (var item in ProtocolCheckList)
                    //    {
                    //        jsonItemCaseSummary = jsonItemCaseSummary + "{ \"titleHtml\": \"" + item.longText + "\",\"hasItems\":" + item.hasItems + " },";
                    //        jsonItemCaseSummary = jsonItemCaseSummary.Replace("itemparentid", item.versionKey);
                    //        jsonItemCaseSummary = jsonItemCaseSummary.Replace("\t", " ");
                    //    }
                    //    jsonItemCaseSummary = jsonItemCaseSummary.Substring(0, jsonItemCaseSummary.Length - 1);
                    //    jsonItemCaseSummary = jsonItemCaseSummary + "]}]";
                    //    var JsonList = new JsonResult(jsonItemCaseSummary);

                    //    return JsonList;
                    //}
                }
                return  new JsonResult(jsonItemCaseSummary);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        public JsonResult OnGetSingleCaseSummary(string ProtocolKey)
        {

            var jsonItemCaseSummary = "";
            string treeViewData = "";
            firstTime = 0;
            ChecklistItem item = new ChecklistItem();
            List<ChecklistItem> items = new List<ChecklistItem>();
            var versions = _protocolVersionRepository.GetChecklistVersion(ProtocolKey);
            if (versions != null)
            {
                if (versions.Count == 1)
                {
                    foreach (var itemsummary in versions)
                    {
                        //treeViewData = Treeview(itemsummary.key + "_C", "", 0, 0);
                        firstTime = 0;
                        jsonItemCaseSummary = "[{ \"key\":\"" + itemsummary.key + "\",\"parentId\":\"\",\"text\":\"CASE SUMMARY (" + itemsummary.protocolName + ")\",\"longText\":\"CASE SUMMARY (" + itemsummary.protocolName + ")\",\"hasItems\":true,\"items\": ";
                        treeViewData = Treeview(itemsummary.key + "_C", "", 0, 0);
                        jsonItemCaseSummary = jsonItemCaseSummary + treeViewData + "}]";
                    }
                    //item.Items = JsonConvert.DeserializeObject<List<ChecklistItem>>(treeViewData);
                    jsonItemCaseSummary = jsonItemCaseSummary.Replace("\t", " ");
                    jsonItemCaseSummary = jsonItemCaseSummary.Replace("\t", " ");
                    jsonItemCaseSummary = jsonItemCaseSummary.Replace("\n", " ");
                    jsonItemCaseSummary = jsonItemCaseSummary.Replace("\r", " ");
                    //jsonItemCaseSummary = jsonItemCaseSummary.Replace("\"", " ");
                    jsonItemCaseSummary = jsonItemCaseSummary.Replace("True", "true");
                    jsonItemCaseSummary = jsonItemCaseSummary.Replace("False", "false");
                    var JsonList = new JsonResult(jsonItemCaseSummary);
                    return JsonList;

                }
                else //if (versions.Count > 1)
                {
                    jsonItemCaseSummary = "[";
                    foreach (var itemsummary in versions)
                    {   
                        firstTime = 0;
                        jsonItemCaseSummary = jsonItemCaseSummary + "{ \"key\":\"" + itemsummary.key + "\",\"parentId\":\"\",\"text\":\"CASE SUMMARY (" + itemsummary.protocolName + ")\",\"longText\":\"CASE SUMMARY (" + itemsummary.protocolName + ")\",\"hasItems\":true,\"items\": ";
                        treeViewData = Treeview(itemsummary.key + "_C", "", 0, 0);
                        jsonItemCaseSummary = jsonItemCaseSummary + treeViewData + "},";
                    }
                    jsonItemCaseSummary = jsonItemCaseSummary.Replace("\t", " ");
                    jsonItemCaseSummary = jsonItemCaseSummary.Replace("\n", " ");
                    jsonItemCaseSummary = jsonItemCaseSummary.Replace("\r", " ");
                    //jsonItemCaseSummary = jsonItemCaseSummary.Replace("\"", " ");
                    jsonItemCaseSummary = jsonItemCaseSummary.Substring(0, jsonItemCaseSummary.Length - 1);
                    jsonItemCaseSummary = jsonItemCaseSummary.Replace("True", "true");
                    jsonItemCaseSummary = jsonItemCaseSummary.Replace("False", "false");
                    jsonItemCaseSummary = jsonItemCaseSummary + "]";
                    //jsonItemCaseSummary = jsonItemCaseSummary + "}]";
                    //item.Items = JsonConvert.DeserializeObject<List<ChecklistItem>>(treeViewData);
                    var JsonList = new JsonResult(jsonItemCaseSummary);
                    return JsonList;
                }
            }
            return new JsonResult(jsonItemCaseSummary);

            //try
            //{
            //    var LatestProtocolVersion = _protocolVersionRepository.GetLatestVersionForProtocol(Convert.ToInt32(ProtocolKey));
            //    var ProtocolTemplateVersion = _protocolVersionRepository.GetTemplateVersion(LatestProtocolVersion.ProtocolVersionKey);
            //    ProtocolCheckList = _protocolVersionRepository.GetCaseSummary(ProtocolTemplateVersion);
            //    var JsonList = new JsonResult(ProtocolCheckList);
            //    return JsonList;
            //}
            //catch (Exception ex)
            //{
            //    return new JsonResult(ex);
            //}
        }
        public JsonResult OnGetMultiplesCaseSummary(string ProtocolKey)
        {
            try
            {
                var versions = _protocolVersionRepository.GetChecklistVersion(ProtocolKey);
                var jsonItemMultipleCaseSummary = "[";
                if (versions.Count > 0)
                {
                    foreach (var itemsummary in versions)
                    {
                        jsonItemMultipleCaseSummary = jsonItemMultipleCaseSummary + "{ \"id\":\"" + itemsummary.key + "_C\",\"parentId\":\"\",\"value\":\"CASE SUMMARY (" + itemsummary.protocolName + ")\",\"titleHtml\":\"CASE SUMMARY (" + itemsummary.protocolName + ")\",\"hasItems\":true,\"items\": [";
                        var results = _protocolVersionRepository.GetItemChildren(itemsummary.key + "_C");
                        var items = new List<ChecklistItemNode>();
                        foreach (var item in results)
                        {
                            jsonItemMultipleCaseSummary = jsonItemMultipleCaseSummary + "{ \"titleHtml\": \"" + item.longText + "\" },";
                            jsonItemMultipleCaseSummary = jsonItemMultipleCaseSummary.Replace("itemparentid", item.versionKey);
                            //items.Add(item.ToChecklistItemNode(itemsummary.key));
                        }
                        jsonItemMultipleCaseSummary = jsonItemMultipleCaseSummary.Substring(0, jsonItemMultipleCaseSummary.Length - 1);
                        jsonItemMultipleCaseSummary = jsonItemMultipleCaseSummary + "]},";
                        //return Json(items);
                        //var jsonItemMultipleCaseSummary = "[{ \"id\":\"itemparentid\",\"parentId\":\"\",\"value\":\"CASE SUMMARY (test)\",\"titleHtml\":\"CASE SUMMARY test)\",\"hasItems\":true,\"items\": ["
                        //nodes.AppendFormat("{{\"id\":\"{0}_C\",\"parentId\":\"4_F\",\"value\":\"CASE SUMMARY ({1})\",\"titleHtml\":\"CASE SUMMARY ({1})\",\"hasItems\":true}},", version.key, version.protocolName);
                    }
                    jsonItemMultipleCaseSummary = jsonItemMultipleCaseSummary.Substring(0, jsonItemMultipleCaseSummary.Length - 1);
                    jsonItemMultipleCaseSummary = jsonItemMultipleCaseSummary + "]";
                }
                var JsonList = new JsonResult(jsonItemMultipleCaseSummary);
                return JsonList;
               
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        public JsonResult OnGetItemChildern(string NodeId)
        {
            try {
                var results = _protocolVersionRepository.GetItemChildren(NodeId);
                var items = new List<ChecklistItemNode>();
                foreach (var item in results)
                {
                    items.Add(item.ToChecklistItemNode(NodeId));
                }
                //return Json(items);
                var LatestProtocolVersion = _protocolVersionRepository.GetLatestVersionForProtocol(Convert.ToInt32(ProtocolKey));
                var ProtocolTemplateVersion = _protocolVersionRepository.GetTemplateVersion(LatestProtocolVersion.ProtocolVersionKey);
                ProtocolCheckList = _protocolVersionRepository.GetCaseSummary(ProtocolTemplateVersion);
                var JsonList = new JsonResult(ProtocolCheckList);
                return JsonList;
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        private void SetUpPage(int protocolVersionKey)
        {
            ProtocolsWithGroupsForDropDown = _protocolWithGroupRepository.List();

            // TODO CS2:

            // TODO CS2:
            //if (protocolVersionKey > 0)
            //{
            //    EditMode = true;
            //    Title = $"Edit Protocol - Case Summary";
            //}
        }

        #region private method

        private void GetUserData()
        {
            if (HttpContext.Session.Get<int?>("userKey") != null)
            {
                _userKey = HttpContext.Session.Get<int>("userKey").ToString();
                _fullName = HttpContext.Session.Get<string>("userFullName");
            }
            //ViewBag.UserFullName = _fullName;
        }

        public string Treeview(string itemID, string mystr, int j, int flag)
        {
            List<ChecklistItem> querylist = new List<ChecklistItem>();

            int mainNode = 0;
            int childquantity = 0;
            int myflag;
            // var ctx = new TreeviewEntities();
            if (firstTime == 0)
            {
                if (itemID.IndexOf("_C") > 1)
                {
                    results = _protocolVersionRepository.GetAllNodes(itemID);
                }
                else if (itemID.IndexOf("_I") > 1)
                {
                    results = _protocolVersionRepository.GetAllNodes(itemID);
                }
            }

            firstTime++;
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
                mystr += ",\"items\":[";
            }

            //Below line shows an example of how to make parent node with his child
            //[{ id: "1", text: "P1", items: [{ id: "5", text: "P2" }] }]

            int i = 1;
            foreach (var item in querylist)
            {
                myflag = 0;
                mystr += "{\"key\":\"" + item.key +
                "\",\"text\":\"" + item.text.Replace('"', ' ').Trim() + "\",\"hasItems\":" + ((item.hasItems==0) ? false : true) + ",\"parentId\":\"" + item.parentid + "\",\"itemType\":\"" + item.itemType + "\", \"longText\":\"" + item.longText.Replace('"', ' ').Trim() + "\",\"comments\":\"" + item.comments.Replace('"', ' ').Trim() + "\",\"condition\":\"" + item.condition + "\",\"required\":\"" + item.required + "\",\"reportText\":\"" + item.reportText +
                "\",\"answerDataTypeKey\":\"" + item.answerDataTypeKey + "\",\"answerUnits\":\"" + item.answerUnits + "\",\"answerMaxValue\":\"" + item.answerMaxValue +
                "\",\"answerMinValue\":\"" + item.answerMinValue + "\",\"answerMaxRepsv\":\"" + item.answerUnits + "\",\"answerMinReps\":\"" + item.answerMinReps + "\" ";
                List<ChecklistItem> querylistParent = new List<ChecklistItem>();
                //Check this parent has child or not , if yes how many?
                querylistParent = (from m in results
                                   where m.parentid == item.key
                                   select m).ToList();
                childquantity = querylistParent.Count;
                //If Parent Has Child again call Treeview with new parameter
                if (childquantity > 0)
                {
                    mystr = Treeview(item.key, mystr, i, 1);
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

        #endregion
    }
}