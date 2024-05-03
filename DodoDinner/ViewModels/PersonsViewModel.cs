using DodoDinner.Repositories;
using DodoDinnerLibrary;
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

        private string _searchText = string.Empty;

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;

                OnPropertyChanged(nameof(SearchText));

                _collectionView.Refresh();
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
                _selectedItem = value;

                InputItem = value.ToString();

                OnPropertyChanged(nameof(SelectedItem));
                OnPropertyChanged(nameof(InputItem));
            }
        }

        public string InputItem { get; set; } = string.Empty;

        public ICommand AddItemCommand { get; set; }
        public ICommand EditItemCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }
        public ICommand SaveItemsCommand { get; set; }
        public ICommand ClosedWindowCommand { get; set; }

        public PersonsViewModel(MainRepository mainRepository, IDialogService dialogService)
        {
            _mainRepository = mainRepository;
            _dialogService = dialogService;

            Items = _mainRepository.Set<Person>().Local.ToObservableCollection();

            _collectionView = (CollectionView)CollectionViewSource.GetDefaultView(Items);

            _collectionView.Filter = FilterTeamByName;

            AddItemCommand = new RelayCommand(AddItem, x => InputItem?.Length > 0);
            EditItemCommand = new RelayCommand(EditItem, x => InputItem?.Length > 0);
            RemoveItemCommand = new RelayCommand(RemoveItem, x => InputItem?.Length > 0);
            SaveItemsCommand = new RelayCommand(SaveItems);
            ClosedWindowCommand = new RelayCommand(ClosedWindow);
        }

        private bool FilterTeamByName(object x)
        {
            Person item = (Person)x;

            return item.ToString().Contains(SearchText);
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
            //SelectedItem.Name = InputItem;
        }

        private void AddItem(object obj)
        {
            //Person item = new Person()
            //{
            //    Name = InputItem
            //};

            //_mainRepository.Set<Person>().Add(item);
        }
    }
}
