using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Supports a simple iteration over a nongeneric collection.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000017 RID: 23
	[ComVisible(true)]
	[Guid("496B0ABF-CDEE-11D3-88E8-00902754C43A")]
	public interface IEnumerator
	{
		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600016F RID: 367
		object Current { get; }

		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000170 RID: 368
		bool MoveNext();

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000171 RID: 369
		void Reset();
	}
}
