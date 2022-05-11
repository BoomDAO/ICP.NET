# ICP.NET
Collection of Internet Computer Protocol (ICP) libraries 

- Agent - Library to allow communication to and from the Internet Computer

- Candid - Library of Candid Encoding, Models and Helpers to work with 

- Samples - A few projects to demo the capabilities of the ICP libraries

# Roadmap/TODO
- Serialization for custom C# models
- Automatic Api Client/Model generation from Candid spec
- Infer type from value, vs specifying value + type

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
QueryResponse response = await this.agent.QueryAsync(governanceCanisterId, method, arg);

QueryReply reply = response.ThrowOrGetReply();

CandidArg responseArg = reply.Arg;
// Use response ...
```

# Candid
## Usage
### Parsing bytes
```cs
CandidArg arg = CandidArg.FromBytes(rawCandidBytes);
```

### Using candid values directly
```cs
CandidArg arg = CandidArg.FromBytes(rawCandidBytes);
CandidValue firstArg = arg.Values[0];
string title = firstArg.AsRecord()["title"];
```

### Converting candid to custom classes (custom serialization is on the roadmap)

```cs
CandidArg arg = CandidArg.FromBytes(rawCandidBytes);
MyObj obj = arg.Values[0].Value.AsRecord(r => {
    return new MyObj
    {
        Title = r["title"].AsText(),
        IsGoodTitle = r["is_good_title"].AsBool()
    }
});

public class MyObj
{
    public string Title { get; set; }
    public bool IsGoodTitle { get; set; }
}
```