@echo off
setlocal enabledelayedexpansion

:: Solution name
set SOLUTION_NAME=Csnp
dotnet new sln -n %SOLUTION_NAME%

:: Directories
set ROOT_DIR=%cd%
set SRC_DIR=%ROOT_DIR%\src
set TEST_DIR=%ROOT_DIR%\tests
set SHARED_DIR=%ROOT_DIR%\shared
set MIGRATIONS_DIR=%ROOT_DIR%\migrations

:: Helper to create classlib
call :create_classlib %SHARED_DIR%\Csnp.Shared.Domain
call :create_classlib %SHARED_DIR%\Csnp.Shared.Application
call :create_classlib %SHARED_DIR%\Csnp.Shared.Infrastructure

:: Credential Bounded Context
call :create_webapi %SRC_DIR%\Credential\Csnp.Credential.Api
call :create_classlib %SRC_DIR%\Credential\Csnp.Credential.Application
call :create_classlib %SRC_DIR%\Credential\Csnp.Credential.Domain
call :create_classlib %SRC_DIR%\Credential\Csnp.Credential.Infrastructure

:: Notification Bounded Context
call :create_webapi %SRC_DIR%\Notification\Csnp.Notification.Api
call :create_classlib %SRC_DIR%\Notification\Csnp.Notification.Application
call :create_classlib %SRC_DIR%\Notification\Csnp.Notification.Domain
call :create_classlib %SRC_DIR%\Notification\Csnp.Notification.Infrastructure

:: Presentation
call :create_mvc %SRC_DIR%\Presentation\Csnp.Presentation.Web

:: Migrations
call :create_classlib %MIGRATIONS_DIR%\Csnp.Migrations.Credential
call :create_classlib %MIGRATIONS_DIR%\Csnp.Migrations.Notification

:: Tests
call :create_test %TEST_DIR%\Csnp.Credential.Tests.Unit
call :create_test %TEST_DIR%\Csnp.Credential.Tests.Integration
call :create_test %TEST_DIR%\Csnp.Notification.Tests.Unit
call :create_test %TEST_DIR%\Csnp.Notification.Tests.Integration

:: Add all projects to solution
for /R %%f in (*.csproj) do (
    dotnet sln %SOLUTION_NAME%.sln add "%%f"
)

:: Add references for Credential
dotnet add %SRC_DIR%\Credential\Csnp.Credential.Api\Csnp.Credential.Api.csproj reference ^
    %SRC_DIR%\Credential\Csnp.Credential.Application\Csnp.Credential.Application.csproj

dotnet add %SRC_DIR%\Credential\Csnp.Credential.Application\Csnp.Credential.Application.csproj reference ^
    %SRC_DIR%\Credential\Csnp.Credential.Domain\Csnp.Credential.Domain.csproj ^
    %SHARED_DIR%\Csnp.Shared.Application\Csnp.Shared.Application.csproj

dotnet add %SRC_DIR%\Credential\Csnp.Credential.Infrastructure\Csnp.Credential.Infrastructure.csproj reference ^
    %SRC_DIR%\Credential\Csnp.Credential.Domain\Csnp.Credential.Domain.csproj ^
    %SHARED_DIR%\Csnp.Shared.Infrastructure\Csnp.Shared.Infrastructure.csproj

:: Add references for Notification
dotnet add %SRC_DIR%\Notification\Csnp.Notification.Api\Csnp.Notification.Api.csproj reference ^
    %SRC_DIR%\Notification\Csnp.Notification.Application\Csnp.Notification.Application.csproj

dotnet add %SRC_DIR%\Notification\Csnp.Notification.Application\Csnp.Notification.Application.csproj reference ^
    %SRC_DIR%\Notification\Csnp.Notification.Domain\Csnp.Notification.Domain.csproj ^
    %SHARED_DIR%\Csnp.Shared.Application\Csnp.Shared.Application.csproj

dotnet add %SRC_DIR%\Notification\Csnp.Notification.Infrastructure\Csnp.Notification.Infrastructure.csproj reference ^
    %SRC_DIR%\Notification\Csnp.Notification.Domain\Csnp.Notification.Domain.csproj ^
    %SHARED_DIR%\Csnp.Shared.Infrastructure\Csnp.Shared.Infrastructure.csproj

:: Presentation references
dotnet add %SRC_DIR%\Presentation\Csnp.Presentation.Web\Csnp.Presentation.Web.csproj reference ^
    %SRC_DIR%\Credential\Csnp.Credential.Application\Csnp.Credential.Application.csproj ^
    %SRC_DIR%\Notification\Csnp.Notification.Application\Csnp.Notification.Application.csproj

goto :eof

:: Functions
:create_classlib
dotnet new classlib -n %~nx1 -o %1
goto :eof

:create_webapi
dotnet new webapi -n %~nx1 -o %1
goto :eof

:create_mvc
dotnet new mvc -n %~nx1 -o %1
goto :eof

:create_test
dotnet new xunit -n %~nx1 -o %1
goto :eof
