#/usr/bin/env bash

mcs Raylib.cs BrainFuck.cs BFCli.cs -out:BFCli.exe
mcs Raylib.cs BrainFuck.cs KettGui.cs BFGui.cs -out:BFGui.exe

if [[ "$1" == "runcli" ]] then
    mono BFCli.exe;
fi
if [[ "$1" == "rungui" ]] then
    mono BFGui.exe;
fi
