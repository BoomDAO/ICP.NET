# Candid - Library of Candid Encoding, Models and Helpers

- Nuget: [`EdjCase.ICP.Candid`](https://www.nuget.org/packages/EdjCase.ICP.Candid)

# Using Native Candid Types

- `CandidArg` is the set of candid typed value parameters that is sent to a IC canister for a request
- `CandidTypedValue` is the combo of a `CandidValue` and its corresponding `CandidType`
- `CandidValue` is a raw candid value
- `CandidType` is the type definition of a candid type

## Create Type

```cs
CandidType natType = CandidType.Nat();
CandidType textType = CandidType.Text();

CandidType recordType = CandidType.Record();
string strinifiedType = "record { field_1:nat64; field_2: vec nat8 }";
recordType = CandidTextParser.Parse<CandidRecordType>(text);
```

# Create Value

```

```

## Parse Arg From bytes

When candid is encoded, its in the form of a `CandidArg`. Individual values and types are not usually encoded

```cs
CandidArg arg = CandidByteParser.Parse(rawCandidBytes);
```

OR

```cs
CandidArg arg = CandidArg.FromBytes(rawCandidBytes);
```

## Reading Values (without custom types)

```cs
CandidArg arg = ...;
CandidTypedValue firstTypedValue = arg.Values[0];
CandidTypedValue secondTypedValue = arg.Values[1];
string title = firstArg.Value.AsRecord()["title"];
```

# Using Custom Types

## From Candid Value

```cs
CandidArg arg = ...;
MyObj1 obj = arg.ToObjects<MyObj1>();
```

## From Candid Arg

```cs
CandidArg arg = ...;
(MyObj1 obj, MyObj2 obj2) = arg.ToObjects<MyObj1, MyObj2>();
```

```cs
// Serialze
MyObj obj = new MyObj
{
    Title = "Title 1",
    IsGoodTitle = false
};
CandidTypedValue value = CandidTypedValue.FromObject(obj);
```

# Defining Variant Types

```cs
[Variant(typeof(MyVariantTag))] // Required to flag as variant and define options with enum
public class MyVariant
{
    [VariantTagProperty] // Flag for tag/enum property, not required if name is `Tag`
    public MyVariantTag Tag { get; set; }
    [VariantValueProperty] // Flag for value property, not required if name is `Value`
    public object? Value { get; set; }
}

public enum MyVariantTag
{
    [CandidName("o1")] // Used to override name for candid
    Option1,
    [CandidName("o2")]
    [VariantType(typeof(string))] // Used to specify if the option has a value associated
    Option2
}
```

Or if variant options have no type, just an Enum can be used

```cs
public enum MyVariant
{
    [CandidName("o1")]
    Option1,
    [CandidName("o2")]
    Option2
}
```

# Defining Record Types

```cs
public class MyRecord
{
    [CandidName("title")] // Used to override name for candid
    public string Title { get; set; }
    [CandidName("is_good_title")]
    public bool IsGoodTitle { get; set; }
}
```

### NOTE: [CandidName(...)] is not needed if the property name is exactly the same as the candid name

```cs
// Equivalent to above
public class MyRecord
{
    public string title { get; set; }
    public bool is_good_title { get; set; }
}
```

# Defining Other Types

```cs
(C# type) -> (Candid type)

UnboundedUInt -> Nat
byte -> Nat8
ushort -> Nat16
uint -> Nat32
ulong -> Nat64
UnboundedInt -> Int
sbyte -> Int8
short -> Int16
int -> Int32
long -> Int64
string -> Text
float -> Float32
double -> Float64
bool -> Bool
Principal -> Principal
List<T> -> Vec T
T[] -> Vec T
CandidFunc -> Func
OptionalValue<T> -> Opt T
EmptyValue -> Empty
ReservedValue -> Reserved
NullValue -> Null
```

## Generate Candid Text representation

```cs
var type = new CandidRecordType(new Dictionary<CandidTag, CandidType>
{
    {
        CandidTag.FromName("field_1"),
        CandidType.Nat64()
    },
    {
        CandidTag.FromName("field_2"),
        new CandidVectorType(CandidType.Nat8())
    }
});
string text = CandidTextGenerator.Generator(type, IndentType.Tab);
```
