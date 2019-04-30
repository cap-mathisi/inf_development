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
    public class ProtocolController : Controller
    {
        private readonly IRepository<Protocol> _protocolRepository;

        public ProtocolController(IRepository<Protocol> protocolRepository)
        {
            _protocolRepository = protocolRepository;
        }

        public IActionResult Index()
        {
            var protocols = _protocolRepository.List();
            return View(protocols);
        }

        public IActionResult Load()
        {
            int recordsAdded = DatabasePopulator.LoadProtocols(_protocolRepository);
            return Ok(recordsAdded);
        }

        public IActionResult Edit()
        {
            var protocols = _protocolRepository.List();
            return View(protocols);
        }
    }
}