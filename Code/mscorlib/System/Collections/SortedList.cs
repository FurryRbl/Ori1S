using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Represents a collection of key/value pairs that are sorted by the keys and are accessible by key and by index.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001CB RID: 459
	[DebuggerDisplay("Count={Count}")]
	[ComVisible(true)]
	[Serializable]
	public class SortedList : IEnumerable, ICloneable, ICollection, IDictionary
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the default initial capacity, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key added to the <see cref="T:System.Collections.SortedList" /> object.</summary>
		// Token: 0x060017A8 RID: 6056 RVA: 0x0005B2D4 File Offset: 0x000594D4
		public SortedList() : this(null, SortedList.INITIAL_SIZE)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the specified initial capacity, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key added to the <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="initialCapacity">The initial number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCapacity" /> is less than zero. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to create a <see cref="T:System.Collections.SortedList" /> object with the specified <paramref name="initialCapacity" />.</exception>
		// Token: 0x060017A9 RID: 6057 RVA: 0x0005B2E4 File Offset: 0x000594E4
		public SortedList(int initialCapacity) : this(null, initialCapacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the specified initial capacity, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.-or- null to use the <see cref="T:System.IComparable" /> implementation of each key. </param>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to create a <see cref="T:System.Collections.SortedList" /> object with the specified <paramref name="capacity" />.</exception>
		// Token: 0x060017AA RID: 6058 RVA: 0x0005B2F0 File Offset: 0x000594F0
		public SortedList(IComparer comparer, int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (capacity == 0)
			{
				this.defaultCapacity = 0;
			}
			else
			{
				this.defaultCapacity = SortedList.INITIAL_SIZE;
			}
			this.comparer = comparer;
			this.InitTable(capacity, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the default initial capacity, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.-or- null to use the <see cref="T:System.IComparable" /> implementation of each key. </param>
		// Token: 0x060017AB RID: 6059 RVA: 0x0005B344 File Offset: 0x00059544
		public SortedList(IComparer comparer)
		{
			this.comparer = comparer;
			this.InitTable(SortedList.INITIAL_SIZE, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that contains elements copied from the specified dictionary, has the same initial capacity as the number of elements copied, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> implementation to copy to a new <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">One or more elements in <paramref name="d" /> do not implement the <see cref="T:System.IComparable" /> interface. </exception>
		// Token: 0x060017AC RID: 6060 RVA: 0x0005B360 File Offset: 0x00059560
		public SortedList(IDictionary d) : this(d, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that contains elements copied from the specified dictionary, has the same initial capacity as the number of elements copied, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> implementation to copy to a new <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.-or- null to use the <see cref="T:System.IComparable" /> implementation of each key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="comparer" /> is null, and one or more elements in <paramref name="d" /> do not implement the <see cref="T:System.IComparable" /> interface. </exception>
		// Token: 0x060017AD RID: 6061 RVA: 0x0005B36C File Offset: 0x0005956C
		public SortedList(IDictionary d, IComparer comparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.InitTable(d.Count, true);
			this.comparer = comparer;
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.SortedList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.SortedList" />.</returns>
		// Token: 0x060017AF RID: 6063 RVA: 0x0005B3DC File Offset: 0x000595DC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList.Enumerator(this, SortedList.EnumeratorMode.ENTRY_MODE);
		}

		/// <summary>Gets the number of elements contained in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x0005B3E8 File Offset: 0x000595E8
		public virtual int Count
		{
			get
			{
				return this.inUse;
			}
		}

		/// <summary>Gets a value indicating whether access to a <see cref="T:System.Collections.SortedList" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.SortedList" /> object is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x0005B3F0 File Offset: 0x000595F0
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x0005B3F4 File Offset: 0x000595F4
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.SortedList" /> object has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object has a fixed size; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0005B3F8 File Offset: 0x000595F8
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.SortedList" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x0005B3FC File Offset: 0x000595FC
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the keys in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0005B400 File Offset: 0x00059600
		public virtual ICollection Keys
		{
			get
			{
				return new SortedList.ListKeys(this);
			}
		}

		/// <summary>Gets the values in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0005B408 File Offset: 0x00059608
		public virtual ICollection Values
		{
			get
			{
				return new SortedList.ListValues(this);
			}
		}

		/// <summary>Gets and sets the value associated with a specific key in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The value associated with the <paramref name="key" /> parameter in the <see cref="T:System.Collections.SortedList" /> object, if <paramref name="key" /> is found; otherwise, null.</returns>
		/// <param name="key">The key associated with the value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.SortedList" /> object is read-only.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to add the element to the <see cref="T:System.Collections.SortedList" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B3 RID: 947
		public virtual object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException();
				}
				return this.GetImpl(key);
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException();
				}
				if (this.IsReadOnly)
				{
					throw new NotSupportedException("SortedList is Read Only.");
				}
				if (this.Find(key) < 0 && this.IsFixedSize)
				{
					throw new NotSupportedException("Key not found and SortedList is fixed size.");
				}
				this.PutImpl(key, value, true);
			}
		}

		/// <summary>Gets or sets the capacity of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned is less than the current number of elements in the <see cref="T:System.Collections.SortedList" /> object.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x0005B484 File Offset: 0x00059684
		// (set) Token: 0x060017BA RID: 6074 RVA: 0x0005B490 File Offset: 0x00059690
		public virtual int Capacity
		{
			get
			{
				return this.table.Length;
			}
			set
			{
				int num = this.table.Length;
				if (this.inUse > value)
				{
					throw new ArgumentOutOfRangeException("capacity too small");
				}
				if (value == 0)
				{
					SortedList.Slot[] destinationArray = new SortedList.Slot[this.defaultCapacity];
					Array.Copy(this.table, destinationArray, this.inUse);
					this.table = destinationArray;
				}
				else if (value > this.inUse)
				{
					SortedList.Slot[] destinationArray2 = new SortedList.Slot[value];
					Array.Copy(this.table, destinationArray2, this.inUse);
					this.table = destinationArray2;
				}
				else if (value > num)
				{
					SortedList.Slot[] destinationArray3 = new SortedList.Slot[value];
					Array.Copy(this.table, destinationArray3, num);
					this.table = destinationArray3;
				}
			}
		}

		/// <summary>Adds an element with the specified key and value to a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key of the element to add. </param>
		/// <param name="value">The value of the element to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An element with the specified <paramref name="key" /> already exists in the <see cref="T:System.Collections.SortedList" /> object.-or- The <see cref="T:System.Collections.SortedList" /> is set to use the <see cref="T:System.IComparable" /> interface, and <paramref name="key" /> does not implement the <see cref="T:System.IComparable" /> interface. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to add the element to the <see cref="T:System.Collections.SortedList" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017BB RID: 6075 RVA: 0x0005B540 File Offset: 0x00059740
		public virtual void Add(object key, object value)
		{
			this.PutImpl(key, value, false);
		}

		/// <summary>Removes all elements from a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017BC RID: 6076 RVA: 0x0005B54C File Offset: 0x0005974C
		public virtual void Clear()
		{
			this.defaultCapacity = SortedList.INITIAL_SIZE;
			this.table = new SortedList.Slot[this.defaultCapacity];
			this.inUse = 0;
			this.modificationCount++;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="key" />; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017BD RID: 6077 RVA: 0x0005B580 File Offset: 0x00059780
		public virtual bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException();
			}
			bool result;
			try
			{
				result = (this.Find(key) >= 0);
			}
			catch (Exception)
			{
				throw new InvalidOperationException();
			}
			return result;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017BE RID: 6078 RVA: 0x0005B5DC File Offset: 0x000597DC
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new SortedList.Enumerator(this, SortedList.EnumeratorMode.ENTRY_MODE);
		}

		/// <summary>Removes the element with the specified key from a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017BF RID: 6079 RVA: 0x0005B5E8 File Offset: 0x000597E8
		public virtual void Remove(object key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Copies <see cref="T:System.Collections.SortedList" /> elements to a one-dimensional <see cref="T:System.Array" /> object, starting at the specified index in the array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.SortedList" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.SortedList" /> object is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.SortedList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017C0 RID: 6080 RVA: 0x0005B60C File Offset: 0x0005980C
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (array.Rank > 1)
			{
				throw new ArgumentException("array is multi-dimensional");
			}
			if (arrayIndex >= array.Length)
			{
				throw new ArgumentNullException("arrayIndex is greater than or equal to array.Length");
			}
			if (this.Count > array.Length - arrayIndex)
			{
				throw new ArgumentNullException("Not enough space in array from arrayIndex to end of array");
			}
			IDictionaryEnumerator enumerator = this.GetEnumerator();
			int num = arrayIndex;
			while (enumerator.MoveNext())
			{
				array.SetValue(enumerator.Entry, num++);
			}
		}

		/// <summary>Creates a shallow copy of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060017C1 RID: 6081 RVA: 0x0005B6B0 File Offset: 0x000598B0
		public virtual object Clone()
		{
			return new SortedList(this, this.comparer)
			{
				modificationCount = this.modificationCount
			};
		}

		/// <summary>Gets the keys in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> object containing the keys in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017C2 RID: 6082 RVA: 0x0005B6D8 File Offset: 0x000598D8
		public virtual IList GetKeyList()
		{
			return new SortedList.ListKeys(this);
		}

		/// <summary>Gets the values in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> object containing the values in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017C3 RID: 6083 RVA: 0x0005B6E0 File Offset: 0x000598E0
		public virtual IList GetValueList()
		{
			return new SortedList.ListValues(this);
		}

		/// <summary>Removes the element at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="index">The zero-based index of the element to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017C4 RID: 6084 RVA: 0x0005B6E8 File Offset: 0x000598E8
		public virtual void RemoveAt(int index)
		{
			SortedList.Slot[] array = this.table;
			int count = this.Count;
			if (index >= 0 && index < count)
			{
				if (index != count - 1)
				{
					Array.Copy(array, index + 1, array, index, count - 1 - index);
				}
				else
				{
					array[index].key = null;
					array[index].value = null;
				}
				this.inUse--;
				this.modificationCount++;
				return;
			}
			throw new ArgumentOutOfRangeException("index out of range");
		}

		/// <summary>Returns the zero-based index of the specified key in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The zero-based index of the <paramref name="key" /> parameter, if <paramref name="key" /> is found in the <see cref="T:System.Collections.SortedList" /> object; otherwise, -1.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017C5 RID: 6085 RVA: 0x0005B778 File Offset: 0x00059978
		public virtual int IndexOfKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException();
			}
			int num = 0;
			try
			{
				num = this.Find(key);
			}
			catch (Exception)
			{
				throw new InvalidOperationException();
			}
			return num | num >> 31;
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified value in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The zero-based index of the first occurrence of the <paramref name="value" /> parameter, if <paramref name="value" /> is found in the <see cref="T:System.Collections.SortedList" /> object; otherwise, -1.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.SortedList" /> object. The value can be null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017C6 RID: 6086 RVA: 0x0005B7D0 File Offset: 0x000599D0
		public virtual int IndexOfValue(object value)
		{
			if (this.inUse == 0)
			{
				return -1;
			}
			for (int i = 0; i < this.inUse; i++)
			{
				SortedList.Slot slot = this.table[i];
				if (object.Equals(value, slot.value))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="key" />; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017C7 RID: 6087 RVA: 0x0005B828 File Offset: 0x00059A28
		public virtual bool ContainsKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException();
			}
			bool result;
			try
			{
				result = this.Contains(key);
			}
			catch (Exception)
			{
				throw new InvalidOperationException();
			}
			return result;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="value" />; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.SortedList" /> object. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017C8 RID: 6088 RVA: 0x0005B87C File Offset: 0x00059A7C
		public virtual bool ContainsValue(object value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		/// <summary>Gets the value at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The value at the specified index of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <param name="index">The zero-based index of the value to get. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017C9 RID: 6089 RVA: 0x0005B88C File Offset: 0x00059A8C
		public virtual object GetByIndex(int index)
		{
			if (index >= 0 && index < this.Count)
			{
				return this.table[index].value;
			}
			throw new ArgumentOutOfRangeException("index out of range");
		}

		/// <summary>Replaces the value at a specific index in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="index">The zero-based index at which to save <paramref name="value" />. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to save into the <see cref="T:System.Collections.SortedList" /> object. The value can be null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017CA RID: 6090 RVA: 0x0005B8C0 File Offset: 0x00059AC0
		public virtual void SetByIndex(int index, object value)
		{
			if (index >= 0 && index < this.Count)
			{
				this.table[index].value = value;
				return;
			}
			throw new ArgumentOutOfRangeException("index out of range");
		}

		/// <summary>Gets the key at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The key at the specified index of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <param name="index">The zero-based index of the key to get. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017CB RID: 6091 RVA: 0x0005B8F8 File Offset: 0x00059AF8
		public virtual object GetKey(int index)
		{
			if (index >= 0 && index < this.Count)
			{
				return this.table[index].key;
			}
			throw new ArgumentOutOfRangeException("index out of range");
		}

		/// <summary>Returns a synchronized (thread-safe) wrapper for a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>A synchronized (thread-safe) wrapper for the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <param name="list">The <see cref="T:System.Collections.SortedList" /> object to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060017CC RID: 6092 RVA: 0x0005B92C File Offset: 0x00059B2C
		public static SortedList Synchronized(SortedList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException(Locale.GetText("Base list is null."));
			}
			return new SortedList.SynchedSortedList(list);
		}

		/// <summary>Sets the capacity to the actual number of elements in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017CD RID: 6093 RVA: 0x0005B94C File Offset: 0x00059B4C
		public virtual void TrimToSize()
		{
			if (this.Count == 0)
			{
				this.Resize(this.defaultCapacity, false);
			}
			else
			{
				this.Resize(this.Count, true);
			}
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0005B984 File Offset: 0x00059B84
		private void Resize(int n, bool copy)
		{
			SortedList.Slot[] sourceArray = this.table;
			SortedList.Slot[] destinationArray = new SortedList.Slot[n];
			if (copy)
			{
				Array.Copy(sourceArray, 0, destinationArray, 0, n);
			}
			this.table = destinationArray;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0005B9B8 File Offset: 0x00059BB8
		private void EnsureCapacity(int n, int free)
		{
			SortedList.Slot[] array = this.table;
			SortedList.Slot[] array2 = null;
			int capacity = this.Capacity;
			bool flag = free >= 0 && free < this.Count;
			if (n > capacity)
			{
				array2 = new SortedList.Slot[n << 1];
			}
			if (array2 != null)
			{
				if (flag)
				{
					if (free > 0)
					{
						Array.Copy(array, 0, array2, 0, free);
					}
					int num = this.Count - free;
					if (num > 0)
					{
						Array.Copy(array, free, array2, free + 1, num);
					}
				}
				else
				{
					Array.Copy(array, array2, this.Count);
				}
				this.table = array2;
			}
			else if (flag)
			{
				Array.Copy(array, free, array, free + 1, this.Count - free);
			}
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0005BA74 File Offset: 0x00059C74
		private void PutImpl(object key, object value, bool overwrite)
		{
			if (key == null)
			{
				throw new ArgumentNullException("null key");
			}
			SortedList.Slot[] array = this.table;
			int num = -1;
			try
			{
				num = this.Find(key);
			}
			catch (Exception)
			{
				throw new InvalidOperationException();
			}
			if (num >= 0)
			{
				if (!overwrite)
				{
					string text = Locale.GetText("Key '{0}' already exists in list.", new object[]
					{
						key
					});
					throw new ArgumentException(text);
				}
				array[num].value = value;
				this.modificationCount++;
				return;
			}
			else
			{
				num = ~num;
				if (num > this.Capacity + 1)
				{
					throw new Exception(string.Concat(new object[]
					{
						"SortedList::internal error (",
						key,
						", ",
						value,
						") at [",
						num,
						"]"
					}));
				}
				this.EnsureCapacity(this.Count + 1, num);
				array = this.table;
				array[num].key = key;
				array[num].value = value;
				this.inUse++;
				this.modificationCount++;
				return;
			}
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0005BBB4 File Offset: 0x00059DB4
		private object GetImpl(object key)
		{
			int num = this.Find(key);
			if (num >= 0)
			{
				return this.table[num].value;
			}
			return null;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0005BBE4 File Offset: 0x00059DE4
		private void InitTable(int capacity, bool forceSize)
		{
			if (!forceSize && capacity < this.defaultCapacity)
			{
				capacity = this.defaultCapacity;
			}
			this.table = new SortedList.Slot[capacity];
			this.inUse = 0;
			this.modificationCount = 0;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0005BC28 File Offset: 0x00059E28
		private void CopyToArray(Array arr, int i, SortedList.EnumeratorMode mode)
		{
			if (arr == null)
			{
				throw new ArgumentNullException("arr");
			}
			if (i < 0 || i + this.Count > arr.Length)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			IEnumerator enumerator = new SortedList.Enumerator(this, mode);
			while (enumerator.MoveNext())
			{
				object value = enumerator.Current;
				arr.SetValue(value, i++);
			}
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0005BC98 File Offset: 0x00059E98
		private int Find(object key)
		{
			SortedList.Slot[] array = this.table;
			int count = this.Count;
			if (count == 0)
			{
				return -1;
			}
			IComparer comparer;
			if (this.comparer == null)
			{
				IComparer @default = Comparer.Default;
				comparer = @default;
			}
			else
			{
				comparer = this.comparer;
			}
			IComparer comparer2 = comparer;
			int i = 0;
			int num = count - 1;
			while (i <= num)
			{
				int num2 = i + num >> 1;
				int num3 = comparer2.Compare(array[num2].key, key);
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x040008A6 RID: 2214
		private static readonly int INITIAL_SIZE = 16;

		// Token: 0x040008A7 RID: 2215
		private int inUse;

		// Token: 0x040008A8 RID: 2216
		private int modificationCount;

		// Token: 0x040008A9 RID: 2217
		private SortedList.Slot[] table;

		// Token: 0x040008AA RID: 2218
		private IComparer comparer;

		// Token: 0x040008AB RID: 2219
		private int defaultCapacity;

		// Token: 0x020001CC RID: 460
		[Serializable]
		internal struct Slot
		{
			// Token: 0x040008AC RID: 2220
			internal object key;

			// Token: 0x040008AD RID: 2221
			internal object value;
		}

		// Token: 0x020001CD RID: 461
		private enum EnumeratorMode
		{
			// Token: 0x040008AF RID: 2223
			KEY_MODE,
			// Token: 0x040008B0 RID: 2224
			VALUE_MODE,
			// Token: 0x040008B1 RID: 2225
			ENTRY_MODE
		}

		// Token: 0x020001CE RID: 462
		private sealed class Enumerator : IEnumerator, ICloneable, IDictionaryEnumerator
		{
			// Token: 0x060017D5 RID: 6101 RVA: 0x0005BD34 File Offset: 0x00059F34
			public Enumerator(SortedList host, SortedList.EnumeratorMode mode)
			{
				this.host = host;
				this.stamp = host.modificationCount;
				this.size = host.Count;
				this.mode = mode;
				this.Reset();
			}

			// Token: 0x060017D6 RID: 6102 RVA: 0x0005BD74 File Offset: 0x00059F74
			public Enumerator(SortedList host) : this(host, SortedList.EnumeratorMode.ENTRY_MODE)
			{
			}

			// Token: 0x060017D8 RID: 6104 RVA: 0x0005BD8C File Offset: 0x00059F8C
			public void Reset()
			{
				if (this.host.modificationCount != this.stamp || this.invalid)
				{
					throw new InvalidOperationException(SortedList.Enumerator.xstr);
				}
				this.pos = -1;
				this.currentKey = null;
				this.currentValue = null;
			}

			// Token: 0x060017D9 RID: 6105 RVA: 0x0005BDDC File Offset: 0x00059FDC
			public bool MoveNext()
			{
				if (this.host.modificationCount != this.stamp || this.invalid)
				{
					throw new InvalidOperationException(SortedList.Enumerator.xstr);
				}
				SortedList.Slot[] table = this.host.table;
				if (++this.pos < this.size)
				{
					SortedList.Slot slot = table[this.pos];
					this.currentKey = slot.key;
					this.currentValue = slot.value;
					return true;
				}
				this.currentKey = null;
				this.currentValue = null;
				return false;
			}

			// Token: 0x170003B5 RID: 949
			// (get) Token: 0x060017DA RID: 6106 RVA: 0x0005BE7C File Offset: 0x0005A07C
			public DictionaryEntry Entry
			{
				get
				{
					if (this.invalid || this.pos >= this.size || this.pos == -1)
					{
						throw new InvalidOperationException(SortedList.Enumerator.xstr);
					}
					return new DictionaryEntry(this.currentKey, this.currentValue);
				}
			}

			// Token: 0x170003B6 RID: 950
			// (get) Token: 0x060017DB RID: 6107 RVA: 0x0005BED0 File Offset: 0x0005A0D0
			public object Key
			{
				get
				{
					if (this.invalid || this.pos >= this.size || this.pos == -1)
					{
						throw new InvalidOperationException(SortedList.Enumerator.xstr);
					}
					return this.currentKey;
				}
			}

			// Token: 0x170003B7 RID: 951
			// (get) Token: 0x060017DC RID: 6108 RVA: 0x0005BF0C File Offset: 0x0005A10C
			public object Value
			{
				get
				{
					if (this.invalid || this.pos >= this.size || this.pos == -1)
					{
						throw new InvalidOperationException(SortedList.Enumerator.xstr);
					}
					return this.currentValue;
				}
			}

			// Token: 0x170003B8 RID: 952
			// (get) Token: 0x060017DD RID: 6109 RVA: 0x0005BF48 File Offset: 0x0005A148
			public object Current
			{
				get
				{
					if (this.invalid || this.pos >= this.size || this.pos == -1)
					{
						throw new InvalidOperationException(SortedList.Enumerator.xstr);
					}
					switch (this.mode)
					{
					case SortedList.EnumeratorMode.KEY_MODE:
						return this.currentKey;
					case SortedList.EnumeratorMode.VALUE_MODE:
						return this.currentValue;
					case SortedList.EnumeratorMode.ENTRY_MODE:
						return this.Entry;
					default:
						throw new NotSupportedException(this.mode + " is not a supported mode.");
					}
				}
			}

			// Token: 0x060017DE RID: 6110 RVA: 0x0005BFDC File Offset: 0x0005A1DC
			public object Clone()
			{
				return new SortedList.Enumerator(this.host, this.mode)
				{
					stamp = this.stamp,
					pos = this.pos,
					size = this.size,
					currentKey = this.currentKey,
					currentValue = this.currentValue,
					invalid = this.invalid
				};
			}

			// Token: 0x040008B2 RID: 2226
			private SortedList host;

			// Token: 0x040008B3 RID: 2227
			private int stamp;

			// Token: 0x040008B4 RID: 2228
			private int pos;

			// Token: 0x040008B5 RID: 2229
			private int size;

			// Token: 0x040008B6 RID: 2230
			private SortedList.EnumeratorMode mode;

			// Token: 0x040008B7 RID: 2231
			private object currentKey;

			// Token: 0x040008B8 RID: 2232
			private object currentValue;

			// Token: 0x040008B9 RID: 2233
			private bool invalid;

			// Token: 0x040008BA RID: 2234
			private static readonly string xstr = "SortedList.Enumerator: snapshot out of sync.";
		}

		// Token: 0x020001CF RID: 463
		[Serializable]
		private class ListKeys : IEnumerable, ICollection, IList
		{
			// Token: 0x060017DF RID: 6111 RVA: 0x0005C044 File Offset: 0x0005A244
			public ListKeys(SortedList host)
			{
				if (host == null)
				{
					throw new ArgumentNullException();
				}
				this.host = host;
			}

			// Token: 0x170003B9 RID: 953
			// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0005C060 File Offset: 0x0005A260
			public virtual int Count
			{
				get
				{
					return this.host.Count;
				}
			}

			// Token: 0x170003BA RID: 954
			// (get) Token: 0x060017E1 RID: 6113 RVA: 0x0005C070 File Offset: 0x0005A270
			public virtual bool IsSynchronized
			{
				get
				{
					return this.host.IsSynchronized;
				}
			}

			// Token: 0x170003BB RID: 955
			// (get) Token: 0x060017E2 RID: 6114 RVA: 0x0005C080 File Offset: 0x0005A280
			public virtual object SyncRoot
			{
				get
				{
					return this.host.SyncRoot;
				}
			}

			// Token: 0x060017E3 RID: 6115 RVA: 0x0005C090 File Offset: 0x0005A290
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				this.host.CopyToArray(array, arrayIndex, SortedList.EnumeratorMode.KEY_MODE);
			}

			// Token: 0x170003BC RID: 956
			// (get) Token: 0x060017E4 RID: 6116 RVA: 0x0005C0A0 File Offset: 0x0005A2A0
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003BD RID: 957
			// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0005C0A4 File Offset: 0x0005A2A4
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003BE RID: 958
			public virtual object this[int index]
			{
				get
				{
					return this.host.GetKey(index);
				}
				set
				{
					throw new NotSupportedException("attempt to modify a key");
				}
			}

			// Token: 0x060017E8 RID: 6120 RVA: 0x0005C0C4 File Offset: 0x0005A2C4
			public virtual int Add(object value)
			{
				throw new NotSupportedException("IList::Add not supported");
			}

			// Token: 0x060017E9 RID: 6121 RVA: 0x0005C0D0 File Offset: 0x0005A2D0
			public virtual void Clear()
			{
				throw new NotSupportedException("IList::Clear not supported");
			}

			// Token: 0x060017EA RID: 6122 RVA: 0x0005C0DC File Offset: 0x0005A2DC
			public virtual bool Contains(object key)
			{
				return this.host.Contains(key);
			}

			// Token: 0x060017EB RID: 6123 RVA: 0x0005C0EC File Offset: 0x0005A2EC
			public virtual int IndexOf(object key)
			{
				return this.host.IndexOfKey(key);
			}

			// Token: 0x060017EC RID: 6124 RVA: 0x0005C0FC File Offset: 0x0005A2FC
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException("IList::Insert not supported");
			}

			// Token: 0x060017ED RID: 6125 RVA: 0x0005C108 File Offset: 0x0005A308
			public virtual void Remove(object value)
			{
				throw new NotSupportedException("IList::Remove not supported");
			}

			// Token: 0x060017EE RID: 6126 RVA: 0x0005C114 File Offset: 0x0005A314
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("IList::RemoveAt not supported");
			}

			// Token: 0x060017EF RID: 6127 RVA: 0x0005C120 File Offset: 0x0005A320
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.Enumerator(this.host, SortedList.EnumeratorMode.KEY_MODE);
			}

			// Token: 0x040008BB RID: 2235
			private SortedList host;
		}

		// Token: 0x020001D0 RID: 464
		[Serializable]
		private class ListValues : IEnumerable, ICollection, IList
		{
			// Token: 0x060017F0 RID: 6128 RVA: 0x0005C130 File Offset: 0x0005A330
			public ListValues(SortedList host)
			{
				if (host == null)
				{
					throw new ArgumentNullException();
				}
				this.host = host;
			}

			// Token: 0x170003BF RID: 959
			// (get) Token: 0x060017F1 RID: 6129 RVA: 0x0005C14C File Offset: 0x0005A34C
			public virtual int Count
			{
				get
				{
					return this.host.Count;
				}
			}

			// Token: 0x170003C0 RID: 960
			// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0005C15C File Offset: 0x0005A35C
			public virtual bool IsSynchronized
			{
				get
				{
					return this.host.IsSynchronized;
				}
			}

			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0005C16C File Offset: 0x0005A36C
			public virtual object SyncRoot
			{
				get
				{
					return this.host.SyncRoot;
				}
			}

			// Token: 0x060017F4 RID: 6132 RVA: 0x0005C17C File Offset: 0x0005A37C
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				this.host.CopyToArray(array, arrayIndex, SortedList.EnumeratorMode.VALUE_MODE);
			}

			// Token: 0x170003C2 RID: 962
			// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0005C18C File Offset: 0x0005A38C
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003C3 RID: 963
			// (get) Token: 0x060017F6 RID: 6134 RVA: 0x0005C190 File Offset: 0x0005A390
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003C4 RID: 964
			public virtual object this[int index]
			{
				get
				{
					return this.host.GetByIndex(index);
				}
				set
				{
					throw new NotSupportedException("This operation is not supported on GetValueList return");
				}
			}

			// Token: 0x060017F9 RID: 6137 RVA: 0x0005C1B0 File Offset: 0x0005A3B0
			public virtual int Add(object value)
			{
				throw new NotSupportedException("IList::Add not supported");
			}

			// Token: 0x060017FA RID: 6138 RVA: 0x0005C1BC File Offset: 0x0005A3BC
			public virtual void Clear()
			{
				throw new NotSupportedException("IList::Clear not supported");
			}

			// Token: 0x060017FB RID: 6139 RVA: 0x0005C1C8 File Offset: 0x0005A3C8
			public virtual bool Contains(object value)
			{
				return this.host.ContainsValue(value);
			}

			// Token: 0x060017FC RID: 6140 RVA: 0x0005C1D8 File Offset: 0x0005A3D8
			public virtual int IndexOf(object value)
			{
				return this.host.IndexOfValue(value);
			}

			// Token: 0x060017FD RID: 6141 RVA: 0x0005C1E8 File Offset: 0x0005A3E8
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException("IList::Insert not supported");
			}

			// Token: 0x060017FE RID: 6142 RVA: 0x0005C1F4 File Offset: 0x0005A3F4
			public virtual void Remove(object value)
			{
				throw new NotSupportedException("IList::Remove not supported");
			}

			// Token: 0x060017FF RID: 6143 RVA: 0x0005C200 File Offset: 0x0005A400
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("IList::RemoveAt not supported");
			}

			// Token: 0x06001800 RID: 6144 RVA: 0x0005C20C File Offset: 0x0005A40C
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.Enumerator(this.host, SortedList.EnumeratorMode.VALUE_MODE);
			}

			// Token: 0x040008BC RID: 2236
			private SortedList host;
		}

		// Token: 0x020001D1 RID: 465
		private class SynchedSortedList : SortedList
		{
			// Token: 0x06001801 RID: 6145 RVA: 0x0005C21C File Offset: 0x0005A41C
			public SynchedSortedList(SortedList host)
			{
				if (host == null)
				{
					throw new ArgumentNullException();
				}
				this.host = host;
			}

			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x06001802 RID: 6146 RVA: 0x0005C238 File Offset: 0x0005A438
			// (set) Token: 0x06001803 RID: 6147 RVA: 0x0005C294 File Offset: 0x0005A494
			public override int Capacity
			{
				get
				{
					object syncRoot = this.host.SyncRoot;
					int capacity;
					lock (syncRoot)
					{
						capacity = this.host.Capacity;
					}
					return capacity;
				}
				set
				{
					object syncRoot = this.host.SyncRoot;
					lock (syncRoot)
					{
						this.host.Capacity = value;
					}
				}
			}

			// Token: 0x170003C6 RID: 966
			// (get) Token: 0x06001804 RID: 6148 RVA: 0x0005C2E8 File Offset: 0x0005A4E8
			public override int Count
			{
				get
				{
					return this.host.Count;
				}
			}

			// Token: 0x170003C7 RID: 967
			// (get) Token: 0x06001805 RID: 6149 RVA: 0x0005C2F8 File Offset: 0x0005A4F8
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003C8 RID: 968
			// (get) Token: 0x06001806 RID: 6150 RVA: 0x0005C2FC File Offset: 0x0005A4FC
			public override object SyncRoot
			{
				get
				{
					return this.host.SyncRoot;
				}
			}

			// Token: 0x170003C9 RID: 969
			// (get) Token: 0x06001807 RID: 6151 RVA: 0x0005C30C File Offset: 0x0005A50C
			public override bool IsFixedSize
			{
				get
				{
					return this.host.IsFixedSize;
				}
			}

			// Token: 0x170003CA RID: 970
			// (get) Token: 0x06001808 RID: 6152 RVA: 0x0005C31C File Offset: 0x0005A51C
			public override bool IsReadOnly
			{
				get
				{
					return this.host.IsReadOnly;
				}
			}

			// Token: 0x170003CB RID: 971
			// (get) Token: 0x06001809 RID: 6153 RVA: 0x0005C32C File Offset: 0x0005A52C
			public override ICollection Keys
			{
				get
				{
					ICollection result = null;
					object syncRoot = this.host.SyncRoot;
					lock (syncRoot)
					{
						result = this.host.Keys;
					}
					return result;
				}
			}

			// Token: 0x170003CC RID: 972
			// (get) Token: 0x0600180A RID: 6154 RVA: 0x0005C384 File Offset: 0x0005A584
			public override ICollection Values
			{
				get
				{
					ICollection result = null;
					object syncRoot = this.host.SyncRoot;
					lock (syncRoot)
					{
						result = this.host.Values;
					}
					return result;
				}
			}

			// Token: 0x170003CD RID: 973
			public override object this[object key]
			{
				get
				{
					object syncRoot = this.host.SyncRoot;
					object impl;
					lock (syncRoot)
					{
						impl = this.host.GetImpl(key);
					}
					return impl;
				}
				set
				{
					object syncRoot = this.host.SyncRoot;
					lock (syncRoot)
					{
						this.host.PutImpl(key, value, true);
					}
				}
			}

			// Token: 0x0600180D RID: 6157 RVA: 0x0005C490 File Offset: 0x0005A690
			public override void CopyTo(Array array, int arrayIndex)
			{
				object syncRoot = this.host.SyncRoot;
				lock (syncRoot)
				{
					this.host.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x0600180E RID: 6158 RVA: 0x0005C4E4 File Offset: 0x0005A6E4
			public override void Add(object key, object value)
			{
				object syncRoot = this.host.SyncRoot;
				lock (syncRoot)
				{
					this.host.PutImpl(key, value, false);
				}
			}

			// Token: 0x0600180F RID: 6159 RVA: 0x0005C53C File Offset: 0x0005A73C
			public override void Clear()
			{
				object syncRoot = this.host.SyncRoot;
				lock (syncRoot)
				{
					this.host.Clear();
				}
			}

			// Token: 0x06001810 RID: 6160 RVA: 0x0005C590 File Offset: 0x0005A790
			public override bool Contains(object key)
			{
				object syncRoot = this.host.SyncRoot;
				bool result;
				lock (syncRoot)
				{
					result = (this.host.Find(key) >= 0);
				}
				return result;
			}

			// Token: 0x06001811 RID: 6161 RVA: 0x0005C5F0 File Offset: 0x0005A7F0
			public override IDictionaryEnumerator GetEnumerator()
			{
				object syncRoot = this.host.SyncRoot;
				IDictionaryEnumerator enumerator;
				lock (syncRoot)
				{
					enumerator = this.host.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06001812 RID: 6162 RVA: 0x0005C64C File Offset: 0x0005A84C
			public override void Remove(object key)
			{
				object syncRoot = this.host.SyncRoot;
				lock (syncRoot)
				{
					this.host.Remove(key);
				}
			}

			// Token: 0x06001813 RID: 6163 RVA: 0x0005C6A0 File Offset: 0x0005A8A0
			public override bool ContainsKey(object key)
			{
				object syncRoot = this.host.SyncRoot;
				bool result;
				lock (syncRoot)
				{
					result = this.host.Contains(key);
				}
				return result;
			}

			// Token: 0x06001814 RID: 6164 RVA: 0x0005C6FC File Offset: 0x0005A8FC
			public override bool ContainsValue(object value)
			{
				object syncRoot = this.host.SyncRoot;
				bool result;
				lock (syncRoot)
				{
					result = this.host.ContainsValue(value);
				}
				return result;
			}

			// Token: 0x06001815 RID: 6165 RVA: 0x0005C758 File Offset: 0x0005A958
			public override object Clone()
			{
				object syncRoot = this.host.SyncRoot;
				object result;
				lock (syncRoot)
				{
					result = (this.host.Clone() as SortedList);
				}
				return result;
			}

			// Token: 0x06001816 RID: 6166 RVA: 0x0005C7B8 File Offset: 0x0005A9B8
			public override object GetByIndex(int index)
			{
				object syncRoot = this.host.SyncRoot;
				object byIndex;
				lock (syncRoot)
				{
					byIndex = this.host.GetByIndex(index);
				}
				return byIndex;
			}

			// Token: 0x06001817 RID: 6167 RVA: 0x0005C814 File Offset: 0x0005AA14
			public override object GetKey(int index)
			{
				object syncRoot = this.host.SyncRoot;
				object key;
				lock (syncRoot)
				{
					key = this.host.GetKey(index);
				}
				return key;
			}

			// Token: 0x06001818 RID: 6168 RVA: 0x0005C870 File Offset: 0x0005AA70
			public override IList GetKeyList()
			{
				object syncRoot = this.host.SyncRoot;
				IList result;
				lock (syncRoot)
				{
					result = new SortedList.ListKeys(this.host);
				}
				return result;
			}

			// Token: 0x06001819 RID: 6169 RVA: 0x0005C8CC File Offset: 0x0005AACC
			public override IList GetValueList()
			{
				object syncRoot = this.host.SyncRoot;
				IList result;
				lock (syncRoot)
				{
					result = new SortedList.ListValues(this.host);
				}
				return result;
			}

			// Token: 0x0600181A RID: 6170 RVA: 0x0005C928 File Offset: 0x0005AB28
			public override void RemoveAt(int index)
			{
				object syncRoot = this.host.SyncRoot;
				lock (syncRoot)
				{
					this.host.RemoveAt(index);
				}
			}

			// Token: 0x0600181B RID: 6171 RVA: 0x0005C97C File Offset: 0x0005AB7C
			public override int IndexOfKey(object key)
			{
				object syncRoot = this.host.SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.host.IndexOfKey(key);
				}
				return result;
			}

			// Token: 0x0600181C RID: 6172 RVA: 0x0005C9D8 File Offset: 0x0005ABD8
			public override int IndexOfValue(object val)
			{
				object syncRoot = this.host.SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.host.IndexOfValue(val);
				}
				return result;
			}

			// Token: 0x0600181D RID: 6173 RVA: 0x0005CA34 File Offset: 0x0005AC34
			public override void SetByIndex(int index, object value)
			{
				object syncRoot = this.host.SyncRoot;
				lock (syncRoot)
				{
					this.host.SetByIndex(index, value);
				}
			}

			// Token: 0x0600181E RID: 6174 RVA: 0x0005CA88 File Offset: 0x0005AC88
			public override void TrimToSize()
			{
				object syncRoot = this.host.SyncRoot;
				lock (syncRoot)
				{
					this.host.TrimToSize();
				}
			}

			// Token: 0x040008BD RID: 2237
			private SortedList host;
		}
	}
}
