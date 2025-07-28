@echo off
setlocal enabledelayedexpansion

:: Root solution and project settings
set ROOT_DIR=%cd%
set SHARED_PROJECT_DIR=%ROOT_DIR%\src\Notification\Csnp.Notification.Infrastructure.Persistence
set SHARED_PROJECT_NAME=Csnp.Notification.Infrastructure.Persistence
set INFRA_PROJECT_DIR=%ROOT_DIR%\src\Notification\Csnp.Notification.Infrastructure
set INFRA_PROJECT_NAME=Csnp.Notification.Infrastructure
set MIGRATIONS_PROJECT_DIR=%ROOT_DIR%\migrations\Csnp.Migrations.Notification
set MIGRATIONS_PROJECT_NAME=Csnp.Migrations.Notification
set SLN_NAME=Csnp.sln

echo ----------------------------------------
echo Creating project: %SHARED_PROJECT_NAME%
echo ----------------------------------------

:: Create project directory
if not exist "%SHARED_PROJECT_DIR%" mkdir "%SHARED_PROJECT_DIR%"

:: Generate class library
dotnet new classlib -n %SHARED_PROJECT_NAME% -o "%SHARED_PROJECT_DIR%" --force > nul 2>&1

:: Create common subfolders
mkdir "%SHARED_PROJECT_DIR%\Constants" > nul 2>&1
mkdir "%SHARED_PROJECT_DIR%\Extensions" > nul 2>&1
mkdir "%SHARED_PROJECT_DIR%\Interceptors" > nul 2>&1
mkdir "%SHARED_PROJECT_DIR%\Middlewares" > nul 2>&1
mkdir "%SHARED_PROJECT_DIR%\Options" > nul 2>&1
mkdir "%SHARED_PROJECT_DIR%\Time" > nul 2>&1

:: Add empty file to allow Git to track folders
echo. > "%SHARED_PROJECT_DIR%\Constants\.gitkeep"

:: Add the shared project to the solution
dotnet sln "%SLN_NAME%" add "%SHARED_PROJECT_DIR%\%SHARED_PROJECT_NAME%.csproj"

:: Add references to Infrastructure and Migrations projects
dotnet add "%INFRA_PROJECT_DIR%\%INFRA_PROJECT_NAME%.csproj" reference "%SHARED_PROJECT_DIR%\%SHARED_PROJECT_NAME%.csproj"
dotnet add "%MIGRATIONS_PROJECT_DIR%\%MIGRATIONS_PROJECT_NAME%.csproj" reference "%SHARED_PROJECT_DIR%\%SHARED_PROJECT_NAME%.csproj"

echo.
echo ✅ Successfully created and linked %SHARED_PROJECT_NAME%.
echo 📦 Added to solution: %SLN_NAME%
echo 🔗 Referenced by: %INFRA_PROJECT_NAME% and %MIGRATIONS_PROJECT_NAME%
