<a name='assembly'></a>
# EdjCase.ICP.ClientGenerator

## Contents

- [ClientCodeGenerator](#T-EdjCase-ICP-ClientGenerator-ClientCodeGenerator 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator')
  - [GenerateClient(service,options)](#M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClient-EdjCase-ICP-Candid-Models-CandidServiceDescription,EdjCase-ICP-ClientGenerator-ClientGenerationOptions- 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator.GenerateClient(EdjCase.ICP.Candid.Models.CandidServiceDescription,EdjCase.ICP.ClientGenerator.ClientGenerationOptions)')
  - [GenerateClientFromCanisterAsync(canisterId,options)](#M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClientFromCanisterAsync-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-ClientGenerator-ClientGenerationOptions- 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator.GenerateClientFromCanisterAsync(EdjCase.ICP.Candid.Models.Principal,EdjCase.ICP.ClientGenerator.ClientGenerationOptions)')
  - [GenerateClientFromFile(fileText,options)](#M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClientFromFile-System-String,EdjCase-ICP-ClientGenerator-ClientGenerationOptions- 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator.GenerateClientFromFile(System.String,EdjCase.ICP.ClientGenerator.ClientGenerationOptions)')
- [ClientGenerationOptions](#T-EdjCase-ICP-ClientGenerator-ClientGenerationOptions 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions')
  - [#ctor(name,namespace,noFolders,featureNullable,keepCandidCase,types)](#M-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-#ctor-System-String,System-String,System-Boolean,System-String,System-String,System-Boolean,System-Boolean,System-Boolean,System-Boolean,System-Uri,System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-ClientGenerator-ITypeOptions}- 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.#ctor(System.String,System.String,System.Boolean,System.String,System.String,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Uri,System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.ClientGenerator.ITypeOptions})')
  - [FeatureNullable](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-FeatureNullable 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.FeatureNullable')
  - [KeepCandidCase](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-KeepCandidCase 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.KeepCandidCase')
  - [Name](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-Name 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.Name')
  - [Namespace](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-Namespace 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.Namespace')
  - [OutputDirectory](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-OutputDirectory 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.OutputDirectory')
  - [Types](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-Types 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.Types')
- [ClientSyntax](#T-EdjCase-ICP-ClientGenerator-ClientSyntax 'EdjCase.ICP.ClientGenerator.ClientSyntax')
  - [#ctor(name,clientFile,typeFiles)](#M-EdjCase-ICP-ClientGenerator-ClientSyntax-#ctor-System-String,Microsoft-CodeAnalysis-CSharp-Syntax-CompilationUnitSyntax,System-Collections-Generic-List{System-ValueTuple{System-String,Microsoft-CodeAnalysis-CSharp-Syntax-CompilationUnitSyntax}}- 'EdjCase.ICP.ClientGenerator.ClientSyntax.#ctor(System.String,Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax,System.Collections.Generic.List{System.ValueTuple{System.String,Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax}})')
  - [ClientFile](#P-EdjCase-ICP-ClientGenerator-ClientSyntax-ClientFile 'EdjCase.ICP.ClientGenerator.ClientSyntax.ClientFile')
  - [Name](#P-EdjCase-ICP-ClientGenerator-ClientSyntax-Name 'EdjCase.ICP.ClientGenerator.ClientSyntax.Name')
  - [TypeFiles](#P-EdjCase-ICP-ClientGenerator-ClientSyntax-TypeFiles 'EdjCase.ICP.ClientGenerator.ClientSyntax.TypeFiles')
  - [GenerateFileContents()](#M-EdjCase-ICP-ClientGenerator-ClientSyntax-GenerateFileContents 'EdjCase.ICP.ClientGenerator.ClientSyntax.GenerateFileContents')
  - [GenerateFileContents(syntax)](#M-EdjCase-ICP-ClientGenerator-ClientSyntax-GenerateFileContents-Microsoft-CodeAnalysis-CSharp-Syntax-CompilationUnitSyntax- 'EdjCase.ICP.ClientGenerator.ClientSyntax.GenerateFileContents(Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax)')
  - [Rewrite(rewriter)](#M-EdjCase-ICP-ClientGenerator-ClientSyntax-Rewrite-Microsoft-CodeAnalysis-CSharp-CSharpSyntaxRewriter- 'EdjCase.ICP.ClientGenerator.ClientSyntax.Rewrite(Microsoft.CodeAnalysis.CSharp.CSharpSyntaxRewriter)')

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

<a name='M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClientFromCanisterAsync-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-ClientGenerator-ClientGenerationOptions-'></a>
### GenerateClientFromCanisterAsync(canisterId,options) `method`

##### Summary

Creates client source code for a canister based on its id. This only works if 
the canister has the \`candid:service\` meta data available in its public state

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| canisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The canister to get the definition from |
| options | [EdjCase.ICP.ClientGenerator.ClientGenerationOptions](#T-EdjCase-ICP-ClientGenerator-ClientGenerationOptions 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions') | The options for client generation |

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

<a name='M-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-#ctor-System-String,System-String,System-Boolean,System-String,System-String,System-Boolean,System-Boolean,System-Boolean,System-Boolean,System-Uri,System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-ClientGenerator-ITypeOptions}-'></a>
### #ctor(name,namespace,noFolders,featureNullable,keepCandidCase,types) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the client class and file to use |
| namespace | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base namespace to use in the generated files |
| noFolders | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | If true, there will be no folders, all files will be in the same directory |
| featureNullable | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | If true, the nullable C# feature will be used |
| keepCandidCase | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | If true, the names of properties and methods will keep the raw candid name. Otherwise they will be converted to something prettier |
| types | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Optional. Specifies options for each candid type in the definition |

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-FeatureNullable'></a>
### FeatureNullable `property`

##### Summary

If true, the nullable C# feature will be used

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-KeepCandidCase'></a>
### KeepCandidCase `property`

##### Summary

If true, the names of properties and methods will keep the raw candid name.
Otherwise they will be converted to something prettier

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-Name'></a>
### Name `property`

##### Summary

The name of the client class and file to use

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-Namespace'></a>
### Namespace `property`

##### Summary

The base namespace to use in the generated files

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-OutputDirectory'></a>
### OutputDirectory `property`

##### Summary

If true, there will be no folders, all files will be in the same directory

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-Types'></a>
### Types `property`

##### Summary

Optional. Specifies options for each candid type in the definition.
Only supports named types, no anonymous types

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

<a name='M-EdjCase-ICP-ClientGenerator-ClientSyntax-GenerateFileContents'></a>
### GenerateFileContents() `method`

##### Summary

Converts the file syntax objects into source code string values to use as a file

##### Returns

Client and type source code file contents

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-ClientGenerator-ClientSyntax-GenerateFileContents-Microsoft-CodeAnalysis-CSharp-Syntax-CompilationUnitSyntax-'></a>
### GenerateFileContents(syntax) `method`

##### Summary

Helper function to turn a client or type into a string of file contents

##### Returns

String source code of the specified syntax

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| syntax | [Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax](#T-Microsoft-CodeAnalysis-CSharp-Syntax-CompilationUnitSyntax 'Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax') | The client or type file to convert to a string |

<a name='M-EdjCase-ICP-ClientGenerator-ClientSyntax-Rewrite-Microsoft-CodeAnalysis-CSharp-CSharpSyntaxRewriter-'></a>
### Rewrite(rewriter) `method`

##### Summary

Rewrites the syntax with the specified \`CSharpSyntaxRewriter\`

##### Returns

Updated client syntax

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| rewriter | [Microsoft.CodeAnalysis.CSharp.CSharpSyntaxRewriter](#T-Microsoft-CodeAnalysis-CSharp-CSharpSyntaxRewriter 'Microsoft.CodeAnalysis.CSharp.CSharpSyntaxRewriter') | A \`CSharpSyntaxRewriter\` to rewrite the csharp syntax |
