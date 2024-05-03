using DodoDinnerLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using DodoDinner.ViewModels;
using DodoDinner.Views;
using MvvmDialogs;
using SimpleInjector;

namespace DodoDinner
{

    public class MainViewModel : BindableBase
    {
        private readonly Container _container;
        private readonly IDialogService _dialogService;
        public ICommand ShowPersonsCommand { get; set; }
        public MainViewModel(IDialogService dialogService, Container container)
        {
            _dialogService = dialogService;
            _container = container;

            ShowPersonsCommand = new RelayCommand(ShowPersons);
        }

        private void ShowPersons(object obj)
        {
            PersonsViewModel viewModel = _container.GetInstance<PersonsViewModel>();

            bool? result = _dialogService.ShowDialog<PersonsWindow>(this, viewModel);
        }
    }
}
