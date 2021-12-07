using System;
using UnityEngine;

// Token: 0x020001ED RID: 493
public class TextMeshFloatSetter : MonoBehaviour
{
	// Token: 0x060010EE RID: 4334 RVA: 0x0004D504 File Offset: 0x0004B704
	public void Start()
	{
		this.m_textMesh = base.GetComponent<TextMesh>();
		this.m_textMesh.text = "0";
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x0004D524 File Offset: 0x0004B724
	public void FixedUpdate()
	{
		float floatValue = this.FloatValueProvider.GetFloatValue();
		if (this.m_lastValue != floatValue)
		{
			this.m_lastValue = floatValue;
			this.m_textMesh.text = floatValue.ToString();
		}
	}

	// Token: 0x04000EAD RID: 3757
	public FloatValueProvider FloatValueProvider;

	// Token: 0x04000EAE RID: 3758
	private float m_lastValue;

	// Token: 0x04000EAF RID: 3759
	private TextMesh m_textMesh;
}
