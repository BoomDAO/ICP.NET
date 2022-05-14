using ICP.Candid.Models;
using ICP.Candid.Models.Types;
using ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ICP.Candid.Tests
{
    public class CandidTextParsingTests
    {
        [Fact]
        public void Parse_Func()
        {
            string text = @"(
    nat64
) -> (
    opt record {
        id:opt record {
            id:nat64
        };
        status:int32;
        topic:int32;
        failure_reason:opt record {
            error_message:text;
            error_type:int32
        };
        ballots:vec record {
            nat64;
            record {
                vote:int32;
                voting_power:nat64
            }
        };
        proposal_timestamp_seconds:nat64;
        reward_event_round:nat64;
        deadline_timestamp_seconds:opt nat64;
        failed_timestamp_seconds:nat64;
        reject_cost_e8s:nat64;
        latest_tally:opt record {
            no:nat64;
            yes:nat64;
            total:nat64;
            timestamp_seconds:nat64
        };
        reward_status:int32;
        decided_timestamp_seconds:nat64;
        proposal:opt μrec_1.record {
            url:text;
            title:opt text;
            action:opt variant {
                RegisterKnownNeuron:record {
                    id:opt record {
                        id:nat64
                    };
                    known_neuron_data:opt record {
                        name:text;
                        description:opt text
                    }
                };
                ManageNeuron:record {
                    id:opt record {
                        id:nat64
                    };
                    command:opt variant {
                        Spawn:record {
                            percentage_to_spawn:opt nat32;
                            new_controller:opt principal;
                            nonce:opt nat64
                        };
                        Split:record {
                            amount_e8s:nat64
                        };
                        Follow:record {
                            topic:int32;
                            followees:vec record {
                                id:nat64
                            }
                        };
                        ClaimOrRefresh:record {
                            by:opt variant {
                                NeuronIdOrSubaccount:record {
                                };
                                MemoAndController:record {
                                    controller:opt principal;
                                    memo:nat64
                                };
                                Memo:nat64
                            }
                        };
                        Configure:record {
                            operation:opt variant {
                                RemoveHotKey:record {
                                    hot_key_to_remove:opt principal
                                };
                                AddHotKey:record {
                                    new_hot_key:opt principal
                                };
                                StopDissolving:record {
                                };
                                StartDissolving:record {
                                };
                                IncreaseDissolveDelay:record {
                                    additional_dissolve_delay_seconds:nat32
                                };
                                JoinCommunityFund:record {
                                };
                                SetDissolveTimestamp:record {
                                    dissolve_timestamp_seconds:nat64
                                }
                            }
                        };
                        RegisterVote:record {
                            vote:int32;
                            proposal:opt record {
                                id:nat64
                            }
                        };
                        Merge:record {
                            source_neuron_id:opt record {
                                id:nat64
                            }
                        };
                        DisburseToNeuron:record {
                            dissolve_delay_seconds:nat64;
                            kyc_verified:bool;
                            amount_e8s:nat64;
                            new_controller:opt principal;
                            nonce:nat64
                        };
                        MakeProposal:rec_1;
                        MergeMaturity:record {
                            percentage_to_merge:nat32
                        };
                        Disburse:record {
                            to_account:opt record {
                                hash:vec nat8
                            };
                            amount:opt record {
                                e8s:nat64
                            }
                        }
                    };
                    neuron_id_or_subaccount:opt variant {
                        Subaccount:vec nat8;
                        NeuronId:record {
                            id:nat64
                        }
                    }
                };
                ExecuteNnsFunction:record {
                    nns_function:int32;
                    payload:vec nat8
                };
                RewardNodeProvider:record {
                    node_provider:opt record {
                        id:opt principal;
                        reward_account:opt record {
                            hash:vec nat8
                        }
                    };
                    reward_mode:opt variant {
                        RewardToNeuron:record {
                            dissolve_delay_seconds:nat64
                        };
                        RewardToAccount:record {
                            to_account:opt record {
                                hash:vec nat8
                            }
                        }
                    };
                    amount_e8s:nat64
                };
                SetDefaultFollowees:record {
                    default_followees:vec record {
                        _0_:int32;
                        _1_:record {
                            followees:vec record {
                                id:nat64
                            }
                        }
                    }
                };
                RewardNodeProviders:record {
                    use_registry_derived_rewards:opt bool;
                    rewards:vec record {
                        node_provider:opt record {
                            id:opt principal;
                            reward_account:opt record {
                                hash:vec nat8
                            }
                        };
                        reward_mode:opt variant {
                            RewardToNeuron:record {
                                dissolve_delay_seconds:nat64
                            };
                            RewardToAccount:record {
                                to_account:opt record {
                                    hash:vec nat8
                                }
                            }
                        };
                        amount_e8s:nat64
                    }
                };
                ManageNetworkEconomics:record {
                    neuron_minimum_stake_e8s:nat64;
                    max_proposals_to_keep_per_topic:nat32;
                    neuron_management_fee_per_proposal_e8s:nat64;
                    reject_cost_e8s:nat64;
                    transaction_fee_e8s:nat64;
                    neuron_spawn_dissolve_delay_seconds:nat64;
                    minimum_icp_xdr_rate:nat64;
                    maximum_node_provider_rewards_e8s:nat64
                };
                ApproveGenesisKyc:record {
                    principals:vec principal
                };
                AddOrRemoveNodeProvider:record {
                    change:opt variant {
                        ToRemove:record {
                            id:opt principal;
                            reward_account:opt record {
                                hash:vec nat8
                            }
                        };
                        ToAdd:record {
                            id:opt principal;
                            reward_account:opt record {
                                hash:vec nat8
                            }
                        }
                    }
                };
                Motion:record {
                    motion_text:text
                }
            };
            summary:text
        };
        proposer:opt record {
            id:nat64
        };
        executed_timestamp_seconds:nat64
    }
) query";
            CandidId proposalReferenceId = CandidId.Parse("rec_1");
            List<FuncMode> expectedModes = new List<FuncMode> { FuncMode.Query };
            List<CandidTypeDefinition> expectedArgTypes = new()
            {
                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64)
            };
            List<CandidTypeDefinition> expectedReturnTypes = new()
            {
                new OptCandidTypeDefinition(
                    new RecordCandidTypeDefinition(
                        new Dictionary<CandidTag, CandidTypeDefinition>
                        {
                            {
                                CandidTag.FromName("id"),
                                new OptCandidTypeDefinition(
                                    new RecordCandidTypeDefinition(
                                        new Dictionary<CandidTag, CandidTypeDefinition>
                                        {
                                            {CandidTag.FromName("id"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                        }
                                    )
                                )
                            },
                            {
                                CandidTag.FromName("status"),
                                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int32)
                            },
                            {
                                CandidTag.FromName("topic"),
                                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int32)
                            },
                            {
                                CandidTag.FromName("failure_reason"),
                                new OptCandidTypeDefinition(
                                    new RecordCandidTypeDefinition(
                                        new Dictionary<CandidTag, CandidTypeDefinition>
                                        {
                                            { CandidTag.FromName("error_message"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Text) },
                                            { CandidTag.FromName("error_type"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int32) }
                                        }
                                    )
                                )
                            },
                            {
                                CandidTag.FromName("ballots"),
                                new VectorCandidTypeDefinition(
                                    new RecordCandidTypeDefinition(
                                        new Dictionary<CandidTag, CandidTypeDefinition>
                                        {
                                            {
                                                CandidTag.FromId(0),
                                                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                            {
                                                CandidTag.FromId(1),
                                                new RecordCandidTypeDefinition(
                                                    new Dictionary<CandidTag, CandidTypeDefinition>
                                                    {
                                                        { CandidTag.FromName("vote"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int32) },
                                                        { CandidTag.FromName("voting_power"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                    }
                                                )
                                            }
                                        }
                                    )
                                )
                            },
                            {
                                CandidTag.FromName("proposal_timestamp_seconds"),
                                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64)
                            },
                            {
                                CandidTag.FromName("reward_event_round"),
                                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64)
                            },
                            {
                                CandidTag.FromName("deadline_timestamp_seconds"),
                                new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64))
                            },
                            {
                                CandidTag.FromName("failed_timestamp_seconds"),
                                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64)
                            },
                            {
                                CandidTag.FromName("reject_cost_e8s"),
                                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64)
                            },
                            {
                                CandidTag.FromName("latest_tally"),
                                new OptCandidTypeDefinition(
                                    new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                    {
                                        { CandidTag.FromName("no"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                        { CandidTag.FromName("yes"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                        { CandidTag.FromName("total"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                        { CandidTag.FromName("timestamp_seconds"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                    })
                                )
                            },
                            {
                                CandidTag.FromName("reward_status"),
                                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int32)
                            },
                            {
                                CandidTag.FromName("decided_timestamp_seconds"),
                                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64)
                            },
                            {
                                CandidTag.FromName("proposal"),
                                new OptCandidTypeDefinition(
                                    new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                    {
                                        {
                                            CandidTag.FromName("url"),
                                            new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Text)
                                        },
                                        {
                                            CandidTag.FromName("title"),
                                            new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Text))
                                        },
                                        {
                                            CandidTag.FromName("action"),
                                            new OptCandidTypeDefinition(
                                                new VariantCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                {
                                                    {
                                                        CandidTag.FromName("RegisterKnownNeuron"),
                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                        {
                                                            {
                                                                CandidTag.FromName("id"),
                                                                new OptCandidTypeDefinition(
                                                                    new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                    {
                                                                        { CandidTag.FromName("id"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                    })
                                                                )
                                                            },
                                                            {
                                                                CandidTag.FromName("known_neuron_data"),
                                                                new OptCandidTypeDefinition(
                                                                    new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                    {
                                                                        { CandidTag.FromName("name"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Text) },
                                                                        { CandidTag.FromName("description"), new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Text)) }
                                                                    })
                                                                )
                                                            }
                                                        })
                                                    },
                                                    {
                                                        CandidTag.FromName("ManageNeuron"),
                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                        {
                                                            {
                                                                CandidTag.FromName("id"),
                                                                new OptCandidTypeDefinition(
                                                                    new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                    {
                                                                        { CandidTag.FromName("id"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                    })
                                                                )
                                                            },
                                                            {
                                                                CandidTag.FromName("command"),
                                                                new OptCandidTypeDefinition(
                                                                    new VariantCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                    {
                                                                        {
                                                                            CandidTag.FromName("Spawn"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                { CandidTag.FromName("percentage_to_spawn"), new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat32)) },
                                                                                { CandidTag.FromName("new_controller"), new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Principal)) },
                                                                                { CandidTag.FromName("nonce"), new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64)) },
                                                                            })
                                                                        },
                                                                        {
                                                                            CandidTag.FromName("Split"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                { CandidTag.FromName("amount_e8s"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                            })
                                                                        },
                                                                        {
                                                                            CandidTag.FromName("Follow"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                { CandidTag.FromName("topic"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int32) },
                                                                                {
                                                                                    CandidTag.FromName("followees"),
                                                                                    new VectorCandidTypeDefinition(new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                    {
                                                                                        { CandidTag.FromName("id"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                                    }))
                                                                                },
                                                                            })
                                                                        },
                                                                        {
                                                                            CandidTag.FromName("ClaimOrRefresh"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                {
                                                                                    CandidTag.FromName("by"),
                                                                                    new OptCandidTypeDefinition(new VariantCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                    {
                                                                                        { CandidTag.FromName("NeuronIdOrSubaccount"), new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>()) },
                                                                                        {
                                                                                            CandidTag.FromName("MemoAndController"),
                                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                            {
                                                                                                { CandidTag.FromName("controller"), new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Principal)) },
                                                                                                { CandidTag.FromName("memo"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                                            })
                                                                                        },
                                                                                        { CandidTag.FromName("Memo"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                                    }))
                                                                                },
                                                                            })
                                                                        },
                                                                        {
                                                                            CandidTag.FromName("Configure"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                {
                                                                                    CandidTag.FromName("operation"),
                                                                                    new OptCandidTypeDefinition(new VariantCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                    {
                                                                                        {
                                                                                            CandidTag.FromName("RemoveHotKey"),
                                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                            {
                                                                                                { CandidTag.FromName("hot_key_to_remove"), new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Principal)) }
                                                                                            })
                                                                                        },
                                                                                        {
                                                                                            CandidTag.FromName("AddHotKey"),
                                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                            {
                                                                                                { CandidTag.FromName("new_hot_key"), new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Principal)) }
                                                                                            })
                                                                                        },
                                                                                        {
                                                                                            CandidTag.FromName("StopDissolving"),
                                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>())
                                                                                        },
                                                                                        {
                                                                                            CandidTag.FromName("StartDissolving"),
                                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>())
                                                                                        },
                                                                                        {
                                                                                            CandidTag.FromName("IncreaseDissolveDelay"),
                                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                            {
                                                                                                { CandidTag.FromName("additional_dissolve_delay_seconds"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat32) }
                                                                                            })
                                                                                        },
                                                                                        {
                                                                                            CandidTag.FromName("JoinCommunityFund"),
                                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>())
                                                                                        },
                                                                                        {
                                                                                            CandidTag.FromName("SetDissolveTimestamp"),
                                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                            {
                                                                                                { CandidTag.FromName("dissolve_timestamp_seconds"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                                            })
                                                                                        },
                                                                                    }))
                                                                                },
                                                                            })
                                                                        },
                                                                        {
                                                                            CandidTag.FromName("RegisterVote"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                { CandidTag.FromName("vote"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int32) },
                                                                                {
                                                                                    CandidTag.FromName("proposal"),
                                                                                    new OptCandidTypeDefinition(
                                                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                        {
                                                                                            { CandidTag.FromName("id"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                                        })
                                                                                    )
                                                                                },
                                                                            })
                                                                        },
                                                                        {
                                                                            CandidTag.FromName("Merge"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                {
                                                                                    CandidTag.FromName("source_neuron_id"),
                                                                                    new OptCandidTypeDefinition(
                                                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                        {
                                                                                            { CandidTag.FromName("id"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                                        })
                                                                                    )
                                                                                },
                                                                            })
                                                                        },
                                                                        {
                                                                            CandidTag.FromName("DisburseToNeuron"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                { CandidTag.FromName("dissolve_delay_seconds"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                                                                { CandidTag.FromName("kyc_verified"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Bool) },
                                                                                { CandidTag.FromName("amount_e8s"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                                                                { CandidTag.FromName("new_controller"), new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Principal)) },
                                                                                { CandidTag.FromName("nonce"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                                                            })
                                                                        },
                                                                        {
                                                                            CandidTag.FromName("MakeProposal"),
                                                                            new ReferenceCandidTypeDefinition(proposalReferenceId, CandidTypeCode.Record)
                                                                        },
                                                                        {
                                                                            CandidTag.FromName("MergeMaturity"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                { CandidTag.FromName("percentage_to_merge"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat32) },
                                                                            })
                                                                        },
                                                                        {
                                                                            CandidTag.FromName("Disburse"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                {
                                                                                    CandidTag.FromName("to_account"),
                                                                                    new OptCandidTypeDefinition(
                                                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                        {
                                                                                            { CandidTag.FromName("hash"), new VectorCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat8)) }
                                                                                        })
                                                                                    )
                                                                                },
                                                                                {
                                                                                    CandidTag.FromName("amount"),
                                                                                    new OptCandidTypeDefinition(
                                                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                        {
                                                                                            { CandidTag.FromName("e8s"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                                        })
                                                                                    )
                                                                                },
                                                                            })
                                                                        },
                                                                    })
                                                                )
                                                            },
                                                            {
                                                                CandidTag.FromName("neuron_id_or_subaccount"),
                                                                new OptCandidTypeDefinition(
                                                                    new VariantCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                    {
                                                                        { CandidTag.FromName("Subaccount"), new VectorCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat8)) },
                                                                        {
                                                                            CandidTag.FromName("NeuronId"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                { CandidTag.FromName("id"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                            })
                                                                        },
                                                                    })
                                                                )
                                                            },
                                                        })
                                                    },
                                                    {
                                                        CandidTag.FromName("ExecuteNnsFunction"),
                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                        {
                                                            {
                                                                CandidTag.FromName("nns_function"),
                                                                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int32)
                                                            },
                                                            {
                                                                CandidTag.FromName("payload"),
                                                                new VectorCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat8))
                                                            }
                                                        })
                                                    },
                                                    {
                                                        CandidTag.FromName("RewardNodeProvider"),
                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                        {
                                                            {
                                                                CandidTag.FromName("node_provider"),
                                                                new OptCandidTypeDefinition(
                                                                    new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                    {
                                                                        { CandidTag.FromName("id"), new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Principal)) },
                                                                        {
                                                                            CandidTag.FromName("reward_account"),
                                                                            new OptCandidTypeDefinition(new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                { CandidTag.FromName("hash"), new VectorCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat8)) }
                                                                            }))
                                                                        },
                                                                    })
                                                                )
                                                            },
                                                            {
                                                                CandidTag.FromName("reward_mode"),
                                                                new OptCandidTypeDefinition(
                                                                    new VariantCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                    {
                                                                        {
                                                                            CandidTag.FromName("RewardToNeuron"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                { CandidTag.FromName("dissolve_delay_seconds"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                            })
                                                                        },
                                                                        {
                                                                            CandidTag.FromName("RewardToAccount"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                {
                                                                                    CandidTag.FromName("to_account"),
                                                                                    new OptCandidTypeDefinition(new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                    {
                                                                                        { CandidTag.FromName("hash"), new VectorCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat8)) }
                                                                                    }))
                                                                                }
                                                                            })
                                                                        },
                                                                    })
                                                                )
                                                            },
                                                            {
                                                                CandidTag.FromName("amount_e8s"),
                                                                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64)
                                                            }
                                                        })
                                                    },
                                                    {
                                                        CandidTag.FromName("SetDefaultFollowees"),
                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                        {
                                                            {
                                                                CandidTag.FromName("default_followees"),
                                                                new VectorCandidTypeDefinition(
                                                                    new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                    {
                                                                        { CandidTag.FromName("_0_"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int32) },
                                                                        {
                                                                            CandidTag.FromName("_1_"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                {
                                                                                    CandidTag.FromName("followees"),
                                                                                    new VectorCandidTypeDefinition(
                                                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                        {
                                                                                            { CandidTag.FromName("id"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                                        })
                                                                                    )
                                                                                }
                                                                            })
                                                                        }

                                                                    })
                                                                )
                                                            }
                                                        })
                                                    },
                                                    {
                                                        CandidTag.FromName("RewardNodeProviders"),
                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                        {
                                                            { CandidTag.FromName("use_registry_derived_rewards"), new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Bool)) },
                                                            {
                                                                CandidTag.FromName("rewards"),
                                                                new VectorCandidTypeDefinition(
                                                                    new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                    {
                                                                        {
                                                                            CandidTag.FromName("node_provider"),
                                                                            new OptCandidTypeDefinition(
                                                                                new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                {
                                                                                    { CandidTag.FromName("id"), new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Principal)) },
                                                                                    {
                                                                                        CandidTag.FromName("reward_account"),
                                                                                        new OptCandidTypeDefinition(new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                        {
                                                                                            { CandidTag.FromName("hash"), new VectorCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat8)) }
                                                                                        }))
                                                                                    },
                                                                                })
                                                                            )
                                                                        },
                                                                        {
                                                                            CandidTag.FromName("reward_mode"),
                                                                            new OptCandidTypeDefinition(
                                                                                new VariantCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                {
                                                                                    {
                                                                                        CandidTag.FromName("RewardToNeuron"),
                                                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                        {
                                                                                            { CandidTag.FromName("dissolve_delay_seconds"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                                        })
                                                                                    },
                                                                                    {
                                                                                        CandidTag.FromName("RewardToAccount"),
                                                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                        {
                                                                                            {
                                                                                                CandidTag.FromName("to_account"),
                                                                                                new OptCandidTypeDefinition(new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                                {
                                                                                                    { CandidTag.FromName("hash"), new VectorCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat8)) }
                                                                                                }))
                                                                                            }
                                                                                        })
                                                                                    },
                                                                                })
                                                                            )
                                                                        },
                                                                        { CandidTag.FromName("amount_e8s"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                                    })
                                                                )
                                                            }
                                                        })
                                                    },
                                                    {
                                                        CandidTag.FromName("ManageNetworkEconomics"),
                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                        {
                                                            { CandidTag.FromName("neuron_minimum_stake_e8s"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                                            { CandidTag.FromName("max_proposals_to_keep_per_topic"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat32) },
                                                            { CandidTag.FromName("neuron_management_fee_per_proposal_e8s"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                                            { CandidTag.FromName("reject_cost_e8s"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                                            { CandidTag.FromName("transaction_fee_e8s"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                                            { CandidTag.FromName("neuron_spawn_dissolve_delay_seconds"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                                            { CandidTag.FromName("minimum_icp_xdr_rate"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) },
                                                            { CandidTag.FromName("maximum_node_provider_rewards_e8s"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                                        })
                                                    },
                                                    {
                                                        CandidTag.FromName("ApproveGenesisKyc"),
                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                        {
                                                            { CandidTag.FromName("principals"), new VectorCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Principal)) },
                                                        })
                                                    },
                                                    {
                                                        CandidTag.FromName("AddOrRemoveNodeProvider"),
                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                        {
                                                            {
                                                                CandidTag.FromName("change"),
                                                                new OptCandidTypeDefinition(
                                                                    new VariantCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                    {
                                                                        {
                                                                            CandidTag.FromName("ToRemove"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                { CandidTag.FromName("id"), new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Principal)) },
                                                                                {
                                                                                    CandidTag.FromName("reward_account"),
                                                                                    new OptCandidTypeDefinition(
                                                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                        {
                                                                                            { CandidTag.FromName("hash"), new VectorCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat8)) }
                                                                                        })
                                                                                    )
                                                                                },
                                                                            })
                                                                        },
                                                                        {
                                                                            CandidTag.FromName("ToAdd"),
                                                                            new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                            {
                                                                                { CandidTag.FromName("id"), new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Principal)) },
                                                                                {
                                                                                    CandidTag.FromName("reward_account"),
                                                                                    new OptCandidTypeDefinition(
                                                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                                                        {
                                                                                            { CandidTag.FromName("hash"), new VectorCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat8)) }
                                                                                        })
                                                                                    )
                                                                                },
                                                                            })
                                                                        }
                                                                    }) 
                                                                )
                                                            }
                                                        })
                                                    },
                                                    {
                                                        CandidTag.FromName("Motion"),
                                                        new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                                        {
                                                            { CandidTag.FromName("motion_text"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Text) },
                                                        })
                                                    },
                                                })
                                            )
                                        },
                                        {
                                            CandidTag.FromName("summary"),
                                            new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Text)
                                        },
                                    }, proposalReferenceId)
                                )
                            },
                            {
                                CandidTag.FromName("proposer"),
                                new OptCandidTypeDefinition(
                                    new RecordCandidTypeDefinition(new Dictionary<CandidTag, CandidTypeDefinition>
                                    {
                                        { CandidTag.FromName("id"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64) }
                                    })
                                )
                            },
                            {
                                CandidTag.FromName("executed_timestamp_seconds"),
                                new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64)
                            },
                        }
                    )
                )
            };
            FuncCandidTypeDefinition expectedFunc = new FuncCandidTypeDefinition(expectedModes, expectedArgTypes, expectedReturnTypes);
            FuncCandidTypeDefinition func = CandidTextReader.ReadFunc(text);

            //Assert.Equal(expectedFunc, func);
            string generatedText = CandidTextGenerator.Generate(func, CandidTextGenerator.IndentType.Spaces_2);
            Assert.Equal(text, generatedText);
        }
    }
}
