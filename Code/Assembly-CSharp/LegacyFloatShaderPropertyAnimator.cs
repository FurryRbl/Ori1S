using System;
using UnityEngine;

// Token: 0x020003AA RID: 938
public class LegacyFloatShaderPropertyAnimator : LegacyAnimator
{
	// Token: 0x06001A3E RID: 6718 RVA: 0x00070D9C File Offset: 0x0006EF9C
	public override void Awake()
	{
		base.Awake();
		if (this.AnimateVectorXYAsFloat)
		{
			this.m_originalValueVector = base.GetComponent<Renderer>().sharedMaterial.GetVector(this.PropertyName);
		}
		else
		{
			this.m_originalValue = base.GetComponent<Renderer>().sharedMaterial.GetFloat(this.PropertyName);
		}
		if (!base.GetComponent<Renderer>().sharedMaterial.HasProperty(this.PropertyName))
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001A3F RID: 6719 RVA: 0x00070E1C File Offset: 0x0006F01C
	protected override void AnimateIt(float value)
	{
		if (this.AnimateVectorXYAsFloat)
		{
			UberShaderAPI.SetVector(base.GetComponent<Renderer>(), new Vector4(value, value, this.m_originalValueVector.z, this.m_originalValueVector.w), this.PropertyName, !this.IsInScene);
		}
		else
		{
			UberShaderAPI.SetFloat(base.GetComponent<Renderer>(), value, this.PropertyName, !this.IsInScene);
		}
	}

	// Token: 0x06001A40 RID: 6720 RVA: 0x00070E8C File Offset: 0x0006F08C
	public override void RestoreToOriginalState()
	{
		if (this.AnimateVectorXYAsFloat)
		{
			UberShaderAPI.SetVector(base.GetComponent<Renderer>(), this.m_originalValueVector, this.PropertyName, !this.IsInScene);
		}
		else
		{
			UberShaderAPI.SetFloat(base.GetComponent<Renderer>(), this.m_originalValue, this.PropertyName, !this.IsInScene);
		}
	}

	// Token: 0x040016A6 RID: 5798
	public string PropertyName = "_PropertyName";

	// Token: 0x040016A7 RID: 5799
	private float m_originalValue;

	// Token: 0x040016A8 RID: 5800
	private Vector4 m_originalValueVector;

	// Token: 0x040016A9 RID: 5801
	public bool AnimateVectorXYAsFloat;
}
