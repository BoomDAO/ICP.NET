# Client Generator

- Library of generating C# client code from \*.did files
- Nuget: [`EdjCase.ICP.ClientGenerator`](https://www.nuget.org/packages/EdjCase.ICP.ClientGenerator)

## Usage (dotnet tool)

### Install with dotnet tools

```
dotnet tool install -g EdjCase.ICP.ClientGenerator
```

### Run tool

(First run only) Initialize config file and update generated file

```
candid-client-generator init ./
```

Creates `candid-client.toml` file to update in specified directory

Example:

```
namespace = "My.Namespace" # Base namespace used for generated files
output-directory = "./Clients" # Directory to put clients. Each client will get its own sub folder based on its name. If not specified, will use current directory
no-folders = false # If true, will put all the files in a single directory

[[clients]]
name = "Dex" # Used for the name of the folder and client class
type = "file" # Create client based on service definition file
file-path = "./ServiceDefinitionFiles/Dex.did" # Service definition file path
output-directory = "./Clients/D" # Override base output directory, but this specifies the subfolder
no-folders = false # If true, will put all the files in a single directory


# Can specify multiple clients by defining another
[[clients]]
name = "Governance"
type = "canister" # Create client based on canister
canister-id = "rrkah-fqaaa-aaaaa-aaaaq-cai" # Canister to create client for
```

### Generate clients

```
candid-client-generator ./
```

or

```
candid-client-generator gen ./
```

### Config file options

#### Top Level:

- `namespace` - (Text) REQUIRED. The base namespace used in all C# files generated.
  Files generated in a sub-folder will have a more specific namespace to match. This namespace can be overidden per client.
- `output-directory` - (Text) OPTIONAL. Directory to put all generated files. Each client will have a sub-folder within the output directory that will match the client name. If not specified, the working directory will be used
- `no-folders` - (Bool) OPTIONAL. If true, no sub-folders will be generated for the clients or the models within the clients. All generated files will be in a flat structure. Defaults to false
- `url` - (Text) OPTIONAL. Sets the boundry node url to use for making calls to canisters on the IC. Can be set to a local developer instance/localhost. Defaults to 'https://ic0.app/'. This setting is only useful for clients of generation type `canister`
- `feature-nullable` - (Bool) Optional. Sets whether to use the C# nullable feature when generating the client (like `object?`). Defaults to true
- `keep-candid-case` - (Bool) Optional. If true, the names of properties and methods will keep the raw candid name. Otherwise they will be converted to something prettier. Defaults to false

#### Client Level:

- `name` - (Text) REQUIRED. The name of the sub-folder put the client files and the prefix to the client class name.
- `type` - (Text) REQUIRED. An enum value to indicate what type of client generation method to use. Each enum value also has associated configuration settings. Options:
  - `file` - Will create a client based on a service definition file (`*.did`)
    - `file-path` - (Text) REQUIRED. The file path to the `*.did` file to generate from
  - `canister` - Creates a client based on a canister id
    - `cansiter-id` - (Text) REQUIRED. The principal id of the canister to generate a client for
- `output-directory` - (Text) OPTIONAL. Directory to put all generated client files. Overrides the top level `output-directory`. NOTE: this does not create a sub-folder based on the client name like the top level `output-directory` does
- `no-folders` - (Bool) OPTIONAL. If true, no sub-folders will be generated for the client. All generated files will be in a flat structure. Defaults to false. Overrides the top level `no-folders`
- `feature-nullable` - (Bool) Optional. Sets whether to use the C# nullable feature when generating the client (like `object?`). Defaults to true. Overrides the top level `feature-nullable`
- `keep-candid-case` - (Bool) Optional. If true, the names of properties and methods will keep the raw candid name. Otherwise they will be converted to something prettier. Defaults to false. Overrides the top level `keep-candid-case`
