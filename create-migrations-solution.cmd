@echo off
setlocal

:: Solution name for EF Core migrations
set SOLUTION_NAME=Credential.Migrations
set SOLUTION_FILE=%SOLUTION_NAME%.sln

:: Delete existing solution file if it exists
if exist %SOLUTION_FILE% (
    echo Deleting existing %SOLUTION_FILE%...
    del /f %SOLUTION_FILE%
)

:: Create the new solution
echo Creating new solution: %SOLUTION_FILE%...
dotnet new sln -n %SOLUTION_NAME%

:: Define project paths (without src\Credential prefix)
set PROJECTS=^
migrations\Csnp.Migrations.Credential\Csnp.Migrations.Credential.csproj ^
Csnp.Credential.Infrastructure\Csnp.Credential.Infrastructure.csproj ^
Csnp.Credential.Application\Csnp.Credential.Application.csproj

:: Add projects to the solution
for %%f in (%PROJECTS%) do (
    if exist %%f (
        echo Adding project %%f to the solution...
        dotnet sln %SOLUTION_FILE% add "%%f"
    ) else (
        echo WARNING: Project %%f not found!
    )
)

echo Done!
endlocal
pause
