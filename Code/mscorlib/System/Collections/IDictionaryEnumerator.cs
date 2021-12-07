using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Enumerates the elements of a nongeneric dictionary.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C4 RID: 452
	[ComVisible(true)]
	public interface IDictionaryEnumerator : IEnumerator
	{
		/// <summary>Gets both the key and the value of the current dictionary entry.</summary>
		/// <returns>A <see cref="T:System.Collections.DictionaryEntry" /> containing both the key and the value of the current dictionary entry.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001774 RID: 6004
		DictionaryEntry Entry { get; }

		/// <summary>Gets the key of the current dictionary entry.</summary>
		/// <returns>The key of the current element of the enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001775 RID: 6005
		object Key { get; }

		/// <summary>Gets the value of the current dictionary entry.</summary>
		/// <returns>The value of the current element of the enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001776 RID: 6006
		object Value { get; }
	}
}
