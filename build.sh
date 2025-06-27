#/usr/bin/env bash

mcs Raylib.cs BrainFuck.cs BFCli.cs -out:BFCli.exe
mcs Raylib.cs BrainFuck.cs KettGui.cs BFGui.cs -out:BFGui.exe
