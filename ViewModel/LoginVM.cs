using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using EvernoteCloneWPF.Model;
using EvernoteCloneWPF.ViewModel.Commands;
using EvernoteCloneWPF.ViewModel.Helpers;

namespace EvernoteCloneWPF.ViewModel;

public class LoginVM : INotifyPropertyChanged
{
    public bool isShowingRegister = false;

    private User _user;
    public User User
	{
		get { return _user; }
        set
        {
            _user = value;
            OnPropertyChanged("User");
        }
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

    private string _username;
    public string UserName
    {
        get { return _username; }
        set
        {
            _username = value;
            User = new User
            {
                UserName = _username,
                Password = this.Password,
                Name = this.Name,
                LastName = this.LastName,
                ConfirmPassword = this.ConfirmPassword
            };
            OnPropertyChanged("Username");
        }
    }

    private string _password;
    public string Password
    {
        get { return _password; }
        set
        {
            _password = value;
            User = new User
            {
                UserName = this.UserName,
                Password = _password,
                Name = this.Name,
                LastName = this.LastName,
                ConfirmPassword = this.ConfirmPassword
            };
            OnPropertyChanged("Password");
        }
    }

    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            User = new User
            {
                UserName = this.UserName,
                Password = this.Password,
                Name = _name,
                LastName = this.LastName,
                ConfirmPassword = this.ConfirmPassword
            };
            OnPropertyChanged("Name");
        }
    }

    private string _lastName;
    public string LastName
    {
        get { return _lastName; }
        set
        {
            _lastName = value;
            User = new User
            {
                UserName = this.UserName,
                Password = this.Password,
                Name = this.Name,
                LastName = _lastName,
                ConfirmPassword = this.ConfirmPassword
            };
            OnPropertyChanged("LastName");
        }
    }

    private string _confirmPassword;
    public string ConfirmPassword
    {
        get { return _confirmPassword; }
        set
        {
            _confirmPassword = value;
            User = new User
            {
                UserName = this.UserName,
                Password = this.Password,
                Name = this.Name,
                LastName = this.LastName,
                ConfirmPassword = _confirmPassword
            };
            OnPropertyChanged("ConfirmPassword");
        }
    }

    public RegisterCommand RegisterCommand { get; set; }
	public LoginCommand LoginCommand { get; set; }
    public ShowRegisterCommand ShowRegisterCommand { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler Authenticated;

    public LoginVM()
    {
		LoginVisibility = Visibility.Visible;
        RegisterVisibility = Visibility.Collapsed;

        RegisterCommand = new RegisterCommand(this);
		LoginCommand = new LoginCommand(this);
        ShowRegisterCommand = new ShowRegisterCommand(this);

        User = new User();
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

    public async void Login()
    {
        bool result = await FirebaseAuthHelper.Login(User);

        if (result)
        {
            Authenticated?.Invoke(this, new EventArgs());
        }
    }

    public async void Register()
    {
        bool result = await FirebaseAuthHelper.Register(User);

        if (result)
        {
            Authenticated?.Invoke(this, new EventArgs());
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}