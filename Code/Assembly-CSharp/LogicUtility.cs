using System;

// Token: 0x02000286 RID: 646
public class LogicUtility
{
	// Token: 0x06001537 RID: 5431 RVA: 0x0005E7E4 File Offset: 0x0005C9E4
	public static bool Compare(float a, float b, LogicUtility.ComparisonType comparison)
	{
		switch (comparison)
		{
		case LogicUtility.ComparisonType.LessThan:
			return a < b;
		case LogicUtility.ComparisonType.LessThanOrEqual:
			return a <= b;
		case LogicUtility.ComparisonType.GreaterThan:
			return a > b;
		case LogicUtility.ComparisonType.GreaterThanOrEqual:
			return a >= b;
		case LogicUtility.ComparisonType.Equal:
			return a == b;
		case LogicUtility.ComparisonType.NotEqual:
			return a != b;
		default:
			return false;
		}
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x0005E840 File Offset: 0x0005CA40
	public static string GetComparisonNiceName(string a, string b, LogicUtility.ComparisonType comparison)
	{
		switch (comparison)
		{
		case LogicUtility.ComparisonType.LessThan:
			return a + " less than " + b;
		case LogicUtility.ComparisonType.LessThanOrEqual:
			return a + " less than or equal to " + b;
		case LogicUtility.ComparisonType.GreaterThan:
			return a + " greater than " + b;
		case LogicUtility.ComparisonType.GreaterThanOrEqual:
			return a + " greater than or equal to " + b;
		case LogicUtility.ComparisonType.Equal:
			return a + " equal to " + b;
		case LogicUtility.ComparisonType.NotEqual:
			return a + " not equal to " + b;
		default:
			return string.Empty;
		}
	}

	// Token: 0x02000287 RID: 647
	public enum ComparisonType
	{
		// Token: 0x04001263 RID: 4707
		LessThan,
		// Token: 0x04001264 RID: 4708
		LessThanOrEqual,
		// Token: 0x04001265 RID: 4709
		GreaterThan,
		// Token: 0x04001266 RID: 4710
		GreaterThanOrEqual,
		// Token: 0x04001267 RID: 4711
		Equal,
		// Token: 0x04001268 RID: 4712
		NotEqual
	}
}
