# ICP.NET

Collection of Internet Computer Protocol (ICP) libraries for .NET/Blazor

- [Agent](src/Agent/README.md) - Library to communicate to and from the Internet Computer

- [Candid](src/Candid/README.md) - Library of Candid Encoding, Models and Helpers

- [Client Generator](src/ClientGenerator/README.md) - Client source code generator for ICP canisters

- [Internet Identity (Experimental)](src/InternetIdentity/README.md) - Internet Identity authenticater (experimental and not secure)

## See each individual project README for more in depth guides


# 🎮 Unity Integration

Note: WebGL/IL2CPP is not supported YET due to an issue with IL2CPP, working on it
- Download latest binaries for the agent: https://github.com/edjCase/ICP.NET/releases
- Extract `.zip` to a plugins folder in your Unity Assets: `Assets/plugins/ICP.NET/`
- If generating a client (see below), place the generated files into the scripts folder: `Assets/scripts/MyClient`
- Start coding 💻

# 📡 Generating a client for a canister
- Prerequisite: Have .Net 6 installed (https://dotnet.microsoft.com/en-us/download/dotnet)
- Navigate to directory of .Net project
  ```
  cd {path/to/project}
  ```
- Add Agent nuget package to project
  ```
  dotnet add package EdjCase.ICP.Agent
  ```
- Install ClientGenerator

  ```
  dotnet tool install -g EdjCase.ICP.ClientGenerator
  ```

  This will allow a client to be automatically be generated for a canister. See [ClientGenerator README](src/ClientGenerator/README.md) for more details and advanced config

- Initialize ClientGenerator config (first run only)
  ```
  candid-client-generator init
  ```
  This will create a TOML config file in the directory that can be changed for more advanced options
- Update created config file `candid-client.toml`

  If using a canister id:

  ```toml
  namespace = "ProjectGovernance" # Base namespace to use
  output-directory = "./Clients" # Output directory

  [[clients]]
  name = "Governance" # Label of client to use
  type = "canister" # Indicates to make client from a canister id
  canister-id = "rrkah-fqaaa-aaaaa-aaaaq-cai" # Canister id to make client for
  ```

  If using a service definition file (.did)

  ```toml
  namespace = "ProjectGovernance" # Base namespace to use
  output-directory = "./Clients" # Output directory

  [[clients]]
  name = "Governance" # Label of client to use
  type = "file" # Indicates to make client from a service definition file
  file-path = "Governance.did" # File to use
  ```

  For all configuration options see [ClientGenerator README](src/ClientGenerator/README.md) for more details

- Generate Client
  ```
  candid-client-generator gen
  ```
  Will output C# file to the output directory specified in the config
- Use client in code
  ```cs
  var agent = new HttpAgent();
  Principal canisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");
  var client = new GovernanceApiClient(agent, canisterId);
  OptionalValue<Sample.Shared.Governance.Models.ProposalInfo> proposalInfo = await client.GetProposalInfo(110174);
  ...
  ```
- SHIP IT! 🚀

# Candid Related Links

- [IC Http Interface Spec](https://smartcontracts.org/docs/current/references/ic-interface-spec)
- [Candid Spec](https://github.com/dfinity/candid/blob/master/spec/Candid.md)
- [Candid Decoder](https://fxa77-fiaaa-aaaae-aaana-cai.raw.ic0.app/explain)
- [Candid UI Tester](https://a4gq6-oaaaa-aaaab-qaa4q-cai.raw.ic0.app)
