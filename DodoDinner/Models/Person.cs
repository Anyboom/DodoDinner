using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DodoDinner;

namespace DodoDinnerLibrary
{
    public class Person : BindableBase
    {
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;

                OnPropertyChanged("Id");
            }
        }

        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public ObservableCollection<Dinner> Dinners { get; set; } = new ObservableCollection<Dinner>();

        public bool IsOpenDinner
        {
            get{
                return Dinners.Any(x => x.ClosedAt == null);
            }
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
