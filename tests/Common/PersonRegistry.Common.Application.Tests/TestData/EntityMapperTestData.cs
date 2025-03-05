namespace PersonRegistry.API.Tests.TestData;

/// <summary>
/// Provides test data for <see cref="EntityMapperTests"/>.
/// </summary>
internal static class EntityMapperTestData
{
    internal class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    internal class Response
    {
        public int Id { get; }
        public string Name { get; }

        public Response(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}