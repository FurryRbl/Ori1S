using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Implements the <see cref="T:System.Collections.IList" /> interface using an array whose size is dynamically increased as required.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001A3 RID: 419
	[ComVisible(true)]
	[DebuggerDisplay("Count={Count}")]
	[DebuggerTypeProxy(typeof(CollectionDebuggerView))]
	[Serializable]
	public class ArrayList : IEnumerable, ICloneable, ICollection, IList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x06001541 RID: 5441 RVA: 0x000541BC File Offset: 0x000523BC
		public ArrayList()
		{
			this._items = ArrayList.EmptyArray;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that contains elements copied from the specified collection and that has the same initial capacity as the number of elements copied.</summary>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements are copied to the new list. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is null. </exception>
		// Token: 0x06001542 RID: 5442 RVA: 0x000541D0 File Offset: 0x000523D0
		public ArrayList(ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			Array array = c as Array;
			if (array != null && array.Rank != 1)
			{
				throw new RankException();
			}
			this._items = new object[c.Count];
			this.AddRange(c);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that is empty and has the specified initial capacity.</summary>
		/// <param name="capacity">The number of elements that the new list can initially store. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06001543 RID: 5443 RVA: 0x0005422C File Offset: 0x0005242C
		public ArrayList(int capacity)
		{
			if (capacity < 0)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("capacity", capacity, "The initial capacity can't be smaller than zero.");
			}
			if (capacity == 0)
			{
				capacity = 4;
			}
			this._items = new object[capacity];
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00054268 File Offset: 0x00052468
		private ArrayList(int initialCapacity, bool forceZeroSize)
		{
			if (forceZeroSize)
			{
				this._items = null;
				return;
			}
			throw new InvalidOperationException("Use ArrayList(int)");
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00054290 File Offset: 0x00052490
		private ArrayList(object[] array, int index, int count)
		{
			if (count == 0)
			{
				this._items = new object[4];
			}
			else
			{
				this._items = new object[count];
			}
			Array.Copy(array, index, this._items, 0, count);
			this._size = count;
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000321 RID: 801
		public virtual object this[int index]
		{
			get
			{
				if (index < 0 || index >= this._size)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Index is less than 0 or more than or equal to the list count.");
				}
				return this._items[index];
			}
			set
			{
				if (index < 0 || index >= this._size)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Index is less than 0 or more than or equal to the list count.");
				}
				this._items[index] = value;
				this._version++;
			}
		}

		/// <summary>Gets the number of elements actually contained in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>The number of elements actually contained in the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00054378 File Offset: 0x00052578
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.ArrayList" /> can contain.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.ArrayList" /> can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.ArrayList.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x00054380 File Offset: 0x00052580
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x0005438C File Offset: 0x0005258C
		public virtual int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("Capacity", value, "Must be more than count.");
				}
				object[] array = new object[value];
				Array.Copy(this._items, 0, array, 0, this._size);
				this._items = array;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.ArrayList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.ArrayList" /> has a fixed size; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x000543DC File Offset: 0x000525DC
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.ArrayList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.ArrayList" /> is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x000543E0 File Offset: 0x000525E0
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ArrayList" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ArrayList" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x000543E4 File Offset: 0x000525E4
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x000543E8 File Offset: 0x000525E8
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x000543EC File Offset: 0x000525EC
		private void EnsureCapacity(int count)
		{
			if (count <= this._items.Length)
			{
				return;
			}
			int i = this._items.Length << 1;
			if (i == 0)
			{
				i = 4;
			}
			while (i < count)
			{
				i <<= 1;
			}
			object[] array = new object[i];
			Array.Copy(this._items, 0, array, 0, this._items.Length);
			this._items = array;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00054450 File Offset: 0x00052650
		private void Shift(int index, int count)
		{
			if (count > 0)
			{
				if (this._size + count > this._items.Length)
				{
					int i;
					for (i = ((this._items.Length <= 0) ? 1 : (this._items.Length << 1)); i < this._size + count; i <<= 1)
					{
					}
					object[] array = new object[i];
					Array.Copy(this._items, 0, array, 0, index);
					Array.Copy(this._items, index, array, index + count, this._size - index);
					this._items = array;
				}
				else
				{
					Array.Copy(this._items, index, this._items, index + count, this._size - index);
				}
			}
			else if (count < 0)
			{
				int num = index - count;
				Array.Copy(this._items, num, this._items, index, this._size - num);
				Array.Clear(this._items, this._size + count, -count);
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>The <see cref="T:System.Collections.ArrayList" /> index at which the <paramref name="value" /> has been added.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001552 RID: 5458 RVA: 0x00054548 File Offset: 0x00052748
		public virtual int Add(object value)
		{
			if (this._items.Length <= this._size)
			{
				this.EnsureCapacity(this._size + 1);
			}
			this._items[this._size] = value;
			this._version++;
			return this._size++;
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001553 RID: 5459 RVA: 0x000545A4 File Offset: 0x000527A4
		public virtual void Clear()
		{
			Array.Clear(this._items, 0, this._size);
			this._size = 0;
			this._version++;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.ArrayList" />; otherwise, false.</returns>
		/// <param name="item">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001554 RID: 5460 RVA: 0x000545D0 File Offset: 0x000527D0
		public virtual bool Contains(object item)
		{
			return this.IndexOf(item, 0, this._size) > -1;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x000545E4 File Offset: 0x000527E4
		internal virtual bool Contains(object value, int startIndex, int count)
		{
			return this.IndexOf(value, startIndex, count) > -1;
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.ArrayList" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001556 RID: 5462 RVA: 0x000545F4 File Offset: 0x000527F4
		public virtual int IndexOf(object value)
		{
			return this.IndexOf(value, 0);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from the specified index to the last element.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from <paramref name="startIndex" /> to the last element, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty array.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001557 RID: 5463 RVA: 0x00054600 File Offset: 0x00052800
		public virtual int IndexOf(object value, int startIndex)
		{
			return this.IndexOf(value, startIndex, this._size - startIndex);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that starts at the specified index and contains the specified number of elements.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that starts at <paramref name="startIndex" /> and contains <paramref name="count" /> number of elements, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty array.</param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001558 RID: 5464 RVA: 0x00054614 File Offset: 0x00052814
		public virtual int IndexOf(object value, int startIndex, int count)
		{
			if (startIndex < 0 || startIndex > this._size)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("startIndex", startIndex, "Does not specify valid index.");
			}
			if (count < 0)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("count", count, "Can't be less than 0.");
			}
			if (startIndex > this._size - count)
			{
				throw new ArgumentOutOfRangeException("count", "Start index and count do not specify a valid range.");
			}
			return Array.IndexOf<object>(this._items, value, startIndex, count);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the last occurrence within the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the entire the <see cref="T:System.Collections.ArrayList" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001559 RID: 5465 RVA: 0x00054694 File Offset: 0x00052894
		public virtual int LastIndexOf(object value)
		{
			return this.LastIndexOf(value, this._size - 1);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from the first element to the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that extends from the first element to <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600155A RID: 5466 RVA: 0x000546A8 File Offset: 0x000528A8
		public virtual int LastIndexOf(object value, int startIndex)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the last occurrence within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that contains the specified number of elements and ends at the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in the <see cref="T:System.Collections.ArrayList" /> that contains <paramref name="count" /> number of elements and ends at <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for the <see cref="T:System.Collections.ArrayList" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600155B RID: 5467 RVA: 0x000546B8 File Offset: 0x000528B8
		public virtual int LastIndexOf(object value, int startIndex, int count)
		{
			return Array.LastIndexOf<object>(this._items, value, startIndex, count);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.ArrayList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600155C RID: 5468 RVA: 0x000546C8 File Offset: 0x000528C8
		public virtual void Insert(int index, object value)
		{
			if (index < 0 || index > this._size)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Index must be >= 0 and <= Count.");
			}
			this.Shift(index, 1);
			this._items[index] = value;
			this._size++;
			this._version++;
		}

		/// <summary>Inserts the elements of a collection into the <see cref="T:System.Collections.ArrayList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which the new elements should be inserted. </param>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements should be inserted into the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be null, but it can contain elements that are null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600155D RID: 5469 RVA: 0x0005472C File Offset: 0x0005292C
		public virtual void InsertRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			if (index < 0 || index > this._size)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Index must be >= 0 and <= Count.");
			}
			int count = c.Count;
			if (this._items.Length < this._size + count)
			{
				this.EnsureCapacity(this._size + count);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + count, this._size - index);
			}
			if (this == c.SyncRoot)
			{
				Array.Copy(this._items, 0, this._items, index, index);
				Array.Copy(this._items, index + count, this._items, index << 1, this._size - index);
			}
			else
			{
				c.CopyTo(this._items, index);
			}
			this._size += c.Count;
			this._version++;
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600155E RID: 5470 RVA: 0x00054838 File Offset: 0x00052A38
		public virtual void Remove(object obj)
		{
			int num = this.IndexOf(obj);
			if (num > -1)
			{
				this.RemoveAt(num);
			}
			this._version++;
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based index of the element to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600155F RID: 5471 RVA: 0x0005486C File Offset: 0x00052A6C
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Less than 0 or more than list count.");
			}
			this.Shift(index, -1);
			this._size--;
			this._version++;
		}

		/// <summary>Removes a range of elements from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based starting index of the range of elements to remove. </param>
		/// <param name="count">The number of elements to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001560 RID: 5472 RVA: 0x000548C8 File Offset: 0x00052AC8
		public virtual void RemoveRange(int index, int count)
		{
			ArrayList.CheckRange(index, count, this._size);
			this.Shift(index, -count);
			this._size -= count;
			this._version++;
		}

		/// <summary>Reverses the order of the elements in the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001561 RID: 5473 RVA: 0x00054908 File Offset: 0x00052B08
		public virtual void Reverse()
		{
			Array.Reverse(this._items, 0, this._size);
			this._version++;
		}

		/// <summary>Reverses the order of the elements in the specified range.</summary>
		/// <param name="index">The zero-based starting index of the range to reverse. </param>
		/// <param name="count">The number of elements in the range to reverse. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001562 RID: 5474 RVA: 0x00054938 File Offset: 0x00052B38
		public virtual void Reverse(int index, int count)
		{
			ArrayList.CheckRange(index, count, this._size);
			Array.Reverse(this._items, index, count);
			this._version++;
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the beginning of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ArrayList" /> is greater than the number of elements that the destination <paramref name="array" /> can contain. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001563 RID: 5475 RVA: 0x00054970 File Offset: 0x00052B70
		public virtual void CopyTo(Array array)
		{
			Array.Copy(this._items, array, this._size);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ArrayList" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001564 RID: 5476 RVA: 0x00054984 File Offset: 0x00052B84
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			this.CopyTo(0, array, arrayIndex, this._size);
		}

		/// <summary>Copies a range of elements from the <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="index">The zero-based index in the source <see cref="T:System.Collections.ArrayList" /> at which copying begins. </param>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <param name="count">The number of elements to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="arrayIndex" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- <paramref name="index" /> is equal to or greater than the <see cref="P:System.Collections.ArrayList.Count" /> of the source <see cref="T:System.Collections.ArrayList" />.-or- The number of elements from <paramref name="index" /> to the end of the source <see cref="T:System.Collections.ArrayList" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001565 RID: 5477 RVA: 0x00054998 File Offset: 0x00052B98
		public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Must have only 1 dimensions.", "array");
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		/// <summary>Returns an enumerator for the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001566 RID: 5478 RVA: 0x000549D8 File Offset: 0x00052BD8
		public virtual IEnumerator GetEnumerator()
		{
			return new ArrayList.SimpleEnumerator(this);
		}

		/// <summary>Returns an enumerator for a range of elements in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the specified range of elements in the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <param name="index">The zero-based starting index of the <see cref="T:System.Collections.ArrayList" /> section that the enumerator should refer to. </param>
		/// <param name="count">The number of elements in the <see cref="T:System.Collections.ArrayList" /> section that the enumerator should refer to. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001567 RID: 5479 RVA: 0x000549E0 File Offset: 0x00052BE0
		public virtual IEnumerator GetEnumerator(int index, int count)
		{
			ArrayList.CheckRange(index, count, this._size);
			return new ArrayList.ArrayListEnumerator(this, index, count);
		}

		/// <summary>Adds the elements of an <see cref="T:System.Collections.ICollection" /> to the end of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements should be added to the end of the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be null, but it can contain elements that are null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001568 RID: 5480 RVA: 0x000549F8 File Offset: 0x00052BF8
		public virtual void AddRange(ICollection c)
		{
			this.InsertRange(this._size, c);
		}

		/// <summary>Searches the entire sorted <see cref="T:System.Collections.ArrayList" /> for an element using the default comparer and returns the zero-based index of the element.</summary>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be null. </param>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001569 RID: 5481 RVA: 0x00054A08 File Offset: 0x00052C08
		public virtual int BinarySearch(object value)
		{
			int result;
			try
			{
				result = Array.BinarySearch<object>(this._items, 0, this._size, value);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException(ex.Message);
			}
			return result;
		}

		/// <summary>Searches the entire sorted <see cref="T:System.Collections.ArrayList" /> for an element using the specified comparer and returns the zero-based index of the element.</summary>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be null. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or- null to use the default comparer that is the <see cref="T:System.IComparable" /> implementation of each element. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparer" /> is null and neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null and <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600156A RID: 5482 RVA: 0x00054A64 File Offset: 0x00052C64
		public virtual int BinarySearch(object value, IComparer comparer)
		{
			int result;
			try
			{
				result = Array.BinarySearch(this._items, 0, this._size, value, comparer);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException(ex.Message);
			}
			return result;
		}

		/// <summary>Searches a range of elements in the sorted <see cref="T:System.Collections.ArrayList" /> for an element using the specified comparer and returns the zero-based index of the element.</summary>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <param name="index">The zero-based starting index of the range to search. </param>
		/// <param name="count">The length of the range to search. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be null. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or- null to use the default comparer that is the <see cref="T:System.IComparable" /> implementation of each element. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in the <see cref="T:System.Collections.ArrayList" />.-or- <paramref name="comparer" /> is null and neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null and <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600156B RID: 5483 RVA: 0x00054AC0 File Offset: 0x00052CC0
		public virtual int BinarySearch(int index, int count, object value, IComparer comparer)
		{
			int result;
			try
			{
				result = Array.BinarySearch(this._items, index, count, value, comparer);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException(ex.Message);
			}
			return result;
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> which represents a subset of the elements in the source <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> which represents a subset of the elements in the source <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <param name="index">The zero-based <see cref="T:System.Collections.ArrayList" /> index at which the range starts. </param>
		/// <param name="count">The number of elements in the range. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600156C RID: 5484 RVA: 0x00054B18 File Offset: 0x00052D18
		public virtual ArrayList GetRange(int index, int count)
		{
			ArrayList.CheckRange(index, count, this._size);
			if (this.IsSynchronized)
			{
				return ArrayList.Synchronized(new ArrayList.RangedArrayList(this, index, count));
			}
			return new ArrayList.RangedArrayList(this, index, count);
		}

		/// <summary>Copies the elements of a collection over a range of elements in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based <see cref="T:System.Collections.ArrayList" /> index at which to start copying the elements of <paramref name="c" />. </param>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements to copy to the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be null, but it can contain elements that are null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> plus the number of elements in <paramref name="c" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600156D RID: 5485 RVA: 0x00054B54 File Offset: 0x00052D54
		public virtual void SetRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			if (index < 0 || index + c.Count > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			c.CopyTo(this._items, index);
			this._version++;
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600156E RID: 5486 RVA: 0x00054BB4 File Offset: 0x00052DB4
		public virtual void TrimToSize()
		{
			if (this._items.Length > this._size)
			{
				object[] array;
				if (this._size == 0)
				{
					array = new object[4];
				}
				else
				{
					array = new object[this._size];
				}
				Array.Copy(this._items, 0, array, 0, this._size);
				this._items = array;
			}
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600156F RID: 5487 RVA: 0x00054C14 File Offset: 0x00052E14
		public virtual void Sort()
		{
			Array.Sort<object>(this._items, 0, this._size);
			this._version++;
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.ArrayList" /> using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or- null to use the <see cref="T:System.IComparable" /> implementation of each element. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001570 RID: 5488 RVA: 0x00054C44 File Offset: 0x00052E44
		public virtual void Sort(IComparer comparer)
		{
			Array.Sort(this._items, 0, this._size, comparer);
		}

		/// <summary>Sorts the elements in a range of elements in <see cref="T:System.Collections.ArrayList" /> using the specified comparer.</summary>
		/// <param name="index">The zero-based starting index of the range to sort. </param>
		/// <param name="count">The length of the range to sort. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or- null to use the <see cref="T:System.IComparable" /> implementation of each element. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001571 RID: 5489 RVA: 0x00054C5C File Offset: 0x00052E5C
		public virtual void Sort(int index, int count, IComparer comparer)
		{
			ArrayList.CheckRange(index, count, this._size);
			Array.Sort(this._items, index, count, comparer);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ArrayList" /> to a new <see cref="T:System.Object" /> array.</summary>
		/// <returns>An <see cref="T:System.Object" /> array containing copies of the elements of the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001572 RID: 5490 RVA: 0x00054C7C File Offset: 0x00052E7C
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			this.CopyTo(array);
			return array;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ArrayList" /> to a new array of the specified element type.</summary>
		/// <returns>An array of the specified element type containing copies of the elements of the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <param name="type">The element <see cref="T:System.Type" /> of the destination array to create and copy elements to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the specified type. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001573 RID: 5491 RVA: 0x00054CA0 File Offset: 0x00052EA0
		public virtual Array ToArray(Type type)
		{
			Array array = Array.CreateInstance(type, this._size);
			this.CopyTo(array);
			return array;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001574 RID: 5492 RVA: 0x00054CC4 File Offset: 0x00052EC4
		public virtual object Clone()
		{
			return new ArrayList(this._items, 0, this._size);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00054CD8 File Offset: 0x00052ED8
		internal static void CheckRange(int index, int count, int listCount)
		{
			if (index < 0)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Can't be less than 0.");
			}
			if (count < 0)
			{
				ArrayList.ThrowNewArgumentOutOfRangeException("count", count, "Can't be less than 0.");
			}
			if (index > listCount - count)
			{
				throw new ArgumentException("Index and count do not denote a valid range of elements.", "index");
			}
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00054D38 File Offset: 0x00052F38
		internal static void ThrowNewArgumentOutOfRangeException(string name, object actual, string message)
		{
			throw new ArgumentOutOfRangeException(name, actual, message);
		}

		/// <summary>Creates an <see cref="T:System.Collections.ArrayList" /> wrapper for a specific <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The <see cref="T:System.Collections.ArrayList" /> wrapper around the <see cref="T:System.Collections.IList" />.</returns>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to wrap.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001577 RID: 5495 RVA: 0x00054D44 File Offset: 0x00052F44
		public static ArrayList Adapter(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			ArrayList arrayList = list as ArrayList;
			if (arrayList != null)
			{
				return arrayList;
			}
			arrayList = new ArrayList.ArrayListAdapter(list);
			if (list.IsSynchronized)
			{
				return ArrayList.Synchronized(arrayList);
			}
			return arrayList;
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> wrapper that is synchronized (thread safe).</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> wrapper that is synchronized (thread safe).</returns>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001578 RID: 5496 RVA: 0x00054D8C File Offset: 0x00052F8C
		public static ArrayList Synchronized(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsSynchronized)
			{
				return list;
			}
			return new ArrayList.SynchronizedArrayListWrapper(list);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IList" /> wrapper that is synchronized (thread safe).</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> wrapper that is synchronized (thread safe).</returns>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001579 RID: 5497 RVA: 0x00054DC0 File Offset: 0x00052FC0
		public static IList Synchronized(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsSynchronized)
			{
				return list;
			}
			return new ArrayList.SynchronizedListWrapper(list);
		}

		/// <summary>Returns a read-only <see cref="T:System.Collections.ArrayList" /> wrapper.</summary>
		/// <returns>A read-only <see cref="T:System.Collections.ArrayList" /> wrapper around <paramref name="list" />.</returns>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600157A RID: 5498 RVA: 0x00054DF4 File Offset: 0x00052FF4
		public static ArrayList ReadOnly(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsReadOnly)
			{
				return list;
			}
			return new ArrayList.ReadOnlyArrayListWrapper(list);
		}

		/// <summary>Returns a read-only <see cref="T:System.Collections.IList" /> wrapper.</summary>
		/// <returns>A read-only <see cref="T:System.Collections.IList" /> wrapper around <paramref name="list" />.</returns>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600157B RID: 5499 RVA: 0x00054E28 File Offset: 0x00053028
		public static IList ReadOnly(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsReadOnly)
			{
				return list;
			}
			return new ArrayList.ReadOnlyListWrapper(list);
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> wrapper with a fixed size.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> wrapper with a fixed size.</returns>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600157C RID: 5500 RVA: 0x00054E5C File Offset: 0x0005305C
		public static ArrayList FixedSize(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsFixedSize)
			{
				return list;
			}
			return new ArrayList.FixedSizeArrayListWrapper(list);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IList" /> wrapper with a fixed size.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> wrapper with a fixed size.</returns>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600157D RID: 5501 RVA: 0x00054E90 File Offset: 0x00053090
		public static IList FixedSize(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.IsFixedSize)
			{
				return list;
			}
			return new ArrayList.FixedSizeListWrapper(list);
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> whose elements are copies of the specified value.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> with <paramref name="count" /> number of elements, all of which are copies of <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to copy multiple times in the new <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <param name="count">The number of times <paramref name="value" /> should be copied. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600157E RID: 5502 RVA: 0x00054EC4 File Offset: 0x000530C4
		public static ArrayList Repeat(object value, int count)
		{
			ArrayList arrayList = new ArrayList(count);
			for (int i = 0; i < count; i++)
			{
				arrayList.Add(value);
			}
			return arrayList;
		}

		// Token: 0x04000849 RID: 2121
		private const int DefaultInitialCapacity = 4;

		// Token: 0x0400084A RID: 2122
		private int _size;

		// Token: 0x0400084B RID: 2123
		private object[] _items;

		// Token: 0x0400084C RID: 2124
		private int _version;

		// Token: 0x0400084D RID: 2125
		private static readonly object[] EmptyArray = new object[0];

		// Token: 0x020001A4 RID: 420
		private sealed class ArrayListEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x0600157F RID: 5503 RVA: 0x00054EF4 File Offset: 0x000530F4
			public ArrayListEnumerator(ArrayList list) : this(list, 0, list.Count)
			{
			}

			// Token: 0x06001580 RID: 5504 RVA: 0x00054F04 File Offset: 0x00053104
			public ArrayListEnumerator(ArrayList list, int index, int count)
			{
				this.m_List = list;
				this.m_Index = index;
				this.m_Count = count;
				this.m_Pos = this.m_Index - 1;
				this.m_Current = null;
				this.m_ExpectedStateChanges = list._version;
			}

			// Token: 0x06001581 RID: 5505 RVA: 0x00054F50 File Offset: 0x00053150
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x17000328 RID: 808
			// (get) Token: 0x06001582 RID: 5506 RVA: 0x00054F58 File Offset: 0x00053158
			public object Current
			{
				get
				{
					if (this.m_Pos == this.m_Index - 1)
					{
						throw new InvalidOperationException("Enumerator unusable (Reset pending, or past end of array.");
					}
					return this.m_Current;
				}
			}

			// Token: 0x06001583 RID: 5507 RVA: 0x00054F8C File Offset: 0x0005318C
			public bool MoveNext()
			{
				if (this.m_List._version != this.m_ExpectedStateChanges)
				{
					throw new InvalidOperationException("List has changed.");
				}
				this.m_Pos++;
				if (this.m_Pos - this.m_Index < this.m_Count)
				{
					this.m_Current = this.m_List[this.m_Pos];
					return true;
				}
				return false;
			}

			// Token: 0x06001584 RID: 5508 RVA: 0x00054FFC File Offset: 0x000531FC
			public void Reset()
			{
				this.m_Current = null;
				this.m_Pos = this.m_Index - 1;
			}

			// Token: 0x0400084E RID: 2126
			private int m_Pos;

			// Token: 0x0400084F RID: 2127
			private int m_Index;

			// Token: 0x04000850 RID: 2128
			private int m_Count;

			// Token: 0x04000851 RID: 2129
			private object m_Current;

			// Token: 0x04000852 RID: 2130
			private ArrayList m_List;

			// Token: 0x04000853 RID: 2131
			private int m_ExpectedStateChanges;
		}

		// Token: 0x020001A5 RID: 421
		private sealed class SimpleEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06001585 RID: 5509 RVA: 0x00055014 File Offset: 0x00053214
			public SimpleEnumerator(ArrayList list)
			{
				this.list = list;
				this.index = -1;
				this.version = list._version;
				this.currentElement = ArrayList.SimpleEnumerator.endFlag;
			}

			// Token: 0x06001587 RID: 5511 RVA: 0x00055050 File Offset: 0x00053250
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06001588 RID: 5512 RVA: 0x00055058 File Offset: 0x00053258
			public bool MoveNext()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException("List has changed.");
				}
				if (++this.index < this.list.Count)
				{
					this.currentElement = this.list[this.index];
					return true;
				}
				this.currentElement = ArrayList.SimpleEnumerator.endFlag;
				return false;
			}

			// Token: 0x17000329 RID: 809
			// (get) Token: 0x06001589 RID: 5513 RVA: 0x000550CC File Offset: 0x000532CC
			public object Current
			{
				get
				{
					if (this.currentElement != ArrayList.SimpleEnumerator.endFlag)
					{
						return this.currentElement;
					}
					if (this.index == -1)
					{
						throw new InvalidOperationException("Enumerator not started");
					}
					throw new InvalidOperationException("Enumerator ended");
				}
			}

			// Token: 0x0600158A RID: 5514 RVA: 0x00055114 File Offset: 0x00053314
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException("List has changed.");
				}
				this.currentElement = ArrayList.SimpleEnumerator.endFlag;
				this.index = -1;
			}

			// Token: 0x04000854 RID: 2132
			private ArrayList list;

			// Token: 0x04000855 RID: 2133
			private int index;

			// Token: 0x04000856 RID: 2134
			private int version;

			// Token: 0x04000857 RID: 2135
			private object currentElement;

			// Token: 0x04000858 RID: 2136
			private static object endFlag = new object();
		}

		// Token: 0x020001A6 RID: 422
		[Serializable]
		private sealed class ArrayListAdapter : ArrayList
		{
			// Token: 0x0600158B RID: 5515 RVA: 0x0005514C File Offset: 0x0005334C
			public ArrayListAdapter(IList adaptee) : base(0, true)
			{
				this.m_Adaptee = adaptee;
			}

			// Token: 0x1700032A RID: 810
			public override object this[int index]
			{
				get
				{
					return this.m_Adaptee[index];
				}
				set
				{
					this.m_Adaptee[index] = value;
				}
			}

			// Token: 0x1700032B RID: 811
			// (get) Token: 0x0600158E RID: 5518 RVA: 0x00055180 File Offset: 0x00053380
			public override int Count
			{
				get
				{
					return this.m_Adaptee.Count;
				}
			}

			// Token: 0x1700032C RID: 812
			// (get) Token: 0x0600158F RID: 5519 RVA: 0x00055190 File Offset: 0x00053390
			// (set) Token: 0x06001590 RID: 5520 RVA: 0x000551A0 File Offset: 0x000533A0
			public override int Capacity
			{
				get
				{
					return this.m_Adaptee.Count;
				}
				set
				{
					if (value < this.m_Adaptee.Count)
					{
						throw new ArgumentException("capacity");
					}
				}
			}

			// Token: 0x1700032D RID: 813
			// (get) Token: 0x06001591 RID: 5521 RVA: 0x000551C0 File Offset: 0x000533C0
			public override bool IsFixedSize
			{
				get
				{
					return this.m_Adaptee.IsFixedSize;
				}
			}

			// Token: 0x1700032E RID: 814
			// (get) Token: 0x06001592 RID: 5522 RVA: 0x000551D0 File Offset: 0x000533D0
			public override bool IsReadOnly
			{
				get
				{
					return this.m_Adaptee.IsReadOnly;
				}
			}

			// Token: 0x1700032F RID: 815
			// (get) Token: 0x06001593 RID: 5523 RVA: 0x000551E0 File Offset: 0x000533E0
			public override object SyncRoot
			{
				get
				{
					return this.m_Adaptee.SyncRoot;
				}
			}

			// Token: 0x06001594 RID: 5524 RVA: 0x000551F0 File Offset: 0x000533F0
			public override int Add(object value)
			{
				return this.m_Adaptee.Add(value);
			}

			// Token: 0x06001595 RID: 5525 RVA: 0x00055200 File Offset: 0x00053400
			public override void Clear()
			{
				this.m_Adaptee.Clear();
			}

			// Token: 0x06001596 RID: 5526 RVA: 0x00055210 File Offset: 0x00053410
			public override bool Contains(object value)
			{
				return this.m_Adaptee.Contains(value);
			}

			// Token: 0x06001597 RID: 5527 RVA: 0x00055220 File Offset: 0x00053420
			public override int IndexOf(object value)
			{
				return this.m_Adaptee.IndexOf(value);
			}

			// Token: 0x06001598 RID: 5528 RVA: 0x00055230 File Offset: 0x00053430
			public override int IndexOf(object value, int startIndex)
			{
				return this.IndexOf(value, startIndex, this.m_Adaptee.Count - startIndex);
			}

			// Token: 0x06001599 RID: 5529 RVA: 0x00055248 File Offset: 0x00053448
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this.m_Adaptee.Count)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("startIndex", startIndex, "Does not specify valid index.");
				}
				if (count < 0)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("count", count, "Can't be less than 0.");
				}
				if (startIndex > this.m_Adaptee.Count - count)
				{
					throw new ArgumentOutOfRangeException("count", "Start index and count do not specify a valid range.");
				}
				if (value == null)
				{
					for (int i = startIndex; i < startIndex + count; i++)
					{
						if (this.m_Adaptee[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j < startIndex + count; j++)
					{
						if (value.Equals(this.m_Adaptee[j]))
						{
							return j;
						}
					}
				}
				return -1;
			}

			// Token: 0x0600159A RID: 5530 RVA: 0x00055324 File Offset: 0x00053524
			public override int LastIndexOf(object value)
			{
				return this.LastIndexOf(value, this.m_Adaptee.Count - 1);
			}

			// Token: 0x0600159B RID: 5531 RVA: 0x0005533C File Offset: 0x0005353C
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x0600159C RID: 5532 RVA: 0x0005534C File Offset: 0x0005354C
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("startIndex", startIndex, "< 0");
				}
				if (count < 0)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("count", count, "count is negative.");
				}
				if (startIndex - count + 1 < 0)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("count", count, "count is too large.");
				}
				if (value == null)
				{
					for (int i = startIndex; i > startIndex - count; i--)
					{
						if (this.m_Adaptee[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j > startIndex - count; j--)
					{
						if (value.Equals(this.m_Adaptee[j]))
						{
							return j;
						}
					}
				}
				return -1;
			}

			// Token: 0x0600159D RID: 5533 RVA: 0x00055414 File Offset: 0x00053614
			public override void Insert(int index, object value)
			{
				this.m_Adaptee.Insert(index, value);
			}

			// Token: 0x0600159E RID: 5534 RVA: 0x00055424 File Offset: 0x00053624
			public override void InsertRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				if (index > this.m_Adaptee.Count)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Index must be >= 0 and <= Count.");
				}
				foreach (object value in c)
				{
					this.m_Adaptee.Insert(index++, value);
				}
			}

			// Token: 0x0600159F RID: 5535 RVA: 0x000554CC File Offset: 0x000536CC
			public override void Remove(object value)
			{
				this.m_Adaptee.Remove(value);
			}

			// Token: 0x060015A0 RID: 5536 RVA: 0x000554DC File Offset: 0x000536DC
			public override void RemoveAt(int index)
			{
				this.m_Adaptee.RemoveAt(index);
			}

			// Token: 0x060015A1 RID: 5537 RVA: 0x000554EC File Offset: 0x000536EC
			public override void RemoveRange(int index, int count)
			{
				ArrayList.CheckRange(index, count, this.m_Adaptee.Count);
				for (int i = 0; i < count; i++)
				{
					this.m_Adaptee.RemoveAt(index);
				}
			}

			// Token: 0x060015A2 RID: 5538 RVA: 0x0005552C File Offset: 0x0005372C
			public override void Reverse()
			{
				this.Reverse(0, this.m_Adaptee.Count);
			}

			// Token: 0x060015A3 RID: 5539 RVA: 0x00055540 File Offset: 0x00053740
			public override void Reverse(int index, int count)
			{
				ArrayList.CheckRange(index, count, this.m_Adaptee.Count);
				for (int i = 0; i < count / 2; i++)
				{
					object value = this.m_Adaptee[i + index];
					this.m_Adaptee[i + index] = this.m_Adaptee[index + count - i + index - 1];
					this.m_Adaptee[index + count - i + index - 1] = value;
				}
			}

			// Token: 0x060015A4 RID: 5540 RVA: 0x000555BC File Offset: 0x000537BC
			public override void SetRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				if (index < 0 || index + c.Count > this.m_Adaptee.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				int num = index;
				foreach (object value in c)
				{
					this.m_Adaptee[num++] = value;
				}
			}

			// Token: 0x060015A5 RID: 5541 RVA: 0x00055668 File Offset: 0x00053868
			public override void CopyTo(Array array)
			{
				this.m_Adaptee.CopyTo(array, 0);
			}

			// Token: 0x060015A6 RID: 5542 RVA: 0x00055678 File Offset: 0x00053878
			public override void CopyTo(Array array, int index)
			{
				this.m_Adaptee.CopyTo(array, index);
			}

			// Token: 0x060015A7 RID: 5543 RVA: 0x00055688 File Offset: 0x00053888
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (index < 0)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Can't be less than zero.");
				}
				if (arrayIndex < 0)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("arrayIndex", arrayIndex, "Can't be less than zero.");
				}
				if (count < 0)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Can't be less than zero.");
				}
				if (index >= this.m_Adaptee.Count)
				{
					throw new ArgumentException("Can't be more or equal to list count.", "index");
				}
				if (array.Rank > 1)
				{
					throw new ArgumentException("Can't copy into multi-dimensional array.");
				}
				if (arrayIndex >= array.Length)
				{
					throw new ArgumentException("arrayIndex can't be greater than array.Length - 1.");
				}
				if (array.Length - arrayIndex + 1 < count)
				{
					throw new ArgumentException("Destination array is too small.");
				}
				if (index > this.m_Adaptee.Count - count)
				{
					throw new ArgumentException("Index and count do not denote a valid range of elements.", "index");
				}
				for (int i = 0; i < count; i++)
				{
					array.SetValue(this.m_Adaptee[index + i], arrayIndex + i);
				}
			}

			// Token: 0x17000330 RID: 816
			// (get) Token: 0x060015A8 RID: 5544 RVA: 0x000557A4 File Offset: 0x000539A4
			public override bool IsSynchronized
			{
				get
				{
					return this.m_Adaptee.IsSynchronized;
				}
			}

			// Token: 0x060015A9 RID: 5545 RVA: 0x000557B4 File Offset: 0x000539B4
			public override IEnumerator GetEnumerator()
			{
				return this.m_Adaptee.GetEnumerator();
			}

			// Token: 0x060015AA RID: 5546 RVA: 0x000557C4 File Offset: 0x000539C4
			public override IEnumerator GetEnumerator(int index, int count)
			{
				ArrayList.CheckRange(index, count, this.m_Adaptee.Count);
				return new ArrayList.ArrayListAdapter.EnumeratorWithRange(this.m_Adaptee.GetEnumerator(), index, count);
			}

			// Token: 0x060015AB RID: 5547 RVA: 0x000557F8 File Offset: 0x000539F8
			public override void AddRange(ICollection c)
			{
				foreach (object value in c)
				{
					this.m_Adaptee.Add(value);
				}
			}

			// Token: 0x060015AC RID: 5548 RVA: 0x00055864 File Offset: 0x00053A64
			public override int BinarySearch(object value)
			{
				return this.BinarySearch(value, null);
			}

			// Token: 0x060015AD RID: 5549 RVA: 0x00055870 File Offset: 0x00053A70
			public override int BinarySearch(object value, IComparer comparer)
			{
				return this.BinarySearch(0, this.m_Adaptee.Count, value, comparer);
			}

			// Token: 0x060015AE RID: 5550 RVA: 0x00055888 File Offset: 0x00053A88
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				ArrayList.CheckRange(index, count, this.m_Adaptee.Count);
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				int i = index;
				int num = index + count - 1;
				while (i <= num)
				{
					int num2 = i + (num - i) / 2;
					int num3 = comparer.Compare(value, this.m_Adaptee[num2]);
					if (num3 < 0)
					{
						num = num2 - 1;
					}
					else
					{
						if (num3 <= 0)
						{
							return num2;
						}
						i = num2 + 1;
					}
				}
				return ~i;
			}

			// Token: 0x060015AF RID: 5551 RVA: 0x0005590C File Offset: 0x00053B0C
			public override object Clone()
			{
				return new ArrayList.ArrayListAdapter(this.m_Adaptee);
			}

			// Token: 0x060015B0 RID: 5552 RVA: 0x0005591C File Offset: 0x00053B1C
			public override ArrayList GetRange(int index, int count)
			{
				ArrayList.CheckRange(index, count, this.m_Adaptee.Count);
				return new ArrayList.RangedArrayList(this, index, count);
			}

			// Token: 0x060015B1 RID: 5553 RVA: 0x00055938 File Offset: 0x00053B38
			public override void TrimToSize()
			{
			}

			// Token: 0x060015B2 RID: 5554 RVA: 0x0005593C File Offset: 0x00053B3C
			public override void Sort()
			{
				this.Sort(Comparer.Default);
			}

			// Token: 0x060015B3 RID: 5555 RVA: 0x0005594C File Offset: 0x00053B4C
			public override void Sort(IComparer comparer)
			{
				this.Sort(0, this.m_Adaptee.Count, comparer);
			}

			// Token: 0x060015B4 RID: 5556 RVA: 0x00055964 File Offset: 0x00053B64
			public override void Sort(int index, int count, IComparer comparer)
			{
				ArrayList.CheckRange(index, count, this.m_Adaptee.Count);
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				ArrayList.ArrayListAdapter.QuickSort(this.m_Adaptee, index, index + count - 1, comparer);
			}

			// Token: 0x060015B5 RID: 5557 RVA: 0x000559A4 File Offset: 0x00053BA4
			private static void Swap(IList list, int x, int y)
			{
				object value = list[x];
				list[x] = list[y];
				list[y] = value;
			}

			// Token: 0x060015B6 RID: 5558 RVA: 0x000559D0 File Offset: 0x00053BD0
			internal static void QuickSort(IList list, int left, int right, IComparer comparer)
			{
				if (left >= right)
				{
					return;
				}
				int num = left + (right - left) / 2;
				if (comparer.Compare(list[num], list[left]) < 0)
				{
					ArrayList.ArrayListAdapter.Swap(list, num, left);
				}
				if (comparer.Compare(list[right], list[left]) < 0)
				{
					ArrayList.ArrayListAdapter.Swap(list, right, left);
				}
				if (comparer.Compare(list[right], list[num]) < 0)
				{
					ArrayList.ArrayListAdapter.Swap(list, right, num);
				}
				if (right - left + 1 <= 3)
				{
					return;
				}
				ArrayList.ArrayListAdapter.Swap(list, right - 1, num);
				object y = list[right - 1];
				int num2 = left;
				int num3 = right - 1;
				for (;;)
				{
					while (comparer.Compare(list[++num2], y) < 0)
					{
					}
					while (comparer.Compare(list[--num3], y) > 0)
					{
					}
					if (num2 >= num3)
					{
						break;
					}
					ArrayList.ArrayListAdapter.Swap(list, num2, num3);
				}
				ArrayList.ArrayListAdapter.Swap(list, right - 1, num2);
				ArrayList.ArrayListAdapter.QuickSort(list, left, num2 - 1, comparer);
				ArrayList.ArrayListAdapter.QuickSort(list, num2 + 1, right, comparer);
			}

			// Token: 0x060015B7 RID: 5559 RVA: 0x00055AF8 File Offset: 0x00053CF8
			public override object[] ToArray()
			{
				object[] array = new object[this.m_Adaptee.Count];
				this.m_Adaptee.CopyTo(array, 0);
				return array;
			}

			// Token: 0x060015B8 RID: 5560 RVA: 0x00055B24 File Offset: 0x00053D24
			public override Array ToArray(Type elementType)
			{
				Array array = Array.CreateInstance(elementType, this.m_Adaptee.Count);
				this.m_Adaptee.CopyTo(array, 0);
				return array;
			}

			// Token: 0x04000859 RID: 2137
			private IList m_Adaptee;

			// Token: 0x020001A7 RID: 423
			private sealed class EnumeratorWithRange : IEnumerator, ICloneable
			{
				// Token: 0x060015B9 RID: 5561 RVA: 0x00055B54 File Offset: 0x00053D54
				public EnumeratorWithRange(IEnumerator enumerator, int index, int count)
				{
					this.m_Count = 0;
					this.m_StartIndex = index;
					this.m_MaxCount = count;
					this.m_Enumerator = enumerator;
					this.Reset();
				}

				// Token: 0x060015BA RID: 5562 RVA: 0x00055B8C File Offset: 0x00053D8C
				public object Clone()
				{
					return base.MemberwiseClone();
				}

				// Token: 0x17000331 RID: 817
				// (get) Token: 0x060015BB RID: 5563 RVA: 0x00055B94 File Offset: 0x00053D94
				public object Current
				{
					get
					{
						return this.m_Enumerator.Current;
					}
				}

				// Token: 0x060015BC RID: 5564 RVA: 0x00055BA4 File Offset: 0x00053DA4
				public bool MoveNext()
				{
					if (this.m_Count >= this.m_MaxCount)
					{
						return false;
					}
					this.m_Count++;
					return this.m_Enumerator.MoveNext();
				}

				// Token: 0x060015BD RID: 5565 RVA: 0x00055BE0 File Offset: 0x00053DE0
				public void Reset()
				{
					this.m_Count = 0;
					this.m_Enumerator.Reset();
					for (int i = 0; i < this.m_StartIndex; i++)
					{
						this.m_Enumerator.MoveNext();
					}
				}

				// Token: 0x0400085A RID: 2138
				private int m_StartIndex;

				// Token: 0x0400085B RID: 2139
				private int m_Count;

				// Token: 0x0400085C RID: 2140
				private int m_MaxCount;

				// Token: 0x0400085D RID: 2141
				private IEnumerator m_Enumerator;
			}
		}

		// Token: 0x020001A8 RID: 424
		[Serializable]
		private class ArrayListWrapper : ArrayList
		{
			// Token: 0x060015BE RID: 5566 RVA: 0x00055C24 File Offset: 0x00053E24
			public ArrayListWrapper(ArrayList innerArrayList)
			{
				this.m_InnerArrayList = innerArrayList;
			}

			// Token: 0x17000332 RID: 818
			public override object this[int index]
			{
				get
				{
					return this.m_InnerArrayList[index];
				}
				set
				{
					this.m_InnerArrayList[index] = value;
				}
			}

			// Token: 0x17000333 RID: 819
			// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00055C54 File Offset: 0x00053E54
			public override int Count
			{
				get
				{
					return this.m_InnerArrayList.Count;
				}
			}

			// Token: 0x17000334 RID: 820
			// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00055C64 File Offset: 0x00053E64
			// (set) Token: 0x060015C3 RID: 5571 RVA: 0x00055C74 File Offset: 0x00053E74
			public override int Capacity
			{
				get
				{
					return this.m_InnerArrayList.Capacity;
				}
				set
				{
					this.m_InnerArrayList.Capacity = value;
				}
			}

			// Token: 0x17000335 RID: 821
			// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00055C84 File Offset: 0x00053E84
			public override bool IsFixedSize
			{
				get
				{
					return this.m_InnerArrayList.IsFixedSize;
				}
			}

			// Token: 0x17000336 RID: 822
			// (get) Token: 0x060015C5 RID: 5573 RVA: 0x00055C94 File Offset: 0x00053E94
			public override bool IsReadOnly
			{
				get
				{
					return this.m_InnerArrayList.IsReadOnly;
				}
			}

			// Token: 0x17000337 RID: 823
			// (get) Token: 0x060015C6 RID: 5574 RVA: 0x00055CA4 File Offset: 0x00053EA4
			public override bool IsSynchronized
			{
				get
				{
					return this.m_InnerArrayList.IsSynchronized;
				}
			}

			// Token: 0x17000338 RID: 824
			// (get) Token: 0x060015C7 RID: 5575 RVA: 0x00055CB4 File Offset: 0x00053EB4
			public override object SyncRoot
			{
				get
				{
					return this.m_InnerArrayList.SyncRoot;
				}
			}

			// Token: 0x060015C8 RID: 5576 RVA: 0x00055CC4 File Offset: 0x00053EC4
			public override int Add(object value)
			{
				return this.m_InnerArrayList.Add(value);
			}

			// Token: 0x060015C9 RID: 5577 RVA: 0x00055CD4 File Offset: 0x00053ED4
			public override void Clear()
			{
				this.m_InnerArrayList.Clear();
			}

			// Token: 0x060015CA RID: 5578 RVA: 0x00055CE4 File Offset: 0x00053EE4
			public override bool Contains(object value)
			{
				return this.m_InnerArrayList.Contains(value);
			}

			// Token: 0x060015CB RID: 5579 RVA: 0x00055CF4 File Offset: 0x00053EF4
			public override int IndexOf(object value)
			{
				return this.m_InnerArrayList.IndexOf(value);
			}

			// Token: 0x060015CC RID: 5580 RVA: 0x00055D04 File Offset: 0x00053F04
			public override int IndexOf(object value, int startIndex)
			{
				return this.m_InnerArrayList.IndexOf(value, startIndex);
			}

			// Token: 0x060015CD RID: 5581 RVA: 0x00055D14 File Offset: 0x00053F14
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this.m_InnerArrayList.IndexOf(value, startIndex, count);
			}

			// Token: 0x060015CE RID: 5582 RVA: 0x00055D24 File Offset: 0x00053F24
			public override int LastIndexOf(object value)
			{
				return this.m_InnerArrayList.LastIndexOf(value);
			}

			// Token: 0x060015CF RID: 5583 RVA: 0x00055D34 File Offset: 0x00053F34
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.m_InnerArrayList.LastIndexOf(value, startIndex);
			}

			// Token: 0x060015D0 RID: 5584 RVA: 0x00055D44 File Offset: 0x00053F44
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this.m_InnerArrayList.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x060015D1 RID: 5585 RVA: 0x00055D54 File Offset: 0x00053F54
			public override void Insert(int index, object value)
			{
				this.m_InnerArrayList.Insert(index, value);
			}

			// Token: 0x060015D2 RID: 5586 RVA: 0x00055D64 File Offset: 0x00053F64
			public override void InsertRange(int index, ICollection c)
			{
				this.m_InnerArrayList.InsertRange(index, c);
			}

			// Token: 0x060015D3 RID: 5587 RVA: 0x00055D74 File Offset: 0x00053F74
			public override void Remove(object value)
			{
				this.m_InnerArrayList.Remove(value);
			}

			// Token: 0x060015D4 RID: 5588 RVA: 0x00055D84 File Offset: 0x00053F84
			public override void RemoveAt(int index)
			{
				this.m_InnerArrayList.RemoveAt(index);
			}

			// Token: 0x060015D5 RID: 5589 RVA: 0x00055D94 File Offset: 0x00053F94
			public override void RemoveRange(int index, int count)
			{
				this.m_InnerArrayList.RemoveRange(index, count);
			}

			// Token: 0x060015D6 RID: 5590 RVA: 0x00055DA4 File Offset: 0x00053FA4
			public override void Reverse()
			{
				this.m_InnerArrayList.Reverse();
			}

			// Token: 0x060015D7 RID: 5591 RVA: 0x00055DB4 File Offset: 0x00053FB4
			public override void Reverse(int index, int count)
			{
				this.m_InnerArrayList.Reverse(index, count);
			}

			// Token: 0x060015D8 RID: 5592 RVA: 0x00055DC4 File Offset: 0x00053FC4
			public override void SetRange(int index, ICollection c)
			{
				this.m_InnerArrayList.SetRange(index, c);
			}

			// Token: 0x060015D9 RID: 5593 RVA: 0x00055DD4 File Offset: 0x00053FD4
			public override void CopyTo(Array array)
			{
				this.m_InnerArrayList.CopyTo(array);
			}

			// Token: 0x060015DA RID: 5594 RVA: 0x00055DE4 File Offset: 0x00053FE4
			public override void CopyTo(Array array, int index)
			{
				this.m_InnerArrayList.CopyTo(array, index);
			}

			// Token: 0x060015DB RID: 5595 RVA: 0x00055DF4 File Offset: 0x00053FF4
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this.m_InnerArrayList.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x060015DC RID: 5596 RVA: 0x00055E08 File Offset: 0x00054008
			public override IEnumerator GetEnumerator()
			{
				return this.m_InnerArrayList.GetEnumerator();
			}

			// Token: 0x060015DD RID: 5597 RVA: 0x00055E18 File Offset: 0x00054018
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this.m_InnerArrayList.GetEnumerator(index, count);
			}

			// Token: 0x060015DE RID: 5598 RVA: 0x00055E28 File Offset: 0x00054028
			public override void AddRange(ICollection c)
			{
				this.m_InnerArrayList.AddRange(c);
			}

			// Token: 0x060015DF RID: 5599 RVA: 0x00055E38 File Offset: 0x00054038
			public override int BinarySearch(object value)
			{
				return this.m_InnerArrayList.BinarySearch(value);
			}

			// Token: 0x060015E0 RID: 5600 RVA: 0x00055E48 File Offset: 0x00054048
			public override int BinarySearch(object value, IComparer comparer)
			{
				return this.m_InnerArrayList.BinarySearch(value, comparer);
			}

			// Token: 0x060015E1 RID: 5601 RVA: 0x00055E58 File Offset: 0x00054058
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this.m_InnerArrayList.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x060015E2 RID: 5602 RVA: 0x00055E6C File Offset: 0x0005406C
			public override object Clone()
			{
				return this.m_InnerArrayList.Clone();
			}

			// Token: 0x060015E3 RID: 5603 RVA: 0x00055E7C File Offset: 0x0005407C
			public override ArrayList GetRange(int index, int count)
			{
				return this.m_InnerArrayList.GetRange(index, count);
			}

			// Token: 0x060015E4 RID: 5604 RVA: 0x00055E8C File Offset: 0x0005408C
			public override void TrimToSize()
			{
				this.m_InnerArrayList.TrimToSize();
			}

			// Token: 0x060015E5 RID: 5605 RVA: 0x00055E9C File Offset: 0x0005409C
			public override void Sort()
			{
				this.m_InnerArrayList.Sort();
			}

			// Token: 0x060015E6 RID: 5606 RVA: 0x00055EAC File Offset: 0x000540AC
			public override void Sort(IComparer comparer)
			{
				this.m_InnerArrayList.Sort(comparer);
			}

			// Token: 0x060015E7 RID: 5607 RVA: 0x00055EBC File Offset: 0x000540BC
			public override void Sort(int index, int count, IComparer comparer)
			{
				this.m_InnerArrayList.Sort(index, count, comparer);
			}

			// Token: 0x060015E8 RID: 5608 RVA: 0x00055ECC File Offset: 0x000540CC
			public override object[] ToArray()
			{
				return this.m_InnerArrayList.ToArray();
			}

			// Token: 0x060015E9 RID: 5609 RVA: 0x00055EDC File Offset: 0x000540DC
			public override Array ToArray(Type elementType)
			{
				return this.m_InnerArrayList.ToArray(elementType);
			}

			// Token: 0x0400085E RID: 2142
			protected ArrayList m_InnerArrayList;
		}

		// Token: 0x020001A9 RID: 425
		[Serializable]
		private sealed class SynchronizedArrayListWrapper : ArrayList.ArrayListWrapper
		{
			// Token: 0x060015EA RID: 5610 RVA: 0x00055EEC File Offset: 0x000540EC
			internal SynchronizedArrayListWrapper(ArrayList innerArrayList) : base(innerArrayList)
			{
				this.m_SyncRoot = innerArrayList.SyncRoot;
			}

			// Token: 0x17000339 RID: 825
			public override object this[int index]
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					object result;
					lock (syncRoot)
					{
						result = this.m_InnerArrayList[index];
					}
					return result;
				}
				set
				{
					object syncRoot = this.m_SyncRoot;
					lock (syncRoot)
					{
						this.m_InnerArrayList[index] = value;
					}
				}
			}

			// Token: 0x1700033A RID: 826
			// (get) Token: 0x060015ED RID: 5613 RVA: 0x00055FAC File Offset: 0x000541AC
			public override int Count
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					int count;
					lock (syncRoot)
					{
						count = this.m_InnerArrayList.Count;
					}
					return count;
				}
			}

			// Token: 0x1700033B RID: 827
			// (get) Token: 0x060015EE RID: 5614 RVA: 0x00056000 File Offset: 0x00054200
			// (set) Token: 0x060015EF RID: 5615 RVA: 0x00056054 File Offset: 0x00054254
			public override int Capacity
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					int capacity;
					lock (syncRoot)
					{
						capacity = this.m_InnerArrayList.Capacity;
					}
					return capacity;
				}
				set
				{
					object syncRoot = this.m_SyncRoot;
					lock (syncRoot)
					{
						this.m_InnerArrayList.Capacity = value;
					}
				}
			}

			// Token: 0x1700033C RID: 828
			// (get) Token: 0x060015F0 RID: 5616 RVA: 0x000560A4 File Offset: 0x000542A4
			public override bool IsFixedSize
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					bool isFixedSize;
					lock (syncRoot)
					{
						isFixedSize = this.m_InnerArrayList.IsFixedSize;
					}
					return isFixedSize;
				}
			}

			// Token: 0x1700033D RID: 829
			// (get) Token: 0x060015F1 RID: 5617 RVA: 0x000560F8 File Offset: 0x000542F8
			public override bool IsReadOnly
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					bool isReadOnly;
					lock (syncRoot)
					{
						isReadOnly = this.m_InnerArrayList.IsReadOnly;
					}
					return isReadOnly;
				}
			}

			// Token: 0x1700033E RID: 830
			// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0005614C File Offset: 0x0005434C
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700033F RID: 831
			// (get) Token: 0x060015F3 RID: 5619 RVA: 0x00056150 File Offset: 0x00054350
			public override object SyncRoot
			{
				get
				{
					return this.m_SyncRoot;
				}
			}

			// Token: 0x060015F4 RID: 5620 RVA: 0x00056158 File Offset: 0x00054358
			public override int Add(object value)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.Add(value);
				}
				return result;
			}

			// Token: 0x060015F5 RID: 5621 RVA: 0x000561B0 File Offset: 0x000543B0
			public override void Clear()
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.Clear();
				}
			}

			// Token: 0x060015F6 RID: 5622 RVA: 0x00056200 File Offset: 0x00054400
			public override bool Contains(object value)
			{
				object syncRoot = this.m_SyncRoot;
				bool result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.Contains(value);
				}
				return result;
			}

			// Token: 0x060015F7 RID: 5623 RVA: 0x00056258 File Offset: 0x00054458
			public override int IndexOf(object value)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.IndexOf(value);
				}
				return result;
			}

			// Token: 0x060015F8 RID: 5624 RVA: 0x000562B0 File Offset: 0x000544B0
			public override int IndexOf(object value, int startIndex)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.IndexOf(value, startIndex);
				}
				return result;
			}

			// Token: 0x060015F9 RID: 5625 RVA: 0x00056308 File Offset: 0x00054508
			public override int IndexOf(object value, int startIndex, int count)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.IndexOf(value, startIndex, count);
				}
				return result;
			}

			// Token: 0x060015FA RID: 5626 RVA: 0x00056360 File Offset: 0x00054560
			public override int LastIndexOf(object value)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.LastIndexOf(value);
				}
				return result;
			}

			// Token: 0x060015FB RID: 5627 RVA: 0x000563B8 File Offset: 0x000545B8
			public override int LastIndexOf(object value, int startIndex)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.LastIndexOf(value, startIndex);
				}
				return result;
			}

			// Token: 0x060015FC RID: 5628 RVA: 0x00056410 File Offset: 0x00054610
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.LastIndexOf(value, startIndex, count);
				}
				return result;
			}

			// Token: 0x060015FD RID: 5629 RVA: 0x00056468 File Offset: 0x00054668
			public override void Insert(int index, object value)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.Insert(index, value);
				}
			}

			// Token: 0x060015FE RID: 5630 RVA: 0x000564B8 File Offset: 0x000546B8
			public override void InsertRange(int index, ICollection c)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.InsertRange(index, c);
				}
			}

			// Token: 0x060015FF RID: 5631 RVA: 0x00056508 File Offset: 0x00054708
			public override void Remove(object value)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.Remove(value);
				}
			}

			// Token: 0x06001600 RID: 5632 RVA: 0x00056558 File Offset: 0x00054758
			public override void RemoveAt(int index)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.RemoveAt(index);
				}
			}

			// Token: 0x06001601 RID: 5633 RVA: 0x000565A8 File Offset: 0x000547A8
			public override void RemoveRange(int index, int count)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.RemoveRange(index, count);
				}
			}

			// Token: 0x06001602 RID: 5634 RVA: 0x000565F8 File Offset: 0x000547F8
			public override void Reverse()
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.Reverse();
				}
			}

			// Token: 0x06001603 RID: 5635 RVA: 0x00056648 File Offset: 0x00054848
			public override void Reverse(int index, int count)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.Reverse(index, count);
				}
			}

			// Token: 0x06001604 RID: 5636 RVA: 0x00056698 File Offset: 0x00054898
			public override void CopyTo(Array array)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.CopyTo(array);
				}
			}

			// Token: 0x06001605 RID: 5637 RVA: 0x000566E8 File Offset: 0x000548E8
			public override void CopyTo(Array array, int index)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.CopyTo(array, index);
				}
			}

			// Token: 0x06001606 RID: 5638 RVA: 0x00056738 File Offset: 0x00054938
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.CopyTo(index, array, arrayIndex, count);
				}
			}

			// Token: 0x06001607 RID: 5639 RVA: 0x0005678C File Offset: 0x0005498C
			public override IEnumerator GetEnumerator()
			{
				object syncRoot = this.m_SyncRoot;
				IEnumerator enumerator;
				lock (syncRoot)
				{
					enumerator = this.m_InnerArrayList.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06001608 RID: 5640 RVA: 0x000567E0 File Offset: 0x000549E0
			public override IEnumerator GetEnumerator(int index, int count)
			{
				object syncRoot = this.m_SyncRoot;
				IEnumerator enumerator;
				lock (syncRoot)
				{
					enumerator = this.m_InnerArrayList.GetEnumerator(index, count);
				}
				return enumerator;
			}

			// Token: 0x06001609 RID: 5641 RVA: 0x00056838 File Offset: 0x00054A38
			public override void AddRange(ICollection c)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.AddRange(c);
				}
			}

			// Token: 0x0600160A RID: 5642 RVA: 0x00056888 File Offset: 0x00054A88
			public override int BinarySearch(object value)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.BinarySearch(value);
				}
				return result;
			}

			// Token: 0x0600160B RID: 5643 RVA: 0x000568E0 File Offset: 0x00054AE0
			public override int BinarySearch(object value, IComparer comparer)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.BinarySearch(value, comparer);
				}
				return result;
			}

			// Token: 0x0600160C RID: 5644 RVA: 0x00056938 File Offset: 0x00054B38
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.BinarySearch(index, count, value, comparer);
				}
				return result;
			}

			// Token: 0x0600160D RID: 5645 RVA: 0x00056994 File Offset: 0x00054B94
			public override object Clone()
			{
				object syncRoot = this.m_SyncRoot;
				object result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.Clone();
				}
				return result;
			}

			// Token: 0x0600160E RID: 5646 RVA: 0x000569E8 File Offset: 0x00054BE8
			public override ArrayList GetRange(int index, int count)
			{
				object syncRoot = this.m_SyncRoot;
				ArrayList range;
				lock (syncRoot)
				{
					range = this.m_InnerArrayList.GetRange(index, count);
				}
				return range;
			}

			// Token: 0x0600160F RID: 5647 RVA: 0x00056A40 File Offset: 0x00054C40
			public override void TrimToSize()
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.TrimToSize();
				}
			}

			// Token: 0x06001610 RID: 5648 RVA: 0x00056A90 File Offset: 0x00054C90
			public override void Sort()
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.Sort();
				}
			}

			// Token: 0x06001611 RID: 5649 RVA: 0x00056AE0 File Offset: 0x00054CE0
			public override void Sort(IComparer comparer)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.Sort(comparer);
				}
			}

			// Token: 0x06001612 RID: 5650 RVA: 0x00056B30 File Offset: 0x00054D30
			public override void Sort(int index, int count, IComparer comparer)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerArrayList.Sort(index, count, comparer);
				}
			}

			// Token: 0x06001613 RID: 5651 RVA: 0x00056B80 File Offset: 0x00054D80
			public override object[] ToArray()
			{
				object syncRoot = this.m_SyncRoot;
				object[] result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.ToArray();
				}
				return result;
			}

			// Token: 0x06001614 RID: 5652 RVA: 0x00056BD4 File Offset: 0x00054DD4
			public override Array ToArray(Type elementType)
			{
				object syncRoot = this.m_SyncRoot;
				Array result;
				lock (syncRoot)
				{
					result = this.m_InnerArrayList.ToArray(elementType);
				}
				return result;
			}

			// Token: 0x0400085F RID: 2143
			private object m_SyncRoot;
		}

		// Token: 0x020001AA RID: 426
		[Serializable]
		private class FixedSizeArrayListWrapper : ArrayList.ArrayListWrapper
		{
			// Token: 0x06001615 RID: 5653 RVA: 0x00056C2C File Offset: 0x00054E2C
			public FixedSizeArrayListWrapper(ArrayList innerList) : base(innerList)
			{
			}

			// Token: 0x17000340 RID: 832
			// (get) Token: 0x06001616 RID: 5654 RVA: 0x00056C38 File Offset: 0x00054E38
			protected virtual string ErrorMessage
			{
				get
				{
					return "Can't add or remove from a fixed-size list.";
				}
			}

			// Token: 0x17000341 RID: 833
			// (get) Token: 0x06001617 RID: 5655 RVA: 0x00056C40 File Offset: 0x00054E40
			// (set) Token: 0x06001618 RID: 5656 RVA: 0x00056C48 File Offset: 0x00054E48
			public override int Capacity
			{
				get
				{
					return base.Capacity;
				}
				set
				{
					throw new NotSupportedException(this.ErrorMessage);
				}
			}

			// Token: 0x17000342 RID: 834
			// (get) Token: 0x06001619 RID: 5657 RVA: 0x00056C58 File Offset: 0x00054E58
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600161A RID: 5658 RVA: 0x00056C5C File Offset: 0x00054E5C
			public override int Add(object value)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x0600161B RID: 5659 RVA: 0x00056C6C File Offset: 0x00054E6C
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x0600161C RID: 5660 RVA: 0x00056C7C File Offset: 0x00054E7C
			public override void Clear()
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x0600161D RID: 5661 RVA: 0x00056C8C File Offset: 0x00054E8C
			public override void Insert(int index, object value)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x0600161E RID: 5662 RVA: 0x00056C9C File Offset: 0x00054E9C
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x0600161F RID: 5663 RVA: 0x00056CAC File Offset: 0x00054EAC
			public override void Remove(object value)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x06001620 RID: 5664 RVA: 0x00056CBC File Offset: 0x00054EBC
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x06001621 RID: 5665 RVA: 0x00056CCC File Offset: 0x00054ECC
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x06001622 RID: 5666 RVA: 0x00056CDC File Offset: 0x00054EDC
			public override void TrimToSize()
			{
				throw new NotSupportedException(this.ErrorMessage);
			}
		}

		// Token: 0x020001AB RID: 427
		[Serializable]
		private sealed class ReadOnlyArrayListWrapper : ArrayList.FixedSizeArrayListWrapper
		{
			// Token: 0x06001623 RID: 5667 RVA: 0x00056CEC File Offset: 0x00054EEC
			public ReadOnlyArrayListWrapper(ArrayList innerArrayList) : base(innerArrayList)
			{
			}

			// Token: 0x17000343 RID: 835
			// (get) Token: 0x06001624 RID: 5668 RVA: 0x00056CF8 File Offset: 0x00054EF8
			protected override string ErrorMessage
			{
				get
				{
					return "Can't modify a readonly list.";
				}
			}

			// Token: 0x17000344 RID: 836
			// (get) Token: 0x06001625 RID: 5669 RVA: 0x00056D00 File Offset: 0x00054F00
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000345 RID: 837
			public override object this[int index]
			{
				get
				{
					return this.m_InnerArrayList[index];
				}
				set
				{
					throw new NotSupportedException(this.ErrorMessage);
				}
			}

			// Token: 0x06001628 RID: 5672 RVA: 0x00056D24 File Offset: 0x00054F24
			public override void Reverse()
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x06001629 RID: 5673 RVA: 0x00056D34 File Offset: 0x00054F34
			public override void Reverse(int index, int count)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x0600162A RID: 5674 RVA: 0x00056D44 File Offset: 0x00054F44
			public override void SetRange(int index, ICollection c)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x0600162B RID: 5675 RVA: 0x00056D54 File Offset: 0x00054F54
			public override void Sort()
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x0600162C RID: 5676 RVA: 0x00056D64 File Offset: 0x00054F64
			public override void Sort(IComparer comparer)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x0600162D RID: 5677 RVA: 0x00056D74 File Offset: 0x00054F74
			public override void Sort(int index, int count, IComparer comparer)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}
		}

		// Token: 0x020001AC RID: 428
		[Serializable]
		private sealed class RangedArrayList : ArrayList.ArrayListWrapper
		{
			// Token: 0x0600162E RID: 5678 RVA: 0x00056D84 File Offset: 0x00054F84
			public RangedArrayList(ArrayList innerList, int index, int count) : base(innerList)
			{
				this.m_InnerIndex = index;
				this.m_InnerCount = count;
				this.m_InnerStateChanges = innerList._version;
			}

			// Token: 0x17000346 RID: 838
			// (get) Token: 0x0600162F RID: 5679 RVA: 0x00056DA8 File Offset: 0x00054FA8
			public override bool IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000347 RID: 839
			public override object this[int index]
			{
				get
				{
					if (index < 0 || index > this.m_InnerCount)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return this.m_InnerArrayList[this.m_InnerIndex + index];
				}
				set
				{
					if (index < 0 || index > this.m_InnerCount)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					this.m_InnerArrayList[this.m_InnerIndex + index] = value;
				}
			}

			// Token: 0x17000348 RID: 840
			// (get) Token: 0x06001632 RID: 5682 RVA: 0x00056E20 File Offset: 0x00055020
			public override int Count
			{
				get
				{
					this.VerifyStateChanges();
					return this.m_InnerCount;
				}
			}

			// Token: 0x17000349 RID: 841
			// (get) Token: 0x06001633 RID: 5683 RVA: 0x00056E30 File Offset: 0x00055030
			// (set) Token: 0x06001634 RID: 5684 RVA: 0x00056E40 File Offset: 0x00055040
			public override int Capacity
			{
				get
				{
					return this.m_InnerArrayList.Capacity;
				}
				set
				{
					if (value < this.m_InnerCount)
					{
						throw new ArgumentOutOfRangeException();
					}
				}
			}

			// Token: 0x06001635 RID: 5685 RVA: 0x00056E54 File Offset: 0x00055054
			private void VerifyStateChanges()
			{
				if (this.m_InnerStateChanges != this.m_InnerArrayList._version)
				{
					throw new InvalidOperationException("ArrayList view is invalid because the underlying ArrayList was modified.");
				}
			}

			// Token: 0x06001636 RID: 5686 RVA: 0x00056E78 File Offset: 0x00055078
			public override int Add(object value)
			{
				this.VerifyStateChanges();
				this.m_InnerArrayList.Insert(this.m_InnerIndex + this.m_InnerCount, value);
				this.m_InnerStateChanges = this.m_InnerArrayList._version;
				return ++this.m_InnerCount;
			}

			// Token: 0x06001637 RID: 5687 RVA: 0x00056EC8 File Offset: 0x000550C8
			public override void Clear()
			{
				this.VerifyStateChanges();
				this.m_InnerArrayList.RemoveRange(this.m_InnerIndex, this.m_InnerCount);
				this.m_InnerCount = 0;
				this.m_InnerStateChanges = this.m_InnerArrayList._version;
			}

			// Token: 0x06001638 RID: 5688 RVA: 0x00056F0C File Offset: 0x0005510C
			public override bool Contains(object value)
			{
				return this.m_InnerArrayList.Contains(value, this.m_InnerIndex, this.m_InnerCount);
			}

			// Token: 0x06001639 RID: 5689 RVA: 0x00056F28 File Offset: 0x00055128
			public override int IndexOf(object value)
			{
				return this.IndexOf(value, 0);
			}

			// Token: 0x0600163A RID: 5690 RVA: 0x00056F34 File Offset: 0x00055134
			public override int IndexOf(object value, int startIndex)
			{
				return this.IndexOf(value, startIndex, this.m_InnerCount - startIndex);
			}

			// Token: 0x0600163B RID: 5691 RVA: 0x00056F48 File Offset: 0x00055148
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this.m_InnerCount)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("startIndex", startIndex, "Does not specify valid index.");
				}
				if (count < 0)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("count", count, "Can't be less than 0.");
				}
				if (startIndex > this.m_InnerCount - count)
				{
					throw new ArgumentOutOfRangeException("count", "Start index and count do not specify a valid range.");
				}
				int num = this.m_InnerArrayList.IndexOf(value, this.m_InnerIndex + startIndex, count);
				if (num == -1)
				{
					return -1;
				}
				return num - this.m_InnerIndex;
			}

			// Token: 0x0600163C RID: 5692 RVA: 0x00056FE0 File Offset: 0x000551E0
			public override int LastIndexOf(object value)
			{
				return this.LastIndexOf(value, this.m_InnerCount - 1);
			}

			// Token: 0x0600163D RID: 5693 RVA: 0x00056FF4 File Offset: 0x000551F4
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x0600163E RID: 5694 RVA: 0x00057004 File Offset: 0x00055204
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("startIndex", startIndex, "< 0");
				}
				if (count < 0)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("count", count, "count is negative.");
				}
				int num = this.m_InnerArrayList.LastIndexOf(value, this.m_InnerIndex + startIndex, count);
				if (num == -1)
				{
					return -1;
				}
				return num - this.m_InnerIndex;
			}

			// Token: 0x0600163F RID: 5695 RVA: 0x00057070 File Offset: 0x00055270
			public override void Insert(int index, object value)
			{
				this.VerifyStateChanges();
				if (index < 0 || index > this.m_InnerCount)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Index must be >= 0 and <= Count.");
				}
				this.m_InnerArrayList.Insert(this.m_InnerIndex + index, value);
				this.m_InnerCount++;
				this.m_InnerStateChanges = this.m_InnerArrayList._version;
			}

			// Token: 0x06001640 RID: 5696 RVA: 0x000570E0 File Offset: 0x000552E0
			public override void InsertRange(int index, ICollection c)
			{
				this.VerifyStateChanges();
				if (index < 0 || index > this.m_InnerCount)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Index must be >= 0 and <= Count.");
				}
				this.m_InnerArrayList.InsertRange(this.m_InnerIndex + index, c);
				this.m_InnerCount += c.Count;
				this.m_InnerStateChanges = this.m_InnerArrayList._version;
			}

			// Token: 0x06001641 RID: 5697 RVA: 0x00057154 File Offset: 0x00055354
			public override void Remove(object value)
			{
				this.VerifyStateChanges();
				int num = this.IndexOf(value);
				if (num > -1)
				{
					this.RemoveAt(num);
				}
				this.m_InnerStateChanges = this.m_InnerArrayList._version;
			}

			// Token: 0x06001642 RID: 5698 RVA: 0x00057190 File Offset: 0x00055390
			public override void RemoveAt(int index)
			{
				this.VerifyStateChanges();
				if (index < 0 || index > this.m_InnerCount)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Index must be >= 0 and <= Count.");
				}
				this.m_InnerArrayList.RemoveAt(this.m_InnerIndex + index);
				this.m_InnerCount--;
				this.m_InnerStateChanges = this.m_InnerArrayList._version;
			}

			// Token: 0x06001643 RID: 5699 RVA: 0x00057200 File Offset: 0x00055400
			public override void RemoveRange(int index, int count)
			{
				this.VerifyStateChanges();
				ArrayList.CheckRange(index, count, this.m_InnerCount);
				this.m_InnerArrayList.RemoveRange(this.m_InnerIndex + index, count);
				this.m_InnerCount -= count;
				this.m_InnerStateChanges = this.m_InnerArrayList._version;
			}

			// Token: 0x06001644 RID: 5700 RVA: 0x00057254 File Offset: 0x00055454
			public override void Reverse()
			{
				this.Reverse(0, this.m_InnerCount);
			}

			// Token: 0x06001645 RID: 5701 RVA: 0x00057264 File Offset: 0x00055464
			public override void Reverse(int index, int count)
			{
				this.VerifyStateChanges();
				ArrayList.CheckRange(index, count, this.m_InnerCount);
				this.m_InnerArrayList.Reverse(this.m_InnerIndex + index, count);
				this.m_InnerStateChanges = this.m_InnerArrayList._version;
			}

			// Token: 0x06001646 RID: 5702 RVA: 0x000572AC File Offset: 0x000554AC
			public override void SetRange(int index, ICollection c)
			{
				this.VerifyStateChanges();
				if (index < 0 || index > this.m_InnerCount)
				{
					ArrayList.ThrowNewArgumentOutOfRangeException("index", index, "Index must be >= 0 and <= Count.");
				}
				this.m_InnerArrayList.SetRange(this.m_InnerIndex + index, c);
				this.m_InnerStateChanges = this.m_InnerArrayList._version;
			}

			// Token: 0x06001647 RID: 5703 RVA: 0x0005730C File Offset: 0x0005550C
			public override void CopyTo(Array array)
			{
				this.CopyTo(array, 0);
			}

			// Token: 0x06001648 RID: 5704 RVA: 0x00057318 File Offset: 0x00055518
			public override void CopyTo(Array array, int index)
			{
				this.CopyTo(0, array, index, this.m_InnerCount);
			}

			// Token: 0x06001649 RID: 5705 RVA: 0x0005732C File Offset: 0x0005552C
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				ArrayList.CheckRange(index, count, this.m_InnerCount);
				this.m_InnerArrayList.CopyTo(this.m_InnerIndex + index, array, arrayIndex, count);
			}

			// Token: 0x0600164A RID: 5706 RVA: 0x00057354 File Offset: 0x00055554
			public override IEnumerator GetEnumerator()
			{
				return this.GetEnumerator(0, this.m_InnerCount);
			}

			// Token: 0x0600164B RID: 5707 RVA: 0x00057364 File Offset: 0x00055564
			public override IEnumerator GetEnumerator(int index, int count)
			{
				ArrayList.CheckRange(index, count, this.m_InnerCount);
				return this.m_InnerArrayList.GetEnumerator(this.m_InnerIndex + index, count);
			}

			// Token: 0x0600164C RID: 5708 RVA: 0x00057388 File Offset: 0x00055588
			public override void AddRange(ICollection c)
			{
				this.VerifyStateChanges();
				this.m_InnerArrayList.InsertRange(this.m_InnerCount, c);
				this.m_InnerCount += c.Count;
				this.m_InnerStateChanges = this.m_InnerArrayList._version;
			}

			// Token: 0x0600164D RID: 5709 RVA: 0x000573D4 File Offset: 0x000555D4
			public override int BinarySearch(object value)
			{
				return this.BinarySearch(0, this.m_InnerCount, value, Comparer.Default);
			}

			// Token: 0x0600164E RID: 5710 RVA: 0x000573EC File Offset: 0x000555EC
			public override int BinarySearch(object value, IComparer comparer)
			{
				return this.BinarySearch(0, this.m_InnerCount, value, comparer);
			}

			// Token: 0x0600164F RID: 5711 RVA: 0x00057400 File Offset: 0x00055600
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				ArrayList.CheckRange(index, count, this.m_InnerCount);
				return this.m_InnerArrayList.BinarySearch(this.m_InnerIndex + index, count, value, comparer);
			}

			// Token: 0x06001650 RID: 5712 RVA: 0x00057434 File Offset: 0x00055634
			public override object Clone()
			{
				return new ArrayList.RangedArrayList((ArrayList)this.m_InnerArrayList.Clone(), this.m_InnerIndex, this.m_InnerCount);
			}

			// Token: 0x06001651 RID: 5713 RVA: 0x00057458 File Offset: 0x00055658
			public override ArrayList GetRange(int index, int count)
			{
				ArrayList.CheckRange(index, count, this.m_InnerCount);
				return new ArrayList.RangedArrayList(this, index, count);
			}

			// Token: 0x06001652 RID: 5714 RVA: 0x00057470 File Offset: 0x00055670
			public override void TrimToSize()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001653 RID: 5715 RVA: 0x00057478 File Offset: 0x00055678
			public override void Sort()
			{
				this.Sort(Comparer.Default);
			}

			// Token: 0x06001654 RID: 5716 RVA: 0x00057488 File Offset: 0x00055688
			public override void Sort(IComparer comparer)
			{
				this.Sort(0, this.m_InnerCount, comparer);
			}

			// Token: 0x06001655 RID: 5717 RVA: 0x00057498 File Offset: 0x00055698
			public override void Sort(int index, int count, IComparer comparer)
			{
				this.VerifyStateChanges();
				ArrayList.CheckRange(index, count, this.m_InnerCount);
				this.m_InnerArrayList.Sort(this.m_InnerIndex + index, count, comparer);
				this.m_InnerStateChanges = this.m_InnerArrayList._version;
			}

			// Token: 0x06001656 RID: 5718 RVA: 0x000574E0 File Offset: 0x000556E0
			public override object[] ToArray()
			{
				object[] array = new object[this.m_InnerCount];
				this.m_InnerArrayList.CopyTo(this.m_InnerIndex, array, 0, this.m_InnerCount);
				return array;
			}

			// Token: 0x06001657 RID: 5719 RVA: 0x00057514 File Offset: 0x00055714
			public override Array ToArray(Type elementType)
			{
				Array array = Array.CreateInstance(elementType, this.m_InnerCount);
				this.m_InnerArrayList.CopyTo(this.m_InnerIndex, array, 0, this.m_InnerCount);
				return array;
			}

			// Token: 0x04000860 RID: 2144
			private int m_InnerIndex;

			// Token: 0x04000861 RID: 2145
			private int m_InnerCount;

			// Token: 0x04000862 RID: 2146
			private int m_InnerStateChanges;
		}

		// Token: 0x020001AD RID: 429
		[Serializable]
		private sealed class SynchronizedListWrapper : ArrayList.ListWrapper
		{
			// Token: 0x06001658 RID: 5720 RVA: 0x00057548 File Offset: 0x00055748
			public SynchronizedListWrapper(IList innerList) : base(innerList)
			{
				this.m_SyncRoot = innerList.SyncRoot;
			}

			// Token: 0x1700034A RID: 842
			// (get) Token: 0x06001659 RID: 5721 RVA: 0x00057560 File Offset: 0x00055760
			public override int Count
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					int count;
					lock (syncRoot)
					{
						count = this.m_InnerList.Count;
					}
					return count;
				}
			}

			// Token: 0x1700034B RID: 843
			// (get) Token: 0x0600165A RID: 5722 RVA: 0x000575B4 File Offset: 0x000557B4
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700034C RID: 844
			// (get) Token: 0x0600165B RID: 5723 RVA: 0x000575B8 File Offset: 0x000557B8
			public override object SyncRoot
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					object syncRoot2;
					lock (syncRoot)
					{
						syncRoot2 = this.m_InnerList.SyncRoot;
					}
					return syncRoot2;
				}
			}

			// Token: 0x1700034D RID: 845
			// (get) Token: 0x0600165C RID: 5724 RVA: 0x0005760C File Offset: 0x0005580C
			public override bool IsFixedSize
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					bool isFixedSize;
					lock (syncRoot)
					{
						isFixedSize = this.m_InnerList.IsFixedSize;
					}
					return isFixedSize;
				}
			}

			// Token: 0x1700034E RID: 846
			// (get) Token: 0x0600165D RID: 5725 RVA: 0x00057660 File Offset: 0x00055860
			public override bool IsReadOnly
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					bool isReadOnly;
					lock (syncRoot)
					{
						isReadOnly = this.m_InnerList.IsReadOnly;
					}
					return isReadOnly;
				}
			}

			// Token: 0x1700034F RID: 847
			public override object this[int index]
			{
				get
				{
					object syncRoot = this.m_SyncRoot;
					object result;
					lock (syncRoot)
					{
						result = this.m_InnerList[index];
					}
					return result;
				}
				set
				{
					object syncRoot = this.m_SyncRoot;
					lock (syncRoot)
					{
						this.m_InnerList[index] = value;
					}
				}
			}

			// Token: 0x06001660 RID: 5728 RVA: 0x0005775C File Offset: 0x0005595C
			public override int Add(object value)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerList.Add(value);
				}
				return result;
			}

			// Token: 0x06001661 RID: 5729 RVA: 0x000577B4 File Offset: 0x000559B4
			public override void Clear()
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerList.Clear();
				}
			}

			// Token: 0x06001662 RID: 5730 RVA: 0x00057804 File Offset: 0x00055A04
			public override bool Contains(object value)
			{
				object syncRoot = this.m_SyncRoot;
				bool result;
				lock (syncRoot)
				{
					result = this.m_InnerList.Contains(value);
				}
				return result;
			}

			// Token: 0x06001663 RID: 5731 RVA: 0x0005785C File Offset: 0x00055A5C
			public override int IndexOf(object value)
			{
				object syncRoot = this.m_SyncRoot;
				int result;
				lock (syncRoot)
				{
					result = this.m_InnerList.IndexOf(value);
				}
				return result;
			}

			// Token: 0x06001664 RID: 5732 RVA: 0x000578B4 File Offset: 0x00055AB4
			public override void Insert(int index, object value)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerList.Insert(index, value);
				}
			}

			// Token: 0x06001665 RID: 5733 RVA: 0x00057904 File Offset: 0x00055B04
			public override void Remove(object value)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerList.Remove(value);
				}
			}

			// Token: 0x06001666 RID: 5734 RVA: 0x00057954 File Offset: 0x00055B54
			public override void RemoveAt(int index)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerList.RemoveAt(index);
				}
			}

			// Token: 0x06001667 RID: 5735 RVA: 0x000579A4 File Offset: 0x00055BA4
			public override void CopyTo(Array array, int index)
			{
				object syncRoot = this.m_SyncRoot;
				lock (syncRoot)
				{
					this.m_InnerList.CopyTo(array, index);
				}
			}

			// Token: 0x06001668 RID: 5736 RVA: 0x000579F4 File Offset: 0x00055BF4
			public override IEnumerator GetEnumerator()
			{
				object syncRoot = this.m_SyncRoot;
				IEnumerator enumerator;
				lock (syncRoot)
				{
					enumerator = this.m_InnerList.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x04000863 RID: 2147
			private object m_SyncRoot;
		}

		// Token: 0x020001AE RID: 430
		[Serializable]
		private class FixedSizeListWrapper : ArrayList.ListWrapper
		{
			// Token: 0x06001669 RID: 5737 RVA: 0x00057A48 File Offset: 0x00055C48
			public FixedSizeListWrapper(IList innerList) : base(innerList)
			{
			}

			// Token: 0x17000350 RID: 848
			// (get) Token: 0x0600166A RID: 5738 RVA: 0x00057A54 File Offset: 0x00055C54
			protected virtual string ErrorMessage
			{
				get
				{
					return "List is fixed-size.";
				}
			}

			// Token: 0x17000351 RID: 849
			// (get) Token: 0x0600166B RID: 5739 RVA: 0x00057A5C File Offset: 0x00055C5C
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600166C RID: 5740 RVA: 0x00057A60 File Offset: 0x00055C60
			public override int Add(object value)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x0600166D RID: 5741 RVA: 0x00057A70 File Offset: 0x00055C70
			public override void Clear()
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x0600166E RID: 5742 RVA: 0x00057A80 File Offset: 0x00055C80
			public override void Insert(int index, object value)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x0600166F RID: 5743 RVA: 0x00057A90 File Offset: 0x00055C90
			public override void Remove(object value)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}

			// Token: 0x06001670 RID: 5744 RVA: 0x00057AA0 File Offset: 0x00055CA0
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException(this.ErrorMessage);
			}
		}

		// Token: 0x020001AF RID: 431
		[Serializable]
		private sealed class ReadOnlyListWrapper : ArrayList.FixedSizeListWrapper
		{
			// Token: 0x06001671 RID: 5745 RVA: 0x00057AB0 File Offset: 0x00055CB0
			public ReadOnlyListWrapper(IList innerList) : base(innerList)
			{
			}

			// Token: 0x17000352 RID: 850
			// (get) Token: 0x06001672 RID: 5746 RVA: 0x00057ABC File Offset: 0x00055CBC
			protected override string ErrorMessage
			{
				get
				{
					return "List is read-only.";
				}
			}

			// Token: 0x17000353 RID: 851
			// (get) Token: 0x06001673 RID: 5747 RVA: 0x00057AC4 File Offset: 0x00055CC4
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000354 RID: 852
			public override object this[int index]
			{
				get
				{
					return this.m_InnerList[index];
				}
				set
				{
					throw new NotSupportedException(this.ErrorMessage);
				}
			}
		}

		// Token: 0x020001B0 RID: 432
		[Serializable]
		private class ListWrapper : IEnumerable, ICollection, IList
		{
			// Token: 0x06001676 RID: 5750 RVA: 0x00057AE8 File Offset: 0x00055CE8
			public ListWrapper(IList innerList)
			{
				this.m_InnerList = innerList;
			}

			// Token: 0x17000355 RID: 853
			public virtual object this[int index]
			{
				get
				{
					return this.m_InnerList[index];
				}
				set
				{
					this.m_InnerList[index] = value;
				}
			}

			// Token: 0x17000356 RID: 854
			// (get) Token: 0x06001679 RID: 5753 RVA: 0x00057B18 File Offset: 0x00055D18
			public virtual int Count
			{
				get
				{
					return this.m_InnerList.Count;
				}
			}

			// Token: 0x17000357 RID: 855
			// (get) Token: 0x0600167A RID: 5754 RVA: 0x00057B28 File Offset: 0x00055D28
			public virtual bool IsSynchronized
			{
				get
				{
					return this.m_InnerList.IsSynchronized;
				}
			}

			// Token: 0x17000358 RID: 856
			// (get) Token: 0x0600167B RID: 5755 RVA: 0x00057B38 File Offset: 0x00055D38
			public virtual object SyncRoot
			{
				get
				{
					return this.m_InnerList.SyncRoot;
				}
			}

			// Token: 0x17000359 RID: 857
			// (get) Token: 0x0600167C RID: 5756 RVA: 0x00057B48 File Offset: 0x00055D48
			public virtual bool IsFixedSize
			{
				get
				{
					return this.m_InnerList.IsFixedSize;
				}
			}

			// Token: 0x1700035A RID: 858
			// (get) Token: 0x0600167D RID: 5757 RVA: 0x00057B58 File Offset: 0x00055D58
			public virtual bool IsReadOnly
			{
				get
				{
					return this.m_InnerList.IsReadOnly;
				}
			}

			// Token: 0x0600167E RID: 5758 RVA: 0x00057B68 File Offset: 0x00055D68
			public virtual int Add(object value)
			{
				return this.m_InnerList.Add(value);
			}

			// Token: 0x0600167F RID: 5759 RVA: 0x00057B78 File Offset: 0x00055D78
			public virtual void Clear()
			{
				this.m_InnerList.Clear();
			}

			// Token: 0x06001680 RID: 5760 RVA: 0x00057B88 File Offset: 0x00055D88
			public virtual bool Contains(object value)
			{
				return this.m_InnerList.Contains(value);
			}

			// Token: 0x06001681 RID: 5761 RVA: 0x00057B98 File Offset: 0x00055D98
			public virtual int IndexOf(object value)
			{
				return this.m_InnerList.IndexOf(value);
			}

			// Token: 0x06001682 RID: 5762 RVA: 0x00057BA8 File Offset: 0x00055DA8
			public virtual void Insert(int index, object value)
			{
				this.m_InnerList.Insert(index, value);
			}

			// Token: 0x06001683 RID: 5763 RVA: 0x00057BB8 File Offset: 0x00055DB8
			public virtual void Remove(object value)
			{
				this.m_InnerList.Remove(value);
			}

			// Token: 0x06001684 RID: 5764 RVA: 0x00057BC8 File Offset: 0x00055DC8
			public virtual void RemoveAt(int index)
			{
				this.m_InnerList.RemoveAt(index);
			}

			// Token: 0x06001685 RID: 5765 RVA: 0x00057BD8 File Offset: 0x00055DD8
			public virtual void CopyTo(Array array, int index)
			{
				this.m_InnerList.CopyTo(array, index);
			}

			// Token: 0x06001686 RID: 5766 RVA: 0x00057BE8 File Offset: 0x00055DE8
			public virtual IEnumerator GetEnumerator()
			{
				return this.m_InnerList.GetEnumerator();
			}

			// Token: 0x04000864 RID: 2148
			protected IList m_InnerList;
		}
	}
}
