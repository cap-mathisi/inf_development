using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sspx.infra.config;
using sspx.infra.data;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Models;
using sspx.web.Helpers;
using sspx.web.Services;
using Microsoft.AspNetCore.Authorization;

namespace sspx.web.Controllers
{
    public class NotesController : Controller
    {
        private readonly ISSPxConfig _config;
        private String _userKey;
        private String _fullName;

        public NotesController(ISSPxConfig config)
        {
            _config = config;
        }

        public JsonResult SSPxItemNoteTitles(String pid)
        {
            var notes = DBHelper.GetNoteTitles(_config.SSPxConnectionString, pid);
            return Json(notes);
        }

        public JsonResult SSPxItemNoteAdd(String pid, String nid)
        {
            GetUserData();
            DBHelper.AddNote(_config.SSPxConnectionString, nid, _userKey);
            var notes = DBHelper.GetNoteTitles(_config.SSPxConnectionString, pid);
            return Json(notes);
        }

        public JsonResult SSPxItemNoteMove(String pid, String id, String nid)
        {
            GetUserData();
            DBHelper.MoveNote(_config.SSPxConnectionString, id, nid, _userKey);
            var notes = DBHelper.GetNoteTitles(_config.SSPxConnectionString, pid);
            return Json(notes);
        }

        public JsonResult SSPxItemNoteDelete(String pid, String nid)
        {
            GetUserData();
            DBHelper.DeleteNote(_config.SSPxConnectionString, nid, _userKey);
            var notes = DBHelper.GetNoteTitles(_config.SSPxConnectionString, pid);
            return Json(notes);
        }

        public JsonResult SSPxItemNoteCopy(String pid, String nid)
        {
            GetUserData();
            DBHelper.CopyNote(_config.SSPxConnectionString, nid, _userKey);
            var notes = DBHelper.GetNoteTitles(_config.SSPxConnectionString, pid);
            return Json(notes);
        }

        #region private variable

        private IProtocolObject DBHelper = new SSPxEditorDBHelper();

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
    }
}
