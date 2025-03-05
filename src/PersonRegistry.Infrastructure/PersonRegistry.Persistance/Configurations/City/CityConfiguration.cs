using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonRegistry.Domain.Schema;

namespace PersonRegistry.Persistence.Configurations.City;

/// <summary>
/// Configures the entity mapping for the <see cref="Domain.Aggregates.City.City"/> entity.
/// </summary>
public class CityConfiguration : IEntityTypeConfiguration<Domain.Aggregates.City.City>
{

    /// <summary>
    /// Configures the entity properties and relationships for the City entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Domain.Aggregates.City.City> builder)
    {
        builder.ToTable("Cities", TableSchema.CITY);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}
