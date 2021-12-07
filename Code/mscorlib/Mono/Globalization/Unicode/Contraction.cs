using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200007B RID: 123
	internal class Contraction
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x00017FDC File Offset: 0x000161DC
		public Contraction(char[] source, string replacement, byte[] sortkey)
		{
			this.Source = source;
			this.Replacement = replacement;
			this.SortKey = sortkey;
		}

		// Token: 0x04000122 RID: 290
		public readonly char[] Source;

		// Token: 0x04000123 RID: 291
		public readonly string Replacement;

		// Token: 0x04000124 RID: 292
		public readonly byte[] SortKey;
	}
}
