﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricount.DAL.EntityConfigurations.Abstract;
using Tricount.Entities.Abstract;
using Tricount.Entities.Concrete;

namespace Tricount.DAL.EntityConfigurations.Concrete
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Iban).HasMaxLength(26);
            builder.HasIndex(u => u.Iban).IsUnique();
            builder.HasIndex(u => u.Slug).IsUnique();
            builder.Property(u => u.Name).HasMaxLength(40);
            builder.Property(u => u.Surname).HasMaxLength(40);
        }
    }
}
