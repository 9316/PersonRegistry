using PersonRegistry.Application.PersonRelation.Command.Create;
using PersonRegistry.Application.PersonRelation.Command.Delete;

namespace PersonRegistry.API.Tests.TestData;

internal static class PersonRelationTestData
{
    internal const int PERSON_ID = 1;
    internal const int RELATION_ID = 10;
    internal const int RELATION_TYPE_ID = 1;

    internal static CreatePersonRelationModelRequest BuildCreatePersonRelationRequest(
        int personId = PERSON_ID,
        int relatedPersonId = RELATION_ID,
        int relationTypeId = RELATION_TYPE_ID)
    {
        return new CreatePersonRelationModelRequest(personId, relatedPersonId, relationTypeId);
    }

    internal static DeletePersonRelationModelRequest BuildDeletePersonRelationRequest(
        int personId = PERSON_ID,
        int relatedPersonId = RELATION_ID,
        int relationTypeId = RELATION_TYPE_ID)
    {
        return new DeletePersonRelationModelRequest(personId, relatedPersonId, relationTypeId);
    }
}