using System;
using System.IO;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        GoalManagement goals = new GoalManagement();
        Console.Clear(); 

        Console.Write($"\nYou have {goals.GetTotalPoints()} points\n");
        MainMenu choice = new MainMenu();
        GoalMenu goalChoice = new GoalMenu();


        int action = 0;
        while (action != 6)
        {
            action = choice.UserChoice();
            switch (action)
            {
                case 1:
                    Console.Clear();  
                    int goalInput = 0;
                    while (goalInput != 5)
                    {
                        goalInput = goalChoice.GoalChoice();
                        switch (goalInput)
                        {
                            case 1:
                                Console.WriteLine("What is the name of your goal?  ");
                                string name = Console.ReadLine();
                                name = textInfo.ToTitleCase(name);
                                Console.WriteLine("What is a short description of your goal?  ");
                                string description = Console.ReadLine();
                                description = textInfo.ToTitleCase(description);
                                Console.Write("What is the amount of points associated with this goal?  ");
                                int points = int.Parse(Console.ReadLine());
                                SimpleGoal sGoal = new SimpleGoal("Simple Goal:", name, description, points);
                                goals.AddGoal(sGoal);
                                goalInput = 5;
                                break;
                            case 2:
                                Console.WriteLine("What is the name of your goal?  ");
                                name = Console.ReadLine();
                                name = textInfo.ToTitleCase(name);
                                Console.WriteLine("What is a short description of your goal?  ");
                                description = Console.ReadLine();
                                description = textInfo.ToTitleCase(description);
                                Console.Write("What is the amount of points associated with this goal?  ");
                                points = int.Parse(Console.ReadLine());
                                EternalGoal eGoal = new EternalGoal("Eternal Goal:", name, description, points);
                                goals.AddGoal(eGoal);
                                goalInput = 5;
                                break;
                            case 3:
                                Console.WriteLine("What is the name of your goal?  ");
                                name = Console.ReadLine();
                                name = textInfo.ToTitleCase(name);
                                Console.WriteLine("What is a short description of your goal?  ");
                                description = Console.ReadLine();
                                description = textInfo.ToTitleCase(description);
                                Console.Write("What is the amount of points associated with this goal?  ");
                                points = int.Parse(Console.ReadLine());
                                Console.Write("How many times does this goal need to be accomplished for a bonus?  ");
                                int numberTimes = int.Parse(Console.ReadLine());
                                Console.Write("What is the bonus for accomplishing it that many times?  ");
                                int bonusPoints = int.Parse(Console.ReadLine());
                                ChecklistGoal clGoal = new ChecklistGoal("Check List Goal:", name, description, points, numberTimes, bonusPoints);
                                goals.AddGoal(clGoal);
                                goalInput = 5;
                                break;
                            case 4:
                                Console.WriteLine("What is the name of your goal?  ");
                                name = Console.ReadLine();
                                name = textInfo.ToTitleCase(name);
                                Console.WriteLine("What is a description of your goal?  ");
                                description = Console.ReadLine();
                                description = textInfo.ToTitleCase(description);
                                Console.Write("How many points should you lose for this bad goal?  ");
                                points = int.Parse(Console.ReadLine());
                                NegativeGoal nGoal = new NegativeGoal("Negative Goal:", name, description, points);
                                goals.AddGoal(nGoal);
                                goalInput = 5;
                                break;
                            case 5:
                                break;
                            default:
                                Console.WriteLine($"\nIncorrect choice.");
                                break;
                        }
                    }
                    break;
                case 2:
                    Console.Clear();                    
                    goals.ListGoals();
                    Console.Write($"\nYou have {goals.GetTotalPoints()} points\n");
                    break;
                case 3:
                    goals.SaveGoals();
                    break;
                case 4:
                    Console.Clear();
                    goals.LoadGoals();
                    Console.Write($"\nYou have {goals.GetTotalPoints()} points\n");
                    break;
                case 5:
            
                    Console.Clear();  
                    goals.RecordGoalEvent();
                    Console.Write($"\nYou have {goals.GetTotalPoints()} points\n");
                    break;
                case 6:
                    break;
                default:
                    Console.WriteLine($"\nIncorrect choice");
                    break;
            }
        }
    }
}
public class MainMenu
{
    private string _menu = $@"
Menu Options:
1) Create New Goal
2) List Goals
3) Save Goals
4) Load Goals
5) Record Goal Event
6) Quit
Select a choice from the menu:  ";

    public string _userInput;
    private int _userChoice = 0;
    public int UserChoice()
    {
        Console.Write(_menu);
        _userInput = Console.ReadLine();
        _userChoice = 0;
        try
        {
            _userChoice = int.Parse(_userInput);
        }
        catch (FormatException)
        {
            _userChoice = 0;
        }
        catch (Exception exception)
        {
            Console.WriteLine(
                $"Unexpected error:  {exception.Message}");
        }
        return _userChoice;
    }
}
public class GoalMenu
{
    private string _menu = $@"
The Types of Goals are:
1. Simple Goal
2. Eternal Goal
3. Checklist Goal
4. Bad-habit Goal
5. Back to Main Menu
What type of goal would you like to create?  ";

