using System;

namespace System.Linq
{
	// Token: 0x02000031 RID: 49
	internal abstract class SortContext<TElement>
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000F864 File Offset: 0x0000DA64
		protected SortContext(SortDirection direction, SortContext<TElement> child_context)
		{
			this.direction = direction;
			this.child_context = child_context;
		}

		// Token: 0x0600036B RID: 875
		public abstract void Initialize(TElement[] elements);

		// Token: 0x0600036C RID: 876
		public abstract int Compare(int first_index, int second_index);

		// Token: 0x040000AB RID: 171
		protected SortDirection direction;

		// Token: 0x040000AC RID: 172
		protected SortContext<TElement> child_context;
	}
}
