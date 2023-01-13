param(
    [string]$OutDir = "./dist"
)

dotnet publish src/InternetIdentity/EdjCase.ICP.InternetIdentity/EdjCase.ICP.InternetIdentity.csproj -o $OutDir -c Release /p:DebugType=None /p:DebugSymbols=false
dotnet publish src/Serialization/EdjCase.ICP.Serialization.csproj -o $OutDir -c Release /p:DebugType=None /p:DebugSymbols=false
