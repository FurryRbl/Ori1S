using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a symbol binder for managed code.</summary>
	// Token: 0x020001E9 RID: 489
	[ComVisible(true)]
	public interface ISymbolBinder1
	{
		/// <summary>Gets the interface of the symbol reader for the current file.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.SymbolStore.ISymbolReader" /> interface that reads the debugging symbols.</returns>
		/// <param name="importer">An <see cref="T:System.IntPtr" /> that refers to the metadata import interface. </param>
		/// <param name="filename">The name of the file for which the reader interface is required. </param>
		/// <param name="searchPath">The search path used to locate the symbol file. </param>
		// Token: 0x060018B1 RID: 6321
		ISymbolReader GetReader(IntPtr importer, string filename, string searchPath);
	}
}
