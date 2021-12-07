using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200046A RID: 1130
	internal class MRUList
	{
		// Token: 0x06002863 RID: 10339 RVA: 0x00080304 File Offset: 0x0007E504
		public MRUList()
		{
			this.head = (this.tail = null);
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x00080328 File Offset: 0x0007E528
		public void Use(object o)
		{
			MRUList.Node node;
			if (this.head == null)
			{
				node = new MRUList.Node(o);
				this.head = (this.tail = node);
				return;
			}
			node = this.head;
			while (node != null && !o.Equals(node.value))
			{
				node = node.previous;
			}
			if (node == null)
			{
				node = new MRUList.Node(o);
			}
			else
			{
				if (node == this.head)
				{
					return;
				}
				if (node == this.tail)
				{
					this.tail = node.next;
				}
				else
				{
					node.previous.next = node.next;
				}
				node.next.previous = node.previous;
			}
			this.head.next = node;
			node.previous = this.head;
			node.next = null;
			this.head = node;
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x00080408 File Offset: 0x0007E608
		public object Evict()
		{
			if (this.tail == null)
			{
				return null;
			}
			object value = this.tail.value;
			this.tail = this.tail.next;
			if (this.tail == null)
			{
				this.head = null;
			}
			else
			{
				this.tail.previous = null;
			}
			return value;
		}

		// Token: 0x04001902 RID: 6402
		private MRUList.Node head;

		// Token: 0x04001903 RID: 6403
		private MRUList.Node tail;

		// Token: 0x0200046B RID: 1131
		private class Node
		{
			// Token: 0x06002866 RID: 10342 RVA: 0x00080464 File Offset: 0x0007E664
			public Node(object value)
			{
				this.value = value;
			}

			// Token: 0x04001904 RID: 6404
			public object value;

			// Token: 0x04001905 RID: 6405
			public MRUList.Node previous;

			// Token: 0x04001906 RID: 6406
			public MRUList.Node next;
		}
	}
}
