using System;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	/// <summary>Represents a first-in, first-out collection of objects.</summary>
	/// <typeparam name="T">Specifies the type of elements in the queue.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000099 RID: 153
	[ComVisible(false)]
	[Serializable]
	public class Queue<T> : IEnumerable<T>, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Queue`1" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x06000652 RID: 1618 RVA: 0x00013D50 File Offset: 0x00011F50
		public Queue()
		{
			this._array = new T[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Queue`1" /> class that is empty and has the specified initial capacity.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Queue`1" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06000653 RID: 1619 RVA: 0x00013D64 File Offset: 0x00011F64
		public Queue(int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._array = new T[count];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Queue`1" /> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.</summary>
		/// <param name="collection">The collection whose elements are copied to the new <see cref="T:System.Collections.Generic.Queue`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x06000654 RID: 1620 RVA: 0x00013D98 File Offset: 0x00011F98
		public Queue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			int num = (collection2 == null) ? 0 : collection2.Count;
			this._array = new T[num];
			foreach (T item in collection)
			{
				this.Enqueue(item);
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06000655 RID: 1621 RVA: 0x00013E34 File Offset: 0x00012034
		void ICollection.CopyTo(Array array, int idx)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (idx > array.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (array.Length - idx < this._size)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this._size == 0)
			{
				return;
			}
			try
			{
				int num = this._array.Length;
				int num2 = num - this._head;
				Array.Copy(this._array, this._head, array, idx, Math.Min(this._size, num2));
				if (this._size > num2)
				{
					Array.Copy(this._array, 0, array, idx + num2, this._size - num2);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException();
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Queue`1" />, this property always returns false.</returns>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00013F08 File Offset: 0x00012108
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Queue`1" />, this property always returns the current instance.</returns>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00013F0C File Offset: 0x0001210C
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00013F10 File Offset: 0x00012110
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06000659 RID: 1625 RVA: 0x00013F20 File Offset: 0x00012120
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600065A RID: 1626 RVA: 0x00013F30 File Offset: 0x00012130
		public void Clear()
		{
			Array.Clear(this._array, 0, this._array.Length);
			this._head = (this._tail = (this._size = 0));
			this._version++;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.Queue`1" />; otherwise, false.</returns>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.Queue`1" />. The value can be null for reference types.</param>
		// Token: 0x0600065B RID: 1627 RVA: 0x00013F78 File Offset: 0x00012178
		public bool Contains(T item)
		{
			if (item == null)
			{
				foreach (T t in this)
				{
					if (t == null)
					{
						return true;
					}
				}
			}
			else
			{
				foreach (T t2 in this)
				{
					if (item.Equals(t2))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Generic.Queue`1" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Queue`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Queue`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x0600065C RID: 1628 RVA: 0x00014064 File Offset: 0x00012264
		public void CopyTo(T[] array, int idx)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			((ICollection)this).CopyTo(array, idx);
		}

		/// <summary>Removes and returns the object at the beginning of the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <returns>The object that is removed from the beginning of the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.Queue`1" /> is empty.</exception>
		// Token: 0x0600065D RID: 1629 RVA: 0x0001407C File Offset: 0x0001227C
		public T Dequeue()
		{
			T result = this.Peek();
			this._array[this._head] = default(T);
			if (++this._head == this._array.Length)
			{
				this._head = 0;
			}
			this._size--;
			this._version++;
			return result;
		}

		/// <summary>Returns the object at the beginning of the <see cref="T:System.Collections.Generic.Queue`1" /> without removing it.</summary>
		/// <returns>The object at the beginning of the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.Queue`1" /> is empty.</exception>
		// Token: 0x0600065E RID: 1630 RVA: 0x000140EC File Offset: 0x000122EC
		public T Peek()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException();
			}
			return this._array[this._head];
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.Queue`1" />. The value can be null for reference types.</param>
		// Token: 0x0600065F RID: 1631 RVA: 0x0001411C File Offset: 0x0001231C
		public void Enqueue(T item)
		{
			if (this._size == this._array.Length || this._tail == this._array.Length)
			{
				this.SetCapacity(Math.Max(Math.Max(this._size, this._tail) * 2, 4));
			}
			this._array[this._tail] = item;
			if (++this._tail == this._array.Length)
			{
				this._tail = 0;
			}
			this._size++;
			this._version++;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Generic.Queue`1" /> elements to a new array.</summary>
		/// <returns>A new array containing elements copied from the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		// Token: 0x06000660 RID: 1632 RVA: 0x000141C4 File Offset: 0x000123C4
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			this.CopyTo(array, 0);
			return array;
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Generic.Queue`1" />, if that number is less than 90 percent of current capacity.</summary>
		// Token: 0x06000661 RID: 1633 RVA: 0x000141E8 File Offset: 0x000123E8
		public void TrimExcess()
		{
			if ((double)this._size < (double)this._array.Length * 0.9)
			{
				this.SetCapacity(this._size);
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00014218 File Offset: 0x00012418
		private void SetCapacity(int new_size)
		{
			if (new_size == this._array.Length)
			{
				return;
			}
			if (new_size < this._size)
			{
				throw new InvalidOperationException("shouldnt happen");
			}
			T[] array = new T[new_size];
			if (this._size > 0)
			{
				this.CopyTo(array, 0);
			}
			this._array = array;
			this._tail = this._size;
			this._head = 0;
			this._version++;
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00014290 File Offset: 0x00012490
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.Queue`1.Enumerator" /> for the <see cref="T:System.Collections.Generic.Queue`1" />.</returns>
		// Token: 0x06000664 RID: 1636 RVA: 0x00014298 File Offset: 0x00012498
		public Queue<T>.Enumerator GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		// Token: 0x040001C0 RID: 448
		private T[] _array;

		// Token: 0x040001C1 RID: 449
		private int _head;

		// Token: 0x040001C2 RID: 450
		private int _tail;

		// Token: 0x040001C3 RID: 451
		private int _size;

		// Token: 0x040001C4 RID: 452
		private int _version;

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
		// Token: 0x0200009A RID: 154
		[Serializable]
		public struct Enumerator : IEnumerator, IDisposable, IEnumerator<T>
		{
			// Token: 0x06000665 RID: 1637 RVA: 0x000142A0 File Offset: 0x000124A0
			internal Enumerator(Queue<T> q)
			{
				this.q = q;
				this.idx = -2;
				this.ver = q._version;
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06000666 RID: 1638 RVA: 0x000142C0 File Offset: 0x000124C0
			void IEnumerator.Reset()
			{
				if (this.ver != this.q._version)
				{
					throw new InvalidOperationException();
				}
				this.idx = -2;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x1700013B RID: 315
			// (get) Token: 0x06000667 RID: 1639 RVA: 0x000142F4 File Offset: 0x000124F4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Queue`1.Enumerator" />.</summary>
			// Token: 0x06000668 RID: 1640 RVA: 0x00014304 File Offset: 0x00012504
			public void Dispose()
			{
				this.idx = -2;
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Queue`1" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06000669 RID: 1641 RVA: 0x00014310 File Offset: 0x00012510
			public bool MoveNext()
			{
				if (this.ver != this.q._version)
				{
					throw new InvalidOperationException();
				}
				if (this.idx == -2)
				{
					this.idx = this.q._size;
				}
				return this.idx != -1 && --this.idx != -1;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.Queue`1" /> at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x1700013C RID: 316
			// (get) Token: 0x0600066A RID: 1642 RVA: 0x00014380 File Offset: 0x00012580
			public T Current
			{
				get
				{
					if (this.idx < 0)
					{
						throw new InvalidOperationException();
					}
					return this.q._array[(this.q._size - 1 - this.idx + this.q._head) % this.q._array.Length];
				}
			}

			// Token: 0x040001C5 RID: 453
			private const int NOT_STARTED = -2;

			// Token: 0x040001C6 RID: 454
			private const int FINISHED = -1;

			// Token: 0x040001C7 RID: 455
			private Queue<T> q;

			// Token: 0x040001C8 RID: 456
			private int idx;

			// Token: 0x040001C9 RID: 457
			private int ver;
		}
	}
}
