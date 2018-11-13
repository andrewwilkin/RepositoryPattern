# RepositoryPattern
A basic implementation of an In Memory Repository written in C#. This IS NOT a thread safe collection.

## Targeted Frameworks
The library is witten in .Net Standard so should be usable in either the .Net Core or .Net Full Frameworks

## Usage

Your model entity must inherit either inherit from the abstract Storeable class or implement IStorable<T>:

```csharp
public class Model : Storeable
{
   public string Message { get; set; }
}

public class AlternativeModel : IStoreable<Model>
{
   public IComparable Id { get; set; }
   public string Message { get; set; }
}
```


Add a reference to the namespace:

```csharp
using RepositoryPattern;
```

Initialize the in memory repository

```csharp
IRepository repository = new InMemoryRepository<Model>();
```


Add an item to the repository:

```csharp
repository.Save(new Model { Id = 1, "Message" });
```


Find an item to the repository:

```csharp
repository.FindById(1);
```

Get IEnumerable collection of all items in the repository:

```csharp
repository.All();
```

Delete an item to the repository:

```csharp
repository.Delete(1);
```


## Tests
A range of tests can be found in the RepositoryPattern.Tests project.
