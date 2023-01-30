<a name='assembly'></a>
# EdjCase.ICP.ClientGenerator

## Contents

- [ClientFileGenerator](#T-EdjCase-ICP-ClientGenerator-ClientFileGenerator 'EdjCase.ICP.ClientGenerator.ClientFileGenerator')
  - [GenerateClientFromCanisterAsync(canisterId,outputDirectory,baseNamespace,clientName,httpBoundryNodeUrl)](#M-EdjCase-ICP-ClientGenerator-ClientFileGenerator-GenerateClientFromCanisterAsync-EdjCase-ICP-Candid-Models-Principal,System-String,System-String,System-String,System-Uri- 'EdjCase.ICP.ClientGenerator.ClientFileGenerator.GenerateClientFromCanisterAsync(EdjCase.ICP.Candid.Models.Principal,System.String,System.String,System.String,System.Uri)')
  - [GenerateClientFromFile(outputDirectory,baseNamespace,clientName,candidFilePath)](#M-EdjCase-ICP-ClientGenerator-ClientFileGenerator-GenerateClientFromFile-System-String,System-String,System-String,System-String- 'EdjCase.ICP.ClientGenerator.ClientFileGenerator.GenerateClientFromFile(System.String,System.String,System.String,System.String)')

<a name='T-EdjCase-ICP-ClientGenerator-ClientFileGenerator'></a>
## ClientFileGenerator `type`

##### Namespace

EdjCase.ICP.ClientGenerator

##### Summary

Generator to create client files based of candid definitions from \`.did\` files
or from a canister id

<a name='M-EdjCase-ICP-ClientGenerator-ClientFileGenerator-GenerateClientFromCanisterAsync-EdjCase-ICP-Candid-Models-Principal,System-String,System-String,System-String,System-Uri-'></a>
### GenerateClientFromCanisterAsync(canisterId,outputDirectory,baseNamespace,clientName,httpBoundryNodeUrl) `method`

##### Summary

Creates client files for a canister based on its id. This only works if 
the canister has the \`candid:service\` meta data available in its public state

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| canisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The canister to get the definition from |
| outputDirectory | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The directory to output to |
| baseNamespace | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base namespace to use in the generated files |
| clientName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional. The name of the client class and file to use. Defaults to 'Service' |
| httpBoundryNodeUrl | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | Optional. The http boundry node url to use, otherwise uses the default |

<a name='M-EdjCase-ICP-ClientGenerator-ClientFileGenerator-GenerateClientFromFile-System-String,System-String,System-String,System-String-'></a>
### GenerateClientFromFile(outputDirectory,baseNamespace,clientName,candidFilePath) `method`

##### Summary

Generates client files for a canister based on a \`.did\` file definition

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| outputDirectory | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The directory to output to |
| baseNamespace | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The base namespace to use in the generated files |
| clientName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional. The name of the client class and file to use. Defaults to 'Service' |
| candidFilePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The path where the \`.did\` definition file is located |
