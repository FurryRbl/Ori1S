using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Resources
{
	/// <summary>Provides the base functionality to read data from resource files.</summary>
	// Token: 0x02000302 RID: 770
	[ComVisible(true)]
	public interface IResourceReader : IEnumerable, IDisposable
	{
		/// <summary>Closes the resource reader after releasing any resources associated with it.</summary>
		// Token: 0x060027D5 RID: 10197
		void Close();

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> of the resources for this reader.</summary>
		/// <returns>A dictionary enumerator for the resources for this reader.</returns>
		// Token: 0x060027D6 RID: 10198
		IDictionaryEnumerator GetEnumerator();
	}
}
