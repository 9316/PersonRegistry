using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonRegistry.Domain.Schema;

namespace PersonRegistry.Persistence.Configurations.PersonRelationType;

/// <summary>
/// Configures the entity mapping for the <see cref="Domain.Aggregates.PersonRelationType.PersonRelationType"/> entity.
/// </summary>
public class PersonRelationTypeConfiguration : IEntityTypeConfiguration<Domain.Aggregates.PersonRelationType.PersonRelationType>
{
    /// <summary>
    /// Configures the entity properties and relationships for the PersonRelationType entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Domain.Aggregates.PersonRelationType.PersonRelationType> builder)
    {
        builder.ToTable("PersonRelationTypes", TableSchema.PERSON);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);

        builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}