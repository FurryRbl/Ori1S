using System;
using System.Collections;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200007C RID: 124
	internal class ContractionComparer : IComparer
	{
		// Token: 0x0600078A RID: 1930 RVA: 0x00018010 File Offset: 0x00016210
		public int Compare(object o1, object o2)
		{
			Contraction contraction = (Contraction)o1;
			Contraction contraction2 = (Contraction)o2;
			char[] source = contraction.Source;
			char[] source2 = contraction2.Source;
			int num = (source.Length <= source2.Length) ? source.Length : source2.Length;
			for (int i = 0; i < num; i++)
			{
				if (source[i] != source2[i])
				{
					return (int)(source[i] - source2[i]);
				}
			}
			return source.Length - source2.Length;
		}

		// Token: 0x04000125 RID: 293
		public static readonly ContractionComparer Instance = new ContractionComparer();
	}
}
