<a name='assembly'></a>
# EdjCase.ICP.Agent

## Contents

- [CallRejectedException](#T-EdjCase-ICP-Agent-CallRejectedException 'EdjCase.ICP.Agent.CallRejectedException')
  - [#ctor(rejectCode,rejectMessage,errorCode)](#M-EdjCase-ICP-Agent-CallRejectedException-#ctor-EdjCase-ICP-Agent-Responses-RejectCode,System-String,System-String- 'EdjCase.ICP.Agent.CallRejectedException.#ctor(EdjCase.ICP.Agent.Responses.RejectCode,System.String,System.String)')
  - [ErrorCode](#P-EdjCase-ICP-Agent-CallRejectedException-ErrorCode 'EdjCase.ICP.Agent.CallRejectedException.ErrorCode')
  - [RejectCode](#P-EdjCase-ICP-Agent-CallRejectedException-RejectCode 'EdjCase.ICP.Agent.CallRejectedException.RejectCode')
  - [RejectMessage](#P-EdjCase-ICP-Agent-CallRejectedException-RejectMessage 'EdjCase.ICP.Agent.CallRejectedException.RejectMessage')
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
  - [IsValid(rootPublicKey)](#M-EdjCase-ICP-Agent-Models-Certificate-IsValid-System-Byte[]- 'EdjCase.ICP.Agent.Models.Certificate.IsValid(System.Byte[])')
- [CertificateDelegation](#T-EdjCase-ICP-Agent-Models-CertificateDelegation 'EdjCase.ICP.Agent.Models.CertificateDelegation')
  - [#ctor(subnetId,certificate)](#M-EdjCase-ICP-Agent-Models-CertificateDelegation-#ctor-EdjCase-ICP-Candid-Models-Principal,EdjCase-ICP-Agent-Models-Certificate- 'EdjCase.ICP.Agent.Models.CertificateDelegation.#ctor(EdjCase.ICP.Candid.Models.Principal,EdjCase.ICP.Agent.Models.Certificate)')
  - [Certificate](#P-EdjCase-ICP-Agent-Models-CertificateDelegation-Certificate 'EdjCase.ICP.Agent.Models.CertificateDelegation.Certificate')
  - [SubnetId](#P-EdjCase-ICP-Agent-Models-CertificateDelegation-SubnetId 'EdjCase.ICP.Agent.Models.CertificateDelegation.SubnetId')
  - [GetPublicKey()](#M-EdjCase-ICP-Agent-Models-CertificateDelegation-GetPublicKey 'EdjCase.ICP.Agent.Models.CertificateDelegation.GetPublicKey')
  - [IsValid(publicKey)](#M-EdjCase-ICP-Agent-Models-CertificateDelegation-IsValid-System-Byte[]@- 'EdjCase.ICP.Agent.Models.CertificateDelegation.IsValid(System.Byte[]@)')
- [DefaultHttpClient](#T-EdjCase-ICP-Agent-Agents-Http-DefaultHttpClient 'EdjCase.ICP.Agent.Agents.Http.DefaultHttpClient')
  - [#ctor(client)](#M-EdjCase-ICP-Agent-Agents-Http-DefaultHttpClient-#ctor-System-Net-Http-HttpClient- 'EdjCase.ICP.Agent.Agents.Http.DefaultHttpClient.#ctor(System.Net.Http.HttpClient)')
  - [SendAsync()](#M-EdjCase-ICP-Agent-Agents-Http-DefaultHttpClient-SendAsync-System-Net-Http-HttpRequestMessage- 'EdjCase.ICP.Agent.Agents.Http.DefaultHttpClient.SendAsync(System.Net.Http.HttpRequestMessage)')
- [Delegation](#T-EdjCase-ICP-Agent-Models-Delegation 'EdjCase.ICP.Agent.Models.Delegation')
  - [#ctor(publicKey,expiration,targets,senders)](#M-EdjCase-ICP-Agent-Models-Delegation-#ctor-System-Byte[],EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}- 'EdjCase.ICP.Agent.Models.Delegation.#ctor(System.Byte[],EdjCase.ICP.Candid.Models.ICTimestamp,System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal},System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal})')
  - [Expiration](#P-EdjCase-ICP-Agent-Models-Delegation-Expiration 'EdjCase.ICP.Agent.Models.Delegation.Expiration')
  - [PublicKey](#P-EdjCase-ICP-Agent-Models-Delegation-PublicKey 'EdjCase.ICP.Agent.Models.Delegation.PublicKey')
  - [Senders](#P-EdjCase-ICP-Agent-Models-Delegation-Senders 'EdjCase.ICP.Agent.Models.Delegation.Senders')
  - [Targets](#P-EdjCase-ICP-Agent-Models-Delegation-Targets 'EdjCase.ICP.Agent.Models.Delegation.Targets')
  - [BuildHashableItem()](#M-EdjCase-ICP-Agent-Models-Delegation-BuildHashableItem 'EdjCase.ICP.Agent.Models.Delegation.BuildHashableItem')
  - [BuildSigningChallenge()](#M-EdjCase-ICP-Agent-Models-Delegation-BuildSigningChallenge 'EdjCase.ICP.Agent.Models.Delegation.BuildSigningChallenge')
  - [ComputeHash()](#M-EdjCase-ICP-Agent-Models-Delegation-ComputeHash-EdjCase-ICP-Candid-Crypto-IHashFunction- 'EdjCase.ICP.Agent.Models.Delegation.ComputeHash(EdjCase.ICP.Candid.Crypto.IHashFunction)')
- [DelegationChain](#T-EdjCase-ICP-Agent-Models-DelegationChain 'EdjCase.ICP.Agent.Models.DelegationChain')
  - [#ctor(publicKey,delegations)](#M-EdjCase-ICP-Agent-Models-DelegationChain-#ctor-EdjCase-ICP-Agent-DerEncodedPublicKey,System-Collections-Generic-List{EdjCase-ICP-Agent-Models-SignedDelegation}- 'EdjCase.ICP.Agent.Models.DelegationChain.#ctor(EdjCase.ICP.Agent.DerEncodedPublicKey,System.Collections.Generic.List{EdjCase.ICP.Agent.Models.SignedDelegation})')
  - [Delegations](#P-EdjCase-ICP-Agent-Models-DelegationChain-Delegations 'EdjCase.ICP.Agent.Models.DelegationChain.Delegations')
  - [PublicKey](#P-EdjCase-ICP-Agent-Models-DelegationChain-PublicKey 'EdjCase.ICP.Agent.Models.DelegationChain.PublicKey')
  - [Create(keyToDelegateTo,delegatingIdentity,expiration,targets,senders)](#M-EdjCase-ICP-Agent-Models-DelegationChain-Create-EdjCase-ICP-Agent-DerEncodedPublicKey,EdjCase-ICP-Agent-Identities-IIdentity,EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}- 'EdjCase.ICP.Agent.Models.DelegationChain.Create(EdjCase.ICP.Agent.DerEncodedPublicKey,EdjCase.ICP.Agent.Identities.IIdentity,EdjCase.ICP.Candid.Models.ICTimestamp,System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal},System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal})')
- [DelegationIdentity](#T-EdjCase-ICP-Agent-Identities-DelegationIdentity 'EdjCase.ICP.Agent.Identities.DelegationIdentity')
  - [#ctor(identity,chain)](#M-EdjCase-ICP-Agent-Identities-DelegationIdentity-#ctor-EdjCase-ICP-Agent-Identities-IIdentity,EdjCase-ICP-Agent-Models-DelegationChain- 'EdjCase.ICP.Agent.Identities.DelegationIdentity.#ctor(EdjCase.ICP.Agent.Identities.IIdentity,EdjCase.ICP.Agent.Models.DelegationChain)')
  - [Chain](#P-EdjCase-ICP-Agent-Identities-DelegationIdentity-Chain 'EdjCase.ICP.Agent.Identities.DelegationIdentity.Chain')
  - [Identity](#P-EdjCase-ICP-Agent-Identities-DelegationIdentity-Identity 'EdjCase.ICP.Agent.Identities.DelegationIdentity.Identity')
  - [GetPublicKey()](#M-EdjCase-ICP-Agent-Identities-DelegationIdentity-GetPublicKey 'EdjCase.ICP.Agent.Identities.DelegationIdentity.GetPublicKey')
  - [GetSenderDelegations()](#M-EdjCase-ICP-Agent-Identities-DelegationIdentity-GetSenderDelegations 'EdjCase.ICP.Agent.Identities.DelegationIdentity.GetSenderDelegations')
  - [Sign()](#M-EdjCase-ICP-Agent-Identities-DelegationIdentity-Sign-System-Byte[]- 'EdjCase.ICP.Agent.Identities.DelegationIdentity.Sign(System.Byte[])')
- [DerEncodedPublicKey](#T-EdjCase-ICP-Agent-DerEncodedPublicKey 'EdjCase.ICP.Agent.DerEncodedPublicKey')
  - [#ctor(derEncodedBytes)](#M-EdjCase-ICP-Agent-DerEncodedPublicKey-#ctor-System-Byte[]- 'EdjCase.ICP.Agent.DerEncodedPublicKey.#ctor(System.Byte[])')
  - [Value](#P-EdjCase-ICP-Agent-DerEncodedPublicKey-Value 'EdjCase.ICP.Agent.DerEncodedPublicKey.Value')
  - [As()](#M-EdjCase-ICP-Agent-DerEncodedPublicKey-As-System-String- 'EdjCase.ICP.Agent.DerEncodedPublicKey.As(System.String)')
  - [As()](#M-EdjCase-ICP-Agent-DerEncodedPublicKey-As-System-Collections-Generic-IEnumerable{System-String}- 'EdjCase.ICP.Agent.DerEncodedPublicKey.As(System.Collections.Generic.IEnumerable{System.String})')
  - [AsBls()](#M-EdjCase-ICP-Agent-DerEncodedPublicKey-AsBls 'EdjCase.ICP.Agent.DerEncodedPublicKey.AsBls')
  - [AsCose()](#M-EdjCase-ICP-Agent-DerEncodedPublicKey-AsCose 'EdjCase.ICP.Agent.DerEncodedPublicKey.AsCose')
  - [AsEd25519()](#M-EdjCase-ICP-Agent-DerEncodedPublicKey-AsEd25519 'EdjCase.ICP.Agent.DerEncodedPublicKey.AsEd25519')
  - [From(value,oid)](#M-EdjCase-ICP-Agent-DerEncodedPublicKey-From-System-Byte[],System-String- 'EdjCase.ICP.Agent.DerEncodedPublicKey.From(System.Byte[],System.String)')
  - [From(value,oids)](#M-EdjCase-ICP-Agent-DerEncodedPublicKey-From-System-Byte[],System-Collections-Generic-IEnumerable{System-String}- 'EdjCase.ICP.Agent.DerEncodedPublicKey.From(System.Byte[],System.Collections.Generic.IEnumerable{System.String})')
  - [FromBls(value)](#M-EdjCase-ICP-Agent-DerEncodedPublicKey-FromBls-System-Byte[]- 'EdjCase.ICP.Agent.DerEncodedPublicKey.FromBls(System.Byte[])')
  - [FromCose(value)](#M-EdjCase-ICP-Agent-DerEncodedPublicKey-FromCose-System-Byte[]- 'EdjCase.ICP.Agent.DerEncodedPublicKey.FromCose(System.Byte[])')
  - [FromDer(value)](#M-EdjCase-ICP-Agent-DerEncodedPublicKey-FromDer-System-Byte[]- 'EdjCase.ICP.Agent.DerEncodedPublicKey.FromDer(System.Byte[])')
  - [FromEd25519(value)](#M-EdjCase-ICP-Agent-DerEncodedPublicKey-FromEd25519-System-Byte[]- 'EdjCase.ICP.Agent.DerEncodedPublicKey.FromEd25519(System.Byte[])')
  - [ToPrincipal()](#M-EdjCase-ICP-Agent-DerEncodedPublicKey-ToPrincipal 'EdjCase.ICP.Agent.DerEncodedPublicKey.ToPrincipal')
- [Ed25519Identity](#T-EdjCase-ICP-Agent-Identities-Ed25519Identity 'EdjCase.ICP.Agent.Identities.Ed25519Identity')
  - [#ctor(publicKey,privateKey)](#M-EdjCase-ICP-Agent-Identities-Ed25519Identity-#ctor-EdjCase-ICP-Agent-DerEncodedPublicKey,System-Byte[]- 'EdjCase.ICP.Agent.Identities.Ed25519Identity.#ctor(EdjCase.ICP.Agent.DerEncodedPublicKey,System.Byte[])')
  - [PrivateKey](#P-EdjCase-ICP-Agent-Identities-Ed25519Identity-PrivateKey 'EdjCase.ICP.Agent.Identities.Ed25519Identity.PrivateKey')
  - [PublicKey](#P-EdjCase-ICP-Agent-Identities-Ed25519Identity-PublicKey 'EdjCase.ICP.Agent.Identities.Ed25519Identity.PublicKey')
  - [Create()](#M-EdjCase-ICP-Agent-Identities-Ed25519Identity-Create 'EdjCase.ICP.Agent.Identities.Ed25519Identity.Create')
  - [GetPublicKey()](#M-EdjCase-ICP-Agent-Identities-Ed25519Identity-GetPublicKey 'EdjCase.ICP.Agent.Identities.Ed25519Identity.GetPublicKey')
  - [GetSenderDelegations()](#M-EdjCase-ICP-Agent-Identities-Ed25519Identity-GetSenderDelegations 'EdjCase.ICP.Agent.Identities.Ed25519Identity.GetSenderDelegations')
  - [Sign()](#M-EdjCase-ICP-Agent-Identities-Ed25519Identity-Sign-System-Byte[]- 'EdjCase.ICP.Agent.Identities.Ed25519Identity.Sign(System.Byte[])')
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
- [IHttpClient](#T-EdjCase-ICP-Agent-Agents-Http-IHttpClient 'EdjCase.ICP.Agent.Agents.Http.IHttpClient')
  - [SendAsync(message)](#M-EdjCase-ICP-Agent-Agents-Http-IHttpClient-SendAsync-System-Net-Http-HttpRequestMessage- 'EdjCase.ICP.Agent.Agents.Http.IHttpClient.SendAsync(System.Net.Http.HttpRequestMessage)')
- [IIdentity](#T-EdjCase-ICP-Agent-Identities-IIdentity 'EdjCase.ICP.Agent.Identities.IIdentity')
  - [GetPublicKey()](#M-EdjCase-ICP-Agent-Identities-IIdentity-GetPublicKey 'EdjCase.ICP.Agent.Identities.IIdentity.GetPublicKey')
  - [GetSenderDelegations()](#M-EdjCase-ICP-Agent-Identities-IIdentity-GetSenderDelegations 'EdjCase.ICP.Agent.Identities.IIdentity.GetSenderDelegations')
  - [Sign(data)](#M-EdjCase-ICP-Agent-Identities-IIdentity-Sign-System-Byte[]- 'EdjCase.ICP.Agent.Identities.IIdentity.Sign(System.Byte[])')
- [IIdentityExtensions](#T-EdjCase-ICP-Agent-Identities-IIdentityExtensions 'EdjCase.ICP.Agent.Identities.IIdentityExtensions')
  - [SignContent(identity,content)](#M-EdjCase-ICP-Agent-Identities-IIdentityExtensions-SignContent-EdjCase-ICP-Agent-Identities-IIdentity,System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-Candid-Models-IHashable}- 'EdjCase.ICP.Agent.Identities.IIdentityExtensions.SignContent(EdjCase.ICP.Agent.Identities.IIdentity,System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.IHashable})')
- [InvalidCertificateException](#T-EdjCase-ICP-Agent-InvalidCertificateException 'EdjCase.ICP.Agent.InvalidCertificateException')
  - [#ctor(message)](#M-EdjCase-ICP-Agent-InvalidCertificateException-#ctor-System-String- 'EdjCase.ICP.Agent.InvalidCertificateException.#ctor(System.String)')
- [InvalidPublicKey](#T-EdjCase-ICP-Agent-InvalidPublicKey 'EdjCase.ICP.Agent.InvalidPublicKey')
  - [#ctor()](#M-EdjCase-ICP-Agent-InvalidPublicKey-#ctor 'EdjCase.ICP.Agent.InvalidPublicKey.#ctor')
- [QueryRejectedException](#T-EdjCase-ICP-Agent-QueryRejectedException 'EdjCase.ICP.Agent.QueryRejectedException')
  - [#ctor(code,message)](#M-EdjCase-ICP-Agent-QueryRejectedException-#ctor-EdjCase-ICP-Agent-Responses-RejectCode,System-String- 'EdjCase.ICP.Agent.QueryRejectedException.#ctor(EdjCase.ICP.Agent.Responses.RejectCode,System.String)')
  - [Code](#P-EdjCase-ICP-Agent-QueryRejectedException-Code 'EdjCase.ICP.Agent.QueryRejectedException.Code')
  - [Message](#P-EdjCase-ICP-Agent-QueryRejectedException-Message 'EdjCase.ICP.Agent.QueryRejectedException.Message')
  - [RejectionMessage](#P-EdjCase-ICP-Agent-QueryRejectedException-RejectionMessage 'EdjCase.ICP.Agent.QueryRejectedException.RejectionMessage')
- [QueryReply](#T-EdjCase-ICP-Agent-Responses-QueryReply 'EdjCase.ICP.Agent.Responses.QueryReply')
  - [Arg](#P-EdjCase-ICP-Agent-Responses-QueryReply-Arg 'EdjCase.ICP.Agent.Responses.QueryReply.Arg')
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
- [SignedContent](#T-EdjCase-ICP-Agent-Models-SignedContent 'EdjCase.ICP.Agent.Models.SignedContent')
  - [#ctor(content,senderPublicKey,delegations,senderSignature)](#M-EdjCase-ICP-Agent-Models-SignedContent-#ctor-System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-Candid-Models-IHashable},System-Byte[],System-Collections-Generic-List{EdjCase-ICP-Agent-Models-SignedDelegation},System-Byte[]- 'EdjCase.ICP.Agent.Models.SignedContent.#ctor(System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.IHashable},System.Byte[],System.Collections.Generic.List{EdjCase.ICP.Agent.Models.SignedDelegation},System.Byte[])')
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
  - [Create(keyToDelegateTo,delegatingIdentity,expiration,targets,senders)](#M-EdjCase-ICP-Agent-Models-SignedDelegation-Create-EdjCase-ICP-Agent-DerEncodedPublicKey,EdjCase-ICP-Agent-Identities-IIdentity,EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}- 'EdjCase.ICP.Agent.Models.SignedDelegation.Create(EdjCase.ICP.Agent.DerEncodedPublicKey,EdjCase.ICP.Agent.Identities.IIdentity,EdjCase.ICP.Candid.Models.ICTimestamp,System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal},System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal})')
  - [Create(keyToDelegateTo,signingFunc,expiration,targets,senders)](#M-EdjCase-ICP-Agent-Models-SignedDelegation-Create-EdjCase-ICP-Agent-DerEncodedPublicKey,System-Func{System-Byte[],System-Byte[]},EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}- 'EdjCase.ICP.Agent.Models.SignedDelegation.Create(EdjCase.ICP.Agent.DerEncodedPublicKey,System.Func{System.Byte[],System.Byte[]},EdjCase.ICP.Candid.Models.ICTimestamp,System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal},System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal})')
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

<a name='P-EdjCase-ICP-Agent-CallRejectedException-RejectCode'></a>
### RejectCode `property`

##### Summary

The type of rejection that occurred

<a name='P-EdjCase-ICP-Agent-CallRejectedException-RejectMessage'></a>
### RejectMessage `property`

##### Summary

The human readable message of the rejection error

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

<a name='M-EdjCase-ICP-Agent-Models-Certificate-IsValid-System-Byte[]-'></a>
### IsValid(rootPublicKey) `method`

##### Summary

Checks the validity of the certificate based off the 
specified root public key

##### Returns

True if the certificate is valid, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| rootPublicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The root public key of the internet computer network |

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

<a name='M-EdjCase-ICP-Agent-Models-CertificateDelegation-IsValid-System-Byte[]@-'></a>
### IsValid(publicKey) `method`

##### Summary

Checks if the Certificate signature is valid and
outputs the public key of the delegation

##### Returns

True if the certificate signature is valid, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [System.Byte[]@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[]@ 'System.Byte[]@') | The public key of the delegation |

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

<a name='M-EdjCase-ICP-Agent-Agents-Http-DefaultHttpClient-SendAsync-System-Net-Http-HttpRequestMessage-'></a>
### SendAsync() `method`

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

<a name='M-EdjCase-ICP-Agent-Models-Delegation-#ctor-System-Byte[],EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}-'></a>
### #ctor(publicKey,expiration,targets,senders) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The public key from the authorizing identity |
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

<a name='M-EdjCase-ICP-Agent-Models-DelegationChain-#ctor-EdjCase-ICP-Agent-DerEncodedPublicKey,System-Collections-Generic-List{EdjCase-ICP-Agent-Models-SignedDelegation}-'></a>
### #ctor(publicKey,delegations) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [EdjCase.ICP.Agent.DerEncodedPublicKey](#T-EdjCase-ICP-Agent-DerEncodedPublicKey 'EdjCase.ICP.Agent.DerEncodedPublicKey') | The public key of the identity that has delegated authority |
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

The public key of the identity that has delegated authority

<a name='M-EdjCase-ICP-Agent-Models-DelegationChain-Create-EdjCase-ICP-Agent-DerEncodedPublicKey,EdjCase-ICP-Agent-Identities-IIdentity,EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}-'></a>
### Create(keyToDelegateTo,delegatingIdentity,expiration,targets,senders) `method`

##### Summary

Creates a delegation chain from the specified keys

##### Returns

A delegation chain signed by the delegating identity

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keyToDelegateTo | [EdjCase.ICP.Agent.DerEncodedPublicKey](#T-EdjCase-ICP-Agent-DerEncodedPublicKey 'EdjCase.ICP.Agent.DerEncodedPublicKey') | The key to delegate authority to |
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

<a name='T-EdjCase-ICP-Agent-DerEncodedPublicKey'></a>
## DerEncodedPublicKey `type`

##### Namespace

EdjCase.ICP.Agent

##### Summary

A helper class to encode and decode public keys to the DER encoding

<a name='M-EdjCase-ICP-Agent-DerEncodedPublicKey-#ctor-System-Byte[]-'></a>
### #ctor(derEncodedBytes) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| derEncodedBytes | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Raw value of the DER encoded public key |

<a name='P-EdjCase-ICP-Agent-DerEncodedPublicKey-Value'></a>
### Value `property`

##### Summary

Raw value of the DER encoded public key

<a name='M-EdjCase-ICP-Agent-DerEncodedPublicKey-As-System-String-'></a>
### As() `method`

##### Summary

Converts the DER encoded key to a raw key based on the OID specified IF its has
the specified OID, otherwise throws an exception

##### Returns

Bytes of the decoded public key

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [EdjCase.ICP.Agent.InvalidPublicKey](#T-EdjCase-ICP-Agent-InvalidPublicKey 'EdjCase.ICP.Agent.InvalidPublicKey') | Throws if not a key with the specified OID |

<a name='M-EdjCase-ICP-Agent-DerEncodedPublicKey-As-System-Collections-Generic-IEnumerable{System-String}-'></a>
### As() `method`

##### Summary

Converts the DER encoded key to a raw key based on the OIDs specified IF its has
the specified OIDs, otherwise throws an exception

##### Returns

Bytes of the decoded public key

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [EdjCase.ICP.Agent.InvalidPublicKey](#T-EdjCase-ICP-Agent-InvalidPublicKey 'EdjCase.ICP.Agent.InvalidPublicKey') | Throws if not a key with the specified OIDs |

<a name='M-EdjCase-ICP-Agent-DerEncodedPublicKey-AsBls'></a>
### AsBls() `method`

##### Summary

Converts the DER encoded key to a \`BLS\` key IF a \`BLS\` key
otherwise throws an exception

##### Returns

Bytes of the \`BLS\` public key

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [EdjCase.ICP.Agent.InvalidPublicKey](#T-EdjCase-ICP-Agent-InvalidPublicKey 'EdjCase.ICP.Agent.InvalidPublicKey') | Throws if not a \`BLS\` key |

<a name='M-EdjCase-ICP-Agent-DerEncodedPublicKey-AsCose'></a>
### AsCose() `method`

##### Summary

Converts the DER encoded key to a \`COSE\` key IF a \`COSE\` key
otherwise throws an exception

##### Returns

Bytes of the \`COSE\` public key

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [EdjCase.ICP.Agent.InvalidPublicKey](#T-EdjCase-ICP-Agent-InvalidPublicKey 'EdjCase.ICP.Agent.InvalidPublicKey') | Throws if not a \`COSE\` key |

<a name='M-EdjCase-ICP-Agent-DerEncodedPublicKey-AsEd25519'></a>
### AsEd25519() `method`

##### Summary

Converts the DER encoded key to a \`Ed25519\` key IF a \`Ed25519\` key
otherwise throws an exception

##### Returns

Bytes of the \`Ed25519\` public key

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [EdjCase.ICP.Agent.InvalidPublicKey](#T-EdjCase-ICP-Agent-InvalidPublicKey 'EdjCase.ICP.Agent.InvalidPublicKey') | Throws if not a \`Ed25519\` key |

<a name='M-EdjCase-ICP-Agent-DerEncodedPublicKey-From-System-Byte[],System-String-'></a>
### From(value,oid) `method`

##### Summary

Creates a DER public key from the raw bytes of a key that has the specified OID

##### Returns

DER encoded key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Bytes of a public key |
| oid | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The OID for the DER encoding |

<a name='M-EdjCase-ICP-Agent-DerEncodedPublicKey-From-System-Byte[],System-Collections-Generic-IEnumerable{System-String}-'></a>
### From(value,oids) `method`

##### Summary

Creates a DER public key from the raw bytes of a key that has the specified OIDs

##### Returns

DER encoded key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Bytes of a public key |
| oids | [System.Collections.Generic.IEnumerable{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.String}') | The OIDs for the DER encoding |

<a name='M-EdjCase-ICP-Agent-DerEncodedPublicKey-FromBls-System-Byte[]-'></a>
### FromBls(value) `method`

##### Summary

Creates a DER public key from the raw bytes of a \`BLS\` key.

##### Returns

DER encoded key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Bytes of a \`BLS\` public key |

<a name='M-EdjCase-ICP-Agent-DerEncodedPublicKey-FromCose-System-Byte[]-'></a>
### FromCose(value) `method`

##### Summary

Creates a DER public key from the raw bytes of a \`COSE\` key.

##### Returns

DER encoded key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Bytes of a \`COSE\` public key |

<a name='M-EdjCase-ICP-Agent-DerEncodedPublicKey-FromDer-System-Byte[]-'></a>
### FromDer(value) `method`

##### Summary

Creates a DER public key from the raw bytes of a DER encoded key. Same as the constructor

##### Returns

DER encoded key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | DER encoded bytes of a public key |

<a name='M-EdjCase-ICP-Agent-DerEncodedPublicKey-FromEd25519-System-Byte[]-'></a>
### FromEd25519(value) `method`

##### Summary

Creates a DER public key from the raw bytes of a \`ED25519\` key.

##### Returns

DER encoded key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Bytes of a \`ED25519\` public key |

<a name='M-EdjCase-ICP-Agent-DerEncodedPublicKey-ToPrincipal'></a>
### ToPrincipal() `method`

##### Summary

Converts the key to a self authenticating principal value

##### Returns



##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-Agent-Identities-Ed25519Identity'></a>
## Ed25519Identity `type`

##### Namespace

EdjCase.ICP.Agent.Identities

##### Summary

An identity using a Ed25519 key

<a name='M-EdjCase-ICP-Agent-Identities-Ed25519Identity-#ctor-EdjCase-ICP-Agent-DerEncodedPublicKey,System-Byte[]-'></a>
### #ctor(publicKey,privateKey) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [EdjCase.ICP.Agent.DerEncodedPublicKey](#T-EdjCase-ICP-Agent-DerEncodedPublicKey 'EdjCase.ICP.Agent.DerEncodedPublicKey') | The public key of the identity |
| privateKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The private key of the identity |

<a name='P-EdjCase-ICP-Agent-Identities-Ed25519Identity-PrivateKey'></a>
### PrivateKey `property`

##### Summary

The private key of the identity

<a name='P-EdjCase-ICP-Agent-Identities-Ed25519Identity-PublicKey'></a>
### PublicKey `property`

##### Summary

The public key of the identity

<a name='M-EdjCase-ICP-Agent-Identities-Ed25519Identity-Create'></a>
### Create() `method`

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

<a name='T-EdjCase-ICP-Agent-Agents-Http-IHttpClient'></a>
## IHttpClient `type`

##### Namespace

EdjCase.ICP.Agent.Agents.Http

##### Summary

A simple http request interface for sending messages

<a name='M-EdjCase-ICP-Agent-Agents-Http-IHttpClient-SendAsync-System-Net-Http-HttpRequestMessage-'></a>
### SendAsync(message) `method`

##### Summary

Sends an http request and awaits a response

##### Returns

Response from the request

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.Net.Http.HttpRequestMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpRequestMessage 'System.Net.Http.HttpRequestMessage') | Http request to send |

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

Exception to indicate that the specified BLS public key is invalid

<a name='M-EdjCase-ICP-Agent-InvalidPublicKey-#ctor'></a>
### #ctor() `constructor`

##### Summary



##### Parameters

This constructor has no parameters.

<a name='T-EdjCase-ICP-Agent-QueryRejectedException'></a>
## QueryRejectedException `type`

##### Namespace

EdjCase.ICP.Agent

##### Summary

Exception for when a query to a canister is rejected/has an error

<a name='M-EdjCase-ICP-Agent-QueryRejectedException-#ctor-EdjCase-ICP-Agent-Responses-RejectCode,System-String-'></a>
### #ctor(code,message) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| code | [EdjCase.ICP.Agent.Responses.RejectCode](#T-EdjCase-ICP-Agent-Responses-RejectCode 'EdjCase.ICP.Agent.Responses.RejectCode') | The type of rejection that occurred |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The human readable message of the rejection error |

<a name='P-EdjCase-ICP-Agent-QueryRejectedException-Code'></a>
### Code `property`

##### Summary

The type of rejection that occurred

<a name='P-EdjCase-ICP-Agent-QueryRejectedException-Message'></a>
### Message `property`

##### Summary

*Inherit from parent.*

<a name='P-EdjCase-ICP-Agent-QueryRejectedException-RejectionMessage'></a>
### RejectionMessage `property`

##### Summary

The human readable message of the rejection error

<a name='T-EdjCase-ICP-Agent-Responses-QueryReply'></a>
## QueryReply `type`

##### Namespace

EdjCase.ICP.Agent.Responses

##### Summary

Wrapper object around the candid arg that is returned

<a name='P-EdjCase-ICP-Agent-Responses-QueryReply-Arg'></a>
### Arg `property`

##### Summary

The candid arg returned from a request

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

<a name='T-EdjCase-ICP-Agent-Models-SignedContent'></a>
## SignedContent `type`

##### Namespace

EdjCase.ICP.Agent.Models

##### Summary

A model containing content and the signature information of it

<a name='M-EdjCase-ICP-Agent-Models-SignedContent-#ctor-System-Collections-Generic-Dictionary{System-String,EdjCase-ICP-Candid-Models-IHashable},System-Byte[],System-Collections-Generic-List{EdjCase-ICP-Agent-Models-SignedDelegation},System-Byte[]-'></a>
### #ctor(content,senderPublicKey,delegations,senderSignature) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| content | [System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.IHashable}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,EdjCase.ICP.Candid.Models.IHashable}') | The content that is signed in the form of key value pairs |
| senderPublicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Public key used to authenticate this request, unless anonymous, then null |
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

<a name='M-EdjCase-ICP-Agent-Models-SignedDelegation-Create-EdjCase-ICP-Agent-DerEncodedPublicKey,EdjCase-ICP-Agent-Identities-IIdentity,EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}-'></a>
### Create(keyToDelegateTo,delegatingIdentity,expiration,targets,senders) `method`

##### Summary

Creates a delegation signed by the delegating identity, authorizing the public key

##### Returns

A delegation signed by the delegating identity

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keyToDelegateTo | [EdjCase.ICP.Agent.DerEncodedPublicKey](#T-EdjCase-ICP-Agent-DerEncodedPublicKey 'EdjCase.ICP.Agent.DerEncodedPublicKey') | The key to delegate authority to |
| delegatingIdentity | [EdjCase.ICP.Agent.Identities.IIdentity](#T-EdjCase-ICP-Agent-Identities-IIdentity 'EdjCase.ICP.Agent.Identities.IIdentity') | The identity that is signing the delegation |
| expiration | [EdjCase.ICP.Candid.Models.ICTimestamp](#T-EdjCase-ICP-Candid-Models-ICTimestamp 'EdjCase.ICP.Candid.Models.ICTimestamp') | How long to delegate for |
| targets | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}') | Optional. List of canister ids to limit delegation to |
| senders | [System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{EdjCase.ICP.Candid.Models.Principal}') | Optional. List of pricipals where requests can originate from |

<a name='M-EdjCase-ICP-Agent-Models-SignedDelegation-Create-EdjCase-ICP-Agent-DerEncodedPublicKey,System-Func{System-Byte[],System-Byte[]},EdjCase-ICP-Candid-Models-ICTimestamp,System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal},System-Collections-Generic-List{EdjCase-ICP-Candid-Models-Principal}-'></a>
### Create(keyToDelegateTo,signingFunc,expiration,targets,senders) `method`

##### Summary

Creates a delegation signed by the delegating identity, authorizing the public key

##### Returns

A delegation signed by the delegating identity

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| keyToDelegateTo | [EdjCase.ICP.Agent.DerEncodedPublicKey](#T-EdjCase-ICP-Agent-DerEncodedPublicKey 'EdjCase.ICP.Agent.DerEncodedPublicKey') | The key to delegate authority to |
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
