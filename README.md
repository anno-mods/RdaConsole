# RdaConsole

A lightweight console repacker for Ubisoft RDA files.

This is entirely based on https://github.com/lysannschlegel/RDAExplorer, I am just adding console commands over it. 

Supported RDA Versions: 2.0 (1404/2070), 2.2 (2205/1800)

Usage: 

```
RdaConsole.exe [-v number] [-f multiple filenames] [-y] [-o output_filename] [-r recursive_search]
```

- -y overwrites the output file
- -v chooses a version: 1 for RDA 2.0, 2 for RDA 2.2
- -r will include subdirectories of directories.
- you can specify multiple filenames after -f
