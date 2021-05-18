using Prism.Mvvm;
using System;

namespace ProfileBook.ViewModel
{
    public class ProfileViewModel : BindableBase
    {
        private int _id;
        private int _userId;
        private string _nickName;
        private string _name;
        private string _imageSource;
        private string _description;
        private DateTime _momentOfRegistration;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value ); }
        }
        public int UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string NickName
        {
            get { return _nickName; }
            set { SetProperty(ref _nickName, value); }
        }
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        public string ImageSource
        {
            get { return _imageSource; }
            set { SetProperty(ref _imageSource, value); }
        }
        public DateTime MomentOfRegistration
        {
            get { return _momentOfRegistration; }
            set { SetProperty(ref _momentOfRegistration, value); }
        }
    }
}
