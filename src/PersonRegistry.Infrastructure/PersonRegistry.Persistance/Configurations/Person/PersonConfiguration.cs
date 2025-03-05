using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonRegistry.Domain.Schema;

namespace PersonRegistry.Persistence.Configurations.Person;

/// <summary>
/// Configures the entity mapping for the <see cref="Domain.Aggregates.Person.Person"/> entity.
/// </summary>
public class PersonConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Person.Person>
{

    /// <summary>
    /// Configures the entity properties and relationships for the Person entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Person.Person> builder)
    {
        builder.ToTable("Persons", TableSchema.PERSON);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Person_Name_MinLength", "LEN(Name) >= 2");
            t.HasCheckConstraint("CK_Person_LastName_MinLength", "LEN(LastName) >= 2");
            t.HasCheckConstraint("CK_Person_BirthDate_MinLength", "DATEDIFF(hour, BirthDate, GETDATE()) / 8766.0 >= 18");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);

        builder.Property(x => x.PersonalNumber).HasMaxLength(11).IsFixedLength().IsRequired();

        builder.Property(x => x.BirthDate).IsRequired();

        builder.Property(x => x.Photo).HasMaxLength(250);

        builder.Property(x => x.Gender).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

        builder.HasMany(x => x.PhoneNumbers)
            .WithOne(x => x.Person)
            .HasForeignKey(f => f.PersonId);

        builder.HasMany(x => x.RelatedPersons)
            .WithOne(x => x.Person)
            .HasForeignKey(f => f.PersonId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}
