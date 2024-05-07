using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using DodoDinner.Commands;
using DodoDinner.Models;
using DodoDinner.Repositories;
using DodoDinner.Views;
using Microsoft.EntityFrameworkCore;
using MvvmDialogs;
using Container = SimpleInjector.Container;

namespace DodoDinner.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly PersonsViewModel _personsViewModel;
        private readonly IDialogService _dialogService;
        private readonly MainRepository _mainRepository;
        private readonly CollectionView _collectionViewPerson;

        private CollectionView _collectionViewDinner = null!;

        public ObservableCollection<Person> Persons { get; set; }

        private Person _selectedPerson;

        public Person SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value ?? new Person();

                _collectionViewDinner = (CollectionView)CollectionViewSource.GetDefaultView(SelectedPerson.Dinners);

                _collectionViewDinner.SortDescriptions.Add(new SortDescription(nameof(Dinner.StartedAt), ListSortDirection.Descending));

                OnPropertyChanged(nameof(SelectedPerson));
            }
        }

        public string SearchText
        {
            set
            {
                _collectionViewPerson.Filter = (x) =>
                {
                    if (x == null && value == null)
                    {
                        return true;
                    }

                    return x.ToString().Contains(value);
                };
            }
        }

        public ICommand ShowPersonsCommand { get; set; }
        public ICommand TouchDinnerCommand { get; set; }

        public MainViewModel(IDialogService dialogService, MainRepository mainRepository, PersonsViewModel personsViewModel)
        {
            _dialogService = dialogService;
            _personsViewModel = personsViewModel;
            _mainRepository = mainRepository;

            _mainRepository.Dinners.Load();
            _mainRepository.Persons.Load();

            Persons = _mainRepository.Persons.Local.ToObservableCollection();

            _collectionViewPerson = (CollectionView) CollectionViewSource.GetDefaultView(Persons);

            _collectionViewPerson.SortDescriptions.Add(new SortDescription(nameof(Person.IsDinnerOpen), ListSortDirection.Descending));

            ShowPersonsCommand = new RelayCommand(ShowPersons);
            TouchDinnerCommand = new RelayCommand(TouchDinner, x => SelectedPerson != null);
        }

        private async void TouchDinner(object obj)
        {
            Dinner? item = SelectedPerson.Dinners.LastOrDefault(x => x.ClosedAt == null);

            if (item == null)
            {
                item = new Dinner()
                {
                    Person = SelectedPerson,
                    StartedAt = DateTime.Now
                };

                SelectedPerson.Dinners.Add(item);
            }
            else
            {
                item.ClosedAt = DateTime.Now;
            }

            await _mainRepository.SaveChangesAsync();

            _collectionViewDinner.Refresh();
            _collectionViewPerson.Refresh();
        }

        private void ShowPersons(object obj)
        {
            _dialogService.ShowDialog<PersonsWindow>(this, _personsViewModel);
        }
    }
}
