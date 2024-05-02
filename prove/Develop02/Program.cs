using System;
using System.Collections.Generic;
using System.IO;

public class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }
    public string AuthorName { get; set; }      // To exceed the requirements for this program I added additional information that the user must fill out.
    public string Location { get; set; }        // For example, the name, location, mood and also hunger status options were added to improve the creativity.
    public string Mood { get; set; }            // These answers will show up when you finish the journal entry and will be loaded in your saved file.
    public string HungerStatus{ get; set; }

    public Entry(string prompt, string response, DateTime date, string authorName, string location, string mood, string hungerstatus)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
        AuthorName = authorName;
        Location = location;
        Mood = mood;
        HungerStatus = hungerstatus;
    }

    public string DisplayEntry()
    {
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\nAuthor: {AuthorName}\nLocation: {Location}\nMood: {Mood}\nHunger Status: {HungerStatus}\n";
    }
}

public class Journal
{
    private List<Entry> entries;

    public Journal()
    {
        entries = new List<Entry>();
    }

    public void AddEntry(Entry entry)
    {
        entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry.DisplayEntry());
        }
    }

    public void SaveToFile(string filename)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response},{entry.AuthorName},{entry.Location},{entry.Mood},{entry.HungerStatus}");
                }
            }
            Console.WriteLine("Journal saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving journal: {ex.Message}");
        }
    }

    public void LoadFromFile(string filename)
    {
        try
        {
            entries.Clear(); // Clear existing entries before loading new ones

            string[] lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 5)
                {
                    DateTime date = DateTime.Parse(parts[0]);
                    string prompt = parts[1];
                    string response = parts[2];
                    string authorName = parts[3];
                    string location = parts[4];
                    string mood = parts [5];
                    string hungerstatus = parts [6];
                    entries.Add(new Entry(prompt, response, date, authorName, location, mood, hungerstatus));
                }
            }
            Console.WriteLine("Journal loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading journal: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();

        while (true)
        {
            Console.WriteLine("Journal App Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal entries");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Choose a prompt:");
                    string[] prompts = {
                        "Who was the most interesting person I interacted with today?",
                        "What was the best part of my day?",
                        "How did I see the hand of the Lord in my life today?",
                        "What was the strongest emotion I felt today?",
                        "If I had one thing I could do over today, what would it be?",
                        "Did I meet someone new? If so, was it a good experience?",
                        "How Did I help someone else today?",
                        "What could I have done to improve today?",
                        "Do I regret something that I did today?",
                        "Overall, did my actions today reflect the kind of person that I want to be?",
                        "What was I grateful for today?",
                        "What was the funniest thing you remember from today that made you happy?",
                        "Who could have used your help today?"
                    };
                    Random rnd = new Random();
                    string randomPrompt = prompts[rnd.Next(prompts.Length)];
                    Console.WriteLine($"Prompt: {randomPrompt}");

                    Console.Write("Enter your response: ");
                    string response = Console.ReadLine();

                    Console.Write("Enter your name: ");
                    string authorName = Console.ReadLine();

                    Console.Write("Enter your location: ");
                    string location = Console.ReadLine();

                    Console.Write("Enter your current mood: ");
                    string mood = Console.ReadLine();

                    Console.Write("Enter your hunger status: ");
                    string hungerstatus = Console.ReadLine();

                    journal.AddEntry(new Entry(randomPrompt, response, DateTime.Now, authorName, location, mood, hungerstatus));
                    break;
                case 2:
                    journal.DisplayEntries();
                    break;
                case 3:
                    Console.Write("Enter filename to save journal: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    break;
                case 4:
                    Console.Write("Enter filename to load journal: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
