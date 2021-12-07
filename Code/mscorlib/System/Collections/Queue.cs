using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Represents a first-in, first-out collection of objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C7 RID: 455
	[DebuggerDisplay("Count={Count}")]
	[ComVisible(true)]
	[DebuggerTypeProxy(typeof(CollectionDebuggerView))]
	[Serializable]
	public class Queue : IEnumerable, ICloneable, ICollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the default initial capacity, and uses the default growth factor.</summary>
		// Token: 0x0600177A RID: 6010 RVA: 0x0005A7D8 File Offset: 0x000589D8
		public Queue() : this(32, 2f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the specified initial capacity, and uses the default growth factor.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Queue" /> can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x0600177B RID: 6011 RVA: 0x0005A7E8 File Offset: 0x000589E8
		public Queue(int capacity) : this(capacity, 2f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that contains elements copied from the specified collection, has the same initial capacity as the number of elements copied, and uses the default growth factor.</summary>
		/// <param name="col">The <see cref="T:System.Collections.ICollection" /> to copy elements from. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is null. </exception>
		// Token: 0x0600177C RID: 6012 RVA: 0x0005A7F8 File Offset: 0x000589F8
		public Queue(ICollection col) : this((col != null) ? col.Count : 32)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Enqueue(obj);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the specified initial capacity, and uses the specified growth factor.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Queue" /> can contain. </param>
		/// <param name="growFactor">The factor by which the capacity of the <see cref="T:System.Collections.Queue" /> is expanded. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- <paramref name="growFactor" /> is less than 1.0 or greater than 10.0. </exception>
		// Token: 0x0600177D RID: 6013 RVA: 0x0005A888 File Offset: 0x00058A88
		public Queue(int capacity, float growFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Needs a non-negative number");
			}
			if (growFactor < 1f || growFactor > 10f)
			{
				throw new ArgumentOutOfRangeException("growFactor", "Queue growth factor must be between 1.0 and 10.0, inclusive");
			}
			this._array = new object[capacity];
			this._growFactor = (int)(growFactor * 100f);
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x0005A8F4 File Offset: 0x00058AF4
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Queue" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.Queue" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x0005A8FC File Offset: 0x00058AFC
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0005A900 File Offset: 0x00058B00
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the <see cref="T:System.Collections.Queue" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Queue" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Queue" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Collections.Queue" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001781 RID: 6017 RVA: 0x0005A904 File Offset: 0x00058B04
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (array.Rank > 1 || (index != 0 && index >= array.Length) || this._size > array.Length - index)
			{
				throw new ArgumentException();
			}
			int num = this._array.Length;
			int num2 = num - this._head;
			Array.Copy(this._array, this._head, array, index, Math.Min(this._size, num2));
			if (this._size > num2)
			{
				Array.Copy(this._array, 0, array, index + num2, this._size - num2);
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001782 RID: 6018 RVA: 0x0005A9C0 File Offset: 0x00058BC0
		public virtual IEnumerator GetEnumerator()
		{
			return new Queue.QueueEnumerator(this);
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001783 RID: 6019 RVA: 0x0005A9C8 File Offset: 0x00058BC8
		public virtual object Clone()
		{
			Queue queue = new Queue(this._array.Length);
			queue._growFactor = this._growFactor;
			Array.Copy(this._array, 0, queue._array, 0, this._array.Length);
			queue._head = this._head;
			queue._size = this._size;
			queue._tail = this._tail;
			return queue;
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Queue" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001784 RID: 6020 RVA: 0x0005AA30 File Offset: 0x00058C30
		public virtual void Clear()
		{
			this._version++;
			this._head = 0;
			this._size = 0;
			this._tail = 0;
			for (int i = this._array.Length - 1; i >= 0; i--)
			{
				this._array[i] = null;
			}
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>true if <paramref name="obj" /> is found in the <see cref="T:System.Collections.Queue" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Queue" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001785 RID: 6021 RVA: 0x0005AA84 File Offset: 0x00058C84
		public virtual bool Contains(object obj)
		{
			int num = this._head + this._size;
			if (obj == null)
			{
				for (int i = this._head; i < num; i++)
				{
					if (this._array[i % this._array.Length] == null)
					{
						return true;
					}
				}
			}
			else
			{
				for (int j = this._head; j < num; j++)
				{
					if (obj.Equals(this._array[j % this._array.Length]))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Removes and returns the object at the beginning of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>The object that is removed from the beginning of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001786 RID: 6022 RVA: 0x0005AB10 File Offset: 0x00058D10
		public virtual object Dequeue()
		{
			this._version++;
			if (this._size < 1)
			{
				throw new InvalidOperationException();
			}
			object result = this._array[this._head];
			this._array[this._head] = null;
			this._head = (this._head + 1) % this._array.Length;
			this._size--;
			return result;
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <param name="obj">The object to add to the <see cref="T:System.Collections.Queue" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001787 RID: 6023 RVA: 0x0005AB80 File Offset: 0x00058D80
		public virtual void Enqueue(object obj)
		{
			this._version++;
			if (this._size == this._array.Length)
			{
				this.grow();
			}
			this._array[this._tail] = obj;
			this._tail = (this._tail + 1) % this._array.Length;
			this._size++;
		}

		/// <summary>Returns the object at the beginning of the <see cref="T:System.Collections.Queue" /> without removing it.</summary>
		/// <returns>The object at the beginning of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001788 RID: 6024 RVA: 0x0005ABE8 File Offset: 0x00058DE8
		public virtual object Peek()
		{
			if (this._size < 1)
			{
				throw new InvalidOperationException();
			}
			return this._array[this._head];
		}

		/// <summary>Returns a <see cref="T:System.Collections.Queue" /> wrapper that is synchronized (thread safe).</summary>
		/// <returns>A <see cref="T:System.Collections.Queue" /> wrapper that is synchronized (thread safe).</returns>
		/// <param name="queue">The <see cref="T:System.Collections.Queue" /> to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="queue" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001789 RID: 6025 RVA: 0x0005AC0C File Offset: 0x00058E0C
		public static Queue Synchronized(Queue queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			return new Queue.SyncQueue(queue);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Queue" /> elements to a new array.</summary>
		/// <returns>A new array containing elements copied from the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600178A RID: 6026 RVA: 0x0005AC28 File Offset: 0x00058E28
		public virtual object[] ToArray()
		{
			object[] array = new object[this._size];
			this.CopyTo(array, 0);
			return array;
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Queue" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Queue" /> is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600178B RID: 6027 RVA: 0x0005AC4C File Offset: 0x00058E4C
		public virtual void TrimToSize()
		{
			this._version++;
			object[] array = new object[this._size];
			this.CopyTo(array, 0);
			this._array = array;
			this._head = 0;
			this._tail = 0;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0005AC90 File Offset: 0x00058E90
		private void grow()
		{
			int num = this._array.Length * this._growFactor / 100;
			if (num < this._array.Length + 1)
			{
				num = this._array.Length + 1;
			}
			object[] array = new object[num];
			this.CopyTo(array, 0);
			this._array = array;
			this._head = 0;
			this._tail = this._head + this._size;
		}

		// Token: 0x0400089B RID: 2203
		private object[] _array;

		// Token: 0x0400089C RID: 2204
		private int _head;

		// Token: 0x0400089D RID: 2205
		private int _size;

		// Token: 0x0400089E RID: 2206
		private int _tail;

		// Token: 0x0400089F RID: 2207
		private int _growFactor;

		// Token: 0x040008A0 RID: 2208
		private int _version;

		// Token: 0x020001C8 RID: 456
		private class SyncQueue : Queue
		{
			// Token: 0x0600178D RID: 6029 RVA: 0x0005ACFC File Offset: 0x00058EFC
			internal SyncQueue(Queue queue)
			{
				this.queue = queue;
			}

			// Token: 0x170003A4 RID: 932
			// (get) Token: 0x0600178E RID: 6030 RVA: 0x0005AD0C File Offset: 0x00058F0C
			public override int Count
			{
				get
				{
					Queue obj = this.queue;
					int count;
					lock (obj)
					{
						count = this.queue.Count;
					}
					return count;
				}
			}

			// Token: 0x170003A5 RID: 933
			// (get) Token: 0x0600178F RID: 6031 RVA: 0x0005AD60 File Offset: 0x00058F60
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003A6 RID: 934
			// (get) Token: 0x06001790 RID: 6032 RVA: 0x0005AD64 File Offset: 0x00058F64
			public override object SyncRoot
			{
				get
				{
					return this.queue.SyncRoot;
				}
			}

			// Token: 0x06001791 RID: 6033 RVA: 0x0005AD74 File Offset: 0x00058F74
			public override void CopyTo(Array array, int index)
			{
				Queue obj = this.queue;
				lock (obj)
				{
					this.queue.CopyTo(array, index);
				}
			}

			// Token: 0x06001792 RID: 6034 RVA: 0x0005ADC4 File Offset: 0x00058FC4
			public override IEnumerator GetEnumerator()
			{
				Queue obj = this.queue;
				IEnumerator enumerator;
				lock (obj)
				{
					enumerator = this.queue.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06001793 RID: 6035 RVA: 0x0005AE18 File Offset: 0x00059018
			public override object Clone()
			{
				Queue obj = this.queue;
				object result;
				lock (obj)
				{
					result = new Queue.SyncQueue((Queue)this.queue.Clone());
				}
				return result;
			}

			// Token: 0x06001794 RID: 6036 RVA: 0x0005AE78 File Offset: 0x00059078
			public override void Clear()
			{
				Queue obj = this.queue;
				lock (obj)
				{
					this.queue.Clear();
				}
			}

			// Token: 0x06001795 RID: 6037 RVA: 0x0005AEC8 File Offset: 0x000590C8
			public override void TrimToSize()
			{
				Queue obj = this.queue;
				lock (obj)
				{
					this.queue.TrimToSize();
				}
			}

			// Token: 0x06001796 RID: 6038 RVA: 0x0005AF18 File Offset: 0x00059118
			public override bool Contains(object obj)
			{
				Queue obj2 = this.queue;
				bool result;
				lock (obj2)
				{
					result = this.queue.Contains(obj);
				}
				return result;
			}

			// Token: 0x06001797 RID: 6039 RVA: 0x0005AF70 File Offset: 0x00059170
			public override object Dequeue()
			{
				Queue obj = this.queue;
				object result;
				lock (obj)
				{
					result = this.queue.Dequeue();
				}
				return result;
			}

			// Token: 0x06001798 RID: 6040 RVA: 0x0005AFC4 File Offset: 0x000591C4
			public override void Enqueue(object obj)
			{
				Queue obj2 = this.queue;
				lock (obj2)
				{
					this.queue.Enqueue(obj);
				}
			}

			// Token: 0x06001799 RID: 6041 RVA: 0x0005B014 File Offset: 0x00059214
			public override object Peek()
			{
				Queue obj = this.queue;
				object result;
				lock (obj)
				{
					result = this.queue.Peek();
				}
				return result;
			}

			// Token: 0x0600179A RID: 6042 RVA: 0x0005B068 File Offset: 0x00059268
			public override object[] ToArray()
			{
				Queue obj = this.queue;
				object[] result;
				lock (obj)
				{
					result = this.queue.ToArray();
				}
				return result;
			}

			// Token: 0x040008A1 RID: 2209
			private Queue queue;
		}

		// Token: 0x020001C9 RID: 457
		[Serializable]
		private class QueueEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x0600179B RID: 6043 RVA: 0x0005B0BC File Offset: 0x000592BC
			internal QueueEnumerator(Queue q)
			{
				this.queue = q;
				this._version = q._version;
				this.current = -1;
			}

			// Token: 0x0600179C RID: 6044 RVA: 0x0005B0EC File Offset: 0x000592EC
			public object Clone()
			{
				return new Queue.QueueEnumerator(this.queue)
				{
					_version = this._version,
					current = this.current
				};
			}

			// Token: 0x170003A7 RID: 935
			// (get) Token: 0x0600179D RID: 6045 RVA: 0x0005B120 File Offset: 0x00059320
			public virtual object Current
			{
				get
				{
					if (this._version != this.queue._version || this.current < 0 || this.current >= this.queue._size)
					{
						throw new InvalidOperationException();
					}
					return this.queue._array[(this.queue._head + this.current) % this.queue._array.Length];
				}
			}

			// Token: 0x0600179E RID: 6046 RVA: 0x0005B198 File Offset: 0x00059398
			public virtual bool MoveNext()
			{
				if (this._version != this.queue._version)
				{
					throw new InvalidOperationException();
				}
				if (this.current >= this.queue._size - 1)
				{
					this.current = int.MaxValue;
					return false;
				}
				this.current++;
				return true;
			}

			// Token: 0x0600179F RID: 6047 RVA: 0x0005B1F8 File Offset: 0x000593F8
			public virtual void Reset()
			{
				if (this._version != this.queue._version)
				{
					throw new InvalidOperationException();
				}
				this.current = -1;
			}

			// Token: 0x040008A2 RID: 2210
			private Queue queue;

			// Token: 0x040008A3 RID: 2211
			private int _version;

			// Token: 0x040008A4 RID: 2212
			private int current;
		}
	}
}
