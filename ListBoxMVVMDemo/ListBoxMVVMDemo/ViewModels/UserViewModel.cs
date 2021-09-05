using ListBoxMVVMDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ListBoxMVVMDemo.Commands;

namespace ListBoxMVVMDemo.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private User domObject = new User();
        private ObservableCollection<User> _UserList;
        public ObservableCollection<User> Users
        {
            get { return _UserList; }
            set { _UserList = value; }
        }
        public UserViewModel()
        {
            _UserList = new ObservableCollection<User>
            {
                new User{UserID=1,FirstName="Sameera",LastName="Momin",Country="India"},
                new User{UserID=2,FirstName="Aseer",LastName="Momin",Country="India"},
                new User{UserID=3,FirstName="Nihal",LastName="Khan",Country="India"},
            };
            _addCommand = new RelayCommand(Add, CanAdd);
        }
        public int? UserID
        {
            get { return domObject.UserID; }
            set { domObject.UserID = Convert.ToInt32(value); onPropertyChanged("UserID"); }
        }
        public string FirstName
        {
            get { return domObject.FirstName; }
            set { domObject.FirstName = value; onPropertyChanged("FirstName"); }
        }
        public string LastName
        {
            get { return domObject.LastName; }
            set { domObject.LastName = value; onPropertyChanged("LastName"); }
        }
        public string Country
        {
            get { return domObject.Country; }
            set { domObject.Country = value; onPropertyChanged("Country"); }
        }
        //display item selected in list in textbox
        public User SelectedUser
        {
            set
            {
                if (value != null)
                {
                    UserID = value.UserID;
                    FirstName = value.FirstName;
                    LastName = value.LastName;
                    Country = value.Country;
                }
            }
        }

        private ICommand _addCommand;
        public ICommand AddUserCmd
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }
        public bool CanAdd(object obj)
        {
            if (UserID != 0 && (FirstName != null))
            {
                return true;
            }
            return false;
        }
        public void Add(object obj)
        {
            //create a new local instance of user before adding dont use domobj since we use it for binding
            User userobj = new User();
            userobj.UserID = Convert.ToInt32(UserID);
            userobj.FirstName = FirstName;
            userobj.LastName = LastName;
            userobj.Country = Country;
            _UserList.Add(userobj);
            MessageBox.Show("User Added");
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
