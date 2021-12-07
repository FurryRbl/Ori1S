using System;
using System.Globalization;

namespace System
{
	// Token: 0x02000175 RID: 373
	[Serializable]
	internal sealed class CultureAwareComparer : StringComparer
	{
		// Token: 0x0600139A RID: 5018 RVA: 0x0004E050 File Offset: 0x0004C250
		public CultureAwareComparer(CultureInfo ci, bool ignore_case)
		{
			this._compareInfo = ci.CompareInfo;
			this._ignoreCase = ignore_case;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0004E06C File Offset: 0x0004C26C
		public override int Compare(string x, string y)
		{
			CompareOptions options = (!this._ignoreCase) ? CompareOptions.None : CompareOptions.IgnoreCase;
			return this._compareInfo.Compare(x, y, options);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004E09C File Offset: 0x0004C29C
		public override bool Equals(string x, string y)
		{
			return this.Compare(x, y) == 0;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0004E0AC File Offset: 0x0004C2AC
		public override int GetHashCode(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			CompareOptions options = (!this._ignoreCase) ? CompareOptions.None : CompareOptions.IgnoreCase;
			return this._compareInfo.GetSortKey(s, options).GetHashCode();
		}

		// Token: 0x040005B0 RID: 1456
		private readonly bool _ignoreCase;

		// Token: 0x040005B1 RID: 1457
		private readonly CompareInfo _compareInfo;
	}
}
