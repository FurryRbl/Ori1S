using System;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	/// <summary>Represents a node in a <see cref="T:System.Collections.Generic.LinkedList`1" />. This class cannot be inherited.</summary>
	/// <typeparam name="T">Specifies the element type of the linked list.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000094 RID: 148
	[ComVisible(false)]
	public sealed class LinkedListNode<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> class, containing the specified value.</summary>
		/// <param name="value">The value to contain in the <see cref="T:System.Collections.Generic.LinkedListNode`1" />.</param>
		// Token: 0x06000620 RID: 1568 RVA: 0x00012E74 File Offset: 0x00011074
		public LinkedListNode(T value)
		{
			this.item = value;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00012E84 File Offset: 0x00011084
		internal LinkedListNode(LinkedList<T> list, T value)
		{
			this.container = list;
			this.item = value;
			this.forward = this;
			this.back = this;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00012EB8 File Offset: 0x000110B8
		internal LinkedListNode(LinkedList<T> list, T value, LinkedListNode<T> previousNode, LinkedListNode<T> nextNode)
		{
			this.container = list;
			this.item = value;
			this.back = previousNode;
			this.forward = nextNode;
			previousNode.forward = this;
			nextNode.back = this;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00012EF8 File Offset: 0x000110F8
		internal void Detach()
		{
			this.back.forward = this.forward;
			this.forward.back = this.back;
			this.forward = (this.back = null);
			this.container = null;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00012F40 File Offset: 0x00011140
		internal void SelfReference(LinkedList<T> list)
		{
			this.forward = this;
			this.back = this;
			this.container = list;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00012F58 File Offset: 0x00011158
		internal void InsertBetween(LinkedListNode<T> previousNode, LinkedListNode<T> nextNode, LinkedList<T> list)
		{
			previousNode.forward = this;
			nextNode.back = this;
			this.forward = nextNode;
			this.back = previousNode;
			this.container = list;
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.LinkedList`1" /> that the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> belongs to.</summary>
		/// <returns>A reference to the <see cref="T:System.Collections.Generic.LinkedList`1" /> that the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> belongs to, or null if the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> is not linked.</returns>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00012F80 File Offset: 0x00011180
		public LinkedList<T> List
		{
			get
			{
				return this.container;
			}
		}

		/// <summary>Gets the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or null if the current node is the last element (<see cref="P:System.Collections.Generic.LinkedList`1.Last" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x00012F88 File Offset: 0x00011188
		public LinkedListNode<T> Next
		{
			get
			{
				return (this.container == null || this.forward == this.container.first) ? null : this.forward;
			}
		}

		/// <summary>Gets the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or null if the current node is the first element (<see cref="P:System.Collections.Generic.LinkedList`1.First" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00012FB8 File Offset: 0x000111B8
		public LinkedListNode<T> Previous
		{
			get
			{
				return (this.container == null || this == this.container.first) ? null : this.back;
			}
		}

		/// <summary>Gets the value contained in the node.</summary>
		/// <returns>The value contained in the node.</returns>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00012FF0 File Offset: 0x000111F0
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x00012FF8 File Offset: 0x000111F8
		public T Value
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		// Token: 0x040001B0 RID: 432
		private T item;

		// Token: 0x040001B1 RID: 433
		private LinkedList<T> container;

		// Token: 0x040001B2 RID: 434
		internal LinkedListNode<T> forward;

		// Token: 0x040001B3 RID: 435
		internal LinkedListNode<T> back;
	}
}
