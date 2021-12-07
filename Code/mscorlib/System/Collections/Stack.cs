using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Represents a simple last-in-first-out (LIFO) non-generic collection of objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001D2 RID: 466
	[ComVisible(true)]
	[DebuggerTypeProxy(typeof(CollectionDebuggerView))]
	[DebuggerDisplay("Count={Count}")]
	[Serializable]
	public class Stack : IEnumerable, ICloneable, ICollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x0600181F RID: 6175 RVA: 0x0005CADC File Offset: 0x0005ACDC
		public Stack()
		{
			this.contents = new object[16];
			this.capacity = 16;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that contains elements copied from the specified collection and has the same initial capacity as the number of elements copied.</summary>
		/// <param name="col">The <see cref="T:System.Collections.ICollection" /> to copy elements from. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is null. </exception>
		// Token: 0x06001820 RID: 6176 RVA: 0x0005CB0C File Offset: 0x0005AD0C
		public Stack(ICollection col) : this((col != null) ? col.Count : 16)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Push(obj);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that is empty and has the specified initial capacity or the default initial capacity, whichever is greater.</summary>
		/// <param name="initialCapacity">The initial number of elements that the <see cref="T:System.Collections.Stack" /> can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCapacity" /> is less than zero. </exception>
		// Token: 0x06001821 RID: 6177 RVA: 0x0005CB9C File Offset: 0x0005AD9C
		public Stack(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity");
			}
			this.capacity = initialCapacity;
			this.contents = new object[this.capacity];
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0005CBD8 File Offset: 0x0005ADD8
		private void Resize(int ncapacity)
		{
			ncapacity = Math.Max(ncapacity, 16);
			object[] destinationArray = new object[ncapacity];
			Array.Copy(this.contents, destinationArray, this.count);
			this.capacity = ncapacity;
			this.contents = destinationArray;
		}

		/// <summary>Returns a synchronized (thread safe) wrapper for the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>A synchronized wrapper around the <see cref="T:System.Collections.Stack" />.</returns>
		/// <param name="stack">The <see cref="T:System.Collections.Stack" /> to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stack" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001823 RID: 6179 RVA: 0x0005CC18 File Offset: 0x0005AE18
		public static Stack Synchronized(Stack stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}
			return new Stack.SyncStack(stack);
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x0005CC34 File Offset: 0x0005AE34
		public virtual int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Stack" /> is synchronized (thread safe).</summary>
		/// <returns>true, if access to the <see cref="T:System.Collections.Stack" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0005CC3C File Offset: 0x0005AE3C
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x0005CC40 File Offset: 0x0005AE40
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Stack" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001827 RID: 6183 RVA: 0x0005CC44 File Offset: 0x0005AE44
		public virtual void Clear()
		{
			this.modCount++;
			for (int i = 0; i < this.count; i++)
			{
				this.contents[i] = null;
			}
			this.count = 0;
			this.current = -1;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001828 RID: 6184 RVA: 0x0005CC90 File Offset: 0x0005AE90
		public virtual object Clone()
		{
			return new Stack(this.contents)
			{
				current = this.current,
				count = this.count
			};
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>true, if <paramref name="obj" /> is found in the <see cref="T:System.Collections.Stack" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Stack" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001829 RID: 6185 RVA: 0x0005CCC4 File Offset: 0x0005AEC4
		public virtual bool Contains(object obj)
		{
			if (this.count == 0)
			{
				return false;
			}
			if (obj == null)
			{
				for (int i = 0; i < this.count; i++)
				{
					if (this.contents[i] == null)
					{
						return true;
					}
				}
			}
			else
			{
				for (int j = 0; j < this.count; j++)
				{
					if (obj.Equals(this.contents[j]))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Stack" /> to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Stack" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Stack" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Stack" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600182A RID: 6186 RVA: 0x0005CD3C File Offset: 0x0005AF3C
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
			if (array.Rank > 1 || (array.Length > 0 && index >= array.Length) || this.count > array.Length - index)
			{
				throw new ArgumentException();
			}
			for (int num = this.current; num != -1; num--)
			{
				array.SetValue(this.contents[num], this.count - (num + 1) + index);
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600182B RID: 6187 RVA: 0x0005CDDC File Offset: 0x0005AFDC
		public virtual IEnumerator GetEnumerator()
		{
			return new Stack.Enumerator(this);
		}

		/// <summary>Returns the object at the top of the <see cref="T:System.Collections.Stack" /> without removing it.</summary>
		/// <returns>The <see cref="T:System.Object" /> at the top of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Stack" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600182C RID: 6188 RVA: 0x0005CDE4 File Offset: 0x0005AFE4
		public virtual object Peek()
		{
			if (this.current == -1)
			{
				throw new InvalidOperationException();
			}
			return this.contents[this.current];
		}

		/// <summary>Removes and returns the object at the top of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> removed from the top of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Stack" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600182D RID: 6189 RVA: 0x0005CE08 File Offset: 0x0005B008
		public virtual object Pop()
		{
			if (this.current == -1)
			{
				throw new InvalidOperationException();
			}
			this.modCount++;
			object result = this.contents[this.current];
			this.contents[this.current] = null;
			this.count--;
			this.current--;
			if (this.count <= this.capacity / 4 && this.count > 16)
			{
				this.Resize(this.capacity / 2);
			}
			return result;
		}

		/// <summary>Inserts an object at the top of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to push onto the <see cref="T:System.Collections.Stack" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600182E RID: 6190 RVA: 0x0005CE9C File Offset: 0x0005B09C
		public virtual void Push(object obj)
		{
			this.modCount++;
			if (this.capacity == this.count)
			{
				this.Resize(this.capacity * 2);
			}
			this.count++;
			this.current++;
			this.contents[this.current] = obj;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Stack" /> to a new array.</summary>
		/// <returns>A new array containing copies of the elements of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600182F RID: 6191 RVA: 0x0005CF00 File Offset: 0x0005B100
		public virtual object[] ToArray()
		{
			object[] array = new object[this.count];
			Array.Copy(this.contents, array, this.count);
			Array.Reverse(array);
			return array;
		}

		// Token: 0x040008BE RID: 2238
		private const int default_capacity = 16;

		// Token: 0x040008BF RID: 2239
		private object[] contents;

		// Token: 0x040008C0 RID: 2240
		private int current = -1;

		// Token: 0x040008C1 RID: 2241
		private int count;

		// Token: 0x040008C2 RID: 2242
		private int capacity;

		// Token: 0x040008C3 RID: 2243
		private int modCount;

		// Token: 0x020001D3 RID: 467
		[Serializable]
		private class SyncStack : Stack
		{
			// Token: 0x06001830 RID: 6192 RVA: 0x0005CF34 File Offset: 0x0005B134
			internal SyncStack(Stack s)
			{
				this.stack = s;
			}

			// Token: 0x170003D1 RID: 977
			// (get) Token: 0x06001831 RID: 6193 RVA: 0x0005CF44 File Offset: 0x0005B144
			public override int Count
			{
				get
				{
					Stack obj = this.stack;
					int count;
					lock (obj)
					{
						count = this.stack.Count;
					}
					return count;
				}
			}

			// Token: 0x170003D2 RID: 978
			// (get) Token: 0x06001832 RID: 6194 RVA: 0x0005CF98 File Offset: 0x0005B198
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x06001833 RID: 6195 RVA: 0x0005CF9C File Offset: 0x0005B19C
			public override object SyncRoot
			{
				get
				{
					return this.stack.SyncRoot;
				}
			}

			// Token: 0x06001834 RID: 6196 RVA: 0x0005CFAC File Offset: 0x0005B1AC
			public override void Clear()
			{
				Stack obj = this.stack;
				lock (obj)
				{
					this.stack.Clear();
				}
			}

			// Token: 0x06001835 RID: 6197 RVA: 0x0005CFFC File Offset: 0x0005B1FC
			public override object Clone()
			{
				Stack obj = this.stack;
				object result;
				lock (obj)
				{
					result = Stack.Synchronized((Stack)this.stack.Clone());
				}
				return result;
			}

			// Token: 0x06001836 RID: 6198 RVA: 0x0005D05C File Offset: 0x0005B25C
			public override bool Contains(object obj)
			{
				Stack obj2 = this.stack;
				bool result;
				lock (obj2)
				{
					result = this.stack.Contains(obj);
				}
				return result;
			}

			// Token: 0x06001837 RID: 6199 RVA: 0x0005D0B4 File Offset: 0x0005B2B4
			public override void CopyTo(Array array, int index)
			{
				Stack obj = this.stack;
				lock (obj)
				{
					this.stack.CopyTo(array, index);
				}
			}

			// Token: 0x06001838 RID: 6200 RVA: 0x0005D104 File Offset: 0x0005B304
			public override IEnumerator GetEnumerator()
			{
				Stack obj = this.stack;
				IEnumerator result;
				lock (obj)
				{
					result = new Stack.Enumerator(this.stack);
				}
				return result;
			}

			// Token: 0x06001839 RID: 6201 RVA: 0x0005D158 File Offset: 0x0005B358
			public override object Peek()
			{
				Stack obj = this.stack;
				object result;
				lock (obj)
				{
					result = this.stack.Peek();
				}
				return result;
			}

			// Token: 0x0600183A RID: 6202 RVA: 0x0005D1AC File Offset: 0x0005B3AC
			public override object Pop()
			{
				Stack obj = this.stack;
				object result;
				lock (obj)
				{
					result = this.stack.Pop();
				}
				return result;
			}

			// Token: 0x0600183B RID: 6203 RVA: 0x0005D200 File Offset: 0x0005B400
			public override void Push(object obj)
			{
				Stack obj2 = this.stack;
				lock (obj2)
				{
					this.stack.Push(obj);
				}
			}

			// Token: 0x0600183C RID: 6204 RVA: 0x0005D250 File Offset: 0x0005B450
			public override object[] ToArray()
			{
				Stack obj = this.stack;
				object[] result;
				lock (obj)
				{
					result = this.stack.ToArray();
				}
				return result;
			}

			// Token: 0x040008C4 RID: 2244
			private Stack stack;
		}

		// Token: 0x020001D4 RID: 468
		private class Enumerator : IEnumerator, ICloneable
		{
			// Token: 0x0600183D RID: 6205 RVA: 0x0005D2A4 File Offset: 0x0005B4A4
			internal Enumerator(Stack s)
			{
				this.stack = s;
				this.modCount = s.modCount;
				this.current = -2;
			}

			// Token: 0x0600183E RID: 6206 RVA: 0x0005D2C8 File Offset: 0x0005B4C8
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x170003D4 RID: 980
			// (get) Token: 0x0600183F RID: 6207 RVA: 0x0005D2D0 File Offset: 0x0005B4D0
			public virtual object Current
			{
				get
				{
					if (this.modCount != this.stack.modCount || this.current == -2 || this.current == -1 || this.current > this.stack.count)
					{
						throw new InvalidOperationException();
					}
					return this.stack.contents[this.current];
				}
			}

			// Token: 0x06001840 RID: 6208 RVA: 0x0005D33C File Offset: 0x0005B53C
			public virtual bool MoveNext()
			{
				if (this.modCount != this.stack.modCount)
				{
					throw new InvalidOperationException();
				}
				int num = this.current;
				if (num == -2)
				{
					this.current = this.stack.current;
					return this.current != -1;
				}
				if (num != -1)
				{
					this.current--;
					return this.current != -1;
				}
				return false;
			}

			// Token: 0x06001841 RID: 6209 RVA: 0x0005D3BC File Offset: 0x0005B5BC
			public virtual void Reset()
			{
				if (this.modCount != this.stack.modCount)
				{
					throw new InvalidOperationException();
				}
				this.current = -2;
			}

			// Token: 0x040008C5 RID: 2245
			private const int EOF = -1;

			// Token: 0x040008C6 RID: 2246
			private const int BOF = -2;

			// Token: 0x040008C7 RID: 2247
			private Stack stack;

			// Token: 0x040008C8 RID: 2248
			private int modCount;

			// Token: 0x040008C9 RID: 2249
			private int current;
		}
	}
}
