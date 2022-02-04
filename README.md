[TOC]

# Call SOAP web service through REST API in .NET 5



Tools:

- Visual Studio for Mac
- .NET 5





## SOAP web service

Call w3school [TempConvert](https://www.w3schools.com/xml/tempconvert.asmx) SOAP web service to covert *Celsius* with *Fahrenheit* temperature.

WSDL: <https://www.w3schools.com/xml/tempconvert.asmx?WSDL>



## Create SOAP web service client



### Create project

Create a Web API project via Visual Studio with uncheck https.

Delete the default generated WeatherForecast class.

Modify `Properties/laucnhSettings.json` to update all `launchUrl` as below:

```json
"launchUrl": "/",
```



### Generate SOAP web service client files

Use [dotnet-svcutil](https://docs.microsoft.com/en-us/dotnet/core/additional-tools/dotnet-svcutil-guide?tabs=dotnetsvcutil2x) to generate SOAP web service client files.

Install `dotnet-svcutil`:

```bash
dotnet tool install --global dotnet-svcutil
```



Generate SOAP web service client files:

```bash
# run on source root folder
dotnet-svcutil https://www.w3schools.com/xml/tempconvert.asmx?wsdl
```

Notes:

- Add `~/.dotnet/tools` in PATH env variable in case `dotnet-svcutil` command not found.



The generated file `Reference.cs` under `ServiceReference` folder, includes:

- `TempConvertSoap` interface
- `TempConvertSoapChannel` interface
- `TempConvertSoapClient` class



### Refactor the generated files

Extract codes from generated `References.cs` and refactor to :

- Interfaces
- Clients
- Repositories



#### Interfaces

Create an `Interfaces` folder, and create below interfaces:

- `ITempConvert` defines methods for *FahrenheitToCelsius* and *CelsiusToFahrenheit* SOAP web service actions
- `ITempConvertChannel` inherits from `ITempConvert` and `IClientChannel`
- `ITempConvertRepository` defines repository interface



#### Clients

Create a `Clients` folder, and create below class:

- `TempConvertClient` codes are extracted from `TempConvertSoapClient` class in `Reference.cs` class



#### Repositories

Create a `Repositories` folder and create below class:

- `TempConvertRepository` class which implements `ITempConvertRepository` interface



### Register repository in Startup class

Modify `Startup.cs` file to add below statements in `ConfigureServices()` method:

```c#
 // Configure your Temp client
services.Configure<ClientConfig>(
  Configuration.GetSection("TempConvert"));

// Add singleton
services.AddSingleton<ITempConvertRepository, TempConvertRepository>();
```



### Add client config

Modify `appsettings.json` to add SOAP web service client configuration:

```json
"TempConvert": {
    "Url": "https://www.w3schools.com/xml/tempconvert.asmx",
    "Timeout": 1000
  }
```



### Add client config model

Create a `Models` folder and create below class:

- `ClientConfig` defines properties of `TempConvert` in `appsettings.json`



## Call SOAP web service through REST API

### Create controller class

Create `TempController` class under `Controllers` folder.

Exposed APIs:

- `POST http://localhost:5000/api/temp/celsius` convert *fahrenheit* to *celsius*
- `POST http://localhost:5000/api/temp/fahrenheit` convert *celsius* to *fahrenheit*



### Create model class for request body

Create `ConvertRequest` class under `Models` folder. 



### REST API Test

Use Postman as REST API client to call SOAP web service throuh REST API.

#### Convert *fahrenheit* to celsius

API:

`POST http://localhost:5000/api/temp/celsius`



Request body (JSON):

```json
{
    "value": "100"
}
```



Repsonse body (JSON):

```json
{
    "success": true,
    "message": "100 degrees fahrenheit is == to 37.7777777777778 degrees celsius"
}
```



#### Convert *celsius* to *fahrenheit*

API:

`POST http://localhost:5000/api/temp/fahrenheit`



Request body (JSON):

```json
{
    "value": "100"
}
```



Repsonse body (JSON):

```json
{
    "success": true,
    "temp": "100 degrees celsius is == to 212 degrees fahrenheit"
}
```



## References

- [Consuming WSDL Services Using ASP.NET Core](https://medium.com/swlh/consuming-wsdl-services-using-asp-net-core-141fbc77924f)
- [GitHub | TempConvert](https://github.com/NimzyMaina/TempConvert)
