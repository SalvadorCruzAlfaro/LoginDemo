using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LoginDemo.DTOs;
using LoginDemo.ViewModels;
using LoginDemo.Views;
using Xamarin.Forms;

namespace LoginDemo
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private LoginViewModel viewModel;
        public AppShell()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginViewModel();
        }
    }
}
