using Cryptography.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cryptography.Infrastructure.Repositories.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("BIGINT").UseIdentityColumn();
        builder.Property(x => x.UserDocument).HasColumnType("NVARCHAR(MAX)");
        builder.Property(x => x.CreditCardToken).HasColumnType("NVARCHAR(MAX)");
        builder.Property(x => x.Value).HasColumnType("BIGINT");
    }
}