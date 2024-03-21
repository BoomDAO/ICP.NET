using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Parsers;
using System.Collections.Generic;
using Xunit;
namespace EdjCase.ICP.Candid.Tests
{
	public class CandidTextParsingTests
	{
		[Fact]
		public void Parse_Func_1()
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
			text = text.Replace("\r", "");
			CandidId proposalReferenceId = CandidId.Create("rec_1");
			List<FuncMode> expectedModes = new List<FuncMode> { FuncMode.Query };
			List<CandidType> expectedArgTypes = new()
			{
				new CandidPrimitiveType(PrimitiveType.Nat64)
			};
			List<CandidType> expectedReturnTypes = new()
			{
				new CandidOptionalType(
					new CandidRecordType(
						new Dictionary<CandidTag, CandidType>
						{
							{
								CandidTag.FromName("id"),
								new CandidOptionalType(
									new CandidRecordType(
										new Dictionary<CandidTag, CandidType>
										{
											{CandidTag.FromName("id"), new CandidPrimitiveType(PrimitiveType.Nat64) }
										}
									)
								)
							},
							{
								CandidTag.FromName("status"),
								new CandidPrimitiveType(PrimitiveType.Int32)
							},
							{
								CandidTag.FromName("topic"),
								new CandidPrimitiveType(PrimitiveType.Int32)
							},
							{
								CandidTag.FromName("failure_reason"),
								new CandidOptionalType(
									new CandidRecordType(
										new Dictionary<CandidTag, CandidType>
										{
											{ CandidTag.FromName("error_message"), new CandidPrimitiveType(PrimitiveType.Text) },
											{ CandidTag.FromName("error_type"), new CandidPrimitiveType(PrimitiveType.Int32) }
										}
									)
								)
							},
							{
								CandidTag.FromName("ballots"),
								new CandidVectorType(
									new CandidRecordType(
										new Dictionary<CandidTag, CandidType>
										{
											{
												CandidTag.FromId(0),
												new CandidPrimitiveType(PrimitiveType.Nat64) },
											{
												CandidTag.FromId(1),
												new CandidRecordType(
													new Dictionary<CandidTag, CandidType>
													{
														{ CandidTag.FromName("vote"), new CandidPrimitiveType(PrimitiveType.Int32) },
														{ CandidTag.FromName("voting_power"), new CandidPrimitiveType(PrimitiveType.Nat64) }
													}
												)
											}
										}
									)
								)
							},
							{
								CandidTag.FromName("proposal_timestamp_seconds"),
								new CandidPrimitiveType(PrimitiveType.Nat64)
							},
							{
								CandidTag.FromName("reward_event_round"),
								new CandidPrimitiveType(PrimitiveType.Nat64)
							},
							{
								CandidTag.FromName("deadline_timestamp_seconds"),
								new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Nat64))
							},
							{
								CandidTag.FromName("failed_timestamp_seconds"),
								new CandidPrimitiveType(PrimitiveType.Nat64)
							},
							{
								CandidTag.FromName("reject_cost_e8s"),
								new CandidPrimitiveType(PrimitiveType.Nat64)
							},
							{
								CandidTag.FromName("latest_tally"),
								new CandidOptionalType(
									new CandidRecordType(new Dictionary<CandidTag, CandidType>
									{
										{ CandidTag.FromName("no"), new CandidPrimitiveType(PrimitiveType.Nat64) },
										{ CandidTag.FromName("yes"), new CandidPrimitiveType(PrimitiveType.Nat64) },
										{ CandidTag.FromName("total"), new CandidPrimitiveType(PrimitiveType.Nat64) },
										{ CandidTag.FromName("timestamp_seconds"), new CandidPrimitiveType(PrimitiveType.Nat64) },
									})
								)
							},
							{
								CandidTag.FromName("reward_status"),
								new CandidPrimitiveType(PrimitiveType.Int32)
							},
							{
								CandidTag.FromName("decided_timestamp_seconds"),
								new CandidPrimitiveType(PrimitiveType.Nat64)
							},
							{
								CandidTag.FromName("proposal"),
								new CandidOptionalType(
									new CandidRecordType(new Dictionary<CandidTag, CandidType>
									{
										{
											CandidTag.FromName("url"),
											new CandidPrimitiveType(PrimitiveType.Text)
										},
										{
											CandidTag.FromName("title"),
											new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Text))
										},
										{
											CandidTag.FromName("action"),
											new CandidOptionalType(
												new CandidVariantType(new Dictionary<CandidTag, CandidType>
												{
													{
														CandidTag.FromName("RegisterKnownNeuron"),
														new CandidRecordType(new Dictionary<CandidTag, CandidType>
														{
															{
																CandidTag.FromName("id"),
																new CandidOptionalType(
																	new CandidRecordType(new Dictionary<CandidTag, CandidType>
																	{
																		{ CandidTag.FromName("id"), new CandidPrimitiveType(PrimitiveType.Nat64) }
																	})
																)
															},
															{
																CandidTag.FromName("known_neuron_data"),
																new CandidOptionalType(
																	new CandidRecordType(new Dictionary<CandidTag, CandidType>
																	{
																		{ CandidTag.FromName("name"), new CandidPrimitiveType(PrimitiveType.Text) },
																		{ CandidTag.FromName("description"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Text)) }
																	})
																)
															}
														})
													},
													{
														CandidTag.FromName("ManageNeuron"),
														new CandidRecordType(new Dictionary<CandidTag, CandidType>
														{
															{
																CandidTag.FromName("id"),
																new CandidOptionalType(
																	new CandidRecordType(new Dictionary<CandidTag, CandidType>
																	{
																		{ CandidTag.FromName("id"), new CandidPrimitiveType(PrimitiveType.Nat64) }
																	})
																)
															},
															{
																CandidTag.FromName("command"),
																new CandidOptionalType(
																	new CandidVariantType(new Dictionary<CandidTag, CandidType>
																	{
																		{
																			CandidTag.FromName("Spawn"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{ CandidTag.FromName("percentage_to_spawn"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Nat32)) },
																				{ CandidTag.FromName("new_controller"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Principal)) },
																				{ CandidTag.FromName("nonce"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Nat64)) },
																			})
																		},
																		{
																			CandidTag.FromName("Split"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{ CandidTag.FromName("amount_e8s"), new CandidPrimitiveType(PrimitiveType.Nat64) }
																			})
																		},
																		{
																			CandidTag.FromName("Follow"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{ CandidTag.FromName("topic"), new CandidPrimitiveType(PrimitiveType.Int32) },
																				{
																					CandidTag.FromName("followees"),
																					new CandidVectorType(new CandidRecordType(new Dictionary<CandidTag, CandidType>
																					{
																						{ CandidTag.FromName("id"), new CandidPrimitiveType(PrimitiveType.Nat64) }
																					}))
																				},
																			})
																		},
																		{
																			CandidTag.FromName("ClaimOrRefresh"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{
																					CandidTag.FromName("by"),
																					new CandidOptionalType(new CandidVariantType(new Dictionary<CandidTag, CandidType>
																					{
																						{ CandidTag.FromName("NeuronIdOrSubaccount"), new CandidRecordType(new Dictionary<CandidTag, CandidType>()) },
																						{
																							CandidTag.FromName("MemoAndController"),
																							new CandidRecordType(new Dictionary<CandidTag, CandidType>
																							{
																								{ CandidTag.FromName("controller"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Principal)) },
																								{ CandidTag.FromName("memo"), new CandidPrimitiveType(PrimitiveType.Nat64) }
																							})
																						},
																						{ CandidTag.FromName("Memo"), new CandidPrimitiveType(PrimitiveType.Nat64) }
																					}))
																				},
																			})
																		},
																		{
																			CandidTag.FromName("Configure"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{
																					CandidTag.FromName("operation"),
																					new CandidOptionalType(new CandidVariantType(new Dictionary<CandidTag, CandidType>
																					{
																						{
																							CandidTag.FromName("RemoveHotKey"),
																							new CandidRecordType(new Dictionary<CandidTag, CandidType>
																							{
																								{ CandidTag.FromName("hot_key_to_remove"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Principal)) }
																							})
																						},
																						{
																							CandidTag.FromName("AddHotKey"),
																							new CandidRecordType(new Dictionary<CandidTag, CandidType>
																							{
																								{ CandidTag.FromName("new_hot_key"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Principal)) }
																							})
																						},
																						{
																							CandidTag.FromName("StopDissolving"),
																							new CandidRecordType(new Dictionary<CandidTag, CandidType>())
																						},
																						{
																							CandidTag.FromName("StartDissolving"),
																							new CandidRecordType(new Dictionary<CandidTag, CandidType>())
																						},
																						{
																							CandidTag.FromName("IncreaseDissolveDelay"),
																							new CandidRecordType(new Dictionary<CandidTag, CandidType>
																							{
																								{ CandidTag.FromName("additional_dissolve_delay_seconds"), new CandidPrimitiveType(PrimitiveType.Nat32) }
																							})
																						},
																						{
																							CandidTag.FromName("JoinCommunityFund"),
																							new CandidRecordType(new Dictionary<CandidTag, CandidType>())
																						},
																						{
																							CandidTag.FromName("SetDissolveTimestamp"),
																							new CandidRecordType(new Dictionary<CandidTag, CandidType>
																							{
																								{ CandidTag.FromName("dissolve_timestamp_seconds"), new CandidPrimitiveType(PrimitiveType.Nat64) }
																							})
																						},
																					}))
																				},
																			})
																		},
																		{
																			CandidTag.FromName("RegisterVote"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{ CandidTag.FromName("vote"), new CandidPrimitiveType(PrimitiveType.Int32) },
																				{
																					CandidTag.FromName("proposal"),
																					new CandidOptionalType(
																						new CandidRecordType(new Dictionary<CandidTag, CandidType>
																						{
																							{ CandidTag.FromName("id"), new CandidPrimitiveType(PrimitiveType.Nat64) }
																						})
																					)
																				},
																			})
																		},
																		{
																			CandidTag.FromName("Merge"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{
																					CandidTag.FromName("source_neuron_id"),
																					new CandidOptionalType(
																						new CandidRecordType(new Dictionary<CandidTag, CandidType>
																						{
																							{ CandidTag.FromName("id"), new CandidPrimitiveType(PrimitiveType.Nat64) }
																						})
																					)
																				},
																			})
																		},
																		{
																			CandidTag.FromName("DisburseToNeuron"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{ CandidTag.FromName("dissolve_delay_seconds"), new CandidPrimitiveType(PrimitiveType.Nat64) },
																				{ CandidTag.FromName("kyc_verified"), new CandidPrimitiveType(PrimitiveType.Bool) },
																				{ CandidTag.FromName("amount_e8s"), new CandidPrimitiveType(PrimitiveType.Nat64) },
																				{ CandidTag.FromName("new_controller"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Principal)) },
																				{ CandidTag.FromName("nonce"), new CandidPrimitiveType(PrimitiveType.Nat64) },
																			})
																		},
																		{
																			CandidTag.FromName("MakeProposal"),
																			new CandidReferenceType(proposalReferenceId)
																		},
																		{
																			CandidTag.FromName("MergeMaturity"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{ CandidTag.FromName("percentage_to_merge"), new CandidPrimitiveType(PrimitiveType.Nat32) },
																			})
																		},
																		{
																			CandidTag.FromName("Disburse"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{
																					CandidTag.FromName("to_account"),
																					new CandidOptionalType(
																						new CandidRecordType(new Dictionary<CandidTag, CandidType>
																						{
																							{ CandidTag.FromName("hash"), new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Nat8)) }
																						})
																					)
																				},
																				{
																					CandidTag.FromName("amount"),
																					new CandidOptionalType(
																						new CandidRecordType(new Dictionary<CandidTag, CandidType>
																						{
																							{ CandidTag.FromName("e8s"), new CandidPrimitiveType(PrimitiveType.Nat64) }
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
																new CandidOptionalType(
																	new CandidVariantType(new Dictionary<CandidTag, CandidType>
																	{
																		{ CandidTag.FromName("Subaccount"), new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Nat8)) },
																		{
																			CandidTag.FromName("NeuronId"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{ CandidTag.FromName("id"), new CandidPrimitiveType(PrimitiveType.Nat64) }
																			})
																		},
																	})
																)
															},
														})
													},
													{
														CandidTag.FromName("ExecuteNnsFunction"),
														new CandidRecordType(new Dictionary<CandidTag, CandidType>
														{
															{
																CandidTag.FromName("nns_function"),
																new CandidPrimitiveType(PrimitiveType.Int32)
															},
															{
																CandidTag.FromName("payload"),
																new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Nat8))
															}
														})
													},
													{
														CandidTag.FromName("RewardNodeProvider"),
														new CandidRecordType(new Dictionary<CandidTag, CandidType>
														{
															{
																CandidTag.FromName("node_provider"),
																new CandidOptionalType(
																	new CandidRecordType(new Dictionary<CandidTag, CandidType>
																	{
																		{ CandidTag.FromName("id"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Principal)) },
																		{
																			CandidTag.FromName("reward_account"),
																			new CandidOptionalType(new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{ CandidTag.FromName("hash"), new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Nat8)) }
																			}))
																		},
																	})
																)
															},
															{
																CandidTag.FromName("reward_mode"),
																new CandidOptionalType(
																	new CandidVariantType(new Dictionary<CandidTag, CandidType>
																	{
																		{
																			CandidTag.FromName("RewardToNeuron"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{ CandidTag.FromName("dissolve_delay_seconds"), new CandidPrimitiveType(PrimitiveType.Nat64) }
																			})
																		},
																		{
																			CandidTag.FromName("RewardToAccount"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{
																					CandidTag.FromName("to_account"),
																					new CandidOptionalType(new CandidRecordType(new Dictionary<CandidTag, CandidType>
																					{
																						{ CandidTag.FromName("hash"), new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Nat8)) }
																					}))
																				}
																			})
																		},
																	})
																)
															},
															{
																CandidTag.FromName("amount_e8s"),
																new CandidPrimitiveType(PrimitiveType.Nat64)
															}
														})
													},
													{
														CandidTag.FromName("SetDefaultFollowees"),
														new CandidRecordType(new Dictionary<CandidTag, CandidType>
														{
															{
																CandidTag.FromName("default_followees"),
																new CandidVectorType(
																	new CandidRecordType(new Dictionary<CandidTag, CandidType>
																	{
																		{ CandidTag.FromName("_0_"), new CandidPrimitiveType(PrimitiveType.Int32) },
																		{
																			CandidTag.FromName("_1_"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{
																					CandidTag.FromName("followees"),
																					new CandidVectorType(
																						new CandidRecordType(new Dictionary<CandidTag, CandidType>
																						{
																							{ CandidTag.FromName("id"), new CandidPrimitiveType(PrimitiveType.Nat64) }
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
														new CandidRecordType(new Dictionary<CandidTag, CandidType>
														{
															{ CandidTag.FromName("use_registry_derived_rewards"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Bool)) },
															{
																CandidTag.FromName("rewards"),
																new CandidVectorType(
																	new CandidRecordType(new Dictionary<CandidTag, CandidType>
																	{
																		{
																			CandidTag.FromName("node_provider"),
																			new CandidOptionalType(
																				new CandidRecordType(new Dictionary<CandidTag, CandidType>
																				{
																					{ CandidTag.FromName("id"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Principal)) },
																					{
																						CandidTag.FromName("reward_account"),
																						new CandidOptionalType(new CandidRecordType(new Dictionary<CandidTag, CandidType>
																						{
																							{ CandidTag.FromName("hash"), new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Nat8)) }
																						}))
																					},
																				})
																			)
																		},
																		{
																			CandidTag.FromName("reward_mode"),
																			new CandidOptionalType(
																				new CandidVariantType(new Dictionary<CandidTag, CandidType>
																				{
																					{
																						CandidTag.FromName("RewardToNeuron"),
																						new CandidRecordType(new Dictionary<CandidTag, CandidType>
																						{
																							{ CandidTag.FromName("dissolve_delay_seconds"), new CandidPrimitiveType(PrimitiveType.Nat64) }
																						})
																					},
																					{
																						CandidTag.FromName("RewardToAccount"),
																						new CandidRecordType(new Dictionary<CandidTag, CandidType>
																						{
																							{
																								CandidTag.FromName("to_account"),
																								new CandidOptionalType(new CandidRecordType(new Dictionary<CandidTag, CandidType>
																								{
																									{ CandidTag.FromName("hash"), new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Nat8)) }
																								}))
																							}
																						})
																					},
																				})
																			)
																		},
																		{ CandidTag.FromName("amount_e8s"), new CandidPrimitiveType(PrimitiveType.Nat64) }
																	})
																)
															}
														})
													},
													{
														CandidTag.FromName("ManageNetworkEconomics"),
														new CandidRecordType(new Dictionary<CandidTag, CandidType>
														{
															{ CandidTag.FromName("neuron_minimum_stake_e8s"), new CandidPrimitiveType(PrimitiveType.Nat64) },
															{ CandidTag.FromName("max_proposals_to_keep_per_topic"), new CandidPrimitiveType(PrimitiveType.Nat32) },
															{ CandidTag.FromName("neuron_management_fee_per_proposal_e8s"), new CandidPrimitiveType(PrimitiveType.Nat64) },
															{ CandidTag.FromName("reject_cost_e8s"), new CandidPrimitiveType(PrimitiveType.Nat64) },
															{ CandidTag.FromName("transaction_fee_e8s"), new CandidPrimitiveType(PrimitiveType.Nat64) },
															{ CandidTag.FromName("neuron_spawn_dissolve_delay_seconds"), new CandidPrimitiveType(PrimitiveType.Nat64) },
															{ CandidTag.FromName("minimum_icp_xdr_rate"), new CandidPrimitiveType(PrimitiveType.Nat64) },
															{ CandidTag.FromName("maximum_node_provider_rewards_e8s"), new CandidPrimitiveType(PrimitiveType.Nat64) }
														})
													},
													{
														CandidTag.FromName("ApproveGenesisKyc"),
														new CandidRecordType(new Dictionary<CandidTag, CandidType>
														{
															{ CandidTag.FromName("principals"), new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Principal)) },
														})
													},
													{
														CandidTag.FromName("AddOrRemoveNodeProvider"),
														new CandidRecordType(new Dictionary<CandidTag, CandidType>
														{
															{
																CandidTag.FromName("change"),
																new CandidOptionalType(
																	new CandidVariantType(new Dictionary<CandidTag, CandidType>
																	{
																		{
																			CandidTag.FromName("ToRemove"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{ CandidTag.FromName("id"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Principal)) },
																				{
																					CandidTag.FromName("reward_account"),
																					new CandidOptionalType(
																						new CandidRecordType(new Dictionary<CandidTag, CandidType>
																						{
																							{ CandidTag.FromName("hash"), new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Nat8)) }
																						})
																					)
																				},
																			})
																		},
																		{
																			CandidTag.FromName("ToAdd"),
																			new CandidRecordType(new Dictionary<CandidTag, CandidType>
																			{
																				{ CandidTag.FromName("id"), new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Principal)) },
																				{
																					CandidTag.FromName("reward_account"),
																					new CandidOptionalType(
																						new CandidRecordType(new Dictionary<CandidTag, CandidType>
																						{
																							{ CandidTag.FromName("hash"), new CandidVectorType(new CandidPrimitiveType(PrimitiveType.Nat8)) }
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
														new CandidRecordType(new Dictionary<CandidTag, CandidType>
														{
															{ CandidTag.FromName("motion_text"), new CandidPrimitiveType(PrimitiveType.Text) },
														})
													},
												})
											)
										},
										{
											CandidTag.FromName("summary"),
											new CandidPrimitiveType(PrimitiveType.Text)
										},
									}, proposalReferenceId)
								)
							},
							{
								CandidTag.FromName("proposer"),
								new CandidOptionalType(
									new CandidRecordType(new Dictionary<CandidTag, CandidType>
									{
										{ CandidTag.FromName("id"), new CandidPrimitiveType(PrimitiveType.Nat64) }
									})
								)
							},
							{
								CandidTag.FromName("executed_timestamp_seconds"),
								new CandidPrimitiveType(PrimitiveType.Nat64)
							},
						}
					)
				)
			};
			CandidFuncType expectedFunc = new CandidFuncType(expectedModes, expectedArgTypes, expectedReturnTypes);
			CandidFuncType func = CandidTextParser.Parse<CandidFuncType>(text);

			Assert.Equal(expectedFunc, func);
			string generatedText = CandidTextGenerator.Generate(func, CandidTextGenerator.IndentType.Spaces_2);
			Assert.Equal(text, generatedText);
		}
	}
}
