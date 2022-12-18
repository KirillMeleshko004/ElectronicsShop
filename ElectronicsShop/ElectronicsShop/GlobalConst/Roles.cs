namespace ElectronicsShop.GlobalConst
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static string ChangeRole(string role)
        {
            return role == User ? Admin : User;
        }
    }
}
