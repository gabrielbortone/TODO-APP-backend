using TODO.Api.Domain.ResumeObject;

namespace TODO.Api.Domain.Entities
{
    public class TodoItem : EntityBase
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Priority Priority { get; private set; }
        public DateTime? DueDate { get; private set; }
        public Guid CategoryId { get; private set; }
        public virtual Category Category { get; private set; }
        public DateTime? FinishDate { get; private set; }

        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public void ConfigureUser(User user)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            UserId = user.Id;
        }

        public static TodoItem Create(
            string title,string description, Priority priority, 
            DateTime? dueDate, Guid categoryId)
        {
            return new TodoItem(title, description, priority, dueDate, categoryId);
        }

        public TodoItem(Guid id, string title, string description, Priority priority, DateTime? dueDate, Guid categoryId, Guid userId)
        {
            this.Id = id;
            Title = title;
            Description = description;
            Priority = priority;
            DueDate = dueDate;
            CategoryId = categoryId;
            UserId = userId;
        }
        public TodoItem(string title, string description, Priority priority, DateTime? dueDate, Guid categoryId, Guid userId)
        {
            Title = title;
            Description = description;
            Priority = priority;
            DueDate = dueDate;
            CategoryId = categoryId;
            UserId = userId;
        }
        public TodoItem(string title, string description, Priority priority, DateTime? dueDate, Guid categoryId)
        {
            Title = title;
            Description = description;
            Priority = priority;
            DueDate = dueDate;
            CategoryId = categoryId;
        }
        public void Update(string title, string description, Priority priority, DateTime? dueDate, Guid categoryId)
        {
            Title = title;
            Description = description;
            Priority = priority;
            DueDate = dueDate;
            CategoryId = categoryId;
            UpdateEntityBase();
        }

        public ToDoItemResume ToResumeObject()
        {
            return new ToDoItemResume(Id,
                Title,Description, 
                (int)Priority, DueDate, 
                FinishDate, CategoryId, 
                Category?.Name, 
                Category?.Description);
        }

        public void Mark()
        {
            if (FinishDate.HasValue)
            {
                MarkAsNotCompleted();
            }
            else
            {
                MarkAsCompleted();
            }
        }

        public void MarkAsCompleted()
        {
            FinishDate = DateTime.UtcNow;
            UpdateEntityBase();
        }
        public void MarkAsNotCompleted()
        {
            FinishDate = null;
            UpdateEntityBase();
        }

    }
}
