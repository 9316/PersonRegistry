using FluentAssertions;
using MediatR;
using Moq;
using PersonRegistry.Application.PersonPhoneNumber.Command.Update;
using PersonRegistry.Domain.Interfaces;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Aggregates.Person;
using PersonRegistry.Domain.Aggregates.PhoneNumberType;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Tests.PersonPhoneNumber.Command.Update;

public class UpdatePersonPhoneNumberCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IPhoneNumberTypeRepository> _phoneNumberTypeRepositoryMock;
    private readonly UpdatePersonPhoneNumberCommandHandler _handler;

    public UpdatePersonPhoneNumberCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _phoneNumberTypeRepositoryMock = new Mock<IPhoneNumberTypeRepository>();

        _unitOfWorkMock.Setup(u => u.PersonRepository).Returns(_personRepositoryMock.Object);
        _unitOfWorkMock.Setup(u => u.PhoneNumberTypeRepository).Returns(_phoneNumberTypeRepositoryMock.Object);

        _handler = new UpdatePersonPhoneNumberCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenPersonDoesNotExist()
    {
        // Arrange
        var personId = 1;
        var command = new UpdatePersonPhoneNumberCommand(personId, 100, 2, "+9876543210");

        _personRepositoryMock.Setup(repo => repo.GetDetailsByIdAsync(personId))
            .ReturnsAsync((Domain.Aggregates.Person.Person)null);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

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
        var phoneNumberId = 100;
        var phoneNumberTypeId = 2;
        var newPhoneNumber = "+9876543210";

        var person = Domain.Aggregates.Person.Person.Create(name: "John", lastName: "Doe", personalNumber: "123456789", birthDate: new DateTime(1990, 1, 1), gender: GenderEnum.Male, cityId: 1);
        var phoneNumber = Domain.Aggregates.Person.PersonPhoneNumber.Create("+1234567890", phoneNumberTypeId);

        var command = new UpdatePersonPhoneNumberCommand(personId, phoneNumberId, phoneNumberTypeId, newPhoneNumber);

        _personRepositoryMock.Setup(repo => repo.GetDetailsByIdAsync(personId))
            .ReturnsAsync(person);

        _phoneNumberTypeRepositoryMock.Setup(repo => repo.AnyAsync(x => x.Id == phoneNumberTypeId))
            .ReturnsAsync(false);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"*{phoneNumberTypeId}*");

        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
