using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a lexical scope within <see cref="T:System.Diagnostics.SymbolStore.ISymbolMethod" />, providing access to the start and end offsets of the scope, as well as its child and parent scopes.</summary>
	// Token: 0x020001EF RID: 495
	[ComVisible(true)]
	public interface ISymbolScope
	{
		/// <summary>Gets the end offset of the current lexical scope.</summary>
		/// <returns>The end offset of the current lexical scope.</returns>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060018D5 RID: 6357
		int EndOffset { get; }

		/// <summary>Gets the method that contains the current lexical scope.</summary>
		/// <returns>The method that contains the current lexical scope.</returns>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060018D6 RID: 6358
		ISymbolMethod Method { get; }

		/// <summary>Gets the parent lexical scope of the current scope.</summary>
		/// <returns>The parent lexical scope of the current scope.</returns>
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060018D7 RID: 6359
		ISymbolScope Parent { get; }

		/// <summary>Gets the start offset of the current lexical scope.</summary>
		/// <returns>The start offset of the current lexical scope.</returns>
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060018D8 RID: 6360
		int StartOffset { get; }

		/// <summary>Gets the child lexical scopes of the current lexical scope.</summary>
		/// <returns>The child lexical scopes that of the current lexical scope.</returns>
		// Token: 0x060018D9 RID: 6361
		ISymbolScope[] GetChildren();

		/// <summary>Gets the local variables within the current lexical scope.</summary>
		/// <returns>The local variables within the current lexical scope.</returns>
		// Token: 0x060018DA RID: 6362
		ISymbolVariable[] GetLocals();

		/// <summary>Gets the namespaces that are used within the current scope.</summary>
		/// <returns>The namespaces that are used within the current scope.</returns>
		// Token: 0x060018DB RID: 6363
		ISymbolNamespace[] GetNamespaces();
	}
}
