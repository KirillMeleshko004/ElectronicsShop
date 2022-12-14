using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels.AdminViewModels
{
    public partial class AdminUsersViewModel : BaseViewModel, IRefreshableAsync
    {
        [ObservableProperty]
        ObservableCollection<User> _users;

        readonly UsersService _userService;
        public AdminUsersViewModel(UsersService usersService)
        {
            _userService = usersService;

            RefreshAsync();
        }
        public async void RefreshAsync()
        {
            if (!NetworkCheckerService.CheckConnection())
            {
                NetworkCheckerService.ShowNewtworkErrorMessage();
                return;
            }

            Users = (await _userService.GetUsersAsync()).ToObservableCollection();
        }

        [RelayCommand]
        async Task GoToUserAsync(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(UserDetailsView)}",
                new Dictionary<string, object>
                {
                    ["User"] = user,
                });
        }
    }
}
