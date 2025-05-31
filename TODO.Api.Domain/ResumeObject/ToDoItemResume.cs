namespace TODO.Api.Domain.ResumeObject
{
    public class ToDoItemResume : IItemGet
    {
        public Guid Id { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Priority { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DateTime? FinishDate { get; private set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public ToDoItemResume(Guid id, string title, string description, int priority,
            DateTime? dueDate, DateTime? finishDate, Guid categoryId,
            string categoryName, string categoryDescription)
        {
            Id = id;
            Title = title;
            Description = description;
            Priority = priority;
            DueDate = dueDate;
            FinishDate = finishDate;
            CategoryId = categoryId;
            CategoryName = categoryName;
            CategoryDescription = categoryDescription;
        }
    }
}
