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
        public Address (string country, string city, string street, int buildingNumber, int? apartmentNumber, int postcode)
        {
            Country = country;
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
            ApartmentNumber = apartmentNumber;
            Postcode = postcode;
        }
        public Address()
        {

        }
        public override string ToString()
        {
            return Country + ", " + City + ", " + Street + ", " + BuildingNumber + ", " + ApartmentNumber + ", " + Postcode;
        }
    }
}
