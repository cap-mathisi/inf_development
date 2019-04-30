using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Filters;
using sspx.web.ApiModels;

namespace sspx.web.Api
{
    [Route("api/[controller]")]
    [ValidateModel]
    public class ProtocolsController : Controller
    {
        private readonly ICkeyRepository<Protocol> _protocolRepository;

        public ProtocolsController(ICkeyRepository<Protocol> protocolRepository)
        {
            _protocolRepository = protocolRepository;
        }

        // GET: api/Protocols
        [HttpGet]
        public IActionResult List()
        {
            var protocols = _protocolRepository.List()
                            .Select(protocol => ProtocolDTO.FromProtocol(protocol));
            return Ok(protocols);
        }

        // GET: api/Protocols
        [HttpGet("{protocolId:decimal}")]
        public IActionResult GetByCkey(decimal id)
        {
            var protocol = ProtocolDTO.FromProtocol(_protocolRepository.GetByCkey(id));
            return Ok(protocol);
        }

        // POST: api/Protocols
        [HttpPost]
        public IActionResult Post([FromBody] ProtocolDTO protocolDTO)
        {
            var protocol = new Protocol()
            {
                ProtocolId = protocolDTO.ProtocolId,
                ProtocolName = protocolDTO.ProtocolName
            };
            _protocolRepository.Add(protocol);
            return Ok(ProtocolDTO.FromProtocol(protocol));
        }

        [HttpPatch("{id:decimal}/draft")]
        public IActionResult Draft(decimal id)
        {
            var protocol = _protocolRepository.GetByCkey(id);
            protocol.DraftReady();
            _protocolRepository.Update(protocol);

            return Ok(ProtocolDTO.FromProtocol(protocol));
        }
    }
}