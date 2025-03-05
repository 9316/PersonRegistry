using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonRegistry.Domain.Schema;

namespace PersonRegistry.Persistence.Configurations.PersonRelation;

/// <summary>
/// Configures the entity mapping for the <see cref="Domain.Aggregates.Person.PersonRelation.PersonRelation"/> entity.
/// </summary>
public class PersonRelationConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Person.PersonRelation.PersonRelation>
{
    /// <summary>
    /// Configures the entity properties and relationships for the PersonRelation entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Person.PersonRelation.PersonRelation> builder)
    {
        builder.ToTable("PersonRelations", TableSchema.PERSON);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.PersonRelationType).WithMany();

        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}
