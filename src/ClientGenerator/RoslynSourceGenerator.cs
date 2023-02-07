using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using ICP.ClientGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.ClientGenerator
{
	internal class RoslynSourceGenerator
	{
		public static string GenerateClientSourceCode(
			TypeName clientName,
			string baseNamespace,
			ServiceSourceCodeType desc,
			List<string>? importedNamespaces = null
		)
		{
			ClassDeclarationSyntax clientClass = GenerateClient(clientName, desc);
			NamespaceDeclarationSyntax @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(baseNamespace))
				.WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(clientClass));

			importedNamespaces ??= new List<string>();
			importedNamespaces.AddRange(new[]
			{
				"EdjCase.ICP.Agent.Agents",
				"EdjCase.ICP.Agent.Responses",
				"EdjCase.ICP.Candid.Models",
				baseNamespace + ".Models"
			});

			IEnumerable<UsingDirectiveSyntax> usingNamespaceList = importedNamespaces
				.Select(n => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(n)));

			CompilationUnitSyntax compilationUnit = SyntaxFactory.CompilationUnit()
				.WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(@namespace))
				.WithUsings(SyntaxFactory.List(usingNamespaceList))
				.NormalizeWhitespace();

			return compilationUnit.ToString();
		}

		private static ClassDeclarationSyntax GenerateClient(TypeName clientName, ServiceSourceCodeType service)
		{
			IEnumerable<MethodDeclarationSyntax> methods = service.Methods
				.Select(method => GenerateFuncMethod(method.Name, method.FuncInfo));
			return GenerateClass(clientName, methods, implementTypes: null);
		}

		private static MethodDeclarationSyntax GenerateFuncMethod(ValueName name, ServiceSourceCodeType.Func info)
		{
			BlockSyntax body = GenerateFuncMethodBody(name.PascalCaseValue, info);
			List<TypedValueName> returnTypes = info.ReturnTypes
				.Select(t => new TypedValueName(t.));
			List<TypedValueName> parameters = info.ArgTypes
				.Select(t => new TypedValueName(t.));
			return GenerateMethod(
				body: body,
				access: MethodAccessType.Public,
				isStatic: false,
				isAsync: true,
				returnTypes: returnTypes,
				name: name,
				parameters
			);
		}

		private static BlockSyntax GenerateFuncMethodBody(string methodName, ServiceSourceCodeType.Func info)
		{
			// Build arguments for conversion to CandidArg
			IEnumerable<ArgumentSyntax> fromCandidArguments = info.ArgTypes
				.Select(t =>
				{
					// `CandidTypedValue.FromObject({argX});`
					return SyntaxFactory.Argument(
						SyntaxFactory.InvocationExpression(
							SyntaxFactory.MemberAccessExpression(
								SyntaxKind.SimpleMemberAccessExpression,
								SyntaxFactory.IdentifierName("CandidTypedValue"),
								SyntaxFactory.IdentifierName("FromObject")
							)
						)
						.WithArgumentList(
							SyntaxFactory.ArgumentList(
								SyntaxFactory.SingletonSeparatedList(
									SyntaxFactory.Argument(SyntaxFactory.IdentifierName(t.Name))
								)
							)
						)
					);
				});

			// Build candid arg
			// `CandidArg arg = CandidArg.FromCandid(arg0, arg1, ...);`
			const string argName = "arg";
			VariableDeclarationSyntax argVariable = SyntaxFactory
				.VariableDeclaration(SyntaxFactory.IdentifierName(nameof(CandidArg)))
				.WithVariables(
					SyntaxFactory.SingletonSeparatedList(
						SyntaxFactory.VariableDeclarator(
							SyntaxFactory.Identifier(argName)
						)
						.WithInitializer(
							SyntaxFactory.EqualsValueClause(
								SyntaxFactory.InvocationExpression(
									SyntaxFactory.MemberAccessExpression(
										SyntaxKind.SimpleMemberAccessExpression,
										SyntaxFactory.IdentifierName(nameof(CandidArg)),
										SyntaxFactory.IdentifierName(nameof(CandidArg.FromCandid))
									)
								)
								.WithArgumentList(
									SyntaxFactory.ArgumentList(
										SyntaxFactory.SeparatedList(fromCandidArguments)
									)
								)
							)
						)
					)
				);
			InvocationExpressionSyntax apiCall;
			if (info.IsQuery)
			{
				// `QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, {methodName}, arg);`
				// `QueryReply reply = response.ThrowOrGetReply();`
				apiCall = ;

			}
			else
			{
				// `CandidArg response = this.Agent.CallAndWaitAsync(this.CanisterId, {methodName}, arg)`
				apiCall = SyntaxFactory.InvocationExpression(
					SyntaxFactory.MemberAccessExpression(
						SyntaxKind.SimpleMemberAccessExpression,
						SyntaxFactory.MemberAccessExpression(
							SyntaxKind.SimpleMemberAccessExpression,
							SyntaxFactory.ThisExpression(),
							SyntaxFactory.IdentifierName("Agent")
						),
						SyntaxFactory.IdentifierName("CallAndWaitAsync")
					)
				)
				.WithArgumentList(
					SyntaxFactory.ArgumentList(
						SyntaxFactory.SeparatedList<ArgumentSyntax>(
							new SyntaxNodeOrToken[] {
								SyntaxFactory.Argument(
									SyntaxFactory.MemberAccessExpression(
										SyntaxKind.SimpleMemberAccessExpression,
										SyntaxFactory.ThisExpression(),
										SyntaxFactory.IdentifierName("CanisterId")
									)
								),
								SyntaxFactory.Token(SyntaxKind.CommaToken),
								SyntaxFactory.Argument(
									SyntaxFactory.LiteralExpression(
										SyntaxKind.StringLiteralExpression,
										SyntaxFactory.Literal(methodName)
									)
								),
								SyntaxFactory.Token(SyntaxKind.CommaToken),
								SyntaxFactory.Argument(
									SyntaxFactory.IdentifierName(argName)
								)
							}
						)
					)
				);

				var responseArg =
						SyntaxFactory.VariableDeclaration(
							SyntaxFactory.IdentifierName(nameof(CandidArg))
						)
						.WithVariables(
							SyntaxFactory.SingletonSeparatedList(
								SyntaxFactory.VariableDeclarator(
									SyntaxFactory.Identifier("responseArg")
								)
								.WithInitializer(
									SyntaxFactory.EqualsValueClause(
										SyntaxFactory.AwaitExpression(apiCall)
									)
								)
							)
						);
			}
			return SyntaxFactory.Block(
				SyntaxFactory.LocalDeclarationStatement(argVariable),
				SyntaxFactory.LocalDeclarationStatement(responseArg),
				SyntaxFactory.ReturnStatement(
					SyntaxFactory.InvocationExpression(
						SyntaxFactory.MemberAccessExpression(
							SyntaxKind.SimpleMemberAccessExpression,
							SyntaxFactory.IdentifierName("responseArg"),
							SyntaxFactory.GenericName(
								SyntaxFactory.Identifier("ToObject")
							)
							.WithTypeArgumentList(
								SyntaxFactory.TypeArgumentList(
									SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
										SyntaxFactory.IdentifierName("CancelOrderReceipt")
									)
								)
							)
						)
					)
				)
			);
		}

		private static MethodDeclarationSyntax GenerateMethod(
			BlockSyntax body,
			MethodAccessType access,
			bool isStatic,
			bool isAsync,
			TypedValueName? returnType,
			string name,
			params TypedParam[] parameters)
		{
			List<TypedValueName> returnTypes = new();
			if (returnType != null)
			{
				returnTypes.Add(returnType);
			}
			return GenerateMethod(body, access, isStatic, isAsync, returnTypes, name, parameters);
		}

		private static MethodDeclarationSyntax GenerateMethod(
			BlockSyntax body,
			MethodAccessType access,
			bool isStatic,
			bool isAsync,
			List<TypedValueName> returnTypes,
			string name,
			params TypedParam[] parameters)
		{
			TypeSyntax returnType;
			if (returnTypes.Any())
			{
				if (returnTypes.Count == 1)
				{
					// Single return type
					returnType = returnTypes[0].Type.ToTypeSyntax();
				}
				else
				{
					// Tuple of return types
					returnType = SyntaxFactory.TupleType(SyntaxFactory.SeparatedList(
						returnTypes.Select(t => SyntaxFactory.TupleElement(t.Type.ToTypeSyntax()))
					));
				}
			}
			else
			{
				// `void` return type
				returnType = SyntaxFactory.IdentifierName(SyntaxFactory.Token(SyntaxKind.VoidKeyword));
			}

			if (isAsync)
			{
				// Wrap return type in Task. `Task<{returnType}>`
				returnType = SyntaxFactory.GenericName("Task".ToSyntaxIdentifier())
					.WithTypeArgumentList(
						SyntaxFactory.TypeArgumentList(
							SyntaxFactory.SingletonSeparatedList(
								returnType
							)
						)
					);
			}

			List<SyntaxToken> methodModifiers = new();

			SyntaxToken? methodAccess = access switch
			{
				MethodAccessType.None => null,
				MethodAccessType.Public => SyntaxFactory.Token(SyntaxKind.PublicKeyword),
				MethodAccessType.Private => SyntaxFactory.Token(SyntaxKind.PrivateKeyword),
				MethodAccessType.Protected => SyntaxFactory.Token(SyntaxKind.PropertyKeyword),
				_ => throw new NotImplementedException(),
			};

			if (methodAccess != null)
			{
				methodModifiers.Add(methodAccess.Value);
			}

			if (isAsync)
			{
				// Add `async` keyword to method modifiers
				methodModifiers.Add(SyntaxFactory.Token(SyntaxKind.AsyncKeyword));
			}
			if (isStatic)
			{
				// Add `static` keyword to method modifiers
				methodModifiers.Add(SyntaxFactory.Token(SyntaxKind.StaticKeyword));
			}

			IEnumerable<ParameterSyntax> @params = parameters.Select(p =>
			{
				return SyntaxFactory.Parameter(p.Name.ToSyntaxIdentifier())
				.WithType(p.Type.ToTypeSyntax());
			});
			return SyntaxFactory.MethodDeclaration(returnType, SyntaxFactory.Identifier(name))
				.WithModifiers(SyntaxFactory.TokenList(methodModifiers))
				.WithParameterList(SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(@params)))
				.WithBody(body);
		}

		private static ClassDeclarationSyntax GenerateClass(
			TypeName name,
			IEnumerable<MethodDeclarationSyntax> methods,
			List<TypeName>? implementTypes = null)
		{
			// public IAgent Agent { get; }
			PropertyDeclarationSyntax agentProperty = SyntaxFactory.PropertyDeclaration(
					SyntaxFactory.IdentifierName("IAgent"),
					SyntaxFactory.Identifier("Agent")
				)
				.WithModifiers(SyntaxFactory.TokenList(
					SyntaxFactory.Token(SyntaxKind.PublicKeyword)
				))
				.WithAccessorList(SyntaxFactory.AccessorList(
					SyntaxFactory.SingletonList(
						SyntaxFactory.AccessorDeclaration(
							SyntaxKind.GetAccessorDeclaration
						)
						.WithSemicolonToken(
							SyntaxFactory.Token(SyntaxKind.SemicolonToken)
						)
					)
				));

			// public Principal CanisterId { get; }
			PropertyDeclarationSyntax canisterIdProperty = SyntaxFactory.PropertyDeclaration(
					SyntaxFactory.IdentifierName("Principal"),
					SyntaxFactory.Identifier("CanisterId")
				)
				.WithModifiers(
					SyntaxFactory.TokenList(
						SyntaxFactory.Token(SyntaxKind.PublicKeyword)
					)
				)
				.WithAccessorList(
					SyntaxFactory.AccessorList(
						SyntaxFactory.SingletonList<AccessorDeclarationSyntax>(
							SyntaxFactory.AccessorDeclaration(
								SyntaxKind.GetAccessorDeclaration
							)
							.WithSemicolonToken(
								SyntaxFactory.Token(SyntaxKind.SemicolonToken)
							)
						)
					)
				);
			var body =
					SyntaxFactory.Block(
						SyntaxFactory.ExpressionStatement(
							SyntaxFactory.AssignmentExpression(
								SyntaxKind.SimpleAssignmentExpression,
								SyntaxFactory.MemberAccessExpression(
									SyntaxKind.SimpleMemberAccessExpression,
									SyntaxFactory.ThisExpression(),
									SyntaxFactory.IdentifierName("Agent")
								),
								SyntaxFactory.BinaryExpression(
									SyntaxKind.CoalesceExpression,
									SyntaxFactory.IdentifierName("agent"),
									SyntaxFactory.ThrowExpression(
										SyntaxFactory.ObjectCreationExpression(
											SyntaxFactory.IdentifierName("ArgumentNullException")
										)
										.WithArgumentList(
											SyntaxFactory.ArgumentList(
												SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
													SyntaxFactory.Argument(
														SyntaxFactory.InvocationExpression(
															SyntaxFactory.IdentifierName(
																SyntaxFactory.Identifier(
																	SyntaxFactory.TriviaList(),
																	SyntaxKind.NameOfKeyword,
																	"nameof",
																	"nameof",
																	SyntaxFactory.TriviaList()
																)
															)
														)
														.WithArgumentList(
															SyntaxFactory.ArgumentList(
																SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
																	SyntaxFactory.Argument(
																		SyntaxFactory.IdentifierName("agent")
																	)
																)
															)
														)
													)
												)
											)
										)
									)
								)
							)
						),
						SyntaxFactory.ExpressionStatement(
							SyntaxFactory.AssignmentExpression(
								SyntaxKind.SimpleAssignmentExpression,
								SyntaxFactory.MemberAccessExpression(
									SyntaxKind.SimpleMemberAccessExpression,
									SyntaxFactory.ThisExpression(),
									SyntaxFactory.IdentifierName("CanisterId")
								),
								SyntaxFactory.BinaryExpression(
									SyntaxKind.CoalesceExpression,
									SyntaxFactory.IdentifierName("canisterId"),
									SyntaxFactory.ThrowExpression(
										SyntaxFactory.ObjectCreationExpression(
											SyntaxFactory.IdentifierName("ArgumentNullException")
										)
										.WithArgumentList(
											SyntaxFactory.ArgumentList(
												SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
													SyntaxFactory.Argument(
														SyntaxFactory.InvocationExpression(
															SyntaxFactory.IdentifierName(
																SyntaxFactory.Identifier(
																	SyntaxFactory.TriviaList(),
																	SyntaxKind.NameOfKeyword,
																	"nameof",
																	"nameof",
																	SyntaxFactory.TriviaList()
																)
															)
														)
														.WithArgumentList(
															SyntaxFactory.ArgumentList(
																SyntaxFactory.SingletonSeparatedList<ArgumentSyntax>(
																	SyntaxFactory.Argument(
																		SyntaxFactory.IdentifierName("canisterId")
																	)
																)
															)
														)
													)
												)
											)
										)
									)
								)
							)
						)
					);
			ConstructorDeclarationSyntax constructorMethod = SyntaxFactory.ConstructorDeclaration(
					SyntaxFactory.Identifier(name.GetName())
				)
				.WithModifiers(
					SyntaxFactory.TokenList(
						SyntaxFactory.Token(SyntaxKind.PublicKeyword)
					)
				)
				.WithParameterList(
					SyntaxFactory.ParameterList(
						SyntaxFactory.SeparatedList<ParameterSyntax>(
							new SyntaxNodeOrToken[]{
								SyntaxFactory.Parameter(
									SyntaxFactory.Identifier("agent")
								)
								.WithType<IAgent>(),
								SyntaxFactory.Token(SyntaxKind.CommaToken),
								SyntaxFactory.Parameter(
									SyntaxFactory.Identifier("canisterId")
								)
								.WithType<Principal>()
							}
						)
					)
				)
				.WithBody(body);

			ClassDeclarationSyntax classSyntax = SyntaxFactory.ClassDeclaration(name.GetName())
				.WithModifiers(SyntaxFactory.TokenList(
					// Make class public
					SyntaxFactory.Token(SyntaxKind.PublicKeyword)
				))
				.WithMembers(SyntaxFactory.List(
					new MemberDeclarationSyntax[]{
						agentProperty,
						canisterIdProperty,
						constructorMethod,
					}
					.Concat(methods)
				));


			if (implementTypes?.Any() == true)
			{
				classSyntax = classSyntax
					.WithBaseList(
						SyntaxFactory.BaseList(
							SyntaxFactory.SeparatedList(
								implementTypes
									.Select(t => SyntaxFactory.SimpleBaseType(SyntaxFactory.IdentifierName(t.GetNamespacedName())))
									.Cast<BaseTypeSyntax>()
									.ToArray()
							)
						)
					);
			}
			return classSyntax;

		}

		private class TypedParam
		{
			public string Type { get; }
			public string Name { get; }
			public string? OptType { get; }

			public TypedParam(string type, string name, string? optType = null)
			{
				this.Type = type ?? throw new ArgumentNullException(nameof(type));
				this.Name = name ?? throw new ArgumentNullException(nameof(name));
				this.OptType = optType;
			}

			public static TypedParam FromType(TypeName type, ValueName name)
			{
				return new TypedParam(type.GetNamespacedName(), name.CamelCaseValue);
			}
		}

		private enum MethodAccessType
		{
			None,
			Public,
			Private,
			Protected
		}
	}
}
