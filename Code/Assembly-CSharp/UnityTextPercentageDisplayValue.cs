using System;
using UnityEngine;

// Token: 0x02000627 RID: 1575
public class UnityTextPercentageDisplayValue : MonoBehaviour
{
	// Token: 0x060026D6 RID: 9942 RVA: 0x000A9C05 File Offset: 0x000A7E05
	public void Awake()
	{
		this.m_textMesh = base.GetComponent<TextMesh>();
	}

	// Token: 0x060026D7 RID: 9943 RVA: 0x000A9C14 File Offset: 0x000A7E14
	public void FixedUpdate()
	{
		float floatValue = this.Value.GetFloatValue();
		if (this.m_changeDetector.CheckValueChanged(floatValue))
		{
			this.m_textMesh.text = Mathf.Round(this.m_changeDetector.RecentValue * 100f).ToString() + "%";
		}
	}

	// Token: 0x0400216D RID: 8557
	public FloatValueProvider Value;

	// Token: 0x0400216E RID: 8558
	private TextMesh m_textMesh;

	// Token: 0x0400216F RID: 8559
	private readonly ChangeDetectorFloat m_changeDetector = new ChangeDetectorFloat();
}
