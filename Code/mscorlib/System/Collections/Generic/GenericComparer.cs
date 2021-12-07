using System;

namespace System.Collections.Generic
{
	// Token: 0x020006D6 RID: 1750
	[Serializable]
	internal sealed class GenericComparer<T> : Comparer<T> where T : IComparable<T>
	{
		// Token: 0x0600434E RID: 17230 RVA: 0x000E5CE0 File Offset: 0x000E3EE0
		public override int Compare(T x, T y)
		{
			if (x == null)
			{
				return (y != null) ? -1 : 0;
			}
			if (y == null)
			{
				return 1;
			}
			return x.CompareTo(y);
		}
	}
}
