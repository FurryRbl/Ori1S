using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000078 RID: 120
	internal class CodePointIndexer
	{
		// Token: 0x06000781 RID: 1921 RVA: 0x00017CFC File Offset: 0x00015EFC
		public CodePointIndexer(int[] starts, int[] ends, int defaultIndex, int defaultCP)
		{
			this.defaultIndex = defaultIndex;
			this.defaultCP = defaultCP;
			this.ranges = new CodePointIndexer.TableRange[starts.Length];
			for (int i = 0; i < this.ranges.Length; i++)
			{
				this.ranges[i] = new CodePointIndexer.TableRange(starts[i], ends[i], (i != 0) ? (this.ranges[i - 1].IndexStart + this.ranges[i - 1].Count) : 0);
			}
			for (int j = 0; j < this.ranges.Length; j++)
			{
				this.TotalCount += this.ranges[j].Count;
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00017DCC File Offset: 0x00015FCC
		public static Array CompressArray(Array source, Type type, CodePointIndexer indexer)
		{
			int num = 0;
			for (int i = 0; i < indexer.ranges.Length; i++)
			{
				num += indexer.ranges[i].Count;
			}
			Array array = Array.CreateInstance(type, num);
			for (int j = 0; j < indexer.ranges.Length; j++)
			{
				Array.Copy(source, indexer.ranges[j].Start, array, indexer.ranges[j].IndexStart, indexer.ranges[j].Count);
			}
			return array;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00017E64 File Offset: 0x00016064
		public int ToIndex(int cp)
		{
			for (int i = 0; i < this.ranges.Length; i++)
			{
				if (cp < this.ranges[i].Start)
				{
					return this.defaultIndex;
				}
				if (cp < this.ranges[i].End)
				{
					return cp - this.ranges[i].Start + this.ranges[i].IndexStart;
				}
			}
			return this.defaultIndex;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00017EEC File Offset: 0x000160EC
		public int ToCodePoint(int i)
		{
			for (int j = 0; j < this.ranges.Length; j++)
			{
				if (i < this.ranges[j].IndexStart)
				{
					return this.defaultCP;
				}
				if (i < this.ranges[j].IndexEnd)
				{
					return i - this.ranges[j].IndexStart + this.ranges[j].Start;
				}
			}
			return this.defaultCP;
		}

		// Token: 0x04000115 RID: 277
		private readonly CodePointIndexer.TableRange[] ranges;

		// Token: 0x04000116 RID: 278
		public readonly int TotalCount;

		// Token: 0x04000117 RID: 279
		private int defaultIndex;

		// Token: 0x04000118 RID: 280
		private int defaultCP;

		// Token: 0x02000079 RID: 121
		[Serializable]
		internal struct TableRange
		{
			// Token: 0x06000785 RID: 1925 RVA: 0x00017F74 File Offset: 0x00016174
			public TableRange(int start, int end, int indexStart)
			{
				this.Start = start;
				this.End = end;
				this.Count = this.End - this.Start;
				this.IndexStart = indexStart;
				this.IndexEnd = this.IndexStart + this.Count;
			}

			// Token: 0x04000119 RID: 281
			public readonly int Start;

			// Token: 0x0400011A RID: 282
			public readonly int End;

			// Token: 0x0400011B RID: 283
			public readonly int Count;

			// Token: 0x0400011C RID: 284
			public readonly int IndexStart;

			// Token: 0x0400011D RID: 285
			public readonly int IndexEnd;
		}
	}
}
