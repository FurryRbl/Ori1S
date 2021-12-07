using System;
using Core;
using UnityEngine;

// Token: 0x020009DE RID: 2526
public class RockTreeSetup : SaveSerialize
{
	// Token: 0x060036F6 RID: 14070 RVA: 0x000E6960 File Offset: 0x000E4B60
	public void Start()
	{
		this.m_rockHelperOffset = this.PushPullBlock.transform.position - this.RockHelper.position;
		this.Animation.Play();
	}

	// Token: 0x060036F7 RID: 14071 RVA: 0x000E69A0 File Offset: 0x000E4BA0
	public void FixedUpdate()
	{
		if (this.m_hasTriggered)
		{
			this.m_time += Time.deltaTime;
		}
		else
		{
			if (this.PushPullBlock.IsGrabbed && Core.Input.Left.IsPressed)
			{
				this.m_time += Time.deltaTime;
			}
			if (!this.PushPullBlock.IsGrabbed || Core.Input.Right.IsPressed)
			{
			}
			if (this.m_time > this.TriggerSequenceTime)
			{
				this.m_hasTriggered = true;
				this.Action.Perform(null);
			}
		}
		if (this.m_time < this.ReleaseRockTime)
		{
			this.PushPullBlock.transform.position = this.RockHelper.position + this.m_rockHelperOffset;
		}
		if (this.m_time > this.ReleaseRockTime && this.m_time - Time.deltaTime <= this.ReleaseRockTime && this.PushPullBlock.GetComponent<Rigidbody>().isKinematic)
		{
			this.PushPullBlock.GetComponent<Rigidbody>().isKinematic = false;
		}
		this.Animation["treeFall"].time = this.m_time;
		this.Animation.Sample();
	}

	// Token: 0x060036F8 RID: 14072 RVA: 0x000E6AF0 File Offset: 0x000E4CF0
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_time);
		ar.Serialize(ref this.m_hasTriggered);
		this.PushPullBlock.GetComponent<Rigidbody>().isKinematic = ar.Serialize(this.PushPullBlock.GetComponent<Rigidbody>().isKinematic);
	}

	// Token: 0x040031DE RID: 12766
	public PushPullBlock PushPullBlock;

	// Token: 0x040031DF RID: 12767
	public Animation Animation;

	// Token: 0x040031E0 RID: 12768
	public Transform RockHelper;

	// Token: 0x040031E1 RID: 12769
	public Vector3 m_rockHelperOffset;

	// Token: 0x040031E2 RID: 12770
	private float m_time;

	// Token: 0x040031E3 RID: 12771
	public float TriggerSequenceTime = 0.9f;

	// Token: 0x040031E4 RID: 12772
	private bool m_hasTriggered;

	// Token: 0x040031E5 RID: 12773
	public ActionMethod Action;

	// Token: 0x040031E6 RID: 12774
	public float ReleaseRockTime = 3f;
}
