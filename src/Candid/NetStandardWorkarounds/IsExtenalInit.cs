// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if NETSTANDARD2_1

using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices
{
	/// <summary>
	/// Reserved to be used by the compiler for tracking metadata.
	/// This class should not be used by developers in source code.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static class IsExternalInit
	{
	}
}

#endif