using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x020000C9 RID: 201
[Serializable]
public class RuntimeSceneMetaData
{
	// Token: 0x06000892 RID: 2194 RVA: 0x00024E98 File Offset: 0x00023098
	public RuntimeSceneMetaData(SceneMetaData sceneMetaData)
	{
		this.Scene = sceneMetaData.SceneName;
		this.PlaceholderPosition = sceneMetaData.SeinPlaceholderPosition;
		this.SceneMoonGuid = sceneMetaData.SceneMoonGuid;
		this.SceneLoadingBoundaries = new List<Rect>(sceneMetaData.SceneLoadingBoundaries);
		this.SceneBoundaries = new List<Rect>(sceneMetaData.SceneBoundaries);
		this.ScenePaddingBoundaries = new List<Rect>(sceneMetaData.ScenePaddingBoundaries);
		this.LoadingCondition = sceneMetaData.LoadingCondition;
		this.FPSTestPosition = new List<Vector3>(sceneMetaData.FPSTestPosition);
		this.IncludedScenes.Clear();
		this.DependantScene = sceneMetaData.DependantScene;
		this.ScenePaddingWideScreenExpansion = sceneMetaData.ScenePaddingWideScreenExpansion;
		while (this.ScenePaddingWideScreenExpansion.Count < this.ScenePaddingBoundaries.Count)
		{
			this.ScenePaddingWideScreenExpansion.Add(1f);
		}
		foreach (SceneMetaData sceneMetaData2 in sceneMetaData.IncludedScenes)
		{
			if (sceneMetaData2)
			{
				this.IncludedScenes.Add(sceneMetaData2.SceneMoonGuid);
			}
		}
	}

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x06000893 RID: 2195 RVA: 0x0002501C File Offset: 0x0002321C
	public Rect SceneBounds
	{
		get
		{
			if (this.SceneBoundaries.Count == 0)
			{
				return new Rect(0f, 0f, 0f, 0f);
			}
			return Utility.CombineRects(this.SceneBoundaries.ToArray());
		}
	}

	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x06000894 RID: 2196 RVA: 0x00025063 File Offset: 0x00023263
	public static float PaddingWidthExtension
	{
		get
		{
			return Scenes.Manager.PaddingWidthExtension;
		}
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00025070 File Offset: 0x00023270
	private Rect Encapsulate(Rect original, Rect add)
	{
		if (original.width == 0f && original.height == 0f)
		{
			return add;
		}
		original.xMin = Mathf.Min(original.xMin, add.xMin);
		original.xMax = Mathf.Max(original.xMax, add.xMax);
		original.yMin = Mathf.Min(original.yMin, add.yMin);
		original.yMax = Mathf.Max(original.yMax, add.yMax);
		return original;
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0002510C File Offset: 0x0002330C
	private void DoTotal()
	{
		if (this.m_doneTotal)
		{
			return;
		}
		int count = this.SceneBoundaries.Count;
		for (int i = 0; i < count; i++)
		{
			this.m_totalRect = this.Encapsulate(this.m_totalRect, this.SceneBoundaries[i]);
		}
		count = this.SceneLoadingBoundaries.Count;
		for (int j = 0; j < count; j++)
		{
			this.m_totalRect = this.Encapsulate(this.m_totalRect, this.SceneLoadingBoundaries[j]);
		}
		count = this.ScenePaddingBoundaries.Count;
		for (int k = 0; k < count; k++)
		{
			this.m_totalRect = this.Encapsulate(this.m_totalRect, this.ScenePaddingBoundaries[k]);
		}
		this.m_totalRect.xMin = this.m_totalRect.xMin - 20f;
		this.m_totalRect.xMax = this.m_totalRect.xMax + 20f;
		this.m_doneTotal = true;
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x0002520E File Offset: 0x0002340E
	public bool IsInTotal(Vector3 position)
	{
		this.DoTotal();
		return this.m_totalRect.Contains(position);
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00025222 File Offset: 0x00023422
	public bool IsInTotal(Rect rect)
	{
		this.DoTotal();
		return this.m_totalRect.Overlaps(rect);
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00025238 File Offset: 0x00023438
	public Rect ApplyWidthExtension(Rect rect, float r = 1f)
	{
		return Rect.MinMaxRect(rect.xMin - RuntimeSceneMetaData.PaddingWidthExtension * r, rect.yMin, rect.xMax + RuntimeSceneMetaData.PaddingWidthExtension * r, rect.yMax);
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x00025276 File Offset: 0x00023476
	public Rect PositionToRect(Vector2 position)
	{
		return new Rect(position.x, position.y, 0f, 0f);
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x00025295 File Offset: 0x00023495
	public bool IsInsideSceneLoadingZone(Vector2 position)
	{
		return this.IsInsideSceneLoadingZone(this.PositionToRect(position));
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x000252A4 File Offset: 0x000234A4
	public bool IsInsideScenePaddingBounds(Vector3 position, Rect currentSceneBounds)
	{
		return this.IsInsideScenePaddingBounds(this.PositionToRect(position), currentSceneBounds);
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x000252B9 File Offset: 0x000234B9
	public bool IsInsideScenePaddingBounds(Vector3 position)
	{
		return this.IsInsideScenePaddingBounds(this.PositionToRect(position));
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x000252CD File Offset: 0x000234CD
	public bool IsInsideSceneBounds(Vector3 position)
	{
		return this.IsInsideSceneBounds(this.PositionToRect(position));
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x000252E1 File Offset: 0x000234E1
	public bool IsInsideScenePaddingBoundsExpanded(Rect rect)
	{
		return this.IsInsideScenePaddingBounds(this.ApplyWidthExtension(rect, 1f));
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x000252F8 File Offset: 0x000234F8
	public bool IsInsideSceneBounds(Rect rect)
	{
		int count = this.SceneBoundaries.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.SceneBoundaries[i].Overlaps(rect))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x00025340 File Offset: 0x00023540
	public bool IsInsideSceneLoadingZone(Rect rect)
	{
		int count = this.SceneLoadingBoundaries.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.SceneLoadingBoundaries[i].Overlaps(rect))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x00025388 File Offset: 0x00023588
	public bool IsInsideScenePaddingBounds(Rect rect, Rect currentSceneBounds)
	{
		int count = this.ScenePaddingBoundaries.Count;
		for (int i = 0; i < count; i++)
		{
			Rect rect2 = this.ScenePaddingBoundaries[i];
			float r = this.ScenePaddingWideScreenExpansion[i];
			if (rect2.Overlaps(currentSceneBounds))
			{
				rect2 = this.ApplyWidthExtension(rect2, r);
			}
			if (rect2.Overlaps(rect))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x000253F4 File Offset: 0x000235F4
	public bool IsInsideScenePaddingBounds(Rect rect)
	{
		int count = this.ScenePaddingBoundaries.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.ScenePaddingBoundaries[i].Overlaps(rect))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0002543C File Offset: 0x0002363C
	public bool CanBeLoaded
	{
		get
		{
			return !(this.LoadingCondition != null) || this.LoadingCondition.Validate(null);
		}
	}

	// Token: 0x040006DA RID: 1754
	public const float MAX_PADDING_WIDTH_EXTENSION = 20f;

	// Token: 0x040006DB RID: 1755
	public string Scene;

	// Token: 0x040006DC RID: 1756
	public Vector2 PlaceholderPosition;

	// Token: 0x040006DD RID: 1757
	public MoonGuid SceneMoonGuid;

	// Token: 0x040006DE RID: 1758
	public List<Rect> SceneLoadingBoundaries = new List<Rect>();

	// Token: 0x040006DF RID: 1759
	public List<Rect> SceneBoundaries = new List<Rect>();

	// Token: 0x040006E0 RID: 1760
	public List<Rect> ScenePaddingBoundaries = new List<Rect>();

	// Token: 0x040006E1 RID: 1761
	public List<float> ScenePaddingWideScreenExpansion = new List<float>();

	// Token: 0x040006E2 RID: 1762
	public Condition LoadingCondition;

	// Token: 0x040006E3 RID: 1763
	public List<Vector3> FPSTestPosition = new List<Vector3>();

	// Token: 0x040006E4 RID: 1764
	public List<MoonGuid> IncludedScenes = new List<MoonGuid>();

	// Token: 0x040006E5 RID: 1765
	public bool DependantScene;

	// Token: 0x040006E6 RID: 1766
	private Rect m_totalRect;

	// Token: 0x040006E7 RID: 1767
	private bool m_doneTotal;
}
