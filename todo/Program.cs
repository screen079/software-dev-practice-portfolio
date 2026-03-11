using System.Collections.Immutable;
using System.Windows.Markup;

namespace todo;

class Program
{
    static void Main(string[] args)
    {
        List<String> tasks = [];
        Dictionary<string, List<int>> tags = [];
        while (true)
        {
            Console.WriteLine("Enter Command (add [item], show, remove [index], tag [index/s] [name], get-tagged [tag], clear, exit)");
            Console.Write("$ ");
            string command = Console.ReadLine() ?? "n/a";
            if (command is "n/a")
            {
                continue;
            }
            string[] arguments = command.Split(" ");
            switch (arguments[0].ToLower())
            {
                case "add":
                    tasks.Add(arguments[1]);
                    Console.WriteLine($"Added {arguments[1]}");
                    break;
                case "show":
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        Console.WriteLine($"[{i}]: {tasks[i]}");
                    }
                    break;
                case "remove":
                    try
                    {
                        int index = int.Parse(arguments[1]);
                        Console.WriteLine($"Removing {tasks[index]}");
                        tasks.RemoveAt(index);
                        // Fix off by 1 errors in tags.
                        if (tags.ToImmutableArray().Length != 0)
                        {
                            foreach (List<int> value in tags.Values)
                            {
                                for (int i = 0; i < value.Count; i++)
                                {
                                    if (value[i] >= index) value[i]--;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}. \nEnsure that {arguments[1]} is an integer.");
                        continue;
                    }
                    break;
                case "clear":
                    tasks.Clear();
                    break;
                case "exit":
                    return; // Exit Program
                case "tag":
                    try
                    {
                        if (arguments[1] is null || arguments[1] == "") throw new ArgumentNullException("[index/s]", "no index/s provided");
                        if (arguments[2] is null || arguments[2] == "") throw new ArgumentNullException("[tag]", "no tag provided");
                        List<int> indexs = [];
                        foreach (var item in arguments[1].Split(","))
                        {
                            int index = int.Parse(item);
                            if (tasks[index] is null) throw new ArgumentOutOfRangeException(nameof(index), index, $"index out of range [0-{tasks.Count}]");
                            indexs.Add(index);
                        }
                        tags.Add(arguments[2], indexs);
                        Console.Write($"Tag: {arguments[2]}, added to: ");
                        foreach (int i in indexs)
                        {
                            Console.Write($"{tasks[i]} | ");
                        }
                        Console.Write('\n');
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("tag [index/s] [name]");
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine($"Input value of {e.Data} cannot be parsed to int, please only enter numbers as the 2nd argument");
                    }
                    catch (ArgumentNullException e)
                    {
                        Console.WriteLine($"Input value of {e.ParamName} cannot be empty");
                        continue;
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine($"Input({e.ActualValue}) out of range [0-{tasks.Count}]");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Unexpected Error: {e.ToString}");
                        continue;
                    }
                    break;
                case "get-tagged":
                    if (tags.Count == 0)
                    {
                        Console.WriteLine("No tags exist");
                        continue;
                    }
                    // if 
                    try
                    {
                        if (arguments[1] is null) throw new ArgumentNullException(nameof(arguments), "nullargs");
                        if (!tags.TryGetValue(arguments[1] ?? string.Empty, out List<int>? indexs))
                        {
                            Console.WriteLine($"Tag '{arguments[1]}' doesn't exist.");
                            continue;
                        }
                        if (indexs is null) throw new ArgumentNullException(nameof(tags), "notags");
                        foreach (int i in tags[arguments[1]])
                        {
                            if (tasks[i] is null) throw new ArgumentOutOfRangeException(nameof(indexs), "index references non-existant task");
                            Console.WriteLine($"[{i}]: {tasks[i]}");
                        }
                    }
                    catch (ArgumentNullException e)
                    {
                        switch (e.Message)
                        {
                            case "nullargs":
                                Console.WriteLine("get-tagged [tag]");
                                continue;
                            case "notags":
                                Console.WriteLine("No tags exist");
                                continue;
                            default:
                                Console.WriteLine($"Unknown ArgumentNullException: {e.Message}");
                                continue;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Unexpected error: {e.ToString}");
                        continue;
                    }
                    break;
                default:
                    continue;

            }
        }
    }
}