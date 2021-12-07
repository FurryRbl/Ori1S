using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x020002F2 RID: 754
	public struct Range
	{
		// Token: 0x060026BE RID: 9918 RVA: 0x000363D4 File Offset: 0x000345D4
		public Range(int fromValue, int valueCount)
		{
			this.from = fromValue;
			this.count = valueCount;
		}

		// Token: 0x04000BE9 RID: 3049
		public int from;

		// Token: 0x04000BEA RID: 3050
		public int count;
	}
}
