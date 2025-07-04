using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

public class BrainFuck
{
    private int pc = 0;
    private int mc = 0;
    private byte[] memory = new byte[4096];
    Stack stack = new Stack();


    private static int Emod(int n, int d)
    {
        return ((n % d) + d) % d;
    }

    public static string Sanitize(string code)
    {
        StringBuilder sb = new StringBuilder();

        char[] allowed = { '+', '-', '>', '<', '[', ']', ',', '.' };

        foreach (char c in code)
        {
            if (allowed.Contains(c))
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    public void FullDump()
    {
        for (int i = 0; i < memory.Length; i++)
        {
            if (i % 32 == 0)
            {
                Console.WriteLine();
                Console.Write($"{i:X3}: ");
            }
            Console.ForegroundColor = i == mc ? ConsoleColor.Green : ConsoleColor.White;
            Console.Write($"{memory[i]:X2} ");
        }
        Console.WriteLine();
    }

    public void DumpContext(int ctxSize)
    {
        for (int i = mc - ctxSize / 2; i <= mc + ctxSize / 2; i++)
        {
            int f = Emod(i, memory.Length);
            Console.ForegroundColor = f == mc ? ConsoleColor.Green : ConsoleColor.White;
            Console.Write($"{memory[f]:X2} ");
        }
        Console.WriteLine();
    }

    public int GetProgramCounter()
    {
        return pc;
    }

    public int GetMemoryCursor()
    {
        return mc;
    }

    public int GetValueFromAddress(int addr)
    {
        return memory[addr];
    }

    public void Execute(string code, int delay = 0)
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
            if (delay != 0.0)
            {
                Thread.Sleep(delay);
            }
            pc++;
        }
        pc = 0;
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
