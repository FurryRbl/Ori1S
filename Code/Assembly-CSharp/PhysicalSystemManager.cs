using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005DA RID: 1498
public class PhysicalSystemManager : SaveSerialize, IFrustumOptimizable, ISceneRootPreEnableObserver, ISuspendable
{
	// Token: 0x17000610 RID: 1552
	// (get) Token: 0x0600259E RID: 9630 RVA: 0x000A4758 File Offset: 0x000A2958
	// (set) Token: 0x0600259F RID: 9631 RVA: 0x000A47D4 File Offset: 0x000A29D4
	public Bounds WorldSpaceBoundingBox
	{
		get
		{
			Vector3 size = new Vector3(this.LocalBoundingBox.width, this.LocalBoundingBox.height, 0f);
			Vector3 vector = base.transform.position;
			vector += new Vector3(this.LocalBoundingBox.center.x, this.LocalBoundingBox.center.y, 0f);
			return new Bounds(vector, size);
		}
		set
		{
			Bounds bounds = value;
			Vector3 size = bounds.size;
			this.LocalBoundingBox.x = bounds.min.x - base.transform.position.x;
			this.LocalBoundingBox.y = bounds.min.y - base.transform.position.y;
			this.LocalBoundingBox.width = size.x;
			this.LocalBoundingBox.height = size.y;
		}
	}

	// Token: 0x060025A0 RID: 9632 RVA: 0x000A4870 File Offset: 0x000A2A70
	public void RegisterInstanciatedRigidbody(Rigidbody dynamicRigidbody)
	{
		if (this.m_dynamicRigidBodyStates == null)
		{
			this.m_dynamicRigidBodyStates = new List<RigidbodyState>(3);
		}
		RigidbodyState rigidbodyState = new RigidbodyState();
		rigidbodyState.Rigidbody = dynamicRigidbody;
		if (this.UseLocalCoordinates)
		{
			rigidbodyState.OriginalPosition = dynamicRigidbody.transform.localPosition;
			rigidbodyState.OriginalRotation = dynamicRigidbody.transform.localRotation;
		}
		else
		{
			rigidbodyState.OriginalPosition = dynamicRigidbody.transform.position;
			rigidbodyState.OriginalRotation = dynamicRigidbody.transform.rotation;
		}
		this.m_dynamicRigidBodyStates.Add(rigidbodyState);
	}

	// Token: 0x060025A1 RID: 9633 RVA: 0x000A4904 File Offset: 0x000A2B04
	public void ResetPhysicalSystemToTheOriginalState()
	{
		for (int i = 0; i < this.m_cachedRigidbodyStates.Length; i++)
		{
			this.ResetRigidbodyToInitialState(this.m_cachedRigidbodyStates[i]);
		}
		if (this.m_dynamicRigidBodyStates != null)
		{
			for (int j = 0; j < this.m_dynamicRigidBodyStates.Count; j++)
			{
				this.ResetRigidbodyToInitialState(this.m_dynamicRigidBodyStates[j]);
			}
		}
	}

	// Token: 0x060025A2 RID: 9634 RVA: 0x000A4974 File Offset: 0x000A2B74
	private void ResetRigidbodyToInitialState(RigidbodyState state)
	{
		if (state.Rigidbody == null)
		{
			return;
		}
		if (this.UseLocalCoordinates)
		{
			state.Rigidbody.transform.localPosition = state.OriginalPosition;
			state.Rigidbody.transform.localRotation = state.OriginalRotation;
		}
		else
		{
			state.Rigidbody.transform.position = state.OriginalPosition;
			state.Rigidbody.transform.rotation = state.OriginalRotation;
		}
		if (!state.Rigidbody.isKinematic)
		{
			state.Rigidbody.velocity = Vector2.zero;
			state.Rigidbody.angularVelocity = Vector2.zero;
		}
	}

