using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using EvernoteCloneWPF.Model;
using EvernoteCloneWPF.ViewModel.Commands;

namespace EvernoteCloneWPF.ViewModel;

public class LoginVM : INotifyPropertyChanged
{
    public bool isShowingRegister = false;

    private User _user;
    public User User
	{
		get { return _user; }
		set { _user = value; }
	}

	private Visibility _loginVisibility;

	public Visibility LoginVisibility
	{
		get { return _loginVisibility; }
        set
        {
            _loginVisibility = value;
            OnPropertyChanged("LoginVisibility");
        }
	}

    private Visibility _registerVisibility;

    public Visibility RegisterVisibility
    {
        get { return _registerVisibility; }
        set
        {
            _registerVisibility = value;
            OnPropertyChanged("RegisterVisibility");
        }
    }


    public RegisterCommand RegisterCommand { get; set; }
	public LoginCommand LoginCommand { get; set; }
    public ShowRegisterCommand ShowRegisterCommand { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public LoginVM()
    {
		LoginVisibility = Visibility.Visible;
        RegisterVisibility = Visibility.Collapsed;

        RegisterCommand = new RegisterCommand(this);
		LoginCommand = new LoginCommand(this);
        ShowRegisterCommand = new ShowRegisterCommand(this);
    }

    public void SwitchViews()
    {
        isShowingRegister = !isShowingRegister;

        if (isShowingRegister)
        {
            RegisterVisibility = Visibility.Visible;
            LoginVisibility = Visibility.Collapsed;
        }
        else
        {
            RegisterVisibility = Visibility.Collapsed;
            LoginVisibility = Visibility.Visible;
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}