﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricount.Entities.Abstract;

namespace Tricount.Entities.Concrete
{
    public class Payment : BaseEntity
    {
        public double Amount { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid ExpenseId { get; set; }
        public Expense? Expense { get; set; }
    }
}
