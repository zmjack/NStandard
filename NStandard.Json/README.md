# NStandard.Json

**Json** extensions for **System.Text.Json**.

- Includes built-in type compatibility , such as **Lazy\<T\>**.


- As well as **Newtonsoft.Json** behavior compatibility.


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

## **Newtonsoft.Json** behavior compatibility

We found some differences in the special number converting between **System.Text.Json** and **Newtonsoft.Json**. Such as **double.NaN**, **double.PositiveInfinity**, **double.NegativeInfinity** etc.

<br/>

Use **Newtonsoft.Json**:

```csharp
var output = Newtonsoft.Json.JsonConvert.SerializeObject(double.PositiveInfinity);
// The output is:   "Infinity"
```

Use **System.Text.Json**:

```csharp
var output = System.Text.Json.JsonSerializer.Serialize(double.PositiveInfinity);
// Throw ArgumentException
// .NET number values such as positive and negative infinity cannot be written as valid JSON.
```

<br/>

Therefore, we provide **NetSingleConverter** and **NetDoubleConverter** to simulate the behavior of the **Newtonsoft.Json**.

```csharp
using System.Text.Json;
using NStandard.Json;
```

```csharp
var options = new JsonSerializerOptions();        
options.Converters.Add(new NetSingleConverter());
options.Converters.Add(new NetDoubleConverter());

var output = JsonSerializer.Serialize(double.PositiveInfinity, options);
// The output is:   "Infinity"
var back = JsonSerializer.Deserialize<double>("\"Infinity\"", options);
// The back is:     double.PositiveInfinity
```

<br/>

## Use in AspNet Core

Use the following code to enable the extension in the **AspNet Core** application.

```csharp
services
	.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new LazyConverter());
        options.JsonSerializerOptions.Converters.Add(new NetSingleConverter());
        options.JsonSerializerOptions.Converters.Add(new NetDoubleConverter());
    });
```

<br/>

