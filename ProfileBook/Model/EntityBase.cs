using ProfileBook.Model.Interface;
using SQLite;

namespace ProfileBook.Model
{
    public abstract class EntityBase:IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
 