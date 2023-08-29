﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricount.Entities.Abstract;

namespace Tricount.DAL.EntityConfigurations.Abstract
{
    public abstract class BaseConfig<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasDefaultValue(Guid.NewGuid().ToString());
            builder.HasIndex(t => t.Id).IsUnique();
            builder.Property(t => t.UpdateDate).HasDefaultValue(DateTime.Now);
            builder.Property(t => t.CreateDate).HasDefaultValue(DateTime.Now);
        }
    }
}
