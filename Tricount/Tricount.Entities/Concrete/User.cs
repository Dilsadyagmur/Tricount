﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricount.Entities.Abstract;

namespace Tricount.Entities.Concrete
{
    public class User : IdentityUser
    {
<<<<<<< HEAD
        public string? TcNo { get; set; }
=======
        public string? TCNO { get; set; }
>>>>>>> Test
        public CurrencyEnum DefaultCurrency { get; set; }
        public ICollection<Group>? Groups { get; set; }
        public ICollection<Expense>? Expenses { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
