using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000030 RID: 48
	internal class QuickSort<TElement>
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		private QuickSort(IEnumerable<TElement> source, SortContext<TElement> context)
		{
			this.elements = source.ToArray<TElement>();
			this.indexes = QuickSort<TElement>.CreateIndexes(this.elements.Length);
			this.context = context;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000F5DC File Offset: 0x0000D7DC
		private static int[] CreateIndexes(int length)
		{
			int[] array = new int[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = i;
			}
			return array;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000F608 File Offset: 0x0000D808
		private void PerformSort()
		{
			if (this.elements.Length <= 1)
			{
				return;
			}
			this.context.Initialize(this.elements);
			this.Sort(0, this.indexes.Length - 1);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000F63C File Offset: 0x0000D83C
		private int CompareItems(int first_index, int second_index)
		{
			return this.context.Compare(first_index, second_index);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000F64C File Offset: 0x0000D84C
		private int MedianOfThree(int left, int right)
		{
			int num = (left + right) / 2;
			if (this.CompareItems(this.indexes[num], this.indexes[left]) < 0)
			{
				this.Swap(left, num);
			}
			if (this.CompareItems(this.indexes[right], this.indexes[left]) < 0)
			{
				this.Swap(left, right);
			}
			if (this.CompareItems(this.indexes[right], this.indexes[num]) < 0)
			{
				this.Swap(num, right);
			}
			this.Swap(num, right - 1);
			return this.indexes[right - 1];
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000F6E0 File Offset: 0x0000D8E0
		private void Sort(int left, int right)
		{
			if (left + 3 <= right)
			{
				int num = left;
				int num2 = right - 1;
				int second_index = this.MedianOfThree(left, right);
				for (;;)
				{
					while (this.CompareItems(this.indexes[++num], second_index) < 0)
					{
					}
					while (this.CompareItems(this.indexes[--num2], second_index) > 0)
					{
					}
					if (num >= num2)
					{
						break;
					}
					this.Swap(num, num2);
				}
				this.Swap(num, right - 1);
				this.Sort(left, num - 1);
				this.Sort(num + 1, right);
			}
			else
			{
				this.InsertionSort(left, right);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000F78C File Offset: 0x0000D98C
		private void InsertionSort(int left, int right)
		{
			for (int i = left + 1; i <= right; i++)
			{
				int num = this.indexes[i];
				int num2 = i;
				while (num2 > left && this.CompareItems(num, this.indexes[num2 - 1]) < 0)
				{
					this.indexes[num2] = this.indexes[num2 - 1];
					num2--;
				}
				this.indexes[num2] = num;
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000F7FC File Offset: 0x0000D9FC
		private void Swap(int left, int right)
		{
			int num = this.indexes[right];
			this.indexes[right] = this.indexes[left];
			this.indexes[left] = num;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000F82C File Offset: 0x0000DA2C
		public static IEnumerable<TElement> Sort(IEnumerable<TElement> source, SortContext<TElement> context)
		{
			QuickSort<TElement> sorter = new QuickSort<TElement>(source, context);
			sorter.PerformSort();
			for (int i = 0; i < sorter.indexes.Length; i++)
			{
				yield return sorter.elements[sorter.indexes[i]];
			}
			yield break;
		}

		// Token: 0x040000A8 RID: 168
		private TElement[] elements;

		// Token: 0x040000A9 RID: 169
		private int[] indexes;

		// Token: 0x040000AA RID: 170
		private SortContext<TElement> context;
	}
}
