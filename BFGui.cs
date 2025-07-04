using System;
using System.IO;
using System.Threading;
using static Raylib;
using static KettGui;

public class BFGui
{
    public static BrainFuck runner;
    private static Thread executorThread;
    private static bool slow;


    public static void Main(string[] args)
    {
        string contents = "";
        if (args.Length == 1)
        {
            contents = File.ReadAllText(args[0]);
        }
        InitWindow(640, 480, "BrainFuck");
        runner = new BrainFuck();

        WindowManager winmgr = new WindowManager();
        winmgr.AddWindow(new MemoryViewer());

        Editor editor = new Editor();
        winmgr.AddWindow(editor);

        if (contents != "")
        {
            editor.contents = BrainFuck.Sanitize(contents);
        }

        winmgr.AddWindow(new ExecutionOptions());

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

    public static void Execute(string code, int delay = 0)
    {
        if (executorThread == null || !executorThread.IsAlive)
        {
            executorThread = new Thread(() => runner.Execute(code, delay));
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

        public Editor() : base(20, 400, 600, 28, "Editor")
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
                BFGui.Execute(contents, slow ? 20 : 0);
            }
        }
    }
    public class ExecutionOptions : Window
    {
        public ExecutionOptions() : base(400, 100, 200, 240, "Execution Options")
        {

        }

        public override void DrawWindowContents()
        {
            DrawRectangle(GetX() + 20, GetY() + 20, 20, 20, BLACK);
            if (slow)
            {
                DrawRectangle(GetX() + 22, GetY() + 22, 16, 16, GREEN);
            }

            DrawText("Slow Run", GetX() + 50, GetY() + 20, 20, BLACK);
        }

        public override void Process()
        {
            if (IsFocused())
            {
                int mouseX = GetMouseX();
                int mouseY = GetMouseY();

                if (mouseX > GetX() + 20 && mouseX < GetX() + MeasureText("Slow Run", 20) + 20)
                {
                    if (mouseY > GetY() + 20 && mouseY < GetY() + 40)
                    {
                        if (IsMouseButtonReleased(MouseButton.LEFT))
                            slow ^= true;
                    }
                }
            }
        }
    }
}
