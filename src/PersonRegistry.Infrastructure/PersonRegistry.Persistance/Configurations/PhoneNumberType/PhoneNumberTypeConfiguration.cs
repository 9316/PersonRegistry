using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonRegistry.Domain.Schema;

namespace PersonRegistry.Persistence.Configurations.PhoneNumberType;


/// <summary>
/// Configures the entity mapping for the <see cref="Domain.Aggregates.PhoneNumberType.PhoneNumberType"/> entity.
/// </summary>
public class PhoneNumberTypeConfiguration : IEntityTypeConfiguration<Domain.Aggregates.PhoneNumberType.PhoneNumberType>
{
    /// <summary>
    /// Configures the entity properties and query filters for PhoneNumberType.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Domain.Aggregates.PhoneNumberType.PhoneNumberType> builder)
    {
        builder.ToTable("PhoneNumberTypes", TableSchema.PERSON);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}