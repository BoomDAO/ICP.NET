# ICP.NET
Collection of Internet Computer Protocol (ICP) libraries for .NET/Blazor

- Agent
  - Library to communicate to and from the Internet Computer
  - Nuget: [`EdjCase.ICP.Agent`](https://www.nuget.org/packages/EdjCase.ICP.Agent) (*currently only pre-release versions are available)

- Candid
  - Library of Candid Encoding, Models and Helpers
  - Nuget: [`EdjCase.ICP.Candid`](https://www.nuget.org/packages/EdjCase.ICP.Camdid) (*currently only pre-release versions are available)

- Samples
  - A few projects to demo the capabilities of the ICP libraries
    - Blazor
    - AspNetCore
    - CLI

# Roadmap/TODO
- Serialization for custom C# models
- Automatic Api Client/Model generation from Candid spec
- Infer type from value, vs specifying value + type
- DID file parsing



# Agent
## Usage
```cs
// Create identity
var identity = new AnonymousIdentity();

// Create http agent
IAgent agent = new HttpAgent(identity);

// Create Candid arg to send in request
CandidArg arg = CandidArg.FromCandid(
    CandidValueWithType.FromValueAndType( // WIP, will reduce redundancy
        CandidPrimitive.Nat64(1234),
        new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64)
    )
);

// Make request to IC
string method = "get_proposal_info";
Principal governanceCanisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");
QueryResponse response = await agent.QueryAsync(governanceCanisterId, method, arg);

QueryReply reply = response.ThrowOrGetReply();

CandidArg responseArg = reply.Arg;
// Use response ...
```

# Candid
## Parse from bytes
```cs
CandidArg arg = CandidArg.FromBytes(rawCandidBytes);
```

## Reading candid values directly
```cs
CandidArg arg = CandidArg.FromBytes(rawCandidBytes);
CandidValue firstArg = arg.Values[0];
string title = firstArg.AsRecord()["title"];
```

## Converting candid to custom classes
### (custom serialization doesn't exist yet but is on the roadmap)

```cs
CandidArg arg = CandidArg.FromBytes(rawCandidBytes);
MyObj obj = arg.Values[0].Value.AsRecord(r => new MyObj
{
    Title = r["title"].AsText(),
    IsGoodTitle = r["is_good_title"].AsBool()
});

public class MyObj
{
    public string Title { get; set; }
    public bool IsGoodTitle { get; set; }
}
```
## Parse from Text
### * DID file formats are currently not supported
```cs
string text = "record { field_1:nat64; field_2: vec nat8 }";
CandidRecordType type = CandidTextParser.Parse<CandidRecordType>(text);
```

## Generate Text representation
```cs
var type = new CandidRecordType(new Dictionary<CandidTag, CandidType>
{
    {
        CandidTag.FromName("field_1"),
        new CandidPrimitiveType(PrimitiveType.Nat64)
    },
    {
        CandidTag.FromName("field_2"),
        new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Nat8))
    }
});
string text = CandidTextGenerator.Generator(type, IndentType.Tab);
```


# Links
- [IC Http Interface Spec](https://smartcontracts.org/docs/current/references/ic-interface-spec)
- [Candid Spec](https://github.com/dfinity/candid/blob/master/spec/Candid.md)
- [Candid Decoder](https://fxa77-fiaaa-aaaae-aaana-cai.raw.ic0.app/explain)
- [Candid UI Tester](https://a4gq6-oaaaa-aaaab-qaa4q-cai.raw.ic0.app)