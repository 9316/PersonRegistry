using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonRegistry.Domain.Schema;

namespace PersonRegistry.Persistence.Configurations.PhoneNumber;

/// <summary>
/// Configures the entity mapping for the <see cref="Domain.Aggregates.Person.PersonPhoneNumber"/> entity.
/// </summary>
public class PhoneNumberConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Person.PersonPhoneNumber>
{
    /// <summary>
    /// Configures the entity properties and relationships for the PersonPhoneNumber entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Person.PersonPhoneNumber> builder)
    {
        builder.ToTable("PersonPhoneNumbers", TableSchema.PERSON);

        builder.ToTable(t => t.HasCheckConstraint("CK_PersonPhoneNumber_PhoneNumber_MinLength", "LEN(PhoneNumber) >= 4"));

        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}