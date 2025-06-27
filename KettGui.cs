using System;
using System.Collections.Generic;
using static Raylib;

public class KettGui
{
    public static void Swap(List<Window> l, int a, int b)
    {
        Window temp = l[a];
        l[a] = l[b];
        l[b] = temp;
    }

    public class WindowManager
    {
        List<Window> windows;
        Window focused;

        public WindowManager()
        {
            windows = new List<Window>();
        }

        public void FloatWindowToTop(Window win)
        {
            for (int i = 0; i < windows.Count - 1; i++)
            {
                if (windows[i] == win)
                {
                    KettGui.Swap(windows, i, i + 1);
                }
            }
        }

        public void AddWindow(Window window)
        {
            windows.Add(window);
        }

        public void Update()
        {
            for (int i = windows.Count - 1; i >= 0; i--)
            {
                Window window = windows[i];
                window.Update();
                if (IsMouseButtonPressed(MouseButton.LEFT))
                {
                    int mouseX = GetMouseX();
                    int mouseY = GetMouseY();

                    if (window.IsPositionInHeader(mouseX, mouseY))
                    {
                        window.SetMoveHandle(mouseX - window.GetX(), mouseY - window.GetY());
                        focused = window;
                        break;
                    }
                    else if (window.IsPositionInBounds(mouseX, mouseY))
                    {
                        break; // TODO: This window will consume the input
                    }
                }
            }
            FloatWindowToTop(focused);
            foreach (Window window in windows)
            {
                window.SetFocus(window == focused);
                window.Draw();
            }
        }
    }

    public class Window
    {
        int posX;
        int posY;
        int winW;
        int winH;
        bool focused;
        string title;

        int dragHandleX;
        int dragHandleY;
        bool dragging;

        public Window(int x, int y, int w, int h, string title = "<no title>")
        {
            this.posX = x;
            this.posY = y;
            this.winW = w;
            this.winH = h;
            this.title = title;
        }

        public void SetFocus(bool focus)
        {
            focused = focus;
        }
        public void SetMoveHandle(int x, int y)
        {
            dragHandleX = x;
            dragHandleY = y;
            dragging = true;
        }

        public int GetX()
        {
            return posX;
        }
        public int GetY()
        {
            return posY;
        }
        public int GetW()
        {
            return winW;
        }
        public int GetH()
        {
            return winH;
        }

        public bool IsPositionInBounds(int x, int y)
        {
            return x > posX && x < posX + winW && y > posY - 32 && y < posY + winH;
        }

        public bool IsPositionInHeader(int x, int y)
        {
            return x > posX && x < posX + winW && y > posY - 32 && y < posY;
        }

        public void Update()
        {
            if (focused)
            {
                Process();
                if (IsMouseButtonDown(MouseButton.LEFT))
                {
                    if (dragging)
                    {
                        posX = GetMouseX() - dragHandleX;
                        posY = GetMouseY() - dragHandleY;
                    }
                }
                if (IsMouseButtonReleased(MouseButton.LEFT))
                {
                    dragging = false;
                }
            }
        }

        public virtual void Process() { }

        public void Draw()
        {
            // Window
            BeginScissorMode(posX, posY, winW, winH);

            DrawRectangle(posX, posY, winW, winH, DARKGRAY);
            DrawWindowContents();

            EndScissorMode();

            // Header
            BeginScissorMode(posX, posY - 32, winW, 32);

            DrawRectangle(posX, posY - 32, winW, 32, focused ? GREEN : GRAY);
            DrawText($"{title}", posX + 6, posY - 32 + 6, 20, focused ? BLACK : WHITE);

            EndScissorMode();

            BeginScissorMode(posX, posY, winW, winH);
            DrawRectangleLines(posX + 1, posY, winW - 1, winH, focused ? GREEN : GRAY);
            EndScissorMode();
        }

        public virtual void DrawWindowContents() { }
    }
}
