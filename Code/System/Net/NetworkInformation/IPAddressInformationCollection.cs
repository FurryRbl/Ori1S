using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types.</summary>
	// Token: 0x0200036D RID: 877
	public class IPAddressInformationCollection : IEnumerable, IEnumerable<IPAddressInformation>, ICollection<IPAddressInformation>
	{
		// Token: 0x06001F2B RID: 7979 RVA: 0x0005DA00 File Offset: 0x0005BC00
		internal IPAddressInformationCollection()
		{
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types in this collection.</returns>
		// Token: 0x06001F2C RID: 7980 RVA: 0x0005DA14 File Offset: 0x0005BC14
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x06001F2D RID: 7981 RVA: 0x0005DA28 File Offset: 0x0005BC28
		public virtual void Add(IPAddressInformation address)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("The collection is read-only.");
			}
			this.list.Add(address);
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		// Token: 0x06001F2E RID: 7982 RVA: 0x0005DA58 File Offset: 0x0005BC58
		public virtual void Clear()
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("The collection is read-only.");
			}
			this.list.Clear();
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> object exists in the collection; otherwise. false.</returns>
		/// <param name="address">The <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> object to be searched in the collection.</param>
		// Token: 0x06001F2F RID: 7983 RVA: 0x0005DA7C File Offset: 0x0005BC7C
		public virtual bool Contains(IPAddressInformation address)
		{
			return this.list.Contains(address);
		}

		/// <summary>Copies the collection to the specified array.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> is greater than the available space from <paramref name="offset" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06001F30 RID: 7984 RVA: 0x0005DA8C File Offset: 0x0005BC8C
		public virtual void CopyTo(IPAddressInformation[] array, int offset)
		{
			this.list.CopyTo(array, offset);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types in this collection.</returns>
		// Token: 0x06001F31 RID: 7985 RVA: 0x0005DA9C File Offset: 0x0005BC9C
		public virtual IEnumerator<IPAddressInformation> GetEnumerator()
		{
			return ((IEnumerable<IPAddressInformation>)this.list).GetEnumerator();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="address">The object to be removed.</param>
		// Token: 0x06001F32 RID: 7986 RVA: 0x0005DAAC File Offset: 0x0005BCAC
		public virtual bool Remove(IPAddressInformation address)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("The collection is read-only.");
			}
			return this.list.Remove(address);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> types in this collection.</returns>
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x0005DADC File Offset: 0x0005BCDC
		public virtual int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to this collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0005DAEC File Offset: 0x0005BCEC
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> at the specified index in the collection. </summary>
		/// <returns>The <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x1700083C RID: 2108
		public virtual IPAddressInformation this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x0400130F RID: 4879
		private List<IPAddressInformation> list = new List<IPAddressInformation>();
	}
}
