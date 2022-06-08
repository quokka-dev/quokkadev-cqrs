[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=quokka-dev_quokkadev-cqrs&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=quokka-dev_quokkadev-cqrs)

# QuokkaDev.CQRS 
A package for apply CQRS pattern in .NET projects and add cross cutting concerns

QuokkaDev.CQRS is a wrap around [MediatR](https://github.com/jbogard/MediatR/wiki). According to [CQRS Design Pattern](https://martinfowler.com/bliki/CQRS.html) it allow a clear distinction between commands and query. It is inspired by this [article](https://cezarypiatek.github.io/post/why-i-dont-use-mediatr-for-cqrs)

## Installing QuokkaDev.Cqrs
    
You should install the packages via the .NET Core command line interface:

    dotnet add package QuokkaDev.Cqrs    

The commands, will download and install QuokkaDev.Cqrsand all required dependencies.

## Register handlers in D.I. Container
Call the extension method `IServiceCollection.AddCQRS()` passing an array of assemblies to scan for search command and query handlers. If no assembly are passed to the extension method the current executing assembly will be used

#### **`program.cs`**
```csharp
using QuokkaDev.Cqrs;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure services
// ...

//Add CQRS infrastructure.
builder.Services.AddCQRS(Assembly.GetExecutingAssembly());


var app = builder.Build();

// Configure middleware

app.Run();
```
## Commands

### Create the command and the result
#### **`OrderCreate.cs`**
```csharp
using QuokkaDev.Cqrs;

namespace QuokkaDevCqrsDemoApi
{
    public class OrderCreateCommand
    {
        public string CustomerName { get; set; } = "";
        public int ItemId { get; set; }
        public decimal ItemQuantity { get; set; }
    }

    public class OrderCreateResult
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
    }
}
```

### Create an handler for the command
#### **`OrderCreateCommandHandler.cs`**
```csharp
using QuokkaDev.Cqrs;

namespace QuokkaDevCqrsDemoApi
{
    public class OrderCreateCommandHandler : ICommandHandler<OrderCreateCommand, OrderCreateResult>
    {
        public Task<OrderCreateResult> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            // Create the order and persist it on database
            // ...
            // ...

            return Task.FromResult(new OrderCreateResult() { Id = 1, Created = DateTime.Now });
        }
    }
}
```

### Use the CommandDispatcher for dispatch commands to the right handler

#### **`OrderController.cs`**
```csharp
using QuokkaDev.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace QuokkaDevCqrsDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;

        public OrderController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]OrderCreateCommand orderCreateCommand)
        {
            OrderCreateResult result = await commandDispatcher.Dispatch<OrderCreateCommand, OrderCreateResult>(orderCreateCommand);
            return Ok(result);
        }
    }
}
```

## Queries

### Create the query and the result
#### **`OrderCreate.cs`**
```csharp
using QuokkaDev.CQRS;

namespace QuokkaDevCqrsDemoApi
{
    public class AllOrdersQuery
    {
    }

    public class AllOrdersQueryResult
    {
        public OrderDTO[]? OrderDTOs { get; set; }
    }

    public class OrderDTO
    {
        public int Id { get; set; }
        public string Customer { get; set; } = "";
    }
}
```

### Create an handler for the query
#### **`AllOrdersQueryHandler.cs`**
```csharp
using QuokkaDev.Cqrs;

namespace QuokkaDevCqrsDemoApi
{
    public class AllOrdersQueryHandler : IQueryHandler<AllOrdersQuery, AllOrdersQueryResult>
    {
        public Task<AllOrdersQueryResult> Handle(AllOrdersQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new AllOrdersQueryResult() { 
                OrderDTOs = new OrderDTO[] { 
                    new OrderDTO() { Id = 1, Customer = "MyCustomerName" }
                } 
            });
        }
    }
}
```


### Use the CommandDispatcher for dispatch commands to the right handler

#### **`OrderController.cs`**
```csharp
using QuokkaDev.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace QuokkaDevCqrsDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IQueryDispatcher queryDispatcher;

        public OrderController(IQueryDispatcher commandDispatcher)
        {
            this.queryDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> AllOrders()
        {
            AllOrdersQueryResult result = await queryDispatcher.Dispatch<AllOrdersQuery, AllOrdersQueryResult>(new AllOrdersQuery());
            return Ok(result);
        }
    }
}
```
