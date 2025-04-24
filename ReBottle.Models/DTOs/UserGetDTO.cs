using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models.DTOs
{
    public class UserGetDTO
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public bool IsActive { get; set; }
    }
}
