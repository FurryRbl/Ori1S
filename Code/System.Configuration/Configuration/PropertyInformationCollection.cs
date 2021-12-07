using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.PropertyInformation" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200005F RID: 95
	[Serializable]
	public sealed class PropertyInformationCollection : NameObjectCollectionBase
	{
		// Token: 0x0600035B RID: 859 RVA: 0x0000972C File Offset: 0x0000792C
		internal PropertyInformationCollection() : base(StringComparer.Ordinal)
		{
		}

		/// <summary>Copies the entire <see cref="T:System.Configuration.PropertyInformationCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Configuration.PropertyInformationCollection" /> collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Array.Length" /> property of <paramref name="array" /> is less than <see cref="P:System.Collections.Specialized.NameObjectCollectionBase.Count" /> + <paramref name="index" />.</exception>
		// Token: 0x0600035C RID: 860 RVA: 0x0000973C File Offset: 0x0000793C
		public void CopyTo(PropertyInformation[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Configuration.PropertyInformation" /> object in the collection, based on the specified property name.</summary>
		/// <returns>A <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		/// <param name="propertyName">The name of the configuration attribute contained in the <see cref="T:System.Configuration.PropertyInformationCollection" />object.</param>
		// Token: 0x170000FA RID: 250
		public PropertyInformation this[string propertyName]
		{
			get
			{
				return (PropertyInformation)base.BaseGet(propertyName);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> object, which is used to iterate through this <see cref="T:System.Configuration.PropertyInformationCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object, which is used to iterate through this <see cref="T:System.Configuration.PropertyInformationCollection" />.</returns>
		// Token: 0x0600035E RID: 862 RVA: 0x00009758 File Offset: 0x00007958
		public override IEnumerator GetEnumerator()
		{
			return new PropertyInformationCollection.PropertyInformationEnumerator(this);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009760 File Offset: 0x00007960
		internal void Add(PropertyInformation pi)
		{
			base.BaseAdd(pi.Name, pi);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the <see cref="T:System.Configuration.PropertyInformationCollection" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Configuration.PropertyInformationCollection" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Configuration.PropertyInformationCollection" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06000360 RID: 864 RVA: 0x00009770 File Offset: 0x00007970
		[MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x02000060 RID: 96
		private class PropertyInformationEnumerator : IEnumerator
		{
			// Token: 0x06000361 RID: 865 RVA: 0x00009778 File Offset: 0x00007978
			public PropertyInformationEnumerator(PropertyInformationCollection collection)
			{
				this.collection = collection;
				this.position = -1;
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000362 RID: 866 RVA: 0x00009790 File Offset: 0x00007990
			public object Current
			{
				get
				{
					if (this.position < this.collection.Count && this.position >= 0)
					{
						return this.collection.BaseGet(this.position);
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06000363 RID: 867 RVA: 0x000097CC File Offset: 0x000079CC
			public bool MoveNext()
			{
				return ++this.position < this.collection.Count;
			}

			// Token: 0x06000364 RID: 868 RVA: 0x00009804 File Offset: 0x00007A04
			public void Reset()
			{
				this.position = -1;
			}

			// Token: 0x04000108 RID: 264
			private PropertyInformationCollection collection;

			// Token: 0x04000109 RID: 265
			private int position;
		}
	}
}
