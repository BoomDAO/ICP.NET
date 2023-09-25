<a name='assembly'></a>
# EdjCase.ICP.Candid

## Contents

- [CRC32](#T-EdjCase-ICP-Candid-Crypto-CRC32 'EdjCase.ICP.Candid.Crypto.CRC32')
  - [ComputeHash(data)](#M-EdjCase-ICP-Candid-Crypto-CRC32-ComputeHash-System-ReadOnlySpan{System-Byte}- 'EdjCase.ICP.Candid.Crypto.CRC32.ComputeHash(System.ReadOnlySpan{System.Byte})')
- [CandidArg](#T-EdjCase-ICP-Candid-Models-CandidArg 'EdjCase.ICP.Candid.Models.CandidArg')
  - [#ctor(values)](#M-EdjCase-ICP-Candid-Models-CandidArg-#ctor-System-Collections-Generic-List{EdjCase-ICP-Candid-Models-CandidTypedValue}- 'EdjCase.ICP.Candid.Models.CandidArg.#ctor(System.Collections.Generic.List{EdjCase.ICP.Candid.Models.CandidTypedValue})')
  - [Values](#P-EdjCase-ICP-Candid-Models-CandidArg-Values 'EdjCase.ICP.Candid.Models.CandidArg.Values')
  - [ComputeHash()](#M-EdjCase-ICP-Candid-Models-CandidArg-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction- 'EdjCase.ICP.Candid.Models.CandidArg.ComputeHash(EdjCase.ICP.Candid.Crypto.IHashFunction)')
  - [Empty()](#M-EdjCase-ICP-Candid-Models-CandidArg-Empty 'EdjCase.ICP.Candid.Models.CandidArg.Empty')
  - [Encode()](#M-EdjCase-ICP-Candid-Models-CandidArg-Encode 'EdjCase.ICP.Candid.Models.CandidArg.Encode')
  - [Encode()](#M-EdjCase-ICP-Candid-Models-CandidArg-Encode-System-Buffers-IBufferWriter{System-Byte}- 'EdjCase.ICP.Candid.Models.CandidArg.Encode(System.Buffers.IBufferWriter{System.Byte})')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-CandidArg-Equals-EdjCase-ICP-Candid-Models-CandidArg- 'EdjCase.ICP.Candid.Models.CandidArg.Equals(EdjCase.ICP.Candid.Models.CandidArg)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-CandidArg-Equals-System-Object- 'EdjCase.ICP.Candid.Models.CandidArg.Equals(System.Object)')
  - [FromBytes(value)](#M-EdjCase-ICP-Candid-Models-CandidArg-FromBytes-System-Byte[]- 'EdjCase.ICP.Candid.Models.CandidArg.FromBytes(System.Byte[])')
  - [FromCandid(values)](#M-EdjCase-ICP-Candid-Models-CandidArg-FromCandid-System-Collections-Generic-List{EdjCase-ICP-Candid-Models-CandidTypedValue}- 'EdjCase.ICP.Candid.Models.CandidArg.FromCandid(System.Collections.Generic.List{EdjCase.ICP.Candid.Models.CandidTypedValue})')
  - [FromCandid(values)](#M-EdjCase-ICP-Candid-Models-CandidArg-FromCandid-EdjCase-ICP-Candid-Models-CandidTypedValue[]- 'EdjCase.ICP.Candid.Models.CandidArg.FromCandid(EdjCase.ICP.Candid.Models.CandidTypedValue[])')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-CandidArg-GetHashCode 'EdjCase.ICP.Candid.Models.CandidArg.GetHashCode')
  - [ToObjects\`\`1(candidConverter)](#M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``1-EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Models.CandidArg.ToObjects``1(EdjCase.ICP.Candid.CandidConverter)')
  - [ToObjects\`\`2(candidConverter)](#M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``2-EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Models.CandidArg.ToObjects``2(EdjCase.ICP.Candid.CandidConverter)')
  - [ToObjects\`\`3(candidConverter)](#M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``3-EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Models.CandidArg.ToObjects``3(EdjCase.ICP.Candid.CandidConverter)')
  - [ToObjects\`\`4(candidConverter)](#M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``4-EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Models.CandidArg.ToObjects``4(EdjCase.ICP.Candid.CandidConverter)')
  - [ToObjects\`\`5(candidConverter)](#M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``5-EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Models.CandidArg.ToObjects``5(EdjCase.ICP.Candid.CandidConverter)')
  - [ToObjects\`\`6(candidConverter)](#M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``6-EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Models.CandidArg.ToObjects``6(EdjCase.ICP.Candid.CandidConverter)')
  - [ToObjects\`\`7(candidConverter)](#M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``7-EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Models.CandidArg.ToObjects``7(EdjCase.ICP.Candid.CandidConverter)')
  - [ToObjects\`\`8(candidConverter)](#M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``8-EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Models.CandidArg.ToObjects``8(EdjCase.ICP.Candid.CandidConverter)')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-CandidArg-ToString 'EdjCase.ICP.Candid.Models.CandidArg.ToString')
  - [op_Equality()](#M-EdjCase-ICP-Candid-Models-CandidArg-op_Equality-EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-CandidArg- 'EdjCase.ICP.Candid.Models.CandidArg.op_Equality(EdjCase.ICP.Candid.Models.CandidArg,EdjCase.ICP.Candid.Models.CandidArg)')
  - [op_Inequality()](#M-EdjCase-ICP-Candid-Models-CandidArg-op_Inequality-EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-CandidArg- 'EdjCase.ICP.Candid.Models.CandidArg.op_Inequality(EdjCase.ICP.Candid.Models.CandidArg,EdjCase.ICP.Candid.Models.CandidArg)')
- [CandidByteParser](#T-EdjCase-ICP-Candid-Parsers-CandidByteParser 'EdjCase.ICP.Candid.Parsers.CandidByteParser')
  - [Parse(value)](#M-EdjCase-ICP-Candid-Parsers-CandidByteParser-Parse-System-Byte[]- 'EdjCase.ICP.Candid.Parsers.CandidByteParser.Parse(System.Byte[])')
- [CandidCompoundType](#T-EdjCase-ICP-Candid-Models-Types-CandidCompoundType 'EdjCase.ICP.Candid.Models.Types.CandidCompoundType')
  - [#ctor(recursiveId)](#M-EdjCase-ICP-Candid-Models-Types-CandidCompoundType-#ctor-EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.Types.CandidCompoundType.#ctor(EdjCase.ICP.Candid.Models.CandidId)')
  - [RecursiveId](#P-EdjCase-ICP-Candid-Models-Types-CandidCompoundType-RecursiveId 'EdjCase.ICP.Candid.Models.Types.CandidCompoundType.RecursiveId')
- [CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter')
  - [#ctor(options)](#M-EdjCase-ICP-Candid-CandidConverter-#ctor-EdjCase-ICP-Candid-CandidConverterOptions- 'EdjCase.ICP.Candid.CandidConverter.#ctor(EdjCase.ICP.Candid.CandidConverterOptions)')
  - [#ctor(configureOptions)](#M-EdjCase-ICP-Candid-CandidConverter-#ctor-System-Action{EdjCase-ICP-Candid-CandidConverterOptions}- 'EdjCase.ICP.Candid.CandidConverter.#ctor(System.Action{EdjCase.ICP.Candid.CandidConverterOptions})')
  - [Default](#P-EdjCase-ICP-Candid-CandidConverter-Default 'EdjCase.ICP.Candid.CandidConverter.Default')
  - [FromObject(obj)](#M-EdjCase-ICP-Candid-CandidConverter-FromObject-System-Object- 'EdjCase.ICP.Candid.CandidConverter.FromObject(System.Object)')
  - [FromTypedObject\`\`1(obj)](#M-EdjCase-ICP-Candid-CandidConverter-FromTypedObject``1-``0- 'EdjCase.ICP.Candid.CandidConverter.FromTypedObject``1(``0)')
  - [ToObject(objType,value)](#M-EdjCase-ICP-Candid-CandidConverter-ToObject-System-Type,EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.CandidConverter.ToObject(System.Type,EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [ToObject\`\`1(value)](#M-EdjCase-ICP-Candid-CandidConverter-ToObject``1-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.CandidConverter.ToObject``1(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [ToOptionalObject(objType,value)](#M-EdjCase-ICP-Candid-CandidConverter-ToOptionalObject-System-Type,EdjCase-ICP-Candid-Models-Values-CandidOptional- 'EdjCase.ICP.Candid.CandidConverter.ToOptionalObject(System.Type,EdjCase.ICP.Candid.Models.Values.CandidOptional)')
  - [ToOptionalObject\`\`1(value)](#M-EdjCase-ICP-Candid-CandidConverter-ToOptionalObject``1-EdjCase-ICP-Candid-Models-Values-CandidOptional- 'EdjCase.ICP.Candid.CandidConverter.ToOptionalObject``1(EdjCase.ICP.Candid.Models.Values.CandidOptional)')
- [CandidConverterOptions](#T-EdjCase-ICP-Candid-CandidConverterOptions 'EdjCase.ICP.Candid.CandidConverterOptions')
  - [CustomMappers](#P-EdjCase-ICP-Candid-CandidConverterOptions-CustomMappers 'EdjCase.ICP.Candid.CandidConverterOptions.CustomMappers')
  - [AddCustomMapper(mapper)](#M-EdjCase-ICP-Candid-CandidConverterOptions-AddCustomMapper-EdjCase-ICP-Candid-Mapping-ICandidValueMapper- 'EdjCase.ICP.Candid.CandidConverterOptions.AddCustomMapper(EdjCase.ICP.Candid.Mapping.ICandidValueMapper)')
  - [AddCustomMapper\`\`1()](#M-EdjCase-ICP-Candid-CandidConverterOptions-AddCustomMapper``1 'EdjCase.ICP.Candid.CandidConverterOptions.AddCustomMapper``1')
- [CandidDecodingException](#T-EdjCase-ICP-Candid-Exceptions-CandidDecodingException 'EdjCase.ICP.Candid.Exceptions.CandidDecodingException')
  - [#ctor(byteEndIndex,message)](#M-EdjCase-ICP-Candid-Exceptions-CandidDecodingException-#ctor-System-Int32,System-String- 'EdjCase.ICP.Candid.Exceptions.CandidDecodingException.#ctor(System.Int32,System.String)')
  - [ByteEndIndex](#P-EdjCase-ICP-Candid-Exceptions-CandidDecodingException-ByteEndIndex 'EdjCase.ICP.Candid.Exceptions.CandidDecodingException.ByteEndIndex')
  - [ErrorMessage](#P-EdjCase-ICP-Candid-Exceptions-CandidDecodingException-ErrorMessage 'EdjCase.ICP.Candid.Exceptions.CandidDecodingException.ErrorMessage')
  - [Message](#P-EdjCase-ICP-Candid-Exceptions-CandidDecodingException-Message 'EdjCase.ICP.Candid.Exceptions.CandidDecodingException.Message')
- [CandidFunc](#T-EdjCase-ICP-Candid-Models-Values-CandidFunc 'EdjCase.ICP.Candid.Models.Values.CandidFunc')
  - [#ctor(service,name)](#M-EdjCase-ICP-Candid-Models-Values-CandidFunc-#ctor-EdjCase-ICP-Candid-Models-Values-CandidService,System-String- 'EdjCase.ICP.Candid.Models.Values.CandidFunc.#ctor(EdjCase.ICP.Candid.Models.Values.CandidService,System.String)')
  - [IsOpaqueReference](#P-EdjCase-ICP-Candid-Models-Values-CandidFunc-IsOpaqueReference 'EdjCase.ICP.Candid.Models.Values.CandidFunc.IsOpaqueReference')
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidFunc-Type 'EdjCase.ICP.Candid.Models.Values.CandidFunc.Type')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidFunc-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}- 'EdjCase.ICP.Candid.Models.Values.CandidFunc.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType},System.Buffers.IBufferWriter{System.Byte})')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidFunc-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidFunc.Equals(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Values-CandidFunc-GetHashCode 'EdjCase.ICP.Candid.Models.Values.CandidFunc.GetHashCode')
  - [OpaqueReference()](#M-EdjCase-ICP-Candid-Models-Values-CandidFunc-OpaqueReference 'EdjCase.ICP.Candid.Models.Values.CandidFunc.OpaqueReference')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Values-CandidFunc-ToString 'EdjCase.ICP.Candid.Models.Values.CandidFunc.ToString')
- [CandidFuncType](#T-EdjCase-ICP-Candid-Models-Types-CandidFuncType 'EdjCase.ICP.Candid.Models.Types.CandidFuncType')
  - [#ctor(modes,argTypes,returnTypes,recursiveId)](#M-EdjCase-ICP-Candid-Models-Types-CandidFuncType-#ctor-System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Types-FuncMode},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Types-CandidType},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Types-CandidType},EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.Types.CandidFuncType.#ctor(System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Types.FuncMode},System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Types.CandidType},System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Types.CandidType},EdjCase.ICP.Candid.Models.CandidId)')
  - [#ctor(modes,argTypes,returnTypes,recursiveId)](#M-EdjCase-ICP-Candid-Models-Types-CandidFuncType-#ctor-System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Types-FuncMode},System-Collections-Generic-List{System-ValueTuple{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidType}},System-Collections-Generic-List{System-ValueTuple{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidType}},EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.Types.CandidFuncType.#ctor(System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Types.FuncMode},System.Collections.Generic.List{System.ValueTuple{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidType}},System.Collections.Generic.List{System.ValueTuple{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidType}},EdjCase.ICP.Candid.Models.CandidId)')
  - [ArgTypes](#P-EdjCase-ICP-Candid-Models-Types-CandidFuncType-ArgTypes 'EdjCase.ICP.Candid.Models.Types.CandidFuncType.ArgTypes')
  - [Modes](#P-EdjCase-ICP-Candid-Models-Types-CandidFuncType-Modes 'EdjCase.ICP.Candid.Models.Types.CandidFuncType.Modes')
  - [ReturnTypes](#P-EdjCase-ICP-Candid-Models-Types-CandidFuncType-ReturnTypes 'EdjCase.ICP.Candid.Models.Types.CandidFuncType.ReturnTypes')
  - [Type](#P-EdjCase-ICP-Candid-Models-Types-CandidFuncType-Type 'EdjCase.ICP.Candid.Models.Types.CandidFuncType.Type')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Types-CandidFuncType-Equals-System-Object- 'EdjCase.ICP.Candid.Models.Types.CandidFuncType.Equals(System.Object)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Types-CandidFuncType-GetHashCode 'EdjCase.ICP.Candid.Models.Types.CandidFuncType.GetHashCode')
- [CandidId](#T-EdjCase-ICP-Candid-Models-CandidId 'EdjCase.ICP.Candid.Models.CandidId')
  - [Value](#P-EdjCase-ICP-Candid-Models-CandidId-Value 'EdjCase.ICP.Candid.Models.CandidId.Value')
  - [CompareTo()](#M-EdjCase-ICP-Candid-Models-CandidId-CompareTo-EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.CandidId.CompareTo(EdjCase.ICP.Candid.Models.CandidId)')
  - [Create(value)](#M-EdjCase-ICP-Candid-Models-CandidId-Create-System-String- 'EdjCase.ICP.Candid.Models.CandidId.Create(System.String)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-CandidId-Equals-System-Object- 'EdjCase.ICP.Candid.Models.CandidId.Equals(System.Object)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-CandidId-Equals-EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.CandidId.Equals(EdjCase.ICP.Candid.Models.CandidId)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-CandidId-Equals-System-String- 'EdjCase.ICP.Candid.Models.CandidId.Equals(System.String)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-CandidId-GetHashCode 'EdjCase.ICP.Candid.Models.CandidId.GetHashCode')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-CandidId-ToString 'EdjCase.ICP.Candid.Models.CandidId.ToString')
  - [op_Equality()](#M-EdjCase-ICP-Candid-Models-CandidId-op_Equality-EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.CandidId.op_Equality(EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.CandidId)')
  - [op_Inequality()](#M-EdjCase-ICP-Candid-Models-CandidId-op_Inequality-EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.CandidId.op_Inequality(EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.CandidId)')
- [CandidIgnoreAttribute](#T-EdjCase-ICP-Candid-Mapping-CandidIgnoreAttribute 'EdjCase.ICP.Candid.Mapping.CandidIgnoreAttribute')
- [CandidKnownType](#T-EdjCase-ICP-Candid-Models-Types-CandidKnownType 'EdjCase.ICP.Candid.Models.Types.CandidKnownType')
  - [Type](#P-EdjCase-ICP-Candid-Models-Types-CandidKnownType-Type 'EdjCase.ICP.Candid.Models.Types.CandidKnownType.Type')
- [CandidNameAttribute](#T-EdjCase-ICP-Candid-Mapping-CandidNameAttribute 'EdjCase.ICP.Candid.Mapping.CandidNameAttribute')
  - [#ctor(name)](#M-EdjCase-ICP-Candid-Mapping-CandidNameAttribute-#ctor-System-String- 'EdjCase.ICP.Candid.Mapping.CandidNameAttribute.#ctor(System.String)')
- [CandidOptional](#T-EdjCase-ICP-Candid-Models-Values-CandidOptional 'EdjCase.ICP.Candid.Models.Values.CandidOptional')
  - [#ctor(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidOptional-#ctor-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidOptional.#ctor(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidOptional-Type 'EdjCase.ICP.Candid.Models.Values.CandidOptional.Type')
  - [Value](#P-EdjCase-ICP-Candid-Models-Values-CandidOptional-Value 'EdjCase.ICP.Candid.Models.Values.CandidOptional.Value')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidOptional-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}- 'EdjCase.ICP.Candid.Models.Values.CandidOptional.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType},System.Buffers.IBufferWriter{System.Byte})')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidOptional-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidOptional.Equals(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Values-CandidOptional-GetHashCode 'EdjCase.ICP.Candid.Models.Values.CandidOptional.GetHashCode')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Values-CandidOptional-ToString 'EdjCase.ICP.Candid.Models.Values.CandidOptional.ToString')
- [CandidOptionalType](#T-EdjCase-ICP-Candid-Models-Types-CandidOptionalType 'EdjCase.ICP.Candid.Models.Types.CandidOptionalType')
  - [#ctor(value,recursiveId)](#M-EdjCase-ICP-Candid-Models-Types-CandidOptionalType-#ctor-EdjCase-ICP-Candid-Models-Types-CandidType,EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.Types.CandidOptionalType.#ctor(EdjCase.ICP.Candid.Models.Types.CandidType,EdjCase.ICP.Candid.Models.CandidId)')
  - [Type](#P-EdjCase-ICP-Candid-Models-Types-CandidOptionalType-Type 'EdjCase.ICP.Candid.Models.Types.CandidOptionalType.Type')
  - [Value](#P-EdjCase-ICP-Candid-Models-Types-CandidOptionalType-Value 'EdjCase.ICP.Candid.Models.Types.CandidOptionalType.Value')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Types-CandidOptionalType-Equals-System-Object- 'EdjCase.ICP.Candid.Models.Types.CandidOptionalType.Equals(System.Object)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Types-CandidOptionalType-GetHashCode 'EdjCase.ICP.Candid.Models.Types.CandidOptionalType.GetHashCode')
- [CandidPrimitive](#T-EdjCase-ICP-Candid-Models-Values-CandidPrimitive 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive')
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-Type 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.Type')
  - [ValueType](#P-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-ValueType 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.ValueType')
  - [AsBool()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsBool 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsBool')
  - [AsFloat32()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsFloat32 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsFloat32')
  - [AsFloat64()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsFloat64 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsFloat64')
  - [AsInt()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsInt 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsInt')
  - [AsInt16()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsInt16 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsInt16')
  - [AsInt32()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsInt32 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsInt32')
  - [AsInt64()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsInt64 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsInt64')
  - [AsInt8()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsInt8 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsInt8')
  - [AsNat()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsNat 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsNat')
  - [AsNat16()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsNat16 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsNat16')
  - [AsNat32()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsNat32 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsNat32')
  - [AsNat64()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsNat64 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsNat64')
  - [AsNat8()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsNat8 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsNat8')
  - [AsPrincipal()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsPrincipal 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsPrincipal')
  - [AsText()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsText 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsText')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}- 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType},System.Buffers.IBufferWriter{System.Byte})')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.Equals(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-GetHashCode 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.GetHashCode')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-ToString 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.ToString')
- [CandidPrimitiveType](#T-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType 'EdjCase.ICP.Candid.Models.Types.CandidPrimitiveType')
  - [#ctor(type)](#M-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType-#ctor-EdjCase-ICP-Candid-Models-Values-PrimitiveType- 'EdjCase.ICP.Candid.Models.Types.CandidPrimitiveType.#ctor(EdjCase.ICP.Candid.Models.Values.PrimitiveType)')
  - [PrimitiveType](#P-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType-PrimitiveType 'EdjCase.ICP.Candid.Models.Types.CandidPrimitiveType.PrimitiveType')
  - [Type](#P-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType-Type 'EdjCase.ICP.Candid.Models.Types.CandidPrimitiveType.Type')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType-Equals-System-Object- 'EdjCase.ICP.Candid.Models.Types.CandidPrimitiveType.Equals(System.Object)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType-GetHashCode 'EdjCase.ICP.Candid.Models.Types.CandidPrimitiveType.GetHashCode')
- [CandidRecord](#T-EdjCase-ICP-Candid-Models-Values-CandidRecord 'EdjCase.ICP.Candid.Models.Values.CandidRecord')
  - [#ctor(fields)](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-#ctor-System-Collections-Generic-Dictionary{EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-Values-CandidValue}- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.#ctor(System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Values.CandidValue})')
  - [Fields](#P-EdjCase-ICP-Candid-Models-Values-CandidRecord-Fields 'EdjCase.ICP.Candid.Models.Values.CandidRecord.Fields')
  - [Item](#P-EdjCase-ICP-Candid-Models-Values-CandidRecord-Item-System-String- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.Item(System.String)')
  - [Item](#P-EdjCase-ICP-Candid-Models-Values-CandidRecord-Item-System-UInt32- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.Item(System.UInt32)')
  - [Item](#P-EdjCase-ICP-Candid-Models-Values-CandidRecord-Item-EdjCase-ICP-Candid-Models-CandidTag- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.Item(EdjCase.ICP.Candid.Models.CandidTag)')
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidRecord-Type 'EdjCase.ICP.Candid.Models.Values.CandidRecord.Type')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType},System.Buffers.IBufferWriter{System.Byte})')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.Equals(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [FromDictionary(fields)](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-FromDictionary-System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-Candid-Models-Values-CandidValue}- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.FromDictionary(System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.Values.CandidValue})')
  - [FromDictionary(fields)](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-FromDictionary-System-Collections-Generic-Dictionary{System-UInt32,EdjCase-ICP-Candid-Models-Values-CandidValue}- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.FromDictionary(System.Collections.Generic.Dictionary{System.UInt32,EdjCase.ICP.Candid.Models.Values.CandidValue})')
  - [FromDictionary(fields)](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-FromDictionary-System-Collections-Generic-Dictionary{EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-Values-CandidValue}- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.FromDictionary(System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Values.CandidValue})')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-GetHashCode 'EdjCase.ICP.Candid.Models.Values.CandidRecord.GetHashCode')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-ToString 'EdjCase.ICP.Candid.Models.Values.CandidRecord.ToString')
  - [TryGetField(name,value)](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-TryGetField-System-String,EdjCase-ICP-Candid-Models-Values-CandidValue@- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.TryGetField(System.String,EdjCase.ICP.Candid.Models.Values.CandidValue@)')
  - [TryGetField(id,value)](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-TryGetField-System-UInt32,EdjCase-ICP-Candid-Models-Values-CandidValue@- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.TryGetField(System.UInt32,EdjCase.ICP.Candid.Models.Values.CandidValue@)')
  - [TryGetField(tag,value)](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-TryGetField-EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-Values-CandidValue@- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.TryGetField(EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Values.CandidValue@)')
- [CandidRecordOrVariantType](#T-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType 'EdjCase.ICP.Candid.Models.Types.CandidRecordOrVariantType')
  - [#ctor()](#M-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType-#ctor-EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.Types.CandidRecordOrVariantType.#ctor(EdjCase.ICP.Candid.Models.CandidId)')
  - [Type](#P-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType-Type 'EdjCase.ICP.Candid.Models.Types.CandidRecordOrVariantType.Type')
  - [TypeString](#P-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType-TypeString 'EdjCase.ICP.Candid.Models.Types.CandidRecordOrVariantType.TypeString')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType-Equals-System-Object- 'EdjCase.ICP.Candid.Models.Types.CandidRecordOrVariantType.Equals(System.Object)')
  - [GetFieldsOrOptions()](#M-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType-GetFieldsOrOptions 'EdjCase.ICP.Candid.Models.Types.CandidRecordOrVariantType.GetFieldsOrOptions')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType-GetHashCode 'EdjCase.ICP.Candid.Models.Types.CandidRecordOrVariantType.GetHashCode')
- [CandidRecordType](#T-EdjCase-ICP-Candid-Models-Types-CandidRecordType 'EdjCase.ICP.Candid.Models.Types.CandidRecordType')
  - [#ctor(fields,recursiveId)](#M-EdjCase-ICP-Candid-Models-Types-CandidRecordType-#ctor-System-Collections-Generic-Dictionary{EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-Types-CandidType},EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.Types.CandidRecordType.#ctor(System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Types.CandidType},EdjCase.ICP.Candid.Models.CandidId)')
  - [Fields](#P-EdjCase-ICP-Candid-Models-Types-CandidRecordType-Fields 'EdjCase.ICP.Candid.Models.Types.CandidRecordType.Fields')
  - [Type](#P-EdjCase-ICP-Candid-Models-Types-CandidRecordType-Type 'EdjCase.ICP.Candid.Models.Types.CandidRecordType.Type')
  - [TypeString](#P-EdjCase-ICP-Candid-Models-Types-CandidRecordType-TypeString 'EdjCase.ICP.Candid.Models.Types.CandidRecordType.TypeString')
  - [GetFieldsOrOptions()](#M-EdjCase-ICP-Candid-Models-Types-CandidRecordType-GetFieldsOrOptions 'EdjCase.ICP.Candid.Models.Types.CandidRecordType.GetFieldsOrOptions')
- [CandidReferenceType](#T-EdjCase-ICP-Candid-Models-Types-CandidReferenceType 'EdjCase.ICP.Candid.Models.Types.CandidReferenceType')
  - [#ctor(id)](#M-EdjCase-ICP-Candid-Models-Types-CandidReferenceType-#ctor-EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.Types.CandidReferenceType.#ctor(EdjCase.ICP.Candid.Models.CandidId)')
  - [Id](#P-EdjCase-ICP-Candid-Models-Types-CandidReferenceType-Id 'EdjCase.ICP.Candid.Models.Types.CandidReferenceType.Id')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Types-CandidReferenceType-Equals-System-Object- 'EdjCase.ICP.Candid.Models.Types.CandidReferenceType.Equals(System.Object)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Types-CandidReferenceType-GetHashCode 'EdjCase.ICP.Candid.Models.Types.CandidReferenceType.GetHashCode')
- [CandidService](#T-EdjCase-ICP-Candid-Models-Values-CandidService 'EdjCase.ICP.Candid.Models.Values.CandidService')
  - [#ctor(principalId)](#M-EdjCase-ICP-Candid-Models-Values-CandidService-#ctor-EdjCase-ICP-Candid-Models-Principal- 'EdjCase.ICP.Candid.Models.Values.CandidService.#ctor(EdjCase.ICP.Candid.Models.Principal)')
  - [IsOpqaueReference](#P-EdjCase-ICP-Candid-Models-Values-CandidService-IsOpqaueReference 'EdjCase.ICP.Candid.Models.Values.CandidService.IsOpqaueReference')
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidService-Type 'EdjCase.ICP.Candid.Models.Values.CandidService.Type')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidService-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}- 'EdjCase.ICP.Candid.Models.Values.CandidService.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType},System.Buffers.IBufferWriter{System.Byte})')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidService-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidService.Equals(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Values-CandidService-GetHashCode 'EdjCase.ICP.Candid.Models.Values.CandidService.GetHashCode')
  - [GetPrincipal()](#M-EdjCase-ICP-Candid-Models-Values-CandidService-GetPrincipal 'EdjCase.ICP.Candid.Models.Values.CandidService.GetPrincipal')
  - [OpaqueReference()](#M-EdjCase-ICP-Candid-Models-Values-CandidService-OpaqueReference 'EdjCase.ICP.Candid.Models.Values.CandidService.OpaqueReference')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Values-CandidService-ToString 'EdjCase.ICP.Candid.Models.Values.CandidService.ToString')
- [CandidServiceDescription](#T-EdjCase-ICP-Candid-Models-CandidServiceDescription 'EdjCase.ICP.Candid.Models.CandidServiceDescription')
  - [#ctor(service,declaredTypes,serviceReferenceId)](#M-EdjCase-ICP-Candid-Models-CandidServiceDescription-#ctor-EdjCase-ICP-Candid-Models-Types-CandidServiceType,System-Collections-Generic-Dictionary{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidType},EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.CandidServiceDescription.#ctor(EdjCase.ICP.Candid.Models.Types.CandidServiceType,System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidType},EdjCase.ICP.Candid.Models.CandidId)')
  - [DeclaredTypes](#P-EdjCase-ICP-Candid-Models-CandidServiceDescription-DeclaredTypes 'EdjCase.ICP.Candid.Models.CandidServiceDescription.DeclaredTypes')
  - [Service](#P-EdjCase-ICP-Candid-Models-CandidServiceDescription-Service 'EdjCase.ICP.Candid.Models.CandidServiceDescription.Service')
  - [ServiceReferenceId](#P-EdjCase-ICP-Candid-Models-CandidServiceDescription-ServiceReferenceId 'EdjCase.ICP.Candid.Models.CandidServiceDescription.ServiceReferenceId')
  - [Parse(text)](#M-EdjCase-ICP-Candid-Models-CandidServiceDescription-Parse-System-String- 'EdjCase.ICP.Candid.Models.CandidServiceDescription.Parse(System.String)')
- [CandidServiceType](#T-EdjCase-ICP-Candid-Models-Types-CandidServiceType 'EdjCase.ICP.Candid.Models.Types.CandidServiceType')
  - [#ctor(methods,recursiveId)](#M-EdjCase-ICP-Candid-Models-Types-CandidServiceType-#ctor-System-Collections-Generic-Dictionary{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidFuncType},EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.Types.CandidServiceType.#ctor(System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidFuncType},EdjCase.ICP.Candid.Models.CandidId)')
  - [Methods](#P-EdjCase-ICP-Candid-Models-Types-CandidServiceType-Methods 'EdjCase.ICP.Candid.Models.Types.CandidServiceType.Methods')
  - [Type](#P-EdjCase-ICP-Candid-Models-Types-CandidServiceType-Type 'EdjCase.ICP.Candid.Models.Types.CandidServiceType.Type')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Types-CandidServiceType-Equals-System-Object- 'EdjCase.ICP.Candid.Models.Types.CandidServiceType.Equals(System.Object)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Types-CandidServiceType-GetHashCode 'EdjCase.ICP.Candid.Models.Types.CandidServiceType.GetHashCode')
- [CandidTag](#T-EdjCase-ICP-Candid-Models-CandidTag 'EdjCase.ICP.Candid.Models.CandidTag')
  - [#ctor(id)](#M-EdjCase-ICP-Candid-Models-CandidTag-#ctor-System-UInt32- 'EdjCase.ICP.Candid.Models.CandidTag.#ctor(System.UInt32)')
  - [Id](#P-EdjCase-ICP-Candid-Models-CandidTag-Id 'EdjCase.ICP.Candid.Models.CandidTag.Id')
  - [Name](#P-EdjCase-ICP-Candid-Models-CandidTag-Name 'EdjCase.ICP.Candid.Models.CandidTag.Name')
  - [CompareTo()](#M-EdjCase-ICP-Candid-Models-CandidTag-CompareTo-System-Object- 'EdjCase.ICP.Candid.Models.CandidTag.CompareTo(System.Object)')
  - [CompareTo()](#M-EdjCase-ICP-Candid-Models-CandidTag-CompareTo-EdjCase-ICP-Candid-Models-CandidTag- 'EdjCase.ICP.Candid.Models.CandidTag.CompareTo(EdjCase.ICP.Candid.Models.CandidTag)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-CandidTag-Equals-EdjCase-ICP-Candid-Models-CandidTag- 'EdjCase.ICP.Candid.Models.CandidTag.Equals(EdjCase.ICP.Candid.Models.CandidTag)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-CandidTag-Equals-System-Object- 'EdjCase.ICP.Candid.Models.CandidTag.Equals(System.Object)')
  - [FromId(id)](#M-EdjCase-ICP-Candid-Models-CandidTag-FromId-System-UInt32- 'EdjCase.ICP.Candid.Models.CandidTag.FromId(System.UInt32)')
  - [FromName(name)](#M-EdjCase-ICP-Candid-Models-CandidTag-FromName-System-String- 'EdjCase.ICP.Candid.Models.CandidTag.FromName(System.String)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-CandidTag-GetHashCode 'EdjCase.ICP.Candid.Models.CandidTag.GetHashCode')
  - [HashName(name)](#M-EdjCase-ICP-Candid-Models-CandidTag-HashName-System-String- 'EdjCase.ICP.Candid.Models.CandidTag.HashName(System.String)')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-CandidTag-ToString 'EdjCase.ICP.Candid.Models.CandidTag.ToString')
  - [op_Equality()](#M-EdjCase-ICP-Candid-Models-CandidTag-op_Equality-EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-CandidTag- 'EdjCase.ICP.Candid.Models.CandidTag.op_Equality(EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.CandidTag)')
  - [op_Implicit(name)](#M-EdjCase-ICP-Candid-Models-CandidTag-op_Implicit-System-String-~EdjCase-ICP-Candid-Models-CandidTag 'EdjCase.ICP.Candid.Models.CandidTag.op_Implicit(System.String)~EdjCase.ICP.Candid.Models.CandidTag')
  - [op_Implicit(id)](#M-EdjCase-ICP-Candid-Models-CandidTag-op_Implicit-System-UInt32-~EdjCase-ICP-Candid-Models-CandidTag 'EdjCase.ICP.Candid.Models.CandidTag.op_Implicit(System.UInt32)~EdjCase.ICP.Candid.Models.CandidTag')
  - [op_Implicit(tag)](#M-EdjCase-ICP-Candid-Models-CandidTag-op_Implicit-EdjCase-ICP-Candid-Models-CandidTag-~System-UInt32 'EdjCase.ICP.Candid.Models.CandidTag.op_Implicit(EdjCase.ICP.Candid.Models.CandidTag)~System.UInt32')
  - [op_Inequality()](#M-EdjCase-ICP-Candid-Models-CandidTag-op_Inequality-EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-CandidTag- 'EdjCase.ICP.Candid.Models.CandidTag.op_Inequality(EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.CandidTag)')
- [CandidTagAttribute](#T-EdjCase-ICP-Candid-Mapping-CandidTagAttribute 'EdjCase.ICP.Candid.Mapping.CandidTagAttribute')
  - [#ctor(tag)](#M-EdjCase-ICP-Candid-Mapping-CandidTagAttribute-#ctor-EdjCase-ICP-Candid-Models-CandidTag- 'EdjCase.ICP.Candid.Mapping.CandidTagAttribute.#ctor(EdjCase.ICP.Candid.Models.CandidTag)')
  - [#ctor(id)](#M-EdjCase-ICP-Candid-Mapping-CandidTagAttribute-#ctor-System-UInt32- 'EdjCase.ICP.Candid.Mapping.CandidTagAttribute.#ctor(System.UInt32)')
  - [#ctor(name)](#M-EdjCase-ICP-Candid-Mapping-CandidTagAttribute-#ctor-System-String- 'EdjCase.ICP.Candid.Mapping.CandidTagAttribute.#ctor(System.String)')
  - [Tag](#P-EdjCase-ICP-Candid-Mapping-CandidTagAttribute-Tag 'EdjCase.ICP.Candid.Mapping.CandidTagAttribute.Tag')
- [CandidTextParseException](#T-EdjCase-ICP-Candid-Exceptions-CandidTextParseException 'EdjCase.ICP.Candid.Exceptions.CandidTextParseException')
  - [Message](#P-EdjCase-ICP-Candid-Exceptions-CandidTextParseException-Message 'EdjCase.ICP.Candid.Exceptions.CandidTextParseException.Message')
- [CandidType](#T-EdjCase-ICP-Candid-Models-Types-CandidType 'EdjCase.ICP.Candid.Models.Types.CandidType')
  - [Bool()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Bool 'EdjCase.ICP.Candid.Models.Types.CandidType.Bool')
  - [Empty()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Empty 'EdjCase.ICP.Candid.Models.Types.CandidType.Empty')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Equals-System-Object- 'EdjCase.ICP.Candid.Models.Types.CandidType.Equals(System.Object)')
  - [Equals(other)](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Equals-EdjCase-ICP-Candid-Models-Types-CandidType- 'EdjCase.ICP.Candid.Models.Types.CandidType.Equals(EdjCase.ICP.Candid.Models.Types.CandidType)')
  - [Float32()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Float32 'EdjCase.ICP.Candid.Models.Types.CandidType.Float32')
  - [Float64()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Float64 'EdjCase.ICP.Candid.Models.Types.CandidType.Float64')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-GetHashCode 'EdjCase.ICP.Candid.Models.Types.CandidType.GetHashCode')
  - [Int()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Int 'EdjCase.ICP.Candid.Models.Types.CandidType.Int')
  - [Int16()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Int16 'EdjCase.ICP.Candid.Models.Types.CandidType.Int16')
  - [Int32()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Int32 'EdjCase.ICP.Candid.Models.Types.CandidType.Int32')
  - [Int64()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Int64 'EdjCase.ICP.Candid.Models.Types.CandidType.Int64')
  - [Int8()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Int8 'EdjCase.ICP.Candid.Models.Types.CandidType.Int8')
  - [Nat()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Nat 'EdjCase.ICP.Candid.Models.Types.CandidType.Nat')
  - [Nat16()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Nat16 'EdjCase.ICP.Candid.Models.Types.CandidType.Nat16')
  - [Nat32()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Nat32 'EdjCase.ICP.Candid.Models.Types.CandidType.Nat32')
  - [Nat64()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Nat64 'EdjCase.ICP.Candid.Models.Types.CandidType.Nat64')
  - [Nat8()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Nat8 'EdjCase.ICP.Candid.Models.Types.CandidType.Nat8')
  - [Null()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Null 'EdjCase.ICP.Candid.Models.Types.CandidType.Null')
  - [Opt()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Opt-EdjCase-ICP-Candid-Models-Types-CandidType- 'EdjCase.ICP.Candid.Models.Types.CandidType.Opt(EdjCase.ICP.Candid.Models.Types.CandidType)')
  - [Principal()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Principal 'EdjCase.ICP.Candid.Models.Types.CandidType.Principal')
  - [Reserved()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Reserved 'EdjCase.ICP.Candid.Models.Types.CandidType.Reserved')
  - [Text()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Text 'EdjCase.ICP.Candid.Models.Types.CandidType.Text')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-ToString 'EdjCase.ICP.Candid.Models.Types.CandidType.ToString')
  - [op_Equality()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-op_Equality-EdjCase-ICP-Candid-Models-Types-CandidType,EdjCase-ICP-Candid-Models-Types-CandidType- 'EdjCase.ICP.Candid.Models.Types.CandidType.op_Equality(EdjCase.ICP.Candid.Models.Types.CandidType,EdjCase.ICP.Candid.Models.Types.CandidType)')
  - [op_Inequality()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-op_Inequality-EdjCase-ICP-Candid-Models-Types-CandidType,EdjCase-ICP-Candid-Models-Types-CandidType- 'EdjCase.ICP.Candid.Models.Types.CandidType.op_Inequality(EdjCase.ICP.Candid.Models.Types.CandidType,EdjCase.ICP.Candid.Models.Types.CandidType)')
- [CandidTypeCode](#T-EdjCase-ICP-Candid-CandidTypeCode 'EdjCase.ICP.Candid.CandidTypeCode')
  - [Bool](#F-EdjCase-ICP-Candid-CandidTypeCode-Bool 'EdjCase.ICP.Candid.CandidTypeCode.Bool')
  - [Empty](#F-EdjCase-ICP-Candid-CandidTypeCode-Empty 'EdjCase.ICP.Candid.CandidTypeCode.Empty')
  - [Float32](#F-EdjCase-ICP-Candid-CandidTypeCode-Float32 'EdjCase.ICP.Candid.CandidTypeCode.Float32')
  - [Float64](#F-EdjCase-ICP-Candid-CandidTypeCode-Float64 'EdjCase.ICP.Candid.CandidTypeCode.Float64')
  - [Func](#F-EdjCase-ICP-Candid-CandidTypeCode-Func 'EdjCase.ICP.Candid.CandidTypeCode.Func')
  - [Int](#F-EdjCase-ICP-Candid-CandidTypeCode-Int 'EdjCase.ICP.Candid.CandidTypeCode.Int')
  - [Int16](#F-EdjCase-ICP-Candid-CandidTypeCode-Int16 'EdjCase.ICP.Candid.CandidTypeCode.Int16')
  - [Int32](#F-EdjCase-ICP-Candid-CandidTypeCode-Int32 'EdjCase.ICP.Candid.CandidTypeCode.Int32')
  - [Int64](#F-EdjCase-ICP-Candid-CandidTypeCode-Int64 'EdjCase.ICP.Candid.CandidTypeCode.Int64')
  - [Int8](#F-EdjCase-ICP-Candid-CandidTypeCode-Int8 'EdjCase.ICP.Candid.CandidTypeCode.Int8')
  - [Nat](#F-EdjCase-ICP-Candid-CandidTypeCode-Nat 'EdjCase.ICP.Candid.CandidTypeCode.Nat')
  - [Nat16](#F-EdjCase-ICP-Candid-CandidTypeCode-Nat16 'EdjCase.ICP.Candid.CandidTypeCode.Nat16')
  - [Nat32](#F-EdjCase-ICP-Candid-CandidTypeCode-Nat32 'EdjCase.ICP.Candid.CandidTypeCode.Nat32')
  - [Nat64](#F-EdjCase-ICP-Candid-CandidTypeCode-Nat64 'EdjCase.ICP.Candid.CandidTypeCode.Nat64')
  - [Nat8](#F-EdjCase-ICP-Candid-CandidTypeCode-Nat8 'EdjCase.ICP.Candid.CandidTypeCode.Nat8')
  - [Null](#F-EdjCase-ICP-Candid-CandidTypeCode-Null 'EdjCase.ICP.Candid.CandidTypeCode.Null')
  - [Opt](#F-EdjCase-ICP-Candid-CandidTypeCode-Opt 'EdjCase.ICP.Candid.CandidTypeCode.Opt')
  - [Principal](#F-EdjCase-ICP-Candid-CandidTypeCode-Principal 'EdjCase.ICP.Candid.CandidTypeCode.Principal')
  - [Record](#F-EdjCase-ICP-Candid-CandidTypeCode-Record 'EdjCase.ICP.Candid.CandidTypeCode.Record')
  - [Reserved](#F-EdjCase-ICP-Candid-CandidTypeCode-Reserved 'EdjCase.ICP.Candid.CandidTypeCode.Reserved')
  - [Service](#F-EdjCase-ICP-Candid-CandidTypeCode-Service 'EdjCase.ICP.Candid.CandidTypeCode.Service')
  - [Text](#F-EdjCase-ICP-Candid-CandidTypeCode-Text 'EdjCase.ICP.Candid.CandidTypeCode.Text')
  - [Variant](#F-EdjCase-ICP-Candid-CandidTypeCode-Variant 'EdjCase.ICP.Candid.CandidTypeCode.Variant')
  - [Vector](#F-EdjCase-ICP-Candid-CandidTypeCode-Vector 'EdjCase.ICP.Candid.CandidTypeCode.Vector')
- [CandidTypedValue](#T-EdjCase-ICP-Candid-Models-CandidTypedValue 'EdjCase.ICP.Candid.Models.CandidTypedValue')
  - [#ctor(value,type)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-#ctor-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-Models-Types-CandidType- 'EdjCase.ICP.Candid.Models.CandidTypedValue.#ctor(EdjCase.ICP.Candid.Models.Values.CandidValue,EdjCase.ICP.Candid.Models.Types.CandidType)')
  - [Type](#P-EdjCase-ICP-Candid-Models-CandidTypedValue-Type 'EdjCase.ICP.Candid.Models.CandidTypedValue.Type')
  - [Value](#P-EdjCase-ICP-Candid-Models-CandidTypedValue-Value 'EdjCase.ICP.Candid.Models.CandidTypedValue.Value')
  - [AsBool()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsBool 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsBool')
  - [AsFloat32()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsFloat32 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsFloat32')
  - [AsFloat64()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsFloat64 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsFloat64')
  - [AsFunc()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsFunc 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsFunc')
  - [AsInt()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsInt 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsInt')
  - [AsInt16()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsInt16 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsInt16')
  - [AsInt32()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsInt32 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsInt32')
  - [AsInt64()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsInt64 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsInt64')
  - [AsInt8()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsInt8 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsInt8')
  - [AsNat()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsNat 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsNat')
  - [AsNat16()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsNat16 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsNat16')
  - [AsNat32()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsNat32 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsNat32')
  - [AsNat64()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsNat64 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsNat64')
  - [AsNat8()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsNat8 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsNat8')
  - [AsOptional()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsOptional 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsOptional')
  - [AsOptional\`\`1()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsOptional``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidValue,``0}- 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsOptional``1(System.Func{EdjCase.ICP.Candid.Models.Values.CandidValue,``0})')
  - [AsPrimitive()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsPrimitive 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsPrimitive')
  - [AsPrincipal()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsPrincipal 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsPrincipal')
  - [AsRecord()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsRecord 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsRecord')
  - [AsRecord\`\`1()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsRecord``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidRecord,``0}- 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsRecord``1(System.Func{EdjCase.ICP.Candid.Models.Values.CandidRecord,``0})')
  - [AsService()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsService 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsService')
  - [AsText()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsText 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsText')
  - [AsVariant()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsVariant 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsVariant')
  - [AsVariant\`\`1()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsVariant``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidVariant,``0}- 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsVariant``1(System.Func{EdjCase.ICP.Candid.Models.Values.CandidVariant,``0})')
  - [AsVector()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsVector 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsVector')
  - [AsVectorAsArray\`\`1()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsVectorAsArray``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidValue,``0}- 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsVectorAsArray``1(System.Func{EdjCase.ICP.Candid.Models.Values.CandidValue,``0})')
  - [AsVectorAsList\`\`1()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsVectorAsList``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidValue,``0}- 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsVectorAsList``1(System.Func{EdjCase.ICP.Candid.Models.Values.CandidValue,``0})')
  - [Bool(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Bool-System-Boolean- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Bool(System.Boolean)')
  - [Empty()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Empty 'EdjCase.ICP.Candid.Models.CandidTypedValue.Empty')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Equals-EdjCase-ICP-Candid-Models-CandidTypedValue- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Equals(EdjCase.ICP.Candid.Models.CandidTypedValue)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Equals-System-Object- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Equals(System.Object)')
  - [Float32(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Float32-System-Single- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Float32(System.Single)')
  - [Float64(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Float64-System-Double- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Float64(System.Double)')
  - [FromObject\`\`1(value,converter)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-FromObject``1-``0,EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Models.CandidTypedValue.FromObject``1(``0,EdjCase.ICP.Candid.CandidConverter)')
  - [FromValueAndType(value,type)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-FromValueAndType-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-Models-Types-CandidType- 'EdjCase.ICP.Candid.Models.CandidTypedValue.FromValueAndType(EdjCase.ICP.Candid.Models.Values.CandidValue,EdjCase.ICP.Candid.Models.Types.CandidType)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-GetHashCode 'EdjCase.ICP.Candid.Models.CandidTypedValue.GetHashCode')
  - [Int(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Int-EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Int(EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [Int16(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Int16-System-Int16- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Int16(System.Int16)')
  - [Int32(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Int32-System-Int32- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Int32(System.Int32)')
  - [Int64(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Int64-System-Int64- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Int64(System.Int64)')
  - [Int8(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Int8-System-SByte- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Int8(System.SByte)')
  - [IsNull()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-IsNull 'EdjCase.ICP.Candid.Models.CandidTypedValue.IsNull')
  - [Nat(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Nat-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Nat(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [Nat16(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Nat16-System-UInt16- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Nat16(System.UInt16)')
  - [Nat32(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Nat32-System-UInt32- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Nat32(System.UInt32)')
  - [Nat64(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Nat64-System-UInt64- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Nat64(System.UInt64)')
  - [Nat8(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Nat8-System-Byte- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Nat8(System.Byte)')
  - [Null()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Null 'EdjCase.ICP.Candid.Models.CandidTypedValue.Null')
  - [Opt(typedValue)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Opt-EdjCase-ICP-Candid-Models-CandidTypedValue- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Opt(EdjCase.ICP.Candid.Models.CandidTypedValue)')
  - [Principal(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Principal-EdjCase-ICP-Candid-Models-Principal- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Principal(EdjCase.ICP.Candid.Models.Principal)')
  - [Reserved()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Reserved 'EdjCase.ICP.Candid.Models.CandidTypedValue.Reserved')
  - [Text(value)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Text-System-String- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Text(System.String)')
  - [ToObject\`\`1(converter)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-ToObject``1-EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Models.CandidTypedValue.ToObject``1(EdjCase.ICP.Candid.CandidConverter)')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-ToString 'EdjCase.ICP.Candid.Models.CandidTypedValue.ToString')
  - [Vector(innerType,values)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Vector-EdjCase-ICP-Candid-Models-Types-CandidType,EdjCase-ICP-Candid-Models-Values-CandidValue[]- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Vector(EdjCase.ICP.Candid.Models.Types.CandidType,EdjCase.ICP.Candid.Models.Values.CandidValue[])')
  - [Vector\`\`1(innerType,values,valueConverter)](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-Vector``1-EdjCase-ICP-Candid-Models-Types-CandidType,System-Collections-Generic-IEnumerable{``0},System-Func{``0,EdjCase-ICP-Candid-Models-Values-CandidValue}- 'EdjCase.ICP.Candid.Models.CandidTypedValue.Vector``1(EdjCase.ICP.Candid.Models.Types.CandidType,System.Collections.Generic.IEnumerable{``0},System.Func{``0,EdjCase.ICP.Candid.Models.Values.CandidValue})')
  - [op_Equality()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-op_Equality-EdjCase-ICP-Candid-Models-CandidTypedValue,EdjCase-ICP-Candid-Models-CandidTypedValue- 'EdjCase.ICP.Candid.Models.CandidTypedValue.op_Equality(EdjCase.ICP.Candid.Models.CandidTypedValue,EdjCase.ICP.Candid.Models.CandidTypedValue)')
  - [op_Inequality()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-op_Inequality-EdjCase-ICP-Candid-Models-CandidTypedValue,EdjCase-ICP-Candid-Models-CandidTypedValue- 'EdjCase.ICP.Candid.Models.CandidTypedValue.op_Inequality(EdjCase.ICP.Candid.Models.CandidTypedValue,EdjCase.ICP.Candid.Models.CandidTypedValue)')
- [CandidValue](#T-EdjCase-ICP-Candid-Models-Values-CandidValue 'EdjCase.ICP.Candid.Models.Values.CandidValue')
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidValue-Type 'EdjCase.ICP.Candid.Models.Values.CandidValue.Type')
  - [AsBool()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsBool 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsBool')
  - [AsFloat32()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsFloat32 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsFloat32')
  - [AsFloat64()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsFloat64 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsFloat64')
  - [AsFunc()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsFunc 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsFunc')
  - [AsInt()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsInt 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsInt')
  - [AsInt16()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsInt16 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsInt16')
  - [AsInt32()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsInt32 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsInt32')
  - [AsInt64()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsInt64 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsInt64')
  - [AsInt8()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsInt8 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsInt8')
  - [AsNat()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsNat 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsNat')
  - [AsNat16()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsNat16 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsNat16')
  - [AsNat32()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsNat32 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsNat32')
  - [AsNat64()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsNat64 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsNat64')
  - [AsNat8()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsNat8 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsNat8')
  - [AsOptional()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsOptional 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsOptional')
  - [AsOptional\`\`1()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsOptional``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidValue,``0}- 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsOptional``1(System.Func{EdjCase.ICP.Candid.Models.Values.CandidValue,``0})')
  - [AsPrimitive()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsPrimitive 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsPrimitive')
  - [AsPrincipal()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsPrincipal 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsPrincipal')
  - [AsRecord()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsRecord 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsRecord')
  - [AsRecord\`\`1(converter)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsRecord``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidRecord,``0}- 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsRecord``1(System.Func{EdjCase.ICP.Candid.Models.Values.CandidRecord,``0})')
  - [AsService()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsService 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsService')
  - [AsText()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsText 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsText')
  - [AsVariant()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsVariant 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsVariant')
  - [AsVariant\`\`1(converter)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsVariant``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidVariant,``0}- 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsVariant``1(System.Func{EdjCase.ICP.Candid.Models.Values.CandidVariant,``0})')
  - [AsVector()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsVector 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsVector')
  - [AsVectorAsArray\`\`1(converter)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsVectorAsArray``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidValue,``0}- 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsVectorAsArray``1(System.Func{EdjCase.ICP.Candid.Models.Values.CandidValue,``0})')
  - [AsVectorAsList\`\`1(converter)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsVectorAsList``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidValue,``0}- 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsVectorAsList``1(System.Func{EdjCase.ICP.Candid.Models.Values.CandidValue,``0})')
  - [Bool(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Bool-System-Boolean- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Bool(System.Boolean)')
  - [Empty()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Empty 'EdjCase.ICP.Candid.Models.Values.CandidValue.Empty')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Equals(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Equals-System-Object- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Equals(System.Object)')
  - [Float32(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Float32-System-Single- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Float32(System.Single)')
  - [Float64(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Float64-System-Double- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Float64(System.Double)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-GetHashCode 'EdjCase.ICP.Candid.Models.Values.CandidValue.GetHashCode')
  - [Int(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Int-EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Int(EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [Int16(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Int16-System-Int16- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Int16(System.Int16)')
  - [Int32(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Int32-System-Int32- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Int32(System.Int32)')
  - [Int64(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Int64-System-Int64- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Int64(System.Int64)')
  - [Int8(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Int8-System-SByte- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Int8(System.SByte)')
  - [IsNull()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-IsNull 'EdjCase.ICP.Candid.Models.Values.CandidValue.IsNull')
  - [Nat(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Nat-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Nat(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [Nat16(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Nat16-System-UInt16- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Nat16(System.UInt16)')
  - [Nat32(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Nat32-System-UInt32- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Nat32(System.UInt32)')
  - [Nat64(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Nat64-System-UInt64- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Nat64(System.UInt64)')
  - [Nat8(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Nat8-System-Byte- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Nat8(System.Byte)')
  - [Null()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Null 'EdjCase.ICP.Candid.Models.Values.CandidValue.Null')
  - [Principal(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Principal-EdjCase-ICP-Candid-Models-Principal- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Principal(EdjCase.ICP.Candid.Models.Principal)')
  - [Reserved()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Reserved 'EdjCase.ICP.Candid.Models.Values.CandidValue.Reserved')
  - [Text(value)](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Text-System-String- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Text(System.String)')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-ToString 'EdjCase.ICP.Candid.Models.Values.CandidValue.ToString')
  - [op_Equality()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-op_Equality-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidValue.op_Equality(EdjCase.ICP.Candid.Models.Values.CandidValue,EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [op_Inequality()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-op_Inequality-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidValue.op_Inequality(EdjCase.ICP.Candid.Models.Values.CandidValue,EdjCase.ICP.Candid.Models.Values.CandidValue)')
- [CandidValueMapper\`1](#T-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1 'EdjCase.ICP.Candid.Mapping.CandidValueMapper`1')
  - [#ctor(candidType)](#M-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-#ctor-EdjCase-ICP-Candid-Models-Types-CandidType- 'EdjCase.ICP.Candid.Mapping.CandidValueMapper`1.#ctor(EdjCase.ICP.Candid.Models.Types.CandidType)')
  - [CandidType](#P-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-CandidType 'EdjCase.ICP.Candid.Mapping.CandidValueMapper`1.CandidType')
  - [GetMappedCandidType()](#M-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-GetMappedCandidType-System-Type- 'EdjCase.ICP.Candid.Mapping.CandidValueMapper`1.GetMappedCandidType(System.Type)')
  - [Map()](#M-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-Map-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Mapping.CandidValueMapper`1.Map(EdjCase.ICP.Candid.Models.Values.CandidValue,EdjCase.ICP.Candid.CandidConverter)')
  - [Map()](#M-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-Map-System-Object,EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Mapping.CandidValueMapper`1.Map(System.Object,EdjCase.ICP.Candid.CandidConverter)')
  - [MapGeneric(value,converter)](#M-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-MapGeneric-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Mapping.CandidValueMapper`1.MapGeneric(EdjCase.ICP.Candid.Models.Values.CandidValue,EdjCase.ICP.Candid.CandidConverter)')
  - [MapGeneric(value,converter)](#M-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-MapGeneric-`0,EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Mapping.CandidValueMapper`1.MapGeneric(`0,EdjCase.ICP.Candid.CandidConverter)')
- [CandidValueType](#T-EdjCase-ICP-Candid-Models-Values-CandidValueType 'EdjCase.ICP.Candid.Models.Values.CandidValueType')
  - [Func](#F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Func 'EdjCase.ICP.Candid.Models.Values.CandidValueType.Func')
  - [Optional](#F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Optional 'EdjCase.ICP.Candid.Models.Values.CandidValueType.Optional')
  - [Primitive](#F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Primitive 'EdjCase.ICP.Candid.Models.Values.CandidValueType.Primitive')
  - [Principal](#F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Principal 'EdjCase.ICP.Candid.Models.Values.CandidValueType.Principal')
  - [Record](#F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Record 'EdjCase.ICP.Candid.Models.Values.CandidValueType.Record')
  - [Service](#F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Service 'EdjCase.ICP.Candid.Models.Values.CandidValueType.Service')
  - [Variant](#F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Variant 'EdjCase.ICP.Candid.Models.Values.CandidValueType.Variant')
  - [Vector](#F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Vector 'EdjCase.ICP.Candid.Models.Values.CandidValueType.Vector')
- [CandidVariant](#T-EdjCase-ICP-Candid-Models-Values-CandidVariant 'EdjCase.ICP.Candid.Models.Values.CandidVariant')
  - [#ctor(tag,value)](#M-EdjCase-ICP-Candid-Models-Values-CandidVariant-#ctor-EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidVariant.#ctor(EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [Tag](#P-EdjCase-ICP-Candid-Models-Values-CandidVariant-Tag 'EdjCase.ICP.Candid.Models.Values.CandidVariant.Tag')
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidVariant-Type 'EdjCase.ICP.Candid.Models.Values.CandidVariant.Type')
  - [Value](#P-EdjCase-ICP-Candid-Models-Values-CandidVariant-Value 'EdjCase.ICP.Candid.Models.Values.CandidVariant.Value')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidVariant-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}- 'EdjCase.ICP.Candid.Models.Values.CandidVariant.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType},System.Buffers.IBufferWriter{System.Byte})')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidVariant-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidVariant.Equals(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Values-CandidVariant-GetHashCode 'EdjCase.ICP.Candid.Models.Values.CandidVariant.GetHashCode')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Values-CandidVariant-ToString 'EdjCase.ICP.Candid.Models.Values.CandidVariant.ToString')
- [CandidVariantType](#T-EdjCase-ICP-Candid-Models-Types-CandidVariantType 'EdjCase.ICP.Candid.Models.Types.CandidVariantType')
  - [#ctor(options,recursiveId)](#M-EdjCase-ICP-Candid-Models-Types-CandidVariantType-#ctor-System-Collections-Generic-Dictionary{EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-Types-CandidType},EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.Types.CandidVariantType.#ctor(System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Types.CandidType},EdjCase.ICP.Candid.Models.CandidId)')
  - [Options](#P-EdjCase-ICP-Candid-Models-Types-CandidVariantType-Options 'EdjCase.ICP.Candid.Models.Types.CandidVariantType.Options')
  - [Type](#P-EdjCase-ICP-Candid-Models-Types-CandidVariantType-Type 'EdjCase.ICP.Candid.Models.Types.CandidVariantType.Type')
  - [TypeString](#P-EdjCase-ICP-Candid-Models-Types-CandidVariantType-TypeString 'EdjCase.ICP.Candid.Models.Types.CandidVariantType.TypeString')
  - [GetFieldsOrOptions()](#M-EdjCase-ICP-Candid-Models-Types-CandidVariantType-GetFieldsOrOptions 'EdjCase.ICP.Candid.Models.Types.CandidVariantType.GetFieldsOrOptions')
- [CandidVector](#T-EdjCase-ICP-Candid-Models-Values-CandidVector 'EdjCase.ICP.Candid.Models.Values.CandidVector')
  - [#ctor(values)](#M-EdjCase-ICP-Candid-Models-Values-CandidVector-#ctor-EdjCase-ICP-Candid-Models-Values-CandidValue[]- 'EdjCase.ICP.Candid.Models.Values.CandidVector.#ctor(EdjCase.ICP.Candid.Models.Values.CandidValue[])')
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidVector-Type 'EdjCase.ICP.Candid.Models.Values.CandidVector.Type')
  - [Values](#P-EdjCase-ICP-Candid-Models-Values-CandidVector-Values 'EdjCase.ICP.Candid.Models.Values.CandidVector.Values')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidVector-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}- 'EdjCase.ICP.Candid.Models.Values.CandidVector.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType},System.Buffers.IBufferWriter{System.Byte})')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidVector-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidVector.Equals(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Values-CandidVector-GetHashCode 'EdjCase.ICP.Candid.Models.Values.CandidVector.GetHashCode')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Values-CandidVector-ToString 'EdjCase.ICP.Candid.Models.Values.CandidVector.ToString')
- [CandidVectorType](#T-EdjCase-ICP-Candid-Models-Types-CandidVectorType 'EdjCase.ICP.Candid.Models.Types.CandidVectorType')
  - [#ctor(innerType,recursiveId)](#M-EdjCase-ICP-Candid-Models-Types-CandidVectorType-#ctor-EdjCase-ICP-Candid-Models-Types-CandidType,EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.Types.CandidVectorType.#ctor(EdjCase.ICP.Candid.Models.Types.CandidType,EdjCase.ICP.Candid.Models.CandidId)')
  - [InnerType](#P-EdjCase-ICP-Candid-Models-Types-CandidVectorType-InnerType 'EdjCase.ICP.Candid.Models.Types.CandidVectorType.InnerType')
  - [Type](#P-EdjCase-ICP-Candid-Models-Types-CandidVectorType-Type 'EdjCase.ICP.Candid.Models.Types.CandidVectorType.Type')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Types-CandidVectorType-Equals-System-Object- 'EdjCase.ICP.Candid.Models.Types.CandidVectorType.Equals(System.Object)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Types-CandidVectorType-GetHashCode 'EdjCase.ICP.Candid.Models.Types.CandidVectorType.GetHashCode')
- [CompoundTypeTable](#T-EdjCase-ICP-Candid-Models-CompoundTypeTable 'EdjCase.ICP.Candid.Models.CompoundTypeTable')
  - [CompoundTypeIndexMap](#F-EdjCase-ICP-Candid-Models-CompoundTypeTable-CompoundTypeIndexMap 'EdjCase.ICP.Candid.Models.CompoundTypeTable.CompoundTypeIndexMap')
  - [TryAdd(typeDef)](#M-EdjCase-ICP-Candid-Models-CompoundTypeTable-TryAdd-EdjCase-ICP-Candid-Models-Types-CandidCompoundType- 'EdjCase.ICP.Candid.Models.CompoundTypeTable.TryAdd(EdjCase.ICP.Candid.Models.Types.CandidCompoundType)')
- [EmptyValue](#T-EdjCase-ICP-Candid-Models-EmptyValue 'EdjCase.ICP.Candid.Models.EmptyValue')
- [EncodedValue](#T-EdjCase-ICP-Candid-Models-HashTree-EncodedValue 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue')
  - [#ctor(value)](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-#ctor-System-Byte[]- 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.#ctor(System.Byte[])')
  - [Value](#P-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-Value 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.Value')
  - [AsNat()](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-AsNat 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.AsNat')
  - [AsUtf8()](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-AsUtf8 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.AsUtf8')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-Equals-System-Object- 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.Equals(System.Object)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-Equals-EdjCase-ICP-Candid-Models-HashTree-EncodedValue- 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.Equals(EdjCase.ICP.Candid.Models.HashTree.EncodedValue)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-Equals-System-Byte[]- 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.Equals(System.Byte[])')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-GetHashCode 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.GetHashCode')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-ToString 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.ToString')
  - [Utf8Value(value)](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-Utf8Value-System-String- 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.Utf8Value(System.String)')
  - [op_Equality()](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-op_Equality-EdjCase-ICP-Candid-Models-HashTree-EncodedValue,EdjCase-ICP-Candid-Models-HashTree-EncodedValue- 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.op_Equality(EdjCase.ICP.Candid.Models.HashTree.EncodedValue,EdjCase.ICP.Candid.Models.HashTree.EncodedValue)')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-op_Implicit-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-~System-Byte[] 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.op_Implicit(EdjCase.ICP.Candid.Models.HashTree.EncodedValue)~System.Byte[]')
  - [op_Implicit(bytes)](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-op_Implicit-System-Byte[]-~EdjCase-ICP-Candid-Models-HashTree-EncodedValue 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.op_Implicit(System.Byte[])~EdjCase.ICP.Candid.Models.HashTree.EncodedValue')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-op_Implicit-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-~System-String 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.op_Implicit(EdjCase.ICP.Candid.Models.HashTree.EncodedValue)~System.String')
  - [op_Implicit(utf8Value)](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-op_Implicit-System-String-~EdjCase-ICP-Candid-Models-HashTree-EncodedValue 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.op_Implicit(System.String)~EdjCase.ICP.Candid.Models.HashTree.EncodedValue')
  - [op_Inequality()](#M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-op_Inequality-EdjCase-ICP-Candid-Models-HashTree-EncodedValue,EdjCase-ICP-Candid-Models-HashTree-EncodedValue- 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue.op_Inequality(EdjCase.ICP.Candid.Models.HashTree.EncodedValue,EdjCase.ICP.Candid.Models.HashTree.EncodedValue)')
- [FuncMode](#T-EdjCase-ICP-Candid-Models-Types-FuncMode 'EdjCase.ICP.Candid.Models.Types.FuncMode')
  - [Oneway](#F-EdjCase-ICP-Candid-Models-Types-FuncMode-Oneway 'EdjCase.ICP.Candid.Models.Types.FuncMode.Oneway')
  - [Query](#F-EdjCase-ICP-Candid-Models-Types-FuncMode-Query 'EdjCase.ICP.Candid.Models.Types.FuncMode.Query')
- [HashTree](#T-EdjCase-ICP-Candid-Models-HashTree 'EdjCase.ICP.Candid.Models.HashTree')
  - [Type](#P-EdjCase-ICP-Candid-Models-HashTree-Type 'EdjCase.ICP.Candid.Models.HashTree.Type')
  - [AsFork()](#M-EdjCase-ICP-Candid-Models-HashTree-AsFork 'EdjCase.ICP.Candid.Models.HashTree.AsFork')
  - [AsLabeled()](#M-EdjCase-ICP-Candid-Models-HashTree-AsLabeled 'EdjCase.ICP.Candid.Models.HashTree.AsLabeled')
  - [AsLeaf()](#M-EdjCase-ICP-Candid-Models-HashTree-AsLeaf 'EdjCase.ICP.Candid.Models.HashTree.AsLeaf')
  - [AsPruned()](#M-EdjCase-ICP-Candid-Models-HashTree-AsPruned 'EdjCase.ICP.Candid.Models.HashTree.AsPruned')
  - [BuildRootHash()](#M-EdjCase-ICP-Candid-Models-HashTree-BuildRootHash 'EdjCase.ICP.Candid.Models.HashTree.BuildRootHash')
  - [Empty()](#M-EdjCase-ICP-Candid-Models-HashTree-Empty 'EdjCase.ICP.Candid.Models.HashTree.Empty')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-HashTree-Equals-EdjCase-ICP-Candid-Models-HashTree- 'EdjCase.ICP.Candid.Models.HashTree.Equals(EdjCase.ICP.Candid.Models.HashTree)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-HashTree-Equals-System-Object- 'EdjCase.ICP.Candid.Models.HashTree.Equals(System.Object)')
  - [Fork(left,right)](#M-EdjCase-ICP-Candid-Models-HashTree-Fork-EdjCase-ICP-Candid-Models-HashTree,EdjCase-ICP-Candid-Models-HashTree- 'EdjCase.ICP.Candid.Models.HashTree.Fork(EdjCase.ICP.Candid.Models.HashTree,EdjCase.ICP.Candid.Models.HashTree)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-HashTree-GetHashCode 'EdjCase.ICP.Candid.Models.HashTree.GetHashCode')
  - [GetValueOrDefault(path)](#M-EdjCase-ICP-Candid-Models-HashTree-GetValueOrDefault-EdjCase-ICP-Candid-Models-StatePathSegment- 'EdjCase.ICP.Candid.Models.HashTree.GetValueOrDefault(EdjCase.ICP.Candid.Models.StatePathSegment)')
  - [GetValueOrDefault(path)](#M-EdjCase-ICP-Candid-Models-HashTree-GetValueOrDefault-EdjCase-ICP-Candid-Models-StatePath- 'EdjCase.ICP.Candid.Models.HashTree.GetValueOrDefault(EdjCase.ICP.Candid.Models.StatePath)')
  - [Labeled(label,tree)](#M-EdjCase-ICP-Candid-Models-HashTree-Labeled-EdjCase-ICP-Candid-Models-HashTree-EncodedValue,EdjCase-ICP-Candid-Models-HashTree- 'EdjCase.ICP.Candid.Models.HashTree.Labeled(EdjCase.ICP.Candid.Models.HashTree.EncodedValue,EdjCase.ICP.Candid.Models.HashTree)')
  - [Leaf(value)](#M-EdjCase-ICP-Candid-Models-HashTree-Leaf-EdjCase-ICP-Candid-Models-HashTree-EncodedValue- 'EdjCase.ICP.Candid.Models.HashTree.Leaf(EdjCase.ICP.Candid.Models.HashTree.EncodedValue)')
  - [Pruned(treeHash)](#M-EdjCase-ICP-Candid-Models-HashTree-Pruned-System-Byte[]- 'EdjCase.ICP.Candid.Models.HashTree.Pruned(System.Byte[])')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-HashTree-ToString 'EdjCase.ICP.Candid.Models.HashTree.ToString')
  - [op_Equality()](#M-EdjCase-ICP-Candid-Models-HashTree-op_Equality-EdjCase-ICP-Candid-Models-HashTree,EdjCase-ICP-Candid-Models-HashTree- 'EdjCase.ICP.Candid.Models.HashTree.op_Equality(EdjCase.ICP.Candid.Models.HashTree,EdjCase.ICP.Candid.Models.HashTree)')
  - [op_Inequality()](#M-EdjCase-ICP-Candid-Models-HashTree-op_Inequality-EdjCase-ICP-Candid-Models-HashTree,EdjCase-ICP-Candid-Models-HashTree- 'EdjCase.ICP.Candid.Models.HashTree.op_Inequality(EdjCase.ICP.Candid.Models.HashTree,EdjCase.ICP.Candid.Models.HashTree)')
- [HashTreeType](#T-EdjCase-ICP-Candid-Models-HashTreeType 'EdjCase.ICP.Candid.Models.HashTreeType')
  - [Empty](#F-EdjCase-ICP-Candid-Models-HashTreeType-Empty 'EdjCase.ICP.Candid.Models.HashTreeType.Empty')
  - [Fork](#F-EdjCase-ICP-Candid-Models-HashTreeType-Fork 'EdjCase.ICP.Candid.Models.HashTreeType.Fork')
  - [Labeled](#F-EdjCase-ICP-Candid-Models-HashTreeType-Labeled 'EdjCase.ICP.Candid.Models.HashTreeType.Labeled')
  - [Leaf](#F-EdjCase-ICP-Candid-Models-HashTreeType-Leaf 'EdjCase.ICP.Candid.Models.HashTreeType.Leaf')
  - [Pruned](#F-EdjCase-ICP-Candid-Models-HashTreeType-Pruned 'EdjCase.ICP.Candid.Models.HashTreeType.Pruned')
- [HashableObject](#T-EdjCase-ICP-Candid-Models-HashableObject 'EdjCase.ICP.Candid.Models.HashableObject')
  - [#ctor(properties)](#M-EdjCase-ICP-Candid-Models-HashableObject-#ctor-System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-Candid-Models-IHashable}- 'EdjCase.ICP.Candid.Models.HashableObject.#ctor(System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.IHashable})')
  - [Properties](#P-EdjCase-ICP-Candid-Models-HashableObject-Properties 'EdjCase.ICP.Candid.Models.HashableObject.Properties')
  - [ComputeHash()](#M-EdjCase-ICP-Candid-Models-HashableObject-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction- 'EdjCase.ICP.Candid.Models.HashableObject.ComputeHash(EdjCase.ICP.Candid.Crypto.IHashFunction)')
  - [GetEnumerator()](#M-EdjCase-ICP-Candid-Models-HashableObject-GetEnumerator 'EdjCase.ICP.Candid.Models.HashableObject.GetEnumerator')
  - [System#Collections#IEnumerable#GetEnumerator()](#M-EdjCase-ICP-Candid-Models-HashableObject-System#Collections#IEnumerable#GetEnumerator 'EdjCase.ICP.Candid.Models.HashableObject.System#Collections#IEnumerable#GetEnumerator')
- [ICTimestamp](#T-EdjCase-ICP-Candid-Models-ICTimestamp 'EdjCase.ICP.Candid.Models.ICTimestamp')
  - [#ctor(nanoSeconds)](#M-EdjCase-ICP-Candid-Models-ICTimestamp-#ctor-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.ICTimestamp.#ctor(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [NanoSeconds](#P-EdjCase-ICP-Candid-Models-ICTimestamp-NanoSeconds 'EdjCase.ICP.Candid.Models.ICTimestamp.NanoSeconds')
  - [ComputeHash()](#M-EdjCase-ICP-Candid-Models-ICTimestamp-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction- 'EdjCase.ICP.Candid.Models.ICTimestamp.ComputeHash(EdjCase.ICP.Candid.Crypto.IHashFunction)')
  - [From(timespan)](#M-EdjCase-ICP-Candid-Models-ICTimestamp-From-System-TimeSpan- 'EdjCase.ICP.Candid.Models.ICTimestamp.From(System.TimeSpan)')
  - [FromNanoSeconds(nanosecondsSinceEpoch)](#M-EdjCase-ICP-Candid-Models-ICTimestamp-FromNanoSeconds-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.ICTimestamp.FromNanoSeconds(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [Future(timeFromNow)](#M-EdjCase-ICP-Candid-Models-ICTimestamp-Future-System-TimeSpan- 'EdjCase.ICP.Candid.Models.ICTimestamp.Future(System.TimeSpan)')
  - [FutureInNanoseconds(nanosecondsFromNow)](#M-EdjCase-ICP-Candid-Models-ICTimestamp-FutureInNanoseconds-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.ICTimestamp.FutureInNanoseconds(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [Now()](#M-EdjCase-ICP-Candid-Models-ICTimestamp-Now 'EdjCase.ICP.Candid.Models.ICTimestamp.Now')
  - [ToCandid()](#M-EdjCase-ICP-Candid-Models-ICTimestamp-ToCandid 'EdjCase.ICP.Candid.Models.ICTimestamp.ToCandid')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-ICTimestamp-ToString 'EdjCase.ICP.Candid.Models.ICTimestamp.ToString')
  - [op_GreaterThanOrEqual()](#M-EdjCase-ICP-Candid-Models-ICTimestamp-op_GreaterThanOrEqual-EdjCase-ICP-Candid-Models-ICTimestamp,EdjCase-ICP-Candid-Models-ICTimestamp- 'EdjCase.ICP.Candid.Models.ICTimestamp.op_GreaterThanOrEqual(EdjCase.ICP.Candid.Models.ICTimestamp,EdjCase.ICP.Candid.Models.ICTimestamp)')
  - [op_LessThanOrEqual()](#M-EdjCase-ICP-Candid-Models-ICTimestamp-op_LessThanOrEqual-EdjCase-ICP-Candid-Models-ICTimestamp,EdjCase-ICP-Candid-Models-ICTimestamp- 'EdjCase.ICP.Candid.Models.ICTimestamp.op_LessThanOrEqual(EdjCase.ICP.Candid.Models.ICTimestamp,EdjCase.ICP.Candid.Models.ICTimestamp)')
- [ICandidValueMapper](#T-EdjCase-ICP-Candid-Mapping-ICandidValueMapper 'EdjCase.ICP.Candid.Mapping.ICandidValueMapper')
  - [GetMappedCandidType(type)](#M-EdjCase-ICP-Candid-Mapping-ICandidValueMapper-GetMappedCandidType-System-Type- 'EdjCase.ICP.Candid.Mapping.ICandidValueMapper.GetMappedCandidType(System.Type)')
  - [Map(value,converter)](#M-EdjCase-ICP-Candid-Mapping-ICandidValueMapper-Map-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Mapping.ICandidValueMapper.Map(EdjCase.ICP.Candid.Models.Values.CandidValue,EdjCase.ICP.Candid.CandidConverter)')
  - [Map(value,converter)](#M-EdjCase-ICP-Candid-Mapping-ICandidValueMapper-Map-System-Object,EdjCase-ICP-Candid-CandidConverter- 'EdjCase.ICP.Candid.Mapping.ICandidValueMapper.Map(System.Object,EdjCase.ICP.Candid.CandidConverter)')
- [IHashFunction](#T-EdjCase-ICP-Candid-Crypto-IHashFunction 'EdjCase.ICP.Candid.Crypto.IHashFunction')
  - [ComputeHash(value)](#M-EdjCase-ICP-Candid-Crypto-IHashFunction-ComputeHash-System-Byte[]- 'EdjCase.ICP.Candid.Crypto.IHashFunction.ComputeHash(System.Byte[])')
- [IHashable](#T-EdjCase-ICP-Candid-Models-IHashable 'EdjCase.ICP.Candid.Models.IHashable')
  - [ComputeHash(hashFunction)](#M-EdjCase-ICP-Candid-Models-IHashable-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction- 'EdjCase.ICP.Candid.Models.IHashable.ComputeHash(EdjCase.ICP.Candid.Crypto.IHashFunction)')
- [IRepresentationIndependentHashItem](#T-EdjCase-ICP-Candid-Models-IRepresentationIndependentHashItem 'EdjCase.ICP.Candid.Models.IRepresentationIndependentHashItem')
  - [BuildHashableItem()](#M-EdjCase-ICP-Candid-Models-IRepresentationIndependentHashItem-BuildHashableItem 'EdjCase.ICP.Candid.Models.IRepresentationIndependentHashItem.BuildHashableItem')
- [InvalidCandidException](#T-EdjCase-ICP-Candid-Exceptions-InvalidCandidException 'EdjCase.ICP.Candid.Exceptions.InvalidCandidException')
  - [Message](#P-EdjCase-ICP-Candid-Exceptions-InvalidCandidException-Message 'EdjCase.ICP.Candid.Exceptions.InvalidCandidException.Message')
- [IsExternalInit](#T-System-Runtime-CompilerServices-IsExternalInit 'System.Runtime.CompilerServices.IsExternalInit')
- [LEB128](#T-EdjCase-ICP-Candid-Encodings-LEB128 'EdjCase.ICP.Candid.Encodings.LEB128')
  - [DecodeSigned(stream)](#M-EdjCase-ICP-Candid-Encodings-LEB128-DecodeSigned-System-IO-Stream- 'EdjCase.ICP.Candid.Encodings.LEB128.DecodeSigned(System.IO.Stream)')
  - [DecodeUnsigned(encodedValue)](#M-EdjCase-ICP-Candid-Encodings-LEB128-DecodeUnsigned-System-ReadOnlySpan{System-Byte}- 'EdjCase.ICP.Candid.Encodings.LEB128.DecodeUnsigned(System.ReadOnlySpan{System.Byte})')
  - [DecodeUnsigned(stream)](#M-EdjCase-ICP-Candid-Encodings-LEB128-DecodeUnsigned-System-IO-Stream- 'EdjCase.ICP.Candid.Encodings.LEB128.DecodeUnsigned(System.IO.Stream)')
  - [EncodeSigned(value)](#M-EdjCase-ICP-Candid-Encodings-LEB128-EncodeSigned-EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Encodings.LEB128.EncodeSigned(EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [EncodeSigned(value,destination)](#M-EdjCase-ICP-Candid-Encodings-LEB128-EncodeSigned-EdjCase-ICP-Candid-Models-UnboundedInt,System-Buffers-IBufferWriter{System-Byte}- 'EdjCase.ICP.Candid.Encodings.LEB128.EncodeSigned(EdjCase.ICP.Candid.Models.UnboundedInt,System.Buffers.IBufferWriter{System.Byte})')
  - [EncodeUnsigned(value)](#M-EdjCase-ICP-Candid-Encodings-LEB128-EncodeUnsigned-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Encodings.LEB128.EncodeUnsigned(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [EncodeUnsigned(value,destination)](#M-EdjCase-ICP-Candid-Encodings-LEB128-EncodeUnsigned-EdjCase-ICP-Candid-Models-UnboundedUInt,System-Buffers-IBufferWriter{System-Byte}- 'EdjCase.ICP.Candid.Encodings.LEB128.EncodeUnsigned(EdjCase.ICP.Candid.Models.UnboundedUInt,System.Buffers.IBufferWriter{System.Byte})')
- [NullValue](#T-EdjCase-ICP-Candid-Models-NullValue 'EdjCase.ICP.Candid.Models.NullValue')
- [OptionalValue\`1](#T-EdjCase-ICP-Candid-Models-OptionalValue`1 'EdjCase.ICP.Candid.Models.OptionalValue`1')
  - [#ctor()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-#ctor 'EdjCase.ICP.Candid.Models.OptionalValue`1.#ctor')
  - [#ctor(value)](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-#ctor-`0- 'EdjCase.ICP.Candid.Models.OptionalValue`1.#ctor(`0)')
  - [HasValue](#P-EdjCase-ICP-Candid-Models-OptionalValue`1-HasValue 'EdjCase.ICP.Candid.Models.OptionalValue`1.HasValue')
  - [ValueOrDefault](#P-EdjCase-ICP-Candid-Models-OptionalValue`1-ValueOrDefault 'EdjCase.ICP.Candid.Models.OptionalValue`1.ValueOrDefault')
  - [Cast\`\`1()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-Cast``1 'EdjCase.ICP.Candid.Models.OptionalValue`1.Cast``1')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-Equals-System-Object- 'EdjCase.ICP.Candid.Models.OptionalValue`1.Equals(System.Object)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-Equals-EdjCase-ICP-Candid-Models-OptionalValue{`0}- 'EdjCase.ICP.Candid.Models.OptionalValue`1.Equals(EdjCase.ICP.Candid.Models.OptionalValue{`0})')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-GetHashCode 'EdjCase.ICP.Candid.Models.OptionalValue`1.GetHashCode')
  - [GetValueOrDefault()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-GetValueOrDefault 'EdjCase.ICP.Candid.Models.OptionalValue`1.GetValueOrDefault')
  - [GetValueOrThrow()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-GetValueOrThrow 'EdjCase.ICP.Candid.Models.OptionalValue`1.GetValueOrThrow')
  - [GetValueType()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-GetValueType 'EdjCase.ICP.Candid.Models.OptionalValue`1.GetValueType')
  - [NoValue()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-NoValue 'EdjCase.ICP.Candid.Models.OptionalValue`1.NoValue')
  - [SetValue(value)](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-SetValue-`0- 'EdjCase.ICP.Candid.Models.OptionalValue`1.SetValue(`0)')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-ToString 'EdjCase.ICP.Candid.Models.OptionalValue`1.ToString')
  - [TryGetValue(value)](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-TryGetValue-`0@- 'EdjCase.ICP.Candid.Models.OptionalValue`1.TryGetValue(`0@)')
  - [UnsetValue()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-UnsetValue 'EdjCase.ICP.Candid.Models.OptionalValue`1.UnsetValue')
  - [WithValue()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-WithValue-`0- 'EdjCase.ICP.Candid.Models.OptionalValue`1.WithValue(`0)')
  - [op_Equality()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-op_Equality-EdjCase-ICP-Candid-Models-OptionalValue{`0},EdjCase-ICP-Candid-Models-OptionalValue{`0}- 'EdjCase.ICP.Candid.Models.OptionalValue`1.op_Equality(EdjCase.ICP.Candid.Models.OptionalValue{`0},EdjCase.ICP.Candid.Models.OptionalValue{`0})')
  - [op_Inequality()](#M-EdjCase-ICP-Candid-Models-OptionalValue`1-op_Inequality-EdjCase-ICP-Candid-Models-OptionalValue{`0},EdjCase-ICP-Candid-Models-OptionalValue{`0}- 'EdjCase.ICP.Candid.Models.OptionalValue`1.op_Inequality(EdjCase.ICP.Candid.Models.OptionalValue{`0},EdjCase.ICP.Candid.Models.OptionalValue{`0})')
- [PrimitiveType](#T-EdjCase-ICP-Candid-Models-Values-PrimitiveType 'EdjCase.ICP.Candid.Models.Values.PrimitiveType')
  - [Bool](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Bool 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Bool')
  - [Empty](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Empty 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Empty')
  - [Float32](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Float32 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Float32')
  - [Float64](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Float64 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Float64')
  - [Int](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Int 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Int')
  - [Int16](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Int16 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Int16')
  - [Int32](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Int32 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Int32')
  - [Int64](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Int64 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Int64')
  - [Int8](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Int8 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Int8')
  - [Nat](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Nat 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Nat')
  - [Nat16](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Nat16 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Nat16')
  - [Nat32](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Nat32 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Nat32')
  - [Nat64](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Nat64 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Nat64')
  - [Nat8](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Nat8 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Nat8')
  - [Null](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Null 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Null')
  - [Principal](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Principal 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Principal')
  - [Reserved](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Reserved 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Reserved')
  - [Text](#F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Text 'EdjCase.ICP.Candid.Models.Values.PrimitiveType.Text')
- [Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal')
  - [Raw](#P-EdjCase-ICP-Candid-Models-Principal-Raw 'EdjCase.ICP.Candid.Models.Principal.Raw')
  - [Type](#P-EdjCase-ICP-Candid-Models-Principal-Type 'EdjCase.ICP.Candid.Models.Principal.Type')
  - [Anonymous()](#M-EdjCase-ICP-Candid-Models-Principal-Anonymous 'EdjCase.ICP.Candid.Models.Principal.Anonymous')
  - [ComputeHash()](#M-EdjCase-ICP-Candid-Models-Principal-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction- 'EdjCase.ICP.Candid.Models.Principal.ComputeHash(EdjCase.ICP.Candid.Crypto.IHashFunction)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Principal-Equals-EdjCase-ICP-Candid-Models-Principal- 'EdjCase.ICP.Candid.Models.Principal.Equals(EdjCase.ICP.Candid.Models.Principal)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Principal-Equals-System-Object- 'EdjCase.ICP.Candid.Models.Principal.Equals(System.Object)')
  - [FromBytes(raw)](#M-EdjCase-ICP-Candid-Models-Principal-FromBytes-System-Byte[]- 'EdjCase.ICP.Candid.Models.Principal.FromBytes(System.Byte[])')
  - [FromHex(hex)](#M-EdjCase-ICP-Candid-Models-Principal-FromHex-System-String- 'EdjCase.ICP.Candid.Models.Principal.FromHex(System.String)')
  - [FromText(text)](#M-EdjCase-ICP-Candid-Models-Principal-FromText-System-String- 'EdjCase.ICP.Candid.Models.Principal.FromText(System.String)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Principal-GetHashCode 'EdjCase.ICP.Candid.Models.Principal.GetHashCode')
  - [ManagementCanisterId()](#M-EdjCase-ICP-Candid-Models-Principal-ManagementCanisterId 'EdjCase.ICP.Candid.Models.Principal.ManagementCanisterId')
  - [SelfAuthenticating(derEncodedPublicKey)](#M-EdjCase-ICP-Candid-Models-Principal-SelfAuthenticating-System-Byte[]- 'EdjCase.ICP.Candid.Models.Principal.SelfAuthenticating(System.Byte[])')
  - [ToHex()](#M-EdjCase-ICP-Candid-Models-Principal-ToHex 'EdjCase.ICP.Candid.Models.Principal.ToHex')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Principal-ToString 'EdjCase.ICP.Candid.Models.Principal.ToString')
  - [ToText()](#M-EdjCase-ICP-Candid-Models-Principal-ToText 'EdjCase.ICP.Candid.Models.Principal.ToText')
- [PrincipalType](#T-EdjCase-ICP-Candid-Models-PrincipalType 'EdjCase.ICP.Candid.Models.PrincipalType')
  - [Anonymous](#F-EdjCase-ICP-Candid-Models-PrincipalType-Anonymous 'EdjCase.ICP.Candid.Models.PrincipalType.Anonymous')
  - [Derived](#F-EdjCase-ICP-Candid-Models-PrincipalType-Derived 'EdjCase.ICP.Candid.Models.PrincipalType.Derived')
  - [Opaque](#F-EdjCase-ICP-Candid-Models-PrincipalType-Opaque 'EdjCase.ICP.Candid.Models.PrincipalType.Opaque')
  - [Reserved](#F-EdjCase-ICP-Candid-Models-PrincipalType-Reserved 'EdjCase.ICP.Candid.Models.PrincipalType.Reserved')
  - [SelfAuthenticating](#F-EdjCase-ICP-Candid-Models-PrincipalType-SelfAuthenticating 'EdjCase.ICP.Candid.Models.PrincipalType.SelfAuthenticating')
- [RequestId](#T-EdjCase-ICP-Candid-Models-RequestId 'EdjCase.ICP.Candid.Models.RequestId')
  - [#ctor(rawValue)](#M-EdjCase-ICP-Candid-Models-RequestId-#ctor-System-Byte[]- 'EdjCase.ICP.Candid.Models.RequestId.#ctor(System.Byte[])')
  - [RawValue](#P-EdjCase-ICP-Candid-Models-RequestId-RawValue 'EdjCase.ICP.Candid.Models.RequestId.RawValue')
  - [ComputeHash()](#M-EdjCase-ICP-Candid-Models-RequestId-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction- 'EdjCase.ICP.Candid.Models.RequestId.ComputeHash(EdjCase.ICP.Candid.Crypto.IHashFunction)')
  - [FromObject(properties,hashFunction)](#M-EdjCase-ICP-Candid-Models-RequestId-FromObject-System-Collections-Generic-IDictionary{System-String,EdjCase-ICP-Candid-Models-IHashable},EdjCase-ICP-Candid-Crypto-IHashFunction- 'EdjCase.ICP.Candid.Models.RequestId.FromObject(System.Collections.Generic.IDictionary{System.String,EdjCase.ICP.Candid.Models.IHashable},EdjCase.ICP.Candid.Crypto.IHashFunction)')
- [ReservedValue](#T-EdjCase-ICP-Candid-Models-ReservedValue 'EdjCase.ICP.Candid.Models.ReservedValue')
- [SHA256HashFunction](#T-EdjCase-ICP-Candid-Crypto-SHA256HashFunction 'EdjCase.ICP.Candid.Crypto.SHA256HashFunction')
  - [ComputeHash()](#M-EdjCase-ICP-Candid-Crypto-SHA256HashFunction-ComputeHash-System-Byte[]- 'EdjCase.ICP.Candid.Crypto.SHA256HashFunction.ComputeHash(System.Byte[])')
  - [Create()](#M-EdjCase-ICP-Candid-Crypto-SHA256HashFunction-Create 'EdjCase.ICP.Candid.Crypto.SHA256HashFunction.Create')
- [StatePath](#T-EdjCase-ICP-Candid-Models-StatePath 'EdjCase.ICP.Candid.Models.StatePath')
  - [#ctor(segments)](#M-EdjCase-ICP-Candid-Models-StatePath-#ctor-System-Collections-Generic-List{EdjCase-ICP-Candid-Models-StatePathSegment}- 'EdjCase.ICP.Candid.Models.StatePath.#ctor(System.Collections.Generic.List{EdjCase.ICP.Candid.Models.StatePathSegment})')
  - [Segments](#P-EdjCase-ICP-Candid-Models-StatePath-Segments 'EdjCase.ICP.Candid.Models.StatePath.Segments')
  - [ComputeHash()](#M-EdjCase-ICP-Candid-Models-StatePath-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction- 'EdjCase.ICP.Candid.Models.StatePath.ComputeHash(EdjCase.ICP.Candid.Crypto.IHashFunction)')
  - [FromSegments(segments)](#M-EdjCase-ICP-Candid-Models-StatePath-FromSegments-EdjCase-ICP-Candid-Models-StatePathSegment[]- 'EdjCase.ICP.Candid.Models.StatePath.FromSegments(EdjCase.ICP.Candid.Models.StatePathSegment[])')
- [StatePathSegment](#T-EdjCase-ICP-Candid-Models-StatePathSegment 'EdjCase.ICP.Candid.Models.StatePathSegment')
  - [#ctor(value)](#M-EdjCase-ICP-Candid-Models-StatePathSegment-#ctor-System-Byte[]- 'EdjCase.ICP.Candid.Models.StatePathSegment.#ctor(System.Byte[])')
  - [Value](#P-EdjCase-ICP-Candid-Models-StatePathSegment-Value 'EdjCase.ICP.Candid.Models.StatePathSegment.Value')
  - [ComputeHash()](#M-EdjCase-ICP-Candid-Models-StatePathSegment-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction- 'EdjCase.ICP.Candid.Models.StatePathSegment.ComputeHash(EdjCase.ICP.Candid.Crypto.IHashFunction)')
  - [FromString(segment)](#M-EdjCase-ICP-Candid-Models-StatePathSegment-FromString-System-String- 'EdjCase.ICP.Candid.Models.StatePathSegment.FromString(System.String)')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-StatePathSegment-op_Implicit-EdjCase-ICP-Candid-Models-StatePathSegment-~System-Byte[] 'EdjCase.ICP.Candid.Models.StatePathSegment.op_Implicit(EdjCase.ICP.Candid.Models.StatePathSegment)~System.Byte[]')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-StatePathSegment-op_Implicit-System-Byte[]-~EdjCase-ICP-Candid-Models-StatePathSegment 'EdjCase.ICP.Candid.Models.StatePathSegment.op_Implicit(System.Byte[])~EdjCase.ICP.Candid.Models.StatePathSegment')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-StatePathSegment-op_Implicit-System-String-~EdjCase-ICP-Candid-Models-StatePathSegment 'EdjCase.ICP.Candid.Models.StatePathSegment.op_Implicit(System.String)~EdjCase.ICP.Candid.Models.StatePathSegment')
- [UnboundedInt](#T-EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt')
  - [CompareTo()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-CompareTo-EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.CompareTo(EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-Equals-EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.Equals(EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-Equals-System-Object- 'EdjCase.ICP.Candid.Models.UnboundedInt.Equals(System.Object)')
  - [FromBigInteger(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-FromBigInteger-System-Numerics-BigInteger- 'EdjCase.ICP.Candid.Models.UnboundedInt.FromBigInteger(System.Numerics.BigInteger)')
  - [FromInt64(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-FromInt64-System-Int64- 'EdjCase.ICP.Candid.Models.UnboundedInt.FromInt64(System.Int64)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-GetHashCode 'EdjCase.ICP.Candid.Models.UnboundedInt.GetHashCode')
  - [GetRawBytes(isBigEndian)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-GetRawBytes-System-Boolean- 'EdjCase.ICP.Candid.Models.UnboundedInt.GetRawBytes(System.Boolean)')
  - [ToBigInteger()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-ToBigInteger 'EdjCase.ICP.Candid.Models.UnboundedInt.ToBigInteger')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-ToString 'EdjCase.ICP.Candid.Models.UnboundedInt.ToString')
  - [TryToInt64(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-TryToInt64-System-Int64@- 'EdjCase.ICP.Candid.Models.UnboundedInt.TryToInt64(System.Int64@)')
  - [op_Addition()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Addition-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Addition(EdjCase.ICP.Candid.Models.UnboundedInt,EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [op_Decrement()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Decrement-EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Decrement(EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [op_Division()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Division-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Division(EdjCase.ICP.Candid.Models.UnboundedInt,EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [op_Equality()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Equality-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Equality(EdjCase.ICP.Candid.Models.UnboundedInt,EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-UInt64 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedInt)~System.UInt64')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-UInt32 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedInt)~System.UInt32')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-UInt16 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedInt)~System.UInt16')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-Byte 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedInt)~System.Byte')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-Int64 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedInt)~System.Int64')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-Int32 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedInt)~System.Int32')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-Int16 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedInt)~System.Int16')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-SByte 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedInt)~System.SByte')
  - [op_GreaterThan()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_GreaterThan-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.op_GreaterThan(EdjCase.ICP.Candid.Models.UnboundedInt,EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [op_GreaterThanOrEqual()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_GreaterThanOrEqual-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.op_GreaterThanOrEqual(EdjCase.ICP.Candid.Models.UnboundedInt,EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Implicit(EdjCase.ICP.Candid.Models.UnboundedUInt)~EdjCase.ICP.Candid.Models.UnboundedInt')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-Int64-~EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Implicit(System.Int64)~EdjCase.ICP.Candid.Models.UnboundedInt')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-Int32-~EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Implicit(System.Int32)~EdjCase.ICP.Candid.Models.UnboundedInt')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-Int16-~EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Implicit(System.Int16)~EdjCase.ICP.Candid.Models.UnboundedInt')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-SByte-~EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Implicit(System.SByte)~EdjCase.ICP.Candid.Models.UnboundedInt')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-UInt64-~EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Implicit(System.UInt64)~EdjCase.ICP.Candid.Models.UnboundedInt')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-UInt32-~EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Implicit(System.UInt32)~EdjCase.ICP.Candid.Models.UnboundedInt')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-UInt16-~EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Implicit(System.UInt16)~EdjCase.ICP.Candid.Models.UnboundedInt')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-Byte-~EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Implicit(System.Byte)~EdjCase.ICP.Candid.Models.UnboundedInt')
  - [op_Increment()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Increment-EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Increment(EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [op_Inequality()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Inequality-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Inequality(EdjCase.ICP.Candid.Models.UnboundedInt,EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [op_LessThan()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_LessThan-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.op_LessThan(EdjCase.ICP.Candid.Models.UnboundedInt,EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [op_LessThanOrEqual()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_LessThanOrEqual-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.op_LessThanOrEqual(EdjCase.ICP.Candid.Models.UnboundedInt,EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [op_Multiply()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Multiply-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Multiply(EdjCase.ICP.Candid.Models.UnboundedInt,EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [op_Subtraction()](#M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Subtraction-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Models.UnboundedInt.op_Subtraction(EdjCase.ICP.Candid.Models.UnboundedInt,EdjCase.ICP.Candid.Models.UnboundedInt)')
- [UnboundedIntExtensions](#T-EdjCase-ICP-Candid-Models-UnboundedIntExtensions 'EdjCase.ICP.Candid.Models.UnboundedIntExtensions')
  - [ToUnbounded(value)](#M-EdjCase-ICP-Candid-Models-UnboundedIntExtensions-ToUnbounded-System-Int64- 'EdjCase.ICP.Candid.Models.UnboundedIntExtensions.ToUnbounded(System.Int64)')
- [UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt')
  - [CompareTo()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-CompareTo-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.CompareTo(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-Equals-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.Equals(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-Equals-System-Object- 'EdjCase.ICP.Candid.Models.UnboundedUInt.Equals(System.Object)')
  - [FromBigInteger(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-FromBigInteger-System-Numerics-BigInteger- 'EdjCase.ICP.Candid.Models.UnboundedUInt.FromBigInteger(System.Numerics.BigInteger)')
  - [FromUInt64(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-FromUInt64-System-UInt64- 'EdjCase.ICP.Candid.Models.UnboundedUInt.FromUInt64(System.UInt64)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-GetHashCode 'EdjCase.ICP.Candid.Models.UnboundedUInt.GetHashCode')
  - [GetRawBytes(isBigEndian)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-GetRawBytes-System-Boolean- 'EdjCase.ICP.Candid.Models.UnboundedUInt.GetRawBytes(System.Boolean)')
  - [ToBigInteger()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-ToBigInteger 'EdjCase.ICP.Candid.Models.UnboundedUInt.ToBigInteger')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-ToString 'EdjCase.ICP.Candid.Models.UnboundedUInt.ToString')
  - [TryToUInt64(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-TryToUInt64-System-UInt64@- 'EdjCase.ICP.Candid.Models.UnboundedUInt.TryToUInt64(System.UInt64@)')
  - [op_Addition()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Addition-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Addition(EdjCase.ICP.Candid.Models.UnboundedUInt,EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [op_Decrement()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Decrement-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Decrement(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [op_Division()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Division-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Division(EdjCase.ICP.Candid.Models.UnboundedUInt,EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [op_Equality()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Equality-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Equality(EdjCase.ICP.Candid.Models.UnboundedUInt,EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedInt)~EdjCase.ICP.Candid.Models.UnboundedUInt')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-System-Int64-~EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Explicit(System.Int64)~EdjCase.ICP.Candid.Models.UnboundedUInt')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-System-Int32-~EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Explicit(System.Int32)~EdjCase.ICP.Candid.Models.UnboundedUInt')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-System-Int16-~EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Explicit(System.Int16)~EdjCase.ICP.Candid.Models.UnboundedUInt')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-System-SByte-~EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Explicit(System.SByte)~EdjCase.ICP.Candid.Models.UnboundedUInt')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-UInt64 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedUInt)~System.UInt64')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-UInt32 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedUInt)~System.UInt32')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-UInt16 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedUInt)~System.UInt16')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-Byte 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedUInt)~System.Byte')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-Int64 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedUInt)~System.Int64')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-Int32 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedUInt)~System.Int32')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-Int16 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedUInt)~System.Int16')
  - [op_Explicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-SByte 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Explicit(EdjCase.ICP.Candid.Models.UnboundedUInt)~System.SByte')
  - [op_GreaterThan()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_GreaterThan-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_GreaterThan(EdjCase.ICP.Candid.Models.UnboundedUInt,EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [op_GreaterThanOrEqual()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_GreaterThanOrEqual-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_GreaterThanOrEqual(EdjCase.ICP.Candid.Models.UnboundedUInt,EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Implicit-System-UInt64-~EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Implicit(System.UInt64)~EdjCase.ICP.Candid.Models.UnboundedUInt')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Implicit-System-UInt32-~EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Implicit(System.UInt32)~EdjCase.ICP.Candid.Models.UnboundedUInt')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Implicit-System-UInt16-~EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Implicit(System.UInt16)~EdjCase.ICP.Candid.Models.UnboundedUInt')
  - [op_Implicit(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Implicit-System-Byte-~EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Implicit(System.Byte)~EdjCase.ICP.Candid.Models.UnboundedUInt')
  - [op_Increment()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Increment-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Increment(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [op_Inequality()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Inequality-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Inequality(EdjCase.ICP.Candid.Models.UnboundedUInt,EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [op_LessThan()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_LessThan-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_LessThan(EdjCase.ICP.Candid.Models.UnboundedUInt,EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [op_LessThanOrEqual()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_LessThanOrEqual-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_LessThanOrEqual(EdjCase.ICP.Candid.Models.UnboundedUInt,EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [op_Multiply()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Multiply-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Multiply(EdjCase.ICP.Candid.Models.UnboundedUInt,EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [op_Subtraction()](#M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Subtraction-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Models.UnboundedUInt.op_Subtraction(EdjCase.ICP.Candid.Models.UnboundedUInt,EdjCase.ICP.Candid.Models.UnboundedUInt)')
- [UnboundedUIntExtensions](#T-EdjCase-ICP-Candid-Models-UnboundedUIntExtensions 'EdjCase.ICP.Candid.Models.UnboundedUIntExtensions')
  - [ToUnbounded(value)](#M-EdjCase-ICP-Candid-Models-UnboundedUIntExtensions-ToUnbounded-System-UInt64- 'EdjCase.ICP.Candid.Models.UnboundedUIntExtensions.ToUnbounded(System.UInt64)')
- [VariantAttribute](#T-EdjCase-ICP-Candid-Mapping-VariantAttribute 'EdjCase.ICP.Candid.Mapping.VariantAttribute')
- [VariantOptionAttribute](#T-EdjCase-ICP-Candid-Mapping-VariantOptionAttribute 'EdjCase.ICP.Candid.Mapping.VariantOptionAttribute')
  - [#ctor(tag)](#M-EdjCase-ICP-Candid-Mapping-VariantOptionAttribute-#ctor-EdjCase-ICP-Candid-Models-CandidTag- 'EdjCase.ICP.Candid.Mapping.VariantOptionAttribute.#ctor(EdjCase.ICP.Candid.Models.CandidTag)')
  - [Tag](#P-EdjCase-ICP-Candid-Mapping-VariantOptionAttribute-Tag 'EdjCase.ICP.Candid.Mapping.VariantOptionAttribute.Tag')
- [VariantTagPropertyAttribute](#T-EdjCase-ICP-Candid-Mapping-VariantTagPropertyAttribute 'EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute')
- [VariantValuePropertyAttribute](#T-EdjCase-ICP-Candid-Mapping-VariantValuePropertyAttribute 'EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute')

<a name='T-EdjCase-ICP-Candid-Crypto-CRC32'></a>
## CRC32 `type`

##### Namespace

EdjCase.ICP.Candid.Crypto

##### Summary

Helper class for computing CRC32 hashes/checksums on byte data
Useful for calculating checksums on data

<a name='M-EdjCase-ICP-Candid-Crypto-CRC32-ComputeHash-System-ReadOnlySpan{System-Byte}-'></a>
### ComputeHash(data) `method`

##### Summary

Computes the 32-bit hash on the data bytes provided

##### Returns

Hash of the byte data as a byte array of length of 4

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.ReadOnlySpan{System.Byte}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ReadOnlySpan 'System.ReadOnlySpan{System.Byte}') | Byte data |

<a name='T-EdjCase-ICP-Candid-Models-CandidArg'></a>
## CandidArg `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

A model representing a candid arg. Used as the list of arguments for a function

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-#ctor-System-Collections-Generic-List{EdjCase-ICP-Candid-Models-CandidTypedValue}-'></a>
### #ctor(values) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| values | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.CandidTypedValue}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.CandidTypedValue}') | Order list of typed values for the arg |

<a name='P-EdjCase-ICP-Candid-Models-CandidArg-Values'></a>
### Values `property`

##### Summary

Order list of typed values for the arg

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction-'></a>
### ComputeHash() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-Empty'></a>
### Empty() `method`

##### Summary

Helper method to create a candid arg with no typed values

##### Returns

Candid arg value

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-Encode'></a>
### Encode() `method`

##### Summary

Encodes the candid arg into a byte array which can be used in sending requests to
a canister

##### Returns



##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-Encode-System-Buffers-IBufferWriter{System-Byte}-'></a>
### Encode() `method`

##### Summary

Encodes the candid arg into a byte array which can be used in sending requests to
a canister

##### Returns



##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-Equals-EdjCase-ICP-Candid-Models-CandidArg-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-FromBytes-System-Byte[]-'></a>
### FromBytes(value) `method`

##### Summary

Decodes a byte array into a candid arg value. Must be a valid encoded candid arg value

##### Returns

Candid arg value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Encoded candid arg value |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [EdjCase.ICP.Candid.Exceptions.CandidDecodingException](#T-EdjCase-ICP-Candid-Exceptions-CandidDecodingException 'EdjCase.ICP.Candid.Exceptions.CandidDecodingException') | Throws if the bytes are not valid Candid |
| [EdjCase.ICP.Candid.Exceptions.InvalidCandidException](#T-EdjCase-ICP-Candid-Exceptions-InvalidCandidException 'EdjCase.ICP.Candid.Exceptions.InvalidCandidException') | Throws if the the candid does not follow the specification |

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-FromCandid-System-Collections-Generic-List{EdjCase-ICP-Candid-Models-CandidTypedValue}-'></a>
### FromCandid(values) `method`

##### Summary

Converts an ordered list of typed values to a candid arg value

##### Returns

Candid arg value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| values | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.CandidTypedValue}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.CandidTypedValue}') | Ordered list of typed values |

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-FromCandid-EdjCase-ICP-Candid-Models-CandidTypedValue[]-'></a>
### FromCandid(values) `method`

##### Summary

Converts an ordered array of typed values to a candid arg value

##### Returns

Candid arg value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| values | [EdjCase.ICP.Candid.Models.CandidTypedValue[]](#T-EdjCase-ICP-Candid-Models-CandidTypedValue[] 'EdjCase.ICP.Candid.Models.CandidTypedValue[]') | Ordered array of typed values |

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``1-EdjCase-ICP-Candid-CandidConverter-'></a>
### ToObjects\`\`1(candidConverter) `method`

##### Summary

Takes the first arg value and converts it to the specified type

##### Returns

The converted object of the first arg value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| candidConverter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | Optional. Specifies the converter to use, othewise uses the default |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T1 | The type to convert the first arg value to |

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``2-EdjCase-ICP-Candid-CandidConverter-'></a>
### ToObjects\`\`2(candidConverter) `method`

##### Summary

Takes the arg values 1->2 and converts them to the specified types

##### Returns

The tuple of all specified arg values

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| candidConverter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | Optional. Specifies the converter to use, othewise uses the default |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T1 | The type to convert the first arg value to |
| T2 | The type to convert the second arg value to |

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``3-EdjCase-ICP-Candid-CandidConverter-'></a>
### ToObjects\`\`3(candidConverter) `method`

##### Summary

Takes the arg values 1->3 and converts them to the specified types

##### Returns

The tuple of all specified arg values

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| candidConverter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | Optional. Specifies the converter to use, othewise uses the default |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T1 | The type to convert the first arg value to |
| T2 | The type to convert the second arg value to |
| T3 | The type to convert the third arg value to |

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``4-EdjCase-ICP-Candid-CandidConverter-'></a>
### ToObjects\`\`4(candidConverter) `method`

##### Summary

Takes the arg values 1->4 and converts them to the specified types

##### Returns

The tuple of all specified arg values

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| candidConverter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | Optional. Specifies the converter to use, othewise uses the default |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T1 | The type to convert the first arg value to |
| T2 | The type to convert the second arg value to |
| T3 | The type to convert the third arg value to |
| T4 | The type to convert the fourth arg value to |

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``5-EdjCase-ICP-Candid-CandidConverter-'></a>
### ToObjects\`\`5(candidConverter) `method`

##### Summary

Takes the arg values 1->5 and converts them to the specified types

##### Returns

The tuple of all specified arg values

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| candidConverter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | Optional. Specifies the converter to use, othewise uses the default |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T1 | The type to convert the first arg value to |
| T2 | The type to convert the second arg value to |
| T3 | The type to convert the third arg value to |
| T4 | The type to convert the fourth arg value to |
| T5 | The type to convert the fifth arg value to |

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``6-EdjCase-ICP-Candid-CandidConverter-'></a>
### ToObjects\`\`6(candidConverter) `method`

##### Summary

Takes the arg value 1->6 and converts them to the specified types

##### Returns

The tuple of all specified arg values

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| candidConverter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | Optional. Specifies the converter to use, othewise uses the default |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T1 | The type to convert the first arg value to |
| T2 | The type to convert the second arg value to |
| T3 | The type to convert the third arg value to |
| T4 | The type to convert the fourth arg value to |
| T5 | The type to convert the fifth arg value to |
| T6 | The type to convert the sixth arg value to |

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``7-EdjCase-ICP-Candid-CandidConverter-'></a>
### ToObjects\`\`7(candidConverter) `method`

##### Summary

Takes the arg value 1->7 and converts them to the specified types

##### Returns

The tuple of all specified arg values

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| candidConverter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | Optional. Specifies the converter to use, othewise uses the default |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T1 | The type to convert the first arg value to |
| T2 | The type to convert the second arg value to |
| T3 | The type to convert the third arg value to |
| T4 | The type to convert the fourth arg value to |
| T5 | The type to convert the fifth arg value to |
| T6 | The type to convert the sixth arg value to |
| T7 | The type to convert the seventh arg value to |

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-ToObjects``8-EdjCase-ICP-Candid-CandidConverter-'></a>
### ToObjects\`\`8(candidConverter) `method`

##### Summary

Takes the arg value 1->8 and converts them to the specified types

##### Returns

The tuple of all specified arg values

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| candidConverter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | Optional. Specifies the converter to use, othewise uses the default |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T1 | The type to convert the first arg value to |
| T2 | The type to convert the second arg value to |
| T3 | The type to convert the third arg value to |
| T4 | The type to convert the fourth arg value to |
| T5 | The type to convert the fifth arg value to |
| T6 | The type to convert the sixth arg value to |
| T7 | The type to convert the seventh arg value to |
| T8 | The type to convert the eighth arg value to |

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-op_Equality-EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-CandidArg-'></a>
### op_Equality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-op_Inequality-EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-CandidArg-'></a>
### op_Inequality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Parsers-CandidByteParser'></a>
## CandidByteParser `type`

##### Namespace

EdjCase.ICP.Candid.Parsers

##### Summary

Functions to help parse candid arguments from the raw bytes

<a name='M-EdjCase-ICP-Candid-Parsers-CandidByteParser-Parse-System-Byte[]-'></a>
### Parse(value) `method`

##### Summary

Converts a byte representation of candid arguments to a usable model

##### Returns

Candid arg value from the specified bytes

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The byte representation of Candid arguments |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [EdjCase.ICP.Candid.Exceptions.CandidDecodingException](#T-EdjCase-ICP-Candid-Exceptions-CandidDecodingException 'EdjCase.ICP.Candid.Exceptions.CandidDecodingException') | Throws if the bytes are not valid Candid |
| [EdjCase.ICP.Candid.Exceptions.InvalidCandidException](#T-EdjCase-ICP-Candid-Exceptions-InvalidCandidException 'EdjCase.ICP.Candid.Exceptions.InvalidCandidException') | Throws if the the candid does not follow the specification |

<a name='T-EdjCase-ICP-Candid-Models-Types-CandidCompoundType'></a>
## CandidCompoundType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Types

##### Summary

A candid type that is not primitive or a reference. These types are considered
more complex and have multiple data structures within them

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidCompoundType-#ctor-EdjCase-ICP-Candid-Models-CandidId-'></a>
### #ctor(recursiveId) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| recursiveId | [EdjCase.ICP.Candid.Models.CandidId](#T-EdjCase-ICP-Candid-Models-CandidId 'EdjCase.ICP.Candid.Models.CandidId') | Optional. Used if this type can be referenced by an inner type recursively.
The inner type will use \`CandidReferenceType\` with this id |

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidCompoundType-RecursiveId'></a>
### RecursiveId `property`

##### Summary

Optional. Used if this type can be referenced by an inner type recursively.
The inner type will use \`CandidReferenceType\` with this id

<a name='T-EdjCase-ICP-Candid-CandidConverter'></a>
## CandidConverter `type`

##### Namespace

EdjCase.ICP.Candid

##### Summary

A class that converts to and from C# and Candid types

<a name='M-EdjCase-ICP-Candid-CandidConverter-#ctor-EdjCase-ICP-Candid-CandidConverterOptions-'></a>
### #ctor(options) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [EdjCase.ICP.Candid.CandidConverterOptions](#T-EdjCase-ICP-Candid-CandidConverterOptions 'EdjCase.ICP.Candid.CandidConverterOptions') | Optional. The options for the converter. If not set, will use defaults |

<a name='M-EdjCase-ICP-Candid-CandidConverter-#ctor-System-Action{EdjCase-ICP-Candid-CandidConverterOptions}-'></a>
### #ctor(configureOptions) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configureOptions | [System.Action{EdjCase.ICP.Candid.CandidConverterOptions}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action{EdjCase.ICP.Candid.CandidConverterOptions}') | Configure function for the converter options. Creates default options |

<a name='P-EdjCase-ICP-Candid-CandidConverter-Default'></a>
### Default `property`

##### Summary

A candid converter with the default settings

<a name='M-EdjCase-ICP-Candid-CandidConverter-FromObject-System-Object-'></a>
### FromObject(obj) `method`

##### Summary

Converts a C# object into a candid value

##### Returns

Candid value mapped from the object

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The object to convert |

<a name='M-EdjCase-ICP-Candid-CandidConverter-FromTypedObject``1-``0-'></a>
### FromTypedObject\`\`1(obj) `method`

##### Summary

Converts a C# object into a typed candid value

##### Returns

Candid typed value mapped from the object

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [\`\`0](#T-``0 '``0') | The object to convert |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Type of the object |

<a name='M-EdjCase-ICP-Candid-CandidConverter-ToObject-System-Type,EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### ToObject(objType,value) `method`

##### Summary

Converts a candid value into a C# object

##### Returns

A C# object of the value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The C# type to convert to |
| value | [EdjCase.ICP.Candid.Models.Values.CandidValue](#T-EdjCase-ICP-Candid-Models-Values-CandidValue 'EdjCase.ICP.Candid.Models.Values.CandidValue') | The candid value to convert |

<a name='M-EdjCase-ICP-Candid-CandidConverter-ToObject``1-EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### ToObject\`\`1(value) `method`

##### Summary

Converts a candid value into a C# object

##### Returns

A C# object of the value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.Values.CandidValue](#T-EdjCase-ICP-Candid-Models-Values-CandidValue 'EdjCase.ICP.Candid.Models.Values.CandidValue') | The candid value to convert |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The C# type to convert to |

<a name='M-EdjCase-ICP-Candid-CandidConverter-ToOptionalObject-System-Type,EdjCase-ICP-Candid-Models-Values-CandidOptional-'></a>
### ToOptionalObject(objType,value) `method`

##### Summary

Converts a candid opt value to an OptionalValue

##### Returns

An optional value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| objType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The C# type to convert the inner type to |
| value | [EdjCase.ICP.Candid.Models.Values.CandidOptional](#T-EdjCase-ICP-Candid-Models-Values-CandidOptional 'EdjCase.ICP.Candid.Models.Values.CandidOptional') | The opt value |

<a name='M-EdjCase-ICP-Candid-CandidConverter-ToOptionalObject``1-EdjCase-ICP-Candid-Models-Values-CandidOptional-'></a>
### ToOptionalObject\`\`1(value) `method`

##### Summary

Converts a candid opt value to an OptionalValue

##### Returns

An optional value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.Values.CandidOptional](#T-EdjCase-ICP-Candid-Models-Values-CandidOptional 'EdjCase.ICP.Candid.Models.Values.CandidOptional') | The opt value |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The C# type to convert the inner type to |

<a name='T-EdjCase-ICP-Candid-CandidConverterOptions'></a>
## CandidConverterOptions `type`

##### Namespace

EdjCase.ICP.Candid

##### Summary

Options for configuring how candid is convertered

<a name='P-EdjCase-ICP-Candid-CandidConverterOptions-CustomMappers'></a>
### CustomMappers `property`

##### Summary

List of custom mappers to use instead of the default mappers provided.
Order does matter, FIFO

<a name='M-EdjCase-ICP-Candid-CandidConverterOptions-AddCustomMapper-EdjCase-ICP-Candid-Mapping-ICandidValueMapper-'></a>
### AddCustomMapper(mapper) `method`

##### Summary

Helper method to add a custom mapper

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| mapper | [EdjCase.ICP.Candid.Mapping.ICandidValueMapper](#T-EdjCase-ICP-Candid-Mapping-ICandidValueMapper 'EdjCase.ICP.Candid.Mapping.ICandidValueMapper') | Candid mapper to add |

<a name='M-EdjCase-ICP-Candid-CandidConverterOptions-AddCustomMapper``1'></a>
### AddCustomMapper\`\`1() `method`

##### Summary

Helper method to add a custom mapper by type. Requires the type to 
be \`ICandidValueMapper\` and it has an empty constrcutor

##### Parameters

This method has no parameters.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='T-EdjCase-ICP-Candid-Exceptions-CandidDecodingException'></a>
## CandidDecodingException `type`

##### Namespace

EdjCase.ICP.Candid.Exceptions

##### Summary

An error that occurs during the decoding of bytes to a candid structure

<a name='M-EdjCase-ICP-Candid-Exceptions-CandidDecodingException-#ctor-System-Int32,System-String-'></a>
### #ctor(byteEndIndex,message) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| byteEndIndex | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The index where the byte reader last read |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Message about the error that occurred |

<a name='P-EdjCase-ICP-Candid-Exceptions-CandidDecodingException-ByteEndIndex'></a>
### ByteEndIndex `property`

##### Summary

The index where the byte reader last read. Helps identitfy the source of the 
decoding issue

<a name='P-EdjCase-ICP-Candid-Exceptions-CandidDecodingException-ErrorMessage'></a>
### ErrorMessage `property`

##### Summary

Message about the error that occurred

<a name='P-EdjCase-ICP-Candid-Exceptions-CandidDecodingException-Message'></a>
### Message `property`

##### Summary

*Inherit from parent.*

<a name='T-EdjCase-ICP-Candid-Models-Values-CandidFunc'></a>
## CandidFunc `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

##### Summary

A model to represent the value of a candid func

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidFunc-#ctor-EdjCase-ICP-Candid-Models-Values-CandidService,System-String-'></a>
### #ctor(service,name) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [EdjCase.ICP.Candid.Models.Values.CandidService](#T-EdjCase-ICP-Candid-Models-Values-CandidService 'EdjCase.ICP.Candid.Models.Values.CandidService') | The candid service definition the function lives in |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the function |

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidFunc-IsOpaqueReference'></a>
### IsOpaqueReference `property`

##### Summary

True if the candid func definition is an opaque (non standard/system specific definition),
otherwise false

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidFunc-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidFunc-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}-'></a>
### EncodeValue() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidFunc-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidFunc-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidFunc-OpaqueReference'></a>
### OpaqueReference() `method`

##### Summary

Creates an opaque reference to a function that is defined by the system
vs being defined in candid

##### Returns

A opaque candid func

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidFunc-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Types-CandidFuncType'></a>
## CandidFuncType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Types

##### Summary

A candid type model that defines a func

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidFuncType-#ctor-System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Types-FuncMode},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Types-CandidType},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Types-CandidType},EdjCase-ICP-Candid-Models-CandidId-'></a>
### #ctor(modes,argTypes,returnTypes,recursiveId) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| modes | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Types.FuncMode}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Types.FuncMode}') | A set of different modes the function supports |
| argTypes | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Types.CandidType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Types.CandidType}') | A list of all the argument types the function will need in a request.
The name is optional and is only used for ease of use, the index/position
of the argument is all that really matters |
| returnTypes | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Types.CandidType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Types.CandidType}') | A list of all the return types the function will supply in respose to a request.
The name is optional and is only used for ease of use, the index/position
of the argument is all that really matters |
| recursiveId | [EdjCase.ICP.Candid.Models.CandidId](#T-EdjCase-ICP-Candid-Models-CandidId 'EdjCase.ICP.Candid.Models.CandidId') | Optional. Used if this type can be referenced by an inner type recursively.
The inner type will use \`CandidReferenceType\` with this id |

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidFuncType-#ctor-System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Types-FuncMode},System-Collections-Generic-List{System-ValueTuple{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidType}},System-Collections-Generic-List{System-ValueTuple{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidType}},EdjCase-ICP-Candid-Models-CandidId-'></a>
### #ctor(modes,argTypes,returnTypes,recursiveId) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| modes | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Types.FuncMode}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Types.FuncMode}') | A set of different modes the function supports |
| argTypes | [System.Collections.Generic.List{System.ValueTuple{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidType}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.ValueTuple{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidType}}') | A list of all the argument types the function will need in a request.
The name is optional and is only used for ease of use, the index/position
of the argument is all that really matters |
| returnTypes | [System.Collections.Generic.List{System.ValueTuple{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidType}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.ValueTuple{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidType}}') | A list of all the return types the function will supply in respose to a request.
The name is optional and is only used for ease of use, the index/position
of the argument is all that really matters |
| recursiveId | [EdjCase.ICP.Candid.Models.CandidId](#T-EdjCase-ICP-Candid-Models-CandidId 'EdjCase.ICP.Candid.Models.CandidId') | Optional. Used if this type can be referenced by an inner type recursively.
The inner type will use \`CandidReferenceType\` with this id |

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidFuncType-ArgTypes'></a>
### ArgTypes `property`

##### Summary

A list of all the argument types the function will need in a request.
The name is optional and is only used for ease of use, the index/position
of the argument is all that really matters

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidFuncType-Modes'></a>
### Modes `property`

##### Summary

A set of different modes the function supports

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidFuncType-ReturnTypes'></a>
### ReturnTypes `property`

##### Summary

A list of all the return types the function will supply in respose to a request.
The name is optional and is only used for ease of use, the index/position
of the argument is all that really matters

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidFuncType-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidFuncType-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidFuncType-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-CandidId'></a>
## CandidId `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

A helper model to store and validate a valid candid id value

<a name='P-EdjCase-ICP-Candid-Models-CandidId-Value'></a>
### Value `property`

##### Summary

The string value of the id

<a name='M-EdjCase-ICP-Candid-Models-CandidId-CompareTo-EdjCase-ICP-Candid-Models-CandidId-'></a>
### CompareTo() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidId-Create-System-String-'></a>
### Create(value) `method`

##### Summary

Helper method to create a candid id from a string value. Will validate if the string is a valid id

##### Returns

A candid id value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The string value to use as the id |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentException 'System.ArgumentException') | Throws if the string is not a valid candid id |

<a name='M-EdjCase-ICP-Candid-Models-CandidId-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidId-Equals-EdjCase-ICP-Candid-Models-CandidId-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidId-Equals-System-String-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidId-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidId-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidId-op_Equality-EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-CandidId-'></a>
### op_Equality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidId-op_Inequality-EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-CandidId-'></a>
### op_Inequality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Mapping-CandidIgnoreAttribute'></a>
## CandidIgnoreAttribute `type`

##### Namespace

EdjCase.ICP.Candid.Mapping

##### Summary

An attribute to ignore a property/field of a class during serialization

<a name='T-EdjCase-ICP-Candid-Models-Types-CandidKnownType'></a>
## CandidKnownType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Types

##### Summary

A candid type that is NOT a reference type. This type is known before any resolution

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidKnownType-Type'></a>
### Type `property`

##### Summary

The candid type that this model represents

<a name='T-EdjCase-ICP-Candid-Mapping-CandidNameAttribute'></a>
## CandidNameAttribute `type`

##### Namespace

EdjCase.ICP.Candid.Mapping

##### Summary

An attribute to specify a candid name to use for serialization. If unspecified 
the serializers will use the property names

<a name='M-EdjCase-ICP-Candid-Mapping-CandidNameAttribute-#ctor-System-String-'></a>
### #ctor(name) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name to use for serialization of candid values |

<a name='T-EdjCase-ICP-Candid-Models-Values-CandidOptional'></a>
## CandidOptional `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

##### Summary

A model representing the value of a candid opt

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidOptional-#ctor-EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### #ctor(value) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.Values.CandidValue](#T-EdjCase-ICP-Candid-Models-Values-CandidValue 'EdjCase.ICP.Candid.Models.Values.CandidValue') | The inner value of an opt. If not set, will be a candid null value |

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidOptional-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidOptional-Value'></a>
### Value `property`

##### Summary

The inner value of an opt. If not set, will be a candid null value

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidOptional-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}-'></a>
### EncodeValue() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidOptional-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidOptional-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidOptional-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Types-CandidOptionalType'></a>
## CandidOptionalType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Types

##### Summary

A model for candid optional types

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidOptionalType-#ctor-EdjCase-ICP-Candid-Models-Types-CandidType,EdjCase-ICP-Candid-Models-CandidId-'></a>
### #ctor(value,recursiveId) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.Types.CandidType](#T-EdjCase-ICP-Candid-Models-Types-CandidType 'EdjCase.ICP.Candid.Models.Types.CandidType') | The inner value type of the optional value, if the value is not null |
| recursiveId | [EdjCase.ICP.Candid.Models.CandidId](#T-EdjCase-ICP-Candid-Models-CandidId 'EdjCase.ICP.Candid.Models.CandidId') | Optional. Used if this type can be referenced by an inner type recursively.
The inner type will use \`CandidReferenceType\` with this id |

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidOptionalType-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidOptionalType-Value'></a>
### Value `property`

##### Summary

The inner value type of the optional value, if the value is not null

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidOptionalType-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidOptionalType-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Values-CandidPrimitive'></a>
## CandidPrimitive `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

##### Summary

A model representing a candid primitive type

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-ValueType'></a>
### ValueType `property`

##### Summary

The specific primitive type that is represented

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsBool'></a>
### AsBool() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsFloat32'></a>
### AsFloat32() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsFloat64'></a>
### AsFloat64() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsInt'></a>
### AsInt() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsInt16'></a>
### AsInt16() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsInt32'></a>
### AsInt32() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsInt64'></a>
### AsInt64() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsInt8'></a>
### AsInt8() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsNat'></a>
### AsNat() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsNat16'></a>
### AsNat16() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsNat32'></a>
### AsNat32() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsNat64'></a>
### AsNat64() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsNat8'></a>
### AsNat8() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsPrincipal'></a>
### AsPrincipal() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsText'></a>
### AsText() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}-'></a>
### EncodeValue() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType'></a>
## CandidPrimitiveType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Types

##### Summary

A model representing any of the primitive candid types

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType-#ctor-EdjCase-ICP-Candid-Models-Values-PrimitiveType-'></a>
### #ctor(type) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [EdjCase.ICP.Candid.Models.Values.PrimitiveType](#T-EdjCase-ICP-Candid-Models-Values-PrimitiveType 'EdjCase.ICP.Candid.Models.Values.PrimitiveType') | The primitive type this model represents |

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType-PrimitiveType'></a>
### PrimitiveType `property`

##### Summary

The primitive type this model represents

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType-Type'></a>
### Type `property`

##### Summary

The candid type this model represents

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Values-CandidRecord'></a>
## CandidRecord `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

##### Summary

A model representing a candid record

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidRecord-#ctor-System-Collections-Generic-Dictionary{EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-Values-CandidValue}-'></a>
### #ctor(fields) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fields | [System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Values.CandidValue}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Values.CandidValue}') | The mapping of field name to field value for the record |

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidRecord-Fields'></a>
### Fields `property`

##### Summary

The mapping of field name to field value for the record

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidRecord-Item-System-String-'></a>
### Item `property`

##### Summary

Gets the candid value of the field with the specified name

##### Returns

The field value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the field value to get, case sensitive |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.Collections.Generic.KeyNotFoundException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.KeyNotFoundException 'System.Collections.Generic.KeyNotFoundException') | Throws if field name is not found |

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidRecord-Item-System-UInt32-'></a>
### Item `property`

##### Summary

Gets the candid value of the field with the specified id (name hash)

##### Returns

The field value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | Id (name hash) of the field value to get |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.Collections.Generic.KeyNotFoundException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.KeyNotFoundException 'System.Collections.Generic.KeyNotFoundException') | Throws if field id is not found |

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidRecord-Item-EdjCase-ICP-Candid-Models-CandidTag-'></a>
### Item `property`

##### Summary

Gets the candid value of the field with the specified tag

##### Returns

The field value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tag | [EdjCase.ICP.Candid.Models.CandidTag](#T-EdjCase-ICP-Candid-Models-CandidTag 'EdjCase.ICP.Candid.Models.CandidTag') | Tag of the field value to get |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.Collections.Generic.KeyNotFoundException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.KeyNotFoundException 'System.Collections.Generic.KeyNotFoundException') | Throws if field tag is not found |

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidRecord-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidRecord-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}-'></a>
### EncodeValue() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidRecord-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidRecord-FromDictionary-System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-Candid-Models-Values-CandidValue}-'></a>
### FromDictionary(fields) `method`

##### Summary

Helper method to create a record value from a dictionary of field names to values

##### Returns

A candid record from the fields specified

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fields | [System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.Values.CandidValue}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.Values.CandidValue}') | Dictionary of field names to values for the record |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidRecord-FromDictionary-System-Collections-Generic-Dictionary{System-UInt32,EdjCase-ICP-Candid-Models-Values-CandidValue}-'></a>
### FromDictionary(fields) `method`

##### Summary

Helper method to create a record value from a dictionary of field ids (name hashes) to values

##### Returns

A candid record from the fields specified

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fields | [System.Collections.Generic.Dictionary{System.UInt32,EdjCase.ICP.Candid.Models.Values.CandidValue}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.UInt32,EdjCase.ICP.Candid.Models.Values.CandidValue}') | Dictionary of ids (name hashes) to values for the record |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidRecord-FromDictionary-System-Collections-Generic-Dictionary{EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-Values-CandidValue}-'></a>
### FromDictionary(fields) `method`

##### Summary

Helper method to create a record value from a dictionary of field tags to values

##### Returns

A candid record from the fields specified

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fields | [System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Values.CandidValue}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Values.CandidValue}') | Dictionary of tags to values for the record |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidRecord-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidRecord-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidRecord-TryGetField-System-String,EdjCase-ICP-Candid-Models-Values-CandidValue@-'></a>
### TryGetField(name,value) `method`

##### Summary

Tries to get the field based on the specified name. If the field does not exist, will return false,
otherwise true. The out value will only be set if returns true, otherwise value will be null

##### Returns

True if field exists, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the field value to get, case sensitive |
| value | [EdjCase.ICP.Candid.Models.Values.CandidValue@](#T-EdjCase-ICP-Candid-Models-Values-CandidValue@ 'EdjCase.ICP.Candid.Models.Values.CandidValue@') | Out value that is set only if the method returns true |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidRecord-TryGetField-System-UInt32,EdjCase-ICP-Candid-Models-Values-CandidValue@-'></a>
### TryGetField(id,value) `method`

##### Summary

Tries to get the field based on the specified id (name hash). If the field does not exist, will return false,
otherwise true. The out value will only be set if returns true, otherwise value will be null

##### Returns

True if field exists, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | Id (name hash) of the field value to get |
| value | [EdjCase.ICP.Candid.Models.Values.CandidValue@](#T-EdjCase-ICP-Candid-Models-Values-CandidValue@ 'EdjCase.ICP.Candid.Models.Values.CandidValue@') | Out value that is set only if the method returns true |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidRecord-TryGetField-EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-Values-CandidValue@-'></a>
### TryGetField(tag,value) `method`

##### Summary

Tries to get the field based on the specified tag. If the field does not exist, will return false,
otherwise true. The out value will only be set if returns true, otherwise value will be null

##### Returns

True if field exists, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tag | [EdjCase.ICP.Candid.Models.CandidTag](#T-EdjCase-ICP-Candid-Models-CandidTag 'EdjCase.ICP.Candid.Models.CandidTag') | Tag of the field value to get |
| value | [EdjCase.ICP.Candid.Models.Values.CandidValue@](#T-EdjCase-ICP-Candid-Models-Values-CandidValue@ 'EdjCase.ICP.Candid.Models.Values.CandidValue@') | Out value that is set only if the method returns true |

<a name='T-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType'></a>
## CandidRecordOrVariantType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Types

##### Summary

A shared class for candid records and variants. Both have a mapping of 
keys with associated types

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType-#ctor-EdjCase-ICP-Candid-Models-CandidId-'></a>
### #ctor() `constructor`

##### Summary

*Inherit from parent.*

##### Parameters

This constructor has no parameters.

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType-TypeString'></a>
### TypeString `property`

##### Summary

The string name of the parent type (record or variant)

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType-GetFieldsOrOptions'></a>
### GetFieldsOrOptions() `method`

##### Summary

Gets the record fields or variant options to be used for encoding

##### Returns



##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidRecordOrVariantType-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Types-CandidRecordType'></a>
## CandidRecordType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Types

##### Summary

A model for candid record types

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidRecordType-#ctor-System-Collections-Generic-Dictionary{EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-Types-CandidType},EdjCase-ICP-Candid-Models-CandidId-'></a>
### #ctor(fields,recursiveId) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fields | [System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Types.CandidType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Types.CandidType}') | The collection of field names with the associate type for that field |
| recursiveId | [EdjCase.ICP.Candid.Models.CandidId](#T-EdjCase-ICP-Candid-Models-CandidId 'EdjCase.ICP.Candid.Models.CandidId') | Optional. Used if this type can be referenced by an inner type recursively.
The inner type will use \`CandidReferenceType\` with this id |

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidRecordType-Fields'></a>
### Fields `property`

##### Summary

The collection of field names with the associate type for that field

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidRecordType-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidRecordType-TypeString'></a>
### TypeString `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidRecordType-GetFieldsOrOptions'></a>
### GetFieldsOrOptions() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Types-CandidReferenceType'></a>
## CandidReferenceType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Types

##### Summary

A special candid type model that is a pointer to a different type.
Usually due to recursive types where a parent type has an inner type
that references that same parent type. The parent type must have a \`RecursiveId\`
specified and the \`Id\` of the reference type must match that

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidReferenceType-#ctor-EdjCase-ICP-Candid-Models-CandidId-'></a>
### #ctor(id) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [EdjCase.ICP.Candid.Models.CandidId](#T-EdjCase-ICP-Candid-Models-CandidId 'EdjCase.ICP.Candid.Models.CandidId') | The id to reference in a parent type. The parent type must have the \`RecursiveId\` specified |

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidReferenceType-Id'></a>
### Id `property`

##### Summary

The id to reference in a parent type. The parent type must have the \`RecursiveId\` specified

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidReferenceType-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidReferenceType-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Values-CandidService'></a>
## CandidService `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

##### Summary

A model that represents a candid service value

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidService-#ctor-EdjCase-ICP-Candid-Models-Principal-'></a>
### #ctor(principalId) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| principalId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The id of the canister where the service lives |

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidService-IsOpqaueReference'></a>
### IsOpqaueReference `property`

##### Summary

True if the candid func definition is an opaque (non standard/system specific definition),
otherwise false

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidService-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidService-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}-'></a>
### EncodeValue() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidService-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidService-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidService-GetPrincipal'></a>
### GetPrincipal() `method`

##### Summary

Gets the prinicipal of the candid service. If it is an opaque reference, then an exception will
be thrown

##### Returns

Pricipal of the candid service

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if service is an opaque reference |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidService-OpaqueReference'></a>
### OpaqueReference() `method`

##### Summary

Helper method to create an opaque service reference where the id/location 
of the service is non-standard/system specific

##### Returns



##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidService-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-CandidServiceDescription'></a>
## CandidServiceDescription `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

A model representing a \`*.did\` file with the definition of the candid 
service and the types associated to it

<a name='M-EdjCase-ICP-Candid-Models-CandidServiceDescription-#ctor-EdjCase-ICP-Candid-Models-Types-CandidServiceType,System-Collections-Generic-Dictionary{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidType},EdjCase-ICP-Candid-Models-CandidId-'></a>
### #ctor(service,declaredTypes,serviceReferenceId) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| service | [EdjCase.ICP.Candid.Models.Types.CandidServiceType](#T-EdjCase-ICP-Candid-Models-Types-CandidServiceType 'EdjCase.ICP.Candid.Models.Types.CandidServiceType') | The type information of the service |
| declaredTypes | [System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidType}') | The types declared outside of the service definition |
| serviceReferenceId | [EdjCase.ICP.Candid.Models.CandidId](#T-EdjCase-ICP-Candid-Models-CandidId 'EdjCase.ICP.Candid.Models.CandidId') | Optional. The id given to the service |

<a name='P-EdjCase-ICP-Candid-Models-CandidServiceDescription-DeclaredTypes'></a>
### DeclaredTypes `property`

##### Summary

The types declared outside of the service definition

<a name='P-EdjCase-ICP-Candid-Models-CandidServiceDescription-Service'></a>
### Service `property`

##### Summary

The type information of the service

<a name='P-EdjCase-ICP-Candid-Models-CandidServiceDescription-ServiceReferenceId'></a>
### ServiceReferenceId `property`

##### Summary

Optional. The id given to the service

<a name='M-EdjCase-ICP-Candid-Models-CandidServiceDescription-Parse-System-String-'></a>
### Parse(text) `method`

##### Summary

Parse the service defintion from a \`*.did\` file contents

##### Returns

The parsed candid service defintion

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The contents of the \`*.did\` file |

<a name='T-EdjCase-ICP-Candid-Models-Types-CandidServiceType'></a>
## CandidServiceType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Types

##### Summary

A model for a candid service type

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidServiceType-#ctor-System-Collections-Generic-Dictionary{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidFuncType},EdjCase-ICP-Candid-Models-CandidId-'></a>
### #ctor(methods,recursiveId) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| methods | [System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidFuncType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidFuncType}') | A mapping of ids to function types that the service contains |
| recursiveId | [EdjCase.ICP.Candid.Models.CandidId](#T-EdjCase-ICP-Candid-Models-CandidId 'EdjCase.ICP.Candid.Models.CandidId') | Optional. Used if this type can be referenced by an inner type recursively.
The inner type will use \`CandidReferenceType\` with this id |

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidServiceType-Methods'></a>
### Methods `property`

##### Summary

A mapping of ids to function types that the service contains

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidServiceType-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidServiceType-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidServiceType-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-CandidTag'></a>
## CandidTag `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

A model representing a candid tag which is a positive number id with an optional name

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-#ctor-System-UInt32-'></a>
### #ctor(id) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | A positive integer value that is either an index, a hash of a string name or arbitrary |

<a name='P-EdjCase-ICP-Candid-Models-CandidTag-Id'></a>
### Id `property`

##### Summary

A positive integer value that is either an index, a hash of a string name or arbitrary

<a name='P-EdjCase-ICP-Candid-Models-CandidTag-Name'></a>
### Name `property`

##### Summary

Optional. The name/label of the tag. If set, the \`Id\` is a hash of the specified name

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-CompareTo-System-Object-'></a>
### CompareTo() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-CompareTo-EdjCase-ICP-Candid-Models-CandidTag-'></a>
### CompareTo() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-Equals-EdjCase-ICP-Candid-Models-CandidTag-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-FromId-System-UInt32-'></a>
### FromId(id) `method`

##### Summary

Helper method to create a tag from an id. No name will be set

##### Returns

A candid tag

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | The id of the tag |

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-FromName-System-String-'></a>
### FromName(name) `method`

##### Summary

Helper method to create a tag from a name. Will calculate the id by hashing the name

##### Returns

A candid tag

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the tag |

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-HashName-System-String-'></a>
### HashName(name) `method`

##### Summary

Hashes the name to get the proper id 
hash(name) = ( Sum_(i=0..k) utf8(name)[i] * 223^(k-i) ) mod 2^32 where k = |utf8(name)|-1

##### Returns

Unsigned 32 byte integer hash

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name to hash |

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-op_Equality-EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-CandidTag-'></a>
### op_Equality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-op_Implicit-System-String-~EdjCase-ICP-Candid-Models-CandidTag'></a>
### op_Implicit(name) `method`

##### Summary

Converts a string value to a candid tag. Will calculate the id based off a hash of the name

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String)~EdjCase.ICP.Candid.Models.CandidTag](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String)~EdjCase.ICP.Candid.Models.CandidTag 'System.String)~EdjCase.ICP.Candid.Models.CandidTag') | A string value of the name |

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-op_Implicit-System-UInt32-~EdjCase-ICP-Candid-Models-CandidTag'></a>
### op_Implicit(id) `method`

##### Summary

Converts a uint value into a candid tag. Will only set the id; name will not be set

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.UInt32)~EdjCase.ICP.Candid.Models.CandidTag](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32)~EdjCase.ICP.Candid.Models.CandidTag 'System.UInt32)~EdjCase.ICP.Candid.Models.CandidTag') |  |

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-op_Implicit-EdjCase-ICP-Candid-Models-CandidTag-~System-UInt32'></a>
### op_Implicit(tag) `method`

##### Summary

Converts a candid tag value to a uint by using the id of the tag

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tag | [EdjCase.ICP.Candid.Models.CandidTag)~System.UInt32](#T-EdjCase-ICP-Candid-Models-CandidTag-~System-UInt32 'EdjCase.ICP.Candid.Models.CandidTag)~System.UInt32') | The candid tag value |

<a name='M-EdjCase-ICP-Candid-Models-CandidTag-op_Inequality-EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-CandidTag-'></a>
### op_Inequality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Mapping-CandidTagAttribute'></a>
## CandidTagAttribute `type`

##### Namespace

EdjCase.ICP.Candid.Mapping

##### Summary

An attribute to specify a candid tag to use for serialization. If unspecified 
the serializers will use the property names

<a name='M-EdjCase-ICP-Candid-Mapping-CandidTagAttribute-#ctor-EdjCase-ICP-Candid-Models-CandidTag-'></a>
### #ctor(tag) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tag | [EdjCase.ICP.Candid.Models.CandidTag](#T-EdjCase-ICP-Candid-Models-CandidTag 'EdjCase.ICP.Candid.Models.CandidTag') | The tag to use for serialization of candid values |

<a name='M-EdjCase-ICP-Candid-Mapping-CandidTagAttribute-#ctor-System-UInt32-'></a>
### #ctor(id) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| id | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | The tag id (name hash) to use for serialization of candid values |

<a name='M-EdjCase-ICP-Candid-Mapping-CandidTagAttribute-#ctor-System-String-'></a>
### #ctor(name) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The tag name to use for serialization of candid values |

<a name='P-EdjCase-ICP-Candid-Mapping-CandidTagAttribute-Tag'></a>
### Tag `property`

##### Summary

The tag to use for serialization of candid values

<a name='T-EdjCase-ICP-Candid-Exceptions-CandidTextParseException'></a>
## CandidTextParseException `type`

##### Namespace

EdjCase.ICP.Candid.Exceptions

##### Summary

An error that occurs when the conversion of text to a candid model fails

<a name='P-EdjCase-ICP-Candid-Exceptions-CandidTextParseException-Message'></a>
### Message `property`

##### Summary

*Inherit from parent.*

<a name='T-EdjCase-ICP-Candid-Models-Types-CandidType'></a>
## CandidType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Types

##### Summary

The base candid type model that all candid types inherit

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Bool'></a>
### Bool() `method`

##### Summary

Helper method to create a Bool candid type

##### Returns

Bool candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Empty'></a>
### Empty() `method`

##### Summary

Helper method to create a Empty candid type

##### Returns

Empty candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Equals-EdjCase-ICP-Candid-Models-Types-CandidType-'></a>
### Equals(other) `method`

##### Summary

Checks for equality of this type against the specified type

##### Returns

True if they are structurally the same, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [EdjCase.ICP.Candid.Models.Types.CandidType](#T-EdjCase-ICP-Candid-Models-Types-CandidType 'EdjCase.ICP.Candid.Models.Types.CandidType') | Another type to compare against |

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Float32'></a>
### Float32() `method`

##### Summary

Helper method to create a Float32 candid type

##### Returns

Float32 candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Float64'></a>
### Float64() `method`

##### Summary

Helper method to create a Float64 candid type

##### Returns

Float64 candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Int'></a>
### Int() `method`

##### Summary

Helper method to create a Int candid type

##### Returns

Int candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Int16'></a>
### Int16() `method`

##### Summary

Helper method to create a Int16 candid type

##### Returns

Int16 candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Int32'></a>
### Int32() `method`

##### Summary

Helper method to create a Int32 candid type

##### Returns

Int32 candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Int64'></a>
### Int64() `method`

##### Summary

Helper method to create a Int64 candid type

##### Returns

Int64 candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Int8'></a>
### Int8() `method`

##### Summary

Helper method to create a Int8 candid type

##### Returns

Int8 candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Nat'></a>
### Nat() `method`

##### Summary

Helper method to create a Nat candid type

##### Returns

Nat candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Nat16'></a>
### Nat16() `method`

##### Summary

Helper method to create a Nat16 candid type

##### Returns

Nat16 candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Nat32'></a>
### Nat32() `method`

##### Summary

Helper method to create a Nat32 candid type

##### Returns

Nat32 candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Nat64'></a>
### Nat64() `method`

##### Summary

Helper method to create a Nat64 candid type

##### Returns

Nat64 candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Nat8'></a>
### Nat8() `method`

##### Summary

Helper method to create a Nat8 candid type

##### Returns

Nat8 candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Null'></a>
### Null() `method`

##### Summary

Helper method to create a Null candid type

##### Returns

Null candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Opt-EdjCase-ICP-Candid-Models-Types-CandidType-'></a>
### Opt() `method`

##### Summary

Helper method to create a Opt candid type

##### Returns

Opt candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Principal'></a>
### Principal() `method`

##### Summary

Helper method to create a Principal candid type

##### Returns

Principal candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Reserved'></a>
### Reserved() `method`

##### Summary

Helper method to create a Reserved candid type

##### Returns

Reserved candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Text'></a>
### Text() `method`

##### Summary

Helper method to create a Text candid type

##### Returns

Text candid type

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-op_Equality-EdjCase-ICP-Candid-Models-Types-CandidType,EdjCase-ICP-Candid-Models-Types-CandidType-'></a>
### op_Equality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-op_Inequality-EdjCase-ICP-Candid-Models-Types-CandidType,EdjCase-ICP-Candid-Models-Types-CandidType-'></a>
### op_Inequality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-CandidTypeCode'></a>
## CandidTypeCode `type`

##### Namespace

EdjCase.ICP.Candid

##### Summary

Specifies all the possible candid types and their corresponding codes

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Bool'></a>
### Bool `constants`

##### Summary

A bool value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Empty'></a>
### Empty `constants`

##### Summary

An empty value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Float32'></a>
### Float32 `constants`

##### Summary

A float32 value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Float64'></a>
### Float64 `constants`

##### Summary

A float64 value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Func'></a>
### Func `constants`

##### Summary

An func value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Int'></a>
### Int `constants`

##### Summary

An unbounded int value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Int16'></a>
### Int16 `constants`

##### Summary

An Int16 value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Int32'></a>
### Int32 `constants`

##### Summary

An Int32 value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Int64'></a>
### Int64 `constants`

##### Summary

An Int64 value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Int8'></a>
### Int8 `constants`

##### Summary

An Int8 value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Nat'></a>
### Nat `constants`

##### Summary

An unbounded uint value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Nat16'></a>
### Nat16 `constants`

##### Summary

An UInt16 value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Nat32'></a>
### Nat32 `constants`

##### Summary

An UInt32 value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Nat64'></a>
### Nat64 `constants`

##### Summary

An UInt64 value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Nat8'></a>
### Nat8 `constants`

##### Summary

An UInt8 value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Null'></a>
### Null `constants`

##### Summary

A null value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Opt'></a>
### Opt `constants`

##### Summary

An optional value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Principal'></a>
### Principal `constants`

##### Summary

An principal value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Record'></a>
### Record `constants`

##### Summary

An record value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Reserved'></a>
### Reserved `constants`

##### Summary

A reserved value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Service'></a>
### Service `constants`

##### Summary

An service value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Text'></a>
### Text `constants`

##### Summary

A string value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Variant'></a>
### Variant `constants`

##### Summary

An variant value

<a name='F-EdjCase-ICP-Candid-CandidTypeCode-Vector'></a>
### Vector `constants`

##### Summary

An vector value

<a name='T-EdjCase-ICP-Candid-Models-CandidTypedValue'></a>
## CandidTypedValue `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

A model representing a candid type and value combination. The type and value must match

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-#ctor-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-Models-Types-CandidType-'></a>
### #ctor(value,type) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.Values.CandidValue](#T-EdjCase-ICP-Candid-Models-Values-CandidValue 'EdjCase.ICP.Candid.Models.Values.CandidValue') | The candid value. Must match the specified type |
| type | [EdjCase.ICP.Candid.Models.Types.CandidType](#T-EdjCase-ICP-Candid-Models-Types-CandidType 'EdjCase.ICP.Candid.Models.Types.CandidType') | The candid type. Must match the specified value |

<a name='P-EdjCase-ICP-Candid-Models-CandidTypedValue-Type'></a>
### Type `property`

##### Summary

The candid type

<a name='P-EdjCase-ICP-Candid-Models-CandidTypedValue-Value'></a>
### Value `property`

##### Summary

The candid value

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsBool'></a>
### AsBool() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsFloat32'></a>
### AsFloat32() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsFloat64'></a>
### AsFloat64() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsFunc'></a>
### AsFunc() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsInt'></a>
### AsInt() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsInt16'></a>
### AsInt16() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsInt32'></a>
### AsInt32() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsInt64'></a>
### AsInt64() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsInt8'></a>
### AsInt8() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsNat'></a>
### AsNat() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsNat16'></a>
### AsNat16() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsNat32'></a>
### AsNat32() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsNat64'></a>
### AsNat64() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsNat8'></a>
### AsNat8() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsOptional'></a>
### AsOptional() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsOptional``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidValue,``0}-'></a>
### AsOptional\`\`1() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsPrimitive'></a>
### AsPrimitive() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsPrincipal'></a>
### AsPrincipal() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsRecord'></a>
### AsRecord() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsRecord``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidRecord,``0}-'></a>
### AsRecord\`\`1() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsService'></a>
### AsService() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsText'></a>
### AsText() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsVariant'></a>
### AsVariant() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsVariant``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidVariant,``0}-'></a>
### AsVariant\`\`1() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsVector'></a>
### AsVector() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsVectorAsArray``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidValue,``0}-'></a>
### AsVectorAsArray\`\`1() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsVectorAsList``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidValue,``0}-'></a>
### AsVectorAsList\`\`1() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Bool-System-Boolean-'></a>
### Bool(value) `method`

##### Summary

A helper method to create a typed bool value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | The value to use for the bool |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Empty'></a>
### Empty() `method`

##### Summary

A helper method to create a typed empty value

##### Returns

A candid typed value of empty

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Equals-EdjCase-ICP-Candid-Models-CandidTypedValue-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Float32-System-Single-'></a>
### Float32(value) `method`

##### Summary

A helper method to create a typed float32 value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | The value to use for the float32 |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Float64-System-Double-'></a>
### Float64(value) `method`

##### Summary

A helper method to create a typed float64 value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The value to use for the float64 |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-FromObject``1-``0,EdjCase-ICP-Candid-CandidConverter-'></a>
### FromObject\`\`1(value,converter) `method`

##### Summary

Converts the object into a typed value. If a converter is not specified, the default
converter will be used

##### Returns

A candid typed value based on the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [\`\`0](#T-``0 '``0') | An object that can be converted into a candid type/value |
| converter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | Optional. Converter to use for the conversion, otherwise will use default converter |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-FromValueAndType-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-Models-Types-CandidType-'></a>
### FromValueAndType(value,type) `method`

##### Summary

Helper method to convert a type and a value to a typed value. Type and value must match

##### Returns

A candid typed value of the specified type and value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.Values.CandidValue](#T-EdjCase-ICP-Candid-Models-Values-CandidValue 'EdjCase.ICP.Candid.Models.Values.CandidValue') | The candid value. Must match the specified type |
| type | [EdjCase.ICP.Candid.Models.Types.CandidType](#T-EdjCase-ICP-Candid-Models-Types-CandidType 'EdjCase.ICP.Candid.Models.Types.CandidType') | The candid type. Must match the specified value |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Int-EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### Int(value) `method`

##### Summary

A helper method to create a typed int value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt](#T-EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt') | The value to use for the int |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Int16-System-Int16-'></a>
### Int16(value) `method`

##### Summary

A helper method to create a typed int16 value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int16](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int16 'System.Int16') | The value to use for the int16 |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Int32-System-Int32-'></a>
### Int32(value) `method`

##### Summary

A helper method to create a typed int32 value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The value to use for the int32 |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Int64-System-Int64-'></a>
### Int64(value) `method`

##### Summary

A helper method to create a typed int64 value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | The value to use for the int64 |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Int8-System-SByte-'></a>
### Int8(value) `method`

##### Summary

A helper method to create a typed int8 value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.SByte](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.SByte 'System.SByte') | The value to use for the int8 |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-IsNull'></a>
### IsNull() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Nat-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### Nat(value) `method`

##### Summary

A helper method to create a typed nat value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | The value to use for the nat |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Nat16-System-UInt16-'></a>
### Nat16(value) `method`

##### Summary

A helper method to create a typed nat16 value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt16](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt16 'System.UInt16') | The value to use for the nat16 |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Nat32-System-UInt32-'></a>
### Nat32(value) `method`

##### Summary

A helper method to create a typed nat32 value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | The value to use for the nat32 |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Nat64-System-UInt64-'></a>
### Nat64(value) `method`

##### Summary

A helper method to create a typed nat64 value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64 'System.UInt64') | The value to use for the nat64 |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Nat8-System-Byte-'></a>
### Nat8(value) `method`

##### Summary

A helper method to create a typed nat8 value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte 'System.Byte') | The value to use for the nat8 |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Null'></a>
### Null() `method`

##### Summary

A helper method to create a typed null value

##### Returns

A candid typed value of null

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Opt-EdjCase-ICP-Candid-Models-CandidTypedValue-'></a>
### Opt(typedValue) `method`

##### Summary

A helper method to create a typed opt value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| typedValue | [EdjCase.ICP.Candid.Models.CandidTypedValue](#T-EdjCase-ICP-Candid-Models-CandidTypedValue 'EdjCase.ICP.Candid.Models.CandidTypedValue') | The inner typed value to wrap an opt around |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Principal-EdjCase-ICP-Candid-Models-Principal-'></a>
### Principal(value) `method`

##### Summary

A helper method to create a typed principal value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The value to use for the principal |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Reserved'></a>
### Reserved() `method`

##### Summary

A helper method to create a typed reserved value

##### Returns

A candid typed value of reserved

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Text-System-String-'></a>
### Text(value) `method`

##### Summary

A helper method to create a typed text value

##### Returns

A candid typed value of the specified value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value to use for the text |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-ToObject``1-EdjCase-ICP-Candid-CandidConverter-'></a>
### ToObject\`\`1(converter) `method`

##### Summary

Helper method to convert a typed value to an generic type value

##### Returns

Value of type T

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| converter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | Optional. Converter to use for the conversion, otherwise will use default converter |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Type to convert the candid value to |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Vector-EdjCase-ICP-Candid-Models-Types-CandidType,EdjCase-ICP-Candid-Models-Values-CandidValue[]-'></a>
### Vector(innerType,values) `method`

##### Summary

A helper method to create a typed vector value

##### Returns

A candid typed value of the array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| innerType | [EdjCase.ICP.Candid.Models.Types.CandidType](#T-EdjCase-ICP-Candid-Models-Types-CandidType 'EdjCase.ICP.Candid.Models.Types.CandidType') | The item type of the vector |
| values | [EdjCase.ICP.Candid.Models.Values.CandidValue[]](#T-EdjCase-ICP-Candid-Models-Values-CandidValue[] 'EdjCase.ICP.Candid.Models.Values.CandidValue[]') | An array of values to use as vector items |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-Vector``1-EdjCase-ICP-Candid-Models-Types-CandidType,System-Collections-Generic-IEnumerable{``0},System-Func{``0,EdjCase-ICP-Candid-Models-Values-CandidValue}-'></a>
### Vector\`\`1(innerType,values,valueConverter) `method`

##### Summary

A helper method to create a typed vector value

##### Returns

A candid typed value of the enumerable

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| innerType | [EdjCase.ICP.Candid.Models.Types.CandidType](#T-EdjCase-ICP-Candid-Models-Types-CandidType 'EdjCase.ICP.Candid.Models.Types.CandidType') | The item type of the vector |
| values | [System.Collections.Generic.IEnumerable{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{``0}') | An enumerable of values to use as vector items |
| valueConverter | [System.Func{\`\`0,EdjCase.ICP.Candid.Models.Values.CandidValue}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0,EdjCase.ICP.Candid.Models.Values.CandidValue}') | A function to convert the enumerable type to a candid value |

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-op_Equality-EdjCase-ICP-Candid-Models-CandidTypedValue,EdjCase-ICP-Candid-Models-CandidTypedValue-'></a>
### op_Equality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-op_Inequality-EdjCase-ICP-Candid-Models-CandidTypedValue,EdjCase-ICP-Candid-Models-CandidTypedValue-'></a>
### op_Inequality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Values-CandidValue'></a>
## CandidValue `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

##### Summary

The base class for all candid value

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidValue-Type'></a>
### Type `property`

##### Summary

The type of candid value is implemented

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsBool'></a>
### AsBool() `method`

##### Summary

Casts the candid value to a bool type. If the type is not a bool, will throw an exception

##### Returns

A bool value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a bool |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsFloat32'></a>
### AsFloat32() `method`

##### Summary

Casts the candid value to a float32 type. If the type is not a float32, will throw an exception

##### Returns

A float32 value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a float32 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsFloat64'></a>
### AsFloat64() `method`

##### Summary

Casts the candid value to a float64 type. If the type is not a float64, will throw an exception

##### Returns

A float64 value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a float64 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsFunc'></a>
### AsFunc() `method`

##### Summary

Casts the candid value to a func type. If the type is not a func, will throw an exception

##### Returns

A func value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a func |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsInt'></a>
### AsInt() `method`

##### Summary

Casts the candid value to an int type. If the type is not an int, will throw an exception

##### Returns

An unbounded int value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not an int |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsInt16'></a>
### AsInt16() `method`

##### Summary

Casts the candid value to an int16 type. If the type is not an int16, will throw an exception

##### Returns

An int16 value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not an int16 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsInt32'></a>
### AsInt32() `method`

##### Summary

Casts the candid value to an int32 type. If the type is not an int32, will throw an exception

##### Returns

An int32 value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not an int32 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsInt64'></a>
### AsInt64() `method`

##### Summary

Casts the candid value to an int64 type. If the type is not an int64, will throw an exception

##### Returns

An int64 value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not an int64 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsInt8'></a>
### AsInt8() `method`

##### Summary

Casts the candid value to an int8 type. If the type is not an int8, will throw an exception

##### Returns

An int8 value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not an int8 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsNat'></a>
### AsNat() `method`

##### Summary

Casts the candid value to a nat type. If the type is not a nat, will throw an exception

##### Returns

An unbounded nat value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a nat |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsNat16'></a>
### AsNat16() `method`

##### Summary

Casts the candid value to a nat16 type. If the type is not a nat16, will throw an exception

##### Returns

A nat16 value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a nat16 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsNat32'></a>
### AsNat32() `method`

##### Summary

Casts the candid value to a nat32 type. If the type is not a nat32, will throw an exception

##### Returns

A nat32 value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a nat32 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsNat64'></a>
### AsNat64() `method`

##### Summary

Casts the candid value to a nat64 type. If the type is not a nat64, will throw an exception

##### Returns

A nat64 value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a nat64 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsNat8'></a>
### AsNat8() `method`

##### Summary

Casts the candid value to a nat8 type. If the type is not a nat8, will throw an exception

##### Returns

A nat8 value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a nat8 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsOptional'></a>
### AsOptional() `method`

##### Summary

Casts the candid value to an optional. If the type is not an optional, will throw an exception

##### Returns

An optional value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not an optional |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsOptional``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidValue,``0}-'></a>
### AsOptional\`\`1() `method`

##### Summary

Casts the candid value to an opt type and maps to a generic type. If the type is not an opt,
will throw an exception

##### Returns

A generic value wrapped in an \`OptionalValue\`

##### Parameters

This method has no parameters.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Type to convert the candid value to |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not an opt |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsPrimitive'></a>
### AsPrimitive() `method`

##### Summary

Casts the candid value to a primitive type. If the type is not a primitive, will throw an exception

##### Returns

A primitive value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a primitive |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsPrincipal'></a>
### AsPrincipal() `method`

##### Summary

Casts the candid value to a principal type. If the type is not a principal, will throw an exception

##### Returns

A principal value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a principal |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsRecord'></a>
### AsRecord() `method`

##### Summary

Casts the candid value to a record type. If the type is not a record, will throw an exception

##### Returns

A record value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a record |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsRecord``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidRecord,``0}-'></a>
### AsRecord\`\`1(converter) `method`

##### Summary

Casts the candid value to a record type and maps to a generic type. If the type is not a record,
will throw an exception

##### Returns

A generic value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| converter | [System.Func{EdjCase.ICP.Candid.Models.Values.CandidRecord,\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{EdjCase.ICP.Candid.Models.Values.CandidRecord,``0}') | The conversion function from candid record to T |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Type to convert the candid value to |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a record |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsService'></a>
### AsService() `method`

##### Summary

Casts the candid value to a service type. If the type is not a service, will throw an exception

##### Returns

A service value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a service |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsText'></a>
### AsText() `method`

##### Summary

Casts the candid value to a text type. If the type is not text, will throw an exception

##### Returns

A text value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not text |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsVariant'></a>
### AsVariant() `method`

##### Summary

Casts the candid value to a variant type. If the type is not a variant, will throw an exception

##### Returns

A variant value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a variant |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsVariant``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidVariant,``0}-'></a>
### AsVariant\`\`1(converter) `method`

##### Summary

Casts the candid value to a variant type and maps to a generic type. If the type is not a variant,
will throw an exception

##### Returns

A generic value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| converter | [System.Func{EdjCase.ICP.Candid.Models.Values.CandidVariant,\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{EdjCase.ICP.Candid.Models.Values.CandidVariant,``0}') | The conversion function from candid variant to T |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Type to convert the candid value to |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a variant |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsVector'></a>
### AsVector() `method`

##### Summary

Casts the candid value to a vector type. If the type is not a vector, will throw an exception

##### Returns

A vector value

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a vector |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsVectorAsArray``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidValue,``0}-'></a>
### AsVectorAsArray\`\`1(converter) `method`

##### Summary

Casts the candid value to a vector type and maps it to an array. If the type is not a vector,
will throw an exception

##### Returns

An array form of the vector

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| converter | [System.Func{EdjCase.ICP.Candid.Models.Values.CandidValue,\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{EdjCase.ICP.Candid.Models.Values.CandidValue,``0}') | The conversion function from candid value to T |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a vector |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsVectorAsList``1-System-Func{EdjCase-ICP-Candid-Models-Values-CandidValue,``0}-'></a>
### AsVectorAsList\`\`1(converter) `method`

##### Summary

Casts the candid value to a vector type and maps it to a List. If the type is not a vector,
will throw an exception

##### Returns

A list form of the vector

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| converter | [System.Func{EdjCase.ICP.Candid.Models.Values.CandidValue,\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{EdjCase.ICP.Candid.Models.Values.CandidValue,``0}') | The conversion function from candid value to T |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Type to convert the candid value to |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if the type is not a vector |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Bool-System-Boolean-'></a>
### Bool(value) `method`

##### Summary

Helper method to create a bool value from a bool

##### Returns

Candid bool value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | An bool value to convert to a candid bool |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Empty'></a>
### Empty() `method`

##### Summary

Helper method to create an empty value

##### Returns

Candid empty value

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Float32-System-Single-'></a>
### Float32(value) `method`

##### Summary

Helper method to create a float32 value from a float

##### Returns

Candid float32 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Single](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Single 'System.Single') | An float value to convert to a candid float32 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Float64-System-Double-'></a>
### Float64(value) `method`

##### Summary

Helper method to create a float64 value from a double

##### Returns

Candid float64 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | An double value to convert to a candid float64 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Int-EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### Int(value) `method`

##### Summary

Helper method to create a int value from an unbounded integer

##### Returns

Candid int value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt](#T-EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt') | A unbounded integer value to convert to a candid int value |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Int16-System-Int16-'></a>
### Int16(value) `method`

##### Summary

Helper method to create a int16 value from a short integer

##### Returns

Candid int16 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int16](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int16 'System.Int16') | A short integer value to convert to a candid int16 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Int32-System-Int32-'></a>
### Int32(value) `method`

##### Summary

Helper method to create a int32 value from an integer

##### Returns

Candid int32 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | An integer value to convert to a candid int32 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Int64-System-Int64-'></a>
### Int64(value) `method`

##### Summary

Helper method to create a int64 value from an long integer

##### Returns

Candid int64 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | An long integer value to convert to a candid int64 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Int8-System-SByte-'></a>
### Int8(value) `method`

##### Summary

Helper method to create a int8 value from a signed byte

##### Returns

Candid int8 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.SByte](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.SByte 'System.SByte') | A signed byte value to convert to a candid int8 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-IsNull'></a>
### IsNull() `method`

##### Summary

Checks if the value is null

##### Returns

Returns true if the value is null, otherwise false

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Nat-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### Nat(value) `method`

##### Summary

Helper method to create a nat value from an unbounded usigned integer

##### Returns

Candid nat value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | A unbounded usigned integer value to convert to a candid nat value |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Nat16-System-UInt16-'></a>
### Nat16(value) `method`

##### Summary

Helper method to create a nat16 value from a unsigned short integer

##### Returns

Candid nat16 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt16](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt16 'System.UInt16') | A unsigned short integer value to convert to a candid nat16 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Nat32-System-UInt32-'></a>
### Nat32(value) `method`

##### Summary

Helper method to create a nat32 value from a unsigned integer

##### Returns

Candid nat32 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32 'System.UInt32') | A unsigned integer value to convert to a candid nat32 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Nat64-System-UInt64-'></a>
### Nat64(value) `method`

##### Summary

Helper method to create a nat64 value from a unsigned long integer

##### Returns

Candid nat64 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64 'System.UInt64') | A unsigned long integer value to convert to a candid nat64 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Nat8-System-Byte-'></a>
### Nat8(value) `method`

##### Summary

Helper method to create a nat8 value from a byte

##### Returns

Candid nat8 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte 'System.Byte') | A byte value to convert to a candid nat8 |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Null'></a>
### Null() `method`

##### Summary

Helper method to create a null value

##### Returns

Candid null value

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Principal-EdjCase-ICP-Candid-Models-Principal-'></a>
### Principal(value) `method`

##### Summary

Helper method to create a principal value from a principal

##### Returns

Candid principal value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | An principal value to convert to a candid principal |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Reserved'></a>
### Reserved() `method`

##### Summary

Helper method to create a reserved value

##### Returns

Candid reserved value

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-Text-System-String-'></a>
### Text(value) `method`

##### Summary

Helper method to create a text value from a string

##### Returns

Candid text value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A string value to convert to a candid text value |

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-op_Equality-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### op_Equality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-op_Inequality-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### op_Inequality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1'></a>
## CandidValueMapper\`1 `type`

##### Namespace

EdjCase.ICP.Candid.Mapping

##### Summary

An abstract mapper to map a C# type to and from a candid type

<a name='M-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-#ctor-EdjCase-ICP-Candid-Models-Types-CandidType-'></a>
### #ctor(candidType) `constructor`

##### Summary

Default constructor, requires a candid type that it maps to

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| candidType | [EdjCase.ICP.Candid.Models.Types.CandidType](#T-EdjCase-ICP-Candid-Models-Types-CandidType 'EdjCase.ICP.Candid.Models.Types.CandidType') | The candid type that the value will map to |

<a name='P-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-CandidType'></a>
### CandidType `property`

##### Summary

The candid type that the value will map to

<a name='M-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-GetMappedCandidType-System-Type-'></a>
### GetMappedCandidType() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-Map-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-CandidConverter-'></a>
### Map() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-Map-System-Object,EdjCase-ICP-Candid-CandidConverter-'></a>
### Map() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-MapGeneric-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-CandidConverter-'></a>
### MapGeneric(value,converter) `method`

##### Summary

Maps a candid value to a C# value.

##### Returns

C# value converted from the candid value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.Values.CandidValue](#T-EdjCase-ICP-Candid-Models-Values-CandidValue 'EdjCase.ICP.Candid.Models.Values.CandidValue') | Candid value to map to a C# value |
| converter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | The converter to use for inner types |

<a name='M-EdjCase-ICP-Candid-Mapping-CandidValueMapper`1-MapGeneric-`0,EdjCase-ICP-Candid-CandidConverter-'></a>
### MapGeneric(value,converter) `method`

##### Summary

Maps a C# value to a candid value and type.

##### Returns

Candid value and type converted from the C# value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [\`0](#T-`0 '`0') | C# value to map to a candid value |
| converter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | The converter to use for inner types |

<a name='T-EdjCase-ICP-Candid-Models-Values-CandidValueType'></a>
## CandidValueType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

##### Summary

The options for candid value types

<a name='F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Func'></a>
### Func `constants`

##### Summary

A function located in a service

<a name='F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Optional'></a>
### Optional `constants`

##### Summary

A value that is either null or a value

<a name='F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Primitive'></a>
### Primitive `constants`

##### Summary

Primitive/simple candid types like nat, int, null, etc...

<a name='F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Principal'></a>
### Principal `constants`

##### Summary

An identifier value used for canister ids and identity ids

<a name='F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Record'></a>
### Record `constants`

##### Summary

A value with a set of fields, each with a name/id and value

<a name='F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Service'></a>
### Service `constants`

##### Summary

A location with a set of functions to call

<a name='F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Variant'></a>
### Variant `constants`

##### Summary

A value with a chosen option name/id and value

<a name='F-EdjCase-ICP-Candid-Models-Values-CandidValueType-Vector'></a>
### Vector `constants`

##### Summary

An array of values

<a name='T-EdjCase-ICP-Candid-Models-Values-CandidVariant'></a>
## CandidVariant `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

##### Summary

A model representing a candid variant value

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidVariant-#ctor-EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### #ctor(tag,value) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tag | [EdjCase.ICP.Candid.Models.CandidTag](#T-EdjCase-ICP-Candid-Models-CandidTag 'EdjCase.ICP.Candid.Models.CandidTag') | The tag (id/name) of the chosen variant option |
| value | [EdjCase.ICP.Candid.Models.Values.CandidValue](#T-EdjCase-ICP-Candid-Models-Values-CandidValue 'EdjCase.ICP.Candid.Models.Values.CandidValue') | The value of the chosen variant option, whose type is based on the option |

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidVariant-Tag'></a>
### Tag `property`

##### Summary

The tag (id/name) of the chosen variant option

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidVariant-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidVariant-Value'></a>
### Value `property`

##### Summary

The value of the chosen variant option, whose type is based on the option

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidVariant-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}-'></a>
### EncodeValue() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidVariant-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidVariant-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidVariant-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Types-CandidVariantType'></a>
## CandidVariantType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Types

##### Summary

A model representing a type definition of a candid variant

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidVariantType-#ctor-System-Collections-Generic-Dictionary{EdjCase-ICP-Candid-Models-CandidTag,EdjCase-ICP-Candid-Models-Types-CandidType},EdjCase-ICP-Candid-Models-CandidId-'></a>
### #ctor(options,recursiveId) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Types.CandidType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidTag,EdjCase.ICP.Candid.Models.Types.CandidType}') | All the potential options and the option types for a variant |
| recursiveId | [EdjCase.ICP.Candid.Models.CandidId](#T-EdjCase-ICP-Candid-Models-CandidId 'EdjCase.ICP.Candid.Models.CandidId') | Optional. Used if this type can be referenced by an inner type recursively.
The inner type will use \`CandidReferenceType\` with this id |

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidVariantType-Options'></a>
### Options `property`

##### Summary

All the potential options and the option types for a variant

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidVariantType-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidVariantType-TypeString'></a>
### TypeString `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidVariantType-GetFieldsOrOptions'></a>
### GetFieldsOrOptions() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Values-CandidVector'></a>
## CandidVector `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

##### Summary

A model representing a candid vector value

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidVector-#ctor-EdjCase-ICP-Candid-Models-Values-CandidValue[]-'></a>
### #ctor(values) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| values | [EdjCase.ICP.Candid.Models.Values.CandidValue[]](#T-EdjCase-ICP-Candid-Models-Values-CandidValue[] 'EdjCase.ICP.Candid.Models.Values.CandidValue[]') | Each candid value that the vector contains. All must be of the same type |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentException 'System.ArgumentException') | Throws if all the values are not of the same type |

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidVector-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidVector-Values'></a>
### Values `property`

##### Summary

Each candid value that the vector contains. All must be of the same type

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidVector-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType},System-Buffers-IBufferWriter{System-Byte}-'></a>
### EncodeValue() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidVector-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidVector-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidVector-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Types-CandidVectorType'></a>
## CandidVectorType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Types

##### Summary

A model representing the type definition of a candid vector

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidVectorType-#ctor-EdjCase-ICP-Candid-Models-Types-CandidType,EdjCase-ICP-Candid-Models-CandidId-'></a>
### #ctor(innerType,recursiveId) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| innerType | [EdjCase.ICP.Candid.Models.Types.CandidType](#T-EdjCase-ICP-Candid-Models-Types-CandidType 'EdjCase.ICP.Candid.Models.Types.CandidType') | The type of the vectors inner values |
| recursiveId | [EdjCase.ICP.Candid.Models.CandidId](#T-EdjCase-ICP-Candid-Models-CandidId 'EdjCase.ICP.Candid.Models.CandidId') | Optional. Used if this type can be referenced by an inner type recursively.
The inner type will use \`CandidReferenceType\` with this id |

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidVectorType-InnerType'></a>
### InnerType `property`

##### Summary

The type of the vectors inner values

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidVectorType-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidVectorType-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidVectorType-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-CompoundTypeTable'></a>
## CompoundTypeTable `type`

##### Namespace

EdjCase.ICP.Candid.Models

<a name='F-EdjCase-ICP-Candid-Models-CompoundTypeTable-CompoundTypeIndexMap'></a>
### CompoundTypeIndexMap `constants`

##### Summary

A mapping of compound type definition to \`EncodedCompoundTypes\` index to be used as reference

<a name='M-EdjCase-ICP-Candid-Models-CompoundTypeTable-TryAdd-EdjCase-ICP-Candid-Models-Types-CandidCompoundType-'></a>
### TryAdd(typeDef) `method`

##### Summary

Adds type to the table unless the type already exists

##### Returns

True if added, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| typeDef | [EdjCase.ICP.Candid.Models.Types.CandidCompoundType](#T-EdjCase-ICP-Candid-Models-Types-CandidCompoundType 'EdjCase.ICP.Candid.Models.Types.CandidCompoundType') | Type to add to table |

<a name='T-EdjCase-ICP-Candid-Models-EmptyValue'></a>
## EmptyValue `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

Dummy struct to represent the \`empty\` candid type's value

<a name='T-EdjCase-ICP-Candid-Models-HashTree-EncodedValue'></a>
## EncodedValue `type`

##### Namespace

EdjCase.ICP.Candid.Models.HashTree

##### Summary

A helper class that wraps around a byte array, giving functions to convert 
to common types like text and numbers

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-#ctor-System-Byte[]-'></a>
### #ctor(value) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The raw value |

<a name='P-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-Value'></a>
### Value `property`

##### Summary

The raw value

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-AsNat'></a>
### AsNat() `method`

##### Summary

The raw value converted to a LEB128 encoded number

##### Returns

A unbounded uint of the value

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-AsUtf8'></a>
### AsUtf8() `method`

##### Summary

The raw value converted to UTF-8 encoded string

##### Returns

A UTF-8 string of the value

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-Equals-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-Equals-System-Byte[]-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-Utf8Value-System-String-'></a>
### Utf8Value(value) `method`

##### Summary

Creates an encoded value from a utf8 string value

##### Returns

UTF8 encoded value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | UTF8 encoded string |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-op_Equality-EdjCase-ICP-Candid-Models-HashTree-EncodedValue,EdjCase-ICP-Candid-Models-HashTree-EncodedValue-'></a>
### op_Equality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-op_Implicit-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-~System-Byte[]'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert an encoded value to a byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.HashTree.EncodedValue)~System.Byte[]](#T-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-~System-Byte[] 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue)~System.Byte[]') | The encoded value to get the raw value from |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-op_Implicit-System-Byte[]-~EdjCase-ICP-Candid-Models-HashTree-EncodedValue'></a>
### op_Implicit(bytes) `method`

##### Summary

A helper method to implicitly convert an byte array to an encoded value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| bytes | [System.Byte[])~EdjCase.ICP.Candid.Models.HashTree.EncodedValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[])~EdjCase.ICP.Candid.Models.HashTree.EncodedValue 'System.Byte[])~EdjCase.ICP.Candid.Models.HashTree.EncodedValue') | The raw value to use with the encoded value |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-op_Implicit-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-~System-String'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert an encoded value to a UTF8 string

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.HashTree.EncodedValue)~System.String](#T-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-~System-String 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue)~System.String') | The encoded value to get the raw value from |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-op_Implicit-System-String-~EdjCase-ICP-Candid-Models-HashTree-EncodedValue'></a>
### op_Implicit(utf8Value) `method`

##### Summary

A helper method to implicitly convert a UTF8 string to an encoded value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| utf8Value | [System.String)~EdjCase.ICP.Candid.Models.HashTree.EncodedValue](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String)~EdjCase.ICP.Candid.Models.HashTree.EncodedValue 'System.String)~EdjCase.ICP.Candid.Models.HashTree.EncodedValue') | The UTF8 string value to use with the encoded value |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-op_Inequality-EdjCase-ICP-Candid-Models-HashTree-EncodedValue,EdjCase-ICP-Candid-Models-HashTree-EncodedValue-'></a>
### op_Inequality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Types-FuncMode'></a>
## FuncMode `type`

##### Namespace

EdjCase.ICP.Candid.Models.Types

##### Summary

All the possible options for function modes which
define special attributes of the function

<a name='F-EdjCase-ICP-Candid-Models-Types-FuncMode-Oneway'></a>
### Oneway `constants`

##### Summary

Mode where the function does not generate a response to a request

<a name='F-EdjCase-ICP-Candid-Models-Types-FuncMode-Query'></a>
### Query `constants`

##### Summary

Mode where the function does not update any state and returns immediately.
Is useful for faster data retrieval

<a name='T-EdjCase-ICP-Candid-Models-HashTree'></a>
## HashTree `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

A variant model representing a hash tree where values can be pruned and labeled in the tree

<a name='P-EdjCase-ICP-Candid-Models-HashTree-Type'></a>
### Type `property`

##### Summary

The type the tree node is

<a name='M-EdjCase-ICP-Candid-Models-HashTree-AsFork'></a>
### AsFork() `method`

##### Summary

Casts the tree to a left and right fork. If the node type is not a fork, then will throw an exception

##### Returns

Left and right trees

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if tree is not a fork |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-AsLabeled'></a>
### AsLabeled() `method`

##### Summary

Casts the tree to a label and a tree. If the node type is not labeled, then will throw an exception

##### Returns

Label of the node and subtree

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if tree is not labeled |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-AsLeaf'></a>
### AsLeaf() `method`

##### Summary

Casts the tree to an encoded value. If the node type is not a leaf, then will throw an exception

##### Returns

Encoded value of the leaf

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if tree is not a leaf |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-AsPruned'></a>
### AsPruned() `method`

##### Summary

Casts the tree to a pruned hash value. If the node type is not pruned, then will throw an exception

##### Returns

Byte array hash value of the pruned subtree

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if tree is not pruned |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-BuildRootHash'></a>
### BuildRootHash() `method`

##### Summary

Computes the root SHA256 hash of the tree based on the IC certificate spec

##### Returns

A byte array of the hash digest

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-Empty'></a>
### Empty() `method`

##### Summary

Helper method to create an empty tree

##### Returns

An empty hash tree

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-Equals-EdjCase-ICP-Candid-Models-HashTree-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-Fork-EdjCase-ICP-Candid-Models-HashTree,EdjCase-ICP-Candid-Models-HashTree-'></a>
### Fork(left,right) `method`

##### Summary

Helper method to create a forked tree

##### Returns

An forked hash tree

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| left | [EdjCase.ICP.Candid.Models.HashTree](#T-EdjCase-ICP-Candid-Models-HashTree 'EdjCase.ICP.Candid.Models.HashTree') | The branch to the left |
| right | [EdjCase.ICP.Candid.Models.HashTree](#T-EdjCase-ICP-Candid-Models-HashTree 'EdjCase.ICP.Candid.Models.HashTree') | The branch to the right |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-GetValueOrDefault-EdjCase-ICP-Candid-Models-StatePathSegment-'></a>
### GetValueOrDefault(path) `method`

##### Summary

Gets the value of the subtree specified by the path, returns null if not found

##### Returns

A hash tree from the path, or null if not found

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [EdjCase.ICP.Candid.Models.StatePathSegment](#T-EdjCase-ICP-Candid-Models-StatePathSegment 'EdjCase.ICP.Candid.Models.StatePathSegment') | The path segment to get a value from |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-GetValueOrDefault-EdjCase-ICP-Candid-Models-StatePath-'></a>
### GetValueOrDefault(path) `method`

##### Summary

Gets the value of the subtree specified by the path, returns null if not found

##### Returns

A hash tree from the path, or null if not found

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [EdjCase.ICP.Candid.Models.StatePath](#T-EdjCase-ICP-Candid-Models-StatePath 'EdjCase.ICP.Candid.Models.StatePath') | The path to get a value from |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-Labeled-EdjCase-ICP-Candid-Models-HashTree-EncodedValue,EdjCase-ICP-Candid-Models-HashTree-'></a>
### Labeled(label,tree) `method`

##### Summary

Helper method to create a labeled tree

##### Returns

An labeled hash tree

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| label | [EdjCase.ICP.Candid.Models.HashTree.EncodedValue](#T-EdjCase-ICP-Candid-Models-HashTree-EncodedValue 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue') | The label for the tree |
| tree | [EdjCase.ICP.Candid.Models.HashTree](#T-EdjCase-ICP-Candid-Models-HashTree 'EdjCase.ICP.Candid.Models.HashTree') | The subtree for the label |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-Leaf-EdjCase-ICP-Candid-Models-HashTree-EncodedValue-'></a>
### Leaf(value) `method`

##### Summary

Helper method to create a leaf tree

##### Returns

An leaf hash tree

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.HashTree.EncodedValue](#T-EdjCase-ICP-Candid-Models-HashTree-EncodedValue 'EdjCase.ICP.Candid.Models.HashTree.EncodedValue') | The value to store in the leaf |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-Pruned-System-Byte[]-'></a>
### Pruned(treeHash) `method`

##### Summary

Helper method to create a pruned tree

##### Returns

An pruned hash tree

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| treeHash | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The hash of the tree that was pruned |

<a name='M-EdjCase-ICP-Candid-Models-HashTree-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-op_Equality-EdjCase-ICP-Candid-Models-HashTree,EdjCase-ICP-Candid-Models-HashTree-'></a>
### op_Equality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashTree-op_Inequality-EdjCase-ICP-Candid-Models-HashTree,EdjCase-ICP-Candid-Models-HashTree-'></a>
### op_Inequality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-HashTreeType'></a>
## HashTreeType `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

Subtypes of what a hash tree can be

<a name='F-EdjCase-ICP-Candid-Models-HashTreeType-Empty'></a>
### Empty `constants`

##### Summary

An empty branch with no data

<a name='F-EdjCase-ICP-Candid-Models-HashTreeType-Fork'></a>
### Fork `constants`

##### Summary

Left and right branching trees

<a name='F-EdjCase-ICP-Candid-Models-HashTreeType-Labeled'></a>
### Labeled `constants`

##### Summary

A branch that is labeled with its own subtree

<a name='F-EdjCase-ICP-Candid-Models-HashTreeType-Leaf'></a>
### Leaf `constants`

##### Summary

A branch with data and no subtree

<a name='F-EdjCase-ICP-Candid-Models-HashTreeType-Pruned'></a>
### Pruned `constants`

##### Summary

A branch that has been trimmed where its data is the hash of the subtree

<a name='T-EdjCase-ICP-Candid-Models-HashableObject'></a>
## HashableObject `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

A helper class to turn a dictionary into a \`IHashable\`

<a name='M-EdjCase-ICP-Candid-Models-HashableObject-#ctor-System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-Candid-Models-IHashable}-'></a>
### #ctor(properties) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| properties | [System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.IHashable}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.IHashable}') | The mapping of property name to hashable value to hash |

<a name='P-EdjCase-ICP-Candid-Models-HashableObject-Properties'></a>
### Properties `property`

##### Summary

The mapping of property name to hashable value to hash

<a name='M-EdjCase-ICP-Candid-Models-HashableObject-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction-'></a>
### ComputeHash() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashableObject-GetEnumerator'></a>
### GetEnumerator() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-HashableObject-System#Collections#IEnumerable#GetEnumerator'></a>
### System#Collections#IEnumerable#GetEnumerator() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-ICTimestamp'></a>
## ICTimestamp `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

Helper class to wrap around an unbounded uint to represent the nanoseconds since 1970-01-01

<a name='M-EdjCase-ICP-Candid-Models-ICTimestamp-#ctor-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### #ctor(nanoSeconds) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nanoSeconds | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | The nanoseconds since 1970-01-01 |

<a name='P-EdjCase-ICP-Candid-Models-ICTimestamp-NanoSeconds'></a>
### NanoSeconds `property`

##### Summary

The nanoseconds since 1970-01-01

<a name='M-EdjCase-ICP-Candid-Models-ICTimestamp-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction-'></a>
### ComputeHash() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-ICTimestamp-From-System-TimeSpan-'></a>
### From(timespan) `method`

##### Summary

Helper method to convert nanoseconds from 1970-01-01 to an ICTimestamp

##### Returns

An ICTimestamp based on the nanoseconds

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| timespan | [System.TimeSpan](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.TimeSpan 'System.TimeSpan') | Time since 1970-01-01 |

<a name='M-EdjCase-ICP-Candid-Models-ICTimestamp-FromNanoSeconds-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### FromNanoSeconds(nanosecondsSinceEpoch) `method`

##### Summary

Helper method to convert nanoseconds from 1970-01-01 to an ICTimestamp

##### Returns

An ICTimestamp based on the nanoseconds

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nanosecondsSinceEpoch | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | Nanoseconds since 1970-01-01 |

<a name='M-EdjCase-ICP-Candid-Models-ICTimestamp-Future-System-TimeSpan-'></a>
### Future(timeFromNow) `method`

##### Summary

Helper method to get a timestamp for X time in the future from NOW instead of 1970-01-01

##### Returns

A timestamp of the time from now

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| timeFromNow | [System.TimeSpan](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.TimeSpan 'System.TimeSpan') | The time from now in the future |

<a name='M-EdjCase-ICP-Candid-Models-ICTimestamp-FutureInNanoseconds-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### FutureInNanoseconds(nanosecondsFromNow) `method`

##### Summary

Helper method to get a timestamp for X nanoseconds in the future from NOW instead of 1970-01-01

##### Returns

A timestamp of the nanoseconds from now

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| nanosecondsFromNow | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | The nanoseconds from now in the future |

<a name='M-EdjCase-ICP-Candid-Models-ICTimestamp-Now'></a>
### Now() `method`

##### Summary

Helper method to get the current timestamp

##### Returns

A timestamp of the current time

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-ICTimestamp-ToCandid'></a>
### ToCandid() `method`

##### Summary

Converts the nanoseconds to a candid Nat value

##### Returns

Candid nat value of the nanoseconds

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-ICTimestamp-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-ICTimestamp-op_GreaterThanOrEqual-EdjCase-ICP-Candid-Models-ICTimestamp,EdjCase-ICP-Candid-Models-ICTimestamp-'></a>
### op_GreaterThanOrEqual() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-ICTimestamp-op_LessThanOrEqual-EdjCase-ICP-Candid-Models-ICTimestamp,EdjCase-ICP-Candid-Models-ICTimestamp-'></a>
### op_LessThanOrEqual() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Mapping-ICandidValueMapper'></a>
## ICandidValueMapper `type`

##### Namespace

EdjCase.ICP.Candid.Mapping

##### Summary

A mapper interface to map a C# type to and from a candid type

<a name='M-EdjCase-ICP-Candid-Mapping-ICandidValueMapper-GetMappedCandidType-System-Type-'></a>
### GetMappedCandidType(type) `method`

##### Summary

Indicates if the mapper can map a certain type

##### Returns

True if it can map, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type to check against |

<a name='M-EdjCase-ICP-Candid-Mapping-ICandidValueMapper-Map-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-CandidConverter-'></a>
### Map(value,converter) `method`

##### Summary

Maps a candid value to a C# value.
Input value will match the \`CandidType\` type property.
Returned value should match the \`Type\` type property.

##### Returns

C# value converted from the candid value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.Values.CandidValue](#T-EdjCase-ICP-Candid-Models-Values-CandidValue 'EdjCase.ICP.Candid.Models.Values.CandidValue') | Candid value to map to a C# value |
| converter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | The converter to use for inner types |

<a name='M-EdjCase-ICP-Candid-Mapping-ICandidValueMapper-Map-System-Object,EdjCase-ICP-Candid-CandidConverter-'></a>
### Map(value,converter) `method`

##### Summary

Maps a C# value to a candid value and type.
Input value will match the \`Type\` type property.
Returned value should match the \`CandidType\` type property.

##### Returns

Candid value and type converted from the C# value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | C# value to map to a candid value |
| converter | [EdjCase.ICP.Candid.CandidConverter](#T-EdjCase-ICP-Candid-CandidConverter 'EdjCase.ICP.Candid.CandidConverter') | The converter to use for inner types |

<a name='T-EdjCase-ICP-Candid-Crypto-IHashFunction'></a>
## IHashFunction `type`

##### Namespace

EdjCase.ICP.Candid.Crypto

##### Summary

Interface to implement different hash function algorithms against

<a name='M-EdjCase-ICP-Candid-Crypto-IHashFunction-ComputeHash-System-Byte[]-'></a>
### ComputeHash(value) `method`

##### Summary

Computes the hash of the byte array based on the algorithm implemented

##### Returns

Hash in the form of a byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Byte array to get the hash of |

<a name='T-EdjCase-ICP-Candid-Models-IHashable'></a>
## IHashable `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

An interface to specify if a class can be hashed by the \`IHashFunction\`

<a name='M-EdjCase-ICP-Candid-Models-IHashable-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction-'></a>
### ComputeHash(hashFunction) `method`

##### Summary

Computes the hash for the object using on the hash function

##### Returns

A byte array of the hash value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hashFunction | [EdjCase.ICP.Candid.Crypto.IHashFunction](#T-EdjCase-ICP-Candid-Crypto-IHashFunction 'EdjCase.ICP.Candid.Crypto.IHashFunction') | A hash function algorithm to use to hash the object |

<a name='T-EdjCase-ICP-Candid-Models-IRepresentationIndependentHashItem'></a>
## IRepresentationIndependentHashItem `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

An interface to specify a representation independent model that can be hashed

<a name='M-EdjCase-ICP-Candid-Models-IRepresentationIndependentHashItem-BuildHashableItem'></a>
### BuildHashableItem() `method`

##### Summary

Builds a mapping of fields to hashable values

##### Returns

Dictionary of field name to hashable field value

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Exceptions-InvalidCandidException'></a>
## InvalidCandidException `type`

##### Namespace

EdjCase.ICP.Candid.Exceptions

##### Summary

An error that occurs if the candid models do not follow the 
specification

<a name='P-EdjCase-ICP-Candid-Exceptions-InvalidCandidException-Message'></a>
### Message `property`

##### Summary

*Inherit from parent.*

<a name='T-System-Runtime-CompilerServices-IsExternalInit'></a>
## IsExternalInit `type`

##### Namespace

System.Runtime.CompilerServices

##### Summary

Reserved to be used by the compiler for tracking metadata.
This class should not be used by developers in source code.

<a name='T-EdjCase-ICP-Candid-Encodings-LEB128'></a>
## LEB128 `type`

##### Namespace

EdjCase.ICP.Candid.Encodings

##### Summary

Utility class to provide methods for LEB128 encoding (https://en.wikipedia.org/wiki/LEB128)

<a name='M-EdjCase-ICP-Candid-Encodings-LEB128-DecodeSigned-System-IO-Stream-'></a>
### DecodeSigned(stream) `method`

##### Summary

Takes a encoded signed LEB128 byte stream and converts it to an \`UnboundedInt\`

##### Returns

\`UnboundedInt\` of LEB128 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stream | [System.IO.Stream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.Stream 'System.IO.Stream') | Byte stream of a signed LEB128 |

<a name='M-EdjCase-ICP-Candid-Encodings-LEB128-DecodeUnsigned-System-ReadOnlySpan{System-Byte}-'></a>
### DecodeUnsigned(encodedValue) `method`

##### Summary

Takes a byte encoded unsigned LEB128 and converts it to an \`UnboundedUInt\`

##### Returns

\`UnboundedUInt\` of LEB128 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| encodedValue | [System.ReadOnlySpan{System.Byte}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ReadOnlySpan 'System.ReadOnlySpan{System.Byte}') | Byte value of an unsigned LEB128 |

<a name='M-EdjCase-ICP-Candid-Encodings-LEB128-DecodeUnsigned-System-IO-Stream-'></a>
### DecodeUnsigned(stream) `method`

##### Summary

Takes a encoded unsigned LEB128 byte stream and converts it to an \`UnboundedUInt\`

##### Returns

\`UnboundedUInt\` of LEB128 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stream | [System.IO.Stream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.Stream 'System.IO.Stream') | Byte stream of an unsigned LEB128 |

<a name='M-EdjCase-ICP-Candid-Encodings-LEB128-EncodeSigned-EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### EncodeSigned(value) `method`

##### Summary

Takes an \`UnboundedInt\` and converts it into an encoded signed LEB128 byte array

##### Returns

LEB128 bytes of value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt](#T-EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt') | Value to convert to LEB128 bytes |

<a name='M-EdjCase-ICP-Candid-Encodings-LEB128-EncodeSigned-EdjCase-ICP-Candid-Models-UnboundedInt,System-Buffers-IBufferWriter{System-Byte}-'></a>
### EncodeSigned(value,destination) `method`

##### Summary

Takes an \`UnboundedInt\` and converts it into an encoded signed LEB128 byte array

##### Returns

LEB128 bytes of value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt](#T-EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt') | Value to convert to LEB128 bytes |
| destination | [System.Buffers.IBufferWriter{System.Byte}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Buffers.IBufferWriter 'System.Buffers.IBufferWriter{System.Byte}') | Buffer writer to write bytes to |

<a name='M-EdjCase-ICP-Candid-Encodings-LEB128-EncodeUnsigned-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### EncodeUnsigned(value) `method`

##### Summary

Takes an \`UnboundedUInt\` and converts it into an encoded unsigned LEB128 byte array

##### Returns

LEB128 bytes of value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | Value to convert to LEB128 bytes |

<a name='M-EdjCase-ICP-Candid-Encodings-LEB128-EncodeUnsigned-EdjCase-ICP-Candid-Models-UnboundedUInt,System-Buffers-IBufferWriter{System-Byte}-'></a>
### EncodeUnsigned(value,destination) `method`

##### Summary

Takes an \`UnboundedUInt\` and converts it into an encoded unsigned LEB128 byte array

##### Returns

LEB128 bytes of value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | Value to convert to LEB128 bytes |
| destination | [System.Buffers.IBufferWriter{System.Byte}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Buffers.IBufferWriter 'System.Buffers.IBufferWriter{System.Byte}') | Buffer writer to write bytes to |

<a name='T-EdjCase-ICP-Candid-Models-NullValue'></a>
## NullValue `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

Dummy struct to represent the \`null\` candid type's value

<a name='T-EdjCase-ICP-Candid-Models-OptionalValue`1'></a>
## OptionalValue\`1 `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

A helper class to represent a candid opt value. This is used instead of just a null value due to 
ambiguity in certain scenarios

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The inner type of the opt |

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-#ctor'></a>
### #ctor() `constructor`

##### Summary

Constructor to create an optional value with no value

##### Parameters

This constructor has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-#ctor-`0-'></a>
### #ctor(value) `constructor`

##### Summary

Constructor to create an optional value with a value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [\`0](#T-`0 '`0') |  |

<a name='P-EdjCase-ICP-Candid-Models-OptionalValue`1-HasValue'></a>
### HasValue `property`

##### Summary

Is true if there is a value, false if null

<a name='P-EdjCase-ICP-Candid-Models-OptionalValue`1-ValueOrDefault'></a>
### ValueOrDefault `property`

##### Summary

The value, will be set if \`HasValue\`, otherwise will be default value

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-Cast``1'></a>
### Cast\`\`1() `method`

##### Summary

Casts the inner type of the optional value to the new type

##### Returns

An optional value with the new type

##### Parameters

This method has no parameters.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T2 | The type to cast to for the inner type |

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-Equals-EdjCase-ICP-Candid-Models-OptionalValue{`0}-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-GetValueOrDefault'></a>
### GetValueOrDefault() `method`

##### Summary

Gets the value if exists, otherwise the default value

##### Returns

The value if exists, otherwise the default value

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-GetValueOrThrow'></a>
### GetValueOrThrow() `method`

##### Summary

Gets the value if exists, otherwise throws an exception

##### Returns

The inner value of the opt

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if there is no value |

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-GetValueType'></a>
### GetValueType() `method`

##### Summary

Gets the type of the optional value, even if there is no value

##### Returns

The type of the value

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-NoValue'></a>
### NoValue() `method`

##### Summary

A helper function to create a optional value with no value

##### Returns

An empty optional value

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-SetValue-`0-'></a>
### SetValue(value) `method`

##### Summary

Sets the value and sets \`HasValue\` to true

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [\`0](#T-`0 '`0') | The value to set |

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-TryGetValue-`0@-'></a>
### TryGetValue(value) `method`

##### Summary

Tries to get the value from the opt. If a value exists it will return true and the out value will be set,
otherwise it will return false and the out value will be the default value

##### Returns

True if there is a value, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [\`0@](#T-`0@ '`0@') | The value, if exists |

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-UnsetValue'></a>
### UnsetValue() `method`

##### Summary

Removes the current value and sets \`HasValue\` to false

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-WithValue-`0-'></a>
### WithValue() `method`

##### Summary

A helper function to create a optional value with a value

##### Returns

An optional value with a value

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-op_Equality-EdjCase-ICP-Candid-Models-OptionalValue{`0},EdjCase-ICP-Candid-Models-OptionalValue{`0}-'></a>
### op_Equality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-OptionalValue`1-op_Inequality-EdjCase-ICP-Candid-Models-OptionalValue{`0},EdjCase-ICP-Candid-Models-OptionalValue{`0}-'></a>
### op_Inequality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Values-PrimitiveType'></a>
## PrimitiveType `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

##### Summary

All the candid primitive types

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Bool'></a>
### Bool `constants`

##### Summary

A boolean (true/false) value

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Empty'></a>
### Empty `constants`

##### Summary

A value with no data that is considered a subtype of all types. Practical use cases for the empty type are relatively rare.

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Float32'></a>
### Float32 `constants`

##### Summary

A 32-bit floating point number

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Float64'></a>
### Float64 `constants`

##### Summary

A 64-bit floating point number

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Int'></a>
### Int `constants`

##### Summary

A unbounded integer

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Int16'></a>
### Int16 `constants`

##### Summary

A 16-bit integer

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Int32'></a>
### Int32 `constants`

##### Summary

A 32-bit integer

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Int64'></a>
### Int64 `constants`

##### Summary

A 64-bit integer

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Int8'></a>
### Int8 `constants`

##### Summary

A 8-bit integer

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Nat'></a>
### Nat `constants`

##### Summary

A unbounded unsigned integer (natural number)

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Nat16'></a>
### Nat16 `constants`

##### Summary

A 16-bit unsigned integer (natural number)

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Nat32'></a>
### Nat32 `constants`

##### Summary

A 32-bit unsigned integer (natural number)

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Nat64'></a>
### Nat64 `constants`

##### Summary

A 64-bit unsigned integer (natural number)

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Nat8'></a>
### Nat8 `constants`

##### Summary

A 8-bit unsigned integer (natural number)

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Null'></a>
### Null `constants`

##### Summary

The null value that is a supertype of any \`opt t\` value

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Principal'></a>
### Principal `constants`

##### Summary

A candid principal value which works as an identifier for identities/canisters

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Reserved'></a>
### Reserved `constants`

##### Summary

A 'any' type value that is a supertype of all types. It allows the removal of a type without breaking
the type structure

<a name='F-EdjCase-ICP-Candid-Models-Values-PrimitiveType-Text'></a>
### Text `constants`

##### Summary

A text/string value

<a name='T-EdjCase-ICP-Candid-Models-Principal'></a>
## Principal `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

A model representing a principal byte value with helper functions

<a name='P-EdjCase-ICP-Candid-Models-Principal-Raw'></a>
### Raw `property`

##### Summary

The raw value of the principal

<a name='P-EdjCase-ICP-Candid-Models-Principal-Type'></a>
### Type `property`

##### Summary

The kind of the principal

<a name='M-EdjCase-ICP-Candid-Models-Principal-Anonymous'></a>
### Anonymous() `method`

##### Summary

Creates an anonymous principal

##### Returns

Anonymous principal

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Principal-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction-'></a>
### ComputeHash() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Principal-Equals-EdjCase-ICP-Candid-Models-Principal-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Principal-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Principal-FromBytes-System-Byte[]-'></a>
### FromBytes(raw) `method`

##### Summary

Converts raw principal bytes to a principal

##### Returns

Principal from the bytes

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| raw | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Byte array of a principal value |

<a name='M-EdjCase-ICP-Candid-Models-Principal-FromHex-System-String-'></a>
### FromHex(hex) `method`

##### Summary

Creates a principal from a non delimited hex string value

##### Returns

Principal from the hex value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hex | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A string form of a hex value with no delimiters |

<a name='M-EdjCase-ICP-Candid-Models-Principal-FromText-System-String-'></a>
### FromText(text) `method`

##### Summary

Converts a text representation of a principal to a principal

##### Returns

Principal based on the text

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The text value of the principal |

<a name='M-EdjCase-ICP-Candid-Models-Principal-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Principal-ManagementCanisterId'></a>
### ManagementCanisterId() `method`

##### Summary

Helper method to create the principal for the Internet Computer management cansiter "aaaaa-aa"

##### Returns

Principal for the management cansiter

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Principal-SelfAuthenticating-System-Byte[]-'></a>
### SelfAuthenticating(derEncodedPublicKey) `method`

##### Summary

Creates a self authenticating principal with the specified public key

##### Returns

Principal from the public key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| derEncodedPublicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | DER encoded public key |

<a name='M-EdjCase-ICP-Candid-Models-Principal-ToHex'></a>
### ToHex() `method`

##### Summary

Converts the raw principal value into a hex string in all caps with no delimiters

##### Returns

Hex value as a string

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Principal-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Principal-ToText'></a>
### ToText() `method`

##### Summary

Converts the principal into its text format, such as "rrkah-fqaaa-aaaaa-aaaaq-cai"

##### Returns

A text version of the principal

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-PrincipalType'></a>
## PrincipalType `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

The specific type of principal that is encoded

<a name='F-EdjCase-ICP-Candid-Models-PrincipalType-Anonymous'></a>
### Anonymous `constants`

##### Summary

Used when there is no authentication/signature
This has the form \`0x04\`

<a name='F-EdjCase-ICP-Candid-Models-PrincipalType-Derived'></a>
### Derived `constants`

##### Summary

These ids are treated specially when an id needs to be registered. In such a request, whoever requests an id
can provide a derivation_nonce. By hashing that together with the principal of the caller, every principal
has a space of ids that only they can register ids from.
These have the form \`H(|registering_principal| · registering_principal · derivation_nonce) · 0x03\` (29 bytes)

<a name='F-EdjCase-ICP-Candid-Models-PrincipalType-Opaque'></a>
### Opaque `constants`

##### Summary

These are always generated by the IC and have no structure of interest outside of it.
Typically end with 0x01

<a name='F-EdjCase-ICP-Candid-Models-PrincipalType-Reserved'></a>
### Reserved `constants`

##### Summary

These have the form of \`blob · 0x7f\` (29 bytes) where the blob length is between 0 and 28 bytes

<a name='F-EdjCase-ICP-Candid-Models-PrincipalType-SelfAuthenticating'></a>
### SelfAuthenticating `constants`

##### Summary

Used if the key is directly used and not delegated/derived.
These have the form \`H(public_key) · 0x02\` (29 bytes)

<a name='T-EdjCase-ICP-Candid-Models-RequestId'></a>
## RequestId `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

A helper class to wrap around the request id byte array that identifies a request

<a name='M-EdjCase-ICP-Candid-Models-RequestId-#ctor-System-Byte[]-'></a>
### #ctor(rawValue) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| rawValue | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The raw id value |

<a name='P-EdjCase-ICP-Candid-Models-RequestId-RawValue'></a>
### RawValue `property`

##### Summary

The raw id value

<a name='M-EdjCase-ICP-Candid-Models-RequestId-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction-'></a>
### ComputeHash() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-RequestId-FromObject-System-Collections-Generic-IDictionary{System-String,EdjCase-ICP-Candid-Models-IHashable},EdjCase-ICP-Candid-Crypto-IHashFunction-'></a>
### FromObject(properties,hashFunction) `method`

##### Summary

Converts a hashable object into a request id

##### Returns

A request id object

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| properties | [System.Collections.Generic.IDictionary{System.String,EdjCase.ICP.Candid.Models.IHashable}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,EdjCase.ICP.Candid.Models.IHashable}') | The properties of the object to hash |
| hashFunction | [EdjCase.ICP.Candid.Crypto.IHashFunction](#T-EdjCase-ICP-Candid-Crypto-IHashFunction 'EdjCase.ICP.Candid.Crypto.IHashFunction') | The hash function to use to generate the hash |

<a name='T-EdjCase-ICP-Candid-Models-ReservedValue'></a>
## ReservedValue `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

Dummy struct to represent the \`reserved\` candid type's value

<a name='T-EdjCase-ICP-Candid-Crypto-SHA256HashFunction'></a>
## SHA256HashFunction `type`

##### Namespace

EdjCase.ICP.Candid.Crypto

##### Summary

A SHA256 implementation of the \`IHashFunction\`

<a name='M-EdjCase-ICP-Candid-Crypto-SHA256HashFunction-ComputeHash-System-Byte[]-'></a>
### ComputeHash() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Crypto-SHA256HashFunction-Create'></a>
### Create() `method`

##### Summary

Helper method to create the hash function object

##### Returns

Hash function object

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-StatePath'></a>
## StatePath `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

A model representing a list of path segments, used to navigate a state hash tree

<a name='M-EdjCase-ICP-Candid-Models-StatePath-#ctor-System-Collections-Generic-List{EdjCase-ICP-Candid-Models-StatePathSegment}-'></a>
### #ctor(segments) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| segments | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.StatePathSegment}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.StatePathSegment}') | The list of segments making up the path |

<a name='P-EdjCase-ICP-Candid-Models-StatePath-Segments'></a>
### Segments `property`

##### Summary

The list of segments making up the path

<a name='M-EdjCase-ICP-Candid-Models-StatePath-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction-'></a>
### ComputeHash() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-StatePath-FromSegments-EdjCase-ICP-Candid-Models-StatePathSegment[]-'></a>
### FromSegments(segments) `method`

##### Summary

Helper method to create a path from a path segment array

##### Returns

A path object

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| segments | [EdjCase.ICP.Candid.Models.StatePathSegment[]](#T-EdjCase-ICP-Candid-Models-StatePathSegment[] 'EdjCase.ICP.Candid.Models.StatePathSegment[]') | An array of segments that make up the path |

<a name='T-EdjCase-ICP-Candid-Models-StatePathSegment'></a>
## StatePathSegment `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

A model representing a segment of a path for a state hash tree

<a name='M-EdjCase-ICP-Candid-Models-StatePathSegment-#ctor-System-Byte[]-'></a>
### #ctor(value) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The raw value of the path segment |

<a name='P-EdjCase-ICP-Candid-Models-StatePathSegment-Value'></a>
### Value `property`

##### Summary

The raw value of the path segment

<a name='M-EdjCase-ICP-Candid-Models-StatePathSegment-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction-'></a>
### ComputeHash() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-StatePathSegment-FromString-System-String-'></a>
### FromString(segment) `method`

##### Summary

Creates a path segment from a string value by converting the string into UTF-8 bytes

##### Returns

A path segment

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| segment | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The path segment to use |

<a name='M-EdjCase-ICP-Candid-Models-StatePathSegment-op_Implicit-EdjCase-ICP-Candid-Models-StatePathSegment-~System-Byte[]'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a path segment to its raw byte array

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.StatePathSegment)~System.Byte[]](#T-EdjCase-ICP-Candid-Models-StatePathSegment-~System-Byte[] 'EdjCase.ICP.Candid.Models.StatePathSegment)~System.Byte[]') | The segment to convert |

<a name='M-EdjCase-ICP-Candid-Models-StatePathSegment-op_Implicit-System-Byte[]-~EdjCase-ICP-Candid-Models-StatePathSegment'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a raw byte array to a path segment

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte[])~EdjCase.ICP.Candid.Models.StatePathSegment](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[])~EdjCase.ICP.Candid.Models.StatePathSegment 'System.Byte[])~EdjCase.ICP.Candid.Models.StatePathSegment') | Byte array to convert |

<a name='M-EdjCase-ICP-Candid-Models-StatePathSegment-op_Implicit-System-String-~EdjCase-ICP-Candid-Models-StatePathSegment'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a string to a path segment

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String)~EdjCase.ICP.Candid.Models.StatePathSegment](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String)~EdjCase.ICP.Candid.Models.StatePathSegment 'System.String)~EdjCase.ICP.Candid.Models.StatePathSegment') | String value to convert |

<a name='T-EdjCase-ICP-Candid-Models-UnboundedInt'></a>
## UnboundedInt `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

An integer value with no bounds on how large it can get and variable byte size

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-CompareTo-EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### CompareTo() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-Equals-EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-FromBigInteger-System-Numerics-BigInteger-'></a>
### FromBigInteger(value) `method`

##### Summary

Converts a big integer to an unbounded int

##### Returns

An unbounded int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Numerics.BigInteger](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Numerics.BigInteger 'System.Numerics.BigInteger') | Big integer to convert |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-FromInt64-System-Int64-'></a>
### FromInt64(value) `method`

##### Summary

A helper method to convert a Int64 to a unbounded int

##### Returns

An unbounded int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | A Int64 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-GetRawBytes-System-Boolean-'></a>
### GetRawBytes(isBigEndian) `method`

##### Summary

Gets the raw bytes of the number

##### Returns

Byte array of the number

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| isBigEndian | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | True if the byte order should be big endian (most significant bytes first),
otherwise the order will be in little endian (least significant bytes first) |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-ToBigInteger'></a>
### ToBigInteger() `method`

##### Summary

Converts a unbounded int to a big integer value

##### Returns

A big integer

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-TryToInt64-System-Int64@-'></a>
### TryToInt64(value) `method`

##### Summary

Tries to get the Int64 representation of the value, will not if that value is too large to fit
into a Int64.

##### Returns

True if converted, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int64@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64@ 'System.Int64@') | Out parameter that is set ONLY if the return value is true |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Addition-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### op_Addition() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Decrement-EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### op_Decrement() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Division-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### op_Division() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Equality-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### op_Equality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-UInt64'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded int to a UInt64

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt)~System.UInt64](#T-EdjCase-ICP-Candid-Models-UnboundedInt-~System-UInt64 'EdjCase.ICP.Candid.Models.UnboundedInt)~System.UInt64') | An UInt64 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-UInt32'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded int to a UInt32

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt)~System.UInt32](#T-EdjCase-ICP-Candid-Models-UnboundedInt-~System-UInt32 'EdjCase.ICP.Candid.Models.UnboundedInt)~System.UInt32') | An UInt32 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-UInt16'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded int to a UInt16

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt)~System.UInt16](#T-EdjCase-ICP-Candid-Models-UnboundedInt-~System-UInt16 'EdjCase.ICP.Candid.Models.UnboundedInt)~System.UInt16') | An UInt16 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-Byte'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded int to a UInt8

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt)~System.Byte](#T-EdjCase-ICP-Candid-Models-UnboundedInt-~System-Byte 'EdjCase.ICP.Candid.Models.UnboundedInt)~System.Byte') | An UInt8 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-Int64'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded int to a Int64

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt)~System.Int64](#T-EdjCase-ICP-Candid-Models-UnboundedInt-~System-Int64 'EdjCase.ICP.Candid.Models.UnboundedInt)~System.Int64') | An Int64 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-Int32'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded int to a Int32

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt)~System.Int32](#T-EdjCase-ICP-Candid-Models-UnboundedInt-~System-Int32 'EdjCase.ICP.Candid.Models.UnboundedInt)~System.Int32') | An Int32 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-Int16'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded int to a Int16

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt)~System.Int16](#T-EdjCase-ICP-Candid-Models-UnboundedInt-~System-Int16 'EdjCase.ICP.Candid.Models.UnboundedInt)~System.Int16') | An Int16 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~System-SByte'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded int to a Int8

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt)~System.SByte](#T-EdjCase-ICP-Candid-Models-UnboundedInt-~System-SByte 'EdjCase.ICP.Candid.Models.UnboundedInt)~System.SByte') | An Int8 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_GreaterThan-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### op_GreaterThan() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_GreaterThanOrEqual-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### op_GreaterThanOrEqual() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~EdjCase-ICP-Candid-Models-UnboundedInt'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a unbounded uint to an unbounded int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt)~EdjCase.ICP.Candid.Models.UnboundedInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt-~EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedUInt)~EdjCase.ICP.Candid.Models.UnboundedInt') | An unbounded uint |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-Int64-~EdjCase-ICP-Candid-Models-UnboundedInt'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a Int64 to an unbounded int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int64)~EdjCase.ICP.Candid.Models.UnboundedInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64)~EdjCase.ICP.Candid.Models.UnboundedInt 'System.Int64)~EdjCase.ICP.Candid.Models.UnboundedInt') | An Int64 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-Int32-~EdjCase-ICP-Candid-Models-UnboundedInt'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a Int32 to an unbounded int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int32)~EdjCase.ICP.Candid.Models.UnboundedInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32)~EdjCase.ICP.Candid.Models.UnboundedInt 'System.Int32)~EdjCase.ICP.Candid.Models.UnboundedInt') | An Int32 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-Int16-~EdjCase-ICP-Candid-Models-UnboundedInt'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a Int16 to an unbounded int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int16)~EdjCase.ICP.Candid.Models.UnboundedInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int16)~EdjCase.ICP.Candid.Models.UnboundedInt 'System.Int16)~EdjCase.ICP.Candid.Models.UnboundedInt') | An Int16 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-SByte-~EdjCase-ICP-Candid-Models-UnboundedInt'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a Int8 to an unbounded int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.SByte)~EdjCase.ICP.Candid.Models.UnboundedInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.SByte)~EdjCase.ICP.Candid.Models.UnboundedInt 'System.SByte)~EdjCase.ICP.Candid.Models.UnboundedInt') | An Int8 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-UInt64-~EdjCase-ICP-Candid-Models-UnboundedInt'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a UInt64 to an unbounded int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt64)~EdjCase.ICP.Candid.Models.UnboundedInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64)~EdjCase.ICP.Candid.Models.UnboundedInt 'System.UInt64)~EdjCase.ICP.Candid.Models.UnboundedInt') | An UInt64 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-UInt32-~EdjCase-ICP-Candid-Models-UnboundedInt'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a UInt32 to an unbounded int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt32)~EdjCase.ICP.Candid.Models.UnboundedInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32)~EdjCase.ICP.Candid.Models.UnboundedInt 'System.UInt32)~EdjCase.ICP.Candid.Models.UnboundedInt') | An UInt32 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-UInt16-~EdjCase-ICP-Candid-Models-UnboundedInt'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a UInt16 to an unbounded int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt16)~EdjCase.ICP.Candid.Models.UnboundedInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt16)~EdjCase.ICP.Candid.Models.UnboundedInt 'System.UInt16)~EdjCase.ICP.Candid.Models.UnboundedInt') | An UInt16 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Implicit-System-Byte-~EdjCase-ICP-Candid-Models-UnboundedInt'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a UInt8 to an unbounded int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte)~EdjCase.ICP.Candid.Models.UnboundedInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte)~EdjCase.ICP.Candid.Models.UnboundedInt 'System.Byte)~EdjCase.ICP.Candid.Models.UnboundedInt') | An UInt8 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Increment-EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### op_Increment() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Inequality-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### op_Inequality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_LessThan-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### op_LessThan() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_LessThanOrEqual-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### op_LessThanOrEqual() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Multiply-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### op_Multiply() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedInt-op_Subtraction-EdjCase-ICP-Candid-Models-UnboundedInt,EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### op_Subtraction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-UnboundedIntExtensions'></a>
## UnboundedIntExtensions `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

Extension methods related to unbounded ints

<a name='M-EdjCase-ICP-Candid-Models-UnboundedIntExtensions-ToUnbounded-System-Int64-'></a>
### ToUnbounded(value) `method`

##### Summary

Converts a Int64 into an unbounded int

##### Returns

An unbounded int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | A Int64 value |

<a name='T-EdjCase-ICP-Candid-Models-UnboundedUInt'></a>
## UnboundedUInt `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

An unsigned integer value with no bounds on how large it can get and variable byte size

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-CompareTo-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### CompareTo() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-Equals-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-Equals-System-Object-'></a>
### Equals() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-FromBigInteger-System-Numerics-BigInteger-'></a>
### FromBigInteger(value) `method`

##### Summary

Converts a big integer to an unbounded uint

##### Returns

An unbounded uint

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Numerics.BigInteger](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Numerics.BigInteger 'System.Numerics.BigInteger') | Big integer to convert |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-FromUInt64-System-UInt64-'></a>
### FromUInt64(value) `method`

##### Summary

A helper method to convert a UInt64 to a unbounded uint

##### Returns

An unbounded uint

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64 'System.UInt64') | A UInt64 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-GetRawBytes-System-Boolean-'></a>
### GetRawBytes(isBigEndian) `method`

##### Summary

Gets the raw bytes of the number

##### Returns

Byte array of the number

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| isBigEndian | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | True if the byte order should be big endian (most significant bytes first),
otherwise the order will be in little endian (least significant bytes first) |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-ToBigInteger'></a>
### ToBigInteger() `method`

##### Summary

Converts a unbounded uint to a big integer value

##### Returns

A big integer

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-TryToUInt64-System-UInt64@-'></a>
### TryToUInt64(value) `method`

##### Summary

Tries to get the UInt64 representation of the value, will not if that value is too large to fit
into a UInt64.

##### Returns

True if converted, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt64@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64@ 'System.UInt64@') | Out parameter that is set ONLY if the return value is true |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Addition-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### op_Addition() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Decrement-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### op_Decrement() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Division-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### op_Division() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Equality-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### op_Equality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedInt-~EdjCase-ICP-Candid-Models-UnboundedUInt'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to implicitly convert a unbounded int to an unbounded uint

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt)~EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedInt-~EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedInt)~EdjCase.ICP.Candid.Models.UnboundedUInt') | An unbounded uint |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-System-Int64-~EdjCase-ICP-Candid-Models-UnboundedUInt'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert a Int64 to an unbounded uint

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int64)~EdjCase.ICP.Candid.Models.UnboundedUInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64)~EdjCase.ICP.Candid.Models.UnboundedUInt 'System.Int64)~EdjCase.ICP.Candid.Models.UnboundedUInt') | An Int64 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-System-Int32-~EdjCase-ICP-Candid-Models-UnboundedUInt'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert a Int32 to an unbounded uint

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int32)~EdjCase.ICP.Candid.Models.UnboundedUInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32)~EdjCase.ICP.Candid.Models.UnboundedUInt 'System.Int32)~EdjCase.ICP.Candid.Models.UnboundedUInt') | An Int32 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-System-Int16-~EdjCase-ICP-Candid-Models-UnboundedUInt'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert a Int16 to an unbounded uint

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Int16)~EdjCase.ICP.Candid.Models.UnboundedUInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int16)~EdjCase.ICP.Candid.Models.UnboundedUInt 'System.Int16)~EdjCase.ICP.Candid.Models.UnboundedUInt') | An Int16 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-System-SByte-~EdjCase-ICP-Candid-Models-UnboundedUInt'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert a Int8 to an unbounded uint

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.SByte)~EdjCase.ICP.Candid.Models.UnboundedUInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.SByte)~EdjCase.ICP.Candid.Models.UnboundedUInt 'System.SByte)~EdjCase.ICP.Candid.Models.UnboundedUInt') | An Int8 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-UInt64'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded uint to a UInt64

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt)~System.UInt64](#T-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-UInt64 'EdjCase.ICP.Candid.Models.UnboundedUInt)~System.UInt64') | A UInt64 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-UInt32'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded uint to a UInt32

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt)~System.UInt32](#T-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-UInt32 'EdjCase.ICP.Candid.Models.UnboundedUInt)~System.UInt32') | A UInt32 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-UInt16'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded uint to a UInt16

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt)~System.UInt16](#T-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-UInt16 'EdjCase.ICP.Candid.Models.UnboundedUInt)~System.UInt16') | A UInt16 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-Byte'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded uint to a UInt8

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt)~System.Byte](#T-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-Byte 'EdjCase.ICP.Candid.Models.UnboundedUInt)~System.Byte') | A UInt8 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-Int64'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded uint to a Int64

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt)~System.Int64](#T-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-Int64 'EdjCase.ICP.Candid.Models.UnboundedUInt)~System.Int64') | An Int64 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-Int32'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded uint to a Int32

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt)~System.Int32](#T-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-Int32 'EdjCase.ICP.Candid.Models.UnboundedUInt)~System.Int32') | An Int32 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-Int16'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded uint to a Int16

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt)~System.Int16](#T-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-Int16 'EdjCase.ICP.Candid.Models.UnboundedUInt)~System.Int16') | An Int16 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Explicit-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-SByte'></a>
### op_Explicit(value) `method`

##### Summary

A helper method to explicitly convert an unbounded uint to a Int8

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt)~System.SByte](#T-EdjCase-ICP-Candid-Models-UnboundedUInt-~System-SByte 'EdjCase.ICP.Candid.Models.UnboundedUInt)~System.SByte') | An Int8 |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_GreaterThan-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### op_GreaterThan() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_GreaterThanOrEqual-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### op_GreaterThanOrEqual() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Implicit-System-UInt64-~EdjCase-ICP-Candid-Models-UnboundedUInt'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a UInt64 to an unbounded uint

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt64)~EdjCase.ICP.Candid.Models.UnboundedUInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64)~EdjCase.ICP.Candid.Models.UnboundedUInt 'System.UInt64)~EdjCase.ICP.Candid.Models.UnboundedUInt') | An UInt64 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Implicit-System-UInt32-~EdjCase-ICP-Candid-Models-UnboundedUInt'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a UInt32 to an unbounded uint

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt32)~EdjCase.ICP.Candid.Models.UnboundedUInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt32)~EdjCase.ICP.Candid.Models.UnboundedUInt 'System.UInt32)~EdjCase.ICP.Candid.Models.UnboundedUInt') | An UInt32 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Implicit-System-UInt16-~EdjCase-ICP-Candid-Models-UnboundedUInt'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a UInt16 to an unbounded uint

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt16)~EdjCase.ICP.Candid.Models.UnboundedUInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt16)~EdjCase.ICP.Candid.Models.UnboundedUInt 'System.UInt16)~EdjCase.ICP.Candid.Models.UnboundedUInt') | An UInt16 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Implicit-System-Byte-~EdjCase-ICP-Candid-Models-UnboundedUInt'></a>
### op_Implicit(value) `method`

##### Summary

A helper method to implicitly convert a UInt8 to an unbounded uint

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte)~EdjCase.ICP.Candid.Models.UnboundedUInt](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte)~EdjCase.ICP.Candid.Models.UnboundedUInt 'System.Byte)~EdjCase.ICP.Candid.Models.UnboundedUInt') | An UInt8 value |

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Increment-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### op_Increment() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Inequality-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### op_Inequality() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_LessThan-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### op_LessThan() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_LessThanOrEqual-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### op_LessThanOrEqual() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Multiply-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### op_Multiply() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUInt-op_Subtraction-EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### op_Subtraction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-UnboundedUIntExtensions'></a>
## UnboundedUIntExtensions `type`

##### Namespace

EdjCase.ICP.Candid.Models

##### Summary

Extensions methods around UnboundedUInt

<a name='M-EdjCase-ICP-Candid-Models-UnboundedUIntExtensions-ToUnbounded-System-UInt64-'></a>
### ToUnbounded(value) `method`

##### Summary

Converts a UInt64 to a unbounded uint

##### Returns

An unbounded uint

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.UInt64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64 'System.UInt64') | Int64 value |

<a name='T-EdjCase-ICP-Candid-Mapping-VariantAttribute'></a>
## VariantAttribute `type`

##### Namespace

EdjCase.ICP.Candid.Mapping

##### Summary

An attribute to put on a class to identify it as a variant type for serialization.
Requires the use of \`VariantTagPropertyAttribute\`, \`VariantOptionTypeAttribute\` and
\`VariantValuePropertyAttribute\` attributes if used

<a name='T-EdjCase-ICP-Candid-Mapping-VariantOptionAttribute'></a>
## VariantOptionAttribute `type`

##### Namespace

EdjCase.ICP.Candid.Mapping

##### Summary

An attribute to put on an enum option to specify if the tag has an attached
value in the variant, otherwise the attached type will be null

<a name='M-EdjCase-ICP-Candid-Mapping-VariantOptionAttribute-#ctor-EdjCase-ICP-Candid-Models-CandidTag-'></a>
### #ctor(tag) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tag | [EdjCase.ICP.Candid.Models.CandidTag](#T-EdjCase-ICP-Candid-Models-CandidTag 'EdjCase.ICP.Candid.Models.CandidTag') | The tag of the variant option |

<a name='P-EdjCase-ICP-Candid-Mapping-VariantOptionAttribute-Tag'></a>
### Tag `property`

##### Summary

The type of the variant option value to use

<a name='T-EdjCase-ICP-Candid-Mapping-VariantTagPropertyAttribute'></a>
## VariantTagPropertyAttribute `type`

##### Namespace

EdjCase.ICP.Candid.Mapping

##### Summary

An attribute to put on a property/field that indicates where to hold the 
tag enum value. Must match the type passed to the \`VariantAttribute\`

<a name='T-EdjCase-ICP-Candid-Mapping-VariantValuePropertyAttribute'></a>
## VariantValuePropertyAttribute `type`

##### Namespace

EdjCase.ICP.Candid.Mapping

##### Summary

An attribute to put on a property/field that indicates where to hold the
tag value object. The type must be compatible with all value types, recommend using \`object?\`
