using System;
using UnityEngine;

// Token: 0x0200089E RID: 2206
public class WorldMapOverworldArea : MonoBehaviour
{
	// Token: 0x06003166 RID: 12646 RVA: 0x000D2D44 File Offset: 0x000D0F44
	public void Awake()
	{
		if (this.Area)
		{
			this.RuntimeArea = GameWorld.Instance.FindRuntimeArea(this.Area);
		}
		this.m_cleverMenu = base.GetComponent<CleverMenuItem>();
	}

	// Token: 0x170007DA RID: 2010
	// (get) Token: 0x06003167 RID: 12647 RVA: 0x000D2D83 File Offset: 0x000D0F83
	public bool IsDiscovered
	{
		get
		{
			return this.RuntimeArea == null || this.RuntimeArea.AreaDiscovered;
		}
	}

	// Token: 0x170007DB RID: 2011
	// (get) Token: 0x06003168 RID: 12648 RVA: 0x000D2D9D File Offset: 0x000D0F9D
	public Vector3 ScrollPosition
	{
		get
		{
			return this.RuntimeArea.FindCenterPositionOnDiscoveredAreas();
		}
	}

	// Token: 0x06003169 RID: 12649 RVA: 0x000D2DB0 File Offset: 0x000D0FB0
	public void OnEnable()
	{
		this.Fog.SetActive(!this.IsDiscovered && !AreaMapUI.Instance.DebugNavigation.UndiscoveredMapVisible);
	}

	// Token: 0x0600316A RID: 12650 RVA: 0x000D2DE8 File Offset: 0x000D0FE8
	public void FixedUpdate()
	{
		Rect rect = new Rect
		{
			width = this.m_cleverMenu.Size.x,
			height = this.m_cleverMenu.Size.y,
			center = this.m_cleverMenu.Position + this.m_cleverMenu.Center
		};
		Vector2 vector = WorldMapUI.Instance.WorldToScreenToUI(new Vector3(rect.xMin, rect.yMin));
		Vector2 vector2 = WorldMapUI.Instance.WorldToScreenToUI(new Vector3(rect.xMax, rect.yMax));
		this.m_cleverMenu.SetBounds(new Rect(vector.x, vector.y, vector2.x - vector.x, vector2.y - vector.y));
	}

	// Token: 0x04002CAE RID: 11438
	public GameWorldArea Area;

	// Token: 0x04002CAF RID: 11439
	public RuntimeGameWorldArea RuntimeArea;

	// Token: 0x04002CB0 RID: 11440
	public GameObject Fog;

	// Token: 0x04002CB1 RID: 11441
	private CleverMenuItem m_cleverMenu;
}
