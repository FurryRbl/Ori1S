using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Provides the abstract base class for a strongly typed non-generic read-only collection.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CA RID: 458
	[ComVisible(true)]
	[Serializable]
	public abstract class ReadOnlyCollectionBase : IEnumerable, ICollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> class.</summary>
		// Token: 0x060017A0 RID: 6048 RVA: 0x0005B220 File Offset: 0x00059420
		protected ReadOnlyCollectionBase()
		{
			this.list = new ArrayList();
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0005B234 File Offset: 0x00059434
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ReadOnlyCollectionBase" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ReadOnlyCollectionBase" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ReadOnlyCollectionBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ReadOnlyCollectionBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x060017A2 RID: 6050 RVA: 0x0005B23C File Offset: 0x0005943C
		void ICollection.CopyTo(Array array, int index)
		{
			ArrayList innerList = this.InnerList;
			lock (innerList)
			{
				this.InnerList.CopyTo(array, index);
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to a <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object.</returns>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x0005B28C File Offset: 0x0005948C
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		/// <summary>Gets a value indicating whether access to a <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object is synchronized (thread safe); otherwise, false. The default is false.</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0005B29C File Offset: 0x0005949C
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.Retrieving the value of this property is an O(1) operation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x0005B2AC File Offset: 0x000594AC
		public virtual int Count
		{
			get
			{
				return this.InnerList.Count;
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017A6 RID: 6054 RVA: 0x0005B2BC File Offset: 0x000594BC
		public virtual IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		/// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> representing the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance itself.</returns>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0005B2CC File Offset: 0x000594CC
		protected ArrayList InnerList
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x040008A5 RID: 2213
		private ArrayList list;
	}
}
