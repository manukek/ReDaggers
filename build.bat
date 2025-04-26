@echo off
set DOTNET_PATH="C:\Program Files\dotnet\dotnet.exe"
set TMODLOADER_PATH="%USERPROFILE%\Documents\My Games\Terraria\tModLoader\tModLoader.dll"

if exist %TMODLOADER_PATH% (
    %DOTNET_PATH% %TMODLOADER_PATH%
) else (
    echo Error: tModLoader.dll not found at %TMODLOADER_PATH%
    echo Please check if the path is correct
    echo Trying to find tModLoader.dll...
    dir /s /b "%USERPROFILE%\Documents\My Games\Terraria\tModLoader\*.dll"
    pause
)
