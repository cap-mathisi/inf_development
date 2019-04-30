using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sspx.core;
using sspx.core.entities;
using sspx.core.interfaces;

namespace sspx.web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository<Protocol> _protocolRepository;

        public AdminController(IRepository<Protocol> protocolRepository)
        {
            _protocolRepository = protocolRepository;
        }

        public IActionResult Index()
        {
            var items = _protocolRepository.List();
            return View(items);
        }

        public IActionResult Edit()
        {
            var items = _protocolRepository.List();
            return View(items);
        }
    }
}