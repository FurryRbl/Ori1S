using System;
using Game;
using UnityEngine;

// Token: 0x020005DB RID: 1499
[RequireComponent(typeof(Rigidbody))]
[ExecuteInEditMode]
public class AttachToRope : SaveSerialize
{
	// Token: 0x17000614 RID: 1556
	// (get) Token: 0x060025BA RID: 9658 RVA: 0x000A5411 File Offset: 0x000A3611
	public ConfigurableJoint GeneratedConfigurableJoint
	{
		get
		{
			return this.m_generatedConfigurableJoint;
		}
	}

	// Token: 0x060025BB RID: 9659 RVA: 0x000A5419 File Offset: 0x000A3619
	private bool IsAttached()
	{
		return this.m_generatedConfigurableJoint != null;
	}

	// Token: 0x060025BC RID: 9660 RVA: 0x000A5428 File Offset: 0x000A3628
	public override void Awake()
	{
		base.Awake();
		if (Application.isPlaying)
		{
			Events.Scheduler.OnGameSerializeLoad.Add(new Action(this.OnGameSerializeLoad));
			Events.Scheduler.OnSceneStartLateAfterSerialize.Add(new Action<SceneRoot>(this.OnSceneStartLateAfterSerialize));
		}
	}

	// Token: 0x060025BD RID: 9661 RVA: 0x000A547C File Offset: 0x000A367C
	public void PerformAttachment()
	{
		if (!Application.isPlaying || this.UseDebug)
		{
		}
		if (this.RopeToAttachTo == null)
		{
			return;
		}
		if (this.m_generatedConfigurableJoint == null)
		{
			this.m_generatedConfigurableJoint = base.gameObject.AddComponent<ConfigurableJoint>();
			this.m_generatedConfigurableJoint.axis = new Vector3(1f, 0f, 0f);
			this.m_generatedConfigurableJoint.xMotion = ConfigurableJointMotion.Locked;
			this.m_generatedConfigurableJoint.yMotion = ConfigurableJointMotion.Locked;
			this.m_generatedConfigurableJoint.zMotion = ConfigurableJointMotion.Locked;
			this.m_generatedConfigurableJoint.angularXMotion = ConfigurableJointMotion.Locked;
			this.m_generatedConfigurableJoint.angularYMotion = ConfigurableJointMotion.Locked;
			this.m_generatedConfigurableJoint.angularZMotion = ConfigurableJointMotion.Locked;
		}
		if (this.AllowRotation)
		{
			this.m_generatedConfigurableJoint.angularZMotion = ConfigurableJointMotion.Limited;
			SoftJointLimit angularZLimit = this.m_generatedConfigurableJoint.angularZLimit;
			SoftJointLimitSpring angularYZLimitSpring = this.m_generatedConfigurableJoint.angularYZLimitSpring;
			angularYZLimitSpring.spring = this.RotationSpring;
			angularYZLimitSpring.damper = this.RotationDampening;
			this.m_generatedConfigurableJoint.angularYZLimitSpring = angularYZLimitSpring;
			angularZLimit.limit = 0.01f;
			this.m_generatedConfigurableJoint.angularZLimit = angularZLimit;
		}
		else
		{
			this.m_generatedConfigurableJoint.angularZMotion = ConfigurableJointMotion.Locked;
		}
		float num = 9999999f;
		Transform transform = null;
		Vector3 b = base.transform.TransformPoint(this.AttachmentPointOffset);
		foreach (Transform transform2 in this.RopeToAttachTo.Links)
		{
			if (!(transform2 == null))
			{
				float magnitude = (transform2.position - b).magnitude;
				if (magnitude < num)
				{
					num = magnitude;
					transform = transform2;
				}
			}
		}
		if (transform != null)
		{
			this.m_generatedConfigurableJoint.connectedBody = transform.GetComponent<Rigidbody>();
			this.m_generatedConfigurableJoint.anchor = this.AttachmentPointOffset;
		}
	}

