namespace TODO.Api.Domain.Entities
{
    public class User : EntityBase
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string? PictureUrl { get; private set; }
        public string IdentityUserId { get; private set; }
        public virtual ICollection<TodoItem> TodoItems { get; private set; }

        public void ChangeName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            UpdateEntityBase();
        }
        public void SetPictureUrl(string pictureUrl)
        {
            PictureUrl = pictureUrl;
            UpdateEntityBase();
        }

        public void Update(string firstName, string lastName, string pictureUrl)
        {
            FirstName = firstName;
            LastName = lastName;
            PictureUrl = pictureUrl;
            UpdateEntityBase();
        }

        public User(string identityUserId,string firstName, string lastName, string pictureUrl)
        {
            IdentityUserId = identityUserId;
            FirstName = firstName;
            LastName = lastName;
            PictureUrl = pictureUrl;
            TodoItems = new HashSet<TodoItem>();
        }
        protected User() { }
    }
}