    public string _goalInput;
    private int _goalChoice = 0;
    public int GoalChoice()
    {
        Console.Write(_menu);

        _goalInput = Console.ReadLine();
        _goalChoice = 0;
        try
        {
            _goalChoice = int.Parse(_goalInput);
        }
        catch (FormatException)
        {
            _goalChoice = 0;
        }
        catch (Exception exception)
        {
            Console.WriteLine(
                $"Unexpected error:  {exception.Message}");
        }
        return _goalChoice;
    }
}

public abstract class Goal
{
    private string _type;
    private string _name;
    private string _description;
    private int _points;

    public Goal(string type, string name, string description, int points)
    {
        _type = type;
        _name = name;
        _description = description;
        _points = points;
    }
    public string GetType()
    {
        return _type;
    }
    public string GetName()
    {
        return _name;
    }
    public string GetDescription()
    {
        return _description;
    }
    public int GetPoints()
    {
        return _points;
    }
    public abstract void ListGoal(int i);
    public abstract string SaveGoal();
    public abstract string LoadGoal();
    public abstract void RecordGoalEvent(List<Goal> goals);

}
public class SimpleGoal : Goal
{
    private string _type = "Simple Goal:";
    private bool _status;
    public SimpleGoal(string type, string name, string description, int points) : base(type, name, description, points)
    {
        _status = false;
    }
    public SimpleGoal(string type, string name, string description, int points, bool status) : base(type, name, description, points)
    {
        _status = status;
    }
    public Boolean Finished()
    {
        return _status;
    }
    public override void ListGoal(int i)
    {
        if (Finished() == false)
        {
            Console.WriteLine($"{i}. [ ] {GetName()} ({GetDescription()})");
        }
        else if (Finished() == true)
        {
            Console.WriteLine($"{i}. [X] {GetName()} ({GetDescription()})");
        }
    }
    public override string SaveGoal()
    {
        return ($"{_type}; {GetName()}; {GetDescription()}; {GetPoints()}; {_status}");
    }
    public override string LoadGoal()
    {
        return ($"{_type}; {GetName()}; {GetDescription()}; {GetPoints()}; {_status}");
    }
    public override void RecordGoalEvent(List<Goal> goals)
    {
       _status = true;
       Console.WriteLine($"You have earned {GetPoints()} points");
    }
}

public class ChecklistGoal : Goal
{
    private string _type = "Check List Goal:";
    private int _numberTimes;
    private int _bonusPoints;
    private bool _status;
    private int _count;
    public ChecklistGoal(string type, string name, string description, int points, int numberTimes, int bonusPoints) : base(type, name, description, points)
    {
        _status = false;
        _numberTimes = numberTimes;
        _bonusPoints = bonusPoints;
        _count = 0;
    }
    public ChecklistGoal(string type, string name, string description, int points, bool status, int numberTimes, int bonusPoints, int count) : base(type, name, description, points)
    {
        _status = status;
        _numberTimes = numberTimes;
        _bonusPoints = bonusPoints;
        _count = count;
    }
    public int GetTimes()
    {
        return _numberTimes;
    }
    public void SetTimes()
    {
        _count = _count + 1;
    }
     public int GetCount()
    {
        return _count;
    }
    public void SetCount()
    {
    }
     public int GetBonusPoints()
    {
        return _bonusPoints;
    }
    public Boolean Finished()
    {
        return _status;
    }
    public override void ListGoal(int i)
    {
        if (Finished() == false)
        {
            Console.WriteLine($"{i}. [ ] {GetName()} ({GetDescription()})  --  Currently Completed: {GetCount()}/{GetTimes()}");
        }
        else if (Finished() == true)
        {
            Console.WriteLine($"{i}. [X] {GetName()} ({GetDescription()})  --  Completed: {GetCount()}/{GetTimes()}");
        }


    }
    public override string SaveGoal()
    {
        return ($"{_type}; {GetName()}; {GetDescription()}; {GetPoints()}; {_status}; {GetTimes()}; {GetBonusPoints()}; {GetCount()}");
    }
    public override string LoadGoal()
    {
        return ($"Simple Goal:; {GetName()}; {GetDescription()}; {GetPoints()}; {_status}; {GetTimes()}; {GetBonusPoints()}; {GetCount()}");
    }
    public override void RecordGoalEvent(List<Goal> goals)
    {
        SetTimes();
        int points = GetPoints();

        if (_count == _numberTimes)
        {
            _status = true;
            points = points + _bonusPoints;
  
            Console.WriteLine($"You have earned {points} points");
        }
        else
        {
            Console.WriteLine($"You have earned {GetPoints()} points");
        }
    }

}
public class EternalGoal : Goal
{
    private string _type = "Eternal Goal:";
    private bool _status;

    public EternalGoal(string type, string name, string description, int points) : base(type, name, description, points)
    {
        _status = false;
    }
    public EternalGoal(string type, string name, string description, int points, bool status) : base(type, name, description, points)
    {
        _status = status;
    }

