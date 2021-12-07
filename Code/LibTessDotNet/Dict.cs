using System;

namespace LibTessDotNet
{
	// Token: 0x02000002 RID: 2
	internal class Dict<TValue> where TValue : class
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Dict(Dict<TValue>.LessOrEqual leq)
		{
			this._leq = leq;
			this._head = new Dict<TValue>.Node
			{
				_key = default(TValue)
			};
			this._head._prev = this._head;
			this._head._next = this._head;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020A3 File Offset: 0x000002A3
		public Dict<TValue>.Node Insert(TValue key)
		{
			return this.InsertBefore(this._head, key);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020B4 File Offset: 0x000002B4
		public Dict<TValue>.Node InsertBefore(Dict<TValue>.Node node, TValue key)
		{
			do
			{
				node = node._prev;
			}
			while (node._key != null && !this._leq(node._key, key));
			Dict<TValue>.Node node2 = new Dict<TValue>.Node
			{
				_key = key
			};
			node2._next = node._next;
			node._next._prev = node2;
			node2._prev = node;
			node._next = node2;
			return node2;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002120 File Offset: 0x00000320
		public Dict<TValue>.Node Find(TValue key)
		{
			Dict<TValue>.Node node = this._head;
			do
			{
				node = node._next;
			}
			while (node._key != null && !this._leq(key, node._key));
			return node;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000215D File Offset: 0x0000035D
		public Dict<TValue>.Node Min()
		{
			return this._head._next;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000216A File Offset: 0x0000036A
		public void Remove(Dict<TValue>.Node node)
		{
			node._next._prev = node._prev;
			node._prev._next = node._next;
		}

		// Token: 0x04000001 RID: 1
		private Dict<TValue>.LessOrEqual _leq;

		// Token: 0x04000002 RID: 2
		private Dict<TValue>.Node _head;

		// Token: 0x02000010 RID: 16
		public class Node
		{
			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600007D RID: 125 RVA: 0x00006086 File Offset: 0x00004286
			public TValue Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600007E RID: 126 RVA: 0x0000608E File Offset: 0x0000428E
			public Dict<TValue>.Node Prev
			{
				get
				{
					return this._prev;
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x0600007F RID: 127 RVA: 0x00006096 File Offset: 0x00004296
			public Dict<TValue>.Node Next
			{
				get
				{
					return this._next;
				}
			}

			// Token: 0x04000040 RID: 64
			internal TValue _key;

			// Token: 0x04000041 RID: 65
			internal Dict<TValue>.Node _prev;

			// Token: 0x04000042 RID: 66
			internal Dict<TValue>.Node _next;
		}

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x06000082 RID: 130
		public delegate bool LessOrEqual(TValue lhs, TValue rhs);
	}
}
