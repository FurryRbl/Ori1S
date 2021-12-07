using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of designers.</summary>
	// Token: 0x020000FA RID: 250
	public class DesignerCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerCollection" /> class that contains the specified designers.</summary>
		/// <param name="designers">An array of <see cref="T:System.ComponentModel.Design.IDesignerHost" /> objects to store. </param>
		// Token: 0x06000A20 RID: 2592 RVA: 0x0001CD1C File Offset: 0x0001AF1C
		public DesignerCollection(IDesignerHost[] designers)
		{
			this.designers = new ArrayList(designers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerCollection" /> class that contains the specified set of designers.</summary>
		/// <param name="designers">A list that contains the collection of designers to add. </param>
		// Token: 0x06000A21 RID: 2593 RVA: 0x0001CD30 File Offset: 0x0001AF30
		public DesignerCollection(IList designers)
		{
			this.designers = new ArrayList(designers);
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0001CD44 File Offset: 0x0001AF44
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a new enumerator for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enumerates the collection.</returns>
		// Token: 0x06000A23 RID: 2595 RVA: 0x0001CD4C File Offset: 0x0001AF4C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0001CD54 File Offset: 0x0001AF54
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.designers.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0001CD64 File Offset: 0x0001AF64
		object ICollection.SyncRoot
		{
			get
			{
				return this.designers.SyncRoot;
			}
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		// Token: 0x06000A26 RID: 2598 RVA: 0x0001CD74 File Offset: 0x0001AF74
		void ICollection.CopyTo(Array array, int index)
		{
			this.designers.CopyTo(array, index);
		}

		/// <summary>Gets the number of designers in the collection.</summary>
		/// <returns>The number of designers in the collection.</returns>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0001CD84 File Offset: 0x0001AF84
		public int Count
		{
			get
			{
				return this.designers.Count;
			}
		}

		/// <summary>Gets the designer at the specified index.</summary>
		/// <returns>The designer at the specified index.</returns>
		/// <param name="index">The index of the designer to return. </param>
		// Token: 0x1700024E RID: 590
		public virtual IDesignerHost this[int index]
		{
			get
			{
				return (IDesignerHost)this.designers[index];
			}
		}

		/// <summary>Gets a new enumerator for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enumerates the collection.</returns>
		// Token: 0x06000A29 RID: 2601 RVA: 0x0001CDA8 File Offset: 0x0001AFA8
		public IEnumerator GetEnumerator()
		{
			return this.designers.GetEnumerator();
		}

		// Token: 0x040002B7 RID: 695
		private ArrayList designers;
	}
}
