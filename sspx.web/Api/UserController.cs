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
    public class UserController : Controller
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult List()
        {
            var users = _userRepository.List()
                            .Select(user => UserDTO.FromUser(user));
            return Ok(users);
        }

        // GET: api/Users
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var user = UserDTO.FromUser(_userRepository.GetById(id));
            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO protocolDTO)
        {
            var user = new User()
            {
                UserId = protocolDTO.UserId,
                FirstName = protocolDTO.FirstName
            };
            _userRepository.Add(user);
            return Ok(UserDTO.FromUser(user));
        }

        [HttpPatch("{id:int}/authenticate")]
        public IActionResult Authenticate(int id, string password)
        {
            var user = _userRepository.GetById(id);
            user.Authenticate(password);
            _userRepository.Update(user);

            return Ok(UserDTO.FromUser(user));
        }
    }
}