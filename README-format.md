# Using dotnet-format to Format .NET Code

This guide explains how to install and use the `dotnet-format` tool to format your .NET projects consistently.

## Prerequisites

- .NET SDK installed (version 6 or later recommended)

## Installation

To install `dotnet-format` as a global tool, run:

```bash
dotnet tool install -g dotnet-format
```

After installation, you can use the `dotnet format` command from any directory.

## Usage

### Format the Entire Solution

To format all projects in the solution `Csnp.sln`:

```bash
dotnet format Csnp.sln
```

### Format with Specific Severity

To apply formatting only to issues with `warn` severity (and above):

```bash
dotnet format --severity warn
```

This is useful to avoid formatting changes for informational or hidden diagnostics.

## Notes

- `dotnet-format` respects `.editorconfig` settings if available.
- You can run the tool in CI pipelines to enforce consistent code style.
- If you want to check formatting without making changes, add the `--verify-no-changes` flag.

## Example CI Usage

```bash
dotnet format --verify-no-changes --severity warn Csnp.sln
```

This command will fail if any formatting issues are found with severity `warn` or higher.

---

For more information, visit the official documentation: [https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format)

