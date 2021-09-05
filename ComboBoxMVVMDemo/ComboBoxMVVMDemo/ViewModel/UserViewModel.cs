using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ComboBoxMVVMDemo.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ComboBoxMVVMDemo.Commands;
using System.Windows;

namespace ComboBoxMVVMDemo.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged, IDataErrorInfo
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
            _deleteCommand = new RelayCommand(Delete, CanDelete);
            _clearCommand = new RelayCommand(Clear, CanClear);
        }
        public int? UserID
        {
            get { return domObject.UserID; }
            set { domObject.UserID = Convert.ToInt32(value); onPropertyChanged("UserID"); }
        }
        //public int? UserId
        //{
        //    get { return domObject.UserId; }
        //    set
        //    {
        //        if (UserId != null)
        //        {
        //            domObject.UserId = Convert.ToInt32(value);
        //        }
        //        OnPropertyChanged("UserId");
        //    }
        //}
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

        #region relaycommand
        private ICommand _addCommand;
        public ICommand AddUserCmd
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }
        public bool CanAdd(object obj)
        {
            //enable button only if mandatory fields are filled
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
        private ICommand _deleteCommand;
        public ICommand DeleteUserCmd
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; }
        }
        public bool CanDelete(object obj)
        {
            if (UserID != 0 && (_UserList.Count != 0))
            {
                return true;
            }
            return false;
        }
        public void Delete(object obj)
        {
            //User obj1 = (from s in _UserList where s.UserID == (int)obj select s).FirstOrDefault();
            User obj1 = _UserList.FirstOrDefault(s => s.UserID == (int)obj);
            _UserList.Remove(obj1);

        }
        private ICommand _clearCommand;
        public ICommand ClearUserCmd
        {
            get { return _clearCommand; }
            set { _clearCommand = value; }
        }
        public bool CanClear(object obj)
        {
            if (UserID != 0 || FirstName != null || LastName != null || Country != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear(object obj)
        {
            UserID = 0;
            FirstName = null;
            LastName = null;
            Country = null;
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        public void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #region Validationerror
        public string Error
        {
            get { return null; }
        }

        public string this[string propertyName]
        {
            get
            {
                string result = string.Empty;
                if (UserID < 0)
                {
                    result = "User id cannot be zero or neagtive !!!";
                }
                return result;
            }
        }
        #endregion
    }
}
