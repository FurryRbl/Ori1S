using System;
using UnityEngine;

// Token: 0x02000422 RID: 1058
public class CharacterGlow : MonoBehaviour
{
	// Token: 0x06001D87 RID: 7559 RVA: 0x00081BA0 File Offset: 0x0007FDA0
	public void Start()
	{
		this.m_parent = base.transform.parent;
		this.m_localPosition = base.transform.localPosition;
		this.m_localRotation = base.transform.localRotation;
		base.transform.parent = null;
	}

	// Token: 0x06001D88 RID: 7560 RVA: 0x00081BEC File Offset: 0x0007FDEC
	public void FixedUpdate()
	{
		if (this.Renderer && this.m_parent)
		{
			base.transform.position = this.m_parent.position + this.m_localRotation * this.m_localPosition;
			base.transform.rotation = this.m_parent.rotation * this.m_localRotation;
		}
		if (this.Renderer == null || !this.Renderer.enabled || !this.Renderer.gameObject.activeInHierarchy)
		{
			this.Animator.AnimatorDriver.ContinueBackwards();
		}
		else
		{
			this.Animator.AnimatorDriver.ContinueForward();
		}
		if (this.Animator.AnimatorDriver.CurrentTime == 0f && this.Renderer == null)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001980 RID: 6528
	public TransparencyAnimator Animator;

	// Token: 0x04001981 RID: 6529
	public Renderer Renderer;

	// Token: 0x04001982 RID: 6530
	private Transform m_parent;

	// Token: 0x04001983 RID: 6531
	private Vector3 m_localPosition;

	// Token: 0x04001984 RID: 6532
	private Quaternion m_localRotation;
}
