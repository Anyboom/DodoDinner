using System.Collections.ObjectModel;
using System.Linq;

namespace DodoDinner.Models
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

        public bool IsDinnerOpen
        {
            get
            {
                return Dinners.Any(x => x.ClosedAt == null);
            }
        }

        public ObservableCollection<Dinner> Dinners { get; set; } = new ObservableCollection<Dinner>();

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
