using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003E8 RID: 1000
[ExecuteInEditMode]
public class CameraSettingsZone : MonoBehaviour
{
	// Token: 0x06001B4D RID: 6989 RVA: 0x000758C8 File Offset: 0x00073AC8
	public CameraSettings GetSettings()
	{
		if (this.m_settings == null && this.Settings)
		{
			this.m_settings = new CameraSettings(this.Settings, this.FogGradient, this.FogRange);
		}
		return this.m_settings;
	}

	// Token: 0x06001B4E RID: 6990 RVA: 0x00075914 File Offset: 0x00073B14
	private void Register()
	{
		for (int i = 0; i < CameraSettingsZone.All.Count; i++)
		{
			if (CameraSettingsZone.All[i].Priority > this.Priority)
			{
				CameraSettingsZone.All.Insert(i, this);
				return;
			}
		}
		CameraSettingsZone.All.Add(this);
	}

	// Token: 0x06001B4F RID: 6991 RVA: 0x00075970 File Offset: 0x00073B70
	public void OnEnable()
	{
		this.Register();
		this.m_area = new Rect
		{
			width = base.transform.lossyScale.x,
			height = base.transform.lossyScale.y,
			center = base.transform.position
		};
		this.m_settings = null;
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x000759E4 File Offset: 0x00073BE4
	public void OnDisable()
	{
		CameraSettingsZone.All.Remove(this);
	}

	// Token: 0x17000485 RID: 1157
	// (get) Token: 0x06001B51 RID: 6993 RVA: 0x000759F2 File Offset: 0x00073BF2
	public float Strength
	{
		get
		{
			return this.m_strength * this.AnimatedStrength;
		}
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x00075A04 File Offset: 0x00073C04
	public void Advance(Vector2 position, float timeDelta)
	{
		float target = this.CalculateStrengthFromPosition(position);
		if (this.Condition && !this.Condition.Validate(null))
		{
			target = 0f;
		}
		this.m_strength = Mathf.MoveTowards(this.m_strength, target, timeDelta / this.Duration);
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x00075A5C File Offset: 0x00073C5C
	public float CalculateStrengthFromPosition(Vector2 position)
	{
		if (!base.enabled || !base.gameObject.activeInHierarchy)
		{
			return 0f;
		}
		if (!this.m_area.Contains(position))
		{
			return 0f;
		}
		float num = this.LeftPadding;
		float num2 = this.RightPadding;
		float num3 = this.BottomPadding;
		float num4 = this.TopPadding;
		if (num == 0f)
		{
			num = 0.01f;
		}
		if (num2 == 0f)
		{
			num2 = 0.01f;
		}
		if (num3 == 0f)
		{
			num3 = 0.01f;
		}
		if (num4 == 0f)
		{
			num4 = 0.01f;
		}
		return Mathf.Min(new float[]
		{
			Mathf.InverseLerp(this.m_area.xMin, this.m_area.xMin + num, position.x),
			Mathf.InverseLerp(this.m_area.xMax, this.m_area.xMax - num2, position.x),
			Mathf.InverseLerp(this.m_area.yMin, this.m_area.yMin + num4, position.y),
			Mathf.InverseLerp(this.m_area.yMax, this.m_area.yMax - num3, position.y)
		});
	}

	// Token: 0x040017B7 RID: 6071
	public static List<CameraSettingsZone> All = new List<CameraSettingsZone>();

	// Token: 0x040017B8 RID: 6072
	public CameraSettingsZone.ZoneMode Mode;

	// Token: 0x040017B9 RID: 6073
	public CameraSettingsZone.Direction GradientDirection;

	// Token: 0x040017BA RID: 6074
	public Condition Condition;

	// Token: 0x040017BB RID: 6075
	public CameraSettingsAsset Settings;

	// Token: 0x040017BC RID: 6076
	public Gradient FogGradient;

	// Token: 0x040017BD RID: 6077
	public float FogRange = 100f;

	// Token: 0x040017BE RID: 6078
	public CameraSettingsAsset Settings2;

	// Token: 0x040017BF RID: 6079
	public Gradient FogGradient2;

	// Token: 0x040017C0 RID: 6080
	public float FogRange2 = 100f;

	// Token: 0x040017C1 RID: 6081
	public float LeftPadding;

	// Token: 0x040017C2 RID: 6082
	public float RightPadding;

	// Token: 0x040017C3 RID: 6083
	public float TopPadding;

	// Token: 0x040017C4 RID: 6084
	public float BottomPadding;

	// Token: 0x040017C5 RID: 6085
	public float Duration = 1f;

	// Token: 0x040017C6 RID: 6086
	public float AnimatedStrength = 1f;

	// Token: 0x040017C7 RID: 6087
	public int Priority;

	// Token: 0x040017C8 RID: 6088
	private CameraSettings m_settings;

	// Token: 0x040017C9 RID: 6089
	private Rect m_area;

	// Token: 0x040017CA RID: 6090
	private float m_strength;

	// Token: 0x040017CB RID: 6091
	private bool m_isActive;

	// Token: 0x020003EF RID: 1007
	public enum Direction
	{
		// Token: 0x040017DC RID: 6108
		Horizontal,
		// Token: 0x040017DD RID: 6109
		Vertical
	}

	// Token: 0x020003F0 RID: 1008
	public enum ZoneMode
	{
		// Token: 0x040017DF RID: 6111
		Single,
		// Token: 0x040017E0 RID: 6112
		Gradient
	}
}
