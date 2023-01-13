# Internet Identity

## Generating Internet Identity Client

Using client generator tool, from the solution root directory:
```
candid-client-generator \
    -f src/InternetIdentity/EdjCase.ICP.InternetIdentity/internet_identity.did \
    -o src/InternetIdentity/EdjCase.ICP.InternetIdentity/Generated \
    -n "EdjCase.ICP.InternetIdentity" \
    -c InternetIdentity
```

Known issues:
- Some candid type declarations of the following form will fail:
```
type A = ...;
type B = A;
```

This fails because it gets translated to a `using` alias which refers to a previous `using` alias, which is not permitted.

