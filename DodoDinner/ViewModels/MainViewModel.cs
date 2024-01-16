using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

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
                OnPropertyChanged("IsOpenDinner");
            }
        }
        public MainCommand AddCommand
        {
            get
            {
                return new MainCommand(obj =>
                {
                    if (SelectedPerson.IsOpenDinner == true)
                    {
                        SelectedPerson.Dinners.Last().EndAt = DateTime.Now;
                    }
                    else
                    {
                        SelectedPerson.Dinners.Add(new Dinner() { StartAt = DateTime.Now });
                    }

                    CollectionViewSource.GetDefaultView(Persons).Refresh();
                    CollectionViewSource.GetDefaultView(SelectedPerson.Dinners).Refresh();

                    OnPropertyChanged("SelectedPerson");
                    OnPropertyChanged("IsOpenDinner");
                });
            }
        }

        private string _FilterPerson;
        public string FilterPerson
        {
            get
            {
                return _FilterPerson;
            }
            set
            {
                _FilterPerson = value;
                CollectionViewSource.GetDefaultView(Persons).Refresh();
                OnPropertyChanged("FilterPerson");
            }
        }

        public MainViewModel()
        {
            Persons = new ObservableCollection<Person>
            {
                new Person {Id = 5, FirstName = "Даниил", LastName = "Волков", Dinners = { new Dinner() { StartAt = new DateTime(2024, 1, 1, 1, 2, 3)}}},
                new Person {Id = 6, FirstName = "Аня", LastName = "Доброумова" },
                new Person {Id = 7, FirstName = "Николай", LastName = "Пятков"  },
                new Person {Id = 8, FirstName = "Карина", LastName = "Родионова" },
                new Person {Id = 9, FirstName = "Артем", LastName = "Гасанов"  },
            };

            var source = CollectionViewSource.GetDefaultView(Persons);

            source.Filter = new Predicate<object>(x =>
            {
                Person item = x as Person;

                if(FilterPerson != null)
                { 
                    return item.FirstName.ToLower().Contains(FilterPerson.ToLower()) || item.LastName.ToLower().Contains(FilterPerson.ToLower());
                }

                return true;
            });

            source.SortDescriptions.Add(new SortDescription(nameof(Person.IsOpenDinner), ListSortDirection.Descending));

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
