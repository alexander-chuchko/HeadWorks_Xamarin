using SQLite;
using System;

namespace ProfileBook.Model
{
    [Table(nameof(ProfileModel))]
    public class ProfileModel:EntityBase
    {
        public int UserId{get; set;}
        public string NickName { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public string Description { get; set; }
        public DateTime MomentOfRegistration { get; set; }
    }
}
