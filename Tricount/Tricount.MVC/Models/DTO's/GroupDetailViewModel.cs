﻿using Tricount.Entities.Concrete;
using Tricount.MVC.Models.DTO_s.;
using Tricount.MVC.Models.DTO_s.Group;

namespace Tricount.MVC.Models.DTO_s
{
	public class GroupDetailViewModel
	{
		public ExpenseCreateDTO? ExpenseDTO { get; set; }
		public ExpenseDetail? ExpenseDetail { get; set; }
        public GroupCreateDTO? GroupDTO { get; set; }
        public Group? Group { get; set; }
		public Payment payment { get; set; }
        public Invite? Invite { get; set; }
        public ICollection<Payment> Payments { get; set; }
        


    }
}
