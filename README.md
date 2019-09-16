# Testing an ASP.NET Core API

This repo is about to show how could we create unit tests and integration tests for an ASP.NET Core 3.0 API, using nothing else than xUnit, Moq and Microsoft.AspNetCore.TestHost.

In the folder 'database' you find the CREATE script of the expected table for SQL Server DB.

I am using EF Core to handle the DB stuff.

To run the test, go to the root path in your preferred CLI tool and run

`dotnet test`

It should be ALL GREEN! :D
