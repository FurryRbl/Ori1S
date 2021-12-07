using System;
using UnityEngine;

// Token: 0x020003B5 RID: 949
public class LegacyScaleAnimator : LegacyAnimator, IPooled
{
	// Token: 0x06001A71 RID: 6769 RVA: 0x00071CD0 File Offset: 0x0006FED0
	public override void Awake()
	{
		base.Awake();
		if (this.Target)
		{
			this.m_transform = this.Target.transform;
			this.m_originalScale = this.Target.transform.localScale;
		}
		else
		{
			this.m_transform = base.transform;
			this.m_originalScale = this.m_transform.localScale;
		}
		this.m_renderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06001A72 RID: 6770 RVA: 0x00071D48 File Offset: 0x0006FF48
	protected override void AnimateIt(float value)
	{
		this.m_transform.localScale = new Vector3((this.ScaleAxisFilter.x != 1f) ? this.m_transform.localScale.x : (this.m_originalScale.x * value), (this.ScaleAxisFilter.y != 1f) ? this.m_transform.localScale.y : (this.m_originalScale.y * value), (this.ScaleAxisFilter.z != 1f) ? this.m_transform.localScale.z : (this.m_originalScale.z * value));
		if (this.m_renderer != null)
		{
			this.m_renderer.enabled = (value > 0.01f);
		}
	}

	// Token: 0x06001A73 RID: 6771 RVA: 0x00071E37 File Offset: 0x00070037
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(1f);
	}

	// Token: 0x040016E0 RID: 5856
	public GameObject Target;

	// Token: 0x040016E1 RID: 5857
	public Vector3 ScaleAxisFilter = new Vector3(1f, 1f, 1f);

	// Token: 0x040016E2 RID: 5858
	private Transform m_transform;

	// Token: 0x040016E3 RID: 5859
	private Vector3 m_originalScale;

	// Token: 0x040016E4 RID: 5860
	private Renderer m_renderer;
}
