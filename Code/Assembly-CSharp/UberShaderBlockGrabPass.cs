using System;
using Game;
using UnityEngine;

// Token: 0x02000826 RID: 2086
[UberShaderOrder(UberShaderOrder.GrabPassBlock)]
[CustomShaderBlock("Screen")]
[ExecuteInEditMode]
public class UberShaderBlockGrabPass : UberShaderBlock, IStrippable
{
	// Token: 0x06002FC8 RID: 12232 RVA: 0x000CABAB File Offset: 0x000C8DAB
	private void OnPoolSpawned()
	{
		this.m_frame = 0;
	}

	// Token: 0x06002FC9 RID: 12233 RVA: 0x000CABB4 File Offset: 0x000C8DB4
	private void Update()
	{
		this.m_frame++;
		if (this.m_frame % 3 == 0)
		{
			if (UI.Cameras.Current)
			{
				Bounds bounds = base.Renderer.bounds;
				this.m_inFrustm = UI.Cameras.Current.Controller.InsideFrustum(bounds);
			}
			else
			{
				this.m_inFrustm = true;
			}
			this.m_inFrustm = true;
		}
		UberPostProcess instance = UberPostProcess.Instance;
		if (instance != null && this.m_inFrustm)
		{
			UberPostProcess.Instance.QueueGrabPass(base.Filter);
		}
		base.Renderer.enabled = false;
	}

	// Token: 0x06002FCA RID: 12234 RVA: 0x000CAC59 File Offset: 0x000C8E59
	public override void SetProperties()
	{
		this.Color.Set("_Color", this);
	}

	// Token: 0x06002FCB RID: 12235 RVA: 0x000CAC6C File Offset: 0x000C8E6C
	public bool DoStrip()
	{
		return false;
	}

	// Token: 0x04002AFD RID: 11005
	public UberShaderColor Color = new UberShaderColor(new Color(0.5f, 0.5f, 0.5f, 0.5f));

	// Token: 0x04002AFE RID: 11006
	public bool OverwriteDistort;

	// Token: 0x04002AFF RID: 11007
	private bool m_inFrustm;

	// Token: 0x04002B00 RID: 11008
	private int m_frame;
}
