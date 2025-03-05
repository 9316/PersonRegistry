using FluentAssertions;
using Moq;
using PersonRegistry.Application.PersonPhoneNumber.Command.Create;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Aggregates.PhoneNumberType;
using PersonRegistry.Domain.Interfaces;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Tests.PersonPhoneNumber.Command.Create;

/// <summary>
/// Unit tests for custom exceptions in <see cref="CreatePersonPhoneNumberCommandHandler"/>.
/// </summary>
public class CreatePersonPhoneNumberCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IPhoneNumberTypeRepository> _phoneNumberTypeRepositoryMock;
    private readonly CreatePersonPhoneNumberCommandHandler _createPersonPhoneNumberCommandHandler;

    public CreatePersonPhoneNumberCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _phoneNumberTypeRepositoryMock = new Mock<IPhoneNumberTypeRepository>();

        _unitOfWorkMock.Setup(u => u.PersonRepository).Returns(_personRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.PhoneNumberTypeRepository).Returns(_phoneNumberTypeRepositoryMock.Object);

        _createPersonPhoneNumberCommandHandler = new CreatePersonPhoneNumberCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenPersonDoesNotExist()
    {
        // Arrange
        var personId = 1;
        var phoneNumber = "+1234567890";
        var createPersonPhoneNumberCommand = new CreatePersonPhoneNumberCommand(personId, PhoneNumberTypeId: 2, phoneNumber);

        _personRepositoryMock.Setup(repo => repo.GetByIdAsync(personId))
            .ReturnsAsync((Domain.Aggregates.Person.Person)null); 

        // Act
        var act = async () => await _createPersonPhoneNumberCommandHandler.Handle(createPersonPhoneNumberCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"*{personId}*");

        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenPhoneNumberTypeDoesNotExist()
    {
        // Arrange
        var personId = 1;
        var phoneNumberTypeId = 2;
        var person = Domain.Aggregates.Person.Person.Create( name: "John", lastName: "Doe", personalNumber: "123456789", birthDate: new DateTime(1990, 1, 1), gender: GenderEnum.Male, cityId: 1);
        var createPersonPhoneNumberCommand = new CreatePersonPhoneNumberCommand(personId,  phoneNumberTypeId, PhoneNumber: "+1234567890");

        _personRepositoryMock.Setup(repo => repo.GetByIdAsync(personId))
            .ReturnsAsync(person);

        _phoneNumberTypeRepositoryMock.Setup(repo => repo.AnyAsync(x => x.Id == phoneNumberTypeId))
            .ReturnsAsync(false);

        // Act
        var act = async () => await _createPersonPhoneNumberCommandHandler.Handle(createPersonPhoneNumberCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"*{phoneNumberTypeId}*");

        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
