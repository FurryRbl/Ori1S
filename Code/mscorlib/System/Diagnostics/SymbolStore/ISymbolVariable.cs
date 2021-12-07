using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a variable within a symbol store.</summary>
	// Token: 0x020001F0 RID: 496
	[ComVisible(true)]
	public interface ISymbolVariable
	{
		/// <summary>Gets the first address of a variable.</summary>
		/// <returns>The first address of the variable.</returns>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060018DC RID: 6364
		int AddressField1 { get; }

		/// <summary>Gets the second address of a variable.</summary>
		/// <returns>The second address of the variable.</returns>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060018DD RID: 6365
		int AddressField2 { get; }

		/// <summary>Gets the third address of a variable.</summary>
		/// <returns>The third address of the variable.</returns>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060018DE RID: 6366
		int AddressField3 { get; }

		/// <summary>Gets the <see cref="T:System.Diagnostics.SymbolStore.SymAddressKind" /> value describing the type of the address.</summary>
		/// <returns>The type of the address. One of the <see cref="T:System.Diagnostics.SymbolStore.SymAddressKind" /> values.</returns>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060018DF RID: 6367
		SymAddressKind AddressKind { get; }

		/// <summary>Gets the attributes of the variable.</summary>
		/// <returns>The variable attributes.</returns>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060018E0 RID: 6368
		object Attributes { get; }

		/// <summary>Gets the end offset of a variable within the scope of the variable.</summary>
		/// <returns>The end offset of the variable.</returns>
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060018E1 RID: 6369
		int EndOffset { get; }

		/// <summary>Gets the name of the variable.</summary>
		/// <returns>The name of the variable.</returns>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060018E2 RID: 6370
		string Name { get; }

		/// <summary>Gets the start offset of the variable within the scope of the variable.</summary>
		/// <returns>The start offset of the variable.</returns>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060018E3 RID: 6371
		int StartOffset { get; }

		/// <summary>Gets the variable signature.</summary>
		/// <returns>The variable signature as an opaque blob.</returns>
		// Token: 0x060018E4 RID: 6372
		byte[] GetSignature();
	}
}
