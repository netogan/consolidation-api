# Consolidation-Api

The consolidation-api module is responsible for processing the consolidation of the period entered.

## Prerequisites
- .Net 8

## Instalation

To install Winget (microsoft package manager) access the [link](https://learn.microsoft.com/pt-br/windows/package-manager/winget/#install-winget).


Install .Net 8 SDK.
```sh
winget install --id=Microsoft.DotNet.SDK.8  -e
```

Build aplication in solution directory
```sh
dotnet build .\Consolidation.Api.sln
```

Run aplication in solution directory in project directory
```sh
dotnet run --project .\src\Consolidation.Api\Consolidation.Api.csproj
```