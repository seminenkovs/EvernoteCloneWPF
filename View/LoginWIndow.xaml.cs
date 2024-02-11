﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EvernoteCloneWPF.ViewModel;

namespace EvernoteCloneWPF.View
{
    /// <summary>
    /// Interaction logic for LoginWIndow.xaml
    /// </summary>
    public partial class LoginWIndow : Window
    {
        private LoginVM viewModel;

        public LoginWIndow()
        {
            InitializeComponent();

            viewModel = Resources["vm"] as LoginVM;
            viewModel.Authenticated += ViewModel_Authenticated;
        }

        private void ViewModel_Authenticated(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
