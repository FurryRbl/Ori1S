using System;

namespace System
{
	// Token: 0x02000176 RID: 374
	[Serializable]
	internal sealed class OrdinalComparer : StringComparer
	{
		// Token: 0x0600139E RID: 5022 RVA: 0x0004E0F0 File Offset: 0x0004C2F0
		public OrdinalComparer(bool ignoreCase)
		{
			this._ignoreCase = ignoreCase;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004E100 File Offset: 0x0004C300
		public override int Compare(string x, string y)
		{
			if (this._ignoreCase)
			{
				return string.CompareOrdinalCaseInsensitiveUnchecked(x, 0, int.MaxValue, y, 0, int.MaxValue);
			}
			return string.CompareOrdinalUnchecked(x, 0, int.MaxValue, y, 0, int.MaxValue);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0004E140 File Offset: 0x0004C340
		public override bool Equals(string x, string y)
		{
			if (this._ignoreCase)
			{
				return this.Compare(x, y) == 0;
			}
			return x == y;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0004E160 File Offset: 0x0004C360
		public override int GetHashCode(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (this._ignoreCase)
			{
				return s.GetCaseInsensitiveHashCode();
			}
			return s.GetHashCode();
		}

		// Token: 0x040005B2 RID: 1458
		private readonly bool _ignoreCase;
	}
}
