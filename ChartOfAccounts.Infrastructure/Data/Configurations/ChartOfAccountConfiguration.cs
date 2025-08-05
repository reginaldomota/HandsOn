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
        builder.HasIndex(x => x.Type);
        builder.HasIndex(x => x.ParentCode);

        builder.Property(x => x.Code)
               .IsRequired()
               .HasMaxLength(255);

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

        builder.Property(x => x.CreatedAt)
               .IsRequired();

        builder.Property(x => x.UpdatedAt)
               .IsRequired();

        // Level é uma coluna gerada automaticamente no SQL e não precisa ser mapeada se for gerenciada só no banco
        builder.Ignore(x => x.Level);
    }
}
