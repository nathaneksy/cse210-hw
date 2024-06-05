using System;

public class Address
{
    private string street;
    private string city;
    private string state;
    private string zipCode;

    public Address(string street, string city, string state, string zipCode)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.zipCode = zipCode;
    }

    public override string ToString()
    {
        return $"{street}, {city}, {state} {zipCode}";
    }
}

public abstract class Event
{
    private string title;
    private string description;
    private DateTime date;
    private string time;
    private Address address;

    protected Event(string title, string description, DateTime date, string time, Address address)
    {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    public virtual string GetStandardDetails()
    {
        return $"Title: {title}\nDescription: {description}\nDate: {date.ToShortDateString()}\nTime: {time}\nAddress: {address}";
    }

    public abstract string GetFullDetails();

    public virtual string GetShortDescription()
    {
        return $"{GetType().Name}: {title} on {date.ToShortDateString()}";
    }
}

public class Lecture : Event
{
    private string speaker;
    private int capacity;

    public Lecture(string title, string description, DateTime date, string time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Lecture\nSpeaker: {speaker}\nCapacity: {capacity}";
    }
}

public class Reception : Event
{
    private string rsvpEmail;

    public Reception(string title, string description, DateTime date, string time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Reception\nRSVP Email: {rsvpEmail}";
    }
}

public class OutdoorGathering : Event
{
    private string weatherForecast;

    public OutdoorGathering(string title, string description, DateTime date, string time, Address address, string weatherForecast)
        : base(title, description, date, time, address)
    {
        this.weatherForecast = weatherForecast;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Outdoor Gathering\nWeather Forecast: {weatherForecast}";
    }
}

public class Program
{
    public static void Main()
    {
        Address address1 = new Address("8764 Jerch St", "Los Angeles", "CA", "79457");
        Address address2 = new Address("18746 Strife Ave", "Dallas", "TX", "89443");
        Address address3 = new Address("78 Hail Rd", "Miami", "FL", "88756");

        Lecture lecture = new Lecture("The Future of Sience", "A talk about the future of Sience and its applications in the modern world.", DateTime.Now.AddDays(15), "10:00 AM", address1, "Dr. Hallie Oscor", 300);
        Reception reception = new Reception("Company Annual Get Together", "An evening of celebration and networking.", DateTime.Now.AddDays(20), "7:00 PM", address2, "rsvp@ourcompany.com");
        OutdoorGathering outdoorGathering = new OutdoorGathering("Community BBQ", "Anoutdoor BBQ event for the whole community.", DateTime.Now.AddDays(25), "12:00 PM", address3, "Sunny with a chance of clouds and wind");

        Event[] events = { lecture, reception, outdoorGathering };

        foreach (var ev in events)
        {
            Console.WriteLine("Standard Details:");
            Console.WriteLine(ev.GetStandardDetails());
            Console.WriteLine();

            Console.WriteLine("Full Details:");
            Console.WriteLine(ev.GetFullDetails());
            Console.WriteLine();

            Console.WriteLine("Short Description:");
            Console.WriteLine(ev.GetShortDescription());
            Console.WriteLine();
        }
    }
}
