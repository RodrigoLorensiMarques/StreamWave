using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entities;

namespace WebApi.Data.Mappings
{
    public class VideoMap : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.ToTable("videos");
            builder.HasKey(x => x.id);

            builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("VARCHAR")
            .HasMaxLength(250)
            .IsRequired();

            builder.Property(x => x.DateAdd)
            .HasColumnName("date_add")
            .HasColumnType("DATETIME2")
            .IsRequired();
        }
    }
}