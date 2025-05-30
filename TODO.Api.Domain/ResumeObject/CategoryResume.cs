namespace TODO.Api.Domain.ResumeObject
{
    public class CategoryResume : IItemGet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }

        public CategoryResume(Guid id, string name, string description, string tags)
        {
            Id = id;
            Name = name;
            Description = description;
            Tags = tags;
        }

    }
}
