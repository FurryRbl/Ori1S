using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Defines size, enumerators, and synchronization methods for all nongeneric collections. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200002D RID: 45
	[ComVisible(true)]
	public interface ICollection : IEnumerable
	{
		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600049E RID: 1182
		int Count { get; }

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600049F RID: 1183
		bool IsSynchronized { get; }

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060004A0 RID: 1184
		object SyncRoot { get; }

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or- The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array." /></exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060004A1 RID: 1185
		void CopyTo(Array array, int index);
	}
}
