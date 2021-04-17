using SQLite;

namespace ProfileBook.Model
{
    [Table(nameof(UserModel))]
    public class UserModel: EntityBase
    {
        [Unique]
        public string Login { get; set; }
        
        public string Password { get; set; }
    }
}
