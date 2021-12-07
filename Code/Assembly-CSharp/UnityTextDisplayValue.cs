using System;
using CatlikeCoding.TextBox;
using UnityEngine;

// Token: 0x02000626 RID: 1574
public class UnityTextDisplayValue : MonoBehaviour
{
	// Token: 0x060026D2 RID: 9938 RVA: 0x000A9AD2 File Offset: 0x000A7CD2
	public void Awake()
	{
		this.m_textMesh = base.GetComponent<TextMesh>();
		this.m_textBox = base.GetComponent<TextBox>();
	}

	// Token: 0x060026D3 RID: 9939 RVA: 0x000A9AEC File Offset: 0x000A7CEC
	public void OnEnable()
	{
		if (this.m_textMesh)
		{
			this.m_textMesh.text = string.Empty;
		}
		if (this.m_textBox)
		{
			this.m_textBox.SetText(string.Empty);
			this.m_textBox.RenderText();
		}
		this.m_changeDetector.RecentValue = -99999f;
	}

	// Token: 0x060026D4 RID: 9940 RVA: 0x000A9B54 File Offset: 0x000A7D54
	public void FixedUpdate()
	{
		float num = this.Value.GetFloatValue();
		if (this.Round)
		{
			num = (float)Mathf.RoundToInt(num);
		}
		if (this.m_changeDetector.CheckValueChanged(num))
		{
			if (this.m_textMesh)
			{
				this.m_textMesh.text = this.m_changeDetector.RecentValue.ToString();
			}
			if (this.m_textBox)
			{
				this.m_textBox.SetText(this.m_changeDetector.RecentValue.ToString());
				this.m_textBox.RenderText();
			}
		}
	}

	// Token: 0x04002168 RID: 8552
	public FloatValueProvider Value;

	// Token: 0x04002169 RID: 8553
	private TextMesh m_textMesh;

	// Token: 0x0400216A RID: 8554
	private TextBox m_textBox;

	// Token: 0x0400216B RID: 8555
	public bool Round = true;

	// Token: 0x0400216C RID: 8556
	private readonly ChangeDetectorFloat m_changeDetector = new ChangeDetectorFloat();
}
