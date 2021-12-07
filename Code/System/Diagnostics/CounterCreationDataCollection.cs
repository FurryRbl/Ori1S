using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.CounterCreationData" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000210 RID: 528
	[Serializable]
	public class CounterCreationDataCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> class, with no associated <see cref="T:System.Diagnostics.CounterCreationData" /> instances.</summary>
		// Token: 0x0600119E RID: 4510 RVA: 0x0002ECC4 File Offset: 0x0002CEC4
		public CounterCreationDataCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> class by using the specified array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances.</summary>
		/// <param name="value">An array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances with which to initialize this <see cref="T:System.Diagnostics.CounterCreationDataCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x0600119F RID: 4511 RVA: 0x0002ECCC File Offset: 0x0002CECC
		public CounterCreationDataCollection(CounterCreationData[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> class by using the specified collection of <see cref="T:System.Diagnostics.CounterCreationData" /> instances.</summary>
		/// <param name="value">A <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> that holds <see cref="T:System.Diagnostics.CounterCreationData" /> instances with which to initialize this <see cref="T:System.Diagnostics.CounterCreationDataCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060011A0 RID: 4512 RVA: 0x0002ECDC File Offset: 0x0002CEDC
		public CounterCreationDataCollection(CounterCreationDataCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Indexes the <see cref="T:System.Diagnostics.CounterCreationData" /> collection.</summary>
		/// <returns>The collection index, which is used to access individual elements of the collection.</returns>
		/// <param name="index">An index into the <see cref="T:System.Diagnostics.CounterCreationDataCollection" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of items in the collection.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000404 RID: 1028
		public CounterCreationData this[int index]
		{
			get
			{
				return (CounterCreationData)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		/// <summary>Adds an instance of the <see cref="T:System.Diagnostics.CounterCreationData" /> class to the collection.</summary>
		/// <returns>The index of the new <see cref="T:System.Diagnostics.CounterCreationData" /> object.</returns>
		/// <param name="value">A <see cref="T:System.Diagnostics.CounterCreationData" /> object to append to the existing collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.CounterCreationData" /> object.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011A3 RID: 4515 RVA: 0x0002ED10 File Offset: 0x0002CF10
		public int Add(CounterCreationData value)
		{
			return base.InnerList.Add(value);
		}

		/// <summary>Adds the specified array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to the collection.</summary>
		/// <param name="value">An array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to append to the existing collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011A4 RID: 4516 RVA: 0x0002ED20 File Offset: 0x0002CF20
		public void AddRange(CounterCreationData[] value)
		{
			foreach (CounterCreationData value2 in value)
			{
				this.Add(value2);
			}
		}

		/// <summary>Adds the specified collection of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to the collection.</summary>
		/// <param name="value">A collection of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to append to the existing collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011A5 RID: 4517 RVA: 0x0002ED50 File Offset: 0x0002CF50
		public void AddRange(CounterCreationDataCollection value)
		{
			foreach (object obj in value)
			{
				CounterCreationData value2 = (CounterCreationData)obj;
				this.Add(value2);
			}
		}

		/// <summary>Determines whether a <see cref="T:System.Diagnostics.CounterCreationData" /> instance exists in the collection.</summary>
		/// <returns>true if the specified <see cref="T:System.Diagnostics.CounterCreationData" /> object exists in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Diagnostics.CounterCreationData" /> object to find in the collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011A6 RID: 4518 RVA: 0x0002EDBC File Offset: 0x0002CFBC
		public bool Contains(CounterCreationData value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Diagnostics.CounterCreationData" /> to an array, starting at the specified index of the array.</summary>
		/// <param name="array">An array of <see cref="T:System.Diagnostics.CounterCreationData" /> instances to add to the collection. </param>
		/// <param name="index">The location at which to add the new instances. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the <see cref="T:System.Diagnostics.CounterCreationDataCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011A7 RID: 4519 RVA: 0x0002EDCC File Offset: 0x0002CFCC
		public void CopyTo(CounterCreationData[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Returns the index of a <see cref="T:System.Diagnostics.CounterCreationData" /> object in the collection.</summary>
		/// <returns>The zero-based index of the specified <see cref="T:System.Diagnostics.CounterCreationData" />, if it is found, in the collection; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Diagnostics.CounterCreationData" /> object to locate in the collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011A8 RID: 4520 RVA: 0x0002EDDC File Offset: 0x0002CFDC
		public int IndexOf(CounterCreationData value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Diagnostics.CounterCreationData" /> object into the collection, at the specified index.</summary>
		/// <param name="index">The zero-based index of the location at which the <see cref="T:System.Diagnostics.CounterCreationData" /> is to be inserted. </param>
		/// <param name="value">The <see cref="T:System.Diagnostics.CounterCreationData" /> to insert into the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.CounterCreationData" /> object.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0. -or-<paramref name="index" /> is greater than the number of items in the collection.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011A9 RID: 4521 RVA: 0x0002EDEC File Offset: 0x0002CFEC
		public void Insert(int index, CounterCreationData value)
		{
			base.InnerList.Insert(index, value);
		}

		/// <summary>Checks the specified object to determine whether it is a valid <see cref="T:System.Diagnostics.CounterCreationData" /> type.</summary>
		/// <param name="value">The object that will be validated.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.CounterCreationData" /> object.</exception>
		// Token: 0x060011AA RID: 4522 RVA: 0x0002EDFC File Offset: 0x0002CFFC
		protected override void OnValidate(object value)
		{
			if (!(value is CounterCreationData))
			{
				throw new NotSupportedException(Locale.GetText("You can only insert CounterCreationData objects into the collection"));
			}
		}

		/// <summary>Removes a <see cref="T:System.Diagnostics.CounterCreationData" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.CounterCreationData" /> to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Diagnostics.CounterCreationData" /> object.-or-<paramref name="value" /> does not exist in the collection.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011AB RID: 4523 RVA: 0x0002EE1C File Offset: 0x0002D01C
		public virtual void Remove(CounterCreationData value)
		{
			base.InnerList.Remove(value);
		}
	}
}
