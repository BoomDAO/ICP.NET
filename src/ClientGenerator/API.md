<a name='assembly'></a>
# EdjCase.ICP.ClientGenerator

## Contents

- [ClientCodeGenerator](#T-EdjCase-ICP-ClientGenerator-ClientCodeGenerator 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator')
  - [GenerateClient(service,options)](#M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClient-EdjCase-ICP-Candid-Models-CandidServiceDescription,EdjCase-ICP-ClientGenerator-ClientGenerationOptions- 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator.GenerateClient(EdjCase.ICP.Candid.Models.CandidServiceDescription,EdjCase.ICP.ClientGenerator.ClientGenerationOptions)')
  - [GenerateClientFromCanisterAsync(canisterId,options,httpBoundryNodeUrl)](#M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClientFromCanisterAsync-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-ClientGenerator-ClientGenerationOptions,System-Uri- 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator.GenerateClientFromCanisterAsync(EdjCase.ICP.Candid.Models.Principal,EdjCase.ICP.ClientGenerator.ClientGenerationOptions,System.Uri)')
  - [GenerateClientFromFile(fileText,options)](#M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClientFromFile-System-String,EdjCase-ICP-ClientGenerator-ClientGenerationOptions- 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator.GenerateClientFromFile(System.String,EdjCase.ICP.ClientGenerator.ClientGenerationOptions)')
- [ClientGenerationOptions](#T-EdjCase-ICP-ClientGenerator-ClientGenerationOptions 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions')
  - [#ctor(name,namespace)](#M-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-#ctor-System-String,System-String- 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.#ctor(System.String,System.String)')
  - [Name](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-Name 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.Name')
  - [Namespace](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-Namespace 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.Namespace')
- [ClientSyntax](#T-EdjCase-ICP-ClientGenerator-ClientSyntax 'EdjCase.ICP.ClientGenerator.ClientSyntax')
  - [#ctor(name,clientFile,typeFiles)](#M-EdjCase-ICP-ClientGenerator-ClientSyntax-#ctor-System-String,Microsoft-CodeAnalysis-CSharp-Syntax-CompilationUnitSyntax,System-Collections-Generic-List{System-ValueTuple{System-String,Microsoft-CodeAnalysis-CSharp-Syntax-CompilationUnitSyntax}}- 'EdjCase.ICP.ClientGenerator.ClientSyntax.#ctor(System.String,Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax,System.Collections.Generic.List{System.ValueTuple{System.String,Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax}})')
  - [ClientFile](#P-EdjCase-ICP-ClientGenerator-ClientSyntax-ClientFile 'EdjCase.ICP.ClientGenerator.ClientSyntax.ClientFile')
  - [Name](#P-EdjCase-ICP-ClientGenerator-ClientSyntax-Name 'EdjCase.ICP.ClientGenerator.ClientSyntax.Name')
  - [TypeFiles](#P-EdjCase-ICP-ClientGenerator-ClientSyntax-TypeFiles 'EdjCase.ICP.ClientGenerator.ClientSyntax.TypeFiles')

<a name='T-EdjCase-ICP-ClientGenerator-ClientCodeGenerator'></a>
## ClientCodeGenerator `type`

##### Namespace

EdjCase.ICP.ClientGenerator

##### Summary

Generator to create client source code based of candid definitions from \`.did\` files
or from a canister id

<a name='M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClient-EdjCase-ICP-Candid-Models-CandidServiceDescription,EdjCase-ICP-ClientGenerator-ClientGenerationOptions-'></a>
### GenerateClient(service,options) `method`

##### Summary

Generates client source code for a canister based on a \`.did\` file definition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [EdjCase.ICP.Candid.Models.CandidServiceDescription](#T-EdjCase-ICP-Candid-Models-CandidServiceDescription 'EdjCase.ICP.Candid.Models.CandidServiceDescription') | The service definition to generate the client from |
| options | [EdjCase.ICP.ClientGenerator.ClientGenerationOptions](#T-EdjCase-ICP-ClientGenerator-ClientGenerationOptions 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions') | The options for client generation |

<a name='M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClientFromCanisterAsync-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-ClientGenerator-ClientGenerationOptions,System-Uri-'></a>
### GenerateClientFromCanisterAsync(canisterId,options,httpBoundryNodeUrl) `method`

##### Summary

Creates client source code for a canister based on its id. This only works if 
the canister has the \`candid:service\` meta data available in its public state

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| canisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The canister to get the definition from |
| options | [EdjCase.ICP.ClientGenerator.ClientGenerationOptions](#T-EdjCase-ICP-ClientGenerator-ClientGenerationOptions 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions') | The options for client generation |
| httpBoundryNodeUrl | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | Optional. The http boundry node url to use, otherwise uses the default |

<a name='M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClientFromFile-System-String,EdjCase-ICP-ClientGenerator-ClientGenerationOptions-'></a>
### GenerateClientFromFile(fileText,options) `method`

##### Summary

Generates client source code for a canister based on a \`.did\` file definition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fileText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text content of the \`.did\` definition file |
| options | [EdjCase.ICP.ClientGenerator.ClientGenerationOptions](#T-EdjCase-ICP-ClientGenerator-ClientGenerationOptions 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions') | The options for client generation |

<a name='T-EdjCase-ICP-ClientGenerator-ClientGenerationOptions'></a>
## ClientGenerationOptions `type`

##### Namespace

EdjCase.ICP.ClientGenerator

##### Summary

Options for generating a client

<a name='M-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-#ctor-System-String,System-String-'></a>
### #ctor(name,namespace) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the client class and file to use |
| namespace | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base namespace to use in the generated files |

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-Name'></a>
### Name `property`

##### Summary

The name of the client class and file to use

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-Namespace'></a>
### Namespace `property`

##### Summary

The base namespace to use in the generated files

<a name='T-EdjCase-ICP-ClientGenerator-ClientSyntax'></a>
## ClientSyntax `type`

##### Namespace

EdjCase.ICP.ClientGenerator

##### Summary

A model containing the client code to be rendered

<a name='M-EdjCase-ICP-ClientGenerator-ClientSyntax-#ctor-System-String,Microsoft-CodeAnalysis-CSharp-Syntax-CompilationUnitSyntax,System-Collections-Generic-List{System-ValueTuple{System-String,Microsoft-CodeAnalysis-CSharp-Syntax-CompilationUnitSyntax}}-'></a>
### #ctor(name,clientFile,typeFiles) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the client |
| clientFile | [Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax](#T-Microsoft-CodeAnalysis-CSharp-Syntax-CompilationUnitSyntax 'Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax') | The syntax of the client file |
| typeFiles | [System.Collections.Generic.List{System.ValueTuple{System.String,Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.ValueTuple{System.String,Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax}}') | The syntax of different declared types for the client |

<a name='P-EdjCase-ICP-ClientGenerator-ClientSyntax-ClientFile'></a>
### ClientFile `property`

##### Summary

The syntax of the client file

<a name='P-EdjCase-ICP-ClientGenerator-ClientSyntax-Name'></a>
### Name `property`

##### Summary

The name of the client

<a name='P-EdjCase-ICP-ClientGenerator-ClientSyntax-TypeFiles'></a>
### TypeFiles `property`

##### Summary

The syntax of different declared types for the client
