<a name='assembly'></a>
# EdjCase.ICP.ClientGenerator

## Contents

- [ClientCodeGenerator](#T-EdjCase-ICP-ClientGenerator-ClientCodeGenerator 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator')
  - [GenerateClient(service,options)](#M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClient-EdjCase-ICP-Candid-Models-CandidServiceDescription,EdjCase-ICP-ClientGenerator-ClientGenerationOptions- 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator.GenerateClient(EdjCase.ICP.Candid.Models.CandidServiceDescription,EdjCase.ICP.ClientGenerator.ClientGenerationOptions)')
  - [GenerateClientFromCanisterAsync(canisterId,options)](#M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClientFromCanisterAsync-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-ClientGenerator-ClientGenerationOptions- 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator.GenerateClientFromCanisterAsync(EdjCase.ICP.Candid.Models.Principal,EdjCase.ICP.ClientGenerator.ClientGenerationOptions)')
  - [GenerateClientFromFile(fileText,options)](#M-EdjCase-ICP-ClientGenerator-ClientCodeGenerator-GenerateClientFromFile-System-String,EdjCase-ICP-ClientGenerator-ClientGenerationOptions- 'EdjCase.ICP.ClientGenerator.ClientCodeGenerator.GenerateClientFromFile(System.String,EdjCase.ICP.ClientGenerator.ClientGenerationOptions)')
- [ClientGenerationOptions](#T-EdjCase-ICP-ClientGenerator-ClientGenerationOptions 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions')
  - [#ctor(name,namespace,getDefinitionFromCanister,filePathOrCandidId,outputDirectory,purgeOutputDirectory,noFolders,featureNullable,keepCandidCase,boundryNodeUrl,types)](#M-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-#ctor-System-String,System-String,System-Boolean,System-String,System-String,System-Boolean,System-Boolean,System-Boolean,System-Boolean,System-Uri,System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-ClientGenerator-NamedTypeOptions}- 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.#ctor(System.String,System.String,System.Boolean,System.String,System.String,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Uri,System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.ClientGenerator.NamedTypeOptions})')
  - [BoundryNodeUrl](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-BoundryNodeUrl 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.BoundryNodeUrl')
  - [FeatureNullable](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-FeatureNullable 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.FeatureNullable')
  - [FilePathOrCanisterId](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-FilePathOrCanisterId 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.FilePathOrCanisterId')
  - [GetDefinitionFromCansiter](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-GetDefinitionFromCansiter 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.GetDefinitionFromCansiter')
  - [KeepCandidCase](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-KeepCandidCase 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.KeepCandidCase')
  - [Name](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-Name 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.Name')
  - [Namespace](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-Namespace 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.Namespace')
  - [NoFolders](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-NoFolders 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.NoFolders')
  - [OutputDirectory](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-OutputDirectory 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.OutputDirectory')
  - [PurgeOutputDirectory](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-PurgeOutputDirectory 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.PurgeOutputDirectory')
  - [Types](#P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-Types 'EdjCase.ICP.ClientGenerator.ClientGenerationOptions.Types')
- [ClientSyntax](#T-EdjCase-ICP-ClientGenerator-ClientSyntax 'EdjCase.ICP.ClientGenerator.ClientSyntax')
  - [#ctor(name,clientFile,typeFiles)](#M-EdjCase-ICP-ClientGenerator-ClientSyntax-#ctor-System-String,Microsoft-CodeAnalysis-CSharp-Syntax-CompilationUnitSyntax,System-Collections-Generic-List{System-ValueTuple{System-String,Microsoft-CodeAnalysis-CSharp-Syntax-CompilationUnitSyntax}}- 'EdjCase.ICP.ClientGenerator.ClientSyntax.#ctor(System.String,Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax,System.Collections.Generic.List{System.ValueTuple{System.String,Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax}})')
  - [ClientFile](#P-EdjCase-ICP-ClientGenerator-ClientSyntax-ClientFile 'EdjCase.ICP.ClientGenerator.ClientSyntax.ClientFile')
  - [Name](#P-EdjCase-ICP-ClientGenerator-ClientSyntax-Name 'EdjCase.ICP.ClientGenerator.ClientSyntax.Name')
  - [TypeFiles](#P-EdjCase-ICP-ClientGenerator-ClientSyntax-TypeFiles 'EdjCase.ICP.ClientGenerator.ClientSyntax.TypeFiles')
  - [GenerateFileContents()](#M-EdjCase-ICP-ClientGenerator-ClientSyntax-GenerateFileContents 'EdjCase.ICP.ClientGenerator.ClientSyntax.GenerateFileContents')
  - [GenerateFileContents(syntax)](#M-EdjCase-ICP-ClientGenerator-ClientSyntax-GenerateFileContents-Microsoft-CodeAnalysis-CSharp-Syntax-CompilationUnitSyntax- 'EdjCase.ICP.ClientGenerator.ClientSyntax.GenerateFileContents(Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax)')
  - [Rewrite(rewriter)](#M-EdjCase-ICP-ClientGenerator-ClientSyntax-Rewrite-Microsoft-CodeAnalysis-CSharp-CSharpSyntaxRewriter- 'EdjCase.ICP.ClientGenerator.ClientSyntax.Rewrite(Microsoft.CodeAnalysis.CSharp.CSharpSyntaxRewriter)')
- [ITypeOptions](#T-EdjCase-ICP-ClientGenerator-ITypeOptions 'EdjCase.ICP.ClientGenerator.ITypeOptions')
  - [Type](#P-EdjCase-ICP-ClientGenerator-ITypeOptions-Type 'EdjCase.ICP.ClientGenerator.ITypeOptions.Type')
- [NamedTypeOptions](#T-EdjCase-ICP-ClientGenerator-NamedTypeOptions 'EdjCase.ICP.ClientGenerator.NamedTypeOptions')
  - [#ctor(nameOverride,typeOptions)](#M-EdjCase-ICP-ClientGenerator-NamedTypeOptions-#ctor-System-String,EdjCase-ICP-ClientGenerator-ITypeOptions- 'EdjCase.ICP.ClientGenerator.NamedTypeOptions.#ctor(System.String,EdjCase.ICP.ClientGenerator.ITypeOptions)')
  - [NameOverride](#P-EdjCase-ICP-ClientGenerator-NamedTypeOptions-NameOverride 'EdjCase.ICP.ClientGenerator.NamedTypeOptions.NameOverride')
  - [TypeOptions](#P-EdjCase-ICP-ClientGenerator-NamedTypeOptions-TypeOptions 'EdjCase.ICP.ClientGenerator.NamedTypeOptions.TypeOptions')
- [RecordTypeOptions](#T-EdjCase-ICP-ClientGenerator-RecordTypeOptions 'EdjCase.ICP.ClientGenerator.RecordTypeOptions')
  - [#ctor(fields)](#M-EdjCase-ICP-ClientGenerator-RecordTypeOptions-#ctor-System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-ClientGenerator-NamedTypeOptions}- 'EdjCase.ICP.ClientGenerator.RecordTypeOptions.#ctor(System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.ClientGenerator.NamedTypeOptions})')
  - [Fields](#P-EdjCase-ICP-ClientGenerator-RecordTypeOptions-Fields 'EdjCase.ICP.ClientGenerator.RecordTypeOptions.Fields')
  - [Type](#P-EdjCase-ICP-ClientGenerator-RecordTypeOptions-Type 'EdjCase.ICP.ClientGenerator.RecordTypeOptions.Type')
- [VariantTypeOptions](#T-EdjCase-ICP-ClientGenerator-VariantTypeOptions 'EdjCase.ICP.ClientGenerator.VariantTypeOptions')
  - [#ctor(options)](#M-EdjCase-ICP-ClientGenerator-VariantTypeOptions-#ctor-System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-ClientGenerator-NamedTypeOptions}- 'EdjCase.ICP.ClientGenerator.VariantTypeOptions.#ctor(System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.ClientGenerator.NamedTypeOptions})')
  - [Options](#P-EdjCase-ICP-ClientGenerator-VariantTypeOptions-Options 'EdjCase.ICP.ClientGenerator.VariantTypeOptions.Options')
  - [Type](#P-EdjCase-ICP-ClientGenerator-VariantTypeOptions-Type 'EdjCase.ICP.ClientGenerator.VariantTypeOptions.Type')
- [VectorRepresentation](#T-EdjCase-ICP-ClientGenerator-VectorRepresentation 'EdjCase.ICP.ClientGenerator.VectorRepresentation')
  - [Array](#F-EdjCase-ICP-ClientGenerator-VectorRepresentation-Array 'EdjCase.ICP.ClientGenerator.VectorRepresentation.Array')
  - [Dictionary](#F-EdjCase-ICP-ClientGenerator-VectorRepresentation-Dictionary 'EdjCase.ICP.ClientGenerator.VectorRepresentation.Dictionary')
  - [List](#F-EdjCase-ICP-ClientGenerator-VectorRepresentation-List 'EdjCase.ICP.ClientGenerator.VectorRepresentation.List')
- [VectorTypeOptions](#T-EdjCase-ICP-ClientGenerator-VectorTypeOptions 'EdjCase.ICP.ClientGenerator.VectorTypeOptions')
  - [#ctor(representation,elementType)](#M-EdjCase-ICP-ClientGenerator-VectorTypeOptions-#ctor-System-Nullable{EdjCase-ICP-ClientGenerator-VectorRepresentation},EdjCase-ICP-ClientGenerator-ITypeOptions- 'EdjCase.ICP.ClientGenerator.VectorTypeOptions.#ctor(System.Nullable{EdjCase.ICP.ClientGenerator.VectorRepresentation},EdjCase.ICP.ClientGenerator.ITypeOptions)')
  - [ElementType](#P-EdjCase-ICP-ClientGenerator-VectorTypeOptions-ElementType 'EdjCase.ICP.ClientGenerator.VectorTypeOptions.ElementType')
  - [Representation](#P-EdjCase-ICP-ClientGenerator-VectorTypeOptions-Representation 'EdjCase.ICP.ClientGenerator.VectorTypeOptions.Representation')
  - [Type](#P-EdjCase-ICP-ClientGenerator-VectorTypeOptions-Type 'EdjCase.ICP.ClientGenerator.VectorTypeOptions.Type')

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

<a name='M-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-#ctor-System-String,System-String,System-Boolean,System-String,System-String,System-Boolean,System-Boolean,System-Boolean,System-Boolean,System-Uri,System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-ClientGenerator-NamedTypeOptions}-'></a>
### #ctor(name,namespace,getDefinitionFromCanister,filePathOrCandidId,outputDirectory,purgeOutputDirectory,noFolders,featureNullable,keepCandidCase,boundryNodeUrl,types) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the client class and file to use |
| namespace | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base namespace to use in the generated files |
| getDefinitionFromCanister | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | If true, will treat `filePathOrCandidId` as a canister id and get the definition from the canister. Otherwise will treat it as a file path and get the definition from the file |
| filePathOrCandidId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The file path to a local *.did file to get definition from or the canister id, depending on `getDefinitionFromCanister` value |
| outputDirectory | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The output directory to generate the client files |
| purgeOutputDirectory | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | If true, removes all files in the output directory before regeneration. Defaults to true |
| noFolders | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | If true, there will be no folders, all files will be in the same directory |
| featureNullable | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | If true, the nullable C# feature will be used |
| keepCandidCase | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | If true, the names of properties and methods will keep the raw candid name. Otherwise they will be converted to something prettier |
| boundryNodeUrl | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | Optional. The url of the boundry node for the internet computer. Defaults to ic0.app |
| types | [System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.ClientGenerator.NamedTypeOptions}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.ClientGenerator.NamedTypeOptions}') | Optional. Specifies options for each candid type in the definition |

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-BoundryNodeUrl'></a>
### BoundryNodeUrl `property`

##### Summary

Optional. The url of the boundry node for the internet computer. Defaults to ic0.app

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-FeatureNullable'></a>
### FeatureNullable `property`

##### Summary

If true, the nullable C# feature will be used

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-FilePathOrCanisterId'></a>
### FilePathOrCanisterId `property`

##### Summary

The file path to a local *.did file to get definition from or the canister id, depending on \`GetDefinitionFromCansiter\` value

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-GetDefinitionFromCansiter'></a>
### GetDefinitionFromCansiter `property`

##### Summary

If true, will treat \`FilePathOrCanisterId\` as a canister id and get the definition from the canister. Otherwise will treat it as a file path and get the definition from the file

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

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-NoFolders'></a>
### NoFolders `property`

##### Summary

If true, there will be no folders, all files will be in the same directory

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-OutputDirectory'></a>
### OutputDirectory `property`

##### Summary

If true, there will be no folders, all files will be in the same directory

<a name='P-EdjCase-ICP-ClientGenerator-ClientGenerationOptions-PurgeOutputDirectory'></a>
### PurgeOutputDirectory `property`

##### Summary

If true, removes all files in the output directory before regeneration, otherwise does nothing. Defaults to true

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

<a name='T-EdjCase-ICP-ClientGenerator-ITypeOptions'></a>
## ITypeOptions `type`

##### Namespace

EdjCase.ICP.ClientGenerator

##### Summary

Interface to specify generation options for specific types in the candid

<a name='P-EdjCase-ICP-ClientGenerator-ITypeOptions-Type'></a>
### Type `property`

##### Summary

The candid type of the implementation class

<a name='T-EdjCase-ICP-ClientGenerator-NamedTypeOptions'></a>
## NamedTypeOptions `type`

##### Namespace

EdjCase.ICP.ClientGenerator

##### Summary

Type options for a record field or variant option

<a name='M-EdjCase-ICP-ClientGenerator-NamedTypeOptions-#ctor-System-String,EdjCase-ICP-ClientGenerator-ITypeOptions-'></a>
### #ctor(nameOverride,typeOptions) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nameOverride | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional. The C# type name to use instead of the default |
| typeOptions | [EdjCase.ICP.ClientGenerator.ITypeOptions](#T-EdjCase-ICP-ClientGenerator-ITypeOptions 'EdjCase.ICP.ClientGenerator.ITypeOptions') | Optional. The field or option type information |

<a name='P-EdjCase-ICP-ClientGenerator-NamedTypeOptions-NameOverride'></a>
### NameOverride `property`

##### Summary

Optional. The C# type name to use instead of the default

<a name='P-EdjCase-ICP-ClientGenerator-NamedTypeOptions-TypeOptions'></a>
### TypeOptions `property`

##### Summary

Optional. The field or option type information

<a name='T-EdjCase-ICP-ClientGenerator-RecordTypeOptions'></a>
## RecordTypeOptions `type`

##### Namespace

EdjCase.ICP.ClientGenerator

##### Summary

Type generation options for record candid types

<a name='M-EdjCase-ICP-ClientGenerator-RecordTypeOptions-#ctor-System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-ClientGenerator-NamedTypeOptions}-'></a>
### #ctor(fields) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fields | [System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.ClientGenerator.NamedTypeOptions}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.ClientGenerator.NamedTypeOptions}') | Optional. The type options for each of the records fields |

<a name='P-EdjCase-ICP-ClientGenerator-RecordTypeOptions-Fields'></a>
### Fields `property`

##### Summary

Optional. The type options for each of the records fields

<a name='P-EdjCase-ICP-ClientGenerator-RecordTypeOptions-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='T-EdjCase-ICP-ClientGenerator-VariantTypeOptions'></a>
## VariantTypeOptions `type`

##### Namespace

EdjCase.ICP.ClientGenerator

##### Summary

Type generation options for variant candid types

<a name='M-EdjCase-ICP-ClientGenerator-VariantTypeOptions-#ctor-System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-ClientGenerator-NamedTypeOptions}-'></a>
### #ctor(options) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.ClientGenerator.NamedTypeOptions}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.ClientGenerator.NamedTypeOptions}') | Optional. The type options for each of the variant options |

<a name='P-EdjCase-ICP-ClientGenerator-VariantTypeOptions-Options'></a>
### Options `property`

##### Summary

Optional. The type options for each of the variant options

<a name='P-EdjCase-ICP-ClientGenerator-VariantTypeOptions-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='T-EdjCase-ICP-ClientGenerator-VectorRepresentation'></a>
## VectorRepresentation `type`

##### Namespace

EdjCase.ICP.ClientGenerator

##### Summary

C# type representations for the vec type

<a name='F-EdjCase-ICP-ClientGenerator-VectorRepresentation-Array'></a>
### Array `constants`

##### Summary

Vector will be an array

<a name='F-EdjCase-ICP-ClientGenerator-VectorRepresentation-Dictionary'></a>
### Dictionary `constants`

##### Summary

Vector will be a dictionary, only if the element type is a record with 2 fields

<a name='F-EdjCase-ICP-ClientGenerator-VectorRepresentation-List'></a>
### List `constants`

##### Summary

Vector will be a List

<a name='T-EdjCase-ICP-ClientGenerator-VectorTypeOptions'></a>
## VectorTypeOptions `type`

##### Namespace

EdjCase.ICP.ClientGenerator

##### Summary

Type generation options for vec candid types

<a name='M-EdjCase-ICP-ClientGenerator-VectorTypeOptions-#ctor-System-Nullable{EdjCase-ICP-ClientGenerator-VectorRepresentation},EdjCase-ICP-ClientGenerator-ITypeOptions-'></a>
### #ctor(representation,elementType) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| representation | [System.Nullable{EdjCase.ICP.ClientGenerator.VectorRepresentation}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{EdjCase.ICP.ClientGenerator.VectorRepresentation}') | Optional. The representation the vec should take in the generated code |
| elementType | [EdjCase.ICP.ClientGenerator.ITypeOptions](#T-EdjCase-ICP-ClientGenerator-ITypeOptions 'EdjCase.ICP.ClientGenerator.ITypeOptions') | Optional. The type options for the element type in the vec |

<a name='P-EdjCase-ICP-ClientGenerator-VectorTypeOptions-ElementType'></a>
### ElementType `property`

##### Summary

Optional. The type options for the element type in the vec

<a name='P-EdjCase-ICP-ClientGenerator-VectorTypeOptions-Representation'></a>
### Representation `property`

##### Summary

Optional. The representation the vec should take in the generated code

<a name='P-EdjCase-ICP-ClientGenerator-VectorTypeOptions-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*
