using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csnp.Notification.Infrastructure.Persistence.Configurations;

public class EmailLogConfiguration : IEntityTypeConfiguration<EmailLogEntity>
{
    #region -- Methods --

    public void Configure(EntityTypeBuilder<EmailLogEntity> builder)
    {
        builder.ToTable("EmailLogs");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.To).IsRequired().HasMaxLength(256);
        builder.Property(e => e.Subject).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Body).IsRequired();
        builder.Property(e => e.SentAt).IsRequired();
        builder.Property(e => e.IsSuccess).IsRequired();
        builder.Property(e => e.ErrorMessage).HasMaxLength(1000);
        builder.Property(e => e.CorrelationId).HasMaxLength(100);
    }

    #endregion
}
