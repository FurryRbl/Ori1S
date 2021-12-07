using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005D9 RID: 1497
[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class Rope : SaveSerialize, ISuspendable
{
	// Token: 0x06002592 RID: 9618 RVA: 0x000A414C File Offset: 0x000A234C
	public void ResetRopeToOriginalPosition()
	{
		for (int i = 0; i < this.m_linkData.Length; i++)
		{
			Rope.LinkData linkData = this.m_linkData[i];
			linkData.Transform.position = linkData.OriginalPosition;
			if (!linkData.Rigidbody.isKinematic)
			{
				linkData.Rigidbody.velocity = Vector2.zero;
				linkData.Rigidbody.angularVelocity = Vector2.zero;
			}
		}
	}

	// Token: 0x06002593 RID: 9619 RVA: 0x000A41D4 File Offset: 0x000A23D4
	public override void Awake()
	{
		base.Awake();
		if (Application.isPlaying)
		{
			this.m_linkData = new Rope.LinkData[this.Links.Count];
			for (int i = 0; i < this.m_linkData.Length; i++)
			{
				this.m_linkData[i] = new Rope.LinkData
				{
					Rigidbody = this.Links[i].GetComponent<Rigidbody>(),
					Transform = this.Links[i].transform,
					OriginalPosition = this.Links[i].transform.position
				};
			}
			if (!this.IsManagedByPhysicsSystemManager)
			{
				SuspensionManager.Register(this);
			}
		}
	}

	// Token: 0x06002594 RID: 9620 RVA: 0x000A4298 File Offset: 0x000A2498
	public override void OnDestroy()
	{
		base.OnDestroy();
		if (Application.isPlaying && !this.IsManagedByPhysicsSystemManager)
		{
			SuspensionManager.Unregister(this);
		}
	}

	// Token: 0x06002595 RID: 9621 RVA: 0x000A42BB File Offset: 0x000A24BB
	public void Start()
	{
		this.m_lineRenderer = base.GetComponent<LineRenderer>();
		this.m_lineRenderer.useWorldSpace = true;
		this.SetPhysicsSettingsForAllLinks();
	}

	// Token: 0x06002596 RID: 9622 RVA: 0x000A42DC File Offset: 0x000A24DC
	public void SetPhysicsSettingsForAllLinks()
	{
		if (this.Links == null)
		{
			return;
		}
		for (int i = 0; i < this.Links.Count; i++)
		{
			Transform transform = this.Links[i];
			if (!(transform == null))
			{
				Rigidbody component = transform.GetComponent<Rigidbody>();
				if (!(component == null))
				{
					component.mass = this.PhysicsSettings.LinkMass;
					component.drag = this.PhysicsSettings.LinkDrag;
					component.solverIterationCount = this.PhysicsSettings.PhysicsIterationCount;
					ConfigurableJoint component2 = transform.GetComponent<ConfigurableJoint>();
					if (!(component2 == null))
					{
						component2.autoConfigureConnectedAnchor = !Application.isPlaying;
						component2.axis = new Vector3(1f, 0f, 0f);
						component2.xMotion = ConfigurableJointMotion.Locked;
						component2.yMotion = ConfigurableJointMotion.Limited;
						component2.zMotion = ConfigurableJointMotion.Locked;
						component2.angularXMotion = ConfigurableJointMotion.Locked;
						component2.angularYMotion = ConfigurableJointMotion.Locked;
						component2.angularZMotion = ConfigurableJointMotion.Limited;
						if (transform == this.Links[0])
						{
							if (this.FreeRotationAtTheStart)
							{
								component2.angularZMotion = ConfigurableJointMotion.Free;
							}
							component2.yMotion = ConfigurableJointMotion.Locked;
						}
						SoftJointLimit linearLimit = component2.linearLimit;
						linearLimit.limit = 0.01f;
						component2.linearLimit = linearLimit;
						SoftJointLimit angularZLimit = component2.angularZLimit;
						SoftJointLimitSpring angularYZLimitSpring = component2.angularYZLimitSpring;
						angularYZLimitSpring.spring = this.PhysicsSettings.LengthSpringStiffness;
						angularYZLimitSpring.damper = this.PhysicsSettings.LengthSpringDamping;
						component2.angularYZLimitSpring = angularYZLimitSpring;
						angularZLimit.limit = 0.01f;
						component2.angularZLimit = angularZLimit;
						JointDrive yDrive = component2.yDrive;
						yDrive.mode = JointDriveMode.None;
						component2.yDrive = yDrive;
					}
				}
			}
		}
	}

	// Token: 0x06002597 RID: 9623 RVA: 0x000A4498 File Offset: 0x000A2698
	private void Update()
	{
		if (this.IsSuspended)
		{
			return;
		}
		for (int i = 0; i < this.Links.Count; i++)
		{
			this.m_lineRenderer.SetPosition(i, this.Links[i].position);
		}
	}

	// Token: 0x06002598 RID: 9624 RVA: 0x000A44EC File Offset: 0x000A26EC
	public override void Serialize(Archive ar)
	{
		if (this.IsManagedByPhysicsSystemManager)
		{
			return;
		}
		for (int i = 0; i < this.m_linkData.Length; i++)
		{
			Rope.LinkData linkData = this.m_linkData[i];
			if (!linkData.Rigidbody.isKinematic)
			{
				linkData.Rigidbody.velocity = ar.Serialize(linkData.Rigidbody.velocity);
				linkData.Rigidbody.angularVelocity = ar.Serialize(linkData.Rigidbody.angularVelocity);
			}
			linkData.Transform.position = ar.Serialize(linkData.Transform.position);
			linkData.Transform.rotation = ar.Serialize(linkData.Transform.rotation);
		}
	}

	// Token: 0x06002599 RID: 9625 RVA: 0x000A45BC File Offset: 0x000A27BC
	public void Suspend()
	{
		for (int i = 0; i < this.m_linkData.Length; i++)
		{
			Rope.LinkData linkData = this.m_linkData[i];
			if (linkData.Rigidbody && !linkData.Rigidbody.isKinematic)
			{
				linkData.Velocity = linkData.Rigidbody.velocity;
				linkData.AngularVelocity = linkData.Rigidbody.angularVelocity;
				linkData.Rigidbody.velocity = Vector3.zero;
				linkData.Rigidbody.angularVelocity = Vector3.zero;
				linkData.Rigidbody.Sleep();
			}
			this.m_linkData[i] = linkData;
		}
	}

	// Token: 0x0600259A RID: 9626 RVA: 0x000A467C File Offset: 0x000A287C
	public void Resume()
	{
		for (int i = 0; i < this.m_linkData.Length; i++)
		{
			Rope.LinkData linkData = this.m_linkData[i];
			if (linkData.Rigidbody && !linkData.Rigidbody.isKinematic)
			{
				linkData.Rigidbody.velocity = linkData.Velocity;
				linkData.Rigidbody.angularVelocity = linkData.AngularVelocity;
				linkData.Rigidbody.WakeUp();
			}
		}
	}

	// Token: 0x1700060F RID: 1551
	// (get) Token: 0x0600259B RID: 9627 RVA: 0x000A4708 File Offset: 0x000A2908
	// (set) Token: 0x0600259C RID: 9628 RVA: 0x000A4710 File Offset: 0x000A2910
	public bool IsSuspended
	{
		get
		{
			return this.m_isSuspended;
		}
		set
		{
			if (value)
			{
				this.Suspend();
			}
			else
			{
				this.Resume();
			}
			this.m_isSuspended = value;
		}
	}

	// Token: 0x0400203B RID: 8251
	public static Color ROPE_END_LINK_COLOR = new Color(0f, 0.8f, 0f, 0.6f);

	// Token: 0x0400203C RID: 8252
	public static Color ROPE_INTERMEDIATE_LINK_COLOR = new Color(1f, 0.93f, 0f, 0.3f);

	// Token: 0x0400203D RID: 8253
	public bool IsManagedByPhysicsSystemManager;

	// Token: 0x0400203E RID: 8254
	public float RopeWidth = 1f;

	// Token: 0x0400203F RID: 8255
	public float LinkDensity = 0.3333f;

	// Token: 0x04002040 RID: 8256
	public RopePhysicsSettings PhysicsSettings;

	// Token: 0x04002041 RID: 8257
	public List<Transform> Links = new List<Transform>();

	// Token: 0x04002042 RID: 8258
	public bool FreeRotationAtTheStart = true;

	// Token: 0x04002043 RID: 8259
	private LineRenderer m_lineRenderer;

	// Token: 0x04002044 RID: 8260
	private Rope.LinkData[] m_linkData;

	// Token: 0x04002045 RID: 8261
	private bool m_isSuspended;

	// Token: 0x020006D8 RID: 1752
	public struct LinkData
	{
		// Token: 0x04002574 RID: 9588
		public Vector3 AngularVelocity;

		// Token: 0x04002575 RID: 9589
		public Vector3 Position;

		// Token: 0x04002576 RID: 9590
		public Rigidbody Rigidbody;

		// Token: 0x04002577 RID: 9591
		public Quaternion Rotation;

		// Token: 0x04002578 RID: 9592
		public Transform Transform;

		// Token: 0x04002579 RID: 9593
		public Vector3 Velocity;

		// Token: 0x0400257A RID: 9594
		public Vector3 OriginalPosition;
	}
}
