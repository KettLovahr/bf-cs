using System;
using System.IO;

public class BrainFuck
{
    int pc = 0;
    int mc = 0;
    byte[] memory = new byte[4096];
    Stack stack = new Stack();

    public static void Main(string[] args)
    {
        BrainFuck runner = new BrainFuck();
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
                Console.Write($"({runner.mc:X3}) ");
                Console.ForegroundColor = ConsoleColor.Gray;
                line = Console.ReadLine();
                if (line == "quit") break;

                runner.pc = 0;
                runner.Execute(line);
                runner.DumpContext(8);
            }
            Console.WriteLine("Bye!");
        }
    }

    private static int Emod(int n, int d)
    {
        return ((n % d) + d) % d;
    }

    private void DumpContext(int ctxSize)
    {
        for (int i = mc - ctxSize / 2; i <= mc + ctxSize / 2; i++)
        {
            int f = Emod(i, memory.Length);
            Console.ForegroundColor = f == mc ? ConsoleColor.Green : ConsoleColor.White;
            Console.Write($"{memory[f]:X2} ");
        }
        Console.WriteLine();
    }

    public void Execute(string code)
    {
        while (pc < code.Length)
        {
            switch (code[pc])
            {
                case '>':
                    mc++;
                    if (mc == memory.Length)
                    {
                        mc = 0;
                    }
                    break;
                case '<':
                    mc--;
                    if (mc == -1)
                    {
                        mc = memory.Length - 1;
                    }
                    break;
                case '+':
                    memory[mc]++;
                    break;
                case '-':
                    memory[mc]--;
                    break;
                case '[':
                    stack.Push(pc);
                    break;
                case ']':
                    if (memory[mc] != 0)
                    {
                        pc = stack.Peek();
                    }
                    else
                    {
                        stack.Pop();
                    }
                    break;
                case ',':
                    memory[mc] = (byte)Console.Read();
                    break;
                case '.':
                    Console.Write((char)memory[mc]);
                    break;
            }
            pc++;
        }
    }

    private class Stack
    {
        int[] contents;
        int cursor;

        public void Push(int val)
        {
            contents[cursor++] = val;
        }

        public int Pop()
        {
            return contents[--cursor];
        }

        public int Peek()
        {
            return contents[cursor - 1];
        }

        public Stack()
        {
            cursor = 0;
            contents = new int[256];
        }
    }
}
