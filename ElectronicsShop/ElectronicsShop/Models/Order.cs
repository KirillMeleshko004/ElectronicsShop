using System.Text.RegularExpressions;

namespace ElectronicsShop.Models
{
    public class Order
    {
        public List<CartProduct> Products { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public double TotalPrice { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }  

        public static bool IsCancelable(string status)
        {
            return status == OrderStatus.AWAITING_CONFIRMATION || status == OrderStatus.ACCEPTED;
        }

        private static readonly string _emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        public static bool IsEmail(string str)
        {
            return Regex.IsMatch(str, _emailPattern, RegexOptions.IgnoreCase);
        }
    }
}
