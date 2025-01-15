using DBwithEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace DBwithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CurrencyController(AppDbContext appDbContext)  // Dependency Injection
        {
            _context = appDbContext;
        }


        //[HttpGet("")]
        //public IActionResult GetAllCurrencies() 
        //{
        //    //var currencies = _context.Currencies.ToList(); // Getting all curriences using LINQ
        //    var currencies = (from cur in _context.Currencies select cur).ToList(); // LINQ like SQL functions same as above line
        //    return Ok(currencies);
        //}


        // Asynchronous call best method to call api becuase increses performance
        [HttpGet("")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            //var currencies = await _context.Currencies.ToListAsync(); // Getting all curriences using LINQ
            var currencies = await (from cur in _context.Currencies select cur).ToListAsync(); // LINQ like SQL functions same as above line
            return Ok(currencies);
        }


        [HttpGet("{id:int}")] // mentioned int to avoid ambguity with name of below method other we face error 
        public async Task<IActionResult> GetAllCurrenciesAsync([FromRoute] int id)
        {
            var currencies = await _context.Currencies.FindAsync(id); // Getting  curriences using LINQ based on id or primary key. we can also use firt or single methods here to find id.
            //var currencies = await (from cur in _context.Currencies select cur).ToListAsync(); we cannot use FindAsync method in this way of using LINQ
            return Ok(currencies);
        }


        [HttpGet("{name}")]
        public async Task<IActionResult> GetAllCurrenciesAsync([FromRoute] string name )
        {
            var currencies = await _context.Currencies.FirstAsync(x => x.Title == name); // Getting  curriences using LINQ based on name
            //var currencies = await _context.Currencies.FirstOrDefaultAsync(x => x.Title == name);
            //var currencies = await _context.Currencies.SingleAsync(x => x.Title == name);
            //var currencies = await _context.Currencies.SingleOrDefaultAsync(x => x.Title == name);
            //var currencies = await _context.Currencies.Where(x => x.Title == name).SingleOrDefaultAsync();  // work as same but not good in performance beacuse in this we visit whole records and then return
                                                                                                              // but in above case we return from where or condition meet first time hence better performance.

            return Ok(currencies);
        }

    }
}

/*
 * 
 * In LINQ (Language Integrated Query) in C#, the methods `First`, `FirstOrDefault`, `Single`, and `SingleOrDefault` are used to retrieve elements from a collection. While they may seem similar, they have distinct behaviors and use cases. Here's a detailed explanation of each method:

---

### 1. **`First` Method**
- **Purpose**: Returns the **first element** in a sequence that matches the specified condition (if any). 
- **Behavior**:
  - If a condition is specified, it finds the first element that matches the condition.
  - Throws an exception (`InvalidOperationException`) if no elements match the condition or if the sequence is empty.
- **Use Case**: Use when you expect at least one matching element and only need the first match.

**Example**:
```csharp
var numbers = new[] { 1, 2, 3, 4 };
var firstEven = numbers.First(x => x % 2 == 0); // Returns 2
```

**Example with exception**:
```csharp
var numbers = new int[] {};
var first = numbers.First(); // Throws InvalidOperationException
```

---

### 2. **`FirstOrDefault` Method**
- **Purpose**: Returns the **first element** in a sequence that matches the specified condition, or the **default value** of the type if no elements are found.
- **Behavior**:
  - Returns the default value (`null` for reference types, `0` for numeric types, etc.) if no elements match the condition or if the sequence is empty.
- **Use Case**: Use when you want to handle empty sequences gracefully without exceptions.

**Example**:
```csharp
var numbers = new int[] {};
var first = numbers.FirstOrDefault(); // Returns 0 (default of int)
```

---

### 3. **`Single` Method**
- **Purpose**: Returns the **only element** in a sequence that matches the specified condition. Throws an exception if there are **no elements** or **more than one element** that match.
- **Behavior**:
  - Throws an exception (`InvalidOperationException`) if the sequence contains no elements or more than one matching element.
- **Use Case**: Use when you expect exactly one matching element and want to ensure no duplicates.

**Example**:
```csharp
var numbers = new[] { 1, 2, 3, 4 };
var singleEven = numbers.Single(x => x == 2); // Returns 2
```

**Example with exceptions**:
```csharp
var numbers = new[] { 1, 2, 3, 4 };
var singleEven = numbers.Single(x => x % 2 == 0); // Throws InvalidOperationException (multiple matches)

var empty = new int[] {};
var single = empty.Single(); // Throws InvalidOperationException (no elements)
```

---

### 4. **`SingleOrDefault` Method**
- **Purpose**: Returns the **only element** in a sequence that matches the specified condition, or the **default value** of the type if no elements are found. Throws an exception if there is more than one matching element.
- **Behavior**:
  - Returns the default value (`null` for reference types, `0` for numeric types, etc.) if no elements match the condition or if the sequence is empty.
  - Throws an exception (`InvalidOperationException`) if more than one matching element exists.
- **Use Case**: Use when you expect **zero or one** matching elements and want to handle empty sequences gracefully, but still enforce uniqueness.

**Example**:
```csharp
var numbers = new int[] {};
var single = numbers.SingleOrDefault(); // Returns 0 (default of int)

var numbers = new[] { 1, 2, 3, 4 };
var singleEven = numbers.SingleOrDefault(x => x == 2); // Returns 2
```

**Example with exception**:
```csharp
var numbers = new[] { 1, 2, 3, 4 };
var singleEven = numbers.SingleOrDefault(x => x % 2 == 0); // Throws InvalidOperationException (multiple matches)
```

---

### Summary of Differences:

| Method              | Returns When Match Found  | Returns When No Match Found        | Throws Exception When Multiple Matches  |
|---------------------|---------------------------|----------------------------------  |-----------------------------------------|
| **First**           | First matching element    | Throws `InvalidOperationException` | No                                      |
| **FirstOrDefault**  | First matching element    | Default value                      | No                                      |
| **Single**          | The only matching element | Throws `InvalidOperationException` | Yes                                     |
| **SingleOrDefault** | The only matching element | Default value                      | Yes                                     |

### Recommendations:
- Use `First` or `FirstOrDefault` when you want the **first matching element**.
- Use `Single` or `SingleOrDefault` when you want to ensure **exactly one matching element**.
- Prefer `FirstOrDefault` and `SingleOrDefault` when dealing with potentially empty sequences to avoid exceptions.


*/