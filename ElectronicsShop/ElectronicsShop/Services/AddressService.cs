﻿using Geocoding.Microsoft;
using System.Globalization;

namespace ElectronicsShop.Services
{
    public class AddressService
    {
        private readonly BingMapsGeocoder geocoder =
            new BingMapsGeocoder("AhxYjH0f7KdF01OnF0qQs_jIlPZdEMSkzxWSQHH - h9lNEOBHl44jNkvnviZP0OR0");

        async Task<string> ValidateAddressAsync(string country, string city, string street, int? buildingNumber)
        {
            if (AreFieldsEmpty(country, city, street) || buildingNumber is null) return AddressErrorMessages.FIELDS_EMPTY;

            IEnumerable<BingAddress> addresses = await geocoder.GeocodeAsync($"{country}, {city}, {street}, {buildingNumber}");

            BingAddress address = addresses.FirstOrDefault((BingAddress)null);
            if(address is null) return null;

            if (!await IsCityCorrectAsync(country, city))
                return AddressErrorMessages.INCORRECT_CITY;
            if(!address.AddressLine.ToLower().Contains(street.ToLower()))
                return AddressErrorMessages.INCORRECT_STREET;
            if (!address.AddressLine.ToLower().Contains(buildingNumber.ToString()))
                return AddressErrorMessages.INCORRECT_BUILDING_NUMBER;
            
            return AddressErrorMessages.SUCCESS;
        }
        public async Task<string> ValidateAddressAsync(string country, string city, string street, int? buildingNumber, string postalCode)
        {
            if (AreFieldsEmpty(country, city, street, postalCode) || buildingNumber is null) return AddressErrorMessages.FIELDS_EMPTY;

            IEnumerable<BingAddress> addresses = await geocoder.GeocodeAsync($"{country}, {city}, {street}, {buildingNumber}");

            BingAddress address = addresses.FirstOrDefault((BingAddress)null);
            if (address is null) return null;

            if (!await IsCityCorrectAsync(country, city))
                return AddressErrorMessages.INCORRECT_CITY;
            if (!address.AddressLine.ToLower().Contains(street.ToLower()))
                return AddressErrorMessages.INCORRECT_STREET;
            if (!address.AddressLine.ToLower().Contains(buildingNumber.ToString()))
                return AddressErrorMessages.INCORRECT_BUILDING_NUMBER;

            return AddressErrorMessages.SUCCESS;
        }
        async Task<bool> IsCityCorrectAsync(string country, string city)
        {
            IEnumerable<BingAddress> addresses = await geocoder.GeocodeAsync($"{country}, {city}");

            BingAddress address = addresses.FirstOrDefault((BingAddress)null);
            if (address is null) return false;

            if (!(address.CountryRegion.ToLower().Contains(country.ToLower()) &&
                    address.Locality.ToLower().Contains(city.ToLower())))
                return false;
            return true;
        }
        public async Task<string> GetPostcode(string country, string city, string street, int? buildingNumber)
        {
            if (AreFieldsEmpty(country, city, street) || buildingNumber is null) return null;
            if (await ValidateAddressAsync(country, city, street, buildingNumber) != AddressErrorMessages.SUCCESS) return null;

            IEnumerable<BingAddress> addresses = await geocoder.GeocodeAsync($"{country}, {city}, {street}, {buildingNumber}");

            return addresses?.First()?.PostalCode;
        }
        public static IEnumerable<string> GetCountries()
        {
            List<string> countries = new();
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo ri = new(ci.Name);
                if (!countries.Contains(ri.EnglishName)) countries.Add(ri.EnglishName);
            }
            return from country in countries orderby country select country;
        }
        public static bool AreFieldsEmpty(params string[] strings)
        {
            foreach (string str in strings)
                if (string.IsNullOrEmpty(str))
                    return true;
            return false;
        }
        public string GetFormattedAddress(string country, string city, string street, int? buildingNumber,
            int? apartmentNumber, string postalcode)
        {
            return $"{country}, {city}, {street}, {buildingNumber}, {apartmentNumber}, {postalcode}";
        }
    }
}