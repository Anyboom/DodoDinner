﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DodoDinnerConfig
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.MouseDown += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DragMove();
                }
            };

            MinimizeButton.Click += (s, e) =>
            {
                this.WindowState = WindowState.Minimized;
            };

            CloseButton.Click += (s, e) =>
            {
                Close();
            };

            this.Icon = Imaging.CreateBitmapSourceFromHIcon(DodoDinnerLibrary.ResourceManager.Favicon.Handle, new Int32Rect(0, 0, DodoDinnerLibrary.ResourceManager.Favicon.Width, DodoDinnerLibrary.ResourceManager.Favicon.Height), BitmapSizeOptions.FromEmptyOptions());
        }
    }
}