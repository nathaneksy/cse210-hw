using System;
using System.Collections.Generic;
using System.Linq;

public class ScriptureWord
{
    public string Word { get; }
    public bool IsHidden { get; set; }

    public ScriptureWord(string word)
    {
        Word = word;
        IsHidden = false;
    }
}

public class ScriptureReference
{
    public string Book { get; }
    public int Chapter { get; }
    public int VerseStart { get; }
    public int VerseEnd { get; }

    public ScriptureReference(string reference)
    {
  
        string[] parts = reference.Split(':');
        Book = parts[0].Trim();

        string[] chapterVerse = parts[1].Trim().Split('-');
        Chapter = int.Parse(chapterVerse[0]);

        if (chapterVerse.Length == 1)
        {
            VerseStart = int.Parse(chapterVerse[0]);
            VerseEnd = int.Parse(chapterVerse[0]);
        }
        else
        {
            VerseStart = int.Parse(chapterVerse[0]);
            VerseEnd = int.Parse(chapterVerse[1]);
        }
    }
}

public class Scripture
{
    private List<ScriptureWord> _words;
    public ScriptureReference Reference { get; }
    public string Text { get; }

    public Scripture(string reference, string text)
    {
        Reference = new ScriptureReference(reference);
        Text = text;

        _words = Text.Split(' ').Select(word => new ScriptureWord(word)).ToList();
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine($"{Reference.Book} {Reference.Chapter}:{Reference.VerseStart}-{Reference.VerseEnd}");
        foreach (var word in _words)
        {
            if (word.IsHidden)
                Console.Write("_____ ");
            else
                Console.Write($"{word.Word} ");
        }
        Console.WriteLine("\nPress Enter to continue or type 'quit' to exit...");
    }

    public bool HideRandomWords(int count)
    {
        var visibleWords = _words.Where(word => !word.IsHidden).ToList();
        if (visibleWords.Count == 0)
            return false;

        Random random = new Random();
        int wordsToHide = Math.Min(count, visibleWords.Count);
        for (int i = 0; i < wordsToHide; i++)
        {
            var wordToHide = visibleWords[random.Next(visibleWords.Count)];
            wordToHide.IsHidden = true;
            visibleWords.Remove(wordToHide); 
        }

        return true;
    }

    public bool AllWordsHidden()
    {
        return _words.All(word => word.IsHidden);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var scriptures = new List<Scripture>
        {
            new Scripture("John 3:16", "For God so loved the world that he gave his only Son, that whoever believes in him should not perish but have eternal life."),
            new Scripture("Proverbs 3:5-6", "Trust in the Lord with all your heart, and do not lean on your own understanding; In all your ways acknowledge him, and he will make straight your paths."),
            new Scripture("1st Nephi 3:7", "And it came to pass that I, Nephi, said unto my father: I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them."),
            new Scripture("D&C 6:36", "Look unto me in every thought; doubt not, fear not."),
            new Scripture("John 3:5", "Jesus answered, Verily, verily, I say unto thee, Except a man be born of water and of the Spirit, he cannot enter into the kingdom of God.")
        };

        Random random = new Random();

        var currentScripture = scriptures[random.Next(scriptures.Count)];

        while (true)
        {
            currentScripture.Display();
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "quit")
                break; 

            if (!currentScripture.HideRandomWords(3))
            {
                currentScripture.Display();

                break; 
            }
        }

      
        while (!currentScripture.AllWordsHidden())
        {
            currentScripture.HideRandomWords(1);
        }

        Console.WriteLine("Program ended.");
    }
}
