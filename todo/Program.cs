namespace todo;

class Program
{
    static void Main(string[] args)
    {
        List<String> tasks = [];
        while (true)
        {
            Console.WriteLine("Enter Command (add [item], show, remove [index], clear, exit)");
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
                    int index;
                    int.TryParse(arguments[1], out index);
                    Console.WriteLine($"Removing {tasks[index]}");
                    tasks.RemoveAt(index);
                    break;
                case "clear":
                    tasks.Clear();
                    break;
                case "exit":
                    return; // Exit Program
                default:
                    continue;
            }
        }
    }
}