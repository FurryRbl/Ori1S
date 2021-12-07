using System;
using System.Collections;

namespace System.Security.Cryptography
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.Oid" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000434 RID: 1076
	public sealed class OidCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.OidCollection" /> class.</summary>
		// Token: 0x060026B0 RID: 9904 RVA: 0x00077EBC File Offset: 0x000760BC
		public OidCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.OidCollection" /> object into an array.</summary>
		/// <param name="array">The array to copy the <see cref="T:System.Security.Cryptography.OidCollection" /> object to.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> cannot be a multidimensional array.-or-The length of <paramref name="array" /> is an invalid offset length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="index" /> is out range.</exception>
		// Token: 0x060026B1 RID: 9905 RVA: 0x00077ED0 File Offset: 0x000760D0
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the collection.</returns>
		// Token: 0x060026B2 RID: 9906 RVA: 0x00077EE0 File Offset: 0x000760E0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new OidEnumerator(this);
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.Oid" /> objects in a collection. </summary>
		/// <returns>The number of <see cref="T:System.Security.Cryptography.Oid" /> objects in a collection.</returns>
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x00077EE8 File Offset: 0x000760E8
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object is thread safe.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x00077EF8 File Offset: 0x000760F8
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.Oid" /> object from the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="index">The location of the <see cref="T:System.Security.Cryptography.Oid" /> object in the collection.</param>
		// Token: 0x17000AE6 RID: 2790
		public Oid this[int index]
		{
			get
			{
				return (Oid)this._list[index];
			}
		}

		/// <summary>Gets the first <see cref="T:System.Security.Cryptography.Oid" /> object that contains a value of the <see cref="P:System.Security.Cryptography.Oid.Value" /> property or a value of the <see cref="P:System.Security.Cryptography.Oid.FriendlyName" /> property that matches the specified string value from the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="oid">A string that represents a <see cref="P:System.Security.Cryptography.Oid.Value" /> property or a <see cref="P:System.Security.Cryptography.Oid.FriendlyName" /> property.</param>
		// Token: 0x17000AE7 RID: 2791
		public Oid this[string oid]
		{
			get
			{
				foreach (object obj in this._list)
				{
					Oid oid2 = (Oid)obj;
					if (oid2.Value == oid)
					{
						return oid2;
					}
				}
				return null;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</returns>
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x060026B7 RID: 9911 RVA: 0x00077FA0 File Offset: 0x000761A0
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.Oid" /> object to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The index of the added <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="oid">The <see cref="T:System.Security.Cryptography.Oid" /> object to add to the collection.</param>
		// Token: 0x060026B8 RID: 9912 RVA: 0x00077FB0 File Offset: 0x000761B0
		public int Add(Oid oid)
		{
			return (!this._readOnly) ? this._list.Add(oid) : 0;
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.OidCollection" /> object into an array.</summary>
		/// <param name="array">The array to copy the <see cref="T:System.Security.Cryptography.OidCollection" /> object into.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		// Token: 0x060026B9 RID: 9913 RVA: 0x00077FD0 File Offset: 0x000761D0
		public void CopyTo(Oid[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidEnumerator" /> object.</returns>
		// Token: 0x060026BA RID: 9914 RVA: 0x00077FE0 File Offset: 0x000761E0
		public OidEnumerator GetEnumerator()
		{
			return new OidEnumerator(this);
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x00077FE8 File Offset: 0x000761E8
		// (set) Token: 0x060026BC RID: 9916 RVA: 0x00077FF0 File Offset: 0x000761F0
		internal bool ReadOnly
		{
			get
			{
				return this._readOnly;
			}
			set
			{
				this._readOnly = value;
			}
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x00077FFC File Offset: 0x000761FC
		internal OidCollection ReadOnlyCopy()
		{
			OidCollection oidCollection = new OidCollection();
			foreach (object obj in this._list)
			{
				Oid oid = (Oid)obj;
				oidCollection.Add(oid);
			}
			oidCollection._readOnly = true;
			return oidCollection;
		}

		// Token: 0x040017C5 RID: 6085
		private ArrayList _list;

		// Token: 0x040017C6 RID: 6086
		private bool _readOnly;
	}
}
