using System;
using UnityEngine;

// Token: 0x02000878 RID: 2168
public class AreaMapScrollLimit : MonoBehaviour
{
	// Token: 0x170007CC RID: 1996
	// (get) Token: 0x060030FA RID: 12538 RVA: 0x000D0B57 File Offset: 0x000CED57
	public bool Active
	{
		get
		{
			return !this.Condition || this.Condition.Validate(null);
		}
	}

	// Token: 0x170007CD RID: 1997
	// (get) Token: 0x060030FB RID: 12539 RVA: 0x000D0B78 File Offset: 0x000CED78
	public Rect Area
	{
		get
		{
			if (!this.m_areaCalculated)
			{
				this.m_areaCalculated = true;
				this.m_area = new Rect
				{
					width = base.transform.lossyScale.x,
					height = base.transform.lossyScale.y,
					center = base.transform.position
				};
			}
			return this.m_area;
		}
	}

	// Token: 0x04002C4F RID: 11343
	public Condition Condition;

	// Token: 0x04002C50 RID: 11344
	private Rect m_area;

	// Token: 0x04002C51 RID: 11345
	private bool m_areaCalculated;
}
