using System;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	/// <summary>Represents a variable size last-in-first-out (LIFO) collection of instances of the same arbitrary type.</summary>
	/// <typeparam name="T">Specifies the type of elements in the stack.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000AA RID: 170
	[ComVisible(false)]
	[Serializable]
	public class Stack<T> : ICollection, IEnumerable<T>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Stack`1" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x06000744 RID: 1860 RVA: 0x000166EC File Offset: 0x000148EC
		public Stack()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Stack`1" /> class that is empty and has the specified initial capacity or the default initial capacity, whichever is greater.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Stack`1" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06000745 RID: 1861 RVA: 0x000166F4 File Offset: 0x000148F4
		public Stack(int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._array = new T[count];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Stack`1" /> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.</summary>
		/// <param name="collection">The collection to copy elements from.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x06000746 RID: 1862 RVA: 0x00016728 File Offset: 0x00014928
		public Stack(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				this._size = collection2.Count;
				this._array = new T[this._size];
				collection2.CopyTo(this._array, 0);
			}
			else
			{
				foreach (T t in collection)
				{
					this.Push(t);
				}
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Stack`1" />, this property always returns false.</returns>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x000167DC File Offset: 0x000149DC
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Stack`1" />, this property always returns the current instance.</returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x000167E0 File Offset: 0x000149E0
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06000749 RID: 1865 RVA: 0x000167E4 File Offset: 0x000149E4
		void ICollection.CopyTo(Array dest, int idx)
		{
			try
			{
				if (this._array != null)
				{
					this._array.CopyTo(dest, idx);
					Array.Reverse(dest, idx, this._size);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00016844 File Offset: 0x00014A44
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x0600074B RID: 1867 RVA: 0x00016854 File Offset: 0x00014A54
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600074C RID: 1868 RVA: 0x00016864 File Offset: 0x00014A64
		public void Clear()
		{
			if (this._array != null)
			{
				Array.Clear(this._array, 0, this._array.Length);
			}
			this._size = 0;
			this._version++;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.Stack`1" />; otherwise, false.</returns>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.Stack`1" />. The value can be null for reference types.</param>
		// Token: 0x0600074D RID: 1869 RVA: 0x000168A8 File Offset: 0x00014AA8
		public bool Contains(T t)
		{
			return this._array != null && Array.IndexOf<T>(this._array, t, 0, this._size) != -1;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Generic.Stack`1" /> to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Stack`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Stack`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x0600074E RID: 1870 RVA: 0x000168D4 File Offset: 0x00014AD4
		public void CopyTo(T[] dest, int idx)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (idx < 0)
			{
				throw new ArgumentOutOfRangeException("idx");
			}
			if (this._array != null)
			{
				Array.Copy(this._array, 0, dest, idx, this._size);
				Array.Reverse(dest, idx, this._size);
			}
		}

		/// <summary>Returns the object at the top of the <see cref="T:System.Collections.Generic.Stack`1" /> without removing it.</summary>
		/// <returns>The object at the top of the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.Stack`1" /> is empty.</exception>
		// Token: 0x0600074F RID: 1871 RVA: 0x00016930 File Offset: 0x00014B30
		public T Peek()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException();
			}
			return this._array[this._size - 1];
		}

		/// <summary>Removes and returns the object at the top of the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <returns>The object removed from the top of the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.Stack`1" /> is empty.</exception>
		// Token: 0x06000750 RID: 1872 RVA: 0x00016964 File Offset: 0x00014B64
		public T Pop()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException();
			}
			this._version++;
			T result = this._array[--this._size];
			this._array[this._size] = default(T);
			return result;
		}

		/// <summary>Inserts an object at the top of the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <param name="item">The object to push onto the <see cref="T:System.Collections.Generic.Stack`1" />. The value can be null for reference types.</param>
		// Token: 0x06000751 RID: 1873 RVA: 0x000169C8 File Offset: 0x00014BC8
		public void Push(T t)
		{
			if (this._array == null || this._size == this._array.Length)
			{
				Array.Resize<T>(ref this._array, (this._size != 0) ? (2 * this._size) : 16);
			}
			this._version++;
			this._array[this._size++] = t;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Generic.Stack`1" /> to a new array.</summary>
		/// <returns>A new array containing copies of the elements of the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		// Token: 0x06000752 RID: 1874 RVA: 0x00016A44 File Offset: 0x00014C44
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			this.CopyTo(array, 0);
			return array;
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Generic.Stack`1" />, if that number is less than 90 percent of current capacity.</summary>
		// Token: 0x06000753 RID: 1875 RVA: 0x00016A68 File Offset: 0x00014C68
		public void TrimExcess()
		{
			if (this._array != null && (double)this._size < (double)this._array.Length * 0.9)
			{
				Array.Resize<T>(ref this._array, this._size);
			}
			this._version++;
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00016AC0 File Offset: 0x00014CC0
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Returns an enumerator for the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.Stack`1.Enumerator" /> for the <see cref="T:System.Collections.Generic.Stack`1" />.</returns>
		// Token: 0x06000755 RID: 1877 RVA: 0x00016AC8 File Offset: 0x00014CC8
		public Stack<T>.Enumerator GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x040001F7 RID: 503
		private const int INITIAL_SIZE = 16;

		// Token: 0x040001F8 RID: 504
		private T[] _array;

		// Token: 0x040001F9 RID: 505
		private int _size;

		// Token: 0x040001FA RID: 506
		private int _version;

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
		// Token: 0x020000AB RID: 171
		[Serializable]
		public struct Enumerator : IEnumerator, IDisposable, IEnumerator<T>
		{
			// Token: 0x06000756 RID: 1878 RVA: 0x00016AD0 File Offset: 0x00014CD0
			internal Enumerator(Stack<T> t)
			{
				this.parent = t;
				this.idx = -2;
				this._version = t._version;
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection. This class cannot be inherited.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06000757 RID: 1879 RVA: 0x00016AF0 File Offset: 0x00014CF0
			void IEnumerator.Reset()
			{
				if (this._version != this.parent._version)
				{
					throw new InvalidOperationException();
				}
				this.idx = -2;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000183 RID: 387
			// (get) Token: 0x06000758 RID: 1880 RVA: 0x00016B24 File Offset: 0x00014D24
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Stack`1.Enumerator" />.</summary>
			// Token: 0x06000759 RID: 1881 RVA: 0x00016B34 File Offset: 0x00014D34
			public void Dispose()
			{
				this.idx = -2;
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Stack`1" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x0600075A RID: 1882 RVA: 0x00016B40 File Offset: 0x00014D40
			public bool MoveNext()
			{
				if (this._version != this.parent._version)
				{
					throw new InvalidOperationException();
				}
				if (this.idx == -2)
				{
					this.idx = this.parent._size;
				}
				return this.idx != -1 && --this.idx != -1;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.Stack`1" /> at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000184 RID: 388
			// (get) Token: 0x0600075B RID: 1883 RVA: 0x00016BB0 File Offset: 0x00014DB0
			public T Current
			{
				get
				{
					if (this.idx < 0)
					{
						throw new InvalidOperationException();
					}
					return this.parent._array[this.idx];
				}
			}

			// Token: 0x040001FB RID: 507
			private const int NOT_STARTED = -2;

			// Token: 0x040001FC RID: 508
			private const int FINISHED = -1;

			// Token: 0x040001FD RID: 509
			private Stack<T> parent;

			// Token: 0x040001FE RID: 510
			private int idx;

			// Token: 0x040001FF RID: 511
			private int _version;
		}
	}
}
