using DodoDinnerLibrary;
using LiteDB;
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
using System.Timers;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

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
                OnPropertyChanged("Dinners");
            }
        }

        public MainCommand AddCommand
        {
            get
            {
                return new MainCommand(obj =>
                {
                    DateTime tempDinner = DateTime.Now;

                    if (SelectedPerson.IsOpenDinner == true)
                    {
                        DateTime newDinner = tempDinner;

                        SelectedPerson.Dinners.Last().EndAt = newDinner;

                        using (LiteDatabase db = new LiteDatabase(ResourceManager.PathDB))
                        {
                            var colPersons = db.GetCollection<Person>();

                            Person updatePerson = colPersons.FindById(SelectedPerson.Id);

                            updatePerson.Dinners.Last().EndAt = newDinner;

                            colPersons.Update(updatePerson);
                        }
                    }
                    else
                    {
                        Dinner newDinner = new Dinner() { StartAt = tempDinner };

                        using (LiteDatabase db = new LiteDatabase(ResourceManager.PathDB))
                        {
                            var colPersons = db.GetCollection<Person>();

                            Person updatePerson = colPersons.FindById(SelectedPerson.Id);

                            updatePerson.Dinners.Add(newDinner);

                            colPersons.Update(updatePerson);

                        }

                        SelectedPerson.Dinners.Add(newDinner);
                    }

                    CollectionViewSource.GetDefaultView(Persons).Refresh();
                    CollectionViewSource.GetDefaultView(SelectedPerson.Dinners).Refresh();

                    OnPropertyChanged("SelectedPerson");
                    OnPropertyChanged("IsOpenDinner");
                    OnPropertyChanged("SelectedPerson.Dinners");

                }, (_) => SelectedPerson != null);
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
            using (LiteDatabase db = new LiteDatabase(ResourceManager.PathDB))
            {
                ILiteCollection<Person> collectionPersons = db.GetCollection<Person>();

                Persons = new ObservableCollection<Person>(collectionPersons.FindAll().ToList());
            };

            var sourcePersons = CollectionViewSource.GetDefaultView(Persons);

            sourcePersons.Filter = new Predicate<object>(x =>
            {
                Person item = x as Person;

                if(FilterPerson != null)
                { 
                    return item.FirstName.ToLower().Contains(FilterPerson.ToLower()) || item.LastName.ToLower().Contains(FilterPerson.ToLower());
                }

                return true;
            });

            sourcePersons.SortDescriptions.Add(new SortDescription(nameof(Person.IsOpenDinner), ListSortDirection.Descending));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
