using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a document referenced by a symbol store.</summary>
	// Token: 0x020001EA RID: 490
	[ComVisible(true)]
	public interface ISymbolDocument
	{
		/// <summary>Gets the checksum algorithm identifier.</summary>
		/// <returns>A GUID identifying the checksum algorithm. The value is all zeros, if there is no checksum.</returns>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060018B2 RID: 6322
		Guid CheckSumAlgorithmId { get; }

		/// <summary>Gets the type of the current document.</summary>
		/// <returns>The type of the current document.</returns>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060018B3 RID: 6323
		Guid DocumentType { get; }

		/// <summary>Checks whether the current document is stored in the symbol store.</summary>
		/// <returns>true if the current document is stored in the symbol store; otherwise, false.</returns>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060018B4 RID: 6324
		bool HasEmbeddedSource { get; }

		/// <summary>Gets the language of the current document.</summary>
		/// <returns>The language of the current document.</returns>
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060018B5 RID: 6325
		Guid Language { get; }

		/// <summary>Gets the language vendor of the current document.</summary>
		/// <returns>The language vendor of the current document.</returns>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060018B6 RID: 6326
		Guid LanguageVendor { get; }

		/// <summary>Gets the length, in bytes, of the embedded source.</summary>
		/// <returns>The source length of the current document.</returns>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060018B7 RID: 6327
		int SourceLength { get; }

		/// <summary>Gets the URL of the current document.</summary>
		/// <returns>The URL of the current document.</returns>
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060018B8 RID: 6328
		string URL { get; }

		/// <summary>Returns the closest line that is a sequence point, given a line in the current document that might or might not be a sequence point.</summary>
		/// <returns>The closest line that is a sequence point.</returns>
		/// <param name="line">The specified line in the document. </param>
		// Token: 0x060018B9 RID: 6329
		int FindClosestLine(int line);

		/// <summary>Gets the checksum.</summary>
		/// <returns>The checksum.</returns>
		// Token: 0x060018BA RID: 6330
		byte[] GetCheckSum();

		/// <summary>Gets the embedded document source for the specified range.</summary>
		/// <returns>The document source for the specified range.</returns>
		/// <param name="startLine">The starting line in the current document. </param>
		/// <param name="startColumn">The starting column in the current document. </param>
		/// <param name="endLine">The ending line in the current document. </param>
		/// <param name="endColumn">The ending column in the current document. </param>
		// Token: 0x060018BB RID: 6331
		byte[] GetSourceRange(int startLine, int startColumn, int endLine, int endColumn);
	}
}
