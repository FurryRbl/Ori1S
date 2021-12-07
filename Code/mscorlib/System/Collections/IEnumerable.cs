using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Exposes the enumerator, which supports a simple iteration over a non-generic collection.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000018 RID: 24
	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	[ComVisible(true)]
	public interface IEnumerable
	{
		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000172 RID: 370
		[DispId(-4)]
		IEnumerator GetEnumerator();
	}
}
