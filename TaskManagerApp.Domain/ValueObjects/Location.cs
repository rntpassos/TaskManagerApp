namespace TaskManagerApp.Domain.ValueObjects;

public class Location
{
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }

    public Location(string address, string city, string state, string country, string zipCode)
    {
        Address = address;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }
}

