using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200014E RID: 334
public class AreaMapNavigation : MonoBehaviour
{
	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0003E878 File Offset: 0x0003CA78
	public float ZoomTime
	{
		get
		{
			return GameMapTransitionManager.Instance.ZoomTime;
		}
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06000D81 RID: 3457 RVA: 0x0003E884 File Offset: 0x0003CA84
	public float Zoom
	{
		get
		{
			if (this.ZoomTime < 1f)
			{
				return 1f / Mathf.Lerp(50f / this.WorldMapZoomLevel, 50f / this.AreaMapZoomLevel, Mathf.SmoothStep(0f, 1f, this.ZoomTime));
			}
			return 1f / Mathf.Lerp(50f / this.AreaMapZoomLevel, 50f / this.AreaMapCloseZoomLevel, Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(1f, 2f, this.ZoomTime)));
		}
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x0003E924 File Offset: 0x0003CB24
	public bool BoxIsInsideVisibleCanvas(Rect bound)
	{
		foreach (RuntimeGameWorldArea runtimeGameWorldArea in GameWorld.Instance.RuntimeAreas)
		{
			CageStructureTool cageStructureTool = runtimeGameWorldArea.Area.CageStructureTool;
			Rect[] facesAsRectangles = cageStructureTool.FacesAsRectangles;
			for (int i = 0; i < facesAsRectangles.Length; i++)
			{
				if (facesAsRectangles[i].Overlaps(bound))
				{
					int id = cageStructureTool.Faces[i].ID;
					if (runtimeGameWorldArea.FaceIsDiscoveredOrVisited(id))
					{
						return true;
					}
					if (this.m_areaMapUi.DebugNavigation.UndiscoveredMapVisible)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x0003EA04 File Offset: 0x0003CC04
	public void Awake()
	{
		this.m_areaMapUi = base.GetComponent<AreaMapUI>();
		this.m_scrollLimits = base.GetComponentsInChildren<AreaMapScrollLimit>();
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x0003EA1E File Offset: 0x0003CC1E
	public void OnDisable()
	{
		this.ScrollSound.Stop();
	}

	// Token: 0x17000293 RID: 659
	// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0003EA2B File Offset: 0x0003CC2B
	// (set) Token: 0x06000D86 RID: 3462 RVA: 0x0003EA33 File Offset: 0x0003CC33
	public Bounds Bounds { get; set; }

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0003EA3C File Offset: 0x0003CC3C
	// (set) Token: 0x06000D88 RID: 3464 RVA: 0x0003EA4E File Offset: 0x0003CC4E
	public Vector2 MapPlanePosition
	{
		get
		{
			return this.MapPivot.localPosition;
		}
		set
		{
			this.MapPivot.localPosition = value;
		}
	}

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0003EA61 File Offset: 0x0003CC61
	// (set) Token: 0x06000D8A RID: 3466 RVA: 0x0003EA74 File Offset: 0x0003CC74
	public Vector2 MapPlaneSize
	{
		get
		{
			return this.MapPivot.localScale;
		}
		set
		{
			Vector3 localScale = this.MapPivot.localScale;
			localScale.x = value.x;
			localScale.y = value.y;
			this.MapPivot.localScale = localScale;
		}
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x0003EAB5 File Offset: 0x0003CCB5
	public void Advance()
	{
		this.HandleMapScrolling();
		this.UpdatePlane();
		this.HandleObjectiveFocus();
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x0003EACC File Offset: 0x0003CCCC
	public void HandleObjectiveFocus()
	{
		bool isTransitioning = GameMapTransitionManager.Instance.IsTransitioning;
		this.m_focusTime = Mathf.Clamp01(this.m_focusTime - 2f * Time.deltaTime);
		if (this.m_focusTime > 0f)
		{
			this.ScrollPosition = Vector2.Lerp(this.m_fromPosition, this.m_toPosition, Mathf.SmoothStep(1f, 0f, this.m_focusTime));
			this.m_scrollTime = 0f;
		}
		if (!isTransitioning && this.m_focusTime == 0f && Core.Input.Focus.OnPressed && !Core.Input.Focus.Used)
		{
			Core.Input.Focus.Used = true;
			this.m_focusTime = 1f;
			this.m_fromPosition = this.ScrollPosition;
			if (Objectives.All.Count == 0)
			{
				this.m_toggleToPlayer = true;
			}
			this.m_toPosition = ((!this.m_toggleToPlayer) ? Objectives.All[0].Position : Characters.Current.Position);
			this.m_toggleToPlayer = !this.m_toggleToPlayer;
			if (this.FocusSound)
			{
				this.FocusSound.Play();
			}
		}
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x0003EC13 File Offset: 0x0003CE13
	public void Init()
	{
		this.m_toggleToPlayer = false;
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x0003EC1C File Offset: 0x0003CE1C
	public void UpdatePlane()
	{
		this.MapPlaneSize = Vector2.one * this.Zoom;
		Vector2 b = new Vector3(Mathf.Sin(Time.time * 1f), Mathf.Cos(Time.time * 1.2f)) * 0.06f;
		this.MapPivot.position = -this.ScrollPosition * this.Zoom + b;
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x0003ECA0 File Offset: 0x0003CEA0
	public void CenterMapOnWorldPosition(Vector3 position)
	{
		this.m_scrollTime = 0f;
		this.ScrollPosition = position;
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x0003ECB9 File Offset: 0x0003CEB9
	public Vector3 WorldToMapPosition(Vector2 position)
	{
		return this.MapPivot.TransformPoint(position);
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x0003ECCC File Offset: 0x0003CECC
	public Vector3 MapToWorldPosition(Vector2 position)
	{
		Vector2 v = position - this.MapPlanePosition;
		v.x /= this.MapPlaneSize.x;
		v.y /= this.MapPlaneSize.y;
		return v;
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x0003ED24 File Offset: 0x0003CF24
	private void HandleMapScrolling()
	{
		if (!GameMapTransitionManager.Instance.InAreaMapMode)
		{
			return;
		}
		if (GameMapUI.Instance.ShowingObjective || GameMapUI.Instance.RevealingMap)
		{
			return;
		}
		Vector2 vector = Vector2.zero;
		Vector2 cursorPositionUI = Core.Input.CursorPositionUI;
		cursorPositionUI.x /= this.MapPlaneSize.x;
		cursorPositionUI.y /= this.MapPlaneSize.y;
		if (Core.Input.LeftClick.OnPressed)
		{
			this.m_lastDragPosition = cursorPositionUI;
		}
		if (Core.Input.LeftClick.Pressed && Core.Input.CursorMoved)
		{
			vector += this.m_lastDragPosition - cursorPositionUI;
			this.m_lastDragPosition = cursorPositionUI;
		}
		if ((double)Core.Input.Axis.magnitude < 0.02)
		{
			this.m_scrollTime = 0f;
		}
		else
		{
			this.m_scrollTime = Mathf.Clamp01(this.m_scrollTime + Time.deltaTime * 4f);
			vector = Core.Input.Axis.normalized * this.ScrollingSensitivityCurve.Evaluate(Core.Input.Axis.magnitude) * this.m_scrollTime;
			vector *= Time.deltaTime * 150f / this.Zoom;
		}
		if (vector.magnitude > 0f)
		{
			if (vector.x < 0f && this.ScrollPosition.x <= this.m_scrollAreaLimit.xMin)
			{
				vector.x = 0f;
			}
			if (vector.x > 0f && this.ScrollPosition.x >= this.m_scrollAreaLimit.xMax)
			{
				vector.x = 0f;
			}
			if (vector.y < 0f && this.ScrollPosition.y <= this.m_scrollAreaLimit.yMin)
			{
				vector.y = 0f;
			}
			if (vector.y > 0f && this.ScrollPosition.y >= this.m_scrollAreaLimit.yMax)
			{
				vector.y = 0f;
			}
			foreach (AreaMapScrollLimit areaMapScrollLimit in this.m_scrollLimits)
			{
				if (areaMapScrollLimit.Active)
				{
					Rect area = areaMapScrollLimit.Area;
					Vector2 scrollPosition = this.ScrollPosition;
					if (area.Contains(scrollPosition + new Vector2(vector.x, 0f)))
					{
						vector.x = 0f;
					}
					if (area.Contains(scrollPosition + new Vector2(0f, vector.y)))
					{
						vector.y = 0f;
					}
				}
			}
			this.ScrollPosition += vector;
			if (this.ScrollSound && !this.ScrollSound.IsPlaying && (double)vector.magnitude >= 0.3)
			{
				this.ScrollSound.Play();
			}
			else if (this.ScrollSound && this.ScrollSound.IsPlaying && (double)vector.magnitude < 0.3)
			{
				this.ScrollSound.StopAndFadeOut(0f);
			}
		}
		else if (this.ScrollSound && this.ScrollSound.IsPlaying)
		{
			this.ScrollSound.StopAndFadeOut(0f);
		}
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x0003F0F0 File Offset: 0x0003D2F0
	public Vector3 ConstrainWorldPositionByBounds(Vector3 worldPosition)
	{
		Bounds bounds = this.Bounds;
		worldPosition.x = Mathf.Clamp(worldPosition.x, bounds.min.x, bounds.max.x);
		worldPosition.y = Mathf.Clamp(worldPosition.y, bounds.min.y, bounds.max.y);
		return worldPosition;
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x0003F168 File Offset: 0x0003D368
	public void UpdateScrollLimits()
	{
		bool flag = false;
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		foreach (RuntimeGameWorldArea runtimeGameWorldArea in GameWorld.Instance.RuntimeAreas)
		{
			GameWorldArea area = runtimeGameWorldArea.Area;
			Rect[] facesAsRectangles = area.CageStructureTool.FacesAsRectangles;
			for (int i = 0; i < area.CageStructureTool.Faces.Count; i++)
			{
				Rect rect = facesAsRectangles[i];
				int id = area.CageStructureTool.Faces[i].ID;
				if (runtimeGameWorldArea.FaceIsDiscoveredOrVisited(id) || AreaMapUI.Instance.DebugNavigation.UndiscoveredMapVisible)
				{
					if (flag)
					{
						num = Mathf.Min(num, rect.xMin);
						num2 = Mathf.Min(num2, rect.yMin);
						num3 = Mathf.Max(num3, rect.xMax);
						num4 = Mathf.Max(num4, rect.yMax);
					}
					else
					{
						flag = true;
						num = rect.xMin;
						num2 = rect.yMin;
						num3 = rect.xMax;
						num4 = rect.yMax;
					}
				}
			}
		}
		for (int j = 0; j < Objectives.All.Count; j++)
		{
			Objective objective = Objectives.All[j];
			Vector2 position = objective.Position;
			num = Mathf.Min(num, position.x);
			num2 = Mathf.Min(num2, position.y);
			num3 = Mathf.Max(num3, position.x);
			num4 = Mathf.Max(num4, position.y);
		}
		if (Characters.Sein)
		{
			Vector2 vector = Characters.Sein.Position;
			num = Mathf.Min(num, vector.x);
			num2 = Mathf.Min(num2, vector.y);
			num3 = Mathf.Max(num3, vector.x);
			num4 = Mathf.Max(num4, vector.y);
		}
		this.m_scrollAreaLimit.xMin = num;
		this.m_scrollAreaLimit.yMin = num2;
		this.m_scrollAreaLimit.xMax = num3;
		this.m_scrollAreaLimit.yMax = num4;
	}

	// Token: 0x04000AFC RID: 2812
	public Transform MapPivot;

	// Token: 0x04000AFD RID: 2813
	public float AreaMapZoomLevel = 1.8f;

	// Token: 0x04000AFE RID: 2814
	public float WorldMapZoomLevel = 0.5f;

	// Token: 0x04000AFF RID: 2815
	public float AreaMapCloseZoomLevel = 3f;

	// Token: 0x04000B00 RID: 2816
	private float m_scrollTime;

	// Token: 0x04000B01 RID: 2817
	public AnimationCurve ScrollingSensitivityCurve;

	// Token: 0x04000B02 RID: 2818
	private Vector2 m_lastDragPosition;

	// Token: 0x04000B03 RID: 2819
	public SoundSource ScrollSound;

	// Token: 0x04000B04 RID: 2820
	public SoundSource FocusSound;

	// Token: 0x04000B05 RID: 2821
	public Vector2 ScrollPosition;

	// Token: 0x04000B06 RID: 2822
	private AreaMapUI m_areaMapUi;

	// Token: 0x04000B07 RID: 2823
	private AreaMapScrollLimit[] m_scrollLimits;

	// Token: 0x04000B08 RID: 2824
	private Vector2 m_fromPosition;

	// Token: 0x04000B09 RID: 2825
	private Vector2 m_toPosition;

	// Token: 0x04000B0A RID: 2826
	private float m_focusTime;

	// Token: 0x04000B0B RID: 2827
	private bool m_toggleToPlayer;

	// Token: 0x04000B0C RID: 2828
	private Rect m_scrollAreaLimit;
}
