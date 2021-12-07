using System;
using System.Collections.Generic;

// Token: 0x020008C4 RID: 2244
public class XboxOneStatistics
{
	// Token: 0x06003208 RID: 12808 RVA: 0x000D4337 File Offset: 0x000D2537
	public static bool RequestStatistics(Action<List<XboxOneStatistics.StatisticReading>> callback, Action failure = null)
	{
		return false;
	}

	// Token: 0x020008C5 RID: 2245
	public struct StatisticReading
	{
		// Token: 0x06003209 RID: 12809 RVA: 0x000D433A File Offset: 0x000D253A
		public StatisticReading(string name, string type, string value)
		{
			this.Name = name;
			this.Type = type;
			this.Value = value;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x000D4351 File Offset: 0x000D2551
		public override string ToString()
		{
			return string.Format("{0} {1} = {2};", this.Type, this.Name, this.Value);
		}

		// Token: 0x04002D07 RID: 11527
		public string Name;

		// Token: 0x04002D08 RID: 11528
		public string Type;

		// Token: 0x04002D09 RID: 11529
		public string Value;
	}
}
