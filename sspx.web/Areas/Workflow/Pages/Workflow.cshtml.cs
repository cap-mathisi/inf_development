    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Models;
using sspx.web.Services;
using System.Text.Encodings.Web;
using sspx.web.Helpers;

namespace sspx.web.Areas.Workflow.Pages
{
    public class WorkflowModel : PageModel
    {

        private IProtocolversionstatus _protocolVersionsStates;
        private String _userKey;
        private String _fullName;
        private int _protocolversionkey;
        public WorkflowModel(IProtocolversionstatus protocolVersionsStates)
        {
            _protocolVersionsStates = protocolVersionsStates;
            
        }

        
        [BindProperty]

        public WorkflowModelInputModel WorkflowModelInput { get; set; } = new WorkflowModelInputModel();
        public class WorkflowModelInputModel
        {
            [BindProperty]
            public int ProtocolVersionsKey { get; set; } = DefaultValue.Key;
            [BindProperty]
            public int ProtocolKey { get; set; } = DefaultValue.Key;

            public List<ProtocolVersionsStates> ProtocolVersionsStates { get; set; }
        }




        public IActionResult OnGet(int ProtocolKey)
        {
            GetUserData();
            WorkflowModelInput.ProtocolVersionsStates = _protocolVersionsStates.GetProtocolVersionsStates(_protocolversionkey);
            return Page();
        }

        

        #region private method

        private void GetUserData()
        {
            if (HttpContext.Session.Get<int?>("userKey") != null)
            {
                _userKey = HttpContext.Session.Get<int>("userKey").ToString();
                _fullName = HttpContext.Session.Get<string>("userFullName");
                _protocolversionkey = HttpContext.Session.Get<int>("protocolVersionKey");                
                if (_protocolversionkey == 0)
                { _protocolversionkey = 141; }
            }
        }

        #endregion
    }
}