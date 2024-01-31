using EvernoteCloneWPF.Model;

namespace EvernoteCloneWPF.ViewModel;

public class LoginVM
{
	private User _user;

	public User User
	{
		get { return _user; }
		set { _user = value; }
	}
}