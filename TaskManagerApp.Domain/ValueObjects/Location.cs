namespace TaskManagerApp.Domain.ValueObjects;

public class Location
{
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public Location(string address, string city, string state, string country)
    {
        Address = address;
        City = city;
        State = state;
        Country = country;
    }
}

