<a name='assembly'></a>
# EdjCase.ICP.Agent

## Contents

- [Account](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-Account 'EdjCase.ICP.Agent.Standards.ICRC1.Models.Account')
  - [#ctor(owner,subaccount)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-Account-#ctor-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Candid-Models-OptionalValue{System-Collections-Generic-List{System-Byte}}- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.Account.#ctor(EdjCase.ICP.Candid.Models.Principal,EdjCase.ICP.Candid.Models.OptionalValue{System.Collections.Generic.List{System.Byte}})')
  - [Owner](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-Account-Owner 'EdjCase.ICP.Agent.Standards.ICRC1.Models.Account.Owner')
  - [Subaccount](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-Account-Subaccount 'EdjCase.ICP.Agent.Standards.ICRC1.Models.Account.Subaccount')
- [AlgorithmIdentifier](#T-EdjCase-ICP-Agent-AlgorithmIdentifier 'EdjCase.ICP.Agent.AlgorithmIdentifier')
  - [#ctor(algorithmOid,parametersOid)](#M-EdjCase-ICP-Agent-AlgorithmIdentifier-#ctor-System-String,System-String- 'EdjCase.ICP.Agent.AlgorithmIdentifier.#ctor(System.String,System.String)')
  - [AlgorithmOid](#P-EdjCase-ICP-Agent-AlgorithmIdentifier-AlgorithmOid 'EdjCase.ICP.Agent.AlgorithmIdentifier.AlgorithmOid')
  - [ParametersOid](#P-EdjCase-ICP-Agent-AlgorithmIdentifier-ParametersOid 'EdjCase.ICP.Agent.AlgorithmIdentifier.ParametersOid')
  - [Bls()](#M-EdjCase-ICP-Agent-AlgorithmIdentifier-Bls 'EdjCase.ICP.Agent.AlgorithmIdentifier.Bls')
  - [Ecdsa(curveOid)](#M-EdjCase-ICP-Agent-AlgorithmIdentifier-Ecdsa-System-String- 'EdjCase.ICP.Agent.AlgorithmIdentifier.Ecdsa(System.String)')
  - [Ed25519()](#M-EdjCase-ICP-Agent-AlgorithmIdentifier-Ed25519 'EdjCase.ICP.Agent.AlgorithmIdentifier.Ed25519')
  - [Secp256k1()](#M-EdjCase-ICP-Agent-AlgorithmIdentifier-Secp256k1 'EdjCase.ICP.Agent.AlgorithmIdentifier.Secp256k1')
- [BadBurnError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadBurnError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.BadBurnError')
  - [#ctor(minBurnAmount)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadBurnError-#ctor-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.BadBurnError.#ctor(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [MinBurnAmount](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadBurnError-MinBurnAmount 'EdjCase.ICP.Agent.Standards.ICRC1.Models.BadBurnError.MinBurnAmount')
- [BadFeeError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadFeeError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.BadFeeError')
  - [#ctor(expectedFee)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadFeeError-#ctor-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.BadFeeError.#ctor(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [ExpectedFee](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadFeeError-ExpectedFee 'EdjCase.ICP.Agent.Standards.ICRC1.Models.BadFeeError.ExpectedFee')
- [CallRejectedException](#T-EdjCase-ICP-Agent-CallRejectedException 'EdjCase.ICP.Agent.CallRejectedException')
  - [#ctor(rejectCode,rejectMessage,errorCode)](#M-EdjCase-ICP-Agent-CallRejectedException-#ctor-EdjCase-ICP-Agent-Responses-RejectCode,System-String,System-String- 'EdjCase.ICP.Agent.CallRejectedException.#ctor(EdjCase.ICP.Agent.Responses.RejectCode,System.String,System.String)')
  - [ErrorCode](#P-EdjCase-ICP-Agent-CallRejectedException-ErrorCode 'EdjCase.ICP.Agent.CallRejectedException.ErrorCode')
  - [Message](#P-EdjCase-ICP-Agent-CallRejectedException-Message 'EdjCase.ICP.Agent.CallRejectedException.Message')
  - [RejectCode](#P-EdjCase-ICP-Agent-CallRejectedException-RejectCode 'EdjCase.ICP.Agent.CallRejectedException.RejectCode')
  - [RejectMessage](#P-EdjCase-ICP-Agent-CallRejectedException-RejectMessage 'EdjCase.ICP.Agent.CallRejectedException.RejectMessage')
- [CallRejectedResponse](#T-EdjCase-ICP-Agent-Responses-CallRejectedResponse 'EdjCase.ICP.Agent.Responses.CallRejectedResponse')
  - [Code](#P-EdjCase-ICP-Agent-Responses-CallRejectedResponse-Code 'EdjCase.ICP.Agent.Responses.CallRejectedResponse.Code')
  - [ErrorCode](#P-EdjCase-ICP-Agent-Responses-CallRejectedResponse-ErrorCode 'EdjCase.ICP.Agent.Responses.CallRejectedResponse.ErrorCode')
  - [Message](#P-EdjCase-ICP-Agent-Responses-CallRejectedResponse-Message 'EdjCase.ICP.Agent.Responses.CallRejectedResponse.Message')
- [CallRequest](#T-EdjCase-ICP-Agent-Requests-CallRequest 'EdjCase.ICP.Agent.Requests.CallRequest')
  - [#ctor(canisterId,method,arg,sender,ingressExpiry,nonce)](#M-EdjCase-ICP-Agent-Requests-CallRequest-#ctor-EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Candid-Models-ICTimestamp,System-Byte[]- 'EdjCase.ICP.Agent.Requests.CallRequest.#ctor(EdjCase.ICP.Candid.Models.Principal,System.String,EdjCase.ICP.Candid.Models.CandidArg,EdjCase.ICP.Candid.Models.Principal,EdjCase.ICP.Candid.Models.ICTimestamp,System.Byte[])')
  - [Arg](#P-EdjCase-ICP-Agent-Requests-CallRequest-Arg 'EdjCase.ICP.Agent.Requests.CallRequest.Arg')
  - [CanisterId](#P-EdjCase-ICP-Agent-Requests-CallRequest-CanisterId 'EdjCase.ICP.Agent.Requests.CallRequest.CanisterId')
  - [IngressExpiry](#P-EdjCase-ICP-Agent-Requests-CallRequest-IngressExpiry 'EdjCase.ICP.Agent.Requests.CallRequest.IngressExpiry')
  - [Method](#P-EdjCase-ICP-Agent-Requests-CallRequest-Method 'EdjCase.ICP.Agent.Requests.CallRequest.Method')
  - [Nonce](#P-EdjCase-ICP-Agent-Requests-CallRequest-Nonce 'EdjCase.ICP.Agent.Requests.CallRequest.Nonce')
  - [Sender](#P-EdjCase-ICP-Agent-Requests-CallRequest-Sender 'EdjCase.ICP.Agent.Requests.CallRequest.Sender')
  - [BuildHashableItem()](#M-EdjCase-ICP-Agent-Requests-CallRequest-BuildHashableItem 'EdjCase.ICP.Agent.Requests.CallRequest.BuildHashableItem')
- [Certificate](#T-EdjCase-ICP-Agent-Models-Certificate 'EdjCase.ICP.Agent.Models.Certificate')
  - [#ctor(tree,signature,delegation)](#M-EdjCase-ICP-Agent-Models-Certificate-#ctor-EdjCase-ICP-Candid-Models-HashTree,System-Byte[],EdjCase-ICP-Agent-Models-CertificateDelegation- 'EdjCase.ICP.Agent.Models.Certificate.#ctor(EdjCase.ICP.Candid.Models.HashTree,System.Byte[],EdjCase.ICP.Agent.Models.CertificateDelegation)')
  - [Delegation](#P-EdjCase-ICP-Agent-Models-Certificate-Delegation 'EdjCase.ICP.Agent.Models.Certificate.Delegation')
  - [Signature](#P-EdjCase-ICP-Agent-Models-Certificate-Signature 'EdjCase.ICP.Agent.Models.Certificate.Signature')
  - [Tree](#P-EdjCase-ICP-Agent-Models-Certificate-Tree 'EdjCase.ICP.Agent.Models.Certificate.Tree')
  - [IsValid(rootPublicKey)](#M-EdjCase-ICP-Agent-Models-Certificate-IsValid-EdjCase-ICP-Agent-SubjectPublicKeyInfo- 'EdjCase.ICP.Agent.Models.Certificate.IsValid(EdjCase.ICP.Agent.SubjectPublicKeyInfo)')
- [CertificateDelegation](#T-EdjCase-ICP-Agent-Models-CertificateDelegation 'EdjCase.ICP.Agent.Models.CertificateDelegation')
  - [#ctor(subnetId,certificate)](#M-EdjCase-ICP-Agent-Models-CertificateDelegation-#ctor-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Agent-Models-Certificate- 'EdjCase.ICP.Agent.Models.CertificateDelegation.#ctor(EdjCase.ICP.Candid.Models.Principal,EdjCase.ICP.Agent.Models.Certificate)')
  - [Certificate](#P-EdjCase-ICP-Agent-Models-CertificateDelegation-Certificate 'EdjCase.ICP.Agent.Models.CertificateDelegation.Certificate')
  - [SubnetId](#P-EdjCase-ICP-Agent-Models-CertificateDelegation-SubnetId 'EdjCase.ICP.Agent.Models.CertificateDelegation.SubnetId')
  - [GetPublicKey()](#M-EdjCase-ICP-Agent-Models-CertificateDelegation-GetPublicKey 'EdjCase.ICP.Agent.Models.CertificateDelegation.GetPublicKey')
- [CreatedInFutureError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-CreatedInFutureError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.CreatedInFutureError')
  - [#ctor(ledgerTime)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-CreatedInFutureError-#ctor-System-UInt64- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.CreatedInFutureError.#ctor(System.UInt64)')
  - [LedgerTime](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-CreatedInFutureError-LedgerTime 'EdjCase.ICP.Agent.Standards.ICRC1.Models.CreatedInFutureError.LedgerTime')
- [DefaultHttpClient](#T-EdjCase-ICP-Agent-Agents-Http-DefaultHttpClient 'EdjCase.ICP.Agent.Agents.Http.DefaultHttpClient')
  - [#ctor(client)](#M-EdjCase-ICP-Agent-Agents-Http-DefaultHttpClient-#ctor-System-Net-Http-HttpClient- 'EdjCase.ICP.Agent.Agents.Http.DefaultHttpClient.#ctor(System.Net.Http.HttpClient)')
  - [GetAsync()](#M-EdjCase-ICP-Agent-Agents-Http-DefaultHttpClient-GetAsync-System-String- 'EdjCase.ICP.Agent.Agents.Http.DefaultHttpClient.GetAsync(System.String)')
  - [PostAsync()](#M-EdjCase-ICP-Agent-Agents-Http-DefaultHttpClient-PostAsync-System-String,System-Byte[]- 'EdjCase.ICP.Agent.Agents.Http.DefaultHttpClient.PostAsync(System.String,System.Byte[])')
- [Delegation](#T-EdjCase-ICP-Agent-Models-Delegation 'EdjCase.ICP.Agent.Models.Delegation')
  - [#ctor(publicKey,expiration,targets,senders)](#M-EdjCase-ICP-Agent-Models-Delegation-#ctor-EdjCase-ICP-Agent-SubjectPublicKeyInfo,EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}- 'EdjCase.ICP.Agent.Models.Delegation.#ctor(EdjCase.ICP.Agent.SubjectPublicKeyInfo,EdjCase.ICP.Candid.Models.ICTimestamp,System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal},System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal})')
  - [Expiration](#P-EdjCase-ICP-Agent-Models-Delegation-Expiration 'EdjCase.ICP.Agent.Models.Delegation.Expiration')
  - [PublicKey](#P-EdjCase-ICP-Agent-Models-Delegation-PublicKey 'EdjCase.ICP.Agent.Models.Delegation.PublicKey')
  - [Senders](#P-EdjCase-ICP-Agent-Models-Delegation-Senders 'EdjCase.ICP.Agent.Models.Delegation.Senders')
  - [Targets](#P-EdjCase-ICP-Agent-Models-Delegation-Targets 'EdjCase.ICP.Agent.Models.Delegation.Targets')
  - [BuildHashableItem()](#M-EdjCase-ICP-Agent-Models-Delegation-BuildHashableItem 'EdjCase.ICP.Agent.Models.Delegation.BuildHashableItem')
  - [BuildSigningChallenge()](#M-EdjCase-ICP-Agent-Models-Delegation-BuildSigningChallenge 'EdjCase.ICP.Agent.Models.Delegation.BuildSigningChallenge')
  - [ComputeHash()](#M-EdjCase-ICP-Agent-Models-Delegation-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction- 'EdjCase.ICP.Agent.Models.Delegation.ComputeHash(EdjCase.ICP.Candid.Crypto.IHashFunction)')
- [DelegationChain](#T-EdjCase-ICP-Agent-Models-DelegationChain 'EdjCase.ICP.Agent.Models.DelegationChain')
  - [#ctor(publicKey,delegations)](#M-EdjCase-ICP-Agent-Models-DelegationChain-#ctor-EdjCase-ICP-Agent-SubjectPublicKeyInfo,System-Collections-Generic-List{EdjCase-ICP-Agent-Models-SignedDelegation}- 'EdjCase.ICP.Agent.Models.DelegationChain.#ctor(EdjCase.ICP.Agent.SubjectPublicKeyInfo,System.Collections.Generic.List{EdjCase.ICP.Agent.Models.SignedDelegation})')
  - [Delegations](#P-EdjCase-ICP-Agent-Models-DelegationChain-Delegations 'EdjCase.ICP.Agent.Models.DelegationChain.Delegations')
  - [PublicKey](#P-EdjCase-ICP-Agent-Models-DelegationChain-PublicKey 'EdjCase.ICP.Agent.Models.DelegationChain.PublicKey')
  - [Create(keyToDelegateTo,delegatingIdentity,expiration,targets,senders)](#M-EdjCase-ICP-Agent-Models-DelegationChain-Create-EdjCase-ICP-Agent-SubjectPublicKeyInfo,EdjCase-ICP-Agent-Identities-IIdentity,EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}- 'EdjCase.ICP.Agent.Models.DelegationChain.Create(EdjCase.ICP.Agent.SubjectPublicKeyInfo,EdjCase.ICP.Agent.Identities.IIdentity,EdjCase.ICP.Candid.Models.ICTimestamp,System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal},System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal})')
- [DelegationIdentity](#T-EdjCase-ICP-Agent-Identities-DelegationIdentity 'EdjCase.ICP.Agent.Identities.DelegationIdentity')
  - [#ctor(identity,chain)](#M-EdjCase-ICP-Agent-Identities-DelegationIdentity-#ctor-EdjCase-ICP-Agent-Identities-IIdentity,EdjCase-ICP-Agent-Models-DelegationChain- 'EdjCase.ICP.Agent.Identities.DelegationIdentity.#ctor(EdjCase.ICP.Agent.Identities.IIdentity,EdjCase.ICP.Agent.Models.DelegationChain)')
  - [Chain](#P-EdjCase-ICP-Agent-Identities-DelegationIdentity-Chain 'EdjCase.ICP.Agent.Identities.DelegationIdentity.Chain')
  - [Identity](#P-EdjCase-ICP-Agent-Identities-DelegationIdentity-Identity 'EdjCase.ICP.Agent.Identities.DelegationIdentity.Identity')
  - [GetPublicKey()](#M-EdjCase-ICP-Agent-Identities-DelegationIdentity-GetPublicKey 'EdjCase.ICP.Agent.Identities.DelegationIdentity.GetPublicKey')
  - [GetSenderDelegations()](#M-EdjCase-ICP-Agent-Identities-DelegationIdentity-GetSenderDelegations 'EdjCase.ICP.Agent.Identities.DelegationIdentity.GetSenderDelegations')
  - [Sign()](#M-EdjCase-ICP-Agent-Identities-DelegationIdentity-Sign-System-Byte[]- 'EdjCase.ICP.Agent.Identities.DelegationIdentity.Sign(System.Byte[])')
- [DuplicateError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-DuplicateError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.DuplicateError')
  - [#ctor(duplicateOf)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-DuplicateError-#ctor-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.DuplicateError.#ctor(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [DuplicateOf](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-DuplicateError-DuplicateOf 'EdjCase.ICP.Agent.Standards.ICRC1.Models.DuplicateError.DuplicateOf')
- [EcdsaIdentity](#T-EdjCase-ICP-Agent-Identities-EcdsaIdentity 'EdjCase.ICP.Agent.Identities.EcdsaIdentity')
  - [#ctor(publicKey,privateKey,curveOid)](#M-EdjCase-ICP-Agent-Identities-EcdsaIdentity-#ctor-System-Byte[],System-Byte[],System-String- 'EdjCase.ICP.Agent.Identities.EcdsaIdentity.#ctor(System.Byte[],System.Byte[],System.String)')
  - [PrivateKey](#P-EdjCase-ICP-Agent-Identities-EcdsaIdentity-PrivateKey 'EdjCase.ICP.Agent.Identities.EcdsaIdentity.PrivateKey')
  - [PublicKey](#P-EdjCase-ICP-Agent-Identities-EcdsaIdentity-PublicKey 'EdjCase.ICP.Agent.Identities.EcdsaIdentity.PublicKey')
  - [DeriveUncompressedPublicKey(privateKey,curveOid)](#M-EdjCase-ICP-Agent-Identities-EcdsaIdentity-DeriveUncompressedPublicKey-System-Byte[],System-String- 'EdjCase.ICP.Agent.Identities.EcdsaIdentity.DeriveUncompressedPublicKey(System.Byte[],System.String)')
  - [GeneratePrivateKey(curveOid)](#M-EdjCase-ICP-Agent-Identities-EcdsaIdentity-GeneratePrivateKey-System-String- 'EdjCase.ICP.Agent.Identities.EcdsaIdentity.GeneratePrivateKey(System.String)')
  - [GetPublicKey()](#M-EdjCase-ICP-Agent-Identities-EcdsaIdentity-GetPublicKey 'EdjCase.ICP.Agent.Identities.EcdsaIdentity.GetPublicKey')
  - [GetSenderDelegations()](#M-EdjCase-ICP-Agent-Identities-EcdsaIdentity-GetSenderDelegations 'EdjCase.ICP.Agent.Identities.EcdsaIdentity.GetSenderDelegations')
  - [Sign()](#M-EdjCase-ICP-Agent-Identities-EcdsaIdentity-Sign-System-Byte[]- 'EdjCase.ICP.Agent.Identities.EcdsaIdentity.Sign(System.Byte[])')
- [Ed25519Identity](#T-EdjCase-ICP-Agent-Identities-Ed25519Identity 'EdjCase.ICP.Agent.Identities.Ed25519Identity')
  - [#ctor(publicKey,privateKey)](#M-EdjCase-ICP-Agent-Identities-Ed25519Identity-#ctor-System-Byte[],System-Byte[]- 'EdjCase.ICP.Agent.Identities.Ed25519Identity.#ctor(System.Byte[],System.Byte[])')
  - [PrivateKey](#P-EdjCase-ICP-Agent-Identities-Ed25519Identity-PrivateKey 'EdjCase.ICP.Agent.Identities.Ed25519Identity.PrivateKey')
  - [PublicKey](#P-EdjCase-ICP-Agent-Identities-Ed25519Identity-PublicKey 'EdjCase.ICP.Agent.Identities.Ed25519Identity.PublicKey')
  - [FromPrivateKey(privateKey)](#M-EdjCase-ICP-Agent-Identities-Ed25519Identity-FromPrivateKey-System-Byte[]- 'EdjCase.ICP.Agent.Identities.Ed25519Identity.FromPrivateKey(System.Byte[])')
  - [Generate()](#M-EdjCase-ICP-Agent-Identities-Ed25519Identity-Generate 'EdjCase.ICP.Agent.Identities.Ed25519Identity.Generate')
  - [GetPublicKey()](#M-EdjCase-ICP-Agent-Identities-Ed25519Identity-GetPublicKey 'EdjCase.ICP.Agent.Identities.Ed25519Identity.GetPublicKey')
  - [GetSenderDelegations()](#M-EdjCase-ICP-Agent-Identities-Ed25519Identity-GetSenderDelegations 'EdjCase.ICP.Agent.Identities.Ed25519Identity.GetSenderDelegations')
  - [Sign()](#M-EdjCase-ICP-Agent-Identities-Ed25519Identity-Sign-System-Byte[]- 'EdjCase.ICP.Agent.Identities.Ed25519Identity.Sign(System.Byte[])')
- [GenericError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-GenericError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.GenericError')
  - [#ctor(errorCode,message)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-GenericError-#ctor-EdjCase-ICP-Candid-Models-UnboundedUInt,System-String- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.GenericError.#ctor(EdjCase.ICP.Candid.Models.UnboundedUInt,System.String)')
  - [ErrorCode](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-GenericError-ErrorCode 'EdjCase.ICP.Agent.Standards.ICRC1.Models.GenericError.ErrorCode')
  - [Message](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-GenericError-Message 'EdjCase.ICP.Agent.Standards.ICRC1.Models.GenericError.Message')
- [HttpAgent](#T-EdjCase-ICP-Agent-Agents-HttpAgent 'EdjCase.ICP.Agent.Agents.HttpAgent')
  - [#ctor(identity,httpClient)](#M-EdjCase-ICP-Agent-Agents-HttpAgent-#ctor-EdjCase-ICP-Agent-Agents-Http-IHttpClient,EdjCase-ICP-Agent-Identities-IIdentity- 'EdjCase.ICP.Agent.Agents.HttpAgent.#ctor(EdjCase.ICP.Agent.Agents.Http.IHttpClient,EdjCase.ICP.Agent.Identities.IIdentity)')
  - [#ctor(identity,httpBoundryNodeUrl)](#M-EdjCase-ICP-Agent-Agents-HttpAgent-#ctor-EdjCase-ICP-Agent-Identities-IIdentity,System-Uri- 'EdjCase.ICP.Agent.Agents.HttpAgent.#ctor(EdjCase.ICP.Agent.Identities.IIdentity,System.Uri)')
  - [Identity](#P-EdjCase-ICP-Agent-Agents-HttpAgent-Identity 'EdjCase.ICP.Agent.Agents.HttpAgent.Identity')
  - [CallAsync()](#M-EdjCase-ICP-Agent-Agents-HttpAgent-CallAsync-EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-Principal- 'EdjCase.ICP.Agent.Agents.HttpAgent.CallAsync(EdjCase.ICP.Candid.Models.Principal,System.String,EdjCase.ICP.Candid.Models.CandidArg,EdjCase.ICP.Candid.Models.Principal)')
  - [GetReplicaStatusAsync()](#M-EdjCase-ICP-Agent-Agents-HttpAgent-GetReplicaStatusAsync 'EdjCase.ICP.Agent.Agents.HttpAgent.GetReplicaStatusAsync')
  - [GetRequestStatusAsync()](#M-EdjCase-ICP-Agent-Agents-HttpAgent-GetRequestStatusAsync-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Candid-Models-RequestId- 'EdjCase.ICP.Agent.Agents.HttpAgent.GetRequestStatusAsync(EdjCase.ICP.Candid.Models.Principal,EdjCase.ICP.Candid.Models.RequestId)')
  - [GetRootKeyAsync()](#M-EdjCase-ICP-Agent-Agents-HttpAgent-GetRootKeyAsync 'EdjCase.ICP.Agent.Agents.HttpAgent.GetRootKeyAsync')
  - [QueryAsync()](#M-EdjCase-ICP-Agent-Agents-HttpAgent-QueryAsync-EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg- 'EdjCase.ICP.Agent.Agents.HttpAgent.QueryAsync(EdjCase.ICP.Candid.Models.Principal,System.String,EdjCase.ICP.Candid.Models.CandidArg)')
  - [ReadStateAsync()](#M-EdjCase-ICP-Agent-Agents-HttpAgent-ReadStateAsync-EdjCase-ICP-Candid-Models-Principal,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-StatePath}- 'EdjCase.ICP.Agent.Agents.HttpAgent.ReadStateAsync(EdjCase.ICP.Candid.Models.Principal,System.Collections.Generic.List{EdjCase.ICP.Candid.Models.StatePath})')
- [HttpResponse](#T-EdjCase-ICP-Agent-Agents-Http-HttpResponse 'EdjCase.ICP.Agent.Agents.Http.HttpResponse')
  - [#ctor(statusCode,getContentFunc)](#M-EdjCase-ICP-Agent-Agents-Http-HttpResponse-#ctor-System-Net-HttpStatusCode,System-Func{System-Threading-Tasks-Task{System-Byte[]}}- 'EdjCase.ICP.Agent.Agents.Http.HttpResponse.#ctor(System.Net.HttpStatusCode,System.Func{System.Threading.Tasks.Task{System.Byte[]}})')
  - [StatusCode](#P-EdjCase-ICP-Agent-Agents-Http-HttpResponse-StatusCode 'EdjCase.ICP.Agent.Agents.Http.HttpResponse.StatusCode')
  - [GetContentAsync()](#M-EdjCase-ICP-Agent-Agents-Http-HttpResponse-GetContentAsync 'EdjCase.ICP.Agent.Agents.Http.HttpResponse.GetContentAsync')
  - [ThrowIfErrorAsync()](#M-EdjCase-ICP-Agent-Agents-Http-HttpResponse-ThrowIfErrorAsync 'EdjCase.ICP.Agent.Agents.Http.HttpResponse.ThrowIfErrorAsync')
- [IAgent](#T-EdjCase-ICP-Agent-Agents-IAgent 'EdjCase.ICP.Agent.Agents.IAgent')
  - [Identity](#P-EdjCase-ICP-Agent-Agents-IAgent-Identity 'EdjCase.ICP.Agent.Agents.IAgent.Identity')
  - [CallAsync(canisterId,method,arg,effectiveCanisterId)](#M-EdjCase-ICP-Agent-Agents-IAgent-CallAsync-EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-Principal- 'EdjCase.ICP.Agent.Agents.IAgent.CallAsync(EdjCase.ICP.Candid.Models.Principal,System.String,EdjCase.ICP.Candid.Models.CandidArg,EdjCase.ICP.Candid.Models.Principal)')
  - [GetReplicaStatusAsync()](#M-EdjCase-ICP-Agent-Agents-IAgent-GetReplicaStatusAsync 'EdjCase.ICP.Agent.Agents.IAgent.GetReplicaStatusAsync')
  - [GetRequestStatusAsync(canisterId,id)](#M-EdjCase-ICP-Agent-Agents-IAgent-GetRequestStatusAsync-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Candid-Models-RequestId- 'EdjCase.ICP.Agent.Agents.IAgent.GetRequestStatusAsync(EdjCase.ICP.Candid.Models.Principal,EdjCase.ICP.Candid.Models.RequestId)')
  - [GetRootKeyAsync()](#M-EdjCase-ICP-Agent-Agents-IAgent-GetRootKeyAsync 'EdjCase.ICP.Agent.Agents.IAgent.GetRootKeyAsync')
  - [QueryAsync(canisterId,method,arg)](#M-EdjCase-ICP-Agent-Agents-IAgent-QueryAsync-EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg- 'EdjCase.ICP.Agent.Agents.IAgent.QueryAsync(EdjCase.ICP.Candid.Models.Principal,System.String,EdjCase.ICP.Candid.Models.CandidArg)')
  - [ReadStateAsync(canisterId,paths)](#M-EdjCase-ICP-Agent-Agents-IAgent-ReadStateAsync-EdjCase-ICP-Candid-Models-Principal,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-StatePath}- 'EdjCase.ICP.Agent.Agents.IAgent.ReadStateAsync(EdjCase.ICP.Candid.Models.Principal,System.Collections.Generic.List{EdjCase.ICP.Candid.Models.StatePath})')
- [IAgentExtensions](#T-EdjCase-ICP-Agent-Agents-IAgentExtensions 'EdjCase.ICP.Agent.Agents.IAgentExtensions')
  - [CallAndWaitAsync(agent,canisterId,method,arg,effectiveCanisterId,cancellationToken)](#M-EdjCase-ICP-Agent-Agents-IAgentExtensions-CallAndWaitAsync-EdjCase-ICP-Agent-Agents-IAgent,EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-Principal,System-Nullable{System-Threading-CancellationToken}- 'EdjCase.ICP.Agent.Agents.IAgentExtensions.CallAndWaitAsync(EdjCase.ICP.Agent.Agents.IAgent,EdjCase.ICP.Candid.Models.Principal,System.String,EdjCase.ICP.Candid.Models.CandidArg,EdjCase.ICP.Candid.Models.Principal,System.Nullable{System.Threading.CancellationToken})')
- [ICRC1Client](#T-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client')
  - [#ctor(agent,canisterId)](#M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-#ctor-EdjCase-ICP-Agent-Agents-IAgent,EdjCase-ICP-Candid-Models-Principal- 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client.#ctor(EdjCase.ICP.Agent.Agents.IAgent,EdjCase.ICP.Candid.Models.Principal)')
  - [Agent](#P-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-Agent 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client.Agent')
  - [CanisterId](#P-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-CanisterId 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client.CanisterId')
  - [BalanceOf(account)](#M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-BalanceOf-EdjCase-ICP-Agent-Standards-ICRC1-Models-Account- 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client.BalanceOf(EdjCase.ICP.Agent.Standards.ICRC1.Models.Account)')
  - [Decimals()](#M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-Decimals 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client.Decimals')
  - [Fee()](#M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-Fee 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client.Fee')
  - [MetaData()](#M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-MetaData 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client.MetaData')
  - [MintingAccount()](#M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-MintingAccount 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client.MintingAccount')
  - [Name()](#M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-Name 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client.Name')
  - [SupportedStandards()](#M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-SupportedStandards 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client.SupportedStandards')
  - [Symbol()](#M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-Symbol 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client.Symbol')
  - [TotalSupply()](#M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-TotalSupply 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client.TotalSupply')
  - [Transfer(args)](#M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-Transfer-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs- 'EdjCase.ICP.Agent.Standards.ICRC1.ICRC1Client.Transfer(EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferArgs)')
- [IHttpClient](#T-EdjCase-ICP-Agent-Agents-Http-IHttpClient 'EdjCase.ICP.Agent.Agents.Http.IHttpClient')
  - [GetAsync(url)](#M-EdjCase-ICP-Agent-Agents-Http-IHttpClient-GetAsync-System-String- 'EdjCase.ICP.Agent.Agents.Http.IHttpClient.GetAsync(System.String)')
  - [PostAsync(url,cborBody)](#M-EdjCase-ICP-Agent-Agents-Http-IHttpClient-PostAsync-System-String,System-Byte[]- 'EdjCase.ICP.Agent.Agents.Http.IHttpClient.PostAsync(System.String,System.Byte[])')
- [IIdentity](#T-EdjCase-ICP-Agent-Identities-IIdentity 'EdjCase.ICP.Agent.Identities.IIdentity')
  - [GetPublicKey()](#M-EdjCase-ICP-Agent-Identities-IIdentity-GetPublicKey 'EdjCase.ICP.Agent.Identities.IIdentity.GetPublicKey')
  - [GetSenderDelegations()](#M-EdjCase-ICP-Agent-Identities-IIdentity-GetSenderDelegations 'EdjCase.ICP.Agent.Identities.IIdentity.GetSenderDelegations')
  - [Sign(data)](#M-EdjCase-ICP-Agent-Identities-IIdentity-Sign-System-Byte[]- 'EdjCase.ICP.Agent.Identities.IIdentity.Sign(System.Byte[])')
- [IIdentityExtensions](#T-EdjCase-ICP-Agent-Identities-IIdentityExtensions 'EdjCase.ICP.Agent.Identities.IIdentityExtensions')
  - [SignContent(identity,content)](#M-EdjCase-ICP-Agent-Identities-IIdentityExtensions-SignContent-EdjCase-ICP-Agent-Identities-IIdentity,System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-Candid-Models-IHashable}- 'EdjCase.ICP.Agent.Identities.IIdentityExtensions.SignContent(EdjCase.ICP.Agent.Identities.IIdentity,System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.IHashable})')
- [IdentityUtil](#T-EdjCase-ICP-Agent-Identities-IdentityUtil 'EdjCase.ICP.Agent.Identities.IdentityUtil')
  - [FromEd25519PrivateKey(privateKey)](#M-EdjCase-ICP-Agent-Identities-IdentityUtil-FromEd25519PrivateKey-System-Byte[]- 'EdjCase.ICP.Agent.Identities.IdentityUtil.FromEd25519PrivateKey(System.Byte[])')
  - [FromPemFile(pemFile)](#M-EdjCase-ICP-Agent-Identities-IdentityUtil-FromPemFile-System-IO-Stream- 'EdjCase.ICP.Agent.Identities.IdentityUtil.FromPemFile(System.IO.Stream)')
  - [FromPemFile(pemFileReader)](#M-EdjCase-ICP-Agent-Identities-IdentityUtil-FromPemFile-System-IO-TextReader- 'EdjCase.ICP.Agent.Identities.IdentityUtil.FromPemFile(System.IO.TextReader)')
  - [FromSecp256k1PrivateKey(privateKey)](#M-EdjCase-ICP-Agent-Identities-IdentityUtil-FromSecp256k1PrivateKey-System-Byte[]- 'EdjCase.ICP.Agent.Identities.IdentityUtil.FromSecp256k1PrivateKey(System.Byte[])')
  - [GenerateEd25519Identity()](#M-EdjCase-ICP-Agent-Identities-IdentityUtil-GenerateEd25519Identity 'EdjCase.ICP.Agent.Identities.IdentityUtil.GenerateEd25519Identity')
  - [GenerateSecp256k1Identity()](#M-EdjCase-ICP-Agent-Identities-IdentityUtil-GenerateSecp256k1Identity 'EdjCase.ICP.Agent.Identities.IdentityUtil.GenerateSecp256k1Identity')
- [InsufficientFundsError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-InsufficientFundsError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.InsufficientFundsError')
  - [#ctor(balance)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-InsufficientFundsError-#ctor-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.InsufficientFundsError.#ctor(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [Balance](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-InsufficientFundsError-Balance 'EdjCase.ICP.Agent.Standards.ICRC1.Models.InsufficientFundsError.Balance')
- [InvalidCertificateException](#T-EdjCase-ICP-Agent-InvalidCertificateException 'EdjCase.ICP.Agent.InvalidCertificateException')
  - [#ctor(message)](#M-EdjCase-ICP-Agent-InvalidCertificateException-#ctor-System-String- 'EdjCase.ICP.Agent.InvalidCertificateException.#ctor(System.String)')
- [InvalidPublicKey](#T-EdjCase-ICP-Agent-InvalidPublicKey 'EdjCase.ICP.Agent.InvalidPublicKey')
  - [#ctor()](#M-EdjCase-ICP-Agent-InvalidPublicKey-#ctor-System-Exception- 'EdjCase.ICP.Agent.InvalidPublicKey.#ctor(System.Exception)')
- [MetaData](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaData 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaData')
  - [#ctor(key,value)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaData-#ctor-System-String,EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaData.#ctor(System.String,EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue)')
  - [Key](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaData-Key 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaData.Key')
  - [Value](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaData-Value 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaData.Value')
- [MetaDataValue](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue')
  - [#ctor()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-#ctor 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue.#ctor')
  - [Tag](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-Tag 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue.Tag')
  - [Value](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-Value 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue.Value')
  - [AsBlob()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-AsBlob 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue.AsBlob')
  - [AsInt()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-AsInt 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue.AsInt')
  - [AsNat()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-AsNat 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue.AsNat')
  - [AsText()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-AsText 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue.AsText')
  - [Blob(value)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-Blob-System-Byte[]- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue.Blob(System.Byte[])')
  - [Int(value)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-Int-EdjCase-ICP-Candid-Models-UnboundedInt- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue.Int(EdjCase.ICP.Candid.Models.UnboundedInt)')
  - [Nat(value)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-Nat-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue.Nat(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [Text(value)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-Text-System-String- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue.Text(System.String)')
- [MetaDataValueTag](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValueTag 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValueTag')
  - [Blob](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValueTag-Blob 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValueTag.Blob')
  - [Int](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValueTag-Int 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValueTag.Int')
  - [Nat](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValueTag-Nat 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValueTag.Nat')
  - [Text](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValueTag-Text 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValueTag.Text')
- [NodeSignature](#T-EdjCase-ICP-Agent-Responses-NodeSignature 'EdjCase.ICP.Agent.Responses.NodeSignature')
  - [#ctor(timestamp,signature,identity)](#M-EdjCase-ICP-Agent-Responses-NodeSignature-#ctor-EdjCase-ICP-Candid-Models-ICTimestamp,System-Byte[],EdjCase-ICP-Candid-Models-Principal- 'EdjCase.ICP.Agent.Responses.NodeSignature.#ctor(EdjCase.ICP.Candid.Models.ICTimestamp,System.Byte[],EdjCase.ICP.Candid.Models.Principal)')
  - [Identity](#P-EdjCase-ICP-Agent-Responses-NodeSignature-Identity 'EdjCase.ICP.Agent.Responses.NodeSignature.Identity')
  - [Signature](#P-EdjCase-ICP-Agent-Responses-NodeSignature-Signature 'EdjCase.ICP.Agent.Responses.NodeSignature.Signature')
  - [Timestamp](#P-EdjCase-ICP-Agent-Responses-NodeSignature-Timestamp 'EdjCase.ICP.Agent.Responses.NodeSignature.Timestamp')
- [QueryRejectInfo](#T-EdjCase-ICP-Agent-Responses-QueryRejectInfo 'EdjCase.ICP.Agent.Responses.QueryRejectInfo')
  - [Code](#P-EdjCase-ICP-Agent-Responses-QueryRejectInfo-Code 'EdjCase.ICP.Agent.Responses.QueryRejectInfo.Code')
  - [ErrorCode](#P-EdjCase-ICP-Agent-Responses-QueryRejectInfo-ErrorCode 'EdjCase.ICP.Agent.Responses.QueryRejectInfo.ErrorCode')
  - [Message](#P-EdjCase-ICP-Agent-Responses-QueryRejectInfo-Message 'EdjCase.ICP.Agent.Responses.QueryRejectInfo.Message')
- [QueryRejectedException](#T-EdjCase-ICP-Agent-QueryRejectedException 'EdjCase.ICP.Agent.QueryRejectedException')
  - [#ctor(info)](#M-EdjCase-ICP-Agent-QueryRejectedException-#ctor-EdjCase-ICP-Agent-Responses-QueryRejectInfo- 'EdjCase.ICP.Agent.QueryRejectedException.#ctor(EdjCase.ICP.Agent.Responses.QueryRejectInfo)')
  - [Info](#P-EdjCase-ICP-Agent-QueryRejectedException-Info 'EdjCase.ICP.Agent.QueryRejectedException.Info')
  - [Message](#P-EdjCase-ICP-Agent-QueryRejectedException-Message 'EdjCase.ICP.Agent.QueryRejectedException.Message')
- [QueryRequest](#T-EdjCase-ICP-Agent-Requests-QueryRequest 'EdjCase.ICP.Agent.Requests.QueryRequest')
  - [#ctor(canisterId,method,arg,sender,ingressExpiry)](#M-EdjCase-ICP-Agent-Requests-QueryRequest-#ctor-EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Candid-Models-ICTimestamp- 'EdjCase.ICP.Agent.Requests.QueryRequest.#ctor(EdjCase.ICP.Candid.Models.Principal,System.String,EdjCase.ICP.Candid.Models.CandidArg,EdjCase.ICP.Candid.Models.Principal,EdjCase.ICP.Candid.Models.ICTimestamp)')
  - [Arg](#P-EdjCase-ICP-Agent-Requests-QueryRequest-Arg 'EdjCase.ICP.Agent.Requests.QueryRequest.Arg')
  - [CanisterId](#P-EdjCase-ICP-Agent-Requests-QueryRequest-CanisterId 'EdjCase.ICP.Agent.Requests.QueryRequest.CanisterId')
  - [IngressExpiry](#P-EdjCase-ICP-Agent-Requests-QueryRequest-IngressExpiry 'EdjCase.ICP.Agent.Requests.QueryRequest.IngressExpiry')
  - [Method](#P-EdjCase-ICP-Agent-Requests-QueryRequest-Method 'EdjCase.ICP.Agent.Requests.QueryRequest.Method')
  - [Nonce](#P-EdjCase-ICP-Agent-Requests-QueryRequest-Nonce 'EdjCase.ICP.Agent.Requests.QueryRequest.Nonce')
  - [RequestType](#P-EdjCase-ICP-Agent-Requests-QueryRequest-RequestType 'EdjCase.ICP.Agent.Requests.QueryRequest.RequestType')
  - [Sender](#P-EdjCase-ICP-Agent-Requests-QueryRequest-Sender 'EdjCase.ICP.Agent.Requests.QueryRequest.Sender')
  - [BuildHashableItem()](#M-EdjCase-ICP-Agent-Requests-QueryRequest-BuildHashableItem 'EdjCase.ICP.Agent.Requests.QueryRequest.BuildHashableItem')
- [QueryResponse](#T-EdjCase-ICP-Agent-Responses-QueryResponse 'EdjCase.ICP.Agent.Responses.QueryResponse')
  - [Signatures](#P-EdjCase-ICP-Agent-Responses-QueryResponse-Signatures 'EdjCase.ICP.Agent.Responses.QueryResponse.Signatures')
  - [Type](#P-EdjCase-ICP-Agent-Responses-QueryResponse-Type 'EdjCase.ICP.Agent.Responses.QueryResponse.Type')
  - [AsRejected()](#M-EdjCase-ICP-Agent-Responses-QueryResponse-AsRejected 'EdjCase.ICP.Agent.Responses.QueryResponse.AsRejected')
  - [AsReplied()](#M-EdjCase-ICP-Agent-Responses-QueryResponse-AsReplied 'EdjCase.ICP.Agent.Responses.QueryResponse.AsReplied')
  - [ThrowOrGetReply()](#M-EdjCase-ICP-Agent-Responses-QueryResponse-ThrowOrGetReply 'EdjCase.ICP.Agent.Responses.QueryResponse.ThrowOrGetReply')
- [QueryResponseType](#T-EdjCase-ICP-Agent-Responses-QueryResponseType 'EdjCase.ICP.Agent.Responses.QueryResponseType')
  - [Rejected](#F-EdjCase-ICP-Agent-Responses-QueryResponseType-Rejected 'EdjCase.ICP.Agent.Responses.QueryResponseType.Rejected')
  - [Replied](#F-EdjCase-ICP-Agent-Responses-QueryResponseType-Replied 'EdjCase.ICP.Agent.Responses.QueryResponseType.Replied')
- [ReadStateRequest](#T-EdjCase-ICP-Agent-Requests-ReadStateRequest 'EdjCase.ICP.Agent.Requests.ReadStateRequest')
  - [#ctor(paths,sender,ingressExpiry)](#M-EdjCase-ICP-Agent-Requests-ReadStateRequest-#ctor-System-Collections-Generic-List{EdjCase-ICP-Candid-Models-StatePath},EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Candid-Models-ICTimestamp- 'EdjCase.ICP.Agent.Requests.ReadStateRequest.#ctor(System.Collections.Generic.List{EdjCase.ICP.Candid.Models.StatePath},EdjCase.ICP.Candid.Models.Principal,EdjCase.ICP.Candid.Models.ICTimestamp)')
  - [IngressExpiry](#P-EdjCase-ICP-Agent-Requests-ReadStateRequest-IngressExpiry 'EdjCase.ICP.Agent.Requests.ReadStateRequest.IngressExpiry')
  - [Paths](#P-EdjCase-ICP-Agent-Requests-ReadStateRequest-Paths 'EdjCase.ICP.Agent.Requests.ReadStateRequest.Paths')
  - [REQUEST_TYPE](#P-EdjCase-ICP-Agent-Requests-ReadStateRequest-REQUEST_TYPE 'EdjCase.ICP.Agent.Requests.ReadStateRequest.REQUEST_TYPE')
  - [Sender](#P-EdjCase-ICP-Agent-Requests-ReadStateRequest-Sender 'EdjCase.ICP.Agent.Requests.ReadStateRequest.Sender')
  - [BuildHashableItem()](#M-EdjCase-ICP-Agent-Requests-ReadStateRequest-BuildHashableItem 'EdjCase.ICP.Agent.Requests.ReadStateRequest.BuildHashableItem')
- [ReadStateResponse](#T-EdjCase-ICP-Agent-Responses-ReadStateResponse 'EdjCase.ICP.Agent.Responses.ReadStateResponse')
  - [#ctor(certificate)](#M-EdjCase-ICP-Agent-Responses-ReadStateResponse-#ctor-EdjCase-ICP-Agent-Models-Certificate- 'EdjCase.ICP.Agent.Responses.ReadStateResponse.#ctor(EdjCase.ICP.Agent.Models.Certificate)')
  - [Certificate](#P-EdjCase-ICP-Agent-Responses-ReadStateResponse-Certificate 'EdjCase.ICP.Agent.Responses.ReadStateResponse.Certificate')
- [RejectCode](#T-EdjCase-ICP-Agent-Responses-RejectCode 'EdjCase.ICP.Agent.Responses.RejectCode')
  - [CanisterError](#F-EdjCase-ICP-Agent-Responses-RejectCode-CanisterError 'EdjCase.ICP.Agent.Responses.RejectCode.CanisterError')
  - [CanisterReject](#F-EdjCase-ICP-Agent-Responses-RejectCode-CanisterReject 'EdjCase.ICP.Agent.Responses.RejectCode.CanisterReject')
  - [DestinationInvalid](#F-EdjCase-ICP-Agent-Responses-RejectCode-DestinationInvalid 'EdjCase.ICP.Agent.Responses.RejectCode.DestinationInvalid')
  - [SysFatal](#F-EdjCase-ICP-Agent-Responses-RejectCode-SysFatal 'EdjCase.ICP.Agent.Responses.RejectCode.SysFatal')
  - [SysTransient](#F-EdjCase-ICP-Agent-Responses-RejectCode-SysTransient 'EdjCase.ICP.Agent.Responses.RejectCode.SysTransient')
- [RequestCleanedUpException](#T-EdjCase-ICP-Agent-RequestCleanedUpException 'EdjCase.ICP.Agent.RequestCleanedUpException')
  - [#ctor()](#M-EdjCase-ICP-Agent-RequestCleanedUpException-#ctor 'EdjCase.ICP.Agent.RequestCleanedUpException.#ctor')
- [RequestStatus](#T-EdjCase-ICP-Agent-Responses-RequestStatus 'EdjCase.ICP.Agent.Responses.RequestStatus')
  - [Type](#P-EdjCase-ICP-Agent-Responses-RequestStatus-Type 'EdjCase.ICP.Agent.Responses.RequestStatus.Type')
  - [AsRejected()](#M-EdjCase-ICP-Agent-Responses-RequestStatus-AsRejected 'EdjCase.ICP.Agent.Responses.RequestStatus.AsRejected')
  - [AsReplied()](#M-EdjCase-ICP-Agent-Responses-RequestStatus-AsReplied 'EdjCase.ICP.Agent.Responses.RequestStatus.AsReplied')
- [Secp256k1Identity](#T-EdjCase-ICP-Agent-Identities-Secp256k1Identity 'EdjCase.ICP.Agent.Identities.Secp256k1Identity')
  - [#ctor(publicKey,privateKey)](#M-EdjCase-ICP-Agent-Identities-Secp256k1Identity-#ctor-System-Byte[],System-Byte[]- 'EdjCase.ICP.Agent.Identities.Secp256k1Identity.#ctor(System.Byte[],System.Byte[])')
  - [FromPrivateKey(privateKey)](#M-EdjCase-ICP-Agent-Identities-Secp256k1Identity-FromPrivateKey-System-Byte[]- 'EdjCase.ICP.Agent.Identities.Secp256k1Identity.FromPrivateKey(System.Byte[])')
  - [Generate()](#M-EdjCase-ICP-Agent-Identities-Secp256k1Identity-Generate 'EdjCase.ICP.Agent.Identities.Secp256k1Identity.Generate')
- [SignedContent](#T-EdjCase-ICP-Agent-Models-SignedContent 'EdjCase.ICP.Agent.Models.SignedContent')
  - [#ctor(content,senderPublicKey,delegations,senderSignature)](#M-EdjCase-ICP-Agent-Models-SignedContent-#ctor-System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-Candid-Models-IHashable},EdjCase-ICP-Agent-SubjectPublicKeyInfo,System-Collections-Generic-List{EdjCase-ICP-Agent-Models-SignedDelegation},System-Byte[]- 'EdjCase.ICP.Agent.Models.SignedContent.#ctor(System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.IHashable},EdjCase.ICP.Agent.SubjectPublicKeyInfo,System.Collections.Generic.List{EdjCase.ICP.Agent.Models.SignedDelegation},System.Byte[])')
  - [Content](#P-EdjCase-ICP-Agent-Models-SignedContent-Content 'EdjCase.ICP.Agent.Models.SignedContent.Content')
  - [SenderDelegations](#P-EdjCase-ICP-Agent-Models-SignedContent-SenderDelegations 'EdjCase.ICP.Agent.Models.SignedContent.SenderDelegations')
  - [SenderPublicKey](#P-EdjCase-ICP-Agent-Models-SignedContent-SenderPublicKey 'EdjCase.ICP.Agent.Models.SignedContent.SenderPublicKey')
  - [SenderSignature](#P-EdjCase-ICP-Agent-Models-SignedContent-SenderSignature 'EdjCase.ICP.Agent.Models.SignedContent.SenderSignature')
  - [BuildHashableItem()](#M-EdjCase-ICP-Agent-Models-SignedContent-BuildHashableItem 'EdjCase.ICP.Agent.Models.SignedContent.BuildHashableItem')
- [SignedDelegation](#T-EdjCase-ICP-Agent-Models-SignedDelegation 'EdjCase.ICP.Agent.Models.SignedDelegation')
  - [#ctor(delegation,signature)](#M-EdjCase-ICP-Agent-Models-SignedDelegation-#ctor-EdjCase-ICP-Agent-Models-Delegation,System-Byte[]- 'EdjCase.ICP.Agent.Models.SignedDelegation.#ctor(EdjCase.ICP.Agent.Models.Delegation,System.Byte[])')
  - [Delegation](#P-EdjCase-ICP-Agent-Models-SignedDelegation-Delegation 'EdjCase.ICP.Agent.Models.SignedDelegation.Delegation')
  - [Signature](#P-EdjCase-ICP-Agent-Models-SignedDelegation-Signature 'EdjCase.ICP.Agent.Models.SignedDelegation.Signature')
  - [ComputeHash()](#M-EdjCase-ICP-Agent-Models-SignedDelegation-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction- 'EdjCase.ICP.Agent.Models.SignedDelegation.ComputeHash(EdjCase.ICP.Candid.Crypto.IHashFunction)')
  - [Create(keyToDelegateTo,delegatingIdentity,expiration,targets,senders)](#M-EdjCase-ICP-Agent-Models-SignedDelegation-Create-EdjCase-ICP-Agent-SubjectPublicKeyInfo,EdjCase-ICP-Agent-Identities-IIdentity,EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}- 'EdjCase.ICP.Agent.Models.SignedDelegation.Create(EdjCase.ICP.Agent.SubjectPublicKeyInfo,EdjCase.ICP.Agent.Identities.IIdentity,EdjCase.ICP.Candid.Models.ICTimestamp,System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal},System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal})')
  - [Create(keyToDelegateTo,signingFunc,expiration,targets,senders)](#M-EdjCase-ICP-Agent-Models-SignedDelegation-Create-EdjCase-ICP-Agent-SubjectPublicKeyInfo,System-Func{System-Byte[],System-Byte[]},EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}- 'EdjCase.ICP.Agent.Models.SignedDelegation.Create(EdjCase.ICP.Agent.SubjectPublicKeyInfo,System.Func{System.Byte[],System.Byte[]},EdjCase.ICP.Candid.Models.ICTimestamp,System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal},System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal})')
- [StatusResponse](#T-EdjCase-ICP-Agent-Responses-StatusResponse 'EdjCase.ICP.Agent.Responses.StatusResponse')
  - [#ctor(icApiVersion,implementationSource,implementationVersion,implementationRevision,developmentRootKey)](#M-EdjCase-ICP-Agent-Responses-StatusResponse-#ctor-System-String,System-String,System-String,System-String,System-Byte[]- 'EdjCase.ICP.Agent.Responses.StatusResponse.#ctor(System.String,System.String,System.String,System.String,System.Byte[])')
  - [DevelopmentRootKey](#P-EdjCase-ICP-Agent-Responses-StatusResponse-DevelopmentRootKey 'EdjCase.ICP.Agent.Responses.StatusResponse.DevelopmentRootKey')
  - [ICApiVersion](#P-EdjCase-ICP-Agent-Responses-StatusResponse-ICApiVersion 'EdjCase.ICP.Agent.Responses.StatusResponse.ICApiVersion')
  - [ImplementationRevision](#P-EdjCase-ICP-Agent-Responses-StatusResponse-ImplementationRevision 'EdjCase.ICP.Agent.Responses.StatusResponse.ImplementationRevision')
  - [ImplementationSource](#P-EdjCase-ICP-Agent-Responses-StatusResponse-ImplementationSource 'EdjCase.ICP.Agent.Responses.StatusResponse.ImplementationSource')
  - [ImplementationVersion](#P-EdjCase-ICP-Agent-Responses-StatusResponse-ImplementationVersion 'EdjCase.ICP.Agent.Responses.StatusResponse.ImplementationVersion')
- [StatusType](#T-EdjCase-ICP-Agent-Responses-RequestStatus-StatusType 'EdjCase.ICP.Agent.Responses.RequestStatus.StatusType')
  - [Done](#F-EdjCase-ICP-Agent-Responses-RequestStatus-StatusType-Done 'EdjCase.ICP.Agent.Responses.RequestStatus.StatusType.Done')
  - [Processing](#F-EdjCase-ICP-Agent-Responses-RequestStatus-StatusType-Processing 'EdjCase.ICP.Agent.Responses.RequestStatus.StatusType.Processing')
  - [Received](#F-EdjCase-ICP-Agent-Responses-RequestStatus-StatusType-Received 'EdjCase.ICP.Agent.Responses.RequestStatus.StatusType.Received')
  - [Rejected](#F-EdjCase-ICP-Agent-Responses-RequestStatus-StatusType-Rejected 'EdjCase.ICP.Agent.Responses.RequestStatus.StatusType.Rejected')
  - [Replied](#F-EdjCase-ICP-Agent-Responses-RequestStatus-StatusType-Replied 'EdjCase.ICP.Agent.Responses.RequestStatus.StatusType.Replied')
- [SubjectPublicKeyInfo](#T-EdjCase-ICP-Agent-SubjectPublicKeyInfo 'EdjCase.ICP.Agent.SubjectPublicKeyInfo')
  - [#ctor(algorithm,subjectPublicKey)](#M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-#ctor-EdjCase-ICP-Agent-AlgorithmIdentifier,System-Byte[]- 'EdjCase.ICP.Agent.SubjectPublicKeyInfo.#ctor(EdjCase.ICP.Agent.AlgorithmIdentifier,System.Byte[])')
  - [Algorithm](#P-EdjCase-ICP-Agent-SubjectPublicKeyInfo-Algorithm 'EdjCase.ICP.Agent.SubjectPublicKeyInfo.Algorithm')
  - [PublicKey](#P-EdjCase-ICP-Agent-SubjectPublicKeyInfo-PublicKey 'EdjCase.ICP.Agent.SubjectPublicKeyInfo.PublicKey')
  - [Bls(publicKey)](#M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-Bls-System-Byte[]- 'EdjCase.ICP.Agent.SubjectPublicKeyInfo.Bls(System.Byte[])')
  - [Ecdsa(publicKey,curveOid)](#M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-Ecdsa-System-Byte[],System-String- 'EdjCase.ICP.Agent.SubjectPublicKeyInfo.Ecdsa(System.Byte[],System.String)')
  - [Ed25519(publicKey)](#M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-Ed25519-System-Byte[]- 'EdjCase.ICP.Agent.SubjectPublicKeyInfo.Ed25519(System.Byte[])')
  - [FromDerEncoding(derEncodedPublicKey)](#M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-FromDerEncoding-System-Byte[]- 'EdjCase.ICP.Agent.SubjectPublicKeyInfo.FromDerEncoding(System.Byte[])')
  - [Secp256k1(publicKey)](#M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-Secp256k1-System-Byte[]- 'EdjCase.ICP.Agent.SubjectPublicKeyInfo.Secp256k1(System.Byte[])')
  - [ToDerEncoding()](#M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-ToDerEncoding 'EdjCase.ICP.Agent.SubjectPublicKeyInfo.ToDerEncoding')
  - [ToPrincipal()](#M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-ToPrincipal 'EdjCase.ICP.Agent.SubjectPublicKeyInfo.ToPrincipal')
- [SupportedStandard](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-SupportedStandard 'EdjCase.ICP.Agent.Standards.ICRC1.Models.SupportedStandard')
  - [#ctor(name,url)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-SupportedStandard-#ctor-System-String,System-String- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.SupportedStandard.#ctor(System.String,System.String)')
  - [Name](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-SupportedStandard-Name 'EdjCase.ICP.Agent.Standards.ICRC1.Models.SupportedStandard.Name')
  - [Url](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-SupportedStandard-Url 'EdjCase.ICP.Agent.Standards.ICRC1.Models.SupportedStandard.Url')
- [TransferArgs](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferArgs')
  - [#ctor(fromSubaccount,to,amount,fee,memo,createdAtTime)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-#ctor-EdjCase-ICP-Candid-Models-OptionalValue{System-Collections-Generic-List{System-Byte}},EdjCase-ICP-Agent-Standards-ICRC1-Models-Account,EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-OptionalValue{EdjCase-ICP-Candid-Models-UnboundedUInt},EdjCase-ICP-Candid-Models-OptionalValue{System-Collections-Generic-List{System-Byte}},EdjCase-ICP-Candid-Models-OptionalValue{System-UInt64}- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferArgs.#ctor(EdjCase.ICP.Candid.Models.OptionalValue{System.Collections.Generic.List{System.Byte}},EdjCase.ICP.Agent.Standards.ICRC1.Models.Account,EdjCase.ICP.Candid.Models.UnboundedUInt,EdjCase.ICP.Candid.Models.OptionalValue{EdjCase.ICP.Candid.Models.UnboundedUInt},EdjCase.ICP.Candid.Models.OptionalValue{System.Collections.Generic.List{System.Byte}},EdjCase.ICP.Candid.Models.OptionalValue{System.UInt64})')
  - [Amount](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-Amount 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferArgs.Amount')
  - [CreatedAtTime](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-CreatedAtTime 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferArgs.CreatedAtTime')
  - [Fee](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-Fee 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferArgs.Fee')
  - [FromSubaccount](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-FromSubaccount 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferArgs.FromSubaccount')
  - [Memo](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-Memo 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferArgs.Memo')
  - [To](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-To 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferArgs.To')
- [TransferError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError')
  - [#ctor()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-#ctor 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.#ctor')
  - [Tag](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-Tag 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.Tag')
  - [Value](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-Value 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.Value')
  - [AsBadBurn()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-AsBadBurn 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.AsBadBurn')
  - [AsBadFee()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-AsBadFee 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.AsBadFee')
  - [AsCreatedInFuture()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-AsCreatedInFuture 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.AsCreatedInFuture')
  - [AsDuplicate()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-AsDuplicate 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.AsDuplicate')
  - [AsGenericError()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-AsGenericError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.AsGenericError')
  - [AsInsufficientFunds()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-AsInsufficientFunds 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.AsInsufficientFunds')
  - [BadBurn(info)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-BadBurn-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadBurnError- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.BadBurn(EdjCase.ICP.Agent.Standards.ICRC1.Models.BadBurnError)')
  - [BadFee(info)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-BadFee-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadFeeError- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.BadFee(EdjCase.ICP.Agent.Standards.ICRC1.Models.BadFeeError)')
  - [CreatedInFuture(info)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-CreatedInFuture-EdjCase-ICP-Agent-Standards-ICRC1-Models-CreatedInFutureError- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.CreatedInFuture(EdjCase.ICP.Agent.Standards.ICRC1.Models.CreatedInFutureError)')
  - [Duplicate(info)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-Duplicate-EdjCase-ICP-Agent-Standards-ICRC1-Models-DuplicateError- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.Duplicate(EdjCase.ICP.Agent.Standards.ICRC1.Models.DuplicateError)')
  - [GenericError(info)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-GenericError-EdjCase-ICP-Agent-Standards-ICRC1-Models-GenericError- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.GenericError(EdjCase.ICP.Agent.Standards.ICRC1.Models.GenericError)')
  - [InsufficientFunds(info)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-InsufficientFunds-EdjCase-ICP-Agent-Standards-ICRC1-Models-InsufficientFundsError- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.InsufficientFunds(EdjCase.ICP.Agent.Standards.ICRC1.Models.InsufficientFundsError)')
  - [TemporarilyUnavailable()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-TemporarilyUnavailable 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.TemporarilyUnavailable')
  - [TooOld()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-TooOld 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.TooOld')
  - [ValidateTag(tag)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-ValidateTag-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError.ValidateTag(EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferErrorTag)')
- [TransferErrorTag](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferErrorTag')
  - [BadBurn](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-BadBurn 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferErrorTag.BadBurn')
  - [BadFee](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-BadFee 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferErrorTag.BadFee')
  - [CreatedInFuture](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-CreatedInFuture 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferErrorTag.CreatedInFuture')
  - [Duplicate](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-Duplicate 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferErrorTag.Duplicate')
  - [GenericError](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-GenericError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferErrorTag.GenericError')
  - [InsufficientFunds](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-InsufficientFunds 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferErrorTag.InsufficientFunds')
  - [TemporarilyUnavailable](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-TemporarilyUnavailable 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferErrorTag.TemporarilyUnavailable')
  - [TooOld](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-TooOld 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferErrorTag.TooOld')
- [TransferResult](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResult')
  - [#ctor()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-#ctor 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResult.#ctor')
  - [Tag](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-Tag 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResult.Tag')
  - [Value](#P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-Value 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResult.Value')
  - [AsErr()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-AsErr 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResult.AsErr')
  - [AsOk()](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-AsOk 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResult.AsOk')
  - [Err(info)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-Err-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResult.Err(EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError)')
  - [Ok(info)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-Ok-EdjCase-ICP-Candid-Models-UnboundedUInt- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResult.Ok(EdjCase.ICP.Candid.Models.UnboundedUInt)')
  - [ValidateTag(tag)](#M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-ValidateTag-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResultTag- 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResult.ValidateTag(EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResultTag)')
- [TransferResultTag](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResultTag 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResultTag')
  - [Err](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResultTag-Err 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResultTag.Err')
  - [Ok](#F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResultTag-Ok 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResultTag.Ok')

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-Account'></a>
## Account `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

This class represents an ICRC1 account

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-Account-#ctor-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Candid-Models-OptionalValue{System-Collections-Generic-List{System-Byte}}-'></a>
### #ctor(owner,subaccount) `constructor`

##### Summary

Primary constructor for the Account class

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| owner | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The owner of the account, represented as a Principal object |
| subaccount | [EdjCase.ICP.Candid.Models.OptionalValue{System.Collections.Generic.List{System.Byte}}](#T-EdjCase-ICP-Candid-Models-OptionalValue{System-Collections-Generic-List{System-Byte}} 'EdjCase.ICP.Candid.Models.OptionalValue{System.Collections.Generic.List{System.Byte}}') | The subaccount of the account, represented as an OptionalValue object |

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-Account-Owner'></a>
### Owner `property`

##### Summary

The owner of the account, represented as a Principal object

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-Account-Subaccount'></a>
### Subaccount `property`

##### Summary

The subaccount of the account, represented as an OptionalValue object

<a name='T-EdjCase-ICP-Agent-AlgorithmIdentifier'></a>
## AlgorithmIdentifier `type`

##### Namespace

EdjCase.ICP.Agent

##### Summary

A model to contain OID information for cryptographic algorithms and their curves.
Used in SubjectPublicKeyInfo models

<a name='M-EdjCase-ICP-Agent-AlgorithmIdentifier-#ctor-System-String,System-String-'></a>
### #ctor(algorithmOid,parametersOid) `constructor`

##### Summary

Default constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| algorithmOid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The OID of the algorithm |
| parametersOid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The OID of the parameters of the algorithm, such as a specific curve OID |

<a name='P-EdjCase-ICP-Agent-AlgorithmIdentifier-AlgorithmOid'></a>
### AlgorithmOid `property`

##### Summary

The OID of the algorithm

<a name='P-EdjCase-ICP-Agent-AlgorithmIdentifier-ParametersOid'></a>
### ParametersOid `property`

##### Summary

The OID of the parameters of the algorithm, such as a specific curve OID

<a name='M-EdjCase-ICP-Agent-AlgorithmIdentifier-Bls'></a>
### Bls() `method`

##### Summary

Helper method to create an \`AlgorithmIdentifier\` for Bls

##### Returns

AlgorithmIdentifier for Bls

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-AlgorithmIdentifier-Ecdsa-System-String-'></a>
### Ecdsa(curveOid) `method`

##### Summary

Helper method to create an \`AlgorithmIdentifier\` for Ecdsa

##### Returns

AlgorithmIdentifier for Ecdsa

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| curveOid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The OID of the specific curve to use for ECDSA |

<a name='M-EdjCase-ICP-Agent-AlgorithmIdentifier-Ed25519'></a>
### Ed25519() `method`

##### Summary

Helper method to create an \`AlgorithmIdentifier\` for Ed25519

##### Returns

AlgorithmIdentifier for Ed25519

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-AlgorithmIdentifier-Secp256k1'></a>
### Secp256k1() `method`

##### Summary

Helper method to create an \`AlgorithmIdentifier\` for Secp256k1

##### Returns

AlgorithmIdentifier for Secp256k1

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadBurnError'></a>
## BadBurnError `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

This class represents an error that occurs when a burn amount is incorrect

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadBurnError-#ctor-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### #ctor(minBurnAmount) `constructor`

##### Summary

Primary constructor for the BadBurnError class

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| minBurnAmount | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | The minimum burn amount for the transaction, represented as an UnboundedUInt object |

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadBurnError-MinBurnAmount'></a>
### MinBurnAmount `property`

##### Summary

The minimum burn amount for the transaction, represented as an UnboundedUInt object

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadFeeError'></a>
## BadFeeError `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

This class represents an error that occurs when a fee is incorrect

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadFeeError-#ctor-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### #ctor(expectedFee) `constructor`

##### Summary

Primary constructor for the BadFeeError class

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| expectedFee | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | The expected fee for the transaction, represented as an UnboundedUInt object |

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadFeeError-ExpectedFee'></a>
### ExpectedFee `property`

##### Summary

The expected fee for the transaction, represented as an UnboundedUInt object

<a name='T-EdjCase-ICP-Agent-CallRejectedException'></a>
## CallRejectedException `type`

##### Namespace

EdjCase.ICP.Agent

##### Summary

Exception for when a call to a canister is rejected/has an error

<a name='M-EdjCase-ICP-Agent-CallRejectedException-#ctor-EdjCase-ICP-Agent-Responses-RejectCode,System-String,System-String-'></a>
### #ctor(rejectCode,rejectMessage,errorCode) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| rejectCode | [EdjCase.ICP.Agent.Responses.RejectCode](#T-EdjCase-ICP-Agent-Responses-RejectCode 'EdjCase.ICP.Agent.Responses.RejectCode') | The type of rejection that occurred |
| rejectMessage | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The human readable message of the rejection error |
| errorCode | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional. Specific error code for differentiating specific errors |

<a name='P-EdjCase-ICP-Agent-CallRejectedException-ErrorCode'></a>
### ErrorCode `property`

##### Summary

Optional. Specific error code for differentiating specific errors

<a name='P-EdjCase-ICP-Agent-CallRejectedException-Message'></a>
### Message `property`

##### Summary

*Inherit from parent.*

<a name='P-EdjCase-ICP-Agent-CallRejectedException-RejectCode'></a>
### RejectCode `property`

##### Summary

The type of rejection that occurred

<a name='P-EdjCase-ICP-Agent-CallRejectedException-RejectMessage'></a>
### RejectMessage `property`

##### Summary

The human readable message of the rejection error

<a name='T-EdjCase-ICP-Agent-Responses-CallRejectedResponse'></a>
## CallRejectedResponse `type`

##### Namespace

EdjCase.ICP.Agent.Responses

<a name='P-EdjCase-ICP-Agent-Responses-CallRejectedResponse-Code'></a>
### Code `property`

##### Summary

The type of query reject

<a name='P-EdjCase-ICP-Agent-Responses-CallRejectedResponse-ErrorCode'></a>
### ErrorCode `property`

##### Summary

Optional. A specific error id for the reject

<a name='P-EdjCase-ICP-Agent-Responses-CallRejectedResponse-Message'></a>
### Message `property`

##### Summary

Optional. A human readable message about the rejection

<a name='T-EdjCase-ICP-Agent-Requests-CallRequest'></a>
## CallRequest `type`

##### Namespace

EdjCase.ICP.Agent.Requests

##### Summary

A model for making call requests to a canister

<a name='M-EdjCase-ICP-Agent-Requests-CallRequest-#ctor-EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Candid-Models-ICTimestamp,System-Byte[]-'></a>
### #ctor(canisterId,method,arg,sender,ingressExpiry,nonce) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| canisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The principal of the canister to call |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the canister method to call |
| arg | [EdjCase.ICP.Candid.Models.CandidArg](#T-EdjCase-ICP-Candid-Models-CandidArg 'EdjCase.ICP.Candid.Models.CandidArg') | Argument to pass to the canister method |
| sender | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The user who issued the request |
| ingressExpiry | [EdjCase.ICP.Candid.Models.ICTimestamp](#T-EdjCase-ICP-Candid-Models-ICTimestamp 'EdjCase.ICP.Candid.Models.ICTimestamp') | An upper limit on the validity of the request, expressed in nanoseconds since 1970-01-01 |
| nonce | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Optional. Arbitrary user-provided data, typically randomly generated. This can be used to create distinct requests with otherwise identical fields. |

<a name='P-EdjCase-ICP-Agent-Requests-CallRequest-Arg'></a>
### Arg `property`

##### Summary

Argument to pass to the canister method

<a name='P-EdjCase-ICP-Agent-Requests-CallRequest-CanisterId'></a>
### CanisterId `property`

##### Summary

The principal of the canister to call

<a name='P-EdjCase-ICP-Agent-Requests-CallRequest-IngressExpiry'></a>
### IngressExpiry `property`

##### Summary

An upper limit on the validity of the request, expressed in nanoseconds since 1970-01-01

<a name='P-EdjCase-ICP-Agent-Requests-CallRequest-Method'></a>
### Method `property`

##### Summary

Name of the canister method to call

<a name='P-EdjCase-ICP-Agent-Requests-CallRequest-Nonce'></a>
### Nonce `property`

##### Summary

Optional. Arbitrary user-provided data, typically randomly generated. This can be used to create distinct requests with otherwise identical fields.

<a name='P-EdjCase-ICP-Agent-Requests-CallRequest-Sender'></a>
### Sender `property`

##### Summary

The user who issued the request

<a name='M-EdjCase-ICP-Agent-Requests-CallRequest-BuildHashableItem'></a>
### BuildHashableItem() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Models-Certificate'></a>
## Certificate `type`

##### Namespace

EdjCase.ICP.Agent.Models

##### Summary

A model that contains a state tree along with a validation signature. If required
the model can have a delegation to allow for subnet data/keys

<a name='M-EdjCase-ICP-Agent-Models-Certificate-#ctor-EdjCase-ICP-Candid-Models-HashTree,System-Byte[],EdjCase-ICP-Agent-Models-CertificateDelegation-'></a>
### #ctor(tree,signature,delegation) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tree | [EdjCase.ICP.Candid.Models.HashTree](#T-EdjCase-ICP-Candid-Models-HashTree 'EdjCase.ICP.Candid.Models.HashTree') | A partial state tree of the requested state data |
| signature | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A signature on the tree root hash. Used to validate the tree |
| delegation | [EdjCase.ICP.Agent.Models.CertificateDelegation](#T-EdjCase-ICP-Agent-Models-CertificateDelegation 'EdjCase.ICP.Agent.Models.CertificateDelegation') | Optional. A signed delegation that links a public key to the root public key |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Throws if either \`tree\` or \`signature\` are null |

<a name='P-EdjCase-ICP-Agent-Models-Certificate-Delegation'></a>
### Delegation `property`

##### Summary

Optional. A signed delegation that links a public key to the root public key

<a name='P-EdjCase-ICP-Agent-Models-Certificate-Signature'></a>
### Signature `property`

##### Summary

A signature on the tree root hash. Used to validate the tree

<a name='P-EdjCase-ICP-Agent-Models-Certificate-Tree'></a>
### Tree `property`

##### Summary

A partial state tree of the requested state data

<a name='M-EdjCase-ICP-Agent-Models-Certificate-IsValid-EdjCase-ICP-Agent-SubjectPublicKeyInfo-'></a>
### IsValid(rootPublicKey) `method`

##### Summary

Checks the validity of the certificate based off the 
specified root public key

##### Returns

True if the certificate is valid, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| rootPublicKey | [EdjCase.ICP.Agent.SubjectPublicKeyInfo](#T-EdjCase-ICP-Agent-SubjectPublicKeyInfo 'EdjCase.ICP.Agent.SubjectPublicKeyInfo') | The root public key (DER encoded) of the internet computer network |

<a name='T-EdjCase-ICP-Agent-Models-CertificateDelegation'></a>
## CertificateDelegation `type`

##### Namespace

EdjCase.ICP.Agent.Models

##### Summary

A model that contains a certificate proving the delegation of a subnet for the subnet certificate

<a name='M-EdjCase-ICP-Agent-Models-CertificateDelegation-#ctor-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Agent-Models-Certificate-'></a>
### #ctor(subnetId,certificate) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| subnetId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The principal of the subnet being delegated to |
| certificate | [EdjCase.ICP.Agent.Models.Certificate](#T-EdjCase-ICP-Agent-Models-Certificate 'EdjCase.ICP.Agent.Models.Certificate') | The signed certificate that is signed by the delegator |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | Throws if either \`subnetId\` or \`certificate\` are null |

<a name='P-EdjCase-ICP-Agent-Models-CertificateDelegation-Certificate'></a>
### Certificate `property`

##### Summary

The signed certificate that is signed by the delegator

<a name='P-EdjCase-ICP-Agent-Models-CertificateDelegation-SubnetId'></a>
### SubnetId `property`

##### Summary

The principal of the subnet being delegated to

<a name='M-EdjCase-ICP-Agent-Models-CertificateDelegation-GetPublicKey'></a>
### GetPublicKey() `method`

##### Summary

Gets the public key value from the hash tree in the certificate

##### Returns

The delegation public key for the subnet

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if certificate is missing \`subnet/{subnet_id}/public_key\` |

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-CreatedInFutureError'></a>
## CreatedInFutureError `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

This class represents an error that occurs when a transaction is created in the future

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-CreatedInFutureError-#ctor-System-UInt64-'></a>
### #ctor(ledgerTime) `constructor`

##### Summary

Primary constructor for the CreatedInFutureError class

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ledgerTime | [System.UInt64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64 'System.UInt64') | The ledger time, represented as a Timestamp object |

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-CreatedInFutureError-LedgerTime'></a>
### LedgerTime `property`

##### Summary

The ledger time, represented as a Timestamp object

<a name='T-EdjCase-ICP-Agent-Agents-Http-DefaultHttpClient'></a>
## DefaultHttpClient `type`

##### Namespace

EdjCase.ICP.Agent.Agents.Http

##### Summary

The default http client to use with the built in \`HttpClient\`

<a name='M-EdjCase-ICP-Agent-Agents-Http-DefaultHttpClient-#ctor-System-Net-Http-HttpClient-'></a>
### #ctor(client) `constructor`

##### Summary

Default constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| client | [System.Net.Http.HttpClient](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpClient 'System.Net.Http.HttpClient') | HTTP client to use |

<a name='M-EdjCase-ICP-Agent-Agents-Http-DefaultHttpClient-GetAsync-System-String-'></a>
### GetAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Agents-Http-DefaultHttpClient-PostAsync-System-String,System-Byte[]-'></a>
### PostAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Models-Delegation'></a>
## Delegation `type`

##### Namespace

EdjCase.ICP.Agent.Models

##### Summary

A model that contains data on delegating authority from an identity

<a name='M-EdjCase-ICP-Agent-Models-Delegation-#ctor-EdjCase-ICP-Agent-SubjectPublicKeyInfo,EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}-'></a>
### #ctor(publicKey,expiration,targets,senders) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [EdjCase.ICP.Agent.SubjectPublicKeyInfo](#T-EdjCase-ICP-Agent-SubjectPublicKeyInfo 'EdjCase.ICP.Agent.SubjectPublicKeyInfo') | The public key from the authorizing identity |
| expiration | [EdjCase.ICP.Candid.Models.ICTimestamp](#T-EdjCase-ICP-Candid-Models-ICTimestamp 'EdjCase.ICP.Candid.Models.ICTimestamp') | The expiration when the delegation will no longer be valid |
| targets | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}') | Optional. A list of canister ids where the delegation can be sent to and be authorized |
| senders | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}') | Optional. A list of sender ids that can send this delegation and be authorized |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') |  |

<a name='P-EdjCase-ICP-Agent-Models-Delegation-Expiration'></a>
### Expiration `property`

##### Summary

The expiration when the delegation will no longer be valid

<a name='P-EdjCase-ICP-Agent-Models-Delegation-PublicKey'></a>
### PublicKey `property`

##### Summary

The public key from the authorizing identity

<a name='P-EdjCase-ICP-Agent-Models-Delegation-Senders'></a>
### Senders `property`

##### Summary

Optional. A list of sender ids that can send this delegation and be authorized

<a name='P-EdjCase-ICP-Agent-Models-Delegation-Targets'></a>
### Targets `property`

##### Summary

Optional. A list of canister ids where the delegation can be sent to and be authorized

<a name='M-EdjCase-ICP-Agent-Models-Delegation-BuildHashableItem'></a>
### BuildHashableItem() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Models-Delegation-BuildSigningChallenge'></a>
### BuildSigningChallenge() `method`

##### Summary

Creates a byte array of the data that can be signed by an algorithm for authorization/signature purposes

##### Returns

Byte array representation of the data

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Models-Delegation-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction-'></a>
### ComputeHash() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Models-DelegationChain'></a>
## DelegationChain `type`

##### Namespace

EdjCase.ICP.Agent.Models

##### Summary

A model containing a list of signed delegations to authorize an identity 
to act on behalf of the chain's public key

<a name='M-EdjCase-ICP-Agent-Models-DelegationChain-#ctor-EdjCase-ICP-Agent-SubjectPublicKeyInfo,System-Collections-Generic-List{EdjCase-ICP-Agent-Models-SignedDelegation}-'></a>
### #ctor(publicKey,delegations) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [EdjCase.ICP.Agent.SubjectPublicKeyInfo](#T-EdjCase-ICP-Agent-SubjectPublicKeyInfo 'EdjCase.ICP.Agent.SubjectPublicKeyInfo') | The public key of the identity that has delegated authority, DER encoded |
| delegations | [System.Collections.Generic.List{EdjCase.ICP.Agent.Models.SignedDelegation}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Agent.Models.SignedDelegation}') | The chain of delegations to authorize a key |

<a name='P-EdjCase-ICP-Agent-Models-DelegationChain-Delegations'></a>
### Delegations `property`

##### Summary

The chain of delegations to authorize a key
Each delegation is signed by its parent key
The first delegation's parent is the root key (\`PublicKey\` in \`DelegationChain\`)
The last delegation is for the key to be authorized

<a name='P-EdjCase-ICP-Agent-Models-DelegationChain-PublicKey'></a>
### PublicKey `property`

##### Summary

The public key of the identity that has delegated authority, DER encoded

<a name='M-EdjCase-ICP-Agent-Models-DelegationChain-Create-EdjCase-ICP-Agent-SubjectPublicKeyInfo,EdjCase-ICP-Agent-Identities-IIdentity,EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}-'></a>
### Create(keyToDelegateTo,delegatingIdentity,expiration,targets,senders) `method`

##### Summary

Creates a delegation chain from the specified keys

##### Returns

A delegation chain signed by the delegating identity

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keyToDelegateTo | [EdjCase.ICP.Agent.SubjectPublicKeyInfo](#T-EdjCase-ICP-Agent-SubjectPublicKeyInfo 'EdjCase.ICP.Agent.SubjectPublicKeyInfo') | The key to delegate authority to, DER encoded |
| delegatingIdentity | [EdjCase.ICP.Agent.Identities.IIdentity](#T-EdjCase-ICP-Agent-Identities-IIdentity 'EdjCase.ICP.Agent.Identities.IIdentity') | The identity that is signing the delegation |
| expiration | [EdjCase.ICP.Candid.Models.ICTimestamp](#T-EdjCase-ICP-Candid-Models-ICTimestamp 'EdjCase.ICP.Candid.Models.ICTimestamp') | How long to delegate for |
| targets | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}') | Optional. List of canister ids to limit delegation to |
| senders | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}') | Optional. List of pricipals where requests can originate from |

<a name='T-EdjCase-ICP-Agent-Identities-DelegationIdentity'></a>
## DelegationIdentity `type`

##### Namespace

EdjCase.ICP.Agent.Identities

##### Summary

An identity that has a signed delegation chain that authorizes
the identity to act as another identity

This is commonly used for things like Internet Identity where
a login session always generates a new key but that key has been 
signed by an authorized device through internet identity

<a name='M-EdjCase-ICP-Agent-Identities-DelegationIdentity-#ctor-EdjCase-ICP-Agent-Identities-IIdentity,EdjCase-ICP-Agent-Models-DelegationChain-'></a>
### #ctor(identity,chain) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identity | [EdjCase.ICP.Agent.Identities.IIdentity](#T-EdjCase-ICP-Agent-Identities-IIdentity 'EdjCase.ICP.Agent.Identities.IIdentity') | The identity that authorization has been delegated to |
| chain | [EdjCase.ICP.Agent.Models.DelegationChain](#T-EdjCase-ICP-Agent-Models-DelegationChain 'EdjCase.ICP.Agent.Models.DelegationChain') | The chain of singed delegations that prove authorization of the identity |

<a name='P-EdjCase-ICP-Agent-Identities-DelegationIdentity-Chain'></a>
### Chain `property`

##### Summary

The chain of singed delegations that prove authorization of the identity

<a name='P-EdjCase-ICP-Agent-Identities-DelegationIdentity-Identity'></a>
### Identity `property`

##### Summary

The identity that authorization has been delegated to

<a name='M-EdjCase-ICP-Agent-Identities-DelegationIdentity-GetPublicKey'></a>
### GetPublicKey() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Identities-DelegationIdentity-GetSenderDelegations'></a>
### GetSenderDelegations() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Identities-DelegationIdentity-Sign-System-Byte[]-'></a>
### Sign() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-DuplicateError'></a>
## DuplicateError `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

This class represents an error that occurs when a transaction is a duplicate

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-DuplicateError-#ctor-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### #ctor(duplicateOf) `constructor`

##### Summary

Primary constructor for the DuplicateError class

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| duplicateOf | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | The ID of the transaction that this transaction is a duplicate of, represented as an UnboundedUInt object |

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-DuplicateError-DuplicateOf'></a>
### DuplicateOf `property`

##### Summary

The ID of the transaction that this transaction is a duplicate

<a name='T-EdjCase-ICP-Agent-Identities-EcdsaIdentity'></a>
## EcdsaIdentity `type`

##### Namespace

EdjCase.ICP.Agent.Identities

##### Summary

An identity using a Ed25519 key

<a name='M-EdjCase-ICP-Agent-Identities-EcdsaIdentity-#ctor-System-Byte[],System-Byte[],System-String-'></a>
### #ctor(publicKey,privateKey,curveOid) `constructor`

##### Summary

Default constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The raw public key |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The raw private key |
| curveOid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The ecdsa curve OID to use |

<a name='P-EdjCase-ICP-Agent-Identities-EcdsaIdentity-PrivateKey'></a>
### PrivateKey `property`

##### Summary

The private key of the identity

<a name='P-EdjCase-ICP-Agent-Identities-EcdsaIdentity-PublicKey'></a>
### PublicKey `property`

##### Summary

The public key of the identity, DER encoded

<a name='M-EdjCase-ICP-Agent-Identities-EcdsaIdentity-DeriveUncompressedPublicKey-System-Byte[],System-String-'></a>
### DeriveUncompressedPublicKey(privateKey,curveOid) `method`

##### Summary

Derive the public key value from the private key and curve

##### Returns

The raw uncompressed public key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The raw private key |
| curveOid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The OID of the curve to use |

<a name='M-EdjCase-ICP-Agent-Identities-EcdsaIdentity-GeneratePrivateKey-System-String-'></a>
### GeneratePrivateKey(curveOid) `method`

##### Summary

Generates a new private key with the specified curve

##### Returns

A raw private key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| curveOid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The OID of the curve to use |

<a name='M-EdjCase-ICP-Agent-Identities-EcdsaIdentity-GetPublicKey'></a>
### GetPublicKey() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Identities-EcdsaIdentity-GetSenderDelegations'></a>
### GetSenderDelegations() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Identities-EcdsaIdentity-Sign-System-Byte[]-'></a>
### Sign() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Identities-Ed25519Identity'></a>
## Ed25519Identity `type`

##### Namespace

EdjCase.ICP.Agent.Identities

##### Summary

An identity using a Ed25519 key

<a name='M-EdjCase-ICP-Agent-Identities-Ed25519Identity-#ctor-System-Byte[],System-Byte[]-'></a>
### #ctor(publicKey,privateKey) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key of the identity |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key of the identity |

<a name='P-EdjCase-ICP-Agent-Identities-Ed25519Identity-PrivateKey'></a>
### PrivateKey `property`

##### Summary

The private key of the identity

<a name='P-EdjCase-ICP-Agent-Identities-Ed25519Identity-PublicKey'></a>
### PublicKey `property`

##### Summary

The public key of the identity

<a name='M-EdjCase-ICP-Agent-Identities-Ed25519Identity-FromPrivateKey-System-Byte[]-'></a>
### FromPrivateKey(privateKey) `method`

##### Summary

Converts a raw ed25519 private key to a Secp256k1Identity, deriving the public key

##### Returns

Ed25519Identity with specified private key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Raw ed25519 private key |

<a name='M-EdjCase-ICP-Agent-Identities-Ed25519Identity-Generate'></a>
### Generate() `method`

##### Summary

Generates an identity with a new Ed25519 key pair

##### Returns

A Ed25519 identity

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Identities-Ed25519Identity-GetPublicKey'></a>
### GetPublicKey() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Identities-Ed25519Identity-GetSenderDelegations'></a>
### GetSenderDelegations() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Identities-Ed25519Identity-Sign-System-Byte[]-'></a>
### Sign() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-GenericError'></a>
## GenericError `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

This class represents a generic error that can occur during a transaction

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-GenericError-#ctor-EdjCase-ICP-Candid-Models-UnboundedUInt,System-String-'></a>
### #ctor(errorCode,message) `constructor`

##### Summary

Primary constructor for the GenericError class

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| errorCode | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | The error code, represented as an UnboundedUInt object |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The error message, represented as a string |

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-GenericError-ErrorCode'></a>
### ErrorCode `property`

##### Summary

The error code, represented as an UnboundedUInt object

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-GenericError-Message'></a>
### Message `property`

##### Summary

The error message, represented as a string

<a name='T-EdjCase-ICP-Agent-Agents-HttpAgent'></a>
## HttpAgent `type`

##### Namespace

EdjCase.ICP.Agent.Agents

##### Summary

An \`IAgent\` implementation using HTTP to make requests to the IC

<a name='M-EdjCase-ICP-Agent-Agents-HttpAgent-#ctor-EdjCase-ICP-Agent-Agents-Http-IHttpClient,EdjCase-ICP-Agent-Identities-IIdentity-'></a>
### #ctor(identity,httpClient) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identity | [EdjCase.ICP.Agent.Agents.Http.IHttpClient](#T-EdjCase-ICP-Agent-Agents-Http-IHttpClient 'EdjCase.ICP.Agent.Agents.Http.IHttpClient') | Optional. Identity to use for each request. If unspecified, will use anonymous identity |
| httpClient | [EdjCase.ICP.Agent.Identities.IIdentity](#T-EdjCase-ICP-Agent-Identities-IIdentity 'EdjCase.ICP.Agent.Identities.IIdentity') | Optional. Sets the http client to use, otherwise will use the default http client |

<a name='M-EdjCase-ICP-Agent-Agents-HttpAgent-#ctor-EdjCase-ICP-Agent-Identities-IIdentity,System-Uri-'></a>
### #ctor(identity,httpBoundryNodeUrl) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identity | [EdjCase.ICP.Agent.Identities.IIdentity](#T-EdjCase-ICP-Agent-Identities-IIdentity 'EdjCase.ICP.Agent.Identities.IIdentity') | Optional. Identity to use for each request. If unspecified, will use anonymous identity |
| httpBoundryNodeUrl | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | Url to the boundry node to connect to. Defaults to \`https://ic0.app/\` |

<a name='P-EdjCase-ICP-Agent-Agents-HttpAgent-Identity'></a>
### Identity `property`

##### Summary

The identity that will be used on each request unless overriden
This identity can be anonymous

<a name='M-EdjCase-ICP-Agent-Agents-HttpAgent-CallAsync-EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-Principal-'></a>
### CallAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Agents-HttpAgent-GetReplicaStatusAsync'></a>
### GetReplicaStatusAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Agents-HttpAgent-GetRequestStatusAsync-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Candid-Models-RequestId-'></a>
### GetRequestStatusAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Agents-HttpAgent-GetRootKeyAsync'></a>
### GetRootKeyAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Agents-HttpAgent-QueryAsync-EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg-'></a>
### QueryAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Agents-HttpAgent-ReadStateAsync-EdjCase-ICP-Candid-Models-Principal,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-StatePath}-'></a>
### ReadStateAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Agents-Http-HttpResponse'></a>
## HttpResponse `type`

##### Namespace

EdjCase.ICP.Agent.Agents.Http

##### Summary

A model holding the HTTP response info

<a name='M-EdjCase-ICP-Agent-Agents-Http-HttpResponse-#ctor-System-Net-HttpStatusCode,System-Func{System-Threading-Tasks-Task{System-Byte[]}}-'></a>
### #ctor(statusCode,getContentFunc) `constructor`

##### Summary

Default constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| statusCode | [System.Net.HttpStatusCode](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.HttpStatusCode 'System.Net.HttpStatusCode') | The status code from the http response |
| getContentFunc | [System.Func{System.Threading.Tasks.Task{System.Byte[]}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Threading.Tasks.Task{System.Byte[]}}') | A func that returns the http response content |

<a name='P-EdjCase-ICP-Agent-Agents-Http-HttpResponse-StatusCode'></a>
### StatusCode `property`

##### Summary

The HTTP status code

<a name='M-EdjCase-ICP-Agent-Agents-Http-HttpResponse-GetContentAsync'></a>
### GetContentAsync() `method`

##### Summary

Returns the response content bytes

##### Returns

Content bytes

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Agents-Http-HttpResponse-ThrowIfErrorAsync'></a>
### ThrowIfErrorAsync() `method`

##### Summary

Throws an exception if the status code is not 200-299, otherwise does nothing

##### Returns



##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Agents-IAgent'></a>
## IAgent `type`

##### Namespace

EdjCase.ICP.Agent.Agents

##### Summary

An agent is used to communicate with the Internet Computer with certain protocols that 
are specific to an \`IAgent\` implementation

<a name='P-EdjCase-ICP-Agent-Agents-IAgent-Identity'></a>
### Identity `property`

##### Summary

The identity to use for requests. If null, then it will use the anonymous identity

<a name='M-EdjCase-ICP-Agent-Agents-IAgent-CallAsync-EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-Principal-'></a>
### CallAsync(canisterId,method,arg,effectiveCanisterId) `method`

##### Summary

Sends a call request to a specified canister method and gets back an id of the 
request that is being processed. This call does NOT wait for the request to be complete.
Either check the status with \`GetRequestStatusAsync\` or use the \`CallAndWaitAsync\` method

##### Returns

The id of the request that can be used to look up its status with \`GetRequestStatusAsync\`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| canisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | Canister to read state for |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the method to call on the cansiter |
| arg | [EdjCase.ICP.Candid.Models.CandidArg](#T-EdjCase-ICP-Candid-Models-CandidArg 'EdjCase.ICP.Candid.Models.CandidArg') | The candid arg to send with the request |
| effectiveCanisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | Optional. Specifies the relevant canister id if calling the root canister |

<a name='M-EdjCase-ICP-Agent-Agents-IAgent-GetReplicaStatusAsync'></a>
### GetReplicaStatusAsync() `method`

##### Summary

Gets the status of the IC replica. This includes versioning information
about the replica

##### Returns

A response containing all replica status information

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Agents-IAgent-GetRequestStatusAsync-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Candid-Models-RequestId-'></a>
### GetRequestStatusAsync(canisterId,id) `method`

##### Summary

Gets the status of a request that is being processed by the specified canister

##### Returns

A status variant of the request. If request is not found, will return null

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| canisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | Canister where the request was sent to |
| id | [EdjCase.ICP.Candid.Models.RequestId](#T-EdjCase-ICP-Candid-Models-RequestId 'EdjCase.ICP.Candid.Models.RequestId') | Id of the request to get a status for |

<a name='M-EdjCase-ICP-Agent-Agents-IAgent-GetRootKeyAsync'></a>
### GetRootKeyAsync() `method`

##### Summary

Gets the root public key of the current Internet Computer network

##### Returns

The root public key bytes

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Agents-IAgent-QueryAsync-EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg-'></a>
### QueryAsync(canisterId,method,arg) `method`

##### Summary

Sends a query request to a specified canister method

##### Returns

The response data of the query call

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| canisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | Canister to read state for |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the method to call on the cansiter |
| arg | [EdjCase.ICP.Candid.Models.CandidArg](#T-EdjCase-ICP-Candid-Models-CandidArg 'EdjCase.ICP.Candid.Models.CandidArg') | The candid arg to send with the request |

<a name='M-EdjCase-ICP-Agent-Agents-IAgent-ReadStateAsync-EdjCase-ICP-Candid-Models-Principal,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-StatePath}-'></a>
### ReadStateAsync(canisterId,paths) `method`

##### Summary

Gets the state of a specified canister with the subset of state information
specified by the paths parameter

##### Returns

A response that contains the certificate of the current cansiter state

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| canisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | Canister to read state for |
| paths | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.StatePath}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.StatePath}') | The state paths to get information for. Other state data will be pruned if not specified |

<a name='T-EdjCase-ICP-Agent-Agents-IAgentExtensions'></a>
## IAgentExtensions `type`

##### Namespace

EdjCase.ICP.Agent.Agents

##### Summary

Extension methods for the \`IAgent\` interface

<a name='M-EdjCase-ICP-Agent-Agents-IAgentExtensions-CallAndWaitAsync-EdjCase-ICP-Agent-Agents-IAgent,EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-Principal,System-Nullable{System-Threading-CancellationToken}-'></a>
### CallAndWaitAsync(agent,canisterId,method,arg,effectiveCanisterId,cancellationToken) `method`

##### Summary

Sends a call request to a specified canister method, waits for the request to be processed,
the returns the candid response to the call. This is helper method built on top of \`CallAsync\`
to wait for the response so it doesn't need to be implemented manually

##### Returns

The id of the request that can be used to look up its status with \`GetRequestStatusAsync\`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| agent | [EdjCase.ICP.Agent.Agents.IAgent](#T-EdjCase-ICP-Agent-Agents-IAgent 'EdjCase.ICP.Agent.Agents.IAgent') | The agent to use for the call |
| canisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | Canister to read state for |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the method to call on the cansiter |
| arg | [EdjCase.ICP.Candid.Models.CandidArg](#T-EdjCase-ICP-Candid-Models-CandidArg 'EdjCase.ICP.Candid.Models.CandidArg') | The candid arg to send with the request |
| effectiveCanisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | Optional. Specifies the relevant canister id if calling the root canister |
| cancellationToken | [System.Nullable{System.Threading.CancellationToken}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Threading.CancellationToken}') | Optional. If specified, will be used to prematurely end the waiting |

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client'></a>
## ICRC1Client `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1

##### Summary

A pre-generated client for the ICRC1 standard

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-#ctor-EdjCase-ICP-Agent-Agents-IAgent,EdjCase-ICP-Candid-Models-Principal-'></a>
### #ctor(agent,canisterId) `constructor`

##### Summary

Primary constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| agent | [EdjCase.ICP.Agent.Agents.IAgent](#T-EdjCase-ICP-Agent-Agents-IAgent 'EdjCase.ICP.Agent.Agents.IAgent') | Agent to use to make requests to the IC |
| canisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The id of the canister to make requests to |

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-Agent'></a>
### Agent `property`

##### Summary

Agent to use to make requests to the IC

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-CanisterId'></a>
### CanisterId `property`

##### Summary

The id of the canister to make requests to

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-BalanceOf-EdjCase-ICP-Agent-Standards-ICRC1-Models-Account-'></a>
### BalanceOf(account) `method`

##### Summary

Returns the balance of the account given as an argument.

##### Returns

The balance of the account given as an argument.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| account | [EdjCase.ICP.Agent.Standards.ICRC1.Models.Account](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-Account 'EdjCase.ICP.Agent.Standards.ICRC1.Models.Account') | Account to check balance for |

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-Decimals'></a>
### Decimals() `method`

##### Summary

Returns the number of decimals the token uses (e.g., 8 means to divide the token amount by 100000000 to get its user representation).

##### Returns

The number of decimals the token uses (e.g., 8 means to divide the token amount by 100000000 to get its user representation).

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-Fee'></a>
### Fee() `method`

##### Summary

Returns the default transfer fee.

##### Returns

The default transfer fee.

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-MetaData'></a>
### MetaData() `method`

##### Summary

Returns the list of metadata entries for this ledger. See the "Metadata" section below.

##### Returns



##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-MintingAccount'></a>
### MintingAccount() `method`

##### Summary

Returns the minting account if this ledger supports minting and burning tokens.

##### Returns

The minting account if this ledger supports minting and burning tokens.

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-Name'></a>
### Name() `method`

##### Summary

Returns the name of the token (e.g., MyToken).

##### Returns

The name of the token (e.g., MyToken).

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-SupportedStandards'></a>
### SupportedStandards() `method`

##### Summary

Returns the list of standards this ledger implements

##### Returns

The list of standards this ledger implements

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-Symbol'></a>
### Symbol() `method`

##### Summary

Returns the symbol of the token (e.g., ICP).

##### Returns

The symbol of the token (e.g., ICP).

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-TotalSupply'></a>
### TotalSupply() `method`

##### Summary

Returns the total number of tokens on all accounts except for the minting account.

##### Returns

The total number of tokens on all accounts except for the minting account.

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-ICRC1Client-Transfer-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-'></a>
### Transfer(args) `method`

##### Summary

Transfers amount of tokens from account record { of = caller; subaccount = from_subaccount } to the to account. The caller pays fee tokens for the transfer.

##### Returns

The result information from the transfer

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| args | [EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferArgs](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferArgs') | Arguments for the transfer |

<a name='T-EdjCase-ICP-Agent-Agents-Http-IHttpClient'></a>
## IHttpClient `type`

##### Namespace

EdjCase.ICP.Agent.Agents.Http

##### Summary

A simple http request interface for sending messages

<a name='M-EdjCase-ICP-Agent-Agents-Http-IHttpClient-GetAsync-System-String-'></a>
### GetAsync(url) `method`

##### Summary

Sends a GET http request and awaits the response

##### Returns

The http response

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| url | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The url to send the GET request to |

<a name='M-EdjCase-ICP-Agent-Agents-Http-IHttpClient-PostAsync-System-String,System-Byte[]-'></a>
### PostAsync(url,cborBody) `method`

##### Summary

Sends a POST http request and awaits a response

##### Returns

The http response

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| url | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The url to send the POST request to |
| cborBody | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The CBOR encoded body to send |

<a name='T-EdjCase-ICP-Agent-Identities-IIdentity'></a>
## IIdentity `type`

##### Namespace

EdjCase.ICP.Agent.Identities

##### Summary

Identity to use for requests to Internet Computer canisters

<a name='M-EdjCase-ICP-Agent-Identities-IIdentity-GetPublicKey'></a>
### GetPublicKey() `method`

##### Summary

Returns the public key of the identity

##### Returns

Public key of the identity

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Identities-IIdentity-GetSenderDelegations'></a>
### GetSenderDelegations() `method`

##### Summary

Gets the signed delegations for the identity.
Delegations will exist if the identity is a delegated identity
instead of having the raw keys. This is used in Internet Identity

##### Returns

The signed delegations, otherwise an empty list

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Identities-IIdentity-Sign-System-Byte[]-'></a>
### Sign(data) `method`

##### Summary

Signs the specified bytes with the identity key

##### Returns

The signature bytes of the specified data bytes

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The byte data to sign |

<a name='T-EdjCase-ICP-Agent-Identities-IIdentityExtensions'></a>
## IIdentityExtensions `type`

##### Namespace

EdjCase.ICP.Agent.Identities

##### Summary

Extension methods for the IIdentity interface

<a name='M-EdjCase-ICP-Agent-Identities-IIdentityExtensions-SignContent-EdjCase-ICP-Agent-Identities-IIdentity,System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-Candid-Models-IHashable}-'></a>
### SignContent(identity,content) `method`

##### Summary

Signs the hashable content

##### Returns

The content with signature(s) from the identity

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| identity | [EdjCase.ICP.Agent.Identities.IIdentity](#T-EdjCase-ICP-Agent-Identities-IIdentity 'EdjCase.ICP.Agent.Identities.IIdentity') | The identity to sign the content with |
| content | [System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.IHashable}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.IHashable}') | The data that needs to be signed |

<a name='T-EdjCase-ICP-Agent-Identities-IdentityUtil'></a>
## IdentityUtil `type`

##### Namespace

EdjCase.ICP.Agent.Identities

##### Summary

Utility class for helper methods around Identities

<a name='M-EdjCase-ICP-Agent-Identities-IdentityUtil-FromEd25519PrivateKey-System-Byte[]-'></a>
### FromEd25519PrivateKey(privateKey) `method`

##### Summary

Converts a raw private key into a Ed25519Identity class

##### Returns

Ed25519 identity

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A raw Ed25519 private key |

<a name='M-EdjCase-ICP-Agent-Identities-IdentityUtil-FromPemFile-System-IO-Stream-'></a>
### FromPemFile(pemFile) `method`

##### Summary

Parses a PEM file into the proper IIdentity class

##### Returns

IIdentity for the private key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| pemFile | [System.IO.Stream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.Stream 'System.IO.Stream') | The stream of a PEM file |

<a name='M-EdjCase-ICP-Agent-Identities-IdentityUtil-FromPemFile-System-IO-TextReader-'></a>
### FromPemFile(pemFileReader) `method`

##### Summary

Parses a PEM file into the proper IIdentity class

##### Returns

IIdentity for the private key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| pemFileReader | [System.IO.TextReader](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.TextReader 'System.IO.TextReader') | The text reader of a PEM file |

<a name='M-EdjCase-ICP-Agent-Identities-IdentityUtil-FromSecp256k1PrivateKey-System-Byte[]-'></a>
### FromSecp256k1PrivateKey(privateKey) `method`

##### Summary

Converts a raw private key into a Secp256k1Identity class

##### Returns

Secp256k1 identity

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A raw Secp256k1 private key |

<a name='M-EdjCase-ICP-Agent-Identities-IdentityUtil-GenerateEd25519Identity'></a>
### GenerateEd25519Identity() `method`

##### Summary

Generates a new Ed25519 identity with a new private key

##### Returns

Ed25519 identity

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Identities-IdentityUtil-GenerateSecp256k1Identity'></a>
### GenerateSecp256k1Identity() `method`

##### Summary

Generates a new Secp256k1 identity with a new private key

##### Returns

Secp256k1 identity

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-InsufficientFundsError'></a>
## InsufficientFundsError `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

This class represents an error that occurs when there are insufficient funds for a transaction

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-InsufficientFundsError-#ctor-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### #ctor(balance) `constructor`

##### Summary

Primary constructor for the InsufficientFundsError class

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| balance | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | The balance of the account, represented as an UnboundedUInt object |

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-InsufficientFundsError-Balance'></a>
### Balance `property`

##### Summary

The balance of the account, represented as an UnboundedUInt object

<a name='T-EdjCase-ICP-Agent-InvalidCertificateException'></a>
## InvalidCertificateException `type`

##### Namespace

EdjCase.ICP.Agent

##### Summary

Exception to indicate that the certificate is invalid

<a name='M-EdjCase-ICP-Agent-InvalidCertificateException-#ctor-System-String-'></a>
### #ctor(message) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Specific error message |

<a name='T-EdjCase-ICP-Agent-InvalidPublicKey'></a>
## InvalidPublicKey `type`

##### Namespace

EdjCase.ICP.Agent

##### Summary

Exception to indicate that the specified public key is invalid

<a name='M-EdjCase-ICP-Agent-InvalidPublicKey-#ctor-System-Exception-'></a>
### #ctor() `constructor`

##### Summary



##### Parameters

This constructor has no parameters.

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaData'></a>
## MetaData `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

A model representing metadata from an icrc1 token

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaData-#ctor-System-String,EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-'></a>
### #ctor(key,value) `constructor`

##### Summary

Primary constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key or name of the metadata value |
| value | [EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue 'EdjCase.ICP.Agent.Standards.ICRC1.Models.MetaDataValue') | The associated value for the metadata key |

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaData-Key'></a>
### Key `property`

##### Summary

The key or name of the metadata value

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaData-Value'></a>
### Value `property`

##### Summary

The associated value for the metadata key

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue'></a>
## MetaDataValue `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

A model representing the metadata value from an icrc1 token

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-#ctor'></a>
### #ctor() `constructor`

##### Summary

Constructor for reflection

##### Parameters

This constructor has no parameters.

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-Tag'></a>
### Tag `property`

##### Summary

The metadata variant option tag/type

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-Value'></a>
### Value `property`

##### Summary

The metadata variant option raw value

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-AsBlob'></a>
### AsBlob() `method`

##### Summary

Gets the Blob value from the metadata. If the variant is not a Blob, will throw an error

##### Returns

The Blob value of the metadata

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-AsInt'></a>
### AsInt() `method`

##### Summary

Gets the Int value from the metadata. If the variant is not a Int, will throw an error

##### Returns

The Int value of the metadata

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-AsNat'></a>
### AsNat() `method`

##### Summary

Gets the Nat value from the metadata. If the variant is not a Nat, will throw an error

##### Returns

The Nat value of the metadata

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-AsText'></a>
### AsText() `method`

##### Summary

Gets the Text value from the metadata. If the variant is not a Text, will throw an error

##### Returns

The Text value of the metadata

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-Blob-System-Byte[]-'></a>
### Blob(value) `method`

##### Summary

Constructs a metadata value with a Blob

##### Returns

A metadata value with a Blob

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The Blob value to use |

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-Int-EdjCase-ICP-Candid-Models-UnboundedInt-'></a>
### Int(value) `method`

##### Summary

Constructs a metadata value with a Int

##### Returns

A metadata value with a Int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedInt](#T-EdjCase-ICP-Candid-Models-UnboundedInt 'EdjCase.ICP.Candid.Models.UnboundedInt') | The Int value to use |

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-Nat-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### Nat(value) `method`

##### Summary

Constructs a metadata value with a Nat

##### Returns

A metadata value with a Nat

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | The Nat value to use |

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValue-Text-System-String-'></a>
### Text(value) `method`

##### Summary

Constructs a metadata value with a Text

##### Returns

A metadata value with a Text

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Text value to use |

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValueTag'></a>
## MetaDataValueTag `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

An enum representing the meta data value types

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValueTag-Blob'></a>
### Blob `constants`

##### Summary

Blob value

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValueTag-Int'></a>
### Int `constants`

##### Summary

Int value

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValueTag-Nat'></a>
### Nat `constants`

##### Summary

Nat value

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-MetaDataValueTag-Text'></a>
### Text `constants`

##### Summary

Text value

<a name='T-EdjCase-ICP-Agent-Responses-NodeSignature'></a>
## NodeSignature `type`

##### Namespace

EdjCase.ICP.Agent.Responses

##### Summary

Signature data from a replica node

<a name='M-EdjCase-ICP-Agent-Responses-NodeSignature-#ctor-EdjCase-ICP-Candid-Models-ICTimestamp,System-Byte[],EdjCase-ICP-Candid-Models-Principal-'></a>
### #ctor(timestamp,signature,identity) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| timestamp | [EdjCase.ICP.Candid.Models.ICTimestamp](#T-EdjCase-ICP-Candid-Models-ICTimestamp 'EdjCase.ICP.Candid.Models.ICTimestamp') | Timestamp when the signature was created |
| signature | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The signature bytes |
| identity | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The identity of the signer |

<a name='P-EdjCase-ICP-Agent-Responses-NodeSignature-Identity'></a>
### Identity `property`

##### Summary

The identity of the signer

<a name='P-EdjCase-ICP-Agent-Responses-NodeSignature-Signature'></a>
### Signature `property`

##### Summary

The signature bytes

<a name='P-EdjCase-ICP-Agent-Responses-NodeSignature-Timestamp'></a>
### Timestamp `property`

##### Summary

Timestamp when the signature was created

<a name='T-EdjCase-ICP-Agent-Responses-QueryRejectInfo'></a>
## QueryRejectInfo `type`

##### Namespace

EdjCase.ICP.Agent.Responses

##### Summary

Data from a query response that has been rejected

<a name='P-EdjCase-ICP-Agent-Responses-QueryRejectInfo-Code'></a>
### Code `property`

##### Summary

The type of query reject

<a name='P-EdjCase-ICP-Agent-Responses-QueryRejectInfo-ErrorCode'></a>
### ErrorCode `property`

##### Summary

Optional. A specific error id for the reject

<a name='P-EdjCase-ICP-Agent-Responses-QueryRejectInfo-Message'></a>
### Message `property`

##### Summary

Optional. A human readable message about the rejection

<a name='T-EdjCase-ICP-Agent-QueryRejectedException'></a>
## QueryRejectedException `type`

##### Namespace

EdjCase.ICP.Agent

##### Summary

Exception for when a query to a canister is rejected/has an error

<a name='M-EdjCase-ICP-Agent-QueryRejectedException-#ctor-EdjCase-ICP-Agent-Responses-QueryRejectInfo-'></a>
### #ctor(info) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| info | [EdjCase.ICP.Agent.Responses.QueryRejectInfo](#T-EdjCase-ICP-Agent-Responses-QueryRejectInfo 'EdjCase.ICP.Agent.Responses.QueryRejectInfo') | The type of rejection that occurred |

<a name='P-EdjCase-ICP-Agent-QueryRejectedException-Info'></a>
### Info `property`

##### Summary

The details of the rejection

<a name='P-EdjCase-ICP-Agent-QueryRejectedException-Message'></a>
### Message `property`

##### Summary

*Inherit from parent.*

<a name='T-EdjCase-ICP-Agent-Requests-QueryRequest'></a>
## QueryRequest `type`

##### Namespace

EdjCase.ICP.Agent.Requests

##### Summary

A model for making query requests to a canister

<a name='M-EdjCase-ICP-Agent-Requests-QueryRequest-#ctor-EdjCase-ICP-Candid-Models-Principal,System-String,EdjCase-ICP-Candid-Models-CandidArg,EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Candid-Models-ICTimestamp-'></a>
### #ctor(canisterId,method,arg,sender,ingressExpiry) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| canisterId | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The principal of the canister to call |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Name of the canister method to call |
| arg | [EdjCase.ICP.Candid.Models.CandidArg](#T-EdjCase-ICP-Candid-Models-CandidArg 'EdjCase.ICP.Candid.Models.CandidArg') | Arguments to pass to the canister method |
| sender | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The user who issued the request |
| ingressExpiry | [EdjCase.ICP.Candid.Models.ICTimestamp](#T-EdjCase-ICP-Candid-Models-ICTimestamp 'EdjCase.ICP.Candid.Models.ICTimestamp') | The expiration of the request to avoid replay attacks |

<a name='P-EdjCase-ICP-Agent-Requests-QueryRequest-Arg'></a>
### Arg `property`

##### Summary

Arguments to pass to the canister method

<a name='P-EdjCase-ICP-Agent-Requests-QueryRequest-CanisterId'></a>
### CanisterId `property`

##### Summary

The principal of the canister to call

<a name='P-EdjCase-ICP-Agent-Requests-QueryRequest-IngressExpiry'></a>
### IngressExpiry `property`

##### Summary

An upper limit on the validity of the request, expressed in nanoseconds since 1970-01-01

<a name='P-EdjCase-ICP-Agent-Requests-QueryRequest-Method'></a>
### Method `property`

##### Summary

Name of the canister method to call

<a name='P-EdjCase-ICP-Agent-Requests-QueryRequest-Nonce'></a>
### Nonce `property`

##### Summary

Optional. Arbitrary user-provided data, typically randomly generated. 
This can be used to create distinct requests with otherwise identical fields.

<a name='P-EdjCase-ICP-Agent-Requests-QueryRequest-RequestType'></a>
### RequestType `property`

##### Summary

The type of request to send. Will always be 'query'

<a name='P-EdjCase-ICP-Agent-Requests-QueryRequest-Sender'></a>
### Sender `property`

##### Summary

The user who issued the request

<a name='M-EdjCase-ICP-Agent-Requests-QueryRequest-BuildHashableItem'></a>
### BuildHashableItem() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Responses-QueryResponse'></a>
## QueryResponse `type`

##### Namespace

EdjCase.ICP.Agent.Responses

##### Summary

A model representing the response data in the form of a variant

<a name='P-EdjCase-ICP-Agent-Responses-QueryResponse-Signatures'></a>
### Signatures `property`

##### Summary

Signatures from the replica node

<a name='P-EdjCase-ICP-Agent-Responses-QueryResponse-Type'></a>
### Type `property`

##### Summary

The type of response returned. Can be used to call the right method
to extract the variant data

<a name='M-EdjCase-ICP-Agent-Responses-QueryResponse-AsRejected'></a>
### AsRejected() `method`

##### Summary

Gets the reply data IF the response type is 'rejected'. Otherwise will throw exception

##### Returns

Reject error information

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Will throw if not of type 'rejected' |

<a name='M-EdjCase-ICP-Agent-Responses-QueryResponse-AsReplied'></a>
### AsReplied() `method`

##### Summary

Gets the reply data IF the response type is 'replied'. Otherwise will throw exception

##### Returns

Reply data

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Will throw if not of type 'replied' |

<a name='M-EdjCase-ICP-Agent-Responses-QueryResponse-ThrowOrGetReply'></a>
### ThrowOrGetReply() `method`

##### Summary

Helper method to either get the reply data or throw an exception
that formats the 'rejected' data

##### Returns

Query reply data

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [EdjCase.ICP.Agent.QueryRejectedException](#T-EdjCase-ICP-Agent-QueryRejectedException 'EdjCase.ICP.Agent.QueryRejectedException') | Throws if 'rejected' |

<a name='T-EdjCase-ICP-Agent-Responses-QueryResponseType'></a>
## QueryResponseType `type`

##### Namespace

EdjCase.ICP.Agent.Responses

##### Summary

The variant options for a query response

<a name='F-EdjCase-ICP-Agent-Responses-QueryResponseType-Rejected'></a>
### Rejected `constants`

##### Summary

When the cansiter request has errors to query request

<a name='F-EdjCase-ICP-Agent-Responses-QueryResponseType-Replied'></a>
### Replied `constants`

##### Summary

When the canister replies to a query request with no errors

<a name='T-EdjCase-ICP-Agent-Requests-ReadStateRequest'></a>
## ReadStateRequest `type`

##### Namespace

EdjCase.ICP.Agent.Requests

##### Summary

A model for making a read state request to a canister

<a name='M-EdjCase-ICP-Agent-Requests-ReadStateRequest-#ctor-System-Collections-Generic-List{EdjCase-ICP-Candid-Models-StatePath},EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Candid-Models-ICTimestamp-'></a>
### #ctor(paths,sender,ingressExpiry) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| paths | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.StatePath}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.StatePath}') | A list of paths to different state data to obtain. If not specified, data will be pruned and 
be unavailable in the response |
| sender | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | The user who is sending the request |
| ingressExpiry | [EdjCase.ICP.Candid.Models.ICTimestamp](#T-EdjCase-ICP-Candid-Models-ICTimestamp 'EdjCase.ICP.Candid.Models.ICTimestamp') | The expiration of the request to avoid replay attacks |

<a name='P-EdjCase-ICP-Agent-Requests-ReadStateRequest-IngressExpiry'></a>
### IngressExpiry `property`

##### Summary

The expiration of the request to avoid replay attacks

<a name='P-EdjCase-ICP-Agent-Requests-ReadStateRequest-Paths'></a>
### Paths `property`

##### Summary

A list of paths to different state data to obtain. If not specified, data will be pruned and 
be unavailable in the response

<a name='P-EdjCase-ICP-Agent-Requests-ReadStateRequest-REQUEST_TYPE'></a>
### REQUEST_TYPE `property`

##### Summary

The type of request to send. Will always be 'query'

<a name='P-EdjCase-ICP-Agent-Requests-ReadStateRequest-Sender'></a>
### Sender `property`

##### Summary

The user who is sending the request

<a name='M-EdjCase-ICP-Agent-Requests-ReadStateRequest-BuildHashableItem'></a>
### BuildHashableItem() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Responses-ReadStateResponse'></a>
## ReadStateResponse `type`

##### Namespace

EdjCase.ICP.Agent.Responses

##### Summary

Model for a reponse to a read state request

<a name='M-EdjCase-ICP-Agent-Responses-ReadStateResponse-#ctor-EdjCase-ICP-Agent-Models-Certificate-'></a>
### #ctor(certificate) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| certificate | [EdjCase.ICP.Agent.Models.Certificate](#T-EdjCase-ICP-Agent-Models-Certificate 'EdjCase.ICP.Agent.Models.Certificate') | The certificate data of the current canister state |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') |  |

<a name='P-EdjCase-ICP-Agent-Responses-ReadStateResponse-Certificate'></a>
### Certificate `property`

##### Summary

The certificate data of the current canister state

<a name='T-EdjCase-ICP-Agent-Responses-RejectCode'></a>
## RejectCode `type`

##### Namespace

EdjCase.ICP.Agent.Responses

##### Summary

The set of code that are possible from a 'reject' response to a request

<a name='F-EdjCase-ICP-Agent-Responses-RejectCode-CanisterError'></a>
### CanisterError `constants`

##### Summary

Canister error (e.g., trap, no response)

<a name='F-EdjCase-ICP-Agent-Responses-RejectCode-CanisterReject'></a>
### CanisterReject `constants`

##### Summary

Explicit reject by the canister

<a name='F-EdjCase-ICP-Agent-Responses-RejectCode-DestinationInvalid'></a>
### DestinationInvalid `constants`

##### Summary

Invalid destination (e.g. canister/account does not exist)

<a name='F-EdjCase-ICP-Agent-Responses-RejectCode-SysFatal'></a>
### SysFatal `constants`

##### Summary

Fatal system error, retry unlikely to be useful

<a name='F-EdjCase-ICP-Agent-Responses-RejectCode-SysTransient'></a>
### SysTransient `constants`

##### Summary

Transient system error, retry might be possible

<a name='T-EdjCase-ICP-Agent-RequestCleanedUpException'></a>
## RequestCleanedUpException `type`

##### Namespace

EdjCase.ICP.Agent

##### Summary

Exception to indicate that a request has been cleaned up.
This is usually due to the request being too old

<a name='M-EdjCase-ICP-Agent-RequestCleanedUpException-#ctor'></a>
### #ctor() `constructor`

##### Summary



##### Parameters

This constructor has no parameters.

<a name='T-EdjCase-ICP-Agent-Responses-RequestStatus'></a>
## RequestStatus `type`

##### Namespace

EdjCase.ICP.Agent.Responses

##### Summary

A variant type of the status of a request that has been sent to a canister

<a name='P-EdjCase-ICP-Agent-Responses-RequestStatus-Type'></a>
### Type `property`

##### Summary

The type of status was returned

<a name='M-EdjCase-ICP-Agent-Responses-RequestStatus-AsRejected'></a>
### AsRejected() `method`

##### Summary

Returns the reject data IF the status is 'rejected', otherwise throws exception

##### Returns

Reject error information

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Responses-RequestStatus-AsReplied'></a>
### AsReplied() `method`

##### Summary

Returns the candid arg IF the status is 'replied', otherwise throws exception

##### Returns

Candid arg of reply

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Identities-Secp256k1Identity'></a>
## Secp256k1Identity `type`

##### Namespace

EdjCase.ICP.Agent.Identities

##### Summary

An identity using a Ed25519 key

<a name='M-EdjCase-ICP-Agent-Identities-Secp256k1Identity-#ctor-System-Byte[],System-Byte[]-'></a>
### #ctor(publicKey,privateKey) `constructor`

##### Summary

Default constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The raw Secp256k1 public key |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The raw Secp256k1 private key |

<a name='M-EdjCase-ICP-Agent-Identities-Secp256k1Identity-FromPrivateKey-System-Byte[]-'></a>
### FromPrivateKey(privateKey) `method`

##### Summary

Converts a raw secp256k1 private key to a Secp256k1Identity, deriving the public key

##### Returns

Secp256k1Identity with specified private key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Raw secp256k1 private key |

<a name='M-EdjCase-ICP-Agent-Identities-Secp256k1Identity-Generate'></a>
### Generate() `method`

##### Summary

Generates a new secp256k1 public/private key pair and creates an identity
for them

##### Returns

Secp256k1Identity with new key pair

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Models-SignedContent'></a>
## SignedContent `type`

##### Namespace

EdjCase.ICP.Agent.Models

##### Summary

A model containing content and the signature information of it

<a name='M-EdjCase-ICP-Agent-Models-SignedContent-#ctor-System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-Candid-Models-IHashable},EdjCase-ICP-Agent-SubjectPublicKeyInfo,System-Collections-Generic-List{EdjCase-ICP-Agent-Models-SignedDelegation},System-Byte[]-'></a>
### #ctor(content,senderPublicKey,delegations,senderSignature) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| content | [System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.IHashable}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.IHashable}') | The content that is signed in the form of key value pairs |
| senderPublicKey | [EdjCase.ICP.Agent.SubjectPublicKeyInfo](#T-EdjCase-ICP-Agent-SubjectPublicKeyInfo 'EdjCase.ICP.Agent.SubjectPublicKeyInfo') | Public key used to authenticate this request, unless anonymous, then null |
| delegations | [System.Collections.Generic.List{EdjCase.ICP.Agent.Models.SignedDelegation}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Agent.Models.SignedDelegation}') | Optional. A chain of delegations, starting with the one signed by sender_pubkey 
and ending with the one delegating to the key relating to sender_sig. |
| senderSignature | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Signature to authenticate this request, unless anonymous, then null |

<a name='P-EdjCase-ICP-Agent-Models-SignedContent-Content'></a>
### Content `property`

##### Summary

The content that is signed in the form of key value pairs

<a name='P-EdjCase-ICP-Agent-Models-SignedContent-SenderDelegations'></a>
### SenderDelegations `property`

##### Summary

Optional. A chain of delegations, starting with the one signed by sender_pubkey
and ending with the one delegating to the key relating to sender_sig.

<a name='P-EdjCase-ICP-Agent-Models-SignedContent-SenderPublicKey'></a>
### SenderPublicKey `property`

##### Summary

Public key used to authenticate this request, unless anonymous, then null

<a name='P-EdjCase-ICP-Agent-Models-SignedContent-SenderSignature'></a>
### SenderSignature `property`

##### Summary

Signature to authenticate this request, unless anonymous, then null

<a name='M-EdjCase-ICP-Agent-Models-SignedContent-BuildHashableItem'></a>
### BuildHashableItem() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Models-SignedDelegation'></a>
## SignedDelegation `type`

##### Namespace

EdjCase.ICP.Agent.Models

##### Summary

A delegation that has been signed by an identity

<a name='M-EdjCase-ICP-Agent-Models-SignedDelegation-#ctor-EdjCase-ICP-Agent-Models-Delegation,System-Byte[]-'></a>
### #ctor(delegation,signature) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| delegation | [EdjCase.ICP.Agent.Models.Delegation](#T-EdjCase-ICP-Agent-Models-Delegation 'EdjCase.ICP.Agent.Models.Delegation') | The delegation that is signed |
| signature | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The signature for the delegation |

<a name='P-EdjCase-ICP-Agent-Models-SignedDelegation-Delegation'></a>
### Delegation `property`

##### Summary

The delegation that is signed

<a name='P-EdjCase-ICP-Agent-Models-SignedDelegation-Signature'></a>
### Signature `property`

##### Summary

The signature for the delegation

<a name='M-EdjCase-ICP-Agent-Models-SignedDelegation-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction-'></a>
### ComputeHash() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Models-SignedDelegation-Create-EdjCase-ICP-Agent-SubjectPublicKeyInfo,EdjCase-ICP-Agent-Identities-IIdentity,EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}-'></a>
### Create(keyToDelegateTo,delegatingIdentity,expiration,targets,senders) `method`

##### Summary

Creates a delegation signed by the delegating identity, authorizing the public key

##### Returns

A delegation signed by the delegating identity

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keyToDelegateTo | [EdjCase.ICP.Agent.SubjectPublicKeyInfo](#T-EdjCase-ICP-Agent-SubjectPublicKeyInfo 'EdjCase.ICP.Agent.SubjectPublicKeyInfo') | The key to delegate authority to |
| delegatingIdentity | [EdjCase.ICP.Agent.Identities.IIdentity](#T-EdjCase-ICP-Agent-Identities-IIdentity 'EdjCase.ICP.Agent.Identities.IIdentity') | The identity that is signing the delegation |
| expiration | [EdjCase.ICP.Candid.Models.ICTimestamp](#T-EdjCase-ICP-Candid-Models-ICTimestamp 'EdjCase.ICP.Candid.Models.ICTimestamp') | How long to delegate for |
| targets | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}') | Optional. List of canister ids to limit delegation to |
| senders | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}') | Optional. List of pricipals where requests can originate from |

<a name='M-EdjCase-ICP-Agent-Models-SignedDelegation-Create-EdjCase-ICP-Agent-SubjectPublicKeyInfo,System-Func{System-Byte[],System-Byte[]},EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}-'></a>
### Create(keyToDelegateTo,signingFunc,expiration,targets,senders) `method`

##### Summary

Creates a delegation signed by the delegating identity, authorizing the public key

##### Returns

A delegation signed by the delegating identity

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keyToDelegateTo | [EdjCase.ICP.Agent.SubjectPublicKeyInfo](#T-EdjCase-ICP-Agent-SubjectPublicKeyInfo 'EdjCase.ICP.Agent.SubjectPublicKeyInfo') | The key to delegate authority to |
| signingFunc | [System.Func{System.Byte[],System.Byte[]}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Byte[],System.Byte[]}') | Function to sign the delegation bytes |
| expiration | [EdjCase.ICP.Candid.Models.ICTimestamp](#T-EdjCase-ICP-Candid-Models-ICTimestamp 'EdjCase.ICP.Candid.Models.ICTimestamp') | How long to delegate for |
| targets | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}') | Optional. List of canister ids to limit delegation to |
| senders | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}') | Optional. List of pricipals where requests can originate from |

<a name='T-EdjCase-ICP-Agent-Responses-StatusResponse'></a>
## StatusResponse `type`

##### Namespace

EdjCase.ICP.Agent.Responses

##### Summary

The model for response data from a status request

<a name='M-EdjCase-ICP-Agent-Responses-StatusResponse-#ctor-System-String,System-String,System-String,System-String,System-Byte[]-'></a>
### #ctor(icApiVersion,implementationSource,implementationVersion,implementationRevision,developmentRootKey) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| icApiVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Identifies the interface version supported, i.e. the version of the present
document that the internet computer aims to support, e.g. \`0.8.1\`.
The implementation may also return unversioned to indicate that it does not
comply to a particular version, e.g. in between releases. |
| implementationSource | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional. Identifies the implementation of the Internet Computer Protocol,
by convention with the canonical location of the source code 
(e.g. https://github.com/dfinity/ic). |
| implementationVersion | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional. If the user is talking to a released version of an Internet Computer Protocol
 implementation, this is the version number. For non-released versions, output
 of git describe like \`0.1.13-13-g2414721\` would also be very suitable. |
| implementationRevision | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional. The precise git revision of the Internet Computer Protocol implementation |
| developmentRootKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key (a DER-encoded BLS key) of the root key of this development
instance of the Internet Computer Protocol. This must be present in short-lived
development instances, to allow the agent to fetch the public key. For the
Internet Computer, agents must have an independent trustworthy source for this data,
and must not be tempted to fetch it from this insecure location |

<a name='P-EdjCase-ICP-Agent-Responses-StatusResponse-DevelopmentRootKey'></a>
### DevelopmentRootKey `property`

##### Summary

The public key (a DER-encoded BLS key) of the root key of this development
instance of the Internet Computer Protocol. This must be present in short-lived
development instances, to allow the agent to fetch the public key. For the
Internet Computer, agents must have an independent trustworthy source for this data,
and must not be tempted to fetch it from this insecure location.

<a name='P-EdjCase-ICP-Agent-Responses-StatusResponse-ICApiVersion'></a>
### ICApiVersion `property`

##### Summary

Identifies the interface version supported, i.e. the version of the present
document that the internet computer aims to support, e.g. \`0.8.1\`.
The implementation may also return unversioned to indicate that it does not
comply to a particular version, e.g. in between releases.

<a name='P-EdjCase-ICP-Agent-Responses-StatusResponse-ImplementationRevision'></a>
### ImplementationRevision `property`

##### Summary

Optional. The precise git revision of the Internet Computer Protocol implementation

<a name='P-EdjCase-ICP-Agent-Responses-StatusResponse-ImplementationSource'></a>
### ImplementationSource `property`

##### Summary

Optional. Identifies the implementation of the Internet Computer Protocol,
by convention with the canonical location of the source code 
(e.g. https://github.com/dfinity/ic).

<a name='P-EdjCase-ICP-Agent-Responses-StatusResponse-ImplementationVersion'></a>
### ImplementationVersion `property`

##### Summary

Optional. If the user is talking to a released version of an Internet Computer Protocol
 implementation, this is the version number. For non-released versions, output
 of git describe like \`0.1.13-13-g2414721\` would also be very suitable.

<a name='T-EdjCase-ICP-Agent-Responses-RequestStatus-StatusType'></a>
## StatusType `type`

##### Namespace

EdjCase.ICP.Agent.Responses.RequestStatus

##### Summary

The types of statuses the request can be

<a name='F-EdjCase-ICP-Agent-Responses-RequestStatus-StatusType-Done'></a>
### Done `constants`

##### Summary

The request has been processed but the response data has been removed.
This usually happens after a certain amount of time to save space

<a name='F-EdjCase-ICP-Agent-Responses-RequestStatus-StatusType-Processing'></a>
### Processing `constants`

##### Summary

The request is being processed and does not have a response yet

<a name='F-EdjCase-ICP-Agent-Responses-RequestStatus-StatusType-Received'></a>
### Received `constants`

##### Summary

The request has been received by the node, but not yet being processed

<a name='F-EdjCase-ICP-Agent-Responses-RequestStatus-StatusType-Rejected'></a>
### Rejected `constants`

##### Summary

The request has been processed and has reject data

<a name='F-EdjCase-ICP-Agent-Responses-RequestStatus-StatusType-Replied'></a>
### Replied `constants`

##### Summary

The request has been processed and it has reply data

<a name='T-EdjCase-ICP-Agent-SubjectPublicKeyInfo'></a>
## SubjectPublicKeyInfo `type`

##### Namespace

EdjCase.ICP.Agent

##### Summary

A model representing a public key value and the cryptographic algorithm that is for

<a name='M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-#ctor-EdjCase-ICP-Agent-AlgorithmIdentifier,System-Byte[]-'></a>
### #ctor(algorithm,subjectPublicKey) `constructor`

##### Summary

Default constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| algorithm | [EdjCase.ICP.Agent.AlgorithmIdentifier](#T-EdjCase-ICP-Agent-AlgorithmIdentifier 'EdjCase.ICP.Agent.AlgorithmIdentifier') | The cryptographic algorithm that the public key is for |
| subjectPublicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The raw public key bytes |

<a name='P-EdjCase-ICP-Agent-SubjectPublicKeyInfo-Algorithm'></a>
### Algorithm `property`

##### Summary

The cryptographic algorithm that the public key is for

<a name='P-EdjCase-ICP-Agent-SubjectPublicKeyInfo-PublicKey'></a>
### PublicKey `property`

##### Summary

The raw public key bytes

<a name='M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-Bls-System-Byte[]-'></a>
### Bls(publicKey) `method`

##### Summary

Converts a raw bls public key into a subject public key info

##### Returns

Bls SubjectPublicKeyInfo

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A raw bls public key |

<a name='M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-Ecdsa-System-Byte[],System-String-'></a>
### Ecdsa(publicKey,curveOid) `method`

##### Summary

Converts a raw ed25519 public key into a subject public key info

##### Returns

Ed25519 SubjectPublicKeyInfo

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A raw ed25519 public key |
| curveOid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The OID of the ecdsa curve (eg "1.3.132.0.10" for secp256k1) |

<a name='M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-Ed25519-System-Byte[]-'></a>
### Ed25519(publicKey) `method`

##### Summary

Converts a raw ed25519 public key into a subject public key info

##### Returns

Ed25519 SubjectPublicKeyInfo

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A raw ed25519 public key |

<a name='M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-FromDerEncoding-System-Byte[]-'></a>
### FromDerEncoding(derEncodedPublicKey) `method`

##### Summary

Parses a DER encoded subject public key info

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| derEncodedPublicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A DER encoded public key |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [EdjCase.ICP.Agent.InvalidPublicKey](#T-EdjCase-ICP-Agent-InvalidPublicKey 'EdjCase.ICP.Agent.InvalidPublicKey') |  |

<a name='M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-Secp256k1-System-Byte[]-'></a>
### Secp256k1(publicKey) `method`

##### Summary

Converts a raw secp256k1 public key into a subject public key info

##### Returns

Secp256k1 SubjectPublicKeyInfo

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | A raw secp256k1 public key |

<a name='M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-ToDerEncoding'></a>
### ToDerEncoding() `method`

##### Summary

Converts the subject public key info into a DER encoded byte array

##### Returns

A DER encoded byte array

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-SubjectPublicKeyInfo-ToPrincipal'></a>
### ToPrincipal() `method`

##### Summary

Converts the key to a self authenticating principal value

##### Returns

Principal of the public key

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-SupportedStandard'></a>
## SupportedStandard `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

This class represents a supported standard with a name and URL

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-SupportedStandard-#ctor-System-String,System-String-'></a>
### #ctor(name,url) `constructor`

##### Summary

Primary constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the supported standard |
| url | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The URL of the supported standard |

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-SupportedStandard-Name'></a>
### Name `property`

##### Summary

The name of the supported standard

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-SupportedStandard-Url'></a>
### Url `property`

##### Summary

The Url of the supported standard

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs'></a>
## TransferArgs `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

This class represents the arguments for transferring an ICRC1 token

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-#ctor-EdjCase-ICP-Candid-Models-OptionalValue{System-Collections-Generic-List{System-Byte}},EdjCase-ICP-Agent-Standards-ICRC1-Models-Account,EdjCase-ICP-Candid-Models-UnboundedUInt,EdjCase-ICP-Candid-Models-OptionalValue{EdjCase-ICP-Candid-Models-UnboundedUInt},EdjCase-ICP-Candid-Models-OptionalValue{System-Collections-Generic-List{System-Byte}},EdjCase-ICP-Candid-Models-OptionalValue{System-UInt64}-'></a>
### #ctor(fromSubaccount,to,amount,fee,memo,createdAtTime) `constructor`

##### Summary

Primary constructor for the TransferArgs class

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fromSubaccount | [EdjCase.ICP.Candid.Models.OptionalValue{System.Collections.Generic.List{System.Byte}}](#T-EdjCase-ICP-Candid-Models-OptionalValue{System-Collections-Generic-List{System-Byte}} 'EdjCase.ICP.Candid.Models.OptionalValue{System.Collections.Generic.List{System.Byte}}') | The subaccount from which the transfer is made, represented as an OptionalValue object |
| to | [EdjCase.ICP.Agent.Standards.ICRC1.Models.Account](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-Account 'EdjCase.ICP.Agent.Standards.ICRC1.Models.Account') | The account to which the transfer is made, represented as an Account object |
| amount | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | The amount of the token being transferred, represented as an UnboundedUInt object |
| fee | [EdjCase.ICP.Candid.Models.OptionalValue{EdjCase.ICP.Candid.Models.UnboundedUInt}](#T-EdjCase-ICP-Candid-Models-OptionalValue{EdjCase-ICP-Candid-Models-UnboundedUInt} 'EdjCase.ICP.Candid.Models.OptionalValue{EdjCase.ICP.Candid.Models.UnboundedUInt}') | The fee for the transfer, represented as an OptionalValue object |
| memo | [EdjCase.ICP.Candid.Models.OptionalValue{System.Collections.Generic.List{System.Byte}}](#T-EdjCase-ICP-Candid-Models-OptionalValue{System-Collections-Generic-List{System-Byte}} 'EdjCase.ICP.Candid.Models.OptionalValue{System.Collections.Generic.List{System.Byte}}') | The memo for the transfer, represented as an OptionalValue object |
| createdAtTime | [EdjCase.ICP.Candid.Models.OptionalValue{System.UInt64}](#T-EdjCase-ICP-Candid-Models-OptionalValue{System-UInt64} 'EdjCase.ICP.Candid.Models.OptionalValue{System.UInt64}') | The time at which the transfer is created, represented as an OptionalValue object |

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-Amount'></a>
### Amount `property`

##### Summary

The amount of the token being transferred, represented as an UnboundedUInt object

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-CreatedAtTime'></a>
### CreatedAtTime `property`

##### Summary

The time at which the transfer is created, represented as an OptionalValue object

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-Fee'></a>
### Fee `property`

##### Summary

The fee for the transfer, represented as an OptionalValue object

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-FromSubaccount'></a>
### FromSubaccount `property`

##### Summary

The subaccount from which the transfer is made, represented as an OptionalValue object

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-Memo'></a>
### Memo `property`

##### Summary

The memo for the transfer, represented as an OptionalValue object

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferArgs-To'></a>
### To `property`

##### Summary

The account to which the transfer is made, represented as an Account object

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError'></a>
## TransferError `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

This class represents an error that can occur during a transfer

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-#ctor'></a>
### #ctor() `constructor`

##### Summary

Constructor for reflection

##### Parameters

This constructor has no parameters.

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-Tag'></a>
### Tag `property`

##### Summary

The tag that indicates the type of transfer error

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-Value'></a>
### Value `property`

##### Summary

The value that contains the error information, represented as an object

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-AsBadBurn'></a>
### AsBadBurn() `method`

##### Summary

Gets the value of this TransferError object as a BadBurnError object

##### Returns

The BadBurnError object representing the error information

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-AsBadFee'></a>
### AsBadFee() `method`

##### Summary

Gets the value of this TransferError object as a BadFeeError object

##### Returns

The BadFeeError object representing the error information

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-AsCreatedInFuture'></a>
### AsCreatedInFuture() `method`

##### Summary

Gets the value of this TransferError object as a CreatedInFutureError object

##### Returns

The CreatedInFutureError object representing the error information

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-AsDuplicate'></a>
### AsDuplicate() `method`

##### Summary

Gets the value of this TransferError object as a DuplicateError object

##### Returns

The DuplicateError object representing the error information

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-AsGenericError'></a>
### AsGenericError() `method`

##### Summary

Gets the value of this TransferError object as a GenericError object

##### Returns

The GenericError object representing the error information

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-AsInsufficientFunds'></a>
### AsInsufficientFunds() `method`

##### Summary

Gets the value of this TransferError object as an InsufficientFundsError object

##### Returns

The InsufficientFundsError object representing the error information

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-BadBurn-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadBurnError-'></a>
### BadBurn(info) `method`

##### Summary

Creates a new instance of TransferError with a BadBurnError object as the value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| info | [EdjCase.ICP.Agent.Standards.ICRC1.Models.BadBurnError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadBurnError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.BadBurnError') | The BadBurnError object containing the error information |

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-BadFee-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadFeeError-'></a>
### BadFee(info) `method`

##### Summary

Creates a new instance of TransferError with a BadFeeError object as the value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| info | [EdjCase.ICP.Agent.Standards.ICRC1.Models.BadFeeError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-BadFeeError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.BadFeeError') | The BadFeeError object containing the error information |

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-CreatedInFuture-EdjCase-ICP-Agent-Standards-ICRC1-Models-CreatedInFutureError-'></a>
### CreatedInFuture(info) `method`

##### Summary

Creates a new instance of TransferError with a CreatedInFutureError object as the value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| info | [EdjCase.ICP.Agent.Standards.ICRC1.Models.CreatedInFutureError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-CreatedInFutureError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.CreatedInFutureError') | The CreatedInFutureError object containing the error information |

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-Duplicate-EdjCase-ICP-Agent-Standards-ICRC1-Models-DuplicateError-'></a>
### Duplicate(info) `method`

##### Summary

Creates a new instance of TransferError with a DuplicateError object as the value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| info | [EdjCase.ICP.Agent.Standards.ICRC1.Models.DuplicateError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-DuplicateError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.DuplicateError') | The DuplicateError object containing the error information |

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-GenericError-EdjCase-ICP-Agent-Standards-ICRC1-Models-GenericError-'></a>
### GenericError(info) `method`

##### Summary

Creates a new instance of TransferError with a GenericError object as the value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| info | [EdjCase.ICP.Agent.Standards.ICRC1.Models.GenericError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-GenericError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.GenericError') | The GenericError object containing the error information |

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-InsufficientFunds-EdjCase-ICP-Agent-Standards-ICRC1-Models-InsufficientFundsError-'></a>
### InsufficientFunds(info) `method`

##### Summary

Creates a new instance of TransferError with an InsufficientFundsError object as the value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| info | [EdjCase.ICP.Agent.Standards.ICRC1.Models.InsufficientFundsError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-InsufficientFundsError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.InsufficientFundsError') | The InsufficientFundsError object containing the error information |

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-TemporarilyUnavailable'></a>
### TemporarilyUnavailable() `method`

##### Summary

Creates a new instance of TransferError with a TemporarilyUnavailable tag and null value

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-TooOld'></a>
### TooOld() `method`

##### Summary

Creates a new instance of TransferError with a TooOld tag and null value

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-ValidateTag-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-'></a>
### ValidateTag(tag) `method`

##### Summary

Throws an exception if the current tag of this TransferError object does not match the given tag

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tag | [EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferErrorTag](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferErrorTag') | The expected tag |

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag'></a>
## TransferErrorTag `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

This enum represents the possible types of errors that can occur during a transfer

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-BadBurn'></a>
### BadBurn `constants`

##### Summary

Indicates an error due to an incorrect burn amount

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-BadFee'></a>
### BadFee `constants`

##### Summary

Indicates an error due to an incorrect fee

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-CreatedInFuture'></a>
### CreatedInFuture `constants`

##### Summary

Indicates an error due to a transaction being created in the future

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-Duplicate'></a>
### Duplicate `constants`

##### Summary

Indicates an error due to a transaction being a duplicate

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-GenericError'></a>
### GenericError `constants`

##### Summary

Indicates a generic error that can occur during a transfer

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-InsufficientFunds'></a>
### InsufficientFunds `constants`

##### Summary

Indicates an error due to insufficient funds for the transaction

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-TemporarilyUnavailable'></a>
### TemporarilyUnavailable `constants`

##### Summary

Indicates that the service is temporarily unavailable

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferErrorTag-TooOld'></a>
### TooOld `constants`

##### Summary

Indicates an error due to a transaction being too old

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult'></a>
## TransferResult `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

Represents the result of a transfer operation, which can either be Ok with a value or Err with an error object

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-#ctor'></a>
### #ctor() `constructor`

##### Summary

Private default constructor used for reflection

##### Parameters

This constructor has no parameters.

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-Tag'></a>
### Tag `property`

##### Summary

The tag indicating whether the transfer operation was successful or resulted in an error

<a name='P-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-Value'></a>
### Value `property`

##### Summary

The value of this TransferResult object, which can be either an UnboundedUInt or a TransferError object

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-AsErr'></a>
### AsErr() `method`

##### Summary

Gets the value of this TransferResult object as a TransferError object

##### Returns

The TransferError object associated with this TransferResult object

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-AsOk'></a>
### AsOk() `method`

##### Summary

Gets the value of this TransferResult object as an UnboundedUInt

##### Returns

The UnboundedUInt value associated with this TransferResult object

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-Err-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError-'></a>
### Err(info) `method`

##### Summary

Creates a new instance of TransferResult with the Err tag and the given TransferError object as the value

##### Returns

A new instance of TransferResult with the Err tag and the given value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| info | [EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferError 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferError') | The TransferError object containing the error information |

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-Ok-EdjCase-ICP-Candid-Models-UnboundedUInt-'></a>
### Ok(info) `method`

##### Summary

Creates a new instance of TransferResult with the Ok tag and the given UnboundedUInt value

##### Returns

A new instance of TransferResult with the Ok tag and the given value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| info | [EdjCase.ICP.Candid.Models.UnboundedUInt](#T-EdjCase-ICP-Candid-Models-UnboundedUInt 'EdjCase.ICP.Candid.Models.UnboundedUInt') | The value associated with this Ok TransferResult object |

<a name='M-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResult-ValidateTag-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResultTag-'></a>
### ValidateTag(tag) `method`

##### Summary

Throws an exception if the current tag of this TransferResult object does not match the given tag

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tag | [EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResultTag](#T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResultTag 'EdjCase.ICP.Agent.Standards.ICRC1.Models.TransferResultTag') | The expected tag |

<a name='T-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResultTag'></a>
## TransferResultTag `type`

##### Namespace

EdjCase.ICP.Agent.Standards.ICRC1.Models

##### Summary

An enumeration of possible tags for a TransferResult object, which can either be Ok with an UnboundedUInt value or Err with a TransferError object

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResultTag-Err'></a>
### Err `constants`

##### Summary

Indicates a failed transfer operation with the associated TransferError object containing information about the error

<a name='F-EdjCase-ICP-Agent-Standards-ICRC1-Models-TransferResultTag-Ok'></a>
### Ok `constants`

##### Summary

Indicates a successful transfer operation with the associated UnboundedUInt value
