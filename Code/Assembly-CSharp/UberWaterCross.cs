using System;
using UnityEngine;

// Token: 0x02000857 RID: 2135
[ExecuteInEditMode]
public class UberWaterCross : UberWaterComponent
{
	// Token: 0x06003065 RID: 12389 RVA: 0x000CD23D File Offset: 0x000CB43D
	private void OnEnable()
	{
		this.m_renderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06003066 RID: 12390 RVA: 0x000CD24C File Offset: 0x000CB44C
	public override void GenerateMesh()
	{
		this.IsVertical = true;
		base.StartMesh(base.ResX + 3);
		float num = Mathf.Clamp01(this.DistortSize / base.Control.transform.localScale.y);
		float num2 = base.Control.ExtendLeft / base.Control.transform.localScale.x;
		float num3 = base.Control.ExtendRight / base.Control.transform.localScale.x;
		base.AppendQuadStrip(new Vector2(-0.5f, -num), new Vector2(0.5f, 0f), new Vector2(0f, 0f), new Vector2(1f, 1f), base.ResX, 1);
		base.AppendQuad(new Vector2(-0.5f, -1f), new Vector2(0.5f, -num), new Vector2(0f, 0f), new Vector2(1f, 0f));
		base.AppendQuad(new Vector2(-0.5f - num2, -1f), new Vector2(-0.5f, 0f), new Vector2(-num2, 0f), new Vector2(0f, 0f));
		base.AppendQuad(new Vector2(0.5f, -1f), new Vector2(0.5f + num3, 0f), new Vector2(1f, 0f), new Vector2(1f + num3, 0f));
		base.CreateMesh(base.Filter.sharedMesh, true);
	}

	// Token: 0x06003067 RID: 12391 RVA: 0x000CD400 File Offset: 0x000CB600
	private void OnWillRenderObject()
	{
		if (UberPostProcess.Instance != null)
		{
			base.Control.BindShaderVariablesToMaterial(this.m_renderer.sharedMaterial);
			UberPostProcess.Instance.QueueGrabPass(base.Filter);
		}
	}

	// Token: 0x06003068 RID: 12392 RVA: 0x000CD443 File Offset: 0x000CB643
	private void Update()
	{
	}

	// Token: 0x04002BB9 RID: 11193
	[Range(0.05f, 10f)]
	public float DistortSize = 0.5f;

	// Token: 0x04002BBA RID: 11194
	private Renderer m_renderer;
}
