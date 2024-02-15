using DodoDinnerLibrary;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DodoDinnerConfig
{ 
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Person> Persons { get; set; } = new ObservableCollection<Person>();

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

                FirstName = (value == null) ? string.Empty : value.FirstName;
                LastName = (value == null) ? string.Empty : value.LastName;

                OnPropertyChanged("SelectedPerson");
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

        private string _FirstName;
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _LastName;
        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public MainCommand DeleteCommand
        {
            get
            {
                return new MainCommand(x =>
                {
                    using (LiteDatabase db = new LiteDatabase(ResourceManager.PathDB))
                    {
                        var colPersons = db.GetCollection<Person>();
                        Person deletePerson = SelectedPerson;
                        Persons.Remove(deletePerson);
                        colPersons.Delete(deletePerson.Id);
                    }
                }, (_) => SelectedPerson != null);
            }
        }

        public MainCommand EditCommand
        {
            get
            {
                return new MainCommand(x =>
                {
                    using (LiteDatabase db = new LiteDatabase(ResourceManager.PathDB))
                    {
                        var colPersons = db.GetCollection<Person>();

                        Person editPerson = colPersons.FindById(SelectedPerson.Id);

                        editPerson.FirstName = FirstName;
                        editPerson.LastName = LastName;

                        SelectedPerson.FirstName = FirstName;
                        SelectedPerson.LastName = LastName;

                        colPersons.Update(editPerson);

                        CollectionViewSource.GetDefaultView(Persons).Refresh();
                    }

                }, (_) => SelectedPerson != null);
            }
        }

        public MainCommand AddCommand
        {
            get
            {
                return new MainCommand(x =>
                {
                    using (LiteDatabase db = new LiteDatabase(ResourceManager.PathDB))
                    {
                        var colPersons = db.GetCollection<Person>();

                        Person tempPerson = new Person { FirstName = this.FirstName, LastName = this.LastName };

                        colPersons.Insert(tempPerson);
                        Persons.Add(tempPerson);
                    };
                }, (_) => FirstName != null && LastName != null && FirstName.Length > 0 && LastName.Length > 0);
            }
        }

        public MainViewModel()
        {
            using (LiteDatabase db = new LiteDatabase(ResourceManager.PathDB))
            {
                var colPersons = db.GetCollection<Person>();

                colPersons.FindAll().ToList().ForEach(x => Persons.Add(x));
            };

            var source = CollectionViewSource.GetDefaultView(Persons);

            source.Filter = new Predicate<object>(x =>
            {
                Person item = x as Person;

                if (FilterPerson != null)
                {
                    return item.FirstName.ToLower().Contains(FilterPerson.ToLower()) || item.LastName.ToLower().Contains(FilterPerson.ToLower());
                }

                return true;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
