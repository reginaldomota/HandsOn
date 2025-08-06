using ChartOfAccounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChartOfAccounts.Infrastructure.Data.Configurations;

public class ChartOfAccountConfiguration : IEntityTypeConfiguration<ChartOfAccount>
{
    public void Configure(EntityTypeBuilder<ChartOfAccount> builder)
    {
        builder.ToTable("ChartOfAccounts");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Code).IsUnique();
        builder.HasIndex(x => x.CodeNormalized);
        builder.HasIndex(x => x.Type);
        builder.HasIndex(x => x.ParentCode);
        builder.HasIndex(x => x.TenantId); 

        builder.Property(x => x.Code)
               .IsRequired()
               .HasMaxLength(25);

        builder.Property(x => x.CodeNormalized)
               .IsRequired()
               .HasMaxLength(25);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.Type)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.IsPostable)
               .IsRequired();

        builder.Property(x => x.ParentCode)
               .HasMaxLength(255);

        builder.Property(x => x.Level)
               .HasComputedColumnSql();

        builder.Property(x => x.CreatedAt)
               .IsRequired();

        builder.Property(x => x.UpdatedAt)
               .IsRequired();

        builder.Property(x => x.IdempotencyKey)
               .IsRequired();

        builder.Property(x => x.RequestId)
               .IsRequired()
               .HasMaxLength(32);

        builder.Property(x => x.TenantId)
               .IsRequired();
    }
}
