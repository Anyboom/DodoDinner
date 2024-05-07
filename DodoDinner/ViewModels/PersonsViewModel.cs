using DodoDinner.Repositories;
using Microsoft.EntityFrameworkCore;
using MvvmDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using DodoDinner.Commands;
using DodoDinner.Models;

namespace DodoDinner.ViewModels
{
    public class PersonsViewModel : BindableBase, IModalDialogViewModel
    {
        private readonly MainRepository _mainRepository;
        private readonly IDialogService _dialogService;
        private readonly CollectionView _collectionView;

        private Person _selectedItem;

        public bool? DialogResult { get; set; } = false;

        public ObservableCollection<Person> Items { get; set; }

        public string SearchText
        {
            set
            {
                _collectionView.Filter = (x) =>
                {
                    if (x == null && value == null)
                    {
                        return true;
                    }

                    return x.ToString().Contains(value);
                };
            }
        }

        public Person SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value ?? new Person();

                FirstName = SelectedItem.FirstName;
                LastName = SelectedItem.LastName;

                OnPropertyChanged(nameof(SelectedItem));
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public ICommand AddItemCommand { get; set; }
        public ICommand EditItemCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }
        public ICommand SaveItemsCommand { get; set; }
        public ICommand ClosedWindowCommand { get; set; }

        public PersonsViewModel(MainRepository mainRepository, IDialogService dialogService)
        {
            _mainRepository = mainRepository;
            _dialogService = dialogService;

            Items = _mainRepository.Persons.Local.ToObservableCollection();

            _collectionView = (CollectionView) CollectionViewSource.GetDefaultView(Items);

            AddItemCommand = new RelayCommand(AddItem, x => FirstName?.Length > 0 && LastName?.Length > 0);
            EditItemCommand = new RelayCommand(EditItem, x => FirstName?.Length > 0 && LastName?.Length > 0);
            RemoveItemCommand = new RelayCommand(RemoveItem, x => FirstName?.Length > 0 && LastName?.Length > 0);
            SaveItemsCommand = new RelayCommand(SaveItems);
            ClosedWindowCommand = new RelayCommand(ClosedWindow);
        }

        private void ClosedWindow(object obj)
        {
            foreach (var entry in _mainRepository.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Detached;
                        entry.Reload();
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        private void SaveItems(object obj)
        {
            _mainRepository.SaveChanges();

            DialogResult = true;

            _dialogService.Close(this);
        }

        private void RemoveItem(object obj)
        {
            _mainRepository.Persons.Remove(SelectedItem);

            _collectionView.Refresh();
        }

        private void EditItem(object obj)
        {
            SelectedItem.FirstName = FirstName;
            SelectedItem.LastName = LastName;
        }

        private void AddItem(object obj)
        {
            Person item = new Person()
            {
                FirstName = FirstName,
                LastName = LastName
            };

            _mainRepository.Persons.Add(item);
        }
    }
}
