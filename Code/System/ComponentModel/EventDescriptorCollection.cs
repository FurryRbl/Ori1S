using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.EventDescriptor" /> objects.</summary>
	// Token: 0x02000147 RID: 327
	[ComVisible(true)]
	public class EventDescriptorCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06000BFD RID: 3069 RVA: 0x0001F768 File Offset: 0x0001D968
		private EventDescriptorCollection()
		{
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0001F77C File Offset: 0x0001D97C
		internal EventDescriptorCollection(ArrayList list)
		{
			this.eventList = list;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptorCollection" /> class with the given array of <see cref="T:System.ComponentModel.EventDescriptor" /> objects.</summary>
		/// <param name="events">An array of type <see cref="T:System.ComponentModel.EventDescriptor" /> that provides the events for this collection. </param>
		// Token: 0x06000BFF RID: 3071 RVA: 0x0001F798 File Offset: 0x0001D998
		public EventDescriptorCollection(EventDescriptor[] events) : this(events, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptorCollection" /> class with the given array of <see cref="T:System.ComponentModel.EventDescriptor" /> objects. The collection is optionally read-only.</summary>
		/// <param name="events">An array of type <see cref="T:System.ComponentModel.EventDescriptor" /> that provides the events for this collection. </param>
		/// <param name="readOnly">true to specify a read-only collection; otherwise, false.</param>
		// Token: 0x06000C00 RID: 3072 RVA: 0x0001F7A4 File Offset: 0x0001D9A4
		public EventDescriptorCollection(EventDescriptor[] events, bool readOnly)
		{
			this.isReadOnly = readOnly;
			if (events == null)
			{
				return;
			}
			for (int i = 0; i < events.Length; i++)
			{
				this.Add(events[i]);
			}
		}

		/// <summary>Removes all the items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000C02 RID: 3074 RVA: 0x0001F800 File Offset: 0x0001DA00
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06000C03 RID: 3075 RVA: 0x0001F808 File Offset: 0x0001DA08
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000C04 RID: 3076 RVA: 0x0001F810 File Offset: 0x0001DA10
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0001F81C File Offset: 0x0001DA1C
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000C06 RID: 3078 RVA: 0x0001F824 File Offset: 0x0001DA24
		int IList.Add(object value)
		{
			return this.Add((EventDescriptor)value);
		}

		/// <summary>Determines whether the collection contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x06000C07 RID: 3079 RVA: 0x0001F834 File Offset: 0x0001DA34
		bool IList.Contains(object value)
		{
			return this.Contains((EventDescriptor)value);
		}

		/// <summary>Determines the index of a specific item in the collection.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x06000C08 RID: 3080 RVA: 0x0001F844 File Offset: 0x0001DA44
		int IList.IndexOf(object value)
		{
			return this.IndexOf((EventDescriptor)value);
		}

		/// <summary>Inserts an item to the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000C09 RID: 3081 RVA: 0x0001F854 File Offset: 0x0001DA54
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (EventDescriptor)value);
		}

		/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000C0A RID: 3082 RVA: 0x0001F864 File Offset: 0x0001DA64
		void IList.Remove(object value)
		{
			this.Remove((EventDescriptor)value);
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0001F874 File Offset: 0x0001DA74
		bool IList.IsFixedSize
		{
			get
			{
				return this.isReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0001F87C File Offset: 0x0001DA7C
		bool IList.IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0. -or-<paramref name="index" /> is equal to or greater than <see cref="P:System.ComponentModel.EventDescriptorCollection.Count" />.</exception>
		// Token: 0x170002B6 RID: 694
		object IList.this[int index]
		{
			get
			{
				return this.eventList[index];
			}
			set
			{
				if (this.isReadOnly)
				{
					throw new NotSupportedException("The collection is read-only");
				}
				this.eventList[index] = value;
			}
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000C0F RID: 3087 RVA: 0x0001F8BC File Offset: 0x0001DABC
		void ICollection.CopyTo(Array array, int index)
		{
			this.eventList.CopyTo(array, index);
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized.</summary>
		/// <returns>true if access to the collection is synchronized; otherwise, false.</returns>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0001F8CC File Offset: 0x0001DACC
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0001F8D0 File Offset: 0x0001DAD0
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Adds an <see cref="T:System.ComponentModel.EventDescriptor" /> to the end of the collection.</summary>
		/// <returns>The position of the <see cref="T:System.ComponentModel.EventDescriptor" /> within the collection.</returns>
		/// <param name="value">An <see cref="T:System.ComponentModel.EventDescriptor" /> to add to the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000C12 RID: 3090 RVA: 0x0001F8D4 File Offset: 0x0001DAD4
		public int Add(EventDescriptor value)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException("The collection is read-only");
			}
			return this.eventList.Add(value);
		}

		/// <summary>Removes all objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000C13 RID: 3091 RVA: 0x0001F904 File Offset: 0x0001DB04
		public void Clear()
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException("The collection is read-only");
			}
			this.eventList.Clear();
		}

		/// <summary>Returns whether the collection contains the given <see cref="T:System.ComponentModel.EventDescriptor" />.</summary>
		/// <returns>true if the collection contains the <paramref name="value" /> parameter given; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to find within the collection. </param>
		// Token: 0x06000C14 RID: 3092 RVA: 0x0001F928 File Offset: 0x0001DB28
		public bool Contains(EventDescriptor value)
		{
			return this.eventList.Contains(value);
		}

		/// <summary>Gets the description of the event with the specified name in the collection.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified name, or null if the event does not exist.</returns>
		/// <param name="name">The name of the event to get from the collection. </param>
		/// <param name="ignoreCase">true if you want to ignore the case of the event; otherwise, false. </param>
		// Token: 0x06000C15 RID: 3093 RVA: 0x0001F938 File Offset: 0x0001DB38
		public virtual EventDescriptor Find(string name, bool ignoreCase)
		{
			foreach (object obj in this.eventList)
			{
				EventDescriptor eventDescriptor = (EventDescriptor)obj;
				if (ignoreCase)
				{
					if (string.Compare(name, eventDescriptor.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return eventDescriptor;
					}
				}
				else if (string.Compare(name, eventDescriptor.Name, StringComparison.Ordinal) == 0)
				{
					return eventDescriptor;
				}
			}
			return null;
		}

		/// <summary>Gets an enumerator for this <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</summary>
		/// <returns>An enumerator that implements <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06000C16 RID: 3094 RVA: 0x0001F9E0 File Offset: 0x0001DBE0
		public IEnumerator GetEnumerator()
		{
			return this.eventList.GetEnumerator();
		}

		/// <summary>Returns the index of the given <see cref="T:System.ComponentModel.EventDescriptor" />.</summary>
		/// <returns>The index of the given <see cref="T:System.ComponentModel.EventDescriptor" /> within the collection.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to find within the collection. </param>
		// Token: 0x06000C17 RID: 3095 RVA: 0x0001F9F0 File Offset: 0x0001DBF0
		public int IndexOf(EventDescriptor value)
		{
			return this.eventList.IndexOf(value);
		}

		/// <summary>Inserts an <see cref="T:System.ComponentModel.EventDescriptor" /> to the collection at a specified index.</summary>
		/// <param name="index">The index within the collection in which to insert the <paramref name="value" /> parameter. </param>
		/// <param name="value">An <see cref="T:System.ComponentModel.EventDescriptor" /> to insert into the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000C18 RID: 3096 RVA: 0x0001FA00 File Offset: 0x0001DC00
		public void Insert(int index, EventDescriptor value)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException("The collection is read-only");
			}
			this.eventList.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.EventDescriptor" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to remove from the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000C19 RID: 3097 RVA: 0x0001FA28 File Offset: 0x0001DC28
		public void Remove(EventDescriptor value)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException("The collection is read-only");
			}
			this.eventList.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.EventDescriptor" /> at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.ComponentModel.EventDescriptor" /> to remove. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000C1A RID: 3098 RVA: 0x0001FA58 File Offset: 0x0001DC58
		public void RemoveAt(int index)
		{
			if (this.isReadOnly)
			{
				throw new NotSupportedException("The collection is read-only");
			}
			this.eventList.RemoveAt(index);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, using the default sort for this collection, which is usually alphabetical.</summary>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		// Token: 0x06000C1B RID: 3099 RVA: 0x0001FA88 File Offset: 0x0001DC88
		public virtual EventDescriptorCollection Sort()
		{
			EventDescriptorCollection eventDescriptorCollection = this.CloneCollection();
			eventDescriptorCollection.InternalSort(null);
			return eventDescriptorCollection;
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		/// <param name="comparer">An <see cref="T:System.Collections.IComparer" /> to use to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x06000C1C RID: 3100 RVA: 0x0001FAA4 File Offset: 0x0001DCA4
		public virtual EventDescriptorCollection Sort(IComparer comparer)
		{
			EventDescriptorCollection eventDescriptorCollection = this.CloneCollection();
			eventDescriptorCollection.InternalSort(comparer);
			return eventDescriptorCollection;
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, given a specified sort order.</summary>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in the collection. </param>
		// Token: 0x06000C1D RID: 3101 RVA: 0x0001FAC0 File Offset: 0x0001DCC0
		public virtual EventDescriptorCollection Sort(string[] order)
		{
			EventDescriptorCollection eventDescriptorCollection = this.CloneCollection();
			eventDescriptorCollection.InternalSort(order);
			return eventDescriptorCollection;
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, given a specified sort order and an <see cref="T:System.Collections.IComparer" />.</summary>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in the collection. </param>
		/// <param name="comparer">An <see cref="T:System.Collections.IComparer" /> to use to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x06000C1E RID: 3102 RVA: 0x0001FADC File Offset: 0x0001DCDC
		public virtual EventDescriptorCollection Sort(string[] order, IComparer comparer)
		{
			EventDescriptorCollection eventDescriptorCollection = this.CloneCollection();
			if (order != null)
			{
				ArrayList arrayList = eventDescriptorCollection.ExtractItems(order);
				eventDescriptorCollection.InternalSort(comparer);
				arrayList.AddRange(eventDescriptorCollection.eventList);
				eventDescriptorCollection.eventList = arrayList;
			}
			else
			{
				eventDescriptorCollection.InternalSort(comparer);
			}
			return eventDescriptorCollection;
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="sorter">A comparer to use to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x06000C1F RID: 3103 RVA: 0x0001FB28 File Offset: 0x0001DD28
		protected void InternalSort(IComparer comparer)
		{
			if (comparer == null)
			{
				comparer = MemberDescriptor.DefaultComparer;
			}
			this.eventList.Sort(comparer);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x06000C20 RID: 3104 RVA: 0x0001FB44 File Offset: 0x0001DD44
		protected void InternalSort(string[] order)
		{
			if (order != null)
			{
				ArrayList arrayList = this.ExtractItems(order);
				this.InternalSort(null);
				arrayList.AddRange(this.eventList);
				this.eventList = arrayList;
			}
			else
			{
				this.InternalSort(null);
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0001FB88 File Offset: 0x0001DD88
		private ArrayList ExtractItems(string[] names)
		{
			ArrayList arrayList = new ArrayList(this.eventList.Count);
			object[] array = new object[names.Length];
			for (int i = 0; i < this.eventList.Count; i++)
			{
				EventDescriptor eventDescriptor = (EventDescriptor)this.eventList[i];
				int num = Array.IndexOf<string>(names, eventDescriptor.Name);
				if (num != -1)
				{
					array[num] = eventDescriptor;
					this.eventList.RemoveAt(i);
					i--;
				}
			}
			foreach (object obj in array)
			{
				if (obj != null)
				{
					arrayList.Add(obj);
				}
			}
			return arrayList;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0001FC3C File Offset: 0x0001DE3C
		private EventDescriptorCollection CloneCollection()
		{
			return new EventDescriptorCollection
			{
				eventList = (ArrayList)this.eventList.Clone()
			};
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0001FC68 File Offset: 0x0001DE68
		internal EventDescriptorCollection Filter(Attribute[] attributes)
		{
			EventDescriptorCollection eventDescriptorCollection = new EventDescriptorCollection();
			foreach (object obj in this.eventList)
			{
				EventDescriptor eventDescriptor = (EventDescriptor)obj;
				if (eventDescriptor.Attributes.Contains(attributes))
				{
					eventDescriptorCollection.eventList.Add(eventDescriptor);
				}
			}
			return eventDescriptorCollection;
		}

		/// <summary>Gets the number of event descriptors in the collection.</summary>
		/// <returns>The number of event descriptors in the collection.</returns>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0001FCF8 File Offset: 0x0001DEF8
		public int Count
		{
			get
			{
				return this.eventList.Count;
			}
		}

		/// <summary>Gets or sets the event with the specified name.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified name, or null if the event does not exist.</returns>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.EventDescriptor" /> to get or set. </param>
		// Token: 0x170002BA RID: 698
		public virtual EventDescriptor this[string name]
		{
			get
			{
				return this.Find(name, false);
			}
		}

		/// <summary>Gets or sets the event with the specified index number.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified index number.</returns>
		/// <param name="index">The zero-based index number of the <see cref="T:System.ComponentModel.EventDescriptor" /> to get or set. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is not a valid index for <see cref="P:System.ComponentModel.EventDescriptorCollection.Item(System.Int32)" />. </exception>
		// Token: 0x170002BB RID: 699
		public virtual EventDescriptor this[int index]
		{
			get
			{
				return (EventDescriptor)this.eventList[index];
			}
		}

		// Token: 0x04000367 RID: 871
		private ArrayList eventList = new ArrayList();

		// Token: 0x04000368 RID: 872
		private bool isReadOnly;

		/// <summary>Specifies an empty collection to use, rather than creating a new one with no items. This static field is read-only.</summary>
		// Token: 0x04000369 RID: 873
		public static readonly EventDescriptorCollection Empty = new EventDescriptorCollection(null, true);
	}
}
