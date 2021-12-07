using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> types.</summary>
	// Token: 0x0200039D RID: 925
	public class MulticastIPAddressInformationCollection : IEnumerable, IEnumerable<MulticastIPAddressInformation>, ICollection<MulticastIPAddressInformation>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformationCollection" /> class.</summary>
		// Token: 0x06002063 RID: 8291 RVA: 0x0005FB1C File Offset: 0x0005DD1C
		protected internal MulticastIPAddressInformationCollection()
		{
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x06002064 RID: 8292 RVA: 0x0005FB30 File Offset: 0x0005DD30
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be added to the collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x06002065 RID: 8293 RVA: 0x0005FB44 File Offset: 0x0005DD44
		public virtual void Add(MulticastIPAddressInformation address)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("The collection is read-only.");
			}
			this.list.Add(address);
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be removed.</summary>
		// Token: 0x06002066 RID: 8294 RVA: 0x0005FB74 File Offset: 0x0005DD74
		public virtual void Clear()
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("The collection is read-only.");
			}
			this.list.Clear();
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> object exists in the collection; otherwise, false.</returns>
		/// <param name="address">The <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> object to be searched in the collection.</param>
		// Token: 0x06002067 RID: 8295 RVA: 0x0005FB98 File Offset: 0x0005DD98
		public virtual bool Contains(MulticastIPAddressInformation address)
		{
			return this.list.Contains(address);
		}

		/// <summary>Copies the elements in this collection to a one-dimensional array of type <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" />.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> is greater than the available space from <paramref name="count" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06002068 RID: 8296 RVA: 0x0005FBA8 File Offset: 0x0005DDA8
		public virtual void CopyTo(MulticastIPAddressInformation[] array, int offset)
		{
			this.list.CopyTo(array, offset);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x06002069 RID: 8297 RVA: 0x0005FBB8 File Offset: 0x0005DDB8
		public virtual IEnumerator<MulticastIPAddressInformation> GetEnumerator()
		{
			return ((IEnumerable<MulticastIPAddressInformation>)this.list).GetEnumerator();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because the collection is read-only and elements cannot be removed.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="address">The object to be removed.</param>
		// Token: 0x0600206A RID: 8298 RVA: 0x0005FBC8 File Offset: 0x0005DDC8
		public virtual bool Remove(MulticastIPAddressInformation address)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("The collection is read-only.");
			}
			return this.list.Remove(address);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x0600206B RID: 8299 RVA: 0x0005FBF8 File Offset: 0x0005DDF8
		public virtual int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to this collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x0005FC08 File Offset: 0x0005DE08
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> at the specific index of the collection.</summary>
		/// <returns>The <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> at the specific index in the collection.</returns>
		/// <param name="index">The index of interest.</param>
		// Token: 0x17000908 RID: 2312
		public virtual MulticastIPAddressInformation this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x040013C3 RID: 5059
		private List<MulticastIPAddressInformation> list = new List<MulticastIPAddressInformation>();
	}
}
