using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entities;

namespace WebApi.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.id);

            builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

            builder.Property(x => x.Password)
            .HasColumnName("password")
            .IsRequired();

            builder
            .HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasConstraintName("FK_Users_Role")
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}