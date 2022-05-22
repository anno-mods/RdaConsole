# RdaConsole

A lightweight console unpacker and repacker for Ubisoft RDA files.

This is entirely based on https://github.com/lysannschlegel/RDAExplorer, I am just adding console commands over it. 

Supported RDA Versions: 2.0 (1404/2070), 2.2 (2205/1800)

Usage for packing: 

```
RdaConsole.exe pack [-v number] [-f multiple filenames] [-y] [-o output_filename] [-r recursive_search]
```

- -y overwrites the output
- -v chooses a version: 1 for RDA 2.0, 2 for RDA 2.2
- -r will include subdirectories of directories.
- you can specify multiple filenames after -f

sample usage: https://gist.github.com/taubenangriff/28e93ead630678ce0314e5a6cdbed209

Usage for extracting: 

```
RdaConsole.exe extract [-f multiple filenames] [-y] [-o output_foldername]
```

-y overwrites the output folder
-o sets the output folder name
- you can specify multiple filenames after -f
