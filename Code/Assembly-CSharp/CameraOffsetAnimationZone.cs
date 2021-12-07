using System;
using Game;
using UnityEngine;

// Token: 0x020003C8 RID: 968
[ExecuteInEditMode]
public class CameraOffsetAnimationZone : SaveSerialize
{
	// Token: 0x06001AC8 RID: 6856 RVA: 0x00073178 File Offset: 0x00071378
	public void Activate(bool instant)
	{
		if (!this.m_isRegistered)
		{
			this.m_isRegistered = true;
			if (UI.Cameras.Current)
			{
				UI.Cameras.Current.OffsetController.AddOffsetLayer(this.m_offsetLayer);
			}
		}
		this.m_offsetLayer.Weight.Start = this.m_offsetLayer.Weight.Current;
		this.m_offsetLayer.Weight.End = this.Weight;
		this.m_offsetLayer.Weight.Time = (float)((!instant) ? 0 : 1);
	}

	// Token: 0x06001AC9 RID: 6857 RVA: 0x00073210 File Offset: 0x00071410
	public void Deactivate(bool instant)
	{
		if (this.m_offsetLayer != null)
		{
			this.m_offsetLayer.Weight.Start = this.m_offsetLayer.Weight.Current;
			this.m_offsetLayer.Weight.End = 0f;
			this.m_offsetLayer.Weight.Time = (float)((!instant) ? 0 : 1);
		}
	}

	// Token: 0x06001ACA RID: 6858 RVA: 0x0007327C File Offset: 0x0007147C
	public void OnCameraOffsetUpdate(bool instant)
	{
		Vector3 targetHelperPosition = UI.Cameras.Current.TargetHelperPosition;
		bool flag = new Rect
		{
			width = this.m_transform.lossyScale.x,
			height = this.m_transform.lossyScale.y,
			center = this.m_transform.position
		}.Contains(targetHelperPosition);
		if (this.m_isActivated != flag)
		{
			if (flag)
			{
				this.Activate(instant);
			}
			else
			{
				this.Deactivate(instant);
			}
			this.m_isActivated = flag;
		}
	}

	// Token: 0x06001ACB RID: 6859 RVA: 0x00073324 File Offset: 0x00071524
	public void OnEnable()
	{
		this.m_offsetLayer = new CameraOffsetController.OffsetLayer(this.Offset, this.UseOffsetX, this.UseOffsetY, this.UseOffsetZ, this.Rotation, this.UseRotationX, this.UseRotationY, this.UseRotationZ);
		this.m_offsetLayer.Duration = this.Duration;
		this.m_transform = base.transform;
		CameraOffsetAnimationZone.All.Add(this);
	}

	// Token: 0x06001ACC RID: 6860 RVA: 0x00073394 File Offset: 0x00071594
	public void OnDisable()
	{
		if (UI.Cameras.Current && this.m_isRegistered)
		{
			UI.Cameras.Current.OffsetController.RemoveOffsetLayer(this.m_offsetLayer);
			this.m_isRegistered = false;
		}
		this.m_isActivated = false;
		CameraOffsetAnimationZone.All.Remove(this);
	}

	// Token: 0x06001ACD RID: 6861 RVA: 0x000733EC File Offset: 0x000715EC
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_offsetLayer.Weight.Time);
		ar.Serialize(ref this.m_offsetLayer.Weight.Start);
		ar.Serialize(ref this.m_offsetLayer.Weight.End);
	}

	// Token: 0x04001730 RID: 5936
	public Vector3 Offset;

	// Token: 0x04001731 RID: 5937
	public bool UseOffsetX;

	// Token: 0x04001732 RID: 5938
	public bool UseOffsetY;

	// Token: 0x04001733 RID: 5939
	public bool UseOffsetZ = true;

	// Token: 0x04001734 RID: 5940
	public Vector3 Rotation;

	// Token: 0x04001735 RID: 5941
	public bool UseRotationX;

	// Token: 0x04001736 RID: 5942
	public bool UseRotationY;

	// Token: 0x04001737 RID: 5943
	public bool UseRotationZ;

	// Token: 0x04001738 RID: 5944
	public float Weight = 1f;

	// Token: 0x04001739 RID: 5945
	public float Duration = 2f;

	// Token: 0x0400173A RID: 5946
	private CameraOffsetController.OffsetLayer m_offsetLayer;

	// Token: 0x0400173B RID: 5947
	private bool m_isActivated;

	// Token: 0x0400173C RID: 5948
	private Transform m_transform;

	// Token: 0x0400173D RID: 5949
	public static AllContainer<CameraOffsetAnimationZone> All = new AllContainer<CameraOffsetAnimationZone>();

	// Token: 0x0400173E RID: 5950
	private bool m_isRegistered;
}
