using System;
using System.Collections;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200007E RID: 126
	internal class Level2MapComparer : IComparer
	{
		// Token: 0x0600078E RID: 1934 RVA: 0x000180B4 File Offset: 0x000162B4
		public int Compare(object o1, object o2)
		{
			Level2Map level2Map = (Level2Map)o1;
			Level2Map level2Map2 = (Level2Map)o2;
			return (int)(level2Map.Source - level2Map2.Source);
		}

		// Token: 0x04000128 RID: 296
		public static readonly Level2MapComparer Instance = new Level2MapComparer();
	}
}
