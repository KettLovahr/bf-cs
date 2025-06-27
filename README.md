A [Brainfuck](https://en.wikipedia.org/wiki/Brainfuck) interpreter written in C# (Mono)

This interpreter reserves 4096 bytes of memory and the memory cursor wraps around when moving out of bounds

To compile, install Mono, then run `build.sh`. This will create `BFCli.exe` and `BFGui.exe`, these can then be run with `mono`, `wine`, or, in Windows, directly via a double-click. The GUI component depends on [raylib](https://github.com/raysan5/raylib/), so make sure the library is available when running.