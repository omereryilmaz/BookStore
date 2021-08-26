# BookStore - RESTful API using .NET 5

This API project has been made for to learn .NET 5.  In addition, the tutorial in patika.dev was benefited for this sample project. There are the following topics in the project.
* Using Swagger documentation
* LINQ CRUD operations
* View Model
* Auto Mapper
* Fluent Validation
* Dependency Injection
* Custom Exception Middleware
* Unit Test 
* Authentication - JWT



## Installation

You need to download and install .NET 5.0 framework for run this project. 

[Download .NET 5.0](https://dotnet.microsoft.com/download)

Not sure where to start? See the [Hello World in 5 minutes tutorial](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/intro) to install .NET and build your first app.



#### Check everything installed correctly

Once you've installed, open a new command prompt and run the following command:

```
dotnet
```
 
## Usage

First you should install project dependency packages. To install the packages you should be in `../BookStore/WebAPI` directory. Then run the following command on command prompt:

```
dotnet restore
```

Second you should add `app.settings.json` file to  `../BookStore/WebAPI` directory. Because WebAPI gets the SecurityKey from there.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "Token":{
    "Issuer":"www.test.com",
    "Audience": "www.test.com",
    "SecurityKey": "This is my custom secret key for authentication."
  }
}
```

To run the project:

```bash
dotnet watch run
```

You can use Swagger, [Postman](https://www.postman.com/) or [REST Client](https://github.com/Huachao/vscode-restclient) VSCode extension for api requests.

## Swagger ScreenShot
![Image of Swagger](ReadmeImages/BookStore_swagger.png)