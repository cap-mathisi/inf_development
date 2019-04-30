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
    public class UserController : Controller
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var users = _userRepository.List();
            return View(users);
        }

        public IActionResult Load()
        {
            int recordsAdded = DatabasePopulator.LoadUsers(_userRepository);
            return Ok(recordsAdded);
        }
    }
}