@echo off
setlocal

:: Name of the new solution
set SOLUTION_NAME=Credential
set SOLUTION_FILE=%SOLUTION_NAME%.sln

:: Delete existing solution file if it exists
if exist %SOLUTION_FILE% (
    echo Deleting existing %SOLUTION_FILE%...
    del /f %SOLUTION_FILE%
)

:: Create a new solution
echo Creating new solution: %SOLUTION_FILE%...
dotnet new sln -n %SOLUTION_NAME%

:: Loop through all .csproj files under src\Credential and add them to the solution
for /r src\Credential %%f in (*.csproj) do (
    echo Adding project %%f to the solution...
    dotnet sln %SOLUTION_FILE% add "%%f"
)

echo Done!
endlocal
pause
