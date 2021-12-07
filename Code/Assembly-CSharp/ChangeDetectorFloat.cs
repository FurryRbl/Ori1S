using System;

// Token: 0x02000535 RID: 1333
public class ChangeDetectorFloat
{
	// Token: 0x0600232D RID: 9005 RVA: 0x0009A1C7 File Offset: 0x000983C7
	public bool CheckValueChanged(float t)
	{
		if (this.RecentValue == t)
		{
			return false;
		}
		this.RecentValue = t;
		return true;
	}

	// Token: 0x04001DA1 RID: 7585
	public float RecentValue;
}
