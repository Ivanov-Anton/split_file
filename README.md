# XlsSplitterWin7

WinForms app for Windows 7 (.NET Framework 4.0) that splits an Excel `.xls` file by the sign of values in column F.

## Features
- Select an `.xls` file via the UI.
- Splits rows into `*_positive.xls` and `*_negative.xls` based on column F.
- Ignores rows where column F is zero, empty, or non-numeric.
- Preserves the header row in both outputs.
- Uses NPOI (no Excel installation required).

## Build and Run (Windows 7 + Visual Studio)
1. Open `XlsSplitterWin7.sln` in Visual Studio 2010+.
2. Restore NuGet packages (right-click solution → **Restore NuGet Packages**).
3. Build the solution (Build → Build Solution).
4. Run `XlsSplitterWin7.exe` from `XlsSplitterWin7\bin\Release` or press **F5**.

## Usage
1. Click **Open XLS...** and select an `.xls` file.
2. Click **Split** to generate output files in the same folder:
   - `<original_name>_positive.xls`
   - `<original_name>_negative.xls`
3. A success message will display the output paths.

## NuGet
This project uses `packages.config` to restore the NPOI dependencies.
