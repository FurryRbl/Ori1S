using System;
using Game;
using UnityEngine;

// Token: 0x020001A1 RID: 417
public class CameraPivotZone : SaveSerialize, ISuspendable
{
	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x06001005 RID: 4101 RVA: 0x00049430 File Offset: 0x00047630
	private bool IsInside
	{
		get
		{
			return Characters.Current != null && (new Rect
			{
				width = base.transform.lossyScale.x,
				height = base.transform.lossyScale.y,
				center = base.transform.position
			}.Contains(Characters.Current.Position) && this.Target) && this.Target.gameObject.activeInHierarchy;
		}
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x000494DC File Offset: 0x000476DC
	public static void InstantUpdate()
	{
		for (int i = 0; i < CameraPivotZone.All.Count; i++)
		{
			CameraPivotZone cameraPivotZone = CameraPivotZone.All[i];
			cameraPivotZone.ImmediatelyUpdate();
		}
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x00049518 File Offset: 0x00047718
	public void ImmediatelyUpdate()
	{
		bool isInside = this.IsInside;
		if (isInside)
		{
			this.m_activated = true;
			this.AddTargetLayerIfNotExist();
			this.m_weight.Start = 0f;
			this.m_weight.End = this.Weight;
			this.m_weight.Time = 1f;
			this.m_targetLayer.Weight = this.m_weight.Current;
		}
		else
		{
			this.m_activated = false;
			this.RemoveTargetLayerIfExists();
			this.m_weight.Start = this.Weight;
			this.m_weight.End = 0f;
			this.m_weight.Time = 1f;
		}
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x000495C9 File Offset: 0x000477C9
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.RemoveTargetLayerIfExists();
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x000495D7 File Offset: 0x000477D7
	public void OnEnable()
	{
		CameraPivotZone.All.Add(this);
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x000495E4 File Offset: 0x000477E4
	public void OnDisable()
	{
		CameraPivotZone.All.Remove(this);
		this.DeactivateTargetLayer();
		this.RemoveTargetLayerIfExists();
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x00049600 File Offset: 0x00047800
	public void ActivateTargetLayer()
	{
		this.m_weight.Start = this.m_weight.Current;
		this.m_weight.End = this.Weight;
		this.m_weight.Time = 0f;
		this.m_activated = true;
		this.AddTargetLayerIfNotExist();
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x00049651 File Offset: 0x00047851
	public void DeactivateTargetLayer()
	{
		this.m_weight.Start = this.m_weight.Current;
		this.m_weight.End = 0f;
		this.m_weight.Time = 0f;
		this.m_activated = false;
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x00049690 File Offset: 0x00047890
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_weight.Time);
		ar.Serialize(ref this.m_weight.Start);
		ar.Serialize(ref this.m_weight.End);
		ar.Serialize(ref this.m_activated);
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x000496DC File Offset: 0x000478DC
	private void RemoveTargetLayerIfExists()
	{
		if (this.m_targetLayer != null)
		{
			if (UI.Cameras.Current != null)
			{
				UI.Cameras.Current.CameraTarget.RemoveTargetLayer(this.m_targetLayer);
			}
			this.m_targetLayer = null;
		}
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x00049715 File Offset: 0x00047915
	public void AddTargetLayerIfNotExist()
	{
		if (this.m_targetLayer == null)
		{
			this.m_targetLayer = UI.Cameras.Current.CameraTarget.AddTargetLayer(this.Target, 0f, this.FollowX, this.FollowY, false);
		}
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x00049750 File Offset: 0x00047950
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		bool isInside = this.IsInside;
		if (this.m_activated)
		{
			if (!isInside)
			{
				this.DeactivateTargetLayer();
			}
		}
		else if (isInside)
		{
			this.ActivateTargetLayer();
		}
		this.m_weight.Time += Time.deltaTime / this.Duration;
		if (this.m_targetLayer != null)
		{
			this.m_targetLayer.Weight = this.m_weight.Current;
			if (this.m_targetLayer.Weight == 0f)
			{
				this.RemoveTargetLayerIfExists();
			}
		}
	}

	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x06001011 RID: 4113 RVA: 0x000497F2 File Offset: 0x000479F2
	// (set) Token: 0x06001012 RID: 4114 RVA: 0x000497FA File Offset: 0x000479FA
	public bool IsSuspended { get; set; }

	// Token: 0x04000D23 RID: 3363
	public static AllContainer<CameraPivotZone> All = new AllContainer<CameraPivotZone>();

	// Token: 0x04000D24 RID: 3364
	public bool FollowX = true;

	// Token: 0x04000D25 RID: 3365
	public bool FollowY = true;

	// Token: 0x04000D26 RID: 3366
	public float Weight = 1f;

	// Token: 0x04000D27 RID: 3367
	public float Duration = 2f;

	// Token: 0x04000D28 RID: 3368
	public Transform Target;

	// Token: 0x04000D29 RID: 3369
	private bool m_activated;

	// Token: 0x04000D2A RID: 3370
	private CameraTarget.TargetLayer m_targetLayer;

	// Token: 0x04000D2B RID: 3371
	private readonly BlendFloat m_weight = new BlendFloat(new Func<float, float>(EaseFunction.easeInOutSine));
}
