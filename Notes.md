# Introduction to ASP.NET Core
### History
MS Created .NET Framework => 2001  
1,1.1,2,3,3.5,4,4.5, 4.6, 4.7, 4.8
Windows centric =>
2009 => MVC 1.0 in .NET Framework
2013 => MVC 5.3

### .NET Core
2016 => .NET Core 1.0 
1. Cross Platform => Windows/Mac/Linux/Android/iOS/tvOS
2. X64, ARM32/64
3. Open Source => GitHub

#### .NET Release Cycle - November months
2017 => .NET Core 2.0
2018 => .NET Core 2.2
2019 => .NET Core 3.0
2020 => .NET 5
2021 => .NET 6 , ASP.NET Core ( Web Apps, Web API), Entity Framework Core (ORM) => communicate with database

#### ASP.NET
1. MVC => Model View Controller (Web Applications)
2. Web API => REST API over HTTP , return JSON Data

#### MVC
1. Model => Show list of employees in a page => List<Employee> 
2. View => UI Browser => HTML+CSS+JS+ Razor Syntax(ASP.NET Specific)
3. Controller => Logic is there => Each and Every request in MVC should go through controller

Whenever you make any HTTP Request (GET, POST, PUT and DELETE) to server => ASP.NET Server and it will respond back with RESPONSE
Response => HTML, CSS, JS => ALL UI Elements (Buttons, Dropdowns, Radio BUttons, CheckBOxex, Textboxes)


1. GET => http://www.example.com/employee/details/22  ASP.NET MVC
   1. First Part after domian name is Controller in MVC
   2. Second Part is action method
   3. Route Paramter/data
2. GET =>  http://www.example.com/employee/create => Show a web page so that user can enter Employee details
3. POST => http://www.example.com/employee/create => Submitting 
Controller is a C# class in MVC but it derives from Controller class

```csharp
public class EmployeeController: Controller
 {
     [HttpGet]
     public IActionResult Details(int id) 
     {
          //  go to database and get employee details by id
          Employee employee = new EmployeeService().GetDetails(id);
         // return a view
         return View(employee);
     }

    [HttpGet]
     public IActionResult Create()
      {
          // empty view for user to enter Employee information
          return View();
      }

    [HttpPost]
     public IActionResult Create(Employee emp) 
     {
         // save the employee to database and return view
         return View();
     } 
 }
```

#### Onion/Clean/Layered Architecture

Make sure Code should properly structured and should be maintenable and testable and reuse the code.
Loosley Coupled Code => Interfaces with Depedency Injection

1. ApplicationCore =>  Base of the project, Class Library
   1. Entities => Objects/Classes that represent our database tables
      1. Entities will have all the properties of that table
      2. Movie { 20 properties}
   2. Models => Objects/Classes that we use for our View
      1. MovieCardModel { 3 properties => Id, Title, PosterUrl }
      2. MovieDetailsModel => {Models based on View Requirements}
   3. Contracts (Interfaces)
      1. Repository Interfaces
      2. Services Interfaces 
   4. Helper => Heler or Untility classes
   5. Exceptions => Custom Exception classes such as MovieNotFoundException, InvalidDataException

2. Infrastructure => Class Library, Refereces Application Core Project
   1. Implementation of the Repository and Services Contracts
   2. Repostories => Persistence (dataabse logic) EF Core, Dapper, ADO.NET
   3. Services => Business Logic => Purchasing Movie, Hashing Password
      1. Servcies Will comminucate with Repositories which will communicate with database
      2. Additional Helpers

3. MVC (Web) => UI Layer, Dependecy on Infrastructure and ApplicationCore
   1. Controllers
   2. Views


IN .NET 6 ASP.NET Project we have Program.cs file
In previous versions of .NET (5,3,2) => we had both Program.cs and Startup.cs files

IN Startup.cs we had two very important methods (older .NET)
1. ConfigureServices => Registering Depedencies
2. Configure => Middlewares


``` cs
public class GenreService 
{
}

public class MovieService : IMovieService
{
}
var gerneService = new  new GenreService();
var movieService = new  new MovieService();

Method2(int y, IMovieService service) => Method2(4,movieService);
```

