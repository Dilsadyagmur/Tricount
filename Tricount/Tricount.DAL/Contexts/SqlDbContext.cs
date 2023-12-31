﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tricount.Entities.Concrete;

namespace Tricount.DAL.Contexts
{
    public class SqlDbContext : IdentityDbContext<User>
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ExpenseDetail> expenseDetails { get; set; }
        public DbSet<Invite> Invites { get; set; }

        public SqlDbContext()
        {
            
        }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server=tcp:dilsadygmr.database.windows.net,1433;Initial Catalog=dilsad;Persist Security Info=False;User ID=admindilsad;Password=Dilsad123-123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
