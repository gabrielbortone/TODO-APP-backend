namespace TODO.Api.Application.DTOs
{
    public class ToDoGeneralResumeDto
    {
        public ToDoGeneralResumeDto(int todoCreated, int todoUpdated, int todoCompleted, int todoRemoved)
        {
            TodoCreated = todoCreated;
            TodoUpdated = todoUpdated;
            TodoCompleted = todoCompleted;
            TodoRemoved = todoRemoved;
        }

        public int TodoCreated { get; private set; } = 0;
        public int TodoUpdated { get; private set; } = 0;
        public int TodoCompleted { get; private set; } = 0;
        public int TodoRemoved { get; private set; } = 0;
    }
}
