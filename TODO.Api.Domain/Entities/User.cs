namespace TODO.Api.Domain.Entities
{
    public class User : EntityBase
    {
        public string UserName { get; private set; }
        public string NormalizedUserName { get; private set; }
        public string? PictureUrl { get; private set; }
        public virtual ICollection<TodoItem> TodoItems { get; private set; }

        public User(string userName, string pictureUrl)
        {
            UserName = userName;
            NormalizedUserName = UserName.Normalize();
            PictureUrl = pictureUrl;
            TodoItems = new HashSet<TodoItem>();
        }
        protected User() { }
    }
}
