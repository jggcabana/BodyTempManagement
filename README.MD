
# BodyTemperature - Summary of Submission

This is my submission for the Body Temperature coding assessment. 
 - Simple .NET 6 WebAPI with `GET`, `POST`, AND `PUT` endpoints to manage employee data and temperatures.
 - Uses EF Core 7 for the persistence layer.
 - Output can be viewed through the `GET employees` request , after making a request to `POST employees/{employeeId}/temperature`.
 - Logging is implemented by leveraging the 3rd party .NET library [Serilog](https://serilog.net/).
 - A global error handler is used, implemented via middleware in the API layer.
 - Uses Onion Architecture
 - Unit tests included

## Technologies and Tools Used:
- .NET 6 WebAPI
-  EF Core 7
- VS 2022, SSMS 18

## Developer Notes - Other Considerations
- Implementing a custom logging project was considered (logging as a separate assembly, so that it can be packaged in NuGet and used in a distributed system), but was considered over-engineering for the purpose of this project.

## Local Development Setup

### Prerequisites

- .NET 6 Runtime: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
  - Download the version for your OS.
- Database : Sql Server: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
  - Download express for quick-setup local development / testing.

### Automated Local Setup
- If you are running windows, you can run an automated batch file located in the root folder of the application. Convert the file by changing the .txt extension to .bat.
```
local-machine-setup.txt --> local-machine-setup.bat 
```
- Take note that installing .NET 6 runtime is a prerequisite, and installation of .NET 6 is not part of the automated process (mainly due to needing a system restart).
- Alternatively, you can follow the below steps to setup local development manually.

### Migrations
- Requires dotnet ef CLI Tools : https://docs.microsoft.com/en-us/ef/core/cli/dotnet
- Install using terminal (requires .NET runtime)
```
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```
- In the root folder (containing the .sln file), run the following in the terminal: 
```
dotnet ef database update -p "./BodyTemp.Repositories" -s "./BodyTemp.WebAPI"
```
### How to test
- In the root folder (containing the .sln file), run the following in the terminal: 
```
dotnet test 
```
### How to run
- In the root folder (containing the .sln file), run the following in the terminal:
- API:
```
dotnet run --project "./BodyTemp.WebAPI"
```
### View Swagger Doc
- While running, open a browser and navigate to https://localhost:7277/swagger


