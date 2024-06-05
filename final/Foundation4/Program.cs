using System;
using System.Collections.Generic;

public abstract class Activity
{
    private DateTime date;
    private int duration; 

    protected Activity(DateTime date, int duration)
    {
        this.date = date;
        this.duration = duration;
    }

    public DateTime Date => date;
    public int Duration => duration;

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{date.ToShortDateString()} {GetType().Name} ({duration} min) - Distance: {GetDistance():0.0} miles, Speed: {GetSpeed():0.0} mph, Pace: {GetPace():0.0} min per mile";
    }
}

public class Running : Activity
{
    private double distance; 

    public Running(DateTime date, int duration, double distance)
        : base(date, duration)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        return distance / (Duration / 60.0);
    }

    public override double GetPace()
    {
        return Duration / distance;
    }
}

public class Cycling : Activity
{
    private double speed; 

    public Cycling(DateTime date, int duration, double speed)
        : base(date, duration)
    {
        this.speed = speed;
    }

    public override double GetDistance()
    {
        return speed * (Duration / 60.0);
    }

    public override double GetSpeed()
    {
        return speed;
    }

    public override double GetPace()
    {
        return 60.0 / speed;
    }
}

public class Swimming : Activity
{
    private int laps;
    private const double LapDistance = 50 / 1609.34; 

    public Swimming(DateTime date, int duration, int laps)
        : base(date, duration)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return laps * LapDistance;
    }

    public override double GetSpeed()
    {
        return GetDistance() / (Duration / 60.0);
    }

    public override double GetPace()
    {
        return Duration / GetDistance();
    }

    public override string GetSummary()
    {
        return $"{Date.ToShortDateString()} {GetType().Name} ({Duration} min) - Distance: {GetDistance():0.0} miles, Speed: {GetSpeed():0.0} mph, Pace: {GetPace():0.0} min per mile, Laps: {laps}";
    }
}

public class Program
{
    public static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2024, 6, 1), 30, 3.0),
            new Cycling(new DateTime(2024, 6, 2), 45, 15.0),
            new Swimming(new DateTime(2024, 6, 3), 60, 40)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
            Console.WriteLine();
        }
    }
}
