# NStandard.Json.Net

**Json** extensions for **Newtonsoft.Json**.

- Includes built-in type compatibility , such as **Lazy\<T\>**.


<br/>

## Built-in compatibility

**Lazy\<T\>** is a commonly used type, but his serialization is not very easy to use.

For example,

```csharp
var lazy = new Lazy<int>(123);
var output = System.Text.Json.JsonSerializer.Serialize(lazy);
```

The output is

```json
{"IsValueCreated":true,"Value":123}
```

We can find that the **IsValueCreated** field is meaningless in **Json**.

<br/>

So, we hope it can be simplified. Use these code:

```csharp
using System.Text.Json;
using NStandard.Json;
```

```csharp
var options = new JsonSerializerOptions();
options.Converters.Add(new LazyConverter());

// Serialize
var lazy = new Lazy<int>(123);
var output = JsonSerializer.Serialize(lazy, options);
// The output is:   123

// Deserialize
var backLazy = JsonSerializer.Deserialize<Lazy<int>>(output, options);
var value = backLazy.Value;
// The value is:    123
```

<br/>

## Use in AspNet Core

You need to install the **Microsoft.AspNetCore.Mvc.NewtonsoftJson** package first.

Use the following code to enable the extension in the **AspNet Core** application.

```csharp
services
	.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new LazyConverter());
    });
```

<br/>

