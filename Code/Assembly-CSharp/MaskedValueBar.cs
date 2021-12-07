using System;
using UnityEngine;

// Token: 0x02000611 RID: 1553
public class MaskedValueBar : MonoBehaviour
{
	// Token: 0x060026A6 RID: 9894 RVA: 0x000A96FE File Offset: 0x000A78FE
	public void Awake()
	{
		this.m_renderer = base.GetComponent<Renderer>();
	}

	// Token: 0x060026A7 RID: 9895 RVA: 0x000A970C File Offset: 0x000A790C
	public void FixedUpdate()
	{
		if (this.m_changeDetector.CheckValueChanged(this.Value.GetFloatValue()) || !this.m_hasRun)
		{
			this.m_hasRun = true;
			this.m_renderer.material.SetTextureOffset(this.Property, new Vector2(0.5f - Mathf.Lerp(this.MinX, this.MaxX, this.Value.GetFloatValue()) * 0.5f, 1f));
		}
	}

	// Token: 0x04002156 RID: 8534
	private Renderer m_renderer;

	// Token: 0x04002157 RID: 8535
	public FloatValueProvider Value;

	// Token: 0x04002158 RID: 8536
	public float MinX;

	// Token: 0x04002159 RID: 8537
	public float MaxX;

	// Token: 0x0400215A RID: 8538
	private readonly ChangeDetectorFloat m_changeDetector = new ChangeDetectorFloat();

	// Token: 0x0400215B RID: 8539
	private bool m_hasRun;

	// Token: 0x0400215C RID: 8540
	public string Property = "_MaskTexture";
}
