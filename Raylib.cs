using System.Runtime.InteropServices;

// ad-hoc Raylib thin bindings

public class Raylib
{
    [DllImport("raylib")] public static extern void SetConfigFlags(ConfigFlags flags);
    [DllImport("raylib")] public static extern void SetTargetFPS(int fps);

    [DllImport("raylib")] public static extern void InitWindow(int width, int height, string title);
    [DllImport("raylib")] public static extern bool WindowShouldClose();
    [DllImport("raylib")] public static extern void CloseWindow();

    [DllImport("raylib")] public static extern void BeginDrawing();
    [DllImport("raylib")] public static extern void EndDrawing();
    [DllImport("raylib")] public static extern void BeginScissorMode(int x, int y, int width, int height);
    [DllImport("raylib")] public static extern void EndScissorMode();

    [DllImport("raylib")] public static extern void ClearBackground(Color color);
    [DllImport("raylib")] public static extern void DrawLine(int startPosX, int startPosY, int endPosX, int endPosY, Color color);
    [DllImport("raylib")] public static extern void DrawPixel(int x, int y, Color color);
    [DllImport("raylib")] public static extern void DrawRectangle(int posX, int posY, int width, int height, Color color);
    [DllImport("raylib")] public static extern void DrawRectangleLines(int posX, int posY, int width, int height, Color color);
    [DllImport("raylib")] public static extern void DrawText(string text, int posX, int posY, int fontSize, Color color);
    [DllImport("raylib")] public static extern int MeasureText(string text, int size);

    [DllImport("raylib")] public static extern int GetScreenWidth();
    [DllImport("raylib")] public static extern int GetScreenHeight();
    [DllImport("raylib")] public static extern float GetFrameTime();

    [DllImport("raylib")] public static extern bool IsKeyPressed(KeyboardKey key);
    [DllImport("raylib")] public static extern bool IsKeyPressedRepeat(KeyboardKey key);
    [DllImport("raylib")] public static extern bool IsKeyDown(KeyboardKey key);
    [DllImport("raylib")] public static extern bool IsKeyReleased(KeyboardKey key);
    [DllImport("raylib")] public static extern bool IsKeyUp(KeyboardKey key);
    [DllImport("raylib")] public static extern KeyboardKey GetKeyPressed();
    [DllImport("raylib")] public static extern int GetCharPressed();
    [DllImport("raylib")] public static extern void SetExitKey(KeyboardKey key);

    [DllImport("raylib")] public static extern bool IsMouseButtonPressed(MouseButton button);
    [DllImport("raylib")] public static extern bool IsMouseButtonDown(MouseButton button);
    [DllImport("raylib")] public static extern bool IsMouseButtonReleased(MouseButton button);
    [DllImport("raylib")] public static extern bool IsMouseButtonUp(MouseButton button);
    [DllImport("raylib")] public static extern int GetMouseX();
    [DllImport("raylib")] public static extern int GetMouseY();
    [DllImport("raylib")] public static extern Vector2 GetMousePosition();
    [DllImport("raylib")] public static extern Vector2 GetMouseDelta();
    [DllImport("raylib")] public static extern void SetMousePosition(int x, int y);
    [DllImport("raylib")] public static extern void SetMouseOffset(int offsetX, int offsetY);
    [DllImport("raylib")] public static extern void SetMouseScale(float scaleX, float scaleY);
    [DllImport("raylib")] public static extern float GetMouseWheelMove();
    [DllImport("raylib")] public static extern Vector2 GetMouseWheelMoveV();
    [DllImport("raylib")] public static extern void SetMouseCursor(int cursor);

    public struct Color
    {
        public byte r;
        public byte g;
        public byte b;
        public byte a;
    }

    public struct Vector2
    {
        public float x;
        public float y;

