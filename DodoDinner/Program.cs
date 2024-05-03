using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DodoDinner.ViewModels;
using DodoDinner.Views;
using MvvmDialogs;
using DodoDinner.Repositories;

namespace DodoDinner
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var container = new Container();

            container.Register<IDialogService>(() => new DialogService(), Lifestyle.Singleton);
            container.Register<MainRepository>(Lifestyle.Singleton);

            container.Register<MainViewModel>();
            container.Register<MainWindow>();

            container.Register<PersonsViewModel>();
            container.Register<PersonsWindow>();

            container.Verify();

            var app = new App();

            MainWindow mainWindow = container.GetInstance<MainWindow>();

            app.Run(mainWindow);
        }
    }
}
