# Framework Repository

A class library that implements the generic repository pattern using Entity Framework Core as base.


Usage
=====

First register repositories interfaces and classes in dependency injection using the automatic registration method.

```csharp
    builder.Services.AddRepositories();
```

**Note:** All repositories are registered with a scoped lifetime, to change that you can use the optional parameter and assign a lifetime from these options (Transient, Scoped And Singleton).

```csharp
    builder.Services.AddRepositories(ServiceLifetime.Transient);
```

After that you can use the repository interfaces through constructor injection.

```csharp
    public Test(IReadOnlyRepository<User, DefaultDbContext> readonOnlyRepository,
                IRepository<User, DefaultDbContext> repository,
                ITransactionRepository<User, DefaultDbContext> transactionRepository,
                IRawRepository<DefaultDbContext> rawRepository)
    {
    }
```

The `User` type is an Entity Framework Core POCO class which represents a table,
while `DefaultDbContext` is the DbContext which will be requested by the repository when created by the IOC.

Each repository interface have it's own usage.<br/><br/>

The `IReadOnlyRepository<>` can only retrieve data.<br/><br/>

The `IRepository<>` can retrieve data and add/update/delete entities.<br/><br/>

The `ITransactionRepository<>` can do the same functions as `IRepository<>` but with two extra methods `SaveChanges()` which commits data and `CreateTransaction()` which creates a transaction (if non exists) and returns it.<br/><br/>

The `IRawRepository<>` has different methods that receives a string command/procedure and a list of parameters to execute.