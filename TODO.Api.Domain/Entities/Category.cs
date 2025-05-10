namespace TODO.Api.Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Tags { get; private set; }
        public virtual ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
        

        public Category(string name, string description, string tags)
        {
            Name = name;
            Description = description;
            Tags = tags;
        }
    }
}
