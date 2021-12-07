using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net.NetworkInformation
{
	/// <summary>Stores a set of <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> types.</summary>
	// Token: 0x0200035B RID: 859
	public class GatewayIPAddressInformationCollection : IEnumerable, IEnumerable<GatewayIPAddressInformation>, ICollection<GatewayIPAddressInformation>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformationCollection" /> class.</summary>
		// Token: 0x06001E48 RID: 7752 RVA: 0x0005CB80 File Offset: 0x0005AD80
		protected GatewayIPAddressInformationCollection()
		{
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x06001E49 RID: 7753 RVA: 0x0005CB94 File Offset: 0x0005AD94
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <param name="address">The object to be added to the collection.</param>
		// Token: 0x06001E4A RID: 7754 RVA: 0x0005CBA8 File Offset: 0x0005ADA8
		public virtual void Add(GatewayIPAddressInformation address)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("The collection is read-only.");
			}
			this.list.Add(address);
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		// Token: 0x06001E4B RID: 7755 RVA: 0x0005CBD8 File Offset: 0x0005ADD8
		public virtual void Clear()
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("The collection is read-only.");
			}
			this.list.Clear();
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> object exists in the collection; otherwise false.</returns>
		/// <param name="address">The <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> object to be searched in the collection.</param>
		// Token: 0x06001E4C RID: 7756 RVA: 0x0005CBFC File Offset: 0x0005ADFC
		public virtual bool Contains(GatewayIPAddressInformation address)
		{
			return this.list.Contains(address);
		}

		/// <summary>Copies the elements in this collection to a one-dimensional array of type <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" />.</summary>
		/// <param name="array">A one-dimensional array that receives a copy of the collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which the copy begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> is greater than the available space from <paramref name="count" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06001E4D RID: 7757 RVA: 0x0005CC0C File Offset: 0x0005AE0C
		public virtual void CopyTo(GatewayIPAddressInformation[] array, int offset)
		{
			this.list.CopyTo(array, offset);
		}

		/// <summary>Returns an object that can be used to iterate through this collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x06001E4E RID: 7758 RVA: 0x0005CC1C File Offset: 0x0005AE1C
		public virtual IEnumerator<GatewayIPAddressInformation> GetEnumerator()
		{
			return ((IEnumerable<GatewayIPAddressInformation>)this.list).GetEnumerator();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> because this operation is not supported for this collection.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="address">The object to be removed.</param>
		// Token: 0x06001E4F RID: 7759 RVA: 0x0005CC2C File Offset: 0x0005AE2C
		public virtual bool Remove(GatewayIPAddressInformation address)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException("The collection is read-only.");
			}
			return this.list.Remove(address);
		}

		/// <summary>Gets the number of <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> types in this collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> types in this collection.</returns>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001E50 RID: 7760 RVA: 0x0005CC5C File Offset: 0x0005AE5C
		public virtual int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to this collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x0005CC6C File Offset: 0x0005AE6C
		public virtual bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> at the specific index of the collection.</summary>
		/// <returns>The <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformation" /> at the specific index in the collection.</returns>
		/// <param name="index">The index of interest.</param>
		// Token: 0x17000783 RID: 1923
		public virtual GatewayIPAddressInformation this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x040012DD RID: 4829
		private List<GatewayIPAddressInformation> list = new List<GatewayIPAddressInformation>();
	}
}
