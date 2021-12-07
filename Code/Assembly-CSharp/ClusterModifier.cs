using System;
using UnityEngine;

// Token: 0x020007E9 RID: 2025
[ExecuteInEditMode]
public class ClusterModifier : UberShaderModifier, IDynamicGraphic
{
	// Token: 0x17000775 RID: 1909
	// (get) Token: 0x06002E7C RID: 11900 RVA: 0x000C520F File Offset: 0x000C340F
	protected virtual string BoneName
	{
		get
		{
			return "Base";
		}
	}

	// Token: 0x17000776 RID: 1910
	// (get) Token: 0x06002E7D RID: 11901 RVA: 0x000C5216 File Offset: 0x000C3416
	protected virtual bool ControlMask
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06002E7E RID: 11902 RVA: 0x000C521C File Offset: 0x000C341C
	private void OnEnable()
	{
		this.m_varsId = Shader.PropertyToID("_ClusterAnimVars" + this.BoneName);
		this.m_pivotId = Shader.PropertyToID("_ClusterAnimPivot" + this.BoneName);
	}

	// Token: 0x06002E7F RID: 11903 RVA: 0x000C5260 File Offset: 0x000C3460
	public void Update()
	{
		if (this.Handle == null || !base.Renderer.isVisible)
		{
			return;
		}
		if (this.Handle.hasChanged || !Application.isPlaying)
		{
			this.RebindClusterTransform();
			this.m_didRebind = true;
		}
	}

	// Token: 0x06002E80 RID: 11904 RVA: 0x000C52B6 File Offset: 0x000C34B6
	private void LateUpdate()
	{
		if (this.m_didRebind)
		{
			this.Handle.hasChanged = false;
			this.m_didRebind = false;
		}
	}

	// Token: 0x06002E81 RID: 11905 RVA: 0x000C52D8 File Offset: 0x000C34D8
	private void RebindClusterTransform()
	{
		Vector3 vector;
		Vector2 vector2;
		if (this.Handle == null)
		{
			vector = Vector3.zero;
			vector2 = Vector2.zero;
		}
		else
		{
			vector = Quaternion.Inverse(base.transform.rotation) * (this.Handle.position - this.m_posOffset - base.transform.position);
			vector2 = base.transform.InverseTransformPoint(base.transform.position + this.m_posOffset);
			Vector3 lossyScale = base.transform.lossyScale;
			vector2.x *= lossyScale.x;
			vector2.y *= lossyScale.y;
		}
		float num;
		float num2;
		if (this.Handle == null)
		{
			num = 0f;
			num2 = 0f;
		}
		else
		{
			num = base.transform.rotation.eulerAngles.z;
			num2 = this.Handle.rotation.eulerAngles.z;
			if (num2 > 180f)
			{
				num2 -= 360f;
			}
		}
		float num3 = (num2 - this.m_rotOffset - num) * 0.017453292f * this.RotationWeight;
		num3 -= 6.2831855f * Mathf.Floor((num3 + 3.1415927f) / 6.2831855f);
		base.BindMaterial.SetVector(this.m_varsId, new Vector4(vector.x, vector.y, num3, 0f));
		base.BindMaterial.SetVector(this.m_pivotId, new Vector4(vector2.x, vector2.y, 0f, 0f));
	}

	// Token: 0x06002E82 RID: 11906 RVA: 0x000C54A8 File Offset: 0x000C36A8
	public override void SetProperties()
	{
		this.Mask.Set("_ClusterAnimMask" + this.BoneName, base.AttachedToShaderBlock);
		this.Mask.IsVertexTexture = true;
	}

	// Token: 0x06002E83 RID: 11907 RVA: 0x000C54E4 File Offset: 0x000C36E4
	public void CalibrateHandle()
	{
		this.m_posOffset = this.Handle.position - base.transform.position;
		this.m_rotOffset = this.Handle.rotation.eulerAngles.z - base.transform.rotation.eulerAngles.z;
	}

	// Token: 0x06002E84 RID: 11908 RVA: 0x000C554F File Offset: 0x000C374F
	public override bool RequiresVertexColor()
	{
		return true;
	}

	// Token: 0x06002E85 RID: 11909 RVA: 0x000C5552 File Offset: 0x000C3752
	public override bool DoStrip()
	{
		return false;
	}

	// Token: 0x040029BF RID: 10687
	public UberShaderTexture Mask = new UberShaderTexture();

	// Token: 0x040029C0 RID: 10688
	public Transform Handle;

	// Token: 0x040029C1 RID: 10689
	[SerializeField]
	private Vector3 m_posOffset;

	// Token: 0x040029C2 RID: 10690
	[SerializeField]
	private float m_rotOffset;

	// Token: 0x040029C3 RID: 10691
	[Range(0f, 1f)]
	public float RotationWeight = 1f;

	// Token: 0x040029C4 RID: 10692
	private bool m_didRebind;

	// Token: 0x040029C5 RID: 10693
	private int m_varsId;

	// Token: 0x040029C6 RID: 10694
	private int m_pivotId;
}
