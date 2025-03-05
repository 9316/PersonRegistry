using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PersonRegistry.Persistence.Context;
using PersonRegistry.Persistance.Extensions;

namespace PersonRegistry.Persistence.Tests.Extensions;

public class PersistenceServiceExtensionTests
{
    private readonly ServiceCollection _services;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly Mock<IConfigurationSection> _mockConnectionStringSection;

    public PersistenceServiceExtensionTests()
    {
        _services = new ServiceCollection();
        _mockConfiguration = new Mock<IConfiguration>();
        _mockConnectionStringSection = new Mock<IConfigurationSection>();

        _mockConnectionStringSection.Setup(x => x.Value)
            .Returns("Server=(localdb)\\mssqllocaldb;Database=TestDb;Trusted_Connection=True;");

        _mockConfiguration.Setup(x => x.GetSection("ConnectionStrings"))
            .Returns(new Mock<IConfigurationSection>().Object);

        _mockConfiguration.Setup(x => x.GetSection("ConnectionStrings")["PersonRegistry"])
            .Returns(_mockConnectionStringSection.Object.Value);
    }
   
    [Fact]
    public void AddPersistenceServices_ShouldRegisterDbContextWithSqlServer()
    {
        // Act
        _services.AddPersistenceServices(_mockConfiguration.Object);
        var serviceProvider = _services.BuildServiceProvider();
        var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<PersonRegistryDbContext>>();

        // Assert
        dbContextOptions.Should().NotBeNull();
    }
}
