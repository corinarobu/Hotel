﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.DTOs
{
    public class BankAccountAddDTO
    {
        [Required]
        public string IBAN { get; set; } =string.Empty;
        [Required]
        public string BankName {  get; set; } = string.Empty;
    }
}