    public override void ListGoal(int i)
    {
        Console.WriteLine($"{i}. [ ] {GetName()} ({GetDescription()})");
    }
    public override string SaveGoal()
    {
        return ($"{_type}; {GetName()}; {GetDescription()}; {GetPoints()}; {_status}");
    }
    public override string LoadGoal()
    {
        return ($"{_type}; {GetName()}; {GetDescription()}; {GetPoints()}; {_status}");
    }
      public override void RecordGoalEvent(List<Goal> goals)
    {
       Console.WriteLine($"You have earned {GetPoints()} points");
    }

}
public class NegativeGoal : Goal
{
    private string _type = "Negative Goal:";
    private bool _status;

    public NegativeGoal(string type, string name, string description, int points) : base(type, name, description, points)
    {
        _status = false;
    }
    public NegativeGoal(string type, string name, string description, int points, bool status) : base(type, name, description, points)
    {
        _status = status;
    }
    public Boolean Finished()
    {
        return _status;
    }
    public override void ListGoal(int i)
    {
        Console.WriteLine($"{i}. [ ] {GetName()} ({GetDescription()})");
    }
    public override string SaveGoal()
    {
        return ($"{_type}; {GetName()}; {GetDescription()}; {GetPoints()}; {_status}");
    }
    public override string LoadGoal()
    {
        return ($"{_type}; {GetName()}; {GetDescription()}; {GetPoints()}; {_status}");
    }
        public override void RecordGoalEvent(List<Goal> goals)
    {
       Console.WriteLine($"You lost {GetPoints()} points");
    }
}
public class GoalManagement

{
    private List<Goal> _goals = new List<Goal>();
    private int _totalPoints;

    public GoalManagement()
    {
        _totalPoints = 0;
    }
    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }
    public int GetTotalPoints()
    {
        return _totalPoints;
    }
    public void AddPoints(int points)
    {
        _totalPoints += points;
    }
    public void AddBonus(int bonusPoints)
    {
        _totalPoints += bonusPoints;
    }
    public void SetTotalPoints(int totalPoints)
    {
        _totalPoints = totalPoints;
    }
    public List<Goal> GetGoalsList()
    {
        return _goals;
    }

    public void ListGoals()
    {
        if (_goals.Count() > 0)
        {
            Console.WriteLine("\nYour Goals are:");

            int index = 1;
            foreach (Goal goal in _goals)
            {
                goal.ListGoal(index);
                index = index + 1;
            }
        }
        else
        {
            Console.WriteLine("\nYou have no goals");
        }
    }
    public void RecordGoalEvent()
    {
        ListGoals();

        Console.Write("\nWhich goal did you accomplished?  ");
        int select = int.Parse(Console.ReadLine())-1;

        int goalPoints = GetGoalsList()[select].GetPoints();
        AddPoints(goalPoints);

        GetGoalsList()[select].RecordGoalEvent(_goals);

        Console.WriteLine($"\nYou have {GetTotalPoints()} points\n");
    }
    public void SaveGoals()
    {
        Console.Write("\nWhat is the name for this goal file?  ");
        string userInput = Console.ReadLine();
        string userFileName = userInput + ".txt";

        using (StreamWriter outputFile = new StreamWriter(userFileName))
        {

            outputFile.WriteLine(GetTotalPoints());
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.SaveGoal());
            }
        }
    }

    public void LoadGoals()
    {
        Console.Write("\nWhat is the name of your goal file?  ");
        string userInput = Console.ReadLine();
        string userFileName = userInput + ".txt";

        if (File.Exists(userFileName))
        {
            string[] readText = File.ReadAllLines(userFileName);
            int totalPoints = int.Parse(readText[0]);
            SetTotalPoints(totalPoints);
            readText = readText.Skip(1).ToArray();
    
            foreach (string line in readText)
            {
                string[] entries = line.Split("; ");

                string type = entries[0];
                string name = entries[1];
                string description = entries[2];
                int points = int.Parse(entries[3]);
                bool status = Convert.ToBoolean(entries[4]);

                if (entries[0] == "Simple Goal:")
                {
                    SimpleGoal sGoal = new SimpleGoal(type, name, description, points, status);
                    AddGoal(sGoal);
                }
                if (entries[0] == "Eternal Goal:")
                {
                    EternalGoal eGoal = new EternalGoal(type, name, description, points, status);
                    AddGoal(eGoal);
                }
                if (entries[0] == "Check List Goal:")
                {
                    int numberTimes = int.Parse(entries[5]);
                    int bonusPoints = int.Parse(entries[6]);
                    int counter = int.Parse(entries[7]);
                    ChecklistGoal clGoal = new ChecklistGoal(type, name, description, points, status, numberTimes, bonusPoints, counter);
                    AddGoal(clGoal);
                }
                if (entries[0] == "Negative Goal:")
                {
                    NegativeGoal nGoal = new NegativeGoal(type, name, description, points, status);
                    AddGoal(nGoal);
                }
            }
        }
    }
}