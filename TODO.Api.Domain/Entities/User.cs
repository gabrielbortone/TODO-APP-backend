namespace TODO.Api.Domain.Entities
{
    public class User : EntityBase
    {
        public string UserName { get; private set; }
        public string NormalizedUserName { get; private set; }
        public string? PictureUrl { get; private set; }
        public string IdentityUserId { get; private set; }
        public bool? IsDeleted { get; private set; } = false;
        public virtual ICollection<TodoItem> TodoItems { get; private set; }

        public void Delete()
        {
            IsDeleted = true;
            UpdateEntityBase();
        }

        public void Update(string userName, string pictureUrl)
        {
            UserName = userName;
            NormalizedUserName = UserName.Normalize();
            PictureUrl = pictureUrl;
            UpdateEntityBase();
        }

        public User(string identityUserId,string userName, string pictureUrl)
        {
            IdentityUserId = identityUserId;
            UserName = userName;
            NormalizedUserName = UserName.Normalize();
            PictureUrl = pictureUrl;
            TodoItems = new HashSet<TodoItem>();
        }
        protected User() { }
    }
}
