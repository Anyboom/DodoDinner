using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DodoDinner
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Person> Persons { get; set; }

        private Person _SelectedPerson;
        public Person SelectedPerson
        {
            get
            {
                return _SelectedPerson;
            }
            set
            {
                _SelectedPerson = value;
                OnPropertyChanged("SelectedPerson");
                OnPropertyChanged("IsCloseDinner");
            }
        }

        public bool IsCloseDinner
        {
            get
            {
                if(_SelectedPerson == null) return true;
                return SelectedPerson.Dinners.Any(x => x.EndAt == null) == false;
            }
        }

        public MainViewModel()
        {

            Persons = new ObservableCollection<Person>
            {
                new Person { FirstName = "Даниил", LastName = "Волков", Dinners = { new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 13, 22, 33) }, new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 13, 22, 33) }, new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 13, 22, 33) }, new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 13, 22, 33) }, new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 13, 22, 33) }, new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 13, 22, 33) }, new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 13, 22, 33) }, new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 13, 22, 33) } } },
                new Person { FirstName = "Аня", LastName = "Доброумова", Dinners = { new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 14, 22, 33) } }  },
                new Person { FirstName = "Николай", LastName = "Пятков", Dinners = { new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 15, 22, 33) } }  },
                new Person { FirstName = "Даниилrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr", LastName = "Волков", Dinners = { new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 16, 22, 33) } }  },
                new Person { FirstName = "Даниил", LastName = "Волков", Dinners = { new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = null } }  },
                new Person { FirstName = "Даниил", LastName = "Волков", Dinners = { new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 16, 22, 33) } }  },
                new Person { FirstName = "Даниил", LastName = "Волков", Dinners = { new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 16, 22, 33) } }  },
                new Person { FirstName = "Даниил", LastName = "Волков", Dinners = { new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 16, 22, 33) } }  },
                new Person { FirstName = "Даниил", LastName = "Волков", Dinners = { new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 16, 22, 33) } }  },
                new Person { FirstName = "Даниил", LastName = "Волков", Dinners = { new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 16, 22, 33) } }  },
                new Person { FirstName = "Даниил", LastName = "Волков", Dinners = { new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 16, 22, 33) } }  },
                new Person { FirstName = "Аня", LastName = "Доброумова", Dinners = { new Dinner { StartAt = new DateTime(2023, 12, 12, 12, 22, 33), EndAt = new DateTime(2023, 12, 12, 17, 22, 33) } }  }
            };

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