        public static Vector2 operator *(Vector2 lhs, float rhs)
        {
            return new Vector2
            {
                x = lhs.x * rhs,
                y = lhs.y * rhs,
            };
        }

        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2
            {
                x = lhs.x + rhs.x,
                y = lhs.y + rhs.y,
            };
        }
    }

    public enum KeyboardKey
    {
        NULL = 0,

        APOSTROPHE = 39,
        COMMA = 44,
        MINUS = 45,
        PERIOD = 46,
        SLASH = 47,
        ZERO = 48,
        ONE = 49,
        TWO = 50,
        THREE = 51,
        FOUR = 52,
        FIVE = 53,
        SIX = 54,
        SEVEN = 55,
        EIGHT = 56,
        NINE = 57,
        SEMICOLON = 59,
        EQUAL = 61,
        A = 65,
        B = 66,
        C = 67,
        D = 68,
        E = 69,
        F = 70,
        G = 71,
        H = 72,
        I = 73,
        J = 74,
        K = 75,
        L = 76,
        M = 77,
        N = 78,
        O = 79,
        P = 80,
        Q = 81,
        R = 82,
        S = 83,
        T = 84,
        U = 85,
        V = 86,
        W = 87,
        X = 88,
        Y = 89,
        Z = 90,
        LEFT_BRACKET = 91,
        BACKSLASH = 92,
        RIGHT_BRACKET = 93,
        GRAVE = 96,

        SPACE = 32,
        ESCAPE = 256,
        ENTER = 257,
        TAB = 258,
        BACKSPACE = 259,
        INSERT = 260,
        DELETE = 261,
        RIGHT = 262,
        LEFT = 263,
        DOWN = 264,
        UP = 265,
        PAGE_UP = 266,
        PAGE_DOWN = 267,
        HOME = 268,
        END = 269,
        CAPS_LOCK = 280,
        SCROLL_LOCK = 281,
        NUM_LOCK = 282,
        PRINT_SCREEN = 283,
        PAUSE = 284,
        F1 = 290,
        F2 = 291,
        F3 = 292,
        F4 = 293,
        F5 = 294,
        F6 = 295,
        F7 = 296,
        F8 = 297,
        F9 = 298,
        F10 = 299,
        F11 = 300,
        F12 = 301,
        LEFT_SHIFT = 340,
        LEFT_CONTROL = 341,
        LEFT_ALT = 342,
        LEFT_SUPER = 343,
        RIGHT_SHIFT = 344,
        RIGHT_CONTROL = 345,
        RIGHT_ALT = 346,
        RIGHT_SUPER = 347,
        KB_MENU = 348,

        KP_0 = 320,
        KP_1 = 321,
        KP_2 = 322,
        KP_3 = 323,
        KP_4 = 324,
        KP_5 = 325,
        KP_6 = 326,
        KP_7 = 327,
        KP_8 = 328,
        KP_9 = 329,
        KP_DECIMAL = 330,
        KP_DIVIDE = 331,
        KP_MULTIPLY = 332,
        KP_SUBTRACT = 333,
        KP_ADD = 334,
        KP_ENTER = 335,
        KP_EQUAL = 336,

        BACK = 4,
        MENU = 5,
        VOLUME_UP = 24,
        VOLUME_DOWN = 25
    };

    public enum MouseButton
    {
        LEFT = 0,
        RIGHT = 1,
        MIDDLE = 2,
        SIDE = 3,
        EXTRA = 4,
        FORWARD = 5,
        BACK = 6,

    }

    public enum MouseCursor
    {
        DEFAULT = 0,
        ARROW = 1,
        IBEAM = 2,
        CROSSHAIR = 3,
        POINTING_HAND = 4,
        RESIZE_EW = 5,
        RESIZE_NS = 6,
        RESIZE_NWSE = 7,
        RESIZE_NESW = 8,
        RESIZE_ALL = 9,
        NOT_ALLOWED = 10
    }



    public static readonly Color LIGHTGRAY = new Color { r = 200, g = 200, b = 200, a = 255 };
    public static readonly Color GRAY = new Color { r = 130, g = 130, b = 130, a = 255 };
    public static readonly Color DARKGRAY = new Color { r = 80, g = 80, b = 80, a = 255 };
    public static readonly Color YELLOW = new Color { r = 253, g = 249, b = 0, a = 255 };
    public static readonly Color GOLD = new Color { r = 255, g = 203, b = 0, a = 255 };
    public static readonly Color ORANGE = new Color { r = 255, g = 161, b = 0, a = 255 };
    public static readonly Color PINK = new Color { r = 255, g = 109, b = 194, a = 255 };
    public static readonly Color RED = new Color { r = 230, g = 41, b = 55, a = 255 };
    public static readonly Color MAROON = new Color { r = 190, g = 33, b = 55, a = 255 };
    public static readonly Color GREEN = new Color { r = 0, g = 228, b = 48, a = 255 };
    public static readonly Color LIME = new Color { r = 0, g = 158, b = 47, a = 255 };
    public static readonly Color DARKGREEN = new Color { r = 0, g = 117, b = 44, a = 255 };
    public static readonly Color SKYBLUE = new Color { r = 102, g = 191, b = 255, a = 255 };
    public static readonly Color BLUE = new Color { r = 0, g = 121, b = 241, a = 255 };
    public static readonly Color DARKBLUE = new Color { r = 0, g = 82, b = 172, a = 255 };
    public static readonly Color PURPLE = new Color { r = 200, g = 122, b = 255, a = 255 };
    public static readonly Color VIOLET = new Color { r = 135, g = 60, b = 190, a = 255 };
    public static readonly Color DARKPURPLE = new Color { r = 112, g = 31, b = 126, a = 255 };
    public static readonly Color BEIGE = new Color { r = 211, g = 176, b = 131, a = 255 };
    public static readonly Color BROWN = new Color { r = 127, g = 106, b = 79, a = 255 };
    public static readonly Color DARKBROWN = new Color { r = 76, g = 63, b = 47, a = 255 };
    public static readonly Color WHITE = new Color { r = 255, g = 255, b = 255, a = 255 };
    public static readonly Color BLACK = new Color { r = 0, g = 0, b = 0, a = 255 };
    public static readonly Color BLANK = new Color { r = 0, g = 0, b = 0, a = 0 };
    public static readonly Color MAGENTA = new Color { r = 255, g = 0, b = 255, a = 255 };
    public static readonly Color RAYWHITE = new Color { r = 245, g = 245, b = 245, a = 255 };

    public enum ConfigFlags : uint
    {
        VSYNC_HINT = 0x00000040,
        FULLSCREEN_MODE = 0x00000002,
        WINDOW_RESIZABLE = 0x00000004,
        WINDOW_UNDECORATED = 0x00000008,
        WINDOW_HIDDEN = 0x00000080,
        WINDOW_MINIMIZED = 0x00000200,
        WINDOW_MAXIMIZED = 0x00000400,
        WINDOW_UNFOCUSED = 0x00000800,
        WINDOW_TOPMOST = 0x00001000,
        WINDOW_ALWAYS_RUN = 0x00000100,
        WINDOW_TRANSPARENT = 0x00000010,
        WINDOW_HIGHDPI = 0x00002000,
        WINDOW_MOUSE_PASSTHROUGH = 0x00004000,
        BORDERLESS_WINDOWED_MODE = 0x00008000,
        MSAA_4X_HINT = 0x00000020,
        INTERLACED_HINT = 0x00010000
    }

}
