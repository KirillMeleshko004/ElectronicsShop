using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels.AdminViewModels
{
    [QueryProperty(nameof(User), nameof(User))]
    public partial class UserDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        User _user;

        [ObservableProperty]
        string _role;

        [ObservableProperty]
        bool _isChangable = false;

        readonly UsersService _userService;
        public UserDetailsViewModel(UsersService usersService)
        {
            _userService = usersService;

            PropertyChanged += UserChanged;
        }

        void UserChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(User)) return;
            Role = User.Role;
            IsChangable = User.UserName != App.UserName;
        }

        [RelayCommand]
        async Task DeleteUserAsync()
        {
            IsBusy = true;
            if (!NetworkCheckerService.CheckConnection())
            {
                NetworkCheckerService.ShowNewtworkErrorMessage();
                IsBusy = false;
                return;
            }

            bool choice = await Shell.Current.DisplayAlert("Are you sure?!",
                    $"User {User.UserName} will be deleted",
                    "Confirm",
                    "Cancel");
            if (choice)
            {
                await _userService.DeleteAccount(_user.UserName);
                await Shell.Current.GoToAsync("..");
            }

            IsBusy = false;
        }

        [RelayCommand]
        async Task SwitchRoleAsync()
        {
            IsBusy = true;
            if (!NetworkCheckerService.CheckConnection())
            {
                NetworkCheckerService.ShowNewtworkErrorMessage();
                IsBusy = false;
                return;
            }

            Role = Roles.ChangeRole(Role);
            await _userService.ChangeRole(_user.UserName, Role);
            IsBusy = false;
        }
    }
}