	// Token: 0x060025BE RID: 9662 RVA: 0x000A5688 File Offset: 0x000A3888
	public void BreakAttachment()
	{
		if (!this.IsAttached())
		{
			return;
		}
		UnityEngine.Object.DestroyObject(this.m_generatedConfigurableJoint);
		this.m_generatedConfigurableJoint = null;
	}

	// Token: 0x060025BF RID: 9663 RVA: 0x000A56A8 File Offset: 0x000A38A8
	public void Start()
	{
		if (this.m_generatedConfigurableJoint == null)
		{
			this.PerformAttachment();
		}
		if (Application.isPlaying && this.m_generatedConfigurableJoint)
		{
			this.m_generatedConfigurableJoint.autoConfigureConnectedAnchor = false;
		}
	}

	// Token: 0x060025C0 RID: 9664 RVA: 0x000A56E8 File Offset: 0x000A38E8
	public void RemoveAllJoints()
	{
		foreach (FixedJoint obj in base.GetComponents<FixedJoint>())
		{
			UnityEngine.Object.DestroyImmediate(obj);
		}
	}

	// Token: 0x060025C1 RID: 9665 RVA: 0x000A571C File Offset: 0x000A391C
	public override void OnDestroy()
	{
		base.OnDestroy();
		if (Application.isPlaying)
		{
			Events.Scheduler.OnGameSerializeLoad.Remove(new Action(this.OnGameSerializeLoad));
			Events.Scheduler.OnSceneStartLateAfterSerialize.Remove(new Action<SceneRoot>(this.OnSceneStartLateAfterSerialize));
		}
	}

	// Token: 0x060025C2 RID: 9666 RVA: 0x000A5770 File Offset: 0x000A3970
	public override void Serialize(Archive a)
	{
		if (this.UseDebug)
		{
		}
		if (a.Writing)
		{
			a.Serialize(this.IsAttached());
		}
		else
		{
			this.m_shouldBeAttachedAfterLoading = this.IsAttached();
			a.Serialize(ref this.m_shouldBeAttachedAfterLoading);
		}
	}

	// Token: 0x060025C3 RID: 9667 RVA: 0x000A57BD File Offset: 0x000A39BD
	private void OnGameSerializeLoad()
	{
		if (this.UseDebug)
		{
		}
		this.OnSerializationLoad();
	}

	// Token: 0x060025C4 RID: 9668 RVA: 0x000A57D0 File Offset: 0x000A39D0
	private void OnSceneStartLateAfterSerialize(SceneRoot root)
	{
		if (root == SceneRoot.FindFromTransform(base.transform))
		{
			if (this.UseDebug)
			{
			}
			this.OnSerializationLoad();
		}
	}

	// Token: 0x060025C5 RID: 9669 RVA: 0x000A57F9 File Offset: 0x000A39F9
	private void OnSerializationLoad()
	{
		if (this.m_shouldBeAttachedAfterLoading)
		{
			if (!this.IsAttached())
			{
				this.PerformAttachment();
			}
		}
		else if (this.IsAttached())
		{
			this.BreakAttachment();
		}
	}

	// Token: 0x04002052 RID: 8274
	public Rope RopeToAttachTo;

	// Token: 0x04002053 RID: 8275
	public Vector3 AttachmentPointOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x04002054 RID: 8276
	public bool UseDebug;

	// Token: 0x04002055 RID: 8277
	public bool AllowRotation;

	// Token: 0x04002056 RID: 8278
	public float RotationSpring = 50f;

	// Token: 0x04002057 RID: 8279
	public float RotationDampening = 0.2f;

	// Token: 0x04002058 RID: 8280
	[SerializeField]
	private ConfigurableJoint m_generatedConfigurableJoint;

	// Token: 0x04002059 RID: 8281
	private bool m_shouldBeAttachedAfterLoading = true;
}
