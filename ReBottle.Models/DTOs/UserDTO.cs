﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Models.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? IsActive { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
    }
}
