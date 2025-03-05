using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonRegistry.Domain.Aggregates.City;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Aggregates.Person.PersonRelation;
using PersonRegistry.Domain.Aggregates.PersonRelationType;
using PersonRegistry.Domain.Aggregates.PhoneNumberType;
using PersonRegistry.Persistence.Configurations.City;
using PersonRegistry.Persistence.Configurations.Person;
using PersonRegistry.Persistence.Configurations.PersonRelation;
using PersonRegistry.Persistence.Configurations.PersonRelationType;
using PersonRegistry.Persistence.Configurations.PhoneNumber;
using PersonRegistry.Persistence.Configurations.PhoneNumberType;

namespace PersonRegistry.Persistence.Context;

/// <summary>
/// Represents the Entity Framework Core database context for the Person Registry.
/// </summary>
public class PersonRegistryDbContext : DbContext
{
    /// <summary>
    /// Gets the application configuration settings.
    /// </summary>
    protected IConfiguration Configuration { get; }

    /// <summary>
    /// Gets or sets the Persons table.
    /// </summary>
    public DbSet<Person> Persons { get; set; }

    /// <summary>
    /// Gets or sets the PersonPhoneNumbers table.
    /// </summary>
    public DbSet<PersonPhoneNumber> PersonPhoneNumbers { get; set; }

    /// <summary>
    /// Gets or sets the PersonRelations table.
    /// </summary>
    public DbSet<PersonRelation> PersonRelations { get; set; }

    /// <summary>
    /// Gets or sets the PersonRelationTypes table.
    /// </summary>
    public DbSet<PersonRelationType> PersonRelationTypes { get; set; }

    /// <summary>
    /// Gets or sets the PhoneNumberTypes table.
    /// </summary>
    public DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }

    /// <summary>
    /// Gets or sets the Cities table.
    /// </summary>
    public DbSet<City> Cities { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonRegistryDbContext"/> class.
    /// </summary>
    /// <param name="dbContextOptions">The database context options.</param>
    /// <param name="configuration">The application configuration.</param>
    public PersonRegistryDbContext(DbContextOptions<PersonRegistryDbContext> dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Configures the database context settings.
    /// This method is called when EF Core initializes the DbContext, 
    /// allowing additional configuration of database providers and options.
    /// </summary>
    /// <param name="optionsBuilder">The options builder used to configure the DbContext.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    /// <summary>
    /// Applies entity configurations for the database.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new PersonRelationConfiguration());
        modelBuilder.ApplyConfiguration(new PersonRelationTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PhoneNumberConfiguration());
        modelBuilder.ApplyConfiguration(new PhoneNumberTypeConfiguration());
    }
}