	// Token: 0x060025A3 RID: 9635 RVA: 0x000A4A38 File Offset: 0x000A2C38
	public void Start()
	{
		this.m_startCalled = true;
		for (int i = 0; i < this.m_cachedJoints.Length; i++)
		{
			ConfigurableJoint configurableJoint = this.m_cachedJoints[i];
			configurableJoint.autoConfigureConnectedAnchor = false;
		}
		if (this.m_loadedBeforeStart)
		{
			this.m_loadedBeforeStart = false;
			this.ResetPhysicalSystemToTheLastState();
		}
		if (this.IsSuspended)
		{
			this.ContinueSleeping();
		}
	}

	// Token: 0x060025A4 RID: 9636 RVA: 0x000A4AA0 File Offset: 0x000A2CA0
	public void OnEnable()
	{
		if (!this.m_loadedBeforeStart)
		{
			this.ResetPhysicalSystemToTheLastState();
			if (this.IsSuspended)
			{
				this.ContinueSleeping();
			}
		}
	}

	// Token: 0x060025A5 RID: 9637 RVA: 0x000A4AD0 File Offset: 0x000A2CD0
	private void ResetPhysicalSystemToTheLastState()
	{
		for (int i = 0; i < this.m_cachedRigidbodyStates.Length; i++)
		{
			RigidbodyState rigidbodyState = this.m_cachedRigidbodyStates[i];
			if (rigidbodyState.WasDisabled && rigidbodyState.Rigidbody)
			{
				this.ResetRigidbodyToLastState(rigidbodyState);
			}
		}
		if (this.m_dynamicRigidBodyStates != null)
		{
			for (int j = 0; j < this.m_dynamicRigidBodyStates.Count; j++)
			{
				RigidbodyState rigidbodyState2 = this.m_dynamicRigidBodyStates[j];
				if (rigidbodyState2.WasDisabled && rigidbodyState2.Rigidbody)
				{
					this.ResetRigidbodyToLastState(rigidbodyState2);
				}
			}
			if (this.IsSuspended)
			{
				this.ContinueSleeping();
			}
		}
	}

	// Token: 0x060025A6 RID: 9638 RVA: 0x000A4B88 File Offset: 0x000A2D88
	private void ResetRigidbodyToLastState(RigidbodyState state)
	{
		if (this.UseLocalCoordinates)
		{
			state.Rigidbody.transform.localPosition = state.LastPosition;
			state.Rigidbody.transform.localRotation = state.LastRotation;
		}
		else
		{
			state.Rigidbody.transform.position = state.LastPosition;
			state.Rigidbody.transform.rotation = state.LastRotation;
		}
	}

	// Token: 0x060025A7 RID: 9639 RVA: 0x000A4C00 File Offset: 0x000A2E00
	public void OnDisable()
	{
		for (int i = 0; i < this.m_cachedRigidbodyStates.Length; i++)
		{
			RigidbodyState rigidbodyState = this.m_cachedRigidbodyStates[i];
			if (rigidbodyState.Rigidbody)
			{
				if (this.UseLocalCoordinates)
				{
					rigidbodyState.LastPosition = rigidbodyState.Rigidbody.transform.localPosition;
					rigidbodyState.LastRotation = rigidbodyState.Rigidbody.transform.localRotation;
				}
				else
				{
					rigidbodyState.LastPosition = rigidbodyState.Rigidbody.transform.position;
					rigidbodyState.LastRotation = rigidbodyState.Rigidbody.transform.rotation;
				}
			}
			rigidbodyState.WasDisabled = true;
			this.ResetRigidbodyToInitialState(rigidbodyState);
		}
		if (this.m_dynamicRigidBodyStates != null)
		{
			for (int j = 0; j < this.m_dynamicRigidBodyStates.Count; j++)
			{
				RigidbodyState rigidbodyState2 = this.m_dynamicRigidBodyStates[j];
				if (rigidbodyState2.Rigidbody)
				{
					if (this.UseLocalCoordinates)
					{
						rigidbodyState2.LastPosition = rigidbodyState2.Rigidbody.transform.localPosition;
						rigidbodyState2.LastRotation = rigidbodyState2.Rigidbody.transform.localRotation;
					}
					else
					{
						rigidbodyState2.LastPosition = rigidbodyState2.Rigidbody.transform.position;
						rigidbodyState2.LastRotation = rigidbodyState2.Rigidbody.transform.rotation;
					}
				}
				rigidbodyState2.WasDisabled = true;
				this.ResetRigidbodyToInitialState(rigidbodyState2);
			}
		}
	}

