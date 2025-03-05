using PersonRegistry.Application.PersonPhoneNumber.Command.Create;
using PersonRegistry.Application.PersonPhoneNumber.Command.Delete;
using PersonRegistry.Application.PersonPhoneNumber.Command.Update;

namespace PersonRegistry.API.Tests.TestData;

internal static class PersonPhoneNumberTestData
{
    internal const int PERSON_ID = 1;
    internal const int PHONE_NUMBER_ID = 10;
    internal const int PHONE_NUMBER_TYPE_ID = 2;
    internal const string DEFAULT_PHONE_NUMBER = "123-456-7890";
    internal const string UPDATED_PHONE_NUMBER = "987-654-3210";

    internal static CreatePersonPhoneNumberModelRequest BuildCreatePersonPhoneNumberRequest(
        int personId = PERSON_ID,
        int phoneNumberTypeId = PHONE_NUMBER_TYPE_ID,
        string phoneNumber = DEFAULT_PHONE_NUMBER) =>
        new CreatePersonPhoneNumberModelRequest(personId, phoneNumberTypeId, phoneNumber);

    internal static DeletePersonPhoneNumberModelRequest BuildDeletePersonPhoneNumberRequest(
        int personId = PERSON_ID,
        int phoneNumberId = PHONE_NUMBER_ID) =>
        new DeletePersonPhoneNumberModelRequest(personId, phoneNumberId);

    internal static UpdatePersonPhoneNumberModelRequest BuildUpdatePersonPhoneNumberRequest(
        int personId = PERSON_ID,
        int phoneNumberId = PHONE_NUMBER_ID,
        int phoneNumberTypeId = PHONE_NUMBER_TYPE_ID,
        string newPhoneNumber = UPDATED_PHONE_NUMBER) =>
        new UpdatePersonPhoneNumberModelRequest(personId, phoneNumberId, phoneNumberTypeId, newPhoneNumber);
}