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
<<<<<<< HEAD
    public class SqlDbContext : IdentityDbContext
=======
    public class SqlDbContext : IdentityDbContext<User>
>>>>>>> ercan
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ExpenseDetail> ExpenseDetails { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public SqlDbContext()
        {
            
        }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

<<<<<<< HEAD
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Tricount1;Trusted_Connection=True;");
=======
            optionsBuilder.UseSqlServer(@"Data Source=MERTCAN\MSSQLSERVER01;Database=Tricount2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
>>>>>>> ercan
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
