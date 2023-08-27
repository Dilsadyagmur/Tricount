﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricount.DAL.EntityConfigurations.Abstract;
using Tricount.Entities.Concrete;

namespace Tricount.DAL.EntityConfigurations.Concrete
{
    public class GroupConfig: BaseConfig<Group>
    {
        public override void Configure(EntityTypeBuilder<Group> builder)
        {
            base.Configure(builder);
            builder.Property(g => g.Name).HasMaxLength(50);
            builder.Property(g => g.Description).HasMaxLength(100);           
            builder.HasIndex(g => g.Slug).IsUnique();
        }
    }
}
