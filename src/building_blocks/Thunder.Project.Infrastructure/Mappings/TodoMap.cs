using Thunder.Project.Domain.Entities;
using Esterdigi.Core.Lib.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Thunder.Project.Infrastructure.Mappings
{
    public class TodoMap : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> entity)
        {
            entity.ToTable("todos");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Title).IsRequired().HasMaxLength(70).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Description).IsRequired().HasMaxLength(100).HasColumnType(Constants.Varchar);

            entity.Property(x => x.Status).IsRequired().HasColumnType(Constants.SmallInt);

            entity.Property(x => x.CreatedAt).HasColumnType(Constants.DateTime);
            entity.Property(x => x.LastUpdatedAt).HasColumnType(Constants.DateTime);

            entity.Ignore(x => x.Notifications);

        }
    }
}