using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Collections.Generic
{
	/// <summary>Represents a doubly linked list.</summary>
	/// <typeparam name="T">Specifies the element type of the linked list.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000092 RID: 146
	[ComVisible(false)]
	[Serializable]
	public class LinkedList<T> : IEnumerable<T>, ICollection, IEnumerable, IDeserializationCallback, ICollection<T>, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedList`1" /> class that is empty.</summary>
		// Token: 0x060005F4 RID: 1524 RVA: 0x00012374 File Offset: 0x00010574
		public LinkedList()
		{
			this.syncRoot = new object();
			this.first = null;
			this.count = (this.version = 0U);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedList`1" /> class that contains elements copied from the specified <see cref="T:System.Collections.IEnumerable" /> and has sufficient capacity to accommodate the number of elements copied. </summary>
		/// <param name="collection">The <see cref="T:System.Collections.IEnumerable" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x060005F5 RID: 1525 RVA: 0x000123AC File Offset: 0x000105AC
		public LinkedList(IEnumerable<T> collection) : this()
		{
			foreach (T value in collection)
			{
				this.AddLast(value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedList`1" /> class that is serializable with the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x060005F6 RID: 1526 RVA: 0x00012414 File Offset: 0x00010614
		protected LinkedList(SerializationInfo info, StreamingContext context) : this()
		{
			this.si = info;
			this.syncRoot = new object();
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00012430 File Offset: 0x00010630
		void ICollection<!0>.Add(T value)
		{
			this.AddLast(value);
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
		// Token: 0x060005F8 RID: 1528 RVA: 0x0001243C File Offset: 0x0001063C
		void ICollection.CopyTo(Array array, int index)
		{
			T[] array2 = array as T[];
			if (array2 == null)
			{
				throw new ArgumentException("array");
			}
			this.CopyTo(array2, index);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001246C File Offset: 0x0001066C
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through the linked list as a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the linked list as a collection.</returns>
		// Token: 0x060005FA RID: 1530 RVA: 0x0001247C File Offset: 0x0001067C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001248C File Offset: 0x0001068C
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.LinkedList`1" />, this property always returns false.</returns>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00012490 File Offset: 0x00010690
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.LinkedList`1" />, this property always returns the current instance.</returns>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00012494 File Offset: 0x00010694
		object ICollection.SyncRoot
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001249C File Offset: 0x0001069C
		private void VerifyReferencedNode(LinkedListNode<T> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.List != this)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000124C4 File Offset: 0x000106C4
		private static void VerifyBlankNode(LinkedListNode<T> newNode)
		{
			if (newNode == null)
			{
				throw new ArgumentNullException("newNode");
			}
			if (newNode.List != null)
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>Adds a new node containing the specified value after the specified existing node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</returns>
		/// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1" /> after which to insert a new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</param>
		/// <param name="value">The value to add to the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06000600 RID: 1536 RVA: 0x000124F4 File Offset: 0x000106F4
		public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
		{
			this.VerifyReferencedNode(node);
			LinkedListNode<T> result = new LinkedListNode<T>(this, value, node, node.forward);
			this.count += 1U;
			this.version += 1U;
			return result;
		}

		/// <summary>Adds the specified new node after the specified existing node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1" /> after which to insert <paramref name="newNode" />.</param>
		/// <param name="newNode">The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to add to the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.-or-<paramref name="newNode" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1" />.-or-<paramref name="newNode" /> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06000601 RID: 1537 RVA: 0x00012534 File Offset: 0x00010734
		public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
		{
			this.VerifyReferencedNode(node);
			LinkedList<T>.VerifyBlankNode(newNode);
			newNode.InsertBetween(node, node.forward, this);
			this.count += 1U;
			this.version += 1U;
		}

		/// <summary>Adds a new node containing the specified value before the specified existing node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</returns>
		/// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1" /> before which to insert a new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</param>
		/// <param name="value">The value to add to the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06000602 RID: 1538 RVA: 0x00012578 File Offset: 0x00010778
		public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
		{
			this.VerifyReferencedNode(node);
			LinkedListNode<T> result = new LinkedListNode<T>(this, value, node.back, node);
			this.count += 1U;
			this.version += 1U;
			if (node == this.first)
			{
				this.first = result;
			}
			return result;
		}

		/// <summary>Adds the specified new node before the specified existing node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1" /> before which to insert <paramref name="newNode" />.</param>
		/// <param name="newNode">The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to add to the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.-or-<paramref name="newNode" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1" />.-or-<paramref name="newNode" /> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06000603 RID: 1539 RVA: 0x000125CC File Offset: 0x000107CC
		public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
		{
			this.VerifyReferencedNode(node);
			LinkedList<T>.VerifyBlankNode(newNode);
			newNode.InsertBetween(node.back, node, this);
			this.count += 1U;
			this.version += 1U;
			if (node == this.first)
			{
				this.first = newNode;
			}
		}

		/// <summary>Adds the specified new node at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to add at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06000604 RID: 1540 RVA: 0x00012624 File Offset: 0x00010824
		public void AddFirst(LinkedListNode<T> node)
		{
			LinkedList<T>.VerifyBlankNode(node);
			if (this.first == null)
			{
				node.SelfReference(this);
			}
			else
			{
				node.InsertBetween(this.first.back, this.first, this);
			}
			this.count += 1U;
			this.version += 1U;
			this.first = node;
		}

		/// <summary>Adds a new node containing the specified value at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</returns>
		/// <param name="value">The value to add at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x06000605 RID: 1541 RVA: 0x0001268C File Offset: 0x0001088C
		public LinkedListNode<T> AddFirst(T value)
		{
			LinkedListNode<T> result;
			if (this.first == null)
			{
				result = new LinkedListNode<T>(this, value);
			}
			else
			{
				result = new LinkedListNode<T>(this, value, this.first.back, this.first);
			}
			this.count += 1U;
			this.version += 1U;
			this.first = result;
			return result;
		}

		/// <summary>Adds a new node containing the specified value at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> containing <paramref name="value" />.</returns>
		/// <param name="value">The value to add at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x06000606 RID: 1542 RVA: 0x000126F0 File Offset: 0x000108F0
		public LinkedListNode<T> AddLast(T value)
		{
			LinkedListNode<T> result;
			if (this.first == null)
			{
				result = new LinkedListNode<T>(this, value);
				this.first = result;
			}
			else
			{
				result = new LinkedListNode<T>(this, value, this.first.back, this.first);
			}
			this.count += 1U;
			this.version += 1U;
			return result;
		}

		/// <summary>Adds the specified new node at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The new <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to add at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> belongs to another <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06000607 RID: 1543 RVA: 0x00012754 File Offset: 0x00010954
		public void AddLast(LinkedListNode<T> node)
		{
			LinkedList<T>.VerifyBlankNode(node);
			if (this.first == null)
			{
				node.SelfReference(this);
				this.first = node;
			}
			else
			{
				node.InsertBetween(this.first.back, this.first, this);
			}
			this.count += 1U;
			this.version += 1U;
		}

		/// <summary>Removes all nodes from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		// Token: 0x06000608 RID: 1544 RVA: 0x000127BC File Offset: 0x000109BC
		public void Clear()
		{
			while (this.first != null)
			{
				this.RemoveLast();
			}
		}

		/// <summary>Determines whether a value is in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>true if <paramref name="value" /> is found in the <see cref="T:System.Collections.Generic.LinkedList`1" />; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.LinkedList`1" />. The value can be null for reference types.</param>
		// Token: 0x06000609 RID: 1545 RVA: 0x000127D4 File Offset: 0x000109D4
		public bool Contains(T value)
		{
			LinkedListNode<T> forward = this.first;
			if (forward == null)
			{
				return false;
			}
			while (!value.Equals(forward.Value))
			{
				forward = forward.forward;
				if (forward == this.first)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Generic.LinkedList`1" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.LinkedList`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.LinkedList`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x0600060A RID: 1546 RVA: 0x00012824 File Offset: 0x00010A24
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < array.GetLowerBound(0))
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("array", "Array is multidimensional");
			}
			if ((long)(array.Length - index + array.GetLowerBound(0)) < (long)((ulong)this.count))
			{
				throw new ArgumentException("number of items exceeds capacity");
			}
			LinkedListNode<T> forward = this.first;
			if (this.first == null)
			{
				return;
			}
			do
			{
				array[index] = forward.Value;
				index++;
				forward = forward.forward;
			}
			while (forward != this.first);
		}

		/// <summary>Finds the first node that contains the specified value.</summary>
		/// <returns>The first <see cref="T:System.Collections.Generic.LinkedListNode`1" /> that contains the specified value, if found; otherwise, null.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x0600060B RID: 1547 RVA: 0x000128D4 File Offset: 0x00010AD4
		public LinkedListNode<T> Find(T value)
		{
			LinkedListNode<T> forward = this.first;
			if (forward == null)
			{
				return null;
			}
			while ((value != null || forward.Value != null) && (value == null || !value.Equals(forward.Value)))
			{
				forward = forward.forward;
				if (forward == this.first)
				{
					return null;
				}
			}
			return forward;
		}

		/// <summary>Finds the last node that contains the specified value.</summary>
		/// <returns>The last <see cref="T:System.Collections.Generic.LinkedListNode`1" /> that contains the specified value, if found; otherwise, null.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x0600060C RID: 1548 RVA: 0x0001294C File Offset: 0x00010B4C
		public LinkedListNode<T> FindLast(T value)
		{
			LinkedListNode<T> back = this.first;
			if (back == null)
			{
				return null;
			}
			for (;;)
			{
				back = back.back;
				if (value.Equals(back.Value))
				{
					break;
				}
				if (back == this.first)
				{
					goto Block_3;
				}
			}
			return back;
			Block_3:
			return null;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.LinkedList`1.Enumerator" /> for the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x0600060D RID: 1549 RVA: 0x0001299C File Offset: 0x00010B9C
		public LinkedList<T>.Enumerator GetEnumerator()
		{
			return new LinkedList<T>.Enumerator(this);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x0600060E RID: 1550 RVA: 0x000129A4 File Offset: 0x00010BA4
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			T[] array = new T[this.count];
			this.CopyTo(array, 0);
			info.AddValue("DataArray", array, typeof(T[]));
			info.AddValue("version", this.version);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.LinkedList`1" /> instance is invalid.</exception>
		// Token: 0x0600060F RID: 1551 RVA: 0x000129F0 File Offset: 0x00010BF0
		public virtual void OnDeserialization(object sender)
		{
			if (this.si != null)
			{
				T[] array = (T[])this.si.GetValue("DataArray", typeof(T[]));
				if (array != null)
				{
					foreach (T value in array)
					{
						this.AddLast(value);
					}
				}
				this.version = this.si.GetUInt32("version");
				this.si = null;
			}
		}

		/// <summary>Removes the first occurrence of the specified value from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>true if the element containing <paramref name="value" /> is successfully removed; otherwise, false.  This method also returns false if <paramref name="value" /> was not found in the original <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		/// <param name="value">The value to remove from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		// Token: 0x06000610 RID: 1552 RVA: 0x00012A74 File Offset: 0x00010C74
		public bool Remove(T value)
		{
			LinkedListNode<T> linkedListNode = this.Find(value);
			if (linkedListNode == null)
			{
				return false;
			}
			this.Remove(linkedListNode);
			return true;
		}

		/// <summary>Removes the specified node from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <param name="node">The <see cref="T:System.Collections.Generic.LinkedListNode`1" /> to remove from the <see cref="T:System.Collections.Generic.LinkedList`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is not in the current <see cref="T:System.Collections.Generic.LinkedList`1" />.</exception>
		// Token: 0x06000611 RID: 1553 RVA: 0x00012A9C File Offset: 0x00010C9C
		public void Remove(LinkedListNode<T> node)
		{
			this.VerifyReferencedNode(node);
			this.count -= 1U;
			if (this.count == 0U)
			{
				this.first = null;
			}
			if (node == this.first)
			{
				this.first = this.first.forward;
			}
			this.version += 1U;
			node.Detach();
		}

		/// <summary>Removes the node at the start of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.LinkedList`1" /> is empty.</exception>
		// Token: 0x06000612 RID: 1554 RVA: 0x00012B04 File Offset: 0x00010D04
		public void RemoveFirst()
		{
			if (this.first != null)
			{
				this.Remove(this.first);
			}
		}

		/// <summary>Removes the node at the end of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Generic.LinkedList`1" /> is empty.</exception>
		// Token: 0x06000613 RID: 1555 RVA: 0x00012B20 File Offset: 0x00010D20
		public void RemoveLast()
		{
			if (this.first != null)
			{
				this.Remove(this.first.back);
			}
		}

		/// <summary>Gets the number of nodes actually contained in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The number of nodes actually contained in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00012B40 File Offset: 0x00010D40
		public int Count
		{
			get
			{
				return (int)this.count;
			}
		}

		/// <summary>Gets the first node of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The first <see cref="T:System.Collections.Generic.LinkedListNode`1" /> of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00012B48 File Offset: 0x00010D48
		public LinkedListNode<T> First
		{
			get
			{
				return this.first;
			}
		}

		/// <summary>Gets the last node of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>The last <see cref="T:System.Collections.Generic.LinkedListNode`1" /> of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00012B50 File Offset: 0x00010D50
		public LinkedListNode<T> Last
		{
			get
			{
				return (this.first == null) ? null : this.first.back;
			}
		}

		// Token: 0x040001A1 RID: 417
		private const string DataArrayKey = "DataArray";

		// Token: 0x040001A2 RID: 418
		private const string VersionKey = "version";

		// Token: 0x040001A3 RID: 419
		private uint count;

		// Token: 0x040001A4 RID: 420
		private uint version;

		// Token: 0x040001A5 RID: 421
		private object syncRoot;

		// Token: 0x040001A6 RID: 422
		internal LinkedListNode<T> first;

		// Token: 0x040001A7 RID: 423
		internal SerializationInfo si;

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		// Token: 0x02000093 RID: 147
		[Serializable]
		public struct Enumerator : IEnumerator, IDisposable, IEnumerator<T>, IDeserializationCallback, ISerializable
		{
			// Token: 0x06000617 RID: 1559 RVA: 0x00012B70 File Offset: 0x00010D70
			internal Enumerator(SerializationInfo info, StreamingContext context)
			{
				this.si = info;
				this.list = (LinkedList<T>)this.si.GetValue("list", typeof(LinkedList<T>));
				this.index = this.si.GetInt32("index");
				this.version = this.si.GetUInt32("version");
				this.current = null;
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x00012BDC File Offset: 0x00010DDC
			internal Enumerator(LinkedList<T> parent)
			{
				this.si = null;
				this.list = parent;
				this.current = null;
				this.index = -1;
				this.version = parent.version;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000619 RID: 1561 RVA: 0x00012C14 File Offset: 0x00010E14
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection. This class cannot be inherited.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x0600061A RID: 1562 RVA: 0x00012C24 File Offset: 0x00010E24
			void IEnumerator.Reset()
			{
				if (this.list == null)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException("list modified");
				}
				this.current = null;
				this.index = -1;
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</summary>
			/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</param>
			/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.LinkedList`1" /> instance.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="info" /> is null.</exception>
			// Token: 0x0600061B RID: 1563 RVA: 0x00012C74 File Offset: 0x00010E74
			[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (this.list == null)
				{
					throw new ObjectDisposedException(null);
				}
				info.AddValue("version", this.version);
				info.AddValue("index", this.index);
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
			/// <param name="sender">The source of the deserialization event.</param>
			/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.LinkedList`1" /> instance is invalid.</exception>
			// Token: 0x0600061C RID: 1564 RVA: 0x00012CB8 File Offset: 0x00010EB8
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				if (this.si == null)
				{
					return;
				}
				if (this.list.si != null)
				{
					((IDeserializationCallback)this.list).OnDeserialization(this);
				}
				this.si = null;
				if (this.version == this.list.version && this.index != -1)
				{
					LinkedListNode<T> linkedListNode = this.list.First;
					for (int i = 0; i < this.index; i++)
					{
						linkedListNode = linkedListNode.forward;
					}
					this.current = linkedListNode;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.LinkedList`1" /> at the current position of the enumerator.</returns>
			// Token: 0x1700012D RID: 301
			// (get) Token: 0x0600061D RID: 1565 RVA: 0x00012D54 File Offset: 0x00010F54
			public T Current
			{
				get
				{
					if (this.list == null)
					{
						throw new ObjectDisposedException(null);
					}
					if (this.current == null)
					{
						throw new InvalidOperationException();
					}
					return this.current.Value;
				}
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x0600061E RID: 1566 RVA: 0x00012D90 File Offset: 0x00010F90
			public bool MoveNext()
			{
				if (this.list == null)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException("list modified");
				}
				if (this.current == null)
				{
					this.current = this.list.first;
				}
				else
				{
					this.current = this.current.forward;
					if (this.current == this.list.first)
					{
						this.current = null;
					}
				}
				if (this.current == null)
				{
					this.index = -1;
					return false;
				}
				this.index++;
				return true;
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.LinkedList`1.Enumerator" />.</summary>
			// Token: 0x0600061F RID: 1567 RVA: 0x00012E44 File Offset: 0x00011044
			public void Dispose()
			{
				if (this.list == null)
				{
					throw new ObjectDisposedException(null);
				}
				this.current = null;
				this.list = null;
			}

			// Token: 0x040001A8 RID: 424
			private const string VersionKey = "version";

			// Token: 0x040001A9 RID: 425
			private const string IndexKey = "index";

			// Token: 0x040001AA RID: 426
			private const string ListKey = "list";

			// Token: 0x040001AB RID: 427
			private LinkedList<T> list;

			// Token: 0x040001AC RID: 428
			private LinkedListNode<T> current;

			// Token: 0x040001AD RID: 429
			private int index;

			// Token: 0x040001AE RID: 430
			private uint version;

			// Token: 0x040001AF RID: 431
			private SerializationInfo si;
		}
	}
}
