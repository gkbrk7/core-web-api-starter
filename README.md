# Core-Web-Api-Starter

A Simple .NET 5 API to build personal or commercial applications easily. This minimal project covers is:

- Introduction to Web API
- Building your first `Asp.Net Core (.NET 5) API`
- Working with relational data
- Controller Action return types
- Sorting, Filtering, and Paging
- `Asp.Net Core Web API` Versioning
- Unit Testing

# Tech Stack

1. Asp .Net Core (.NET 5) API
2. Serilog for logging
3. Microsoft MVC default versioning
4. Microsoft SQL Server
5. Entity Framework
6. NUnit for unit testing

# Api Versioning

> Install _`Microsoft.AspNetCore.Mvc.Versioning`_ package from nuget and add code snippet into _**`Startup.cs`**_ below.

```C#
services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0); // Default versioning number major,minor e.g. (1.0)
    config.AssumeDefaultVersionWhenUnspecified = true; // Assume default versioning as v1.0

    // config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header"); // header based api versioning
    // config.ApiVersionReader = new MediaTypeApiVersionReader(); // content/media type api versioning, to use add Content-Type text/plain;v=2.0 to header and send request
});
```

> Api versioning canbe used or integrate as four different way like `Query String Based, Url Based, HTTP Header-Based and HTTP Media-Type Based`.

- Query String Based Versioning
  - Use `[ApiVersion("1.0")]` attribute to specify api version
  - To call api, send request to [https://....?api-version=1.0](#)
- Url Based Versioning
  - Use `[Route("api/v{version:apiVersion}/[controller]")]` attribute to configure Url Based versioning
  - Create sub folders into Controller directory like v1, v2 and create Controller into these sub folders
- HTTP Header-Based Versioning
  - Add `config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header");` settings into `Startup.cs` to use HTTP Header-Based Versioning.
  - You can write header parameter whatever you want. In this case, `custom-version-header` parameter will be key and value would be `1.0, 1.1 or 2.0`.
  - You must also specify `[ApiVersion("1.0")]`. Note that **ApiVersion** annotation canbe used more than one.
- HTTP Media-Type Based Versioning
  - Add `config.ApiVersionReader = new MediaTypeApiVersionReader();` settings into `Startup.cs` to use HTTP Media-Type Based Versioning
  - To call api, add `Content-Type` header and set its value like `text/plain;v=2.0` before sending request to [https://..../api/[controller]/[action]](#)
  - Note that api version is set `2.0`.

# Logging

> Install _`Serilog.AspNetCore`_, _`Serilog.Sinks.MSSqlServer`_ and _`Serilog.Sinks.File`_ packages from nuget.
> Change `Program.cs` like this to use Serilog. Try-catch block is not obligatory,

```C#
public static void Main(string[] args)
{
    try
    {
        Log.Logger = new LoggerConfiguration().CreateLogger();

        CreateHostBuilder(args).Build().Run();
    }
    finally
    {
        Log.CloseAndFlush();
    }

}

public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
```

- Logging to a File using Serilog

  - Change default configuration in `Program.cs` with below code

  ```C#
  Log.Logger = new LoggerConfiguration().WriteTo.File("Logs/logs.txt").CreateLogger();
  ```

  - If we specify `rollingInterval: RollingInterval.Day` where being an additional parameter for File method, for instance, it means every day Serilog will create a new log file similar to `logs20210319.txt`.
  - We can specify configuration settings in `appsettings.json` or `appsettings.Development.json`.

  ```JSON
  {
    "Serilog":{
      "MinimumLevel": {
        "Default": "Information",
        "Override": {
          "System": "Error",
          "Microsoft": "Error"
        }
      },
      "WriteTo" : [
        {
          "Name": "File",
          "Args": {
            "path": "Logs/log.txt",
            "rollingInterval": "Day",
            "outputTemplate": "{Timestamp} [{level}] - Message: {Message}{NewLine}{Exception}"
          }
        }
      ]
    }
  }
  ```

  - To read configuration from json settings file add/change `Program.cs` with below code snippet:

  ```C#
  var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
  Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
  ```

- Logging to an SQL Table using Serilog

  - Add below code into `WriteTo` section in `appsettings.json` or `appsettings.Development.json`

  ```JSON
  {
    "Name": "MSSqlServer",
    "Args": {
      "connectionString": "server=.;database=MyBooksDb;uid=sa;pwd=123",
      "tableName": "Logs"
    }
  }
  ```

# Authentication and Authorization

# Unit Testing

> Install
