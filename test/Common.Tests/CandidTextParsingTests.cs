using ICP.Candid.Models.Types;
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
        public void Parse()
        {
            string text = @"
(opt record {
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
        nat64; record {
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
                            NeuronIdOrSubaccount:record {};
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
                            StopDissolving:record {};
                            StartDissolving:record {};
                            IncreaseDissolveDelay:record {
                                additional_dissolve_delay_seconds:nat32
                            };
                            JoinCommunityFund:record {};
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
}) query";
            List<CandidTypeDefinition> expectedTypes = new()
            {
                new OptCandidTypeDefinition(
                    new RecordCandidTypeDefinition(
                        new Dictionary<CandidLabel, CandidTypeDefinition>
                        {
                            {
                                CandidLabel.FromName("id"),
                                new OptCandidTypeDefinition(
                                    new RecordCandidTypeDefinition(
                                        new Dictionary<CandidLabel, CandidTypeDefinition>
                                        {
                                            {CandidLabel.FromName("id"), new PrimitiveCandidTypeDefinition(Models.Values.CandidPrimitiveType.Nat64) }
                                        }
                                    )
                                )
                            },

                        }
                    )
                )
            };
            (List<CandidTypeDefinition> types, FuncMode mode) = CandidTypeDefinition.ParseArgs(text);

            Assert.Equal(expectedTypes, types);
            Assert.Equal(FuncMode.Query, mode);
        }
    }
}
