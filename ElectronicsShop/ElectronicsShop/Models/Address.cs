using System.Globalization;

namespace ElectronicsShop.Models
{
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        public int Postcode { get; set; }

        public override string ToString()
        {
            return Country + ", " + City + ", " + Street + ", " + BuildingNumber + ", " + ApartmentNumber + ", " + Postcode;
        }
        public static IEnumerable<string> GetCountries()
        {
            List<string> countries = new();
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo ri = new RegionInfo(ci.Name);
                if (!countries.Contains(ri.EnglishName)) countries.Add(ri.EnglishName);
            }
            return countries;
        }
    }
}
