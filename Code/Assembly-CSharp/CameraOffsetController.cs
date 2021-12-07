using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x020001FB RID: 507
public class CameraOffsetController : MonoBehaviour
{
	// Token: 0x17000325 RID: 805
	// (get) Token: 0x06001197 RID: 4503 RVA: 0x00050F1B File Offset: 0x0004F11B
	// (set) Token: 0x06001198 RID: 4504 RVA: 0x00050F28 File Offset: 0x0004F128
	public Vector3 Offset
	{
		get
		{
			return base.transform.localPosition;
		}
		set
		{
			base.transform.localPosition = value;
		}
	}

	// Token: 0x17000326 RID: 806
	// (get) Token: 0x06001199 RID: 4505 RVA: 0x00050F36 File Offset: 0x0004F136
	// (set) Token: 0x0600119A RID: 4506 RVA: 0x00050F48 File Offset: 0x0004F148
	public Vector3 Rotation
	{
		get
		{
			return base.transform.parent.localEulerAngles;
		}
		set
		{
			base.transform.parent.localEulerAngles = value;
		}
	}

	// Token: 0x0600119B RID: 4507 RVA: 0x00050F5B File Offset: 0x0004F15B
	public void AddOffsetLayer(CameraOffsetController.OffsetLayer layer)
	{
		this.m_offsetLayers.Add(layer);
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x00050F69 File Offset: 0x0004F169
	public void RemoveOffsetLayer(CameraOffsetController.OffsetLayer layer)
	{
		this.m_offsetLayers.Remove(layer);
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x00050F78 File Offset: 0x0004F178
	public static void Register(CameraOffsetZone offsetZone)
	{
		for (int i = 0; i < CameraOffsetController.m_offsetZones.Count; i++)
		{
			if (CameraOffsetController.m_offsetZones[i].priority >= offsetZone.priority)
			{
				CameraOffsetController.m_offsetZones.Insert(i, offsetZone);
				return;
			}
		}
		CameraOffsetController.m_offsetZones.Add(offsetZone);
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x00050FD3 File Offset: 0x0004F1D3
	public static void Unregister(CameraOffsetZone offsetZome)
	{
		CameraOffsetController.m_offsetZones.Remove(offsetZome);
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x00050FE4 File Offset: 0x0004F1E4
	public void Awake()
	{
		Game.Checkpoint.Events.OnScrollLockPassed.Add(new Action(this.OnScrollLockPassed));
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
		AspectRatioManager.OnAspectChanged.Add(new Action(this.OnAspectChanged));
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x00051038 File Offset: 0x0004F238
	public void OnDestroy()
	{
		Game.Checkpoint.Events.OnScrollLockPassed.Remove(new Action(this.OnScrollLockPassed));
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
		AspectRatioManager.OnAspectChanged.Remove(new Action(this.OnAspectChanged));
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x0005108C File Offset: 0x0004F28C
	public void OnAspectChanged()
	{
		this.UpdateOffset(true);
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x00051095 File Offset: 0x0004F295
	public void OnGameReset()
	{
		this.AdditiveDefaultOffset = Vector3.zero;
	}

	// Token: 0x060011A3 RID: 4515 RVA: 0x000510A2 File Offset: 0x0004F2A2
	public void OnScrollLockPassed()
	{
		this.AdditiveDefaultOffset = Vector3.zero;
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x000510B0 File Offset: 0x0004F2B0
	public void UpdateOffset(bool instant = false)
	{
		float num = this.MultipliedBasedOnCharacterSpeedCurve.Evaluate(this.m_characterSpeed);
		float num2 = (!instant) ? (UI.Cameras.Current.TimeDelta * num) : float.PositiveInfinity;
		Vector3 targetHelperPosition = UI.Cameras.Current.TargetHelperPosition;
		this.m_defaultSettingsHelper.Advance(targetHelperPosition, num2);
		Vector3 vector = new Vector3(0f, 0f, 1f) * this.DefaultZoom;
		if (this.m_defaultSettingsHelper.TweenTime == 0f)
		{
			if (this.m_defaultSettingsHelper.HasFromSettings)
			{
				vector = this.m_defaultSettingsHelper.FromSettings.DefaultCameraZoom;
			}
		}
		else if (this.m_defaultSettingsHelper.TweenTime == 1f)
		{
			if (this.m_defaultSettingsHelper.HasToSettings)
			{
				vector = this.m_defaultSettingsHelper.ToSettings.DefaultCameraZoom;
			}
		}
		else if (this.m_defaultSettingsHelper.HasToSettings && this.m_defaultSettingsHelper.HasFromSettings)
		{
			vector = Vector3.Lerp(this.m_defaultSettingsHelper.FromSettings.DefaultCameraZoom, this.m_defaultSettingsHelper.ToSettings.DefaultCameraZoom, this.ZoomCurve.Evaluate(this.m_defaultSettingsHelper.TweenTime));
		}
		for (int i = 0; i < CameraOffsetController.m_offsetZones.Count; i++)
		{
			CameraOffsetZone cameraOffsetZone = CameraOffsetController.m_offsetZones[i];
			float num3 = cameraOffsetZone.NormalizedMarginPenetration(targetHelperPosition);
			if (num3 > 0f)
			{
				if (cameraOffsetZone.Offset.x != 0f)
				{
					vector.x = Mathf.Lerp(vector.x, cameraOffsetZone.Offset.x, cameraOffsetZone.ZoomCurve.Evaluate(num3));
				}
				if (cameraOffsetZone.Offset.y != 0f)
				{
					vector.y = Mathf.Lerp(vector.y, cameraOffsetZone.Offset.y, cameraOffsetZone.ZoomCurve.Evaluate(num3));
				}
				if (cameraOffsetZone.Offset.z != 0f)
				{
					vector.z = Mathf.Lerp(vector.z, cameraOffsetZone.Offset.z, cameraOffsetZone.ZoomCurve.Evaluate(num3));
				}
			}
		}
		Vector3 zero = Vector3.zero;
		for (int j = 0; j < CameraOffsetAnimationZone.All.Count; j++)
		{
			CameraOffsetAnimationZone cameraOffsetAnimationZone = CameraOffsetAnimationZone.All[j];
			if (cameraOffsetAnimationZone)
			{
				cameraOffsetAnimationZone.OnCameraOffsetUpdate(instant);
			}
		}
		for (int k = 0; k < this.m_offsetLayers.Count; k++)
		{
			CameraOffsetController.OffsetLayer offsetLayer = this.m_offsetLayers[k];
			offsetLayer.Weight.Time += num2 / offsetLayer.Duration;
			if (offsetLayer.UseOffsetX)
			{
				vector.x = Mathf.Lerp(vector.x, offsetLayer.Offset.x, offsetLayer.Weight.Current);
			}
			if (offsetLayer.UseOffsetY)
			{
				vector.y = Mathf.Lerp(vector.y, offsetLayer.Offset.y, offsetLayer.Weight.Current);
			}
			if (offsetLayer.UseOffsetZ)
			{
				vector.z = Mathf.Lerp(vector.z, offsetLayer.Offset.z, offsetLayer.Weight.Current);
			}
			if (offsetLayer.UseRotationX)
			{
				zero.x = Mathf.Lerp(zero.x, offsetLayer.Rotation.x, offsetLayer.Weight.Current);
			}
			if (offsetLayer.UseRotationY)
			{
				zero.y = Mathf.Lerp(zero.y, offsetLayer.Rotation.y, offsetLayer.Weight.Current);
			}
			if (offsetLayer.UseRotationZ)
			{
				zero.z = Mathf.Lerp(zero.z, offsetLayer.Rotation.z, offsetLayer.Weight.Current);
			}
		}
		vector += this.AdditiveDefaultOffset;
		vector.z *= -1f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		for (int l = 0; l < CameraWideScreenZone.All.Count; l++)
		{
			CameraWideScreenZone cameraWideScreenZone = CameraWideScreenZone.All[l];
			cameraWideScreenZone.UpdateOffset(num2, targetHelperPosition);
			float strength = cameraWideScreenZone.Strength;
			num4 = Mathf.Max(num4, cameraWideScreenZone.WideScreenAdjustment.ZoomStrength * strength);
			num6 += cameraWideScreenZone.WideScreenAdjustment.HorizontalPanStrength * strength;
			num5 += cameraWideScreenZone.WideScreenAdjustment.VerticalPanStrength * strength;
		}
		float fieldOfView = UI.Cameras.Current.Camera.fieldOfView;
		float num7 = -vector.z;
		float num8 = 2f * num7 * Mathf.Tan(fieldOfView * 0.5f * 0.017453292f);
		float aspectRatio = AspectRatioManager.AspectRatio;
		float num9 = aspectRatio * num8;
		float num10 = 1.7777778f * num8;
		float num11 = num9 - num10;
		vector.x += num11 * 0.5f * num6;
		vector.y += num11 * 0.5f * num5;
		vector.z *= Mathf.Lerp(1f, 1.7777778f / AspectRatioManager.AspectRatio, num4);
		if (instant)
		{
			this.Offset = vector;
		}
		else
		{
			this.Offset = Vector3.Lerp(vector, this.Offset, Mathf.Pow(0.5f, UI.Cameras.Current.TimeDelta * 20f));
		}
		this.Rotation = zero;
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x00051690 File Offset: 0x0004F890
	public void UpdateMultiplier()
	{
		if (Characters.Current != null)
		{
			this.m_characterSpeed = Mathf.Lerp(Characters.Current.Speed.magnitude, this.m_characterSpeed, 1f - Mathf.Pow(0.5f, Time.deltaTime));
		}
		else
		{
			this.m_characterSpeed = 0f;
		}
	}

	// Token: 0x04000F2D RID: 3885
	private static List<CameraOffsetZone> m_offsetZones = new List<CameraOffsetZone>();

	// Token: 0x04000F2E RID: 3886
	private readonly AllContainer<CameraOffsetController.OffsetLayer> m_offsetLayers = new AllContainer<CameraOffsetController.OffsetLayer>();

	// Token: 0x04000F2F RID: 3887
	public Vector3 AdditiveDefaultOffset = Vector3.zero;

	// Token: 0x04000F30 RID: 3888
	public float DefaultZoom = 20f;

	// Token: 0x04000F31 RID: 3889
	private readonly SceneDefaultSettingsHelper m_defaultSettingsHelper = new SceneDefaultSettingsHelper(2f);

	// Token: 0x04000F32 RID: 3890
	public AnimationCurve ZoomCurve;

	// Token: 0x04000F33 RID: 3891
	private float m_characterSpeed = 1f;

	// Token: 0x04000F34 RID: 3892
	public AnimationCurve MultipliedBasedOnCharacterSpeedCurve;

	// Token: 0x020003C9 RID: 969
	public class OffsetLayer
	{
		// Token: 0x06001ACE RID: 6862 RVA: 0x0007343B File Offset: 0x0007163B
		public OffsetLayer()
		{
			this.Weight = new BlendFloat(new Func<float, float>(EaseFunction.easeInOutSine));
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0007345C File Offset: 0x0007165C
		public OffsetLayer(Vector3 offset, bool useOffsetX, bool useOffsetY, bool useOffsetZ, Vector3 rotation, bool useRotationX, bool useRotationY, bool useRotationZ)
		{
			this.Offset = offset;
			this.UseOffsetX = useOffsetX;
			this.UseOffsetY = useOffsetY;
			this.UseOffsetZ = useOffsetZ;
			this.Rotation = rotation;
			this.UseRotationX = useRotationX;
			this.UseRotationY = useRotationY;
			this.UseRotationZ = useRotationZ;
			this.Weight = new BlendFloat(new Func<float, float>(EaseFunction.easeInOutSine));
		}

		// Token: 0x0400173F RID: 5951
		public Vector3 Offset;

		// Token: 0x04001740 RID: 5952
		public Vector3 Rotation;

		// Token: 0x04001741 RID: 5953
		public bool UseOffsetX;

		// Token: 0x04001742 RID: 5954
		public bool UseOffsetY;

		// Token: 0x04001743 RID: 5955
		public bool UseOffsetZ;

		// Token: 0x04001744 RID: 5956
		public bool UseRotationX;

		// Token: 0x04001745 RID: 5957
		public bool UseRotationY;

		// Token: 0x04001746 RID: 5958
		public bool UseRotationZ;

		// Token: 0x04001747 RID: 5959
		public float Duration;

		// Token: 0x04001748 RID: 5960
		public BlendFloat Weight;
	}
}
