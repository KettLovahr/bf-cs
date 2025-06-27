using System;
using System.Threading;
using static Raylib;
using static KettGui;

public class BFGui
{
    public static BrainFuck runner;
    private static Thread executorThread;


    public static void Main()
    {
        InitWindow(640, 480, "BrainFuck");
        runner = new BrainFuck();

        WindowManager winmgr = new WindowManager();
        winmgr.AddWindow(new MemoryViewer());
        winmgr.AddWindow(new Editor());

        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(BLACK);

            winmgr.Update();

            EndDrawing();
        }

        CloseWindow();
        if (executorThread != null)
        {
            if (executorThread.IsAlive)
            {
                executorThread.Abort();
            }
            else
            {
                executorThread.Join();
            }
        }
    }

    public static void Execute(string code)
    {
        if (executorThread == null || !executorThread.IsAlive)
        {
            executorThread = new Thread(() => runner.Execute(code));
            executorThread.Start();
        }
    }


    public class MemoryViewer : Window
    {
        public MemoryViewer() : base(100, 40, 256, 300, "Memory View")
        {
        }

        public override void DrawWindowContents()
        {
            int cursor = BFGui.runner.GetMemoryCursor();
            for (int y = 0; y < 64; y++)
            {
                for (int x = 0; x < 64; x++)
                {
                    int position = y * 64 + x;
                    int val = BFGui.runner.GetValueFromAddress(position);
                    DrawRectangle(GetX() + x * 4, GetY() + y * 4, 4, 4, new Color
                    {
                        r = (byte)((val & 0xF) << 4),
                        g = (byte)(cursor == (position) ? 255 : 0),
                        b = (byte)(val & 0xF0),
                        a = 255
                    });
                }
            }

            int value = BFGui.runner.GetValueFromAddress(cursor);
            DrawText($"ADDR: {cursor}; VALUE: {value}", GetX() + 4, GetY() + 264, 20, WHITE);
        }
    }
    public class Editor : Window
    {
        public string contents = "";

        public Editor() : base(100, 400, 400, 28, "Editor")
        {

        }

        public override void DrawWindowContents()
        {
            int offset = 0;
            for (int i = 0; i < contents.Length; i++)
            {
                if (i > 0) offset += MeasureText(contents[i - 1].ToString(), 20) + 1;
                DrawText(contents[i].ToString(), GetX() + 4 + offset, GetY() + 4, 20, BFGui.runner.GetProgramCounter() == i ? GREEN : WHITE);
            }
        }

        public override void Process()
        {
            while (true)
            {
                int character = GetCharPressed();
                if (character == 0) break;
                contents += (char)character;
            }
            if (IsKeyPressed(KeyboardKey.BACKSPACE) || IsKeyPressedRepeat(KeyboardKey.BACKSPACE))
            {
                if (contents.Length > 0)
                {
                    contents = contents.Substring(0, contents.Length - 1);
                }
            }
            if (IsKeyPressed(KeyboardKey.ENTER))
            {
                BFGui.Execute(contents);
            }
        }
    }
}
