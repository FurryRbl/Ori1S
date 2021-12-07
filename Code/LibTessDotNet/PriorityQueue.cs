using System;
using System.Collections.Generic;

namespace LibTessDotNet
{
	// Token: 0x02000009 RID: 9
	internal class PriorityQueue<TValue> where TValue : class
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003720 File Offset: 0x00001920
		public bool Empty
		{
			get
			{
				return this._size == 0 && this._heap.Empty;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003737 File Offset: 0x00001937
		public PriorityQueue(int initialSize, PriorityHeap<TValue>.LessOrEqual leq)
		{
			this._leq = leq;
			this._heap = new PriorityHeap<TValue>(initialSize, leq);
			this._keys = new TValue[initialSize];
			this._size = 0;
			this._max = initialSize;
			this._initialized = false;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003774 File Offset: 0x00001974
		private static void Swap(ref int a, ref int b)
		{
			int num = a;
			a = b;
			b = num;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000378C File Offset: 0x0000198C
		public void Init()
		{
			Stack<PriorityQueue<TValue>.StackItem> stack = new Stack<PriorityQueue<TValue>.StackItem>();
			uint num = 2016473283U;
			int num2 = 0;
			int i = this._size - 1;
			this._order = new int[this._size + 1];
			int num3 = 0;
			for (int j = num2; j <= i; j++)
			{
				this._order[j] = num3;
				num3++;
			}
			stack.Push(new PriorityQueue<TValue>.StackItem
			{
				p = num2,
				r = i
			});
			while (stack.Count > 0)
			{
				PriorityQueue<TValue>.StackItem stackItem = stack.Pop();
				num2 = stackItem.p;
				i = stackItem.r;
				while (i > num2 + 10)
				{
					num = num * 1539415821U + 1U;
					int j = num2 + (int)((ulong)num % (ulong)((long)(i - num2 + 1)));
					num3 = this._order[j];
					this._order[j] = this._order[num2];
					this._order[num2] = num3;
					j = num2 - 1;
					int num4 = i + 1;
					for (;;)
					{
						j++;
						if (this._leq(this._keys[this._order[j]], this._keys[num3]))
						{
							do
							{
								num4--;
							}
							while (!this._leq(this._keys[num3], this._keys[this._order[num4]]));
							PriorityQueue<TValue>.Swap(ref this._order[j], ref this._order[num4]);
							if (j >= num4)
							{
								break;
							}
						}
					}
					PriorityQueue<TValue>.Swap(ref this._order[j], ref this._order[num4]);
					if (j - num2 < i - num4)
					{
						stack.Push(new PriorityQueue<TValue>.StackItem
						{
							p = num4 + 1,
							r = i
						});
						i = j - 1;
					}
					else
					{
						stack.Push(new PriorityQueue<TValue>.StackItem
						{
							p = num2,
							r = j - 1
						});
						num2 = num4 + 1;
					}
				}
				for (int j = num2 + 1; j <= i; j++)
				{
					num3 = this._order[j];
					int num4 = j;
					while (num4 > num2 && !this._leq(this._keys[num3], this._keys[this._order[num4 - 1]]))
					{
						this._order[num4] = this._order[num4 - 1];
						num4--;
					}
					this._order[num4] = num3;
				}
			}
			this._max = this._size;
			this._initialized = true;
			this._heap.Init();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003A00 File Offset: 0x00001C00
		public PQHandle Insert(TValue value)
		{
			if (this._initialized)
			{
				return this._heap.Insert(value);
			}
			int size = this._size;
			int num = this._size + 1;
			this._size = num;
			if (num >= this._max)
			{
				this._max <<= 1;
				Array.Resize<TValue>(ref this._keys, this._max);
			}
			this._keys[size] = value;
			return new PQHandle
			{
				_handle = -(size + 1)
			};
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003A84 File Offset: 0x00001C84
		public TValue ExtractMin()
		{
			if (this._size == 0)
			{
				return this._heap.ExtractMin();
			}
			TValue tvalue = this._keys[this._order[this._size - 1]];
			if (!this._heap.Empty)
			{
				TValue lhs = this._heap.Minimum();
				if (this._leq(lhs, tvalue))
				{
					return this._heap.ExtractMin();
				}
			}
			do
			{
				this._size--;
			}
			while (this._size > 0 && this._keys[this._order[this._size - 1]] == null);
			return tvalue;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003B30 File Offset: 0x00001D30
		public TValue Minimum()
		{
			if (this._size == 0)
			{
				return this._heap.Minimum();
			}
			TValue tvalue = this._keys[this._order[this._size - 1]];
			if (!this._heap.Empty)
			{
				TValue tvalue2 = this._heap.Minimum();
				if (this._leq(tvalue2, tvalue))
				{
					return tvalue2;
				}
			}
			return tvalue;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003B98 File Offset: 0x00001D98
		public void Remove(PQHandle handle)
		{
			int num = handle._handle;
			if (num >= 0)
			{
				this._heap.Remove(handle);
				return;
			}
			num = -(num + 1);
			this._keys[num] = default(TValue);
			while (this._size > 0 && this._keys[this._order[this._size - 1]] == null)
			{
				this._size--;
			}
		}

		// Token: 0x04000015 RID: 21
		private PriorityHeap<TValue>.LessOrEqual _leq;

		// Token: 0x04000016 RID: 22
		private PriorityHeap<TValue> _heap;

		// Token: 0x04000017 RID: 23
		private TValue[] _keys;

		// Token: 0x04000018 RID: 24
		private int[] _order;

		// Token: 0x04000019 RID: 25
		private int _size;

		// Token: 0x0400001A RID: 26
		private int _max;

		// Token: 0x0400001B RID: 27
		private bool _initialized;

		// Token: 0x02000018 RID: 24
		private class StackItem
		{
			// Token: 0x04000060 RID: 96
			internal int p;

			// Token: 0x04000061 RID: 97
			internal int r;
		}
	}
}
