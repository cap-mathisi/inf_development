using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using sspx.core.entities;

namespace sspx.web.ApiModels
{
    public class UserDTO
    {
        [Required]
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public bool IsAuthenticated { get; private set; }

        public static UserDTO FromUser(User user)
        {
            return new UserDTO()
            {
                UserId = user.UserId,
                FirstName = user.FirstName
            };
        }
    }
}