Razor Syntax will allow you to combine C# and HTML in the View, they are called Razor Views
Model will encapsulate all the data/model that beding passed by Action method
 3 ways we can pass the data from COntroller/Action methods to the Views
            // 1. *** Pass the Strongly Typed Models ***
            // 2. ViewBag => dynamic
            // 3. ViewData => object key/value

Always use Strongly Typed Models, use ViewBag/ViewData only when your model does not satify View needs
You can use them to send small additional information along with Model

### Depedency Injection
DI is a design Pattern that helps us in writing loosly coupled code and .NET Core has built-in support for DI
WE can register our dependecies/services in Program.cs file builder.Services.AddScoped<IMovieService, MovieService>();
But in .NET Core versions less than 6 we register DI in Startup.cs file in ConfigureServices method
   1.Constructor Injection
   2.Method Injection
   3.Property Injection

1. Create an abstarction, an interface
2. Implement the interface
3. Make sure the higher level classes (Controllers) reference the interface rather than the lower level classes
4. Create a readonly private field and inject the implementation with conctrutor Injection
5. Make sure you specify the Injection for interface in Program.cs (.net 6) or Startup.cs ConfigureServices method ( .NET 5)

For Advacned DI scenarios we rely on 3rd party IOC libraries such as Autofac, Ninject

### Entity Framework Core - 6

1. Executing SQL Statements
2. Executing Stored Procedures
3. Executing Views/Functions

Repositories deal with data access logic

1. ADO.NET => It takes lots of code and repeated code. ITs very fast perfoamce wise, its takes lots
 of developer times
  1. SQLConnection
  2. SQLCommand
2. Dapper - Stackoverflow, micro ORM => it takes SP, SQL  commands and executes them maps them with C# objects 
3. Entity Framework Core 6 => Cross platform, supports many databases, Full ORM
  1. With EF Core, we can create database using just C# code with Entities and concept called Migrations
  2. Querying can be done on those Entities using LINQ ann then EF Core will transalte the LINQ to SQL Code and send it to 
     Database
  3. Save the data using EF
  4. Execute SQL Statements, Store Procedures also

  Code - First Approach, we write code and create database
  Database - First approach

  EF Core has two most important classes
  1. DbContext => represents your database
  2. DbSet => represents your tables

  #### Steps for creating Databse using Code-First Approach with EF Core

  1. Install required packages from Nuget. 
    1. add package Microsoft.EntityFrameworkCore.SqlServer  inside Infra and Web Project
  2. There are 2 ways to work with Migrations in EF Core, 
    1. dotnet ef command line tools => Install dotnet tool install --global dotnet-ef
    2. Using Visual Studio (people who are familiar with .NET Framwork and Visual Studio), insatall a package per project
       install Microsoft.EntityFrameworkCore.Tools if (Visual Studio in Windows)
  3. Specify the connection string that you wanna use, Server name, Database name, any un/pw 
  4. Create a class that inherits from DbContext
  5. Establish connection string with DbContext
  6. Create your first Entity Class (Genre) that represents your table through DbSet as property of DbContext
  7. Use Migrations to create the Database and table 
      Always check the Migration file before updating the database
      1. Add-Migration InitialMigration
      2. Update-database
  8. Never change database schema when you are working with Code First approach, always change the Entity and use Migrations
  9. In EF we have two options to model our database 
    1. Data Annotations
    2. Fluent API - more powerful


### async/await

1. Every request that comesa sfrom client is processes by a thread => worker in factory
2. Threads are not infinite, e have manage/unitlize the exisitng threads properly

ASP.NET has Threadpool => (Collection of threads  T1 to 100 ) => Memory
Factory => W1 to W100 => Money 

 User 1 =>  10:00 AM  localhost:/home/index   T1 1 seec, 2sec, 100 ms, 5 sec
 User 2 =>  10:00 AM  localhost:/home/index   T2  
 User 3 =>  10:00 AM  localhost:/home/index 
 User 4 =>  10:00 AM  localhost:/home/index 
 ...
 ...
 User 100 =>  10:00 AM  localhost:/home/index T100

 User 101 =>  10:00 AM  localhost:/home/index 
 // I/O bound operation => Network and going somewhere database is located and get the data
            // 1 sec, 100 ms, 10 ms, 5 sec
            // Thread is waiting for operation to finish unnessar
            // Thread Starvation
            // I/O bound operations => Database calls, Network calls, File, 
            // CPU Bound => IMage resizing, calualte the PI numner, Loan , algorith


