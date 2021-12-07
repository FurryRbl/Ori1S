using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000482 RID: 1154
	internal class IntervalCollection : ICollection, IEnumerable
	{
		// Token: 0x0600293D RID: 10557 RVA: 0x00088230 File Offset: 0x00086430
		public IntervalCollection()
		{
			this.intervals = new ArrayList();
		}

		// Token: 0x17000B78 RID: 2936
		public Interval this[int i]
		{
			get
			{
				return (Interval)this.intervals[i];
			}
			set
			{
				this.intervals[i] = value;
			}
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x0008826C File Offset: 0x0008646C
		public void Add(Interval i)
		{
			this.intervals.Add(i);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x00088280 File Offset: 0x00086480
		public void Clear()
		{
			this.intervals.Clear();
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x00088290 File Offset: 0x00086490
		public void Sort()
		{
			this.intervals.Sort();
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000882A0 File Offset: 0x000864A0
		public void Normalize()
		{
			this.intervals.Sort();
			int i = 0;
			while (i < this.intervals.Count - 1)
			{
				Interval interval = (Interval)this.intervals[i];
				Interval i2 = (Interval)this.intervals[i + 1];
				if (!interval.IsDisjoint(i2) || interval.IsAdjacent(i2))
				{
					interval.Merge(i2);
					this.intervals[i] = interval;
					this.intervals.RemoveAt(i + 1);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x00088344 File Offset: 0x00086544
		public IntervalCollection GetMetaCollection(IntervalCollection.CostDelegate cost_del)
		{
			IntervalCollection intervalCollection = new IntervalCollection();
			this.Normalize();
			this.Optimize(0, this.Count - 1, intervalCollection, cost_del);
			intervalCollection.intervals.Sort();
			return intervalCollection;
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x0008837C File Offset: 0x0008657C
		private void Optimize(int begin, int end, IntervalCollection meta, IntervalCollection.CostDelegate cost_del)
		{
			Interval i;
			i.contiguous = false;
			int num = -1;
			int num2 = -1;
			double num3 = 0.0;
			for (int j = begin; j <= end; j++)
			{
				i.low = this[j].low;
				double num4 = 0.0;
				for (int k = j; k <= end; k++)
				{
					i.high = this[k].high;
					num4 += cost_del(this[k]);
					double num5 = cost_del(i);
					if (num5 < num4 && num4 > num3)
					{
						num = j;
						num2 = k;
						num3 = num4;
					}
				}
			}
			if (num < 0)
			{
				for (int l = begin; l <= end; l++)
				{
					meta.Add(this[l]);
				}
			}
			else
			{
				i.low = this[num].low;
				i.high = this[num2].high;
				meta.Add(i);
				if (num > begin)
				{
					this.Optimize(begin, num - 1, meta, cost_del);
				}
				if (num2 < end)
				{
					this.Optimize(num2 + 1, end, meta, cost_del);
				}
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x000884CC File Offset: 0x000866CC
		public int Count
		{
			get
			{
				return this.intervals.Count;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000884DC File Offset: 0x000866DC
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x000884E0 File Offset: 0x000866E0
		public object SyncRoot
		{
			get
			{
				return this.intervals;
			}
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000884E8 File Offset: 0x000866E8
		public void CopyTo(Array array, int index)
		{
			foreach (object obj in this.intervals)
			{
				Interval interval = (Interval)obj;
				if (index > array.Length)
				{
					break;
				}
				array.SetValue(interval, index++);
			}
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x00088574 File Offset: 0x00086774
		public IEnumerator GetEnumerator()
		{
			return new IntervalCollection.Enumerator(this.intervals);
		}

		// Token: 0x040019F0 RID: 6640
		private ArrayList intervals;

		// Token: 0x02000483 RID: 1155
		private class Enumerator : IEnumerator
		{
			// Token: 0x0600294B RID: 10571 RVA: 0x00088584 File Offset: 0x00086784
			public Enumerator(IList list)
			{
				this.list = list;
				this.Reset();
			}

			// Token: 0x17000B7C RID: 2940
			// (get) Token: 0x0600294C RID: 10572 RVA: 0x0008859C File Offset: 0x0008679C
			public object Current
			{
				get
				{
					if (this.ptr >= this.list.Count)
					{
						throw new InvalidOperationException();
					}
					return this.list[this.ptr];
				}
			}

			// Token: 0x0600294D RID: 10573 RVA: 0x000885CC File Offset: 0x000867CC
			public bool MoveNext()
			{
				if (this.ptr > this.list.Count)
				{
					throw new InvalidOperationException();
				}
				return ++this.ptr < this.list.Count;
			}

			// Token: 0x0600294E RID: 10574 RVA: 0x00088614 File Offset: 0x00086814
			public void Reset()
			{
				this.ptr = -1;
			}

			// Token: 0x040019F1 RID: 6641
			private IList list;

			// Token: 0x040019F2 RID: 6642
			private int ptr;
		}

		// Token: 0x020004ED RID: 1261
		// (Invoke) Token: 0x06002C60 RID: 11360
		public delegate double CostDelegate(Interval i);
	}
}
