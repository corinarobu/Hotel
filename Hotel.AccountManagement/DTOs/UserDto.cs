﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.DTOs
{
    public class UserDto
    {
        [Required]
        public string Username {  get; set; }=string.Empty;
        [Required]
        public string Email {  get; set; }=string.Empty;
        [Required]
        public string Token {  get; set; }=string.Empty;
    }
}