sync code  => int, List<Movie>, void 
async/await => Task<T>, Task<int>, Task<List<Movie>>, Task

async/await were introduced in 2012 in C# 5

If you wanna re-use certain UI in multiple views
Partial View will get data from the parent view


Home Page => MovieCard UI
User Purchased Movies => Movie Card UI
User Favorited Movies => Movie Card UI
Movies By Genre => Movie Card UI

### Authentication and Authorization

#### Registration Process
1. Log in to website we need email/password
2. Register email/password in the system
   1. We need a View Register (FirstName, LAstName, Email, Dof, Password), submit button
   2. POST => to the AccountController/Register 
   3. User table will be used to save information
      1. Encryption => Decrypt back to original string
      2. Hashing => One-Way
      3. Hashing with Salt(unique randomly generated string)
3. U1 => abc@abc.com (AAbc123!!+ fjslkfjsjf) => Hashing ALg1 => hgokldfglkfjklsdjdflksdjflksj
4. U2 => xyz@xyz.com (AAbc123!!+ hvksdhfndsk) => Hashing ALg1 => sdkjdgfjdshfjdshfjkdshfksjdhfdfsyfiosdh

#### Login Process
1. Create Login View => email/password and submit button
2. POST AccountController/Login
   1. take the password from the user and combine with salt from database and compare the newly created hash with database hash
   2. IF both hashes are equal then user entered right password
   3. We create a cookie (stored in browser), each and every time you make a request the cookies are sent to server from browser.
   4. Store some user related information so that that cookie is valid for certain time => 10min, 30min, 20 days
3. localhost:54345/user/purchases => Get all the Movies that user purchased
4. Cookie based Authentication


#### Layout
Layout is used for Navigation, Top Nav Bar

View => _ViewStart => Layout View
RenderBody() is Placeholder for the actual view

#### ViewComponents in ASP.NET Core MVC



# 3 major Cloud Providers

1. AWS
2. Azure

 anaging Infrastrure for any company is not easy

#Cloud Servcies
1. IaaS (Infrastructure as a service)

=> VM, DataCenters => 

2. ** Platform as a Service **
	
    1. SQL server => Azure SQL
    2. IIS (deploy ur MVC or Angular) => Azure App Service	
    3. Azure Functions
    4. Azure Storage => IMages, Files. Videos
    5. Azure Cognitive Services => Sentiment analysis => Image classification, Face API
    6. Azure DevOPs


DevOps => Development + Operations

CI/CD => Continous Integration & Continuous Deploment/Deliver

Team of 5 => MovieShopMVC

GitHub => checkin code frequently =>

CI will take the code from GitHub and it will build the project with Agent and also run any unit test cases for project
will package the code ans stores it	

CD => takes the code and deploy it to Azure App Service => MovieShopMVC => change the connectionstring to point to Azure SQL

#######
mVVLwV83czSe$Wn   sql-password-username-zheng

Cannot open server 'zhengservers' requested by the login. Client with IP address '20.49.104.54' is not allowed to access the server.
  To enable access, use the Windows Azure Management Portal or run sp_set_firewall_rule on the master database to create a firewall
 rule for this IP address or address range.  It may take up to five minutes for this change to take effect. 


#API => Application Programming Interface

Web Services => SOAP based XML based and SOAP Protocol (old)
              => REST architecture pattern => HTTP => JSON data

Company XYZ

.NET Team => Movie Shop C# **ASP.NET MVC** => SQL Server Database, API => that exposs JSON data over HTTP
 
ASP.NET API
GET http://movieshop/api/movies/toprevenue => JSON data
 POST http://movieshop/api/account

Java Team

iOS Team => Swift => iOS => to revenue movie =>  

    http://movieshop/api/movies/toprevenue 


Android Team => Java

Front-End Team => React/Angular
               website => Angular =>
















