﻿using System.Windows.Input;

namespace EvernoteCloneWPF.ViewModel.Commands;

public class LoginCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public LoginVM ViewModel { get; set; }

    public LoginCommand(LoginVM vm)
    {
        ViewModel = vm;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        ViewModel.Login();
    }
}