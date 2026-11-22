# RD3 BD7's Tools
Modding tools made to make mods for the game TOCA Race Driver 3 made by me, with the help of the research notes and reverse-engineering of the game files.
                                                       
## p3d2px
Made for converting the PSP RD3's P3D track model files to PC RD3's .px, this tool is still in development for now.

Build the solution with the .NET Framework 4.5 targeting pack. The command-line tool accepts separate input and output paths. Quote paths containing spaces.

```powershell
.\RD3_p3d2px.exe -p3d 'C:\Tracks\demo-track.p3d' -o 'C:\Tracks\demo-track.px' -c
```

The converter currently reports that conversion is unimplemented and exits with code 3. It leaves the output path untouched. Invalid arguments and missing input files return exit code 2. Use `--help` to view the options. The structures under `Shared/Models/` are research notes.
