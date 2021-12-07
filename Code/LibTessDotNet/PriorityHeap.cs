using System;

namespace LibTessDotNet
{
	// Token: 0x02000008 RID: 8
	internal class PriorityHeap<TValue> where TValue : class
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000032A8 File Offset: 0x000014A8
		public bool Empty
		{
			get
			{
				return this._size == 0;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000032B4 File Offset: 0x000014B4
		public PriorityHeap(int initialSize, PriorityHeap<TValue>.LessOrEqual leq)
		{
			this._leq = leq;
			this._nodes = new int[initialSize + 1];
			this._handles = new PriorityHeap<TValue>.HandleElem[initialSize + 1];
			this._size = 0;
			this._max = initialSize;
			this._freeList = 0;
			this._initialized = false;
			this._nodes[1] = 1;
			this._handles[1] = new PriorityHeap<TValue>.HandleElem
			{
				_key = default(TValue)
			};
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003328 File Offset: 0x00001528
		private void FloatDown(int curr)
		{
			int num = this._nodes[curr];
			for (;;)
			{
				int num2 = curr << 1;
				if (num2 < this._size && this._leq(this._handles[this._nodes[num2 + 1]]._key, this._handles[this._nodes[num2]]._key))
				{
					num2++;
				}
				int num3 = this._nodes[num2];
				if (num2 > this._size || this._leq(this._handles[num]._key, this._handles[num3]._key))
				{
					break;
				}
				this._nodes[curr] = num3;
				this._handles[num3]._node = curr;
				curr = num2;
			}
			this._nodes[curr] = num;
			this._handles[num]._node = curr;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000033F8 File Offset: 0x000015F8
		private void FloatUp(int curr)
		{
			int num = this._nodes[curr];
			for (;;)
			{
				int num2 = curr >> 1;
				int num3 = this._nodes[num2];
				if (num2 == 0 || this._leq(this._handles[num3]._key, this._handles[num]._key))
				{
					break;
				}
				this._nodes[curr] = num3;
				this._handles[num3]._node = curr;
				curr = num2;
			}
			this._nodes[curr] = num;
			this._handles[num]._node = curr;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003478 File Offset: 0x00001678
		public void Init()
		{
			for (int i = this._size; i >= 1; i--)
			{
				this.FloatDown(i);
			}
			this._initialized = true;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000034A4 File Offset: 0x000016A4
		public PQHandle Insert(TValue value)
		{
			int num = this._size + 1;
			this._size = num;
			int num2 = num;
			if (num2 * 2 > this._max)
			{
				this._max <<= 1;
				Array.Resize<int>(ref this._nodes, this._max + 1);
				Array.Resize<PriorityHeap<TValue>.HandleElem>(ref this._handles, this._max + 1);
			}
			int num3;
			if (this._freeList == 0)
			{
				num3 = num2;
			}
			else
			{
				num3 = this._freeList;
				this._freeList = this._handles[num3]._node;
			}
			this._nodes[num2] = num3;
			if (this._handles[num3] == null)
			{
				this._handles[num3] = new PriorityHeap<TValue>.HandleElem
				{
					_key = value,
					_node = num2
				};
			}
			else
			{
				this._handles[num3]._node = num2;
				this._handles[num3]._key = value;
			}
			if (this._initialized)
			{
				this.FloatUp(num2);
			}
			return new PQHandle
			{
				_handle = num3
			};
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003594 File Offset: 0x00001794
		public TValue ExtractMin()
		{
			int num = this._nodes[1];
			TValue key = this._handles[num]._key;
			if (this._size > 0)
			{
				this._nodes[1] = this._nodes[this._size];
				this._handles[this._nodes[1]]._node = 1;
				this._handles[num]._key = default(TValue);
				this._handles[num]._node = this._freeList;
				this._freeList = num;
				int num2 = this._size - 1;
				this._size = num2;
				if (num2 > 0)
				{
					this.FloatDown(1);
				}
			}
			return key;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003632 File Offset: 0x00001832
		public TValue Minimum()
		{
			return this._handles[this._nodes[1]]._key;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003648 File Offset: 0x00001848
		public void Remove(PQHandle handle)
		{
			int handle2 = handle._handle;
			int node = this._handles[handle2]._node;
			this._nodes[node] = this._nodes[this._size];
			this._handles[this._nodes[node]]._node = node;
			int num = node;
			int num2 = this._size - 1;
			this._size = num2;
			if (num <= num2)
			{
				if (node <= 1 || this._leq(this._handles[this._nodes[node >> 1]]._key, this._handles[this._nodes[node]]._key))
				{
					this.FloatDown(node);
				}
				else
				{
					this.FloatUp(node);
				}
			}
			this._handles[handle2]._key = default(TValue);
			this._handles[handle2]._node = this._freeList;
			this._freeList = handle2;
		}

		// Token: 0x0400000E RID: 14
		private PriorityHeap<TValue>.LessOrEqual _leq;

		// Token: 0x0400000F RID: 15
		private int[] _nodes;

		// Token: 0x04000010 RID: 16
		private PriorityHeap<TValue>.HandleElem[] _handles;

		// Token: 0x04000011 RID: 17
		private int _size;

		// Token: 0x04000012 RID: 18
		private int _max;

		// Token: 0x04000013 RID: 19
		private int _freeList;

		// Token: 0x04000014 RID: 20
		private bool _initialized;

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x0600009C RID: 156
		public delegate bool LessOrEqual(TValue lhs, TValue rhs);

		// Token: 0x02000017 RID: 23
		protected class HandleElem
		{
			// Token: 0x0400005E RID: 94
			internal TValue _key;

			// Token: 0x0400005F RID: 95
			internal int _node;
		}
	}
}
