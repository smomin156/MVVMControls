using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListBoxMVVMDemo.Models
{
    public class User : INotifyPropertyChanged
    {
        private int userID;
        public int UserID
        {
            get { return userID; }
            set { userID = value; onPropertyChanged("UserID"); }
        }
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; onPropertyChanged("FirstName"); }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; onPropertyChanged("LastName"); }
        }
        private string country;
        public string Country
        {
            get { return country; }
            set { country = value; onPropertyChanged("Country"); }
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
