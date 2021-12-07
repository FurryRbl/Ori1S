using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000033 RID: 51
	internal class SortSequenceContext<TElement, TKey> : SortContext<TElement>
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0000F87C File Offset: 0x0000DA7C
		public SortSequenceContext(Func<TElement, TKey> selector, IComparer<TKey> comparer, SortDirection direction, SortContext<TElement> child_context) : base(direction, child_context)
		{
			this.selector = selector;
			this.comparer = comparer;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000F898 File Offset: 0x0000DA98
		public override void Initialize(TElement[] elements)
		{
			if (this.child_context != null)
			{
				this.child_context.Initialize(elements);
			}
			this.keys = new TKey[elements.Length];
			for (int i = 0; i < this.keys.Length; i++)
			{
				this.keys[i] = this.selector(elements[i]);
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000F904 File Offset: 0x0000DB04
		public override int Compare(int first_index, int second_index)
		{
			int num = this.comparer.Compare(this.keys[first_index], this.keys[second_index]);
			if (num == 0)
			{
				if (this.child_context != null)
				{
					return this.child_context.Compare(first_index, second_index);
				}
				num = ((this.direction != SortDirection.Descending) ? (first_index - second_index) : (second_index - first_index));
			}
			return (this.direction != SortDirection.Descending) ? num : (-num);
		}

		// Token: 0x040000B0 RID: 176
		private Func<TElement, TKey> selector;

		// Token: 0x040000B1 RID: 177
		private IComparer<TKey> comparer;

		// Token: 0x040000B2 RID: 178
		private TKey[] keys;
	}
}
