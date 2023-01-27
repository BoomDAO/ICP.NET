<a name='assembly'></a>
# EdjCase.ICP.Candid

## Contents

- [BinarySequence](#T-EdjCase-ICP-Candid-BinarySequence 'EdjCase.ICP.Candid.BinarySequence')
  - [#ctor(bits)](#M-EdjCase-ICP-Candid-BinarySequence-#ctor-System-Boolean[]- 'EdjCase.ICP.Candid.BinarySequence.#ctor(System.Boolean[])')
- [CRC32](#T-EdjCase-ICP-Candid-Crypto-CRC32 'EdjCase.ICP.Candid.Crypto.CRC32')
  - [ComputeHash(stream)](#M-EdjCase-ICP-Candid-Crypto-CRC32-ComputeHash-System-IO-Stream- 'EdjCase.ICP.Candid.Crypto.CRC32.ComputeHash(System.IO.Stream)')
  - [ComputeHash(data)](#M-EdjCase-ICP-Candid-Crypto-CRC32-ComputeHash-System-Byte[]- 'EdjCase.ICP.Candid.Crypto.CRC32.ComputeHash(System.Byte[])')
- [CandidArg](#T-EdjCase-ICP-Candid-Models-CandidArg 'EdjCase.ICP.Candid.Models.CandidArg')
  - [ComputeHash()](#M-EdjCase-ICP-Candid-Models-CandidArg-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction- 'EdjCase.ICP.Candid.Models.CandidArg.ComputeHash(EdjCase.ICP.Candid.Crypto.IHashFunction)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-CandidArg-Equals-EdjCase-ICP-Candid-Models-CandidArg- 'EdjCase.ICP.Candid.Models.CandidArg.Equals(EdjCase.ICP.Candid.Models.CandidArg)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-CandidArg-Equals-System-Object- 'EdjCase.ICP.Candid.Models.CandidArg.Equals(System.Object)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-CandidArg-GetHashCode 'EdjCase.ICP.Candid.Models.CandidArg.GetHashCode')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-CandidArg-ToString 'EdjCase.ICP.Candid.Models.CandidArg.ToString')
  - [op_Equality()](#M-EdjCase-ICP-Candid-Models-CandidArg-op_Equality-EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-CandidArg- 'EdjCase.ICP.Candid.Models.CandidArg.op_Equality(EdjCase.ICP.Candid.Models.CandidArg,EdjCase.ICP.Candid.Models.CandidArg)')
  - [op_Inequality()](#M-EdjCase-ICP-Candid-Models-CandidArg-op_Inequality-EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-CandidArg- 'EdjCase.ICP.Candid.Models.CandidArg.op_Inequality(EdjCase.ICP.Candid.Models.CandidArg,EdjCase.ICP.Candid.Models.CandidArg)')
- [CandidArgBuilder](#T-EdjCase-ICP-Candid-CandidArgBuilder 'EdjCase.ICP.Candid.CandidArgBuilder')
  - [EncodedTypes](#F-EdjCase-ICP-Candid-CandidArgBuilder-EncodedTypes 'EdjCase.ICP.Candid.CandidArgBuilder.EncodedTypes')
  - [EncodedValues](#F-EdjCase-ICP-Candid-CandidArgBuilder-EncodedValues 'EdjCase.ICP.Candid.CandidArgBuilder.EncodedValues')
  - [compoundTypeTable](#F-EdjCase-ICP-Candid-CandidArgBuilder-compoundTypeTable 'EdjCase.ICP.Candid.CandidArgBuilder.compoundTypeTable')
- [CandidByteParser](#T-EdjCase-ICP-Candid-Parsers-CandidByteParser 'EdjCase.ICP.Candid.Parsers.CandidByteParser')
  - [Parse(value)](#M-EdjCase-ICP-Candid-Parsers-CandidByteParser-Parse-System-Byte[]- 'EdjCase.ICP.Candid.Parsers.CandidByteParser.Parse(System.Byte[])')
- [CandidCompoundType](#T-EdjCase-ICP-Candid-Models-Types-CandidCompoundType 'EdjCase.ICP.Candid.Models.Types.CandidCompoundType')
  - [#ctor(recursiveId)](#M-EdjCase-ICP-Candid-Models-Types-CandidCompoundType-#ctor-EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.Types.CandidCompoundType.#ctor(EdjCase.ICP.Candid.Models.CandidId)')
  - [RecursiveId](#P-EdjCase-ICP-Candid-Models-Types-CandidCompoundType-RecursiveId 'EdjCase.ICP.Candid.Models.Types.CandidCompoundType.RecursiveId')
  - [EncodeInnerTypes(compoundTypeTable)](#M-EdjCase-ICP-Candid-Models-Types-CandidCompoundType-EncodeInnerTypes-EdjCase-ICP-Candid-Models-CompoundTypeTable- 'EdjCase.ICP.Candid.Models.Types.CandidCompoundType.EncodeInnerTypes(EdjCase.ICP.Candid.Models.CompoundTypeTable)')
- [CandidDecodingException](#T-EdjCase-ICP-Candid-Exceptions-CandidDecodingException 'EdjCase.ICP.Candid.Exceptions.CandidDecodingException')
  - [#ctor(byteEndIndex,message)](#M-EdjCase-ICP-Candid-Exceptions-CandidDecodingException-#ctor-System-Int32,System-String- 'EdjCase.ICP.Candid.Exceptions.CandidDecodingException.#ctor(System.Int32,System.String)')
  - [ByteEndIndex](#P-EdjCase-ICP-Candid-Exceptions-CandidDecodingException-ByteEndIndex 'EdjCase.ICP.Candid.Exceptions.CandidDecodingException.ByteEndIndex')
  - [ErrorMessage](#P-EdjCase-ICP-Candid-Exceptions-CandidDecodingException-ErrorMessage 'EdjCase.ICP.Candid.Exceptions.CandidDecodingException.ErrorMessage')
  - [Message](#P-EdjCase-ICP-Candid-Exceptions-CandidDecodingException-Message 'EdjCase.ICP.Candid.Exceptions.CandidDecodingException.Message')
- [CandidFunc](#T-EdjCase-ICP-Candid-Models-Values-CandidFunc 'EdjCase.ICP.Candid.Models.Values.CandidFunc')
  - [#ctor(service,name)](#M-EdjCase-ICP-Candid-Models-Values-CandidFunc-#ctor-EdjCase-ICP-Candid-Models-Values-CandidService,System-String- 'EdjCase.ICP.Candid.Models.Values.CandidFunc.#ctor(EdjCase.ICP.Candid.Models.Values.CandidService,System.String)')
  - [IsOpaqueReference](#P-EdjCase-ICP-Candid-Models-Values-CandidFunc-IsOpaqueReference 'EdjCase.ICP.Candid.Models.Values.CandidFunc.IsOpaqueReference')
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidFunc-Type 'EdjCase.ICP.Candid.Models.Values.CandidFunc.Type')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidFunc-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}- 'EdjCase.ICP.Candid.Models.Values.CandidFunc.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType})')
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
  - [CompareTo()](#M-EdjCase-ICP-Candid-Models-CandidId-CompareTo-EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.CandidId.CompareTo(EdjCase.ICP.Candid.Models.CandidId)')
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
  - [Name](#P-EdjCase-ICP-Candid-Mapping-CandidNameAttribute-Name 'EdjCase.ICP.Candid.Mapping.CandidNameAttribute.Name')
- [CandidOptional](#T-EdjCase-ICP-Candid-Models-Values-CandidOptional 'EdjCase.ICP.Candid.Models.Values.CandidOptional')
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidOptional-Type 'EdjCase.ICP.Candid.Models.Values.CandidOptional.Type')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidOptional-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}- 'EdjCase.ICP.Candid.Models.Values.CandidOptional.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType})')
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
  - [AsPrincipal()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsPrincipal 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.AsPrincipal')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}- 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType})')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.Equals(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-GetHashCode 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.GetHashCode')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-ToString 'EdjCase.ICP.Candid.Models.Values.CandidPrimitive.ToString')
- [CandidPrimitiveType](#T-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType 'EdjCase.ICP.Candid.Models.Types.CandidPrimitiveType')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType-Equals-System-Object- 'EdjCase.ICP.Candid.Models.Types.CandidPrimitiveType.Equals(System.Object)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Types-CandidPrimitiveType-GetHashCode 'EdjCase.ICP.Candid.Models.Types.CandidPrimitiveType.GetHashCode')
- [CandidRecord](#T-EdjCase-ICP-Candid-Models-Values-CandidRecord 'EdjCase.ICP.Candid.Models.Values.CandidRecord')
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidRecord-Type 'EdjCase.ICP.Candid.Models.Values.CandidRecord.Type')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType})')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidRecord.Equals(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-GetHashCode 'EdjCase.ICP.Candid.Models.Values.CandidRecord.GetHashCode')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Values-CandidRecord-ToString 'EdjCase.ICP.Candid.Models.Values.CandidRecord.ToString')
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
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidService-Type 'EdjCase.ICP.Candid.Models.Values.CandidService.Type')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidService-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}- 'EdjCase.ICP.Candid.Models.Values.CandidService.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType})')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidService-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidService.Equals(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Values-CandidService-GetHashCode 'EdjCase.ICP.Candid.Models.Values.CandidService.GetHashCode')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Values-CandidService-ToString 'EdjCase.ICP.Candid.Models.Values.CandidService.ToString')
- [CandidServiceType](#T-EdjCase-ICP-Candid-Models-Types-CandidServiceType 'EdjCase.ICP.Candid.Models.Types.CandidServiceType')
  - [#ctor(methods,recursiveId)](#M-EdjCase-ICP-Candid-Models-Types-CandidServiceType-#ctor-System-Collections-Generic-Dictionary{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidFuncType},EdjCase-ICP-Candid-Models-CandidId- 'EdjCase.ICP.Candid.Models.Types.CandidServiceType.#ctor(System.Collections.Generic.Dictionary{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidFuncType},EdjCase.ICP.Candid.Models.CandidId)')
  - [Methods](#P-EdjCase-ICP-Candid-Models-Types-CandidServiceType-Methods 'EdjCase.ICP.Candid.Models.Types.CandidServiceType.Methods')
  - [Type](#P-EdjCase-ICP-Candid-Models-Types-CandidServiceType-Type 'EdjCase.ICP.Candid.Models.Types.CandidServiceType.Type')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Types-CandidServiceType-Equals-System-Object- 'EdjCase.ICP.Candid.Models.Types.CandidServiceType.Equals(System.Object)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Types-CandidServiceType-GetHashCode 'EdjCase.ICP.Candid.Models.Types.CandidServiceType.GetHashCode')
- [CandidTag](#T-EdjCase-ICP-Candid-Models-CandidTag 'EdjCase.ICP.Candid.Models.CandidTag')
  - [HashName(name)](#M-EdjCase-ICP-Candid-Models-CandidTag-HashName-System-String- 'EdjCase.ICP.Candid.Models.CandidTag.HashName(System.String)')
- [CandidTextParseException](#T-EdjCase-ICP-Candid-Exceptions-CandidTextParseException 'EdjCase.ICP.Candid.Exceptions.CandidTextParseException')
  - [Message](#P-EdjCase-ICP-Candid-Exceptions-CandidTextParseException-Message 'EdjCase.ICP.Candid.Exceptions.CandidTextParseException.Message')
- [CandidType](#T-EdjCase-ICP-Candid-Models-Types-CandidType 'EdjCase.ICP.Candid.Models.Types.CandidType')
  - [Bool()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Bool 'EdjCase.ICP.Candid.Models.Types.CandidType.Bool')
  - [Empty()](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Empty 'EdjCase.ICP.Candid.Models.Types.CandidType.Empty')
  - [Encode(compoundTypeTable)](#M-EdjCase-ICP-Candid-Models-Types-CandidType-Encode-EdjCase-ICP-Candid-Models-CompoundTypeTable- 'EdjCase.ICP.Candid.Models.Types.CandidType.Encode(EdjCase.ICP.Candid.Models.CompoundTypeTable)')
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
- [CandidTypedValue](#T-EdjCase-ICP-Candid-Models-CandidTypedValue 'EdjCase.ICP.Candid.Models.CandidTypedValue')
  - [AsPrincipal()](#M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsPrincipal 'EdjCase.ICP.Candid.Models.CandidTypedValue.AsPrincipal')
- [CandidValue](#T-EdjCase-ICP-Candid-Models-Values-CandidValue 'EdjCase.ICP.Candid.Models.Values.CandidValue')
  - [AsPrincipal()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsPrincipal 'EdjCase.ICP.Candid.Models.Values.CandidValue.AsPrincipal')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Equals(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-Equals-System-Object- 'EdjCase.ICP.Candid.Models.Values.CandidValue.Equals(System.Object)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-GetHashCode 'EdjCase.ICP.Candid.Models.Values.CandidValue.GetHashCode')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-ToString 'EdjCase.ICP.Candid.Models.Values.CandidValue.ToString')
  - [op_Equality()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-op_Equality-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidValue.op_Equality(EdjCase.ICP.Candid.Models.Values.CandidValue,EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [op_Inequality()](#M-EdjCase-ICP-Candid-Models-Values-CandidValue-op_Inequality-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidValue.op_Inequality(EdjCase.ICP.Candid.Models.Values.CandidValue,EdjCase.ICP.Candid.Models.Values.CandidValue)')
- [CandidVariant](#T-EdjCase-ICP-Candid-Models-Values-CandidVariant 'EdjCase.ICP.Candid.Models.Values.CandidVariant')
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidVariant-Type 'EdjCase.ICP.Candid.Models.Values.CandidVariant.Type')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidVariant-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}- 'EdjCase.ICP.Candid.Models.Values.CandidVariant.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType})')
  - [Equals()](#M-EdjCase-ICP-Candid-Models-Values-CandidVariant-Equals-EdjCase-ICP-Candid-Models-Values-CandidValue- 'EdjCase.ICP.Candid.Models.Values.CandidVariant.Equals(EdjCase.ICP.Candid.Models.Values.CandidValue)')
  - [GetHashCode()](#M-EdjCase-ICP-Candid-Models-Values-CandidVariant-GetHashCode 'EdjCase.ICP.Candid.Models.Values.CandidVariant.GetHashCode')
  - [ToString()](#M-EdjCase-ICP-Candid-Models-Values-CandidVariant-ToString 'EdjCase.ICP.Candid.Models.Values.CandidVariant.ToString')
- [CandidVariantType](#T-EdjCase-ICP-Candid-Models-Types-CandidVariantType 'EdjCase.ICP.Candid.Models.Types.CandidVariantType')
  - [Type](#P-EdjCase-ICP-Candid-Models-Types-CandidVariantType-Type 'EdjCase.ICP.Candid.Models.Types.CandidVariantType.Type')
- [CandidVector](#T-EdjCase-ICP-Candid-Models-Values-CandidVector 'EdjCase.ICP.Candid.Models.Values.CandidVector')
  - [Type](#P-EdjCase-ICP-Candid-Models-Values-CandidVector-Type 'EdjCase.ICP.Candid.Models.Values.CandidVector.Type')
  - [EncodeValue()](#M-EdjCase-ICP-Candid-Models-Values-CandidVector-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}- 'EdjCase.ICP.Candid.Models.Values.CandidVector.EncodeValue(EdjCase.ICP.Candid.Models.Types.CandidType,System.Func{EdjCase.ICP.Candid.Models.CandidId,EdjCase.ICP.Candid.Models.Types.CandidCompoundType})')
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
- [CustomMapperAttribute](#T-EdjCase-ICP-Candid-Mapping-CustomMapperAttribute 'EdjCase.ICP.Candid.Mapping.CustomMapperAttribute')
  - [#ctor(mapper)](#M-EdjCase-ICP-Candid-Mapping-CustomMapperAttribute-#ctor-EdjCase-ICP-Candid-Mapping-IObjectMapper- 'EdjCase.ICP.Candid.Mapping.CustomMapperAttribute.#ctor(EdjCase.ICP.Candid.Mapping.IObjectMapper)')
  - [Mapper](#P-EdjCase-ICP-Candid-Mapping-CustomMapperAttribute-Mapper 'EdjCase.ICP.Candid.Mapping.CustomMapperAttribute.Mapper')
- [FuncMode](#T-EdjCase-ICP-Candid-Models-Types-FuncMode 'EdjCase.ICP.Candid.Models.Types.FuncMode')
  - [Oneway](#F-EdjCase-ICP-Candid-Models-Types-FuncMode-Oneway 'EdjCase.ICP.Candid.Models.Types.FuncMode.Oneway')
  - [Query](#F-EdjCase-ICP-Candid-Models-Types-FuncMode-Query 'EdjCase.ICP.Candid.Models.Types.FuncMode.Query')
- [HashTree](#T-EdjCase-ICP-Candid-Models-HashTree 'EdjCase.ICP.Candid.Models.HashTree')
  - [BuildRootHash()](#M-EdjCase-ICP-Candid-Models-HashTree-BuildRootHash 'EdjCase.ICP.Candid.Models.HashTree.BuildRootHash')
- [IHashFunction](#T-EdjCase-ICP-Candid-Crypto-IHashFunction 'EdjCase.ICP.Candid.Crypto.IHashFunction')
  - [ComputeHash(value)](#M-EdjCase-ICP-Candid-Crypto-IHashFunction-ComputeHash-System-Byte[]- 'EdjCase.ICP.Candid.Crypto.IHashFunction.ComputeHash(System.Byte[])')
- [IObjectMapper](#T-EdjCase-ICP-Candid-Mapping-IObjectMapper 'EdjCase.ICP.Candid.Mapping.IObjectMapper')
  - [CandidType](#P-EdjCase-ICP-Candid-Mapping-IObjectMapper-CandidType 'EdjCase.ICP.Candid.Mapping.IObjectMapper.CandidType')
  - [Type](#P-EdjCase-ICP-Candid-Mapping-IObjectMapper-Type 'EdjCase.ICP.Candid.Mapping.IObjectMapper.Type')
  - [Map(value,options)](#M-EdjCase-ICP-Candid-Mapping-IObjectMapper-Map-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-CandidConverterOptions- 'EdjCase.ICP.Candid.Mapping.IObjectMapper.Map(EdjCase.ICP.Candid.Models.Values.CandidValue,EdjCase.ICP.Candid.CandidConverterOptions)')
  - [Map(value,options)](#M-EdjCase-ICP-Candid-Mapping-IObjectMapper-Map-System-Object,EdjCase-ICP-Candid-CandidConverterOptions- 'EdjCase.ICP.Candid.Mapping.IObjectMapper.Map(System.Object,EdjCase.ICP.Candid.CandidConverterOptions)')
- [InvalidCandidException](#T-EdjCase-ICP-Candid-Exceptions-InvalidCandidException 'EdjCase.ICP.Candid.Exceptions.InvalidCandidException')
  - [Message](#P-EdjCase-ICP-Candid-Exceptions-InvalidCandidException-Message 'EdjCase.ICP.Candid.Exceptions.InvalidCandidException.Message')
- [IsExternalInit](#T-System-Runtime-CompilerServices-IsExternalInit 'System.Runtime.CompilerServices.IsExternalInit')
- [LEB128](#T-EdjCase-ICP-Candid-Encodings-LEB128 'EdjCase.ICP.Candid.Encodings.LEB128')
  - [DecodeSigned(stream)](#M-EdjCase-ICP-Candid-Encodings-LEB128-DecodeSigned-System-IO-Stream- 'EdjCase.ICP.Candid.Encodings.LEB128.DecodeSigned(System.IO.Stream)')
  - [DecodeUnsigned(encodedValue)](#M-EdjCase-ICP-Candid-Encodings-LEB128-DecodeUnsigned-System-Byte[]- 'EdjCase.ICP.Candid.Encodings.LEB128.DecodeUnsigned(System.Byte[])')
  - [DecodeUnsigned(stream)](#M-EdjCase-ICP-Candid-Encodings-LEB128-DecodeUnsigned-System-IO-Stream- 'EdjCase.ICP.Candid.Encodings.LEB128.DecodeUnsigned(System.IO.Stream)')
  - [EncodeSigned(value)](#M-EdjCase-ICP-Candid-Encodings-LEB128-EncodeSigned-EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Candid.Encodings.LEB128.EncodeSigned(EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [EncodeUnsigned(value)](#M-EdjCase-ICP-Candid-Encodings-LEB128-EncodeUnsigned-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Candid.Encodings.LEB128.EncodeUnsigned(EdjCase.ICP.Candid.Models.UnboundedUInt)')
- [SHA256HashFunction](#T-EdjCase-ICP-Candid-Crypto-SHA256HashFunction 'EdjCase.ICP.Candid.Crypto.SHA256HashFunction')
  - [ComputeHash()](#M-EdjCase-ICP-Candid-Crypto-SHA256HashFunction-ComputeHash-System-Byte[]- 'EdjCase.ICP.Candid.Crypto.SHA256HashFunction.ComputeHash(System.Byte[])')
  - [Create()](#M-EdjCase-ICP-Candid-Crypto-SHA256HashFunction-Create 'EdjCase.ICP.Candid.Crypto.SHA256HashFunction.Create')
- [VariantAttribute](#T-EdjCase-ICP-Candid-Mapping-VariantAttribute 'EdjCase.ICP.Candid.Mapping.VariantAttribute')
  - [#ctor(enumType)](#M-EdjCase-ICP-Candid-Mapping-VariantAttribute-#ctor-System-Type- 'EdjCase.ICP.Candid.Mapping.VariantAttribute.#ctor(System.Type)')
  - [TagType](#P-EdjCase-ICP-Candid-Mapping-VariantAttribute-TagType 'EdjCase.ICP.Candid.Mapping.VariantAttribute.TagType')
- [VariantOptionTypeAttribute](#T-EdjCase-ICP-Candid-Mapping-VariantOptionTypeAttribute 'EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute')
  - [#ctor(optionType)](#M-EdjCase-ICP-Candid-Mapping-VariantOptionTypeAttribute-#ctor-System-Type- 'EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute.#ctor(System.Type)')
  - [OptionType](#P-EdjCase-ICP-Candid-Mapping-VariantOptionTypeAttribute-OptionType 'EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute.OptionType')
- [VariantTagPropertyAttribute](#T-EdjCase-ICP-Candid-Mapping-VariantTagPropertyAttribute 'EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute')
- [VariantValuePropertyAttribute](#T-EdjCase-ICP-Candid-Mapping-VariantValuePropertyAttribute 'EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute')

<a name='T-EdjCase-ICP-Candid-BinarySequence'></a>
## BinarySequence `type`

##### Namespace

EdjCase.ICP.Candid

<a name='M-EdjCase-ICP-Candid-BinarySequence-#ctor-System-Boolean[]-'></a>
### #ctor(bits) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| bits | [System.Boolean[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean[] 'System.Boolean[]') | Least signifcant to most ordered bits |

<a name='T-EdjCase-ICP-Candid-Crypto-CRC32'></a>
## CRC32 `type`

##### Namespace

EdjCase.ICP.Candid.Crypto

##### Summary

Helper class for computing CRC32 hashes/checksums on byte data
Useful for calculating checksums on data

<a name='M-EdjCase-ICP-Candid-Crypto-CRC32-ComputeHash-System-IO-Stream-'></a>
### ComputeHash(stream) `method`

##### Summary

Computes the 32-bit hash on the stream of data provided

##### Returns

Hash of the byte data as a byte array of length of 4

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stream | [System.IO.Stream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.Stream 'System.IO.Stream') | Byte data. Will use the whole stream |

<a name='M-EdjCase-ICP-Candid-Crypto-CRC32-ComputeHash-System-Byte[]-'></a>
### ComputeHash(data) `method`

##### Summary

Computes the 32-bit hash on the data bytes provided

##### Returns

Hash of the byte data as a byte array of length of 4

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Byte data |

<a name='T-EdjCase-ICP-Candid-Models-CandidArg'></a>
## CandidArg `type`

##### Namespace

EdjCase.ICP.Candid.Models

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction-'></a>
### ComputeHash() `method`

##### Summary

*Inherit from parent.*

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

<a name='M-EdjCase-ICP-Candid-Models-CandidArg-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

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

<a name='T-EdjCase-ICP-Candid-CandidArgBuilder'></a>
## CandidArgBuilder `type`

##### Namespace

EdjCase.ICP.Candid

<a name='F-EdjCase-ICP-Candid-CandidArgBuilder-EncodedTypes'></a>
### EncodedTypes `constants`

##### Summary

Ordered list of encoded types (encoded with SLEB128).
If SLEB value is positive, it is an index for \`EncodedCompoundTypes\` for a compound type
If SLEB value is negative, it is type code for a primitive value

<a name='F-EdjCase-ICP-Candid-CandidArgBuilder-EncodedValues'></a>
### EncodedValues `constants`

##### Summary

Ordered list of encoded values

<a name='F-EdjCase-ICP-Candid-CandidArgBuilder-compoundTypeTable'></a>
### compoundTypeTable `constants`

##### Summary

Helper to capture compound types

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

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidCompoundType-EncodeInnerTypes-EdjCase-ICP-Candid-Models-CompoundTypeTable-'></a>
### EncodeInnerTypes(compoundTypeTable) `method`

##### Summary

Adds all inner types to the compound table if applicable and returns its encoded type value

##### Returns

Byte array of encoded type

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| compoundTypeTable | [EdjCase.ICP.Candid.Models.CompoundTypeTable](#T-EdjCase-ICP-Candid-Models-CompoundTypeTable 'EdjCase.ICP.Candid.Models.CompoundTypeTable') | The collection of compound types for a candid arg |

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

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidFunc-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}-'></a>
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

<a name='M-EdjCase-ICP-Candid-Models-CandidId-CompareTo-EdjCase-ICP-Candid-Models-CandidId-'></a>
### CompareTo() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

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

<a name='P-EdjCase-ICP-Candid-Mapping-CandidNameAttribute-Name'></a>
### Name `property`

##### Summary

The name to use for serialization of candid values

<a name='T-EdjCase-ICP-Candid-Models-Values-CandidOptional'></a>
## CandidOptional `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidOptional-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidOptional-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}-'></a>
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

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-AsPrincipal'></a>
### AsPrincipal() `method`

##### Summary

If opaque, returns null, otherwise the principalid

##### Returns



##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidPrimitive-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}-'></a>
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

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidRecord-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidRecord-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}-'></a>
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

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidService-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidService-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}-'></a>
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

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidService-ToString'></a>
### ToString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

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

<a name='M-EdjCase-ICP-Candid-Models-Types-CandidType-Encode-EdjCase-ICP-Candid-Models-CompoundTypeTable-'></a>
### Encode(compoundTypeTable) `method`

##### Summary

Encodes this type into a byte array. If its a primitive type the value will
be an encoded negative number, if its a compound type it will be added to the 
type table and be a positive number of the table index where its info is stored

##### Returns

Byte array of the type number

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| compoundTypeTable | [EdjCase.ICP.Candid.Models.CompoundTypeTable](#T-EdjCase-ICP-Candid-Models-CompoundTypeTable 'EdjCase.ICP.Candid.Models.CompoundTypeTable') | The collection of compound types for a candid arg |

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

<a name='T-EdjCase-ICP-Candid-Models-CandidTypedValue'></a>
## CandidTypedValue `type`

##### Namespace

EdjCase.ICP.Candid.Models

<a name='M-EdjCase-ICP-Candid-Models-CandidTypedValue-AsPrincipal'></a>
### AsPrincipal() `method`

##### Summary

If opaque, returns null, otherwise the principalid

##### Returns



##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Candid-Models-Values-CandidValue'></a>
## CandidValue `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-AsPrincipal'></a>
### AsPrincipal() `method`

##### Summary

If opaque, returns null, otherwise the principalid

##### Returns



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

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidValue-GetHashCode'></a>
### GetHashCode() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

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

<a name='T-EdjCase-ICP-Candid-Models-Values-CandidVariant'></a>
## CandidVariant `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidVariant-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidVariant-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}-'></a>
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

<a name='P-EdjCase-ICP-Candid-Models-Types-CandidVariantType-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='T-EdjCase-ICP-Candid-Models-Values-CandidVector'></a>
## CandidVector `type`

##### Namespace

EdjCase.ICP.Candid.Models.Values

<a name='P-EdjCase-ICP-Candid-Models-Values-CandidVector-Type'></a>
### Type `property`

##### Summary

*Inherit from parent.*

<a name='M-EdjCase-ICP-Candid-Models-Values-CandidVector-EncodeValue-EdjCase-ICP-Candid-Models-Types-CandidType,System-Func{EdjCase-ICP-Candid-Models-CandidId,EdjCase-ICP-Candid-Models-Types-CandidCompoundType}-'></a>
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

<a name='T-EdjCase-ICP-Candid-Mapping-CustomMapperAttribute'></a>
## CustomMapperAttribute `type`

##### Namespace

EdjCase.ICP.Candid.Mapping

##### Summary

An attribute that specifies a custom mapper for the class, struct, property or field

<a name='M-EdjCase-ICP-Candid-Mapping-CustomMapperAttribute-#ctor-EdjCase-ICP-Candid-Mapping-IObjectMapper-'></a>
### #ctor(mapper) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| mapper | [EdjCase.ICP.Candid.Mapping.IObjectMapper](#T-EdjCase-ICP-Candid-Mapping-IObjectMapper 'EdjCase.ICP.Candid.Mapping.IObjectMapper') | The object mapper to use for the decorated item |

<a name='P-EdjCase-ICP-Candid-Mapping-CustomMapperAttribute-Mapper'></a>
### Mapper `property`

##### Summary

The object mapper to use for the decorated item

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

<a name='M-EdjCase-ICP-Candid-Models-HashTree-BuildRootHash'></a>
### BuildRootHash() `method`

##### Summary

Computes the root SHA256 hash of the tree based on the IC certificate spec

##### Returns

A blob of the hash digest

##### Parameters

This method has no parameters.

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

<a name='T-EdjCase-ICP-Candid-Mapping-IObjectMapper'></a>
## IObjectMapper `type`

##### Namespace

EdjCase.ICP.Candid.Mapping

##### Summary

A custom mapper interface to map a C# type to and from a candid type

<a name='P-EdjCase-ICP-Candid-Mapping-IObjectMapper-CandidType'></a>
### CandidType `property`

##### Summary

Candid type to convert to/from

<a name='P-EdjCase-ICP-Candid-Mapping-IObjectMapper-Type'></a>
### Type `property`

##### Summary

C# type to convert to/from

<a name='M-EdjCase-ICP-Candid-Mapping-IObjectMapper-Map-EdjCase-ICP-Candid-Models-Values-CandidValue,EdjCase-ICP-Candid-CandidConverterOptions-'></a>
### Map(value,options) `method`

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
| options | [EdjCase.ICP.Candid.CandidConverterOptions](#T-EdjCase-ICP-Candid-CandidConverterOptions 'EdjCase.ICP.Candid.CandidConverterOptions') | Options that are being used for the mappings |

<a name='M-EdjCase-ICP-Candid-Mapping-IObjectMapper-Map-System-Object,EdjCase-ICP-Candid-CandidConverterOptions-'></a>
### Map(value,options) `method`

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
| options | [EdjCase.ICP.Candid.CandidConverterOptions](#T-EdjCase-ICP-Candid-CandidConverterOptions 'EdjCase.ICP.Candid.CandidConverterOptions') | Options that are being used for the mappings |

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

<a name='M-EdjCase-ICP-Candid-Encodings-LEB128-DecodeUnsigned-System-Byte[]-'></a>
### DecodeUnsigned(encodedValue) `method`

##### Summary

Takes a byte encoded unsigned LEB128 and converts it to an \`UnboundedUInt\`

##### Returns

\`UnboundedUInt\` of LEB128 value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| encodedValue | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Byte value of an unsigned LEB128 |

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

<a name='T-EdjCase-ICP-Candid-Mapping-VariantAttribute'></a>
## VariantAttribute `type`

##### Namespace

EdjCase.ICP.Candid.Mapping

##### Summary

An attribute to put on a class to identify it as a variant type for serialization.
Requires the use of \`VariantTagPropertyAttribute\`, \`VariantOptionTypeAttribute\` and
\`VariantValuePropertyAttribute\` attributes if used

<a name='M-EdjCase-ICP-Candid-Mapping-VariantAttribute-#ctor-System-Type-'></a>
### #ctor(enumType) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| enumType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The enum type to use for specifying the tags of the variant |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentException 'System.ArgumentException') | Throws if the type is not an enum |

<a name='P-EdjCase-ICP-Candid-Mapping-VariantAttribute-TagType'></a>
### TagType `property`

##### Summary

The enum type to use for specifying the tags of the variant

<a name='T-EdjCase-ICP-Candid-Mapping-VariantOptionTypeAttribute'></a>
## VariantOptionTypeAttribute `type`

##### Namespace

EdjCase.ICP.Candid.Mapping

##### Summary

An attribute to put on an enum option to specify if the tag has an attached
value in the variant, otherwise the attached type will be null

<a name='M-EdjCase-ICP-Candid-Mapping-VariantOptionTypeAttribute-#ctor-System-Type-'></a>
### #ctor(optionType) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| optionType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The type of the variant option value to use |

<a name='P-EdjCase-ICP-Candid-Mapping-VariantOptionTypeAttribute-OptionType'></a>
### OptionType `property`

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
