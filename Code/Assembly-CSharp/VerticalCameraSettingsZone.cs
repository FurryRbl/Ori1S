using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003E9 RID: 1001
[ExecuteInEditMode]
public class VerticalCameraSettingsZone : MonoBehaviour
{
	// Token: 0x06001B56 RID: 6998 RVA: 0x00075BD4 File Offset: 0x00073DD4
	public void OnEnable()
	{
		VerticalCameraSettingsZone.All.Add(this);
		this.m_bounds = new Rect
		{
			width = base.transform.lossyScale.x,
			height = base.transform.lossyScale.y,
			center = base.transform.position
		};
		for (int i = 0; i < this.Items.Count; i++)
		{
			VerticalCameraSettingsZone.CameraPostMetaData cameraPostMetaData = this.Items[i];
			cameraPostMetaData.UpdateCameraSettings();
		}
		this.CurrentSettings = new CameraSettings(this.Items[0].CameraSettings);
	}

	// Token: 0x06001B57 RID: 6999 RVA: 0x00075C92 File Offset: 0x00073E92
	public void OnDisable()
	{
		VerticalCameraSettingsZone.All.Remove(this);
	}

	// Token: 0x06001B58 RID: 7000 RVA: 0x00075CA0 File Offset: 0x00073EA0
	[ContextMenu("Sort items")]
	public void SortItems()
	{
		this.Items.Sort((VerticalCameraSettingsZone.CameraPostMetaData a, VerticalCameraSettingsZone.CameraPostMetaData b) => a.Time.CompareTo(b.Time));
	}

	// Token: 0x06001B59 RID: 7001 RVA: 0x00075CD8 File Offset: 0x00073ED8
	public void UpdateCameraSettings()
	{
		VerticalCameraSettingsZone.CameraPostMetaData cameraPostMetaData = this.Items[0];
		VerticalCameraSettingsZone.CameraPostMetaData cameraPostMetaData2 = this.Items[1];
		for (int i = 0; i < this.Items.Count; i++)
		{
			VerticalCameraSettingsZone.CameraPostMetaData cameraPostMetaData3 = this.Items[i];
			cameraPostMetaData2 = cameraPostMetaData3;
			if (cameraPostMetaData3.Time > this.Time)
			{
				break;
			}
			cameraPostMetaData = cameraPostMetaData3;
		}
		float curveValue = Mathf.InverseLerp(cameraPostMetaData.Time, cameraPostMetaData2.Time, this.Time);
		UberPostProcessingAnimation.AnimateCameraSettings(ref this.CurrentSettings, cameraPostMetaData.CameraSettings, cameraPostMetaData2.CameraSettings, curveValue);
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x00075D74 File Offset: 0x00073F74
	public void Advance(Vector2 position, float timeDelta)
	{
		this.Time = Mathf.InverseLerp(this.m_bounds.yMin, this.m_bounds.yMax, position.y);
		this.UpdateCameraSettings();
		float target = (float)((!this.IsInside(position)) ? 0 : 1);
		this.Strength = Mathf.MoveTowards(this.Strength, target, timeDelta / this.Duration);
	}

	// Token: 0x17000486 RID: 1158
	// (get) Token: 0x06001B5B RID: 7003 RVA: 0x00075DE3 File Offset: 0x00073FE3
	// (set) Token: 0x06001B5C RID: 7004 RVA: 0x00075DEB File Offset: 0x00073FEB
	public float Strength { get; private set; }

	// Token: 0x06001B5D RID: 7005 RVA: 0x00075DF4 File Offset: 0x00073FF4
	public bool IsInside(Vector3 position)
	{
		return this.m_bounds.Contains(position);
	}

	// Token: 0x040017CC RID: 6092
	public static AllContainer<VerticalCameraSettingsZone> All = new AllContainer<VerticalCameraSettingsZone>();

	// Token: 0x040017CD RID: 6093
	public CameraSettings CurrentSettings;

	// Token: 0x040017CE RID: 6094
	public float Time;

	// Token: 0x040017CF RID: 6095
	public float Duration = 1f;

	// Token: 0x040017D0 RID: 6096
	private Rect m_bounds;

	// Token: 0x040017D1 RID: 6097
	public List<VerticalCameraSettingsZone.CameraPostMetaData> Items = new List<VerticalCameraSettingsZone.CameraPostMetaData>();

	// Token: 0x020003F9 RID: 1017
	[Serializable]
	public class CameraPostMetaData
	{
		// Token: 0x06001B9D RID: 7069 RVA: 0x00076E1B File Offset: 0x0007501B
		public void UpdateCameraSettings()
		{
			this.CameraSettings = new CameraSettings(this.CameraSettingsAsset, this.Fog, this.FogRange);
		}

		// Token: 0x04001802 RID: 6146
		public CameraSettings CameraSettings;

		// Token: 0x04001803 RID: 6147
		public CameraSettingsAsset CameraSettingsAsset;

		// Token: 0x04001804 RID: 6148
		public Gradient Fog;

		// Token: 0x04001805 RID: 6149
		public float FogRange;

		// Token: 0x04001806 RID: 6150
		public float Time;
	}
}
