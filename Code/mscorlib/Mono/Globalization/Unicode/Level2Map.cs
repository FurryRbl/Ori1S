using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200007D RID: 125
	internal class Level2Map
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00018088 File Offset: 0x00016288
		public Level2Map(byte source, byte replace)
		{
			this.Source = source;
			this.Replace = replace;
		}

		// Token: 0x04000126 RID: 294
		public byte Source;

		// Token: 0x04000127 RID: 295
		public byte Replace;
	}
}
