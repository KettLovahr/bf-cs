using System;
using System.IO;

public class BFCli
{
    static BrainFuck runner;

    public static void Main(string[] args)
    {
        runner = new BrainFuck();
        if (args.Length == 1)
        {
            string code = File.ReadAllText(args[0]);
            runner.Execute(code);
        }
        else
        {
            Console.WriteLine("No file specified, running REPL...");
            string line = "";
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"({runner.GetMemoryCursor():X3}) ");
                Console.ForegroundColor = ConsoleColor.Gray;
                line = Console.ReadLine();
                if (line == "quit" || line == "exit")
                {
                    break;
                }
                else if (line == "dump")
                {
                    runner.FullDump();
                }
                else
                {
                    runner.Execute(line);
                    runner.DumpContext(8);
                }


            }
            Console.WriteLine("Bye!");
        }
    }

}
