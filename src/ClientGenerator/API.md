<a name='assembly'></a>
# EdjCase.ICP.ClientGenerator

## Contents

- [ClientCodeGenerator](#T-EdjCase-ICP-ClientGenerator-ClientCodeGenerator 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator')
  - [GenerateClient(serviceName,baseNamespace,service)](#M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClient-System-String,System-String,EdjCase-ICP-Candid-Models-CandidServiceDescription- 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator.GenerateClient(System.String,System.String,EdjCase.ICP.Candid.Models.CandidServiceDescription)')
  - [GenerateClientFromCanisterAsync(canisterId,baseNamespace,clientName,httpBoundryNodeUrl)](#M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClientFromCanisterAsync-EdjCase-ICP-Candid-Models-Principal,System-String,System-String,System-Uri- 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator.GenerateClientFromCanisterAsync(EdjCase.ICP.Candid.Models.Principal,System.String,System.String,System.Uri)')
  - [GenerateClientFromFile(fileText,baseNamespace,clientName)](#M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClientFromFile-System-String,System-String,System-String- 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator.GenerateClientFromFile(System.String,System.String,System.String)')
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

<a name='M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClient-System-String,System-String,EdjCase-ICP-Candid-Models-CandidServiceDescription-'></a>
### GenerateClient(serviceName,baseNamespace,service) `method`

##### Summary

Generates client source code for a canister based on a \`.did\` file definition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional. The name of the client class and file to use. Defaults to 'Service' |
| baseNamespace | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base namespace to use in the generated files |
| service | [EdjCase.ICP.Candid.Models.CandidServiceDescription](#T-EdjCase-ICP-Candid-Models-CandidServiceDescription 'EdjCase.ICP.Candid.Models.CandidServiceDescription') | The service definition to generate the client from |

<a name='M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClientFromCanisterAsync-EdjCase-ICP-Candid-Models-Principal,System-String,System-String,System-Uri-'></a>
### GenerateClientFromCanisterAsync(canisterId,baseNamespace,clientName,httpBoundryNodeUrl) `method`

##### Summary

Creates client source code for a canister based on its id. This only works if 
the canister has the \`candid:service\` meta data available in its public state

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| canisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The canister to get the definition from |
| baseNamespace | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base namespace to use in the generated files |
| clientName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional. The name of the client class and file to use. Defaults to 'Service' |
| httpBoundryNodeUrl | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | Optional. The http boundry node url to use, otherwise uses the default |

<a name='M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClientFromFile-System-String,System-String,System-String-'></a>
### GenerateClientFromFile(fileText,baseNamespace,clientName) `method`

##### Summary

Generates client source code for a canister based on a \`.did\` file definition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fileText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text content of the \`.did\` definition file |
| baseNamespace | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base namespace to use in the generated files |
| clientName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional. The name of the client class and file to use. Defaults to 'Service' |

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