	// Token: 0x060025A8 RID: 9640 RVA: 0x000A4D70 File Offset: 0x000A2F70
	private void ContinueSleeping()
	{
		for (int i = 0; i < this.m_cachedRigidbodyStates.Length; i++)
		{
			this.PutRigidBodyToSleepNoCaching(this.m_cachedRigidbodyStates[i]);
		}
		if (this.m_dynamicRigidBodyStates != null)
		{
			for (int j = 0; j < this.m_dynamicRigidBodyStates.Count; j++)
			{
				this.PutRigidBodyToSleepNoCaching(this.m_dynamicRigidBodyStates[j]);
			}
		}
	}

	// Token: 0x060025A9 RID: 9641 RVA: 0x000A4DE0 File Offset: 0x000A2FE0
	private void PutRigidBodyToSleepNoCaching(RigidbodyState rigidbodyState)
	{
		if (rigidbodyState.Rigidbody == null)
		{
			return;
		}
		Rigidbody rigidbody = rigidbodyState.Rigidbody;
		if (!rigidbody.isKinematic)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			rigidbody.Sleep();
		}
	}

	// Token: 0x060025AA RID: 9642 RVA: 0x000A4E2D File Offset: 0x000A302D
	public void OnSceneRootPreEnable()
	{
		if (this.m_awakeCalled)
		{
			return;
		}
		this.UpdateCachedBoundingBox();
		this.m_isInsideFrustum = false;
		base.gameObject.SetActive(false);
		CameraFrustumOptimizer.RegisterUninitialized(this);
	}

	// Token: 0x060025AB RID: 9643 RVA: 0x000A4E5A File Offset: 0x000A305A
	public new void Awake()
	{
		this.m_awakeCalled = true;
		base.Awake();
		this.UpdateCachedBoundingBox();
		SuspensionManager.Register(this);
		CameraFrustumOptimizer.Register(this);
	}

	// Token: 0x060025AC RID: 9644 RVA: 0x000A4E7B File Offset: 0x000A307B
	private void UpdateCachedBoundingBox()
	{
		this.m_cachedBoundingBox = this.WorldSpaceBoundingBox;
	}

	// Token: 0x060025AD RID: 9645 RVA: 0x000A4E89 File Offset: 0x000A3089
	public new void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
		CameraFrustumOptimizer.Unregister(this);
	}

	// Token: 0x060025AE RID: 9646 RVA: 0x000A4EA0 File Offset: 0x000A30A0
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.m_loadedBeforeStart = !this.m_startCalled;
		}
		bool activeInHierarchy = base.gameObject.activeInHierarchy;
		for (int i = 0; i < this.m_cachedRigidbodyStates.Length; i++)
		{
			RigidbodyState rigidbodyState = this.m_cachedRigidbodyStates[i];
			Rigidbody rigidbody = rigidbodyState.Rigidbody;
			if ((ar.Writing && !rigidbodyState.WasDisabled) || (ar.Reading && activeInHierarchy))
			{
				if (rigidbody)
				{
					if (rigidbody.isKinematic)
					{
						ar.Serialize(Vector3.zero);
						ar.Serialize(Vector3.zero);
					}
					else
					{
						rigidbody.velocity = ar.Serialize(rigidbody.velocity);
						rigidbody.angularVelocity = ar.Serialize(rigidbody.angularVelocity);
					}
					if (this.UseLocalCoordinates)
					{
						rigidbody.transform.localPosition = ar.Serialize(rigidbody.transform.localPosition);
						rigidbody.transform.localRotation = ar.Serialize(rigidbody.transform.localRotation);
					}
					else
					{
						rigidbody.transform.position = ar.Serialize(rigidbody.transform.position);
						rigidbody.transform.rotation = ar.Serialize(rigidbody.transform.rotation);
					}
				}
				else
				{
					ar.Serialize(Vector3.zero);
					ar.Serialize(Vector3.zero);
					ar.Serialize(Vector3.zero);
					ar.Serialize(Quaternion.identity);
				}
			}
			else if (rigidbody)
			{
				rigidbodyState.Velocity = ar.Serialize(rigidbodyState.Velocity);
				rigidbodyState.AngularVelocity = ar.Serialize(rigidbodyState.AngularVelocity);
				rigidbodyState.LastPosition = ar.Serialize(rigidbodyState.LastPosition);
				rigidbodyState.LastRotation = ar.Serialize(rigidbodyState.LastRotation);
				if (ar.Reading)
				{
					rigidbodyState.WasDisabled = true;
				}
			}
			else
			{
				ar.Serialize(Vector3.zero);
				ar.Serialize(Vector3.zero);
				ar.Serialize(Vector3.zero);
				ar.Serialize(Quaternion.identity);
			}
		}
		if (this.IsSuspended)
		{
			for (int j = 0; j < this.m_cachedRigidbodyStates.Length; j++)
			{
				this.SuspendRigidbody(this.m_cachedRigidbodyStates[j]);
			}
			if (this.m_dynamicRigidBodyStates != null)
			{
				for (int k = 0; k < this.m_dynamicRigidBodyStates.Count; k++)
				{
					this.SuspendRigidbody(this.m_dynamicRigidBodyStates[k]);
				}
			}
		}
	}

	// Token: 0x060025AF RID: 9647 RVA: 0x000A5144 File Offset: 0x000A3344
	public void OnFrustumEnter()
	{
		base.gameObject.SetActive(true);
		this.m_isInsideFrustum = true;
		if (this.m_awakeCalled)
		{
			SuspensionManager.Resume(this);
		}
	}

	// Token: 0x060025B0 RID: 9648 RVA: 0x000A5175 File Offset: 0x000A3375
	public void OnFrustumExit()
	{
		this.m_isInsideFrustum = false;
		if (this.m_awakeCalled)
		{
			SuspensionManager.Suspend(this);
		}
	}

	// Token: 0x17000611 RID: 1553
	// (get) Token: 0x060025B1 RID: 9649 RVA: 0x000A518F File Offset: 0x000A338F
	public bool InsideFrustum
	{
		get
		{
			return this.m_isInsideFrustum;
		}
	}

	// Token: 0x17000612 RID: 1554
	// (get) Token: 0x060025B2 RID: 9650 RVA: 0x000A5197 File Offset: 0x000A3397
	public Bounds Bounds
	{
		get
		{
			return this.m_cachedBoundingBox;
		}
	}

	// Token: 0x17000613 RID: 1555
	// (get) Token: 0x060025B3 RID: 9651 RVA: 0x000A519F File Offset: 0x000A339F
	// (set) Token: 0x060025B4 RID: 9652 RVA: 0x000A51A8 File Offset: 0x000A33A8
	public bool IsSuspended
	{
		get
		{
			return this.m_isSuspended;
		}
		set
		{
			if (this.m_isSuspended != value)
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
	}

	// Token: 0x060025B5 RID: 9653 RVA: 0x000A51E0 File Offset: 0x000A33E0
	private void Suspend()
	{
		if (!this.m_awakeCalled)
		{
			return;
		}
		for (int i = 0; i < this.m_applyTurbulentForces.Length; i++)
		{
			ApplyTurbulentForce suspendable = this.m_applyTurbulentForces[i];
			SuspensionManager.Suspend(suspendable);
		}
		for (int j = 0; j < this.m_cachedRigidbodyStates.Length; j++)
		{
			this.SuspendRigidbody(this.m_cachedRigidbodyStates[j]);
		}
		if (this.m_dynamicRigidBodyStates != null)
		{
			for (int k = 0; k < this.m_dynamicRigidBodyStates.Count; k++)
			{
				this.SuspendRigidbody(this.m_dynamicRigidBodyStates[k]);
			}
		}
	}

	// Token: 0x060025B6 RID: 9654 RVA: 0x000A5284 File Offset: 0x000A3484
	private void Resume()
	{
		if (!this.m_awakeCalled)
		{
			return;
		}
		for (int i = 0; i < this.m_applyTurbulentForces.Length; i++)
		{
			ApplyTurbulentForce suspendable = this.m_applyTurbulentForces[i];
			SuspensionManager.Resume(suspendable);
		}
		for (int j = 0; j < this.m_cachedRigidbodyStates.Length; j++)
		{
			this.ResumeRigidbody(this.m_cachedRigidbodyStates[j]);
		}
		if (this.m_dynamicRigidBodyStates != null)
		{
			for (int k = 0; k < this.m_dynamicRigidBodyStates.Count; k++)
			{
				this.ResumeRigidbody(this.m_dynamicRigidBodyStates[k]);
			}
		}
	}

	// Token: 0x060025B7 RID: 9655 RVA: 0x000A5328 File Offset: 0x000A3528
	private void SuspendRigidbody(RigidbodyState rigidbodyState)
	{
		Rigidbody rigidbody = rigidbodyState.Rigidbody;
		if (rigidbody == null)
		{
			return;
		}
		if (!rigidbody.isKinematic)
		{
			rigidbodyState.Velocity = rigidbody.velocity;
			rigidbodyState.AngularVelocity = rigidbody.angularVelocity;
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			rigidbody.Sleep();
		}
	}

	// Token: 0x060025B8 RID: 9656 RVA: 0x000A5388 File Offset: 0x000A3588
	private void ResumeRigidbody(RigidbodyState rigidbodyState)
	{
		Rigidbody rigidbody = rigidbodyState.Rigidbody;
		if (rigidbody == null)
		{
			return;
		}
		if (!rigidbody.isKinematic)
		{
			rigidbody.velocity = rigidbodyState.Velocity;
			rigidbody.angularVelocity = rigidbodyState.AngularVelocity;
			rigidbody.WakeUp();
		}
	}

	// Token: 0x04002046 RID: 8262
	public Rect LocalBoundingBox = new Rect(-1f, 1f, 2f, 2f);

	// Token: 0x04002047 RID: 8263
	public bool UseLocalCoordinates;

	// Token: 0x04002048 RID: 8264
	private bool m_awakeCalled;

	// Token: 0x04002049 RID: 8265
	private bool m_startCalled;

	// Token: 0x0400204A RID: 8266
	private bool m_loadedBeforeStart;

	// Token: 0x0400204B RID: 8267
	private Bounds m_cachedBoundingBox;

	// Token: 0x0400204C RID: 8268
	private bool m_isInsideFrustum;

	// Token: 0x0400204D RID: 8269
	private bool m_isSuspended;

	// Token: 0x0400204E RID: 8270
	[SerializeField]
	private ConfigurableJoint[] m_cachedJoints;

	// Token: 0x0400204F RID: 8271
	[SerializeField]
	private ApplyTurbulentForce[] m_applyTurbulentForces;

	// Token: 0x04002050 RID: 8272
	[SerializeField]
	private RigidbodyState[] m_cachedRigidbodyStates;

	// Token: 0x04002051 RID: 8273
	private List<RigidbodyState> m_dynamicRigidBodyStates;
}
