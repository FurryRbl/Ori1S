using System;
using UnityEngine;

// Token: 0x02000880 RID: 2176
[Serializable]
public class GameMapTeleporter
{
	// Token: 0x06003110 RID: 12560 RVA: 0x000D10D6 File Offset: 0x000CF2D6
	public GameMapTeleporter(SceneMetaData.Teleporter teleporter, SceneMetaData sceneMetaData)
	{
		this.Identifier = teleporter.Identifier;
		this.WorldPosition = teleporter.SceneLocalPosition + sceneMetaData.RootPosition;
	}

	// Token: 0x06003111 RID: 12561 RVA: 0x000D1104 File Offset: 0x000CF304
	public void Show()
	{
		AreaMapUI instance = AreaMapUI.Instance;
		if (this.m_worldMapIconGameObject)
		{
			this.m_worldMapIconGameObject.SetActive(true);
		}
		else
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(instance.TeleportPrefab);
			this.m_worldMapIconTransform = gameObject.transform;
			this.m_worldMapIconGameObject = this.m_worldMapIconTransform.gameObject;
			this.m_worldMapIconHighlightAnimator = this.m_worldMapIconGameObject.transform.FindChild("highlight").GetComponentInChildren<TransparencyAnimator>();
			this.m_worldMapIconTransform.position = WorldMapUI.Instance.WorldToUIPosition(this.WorldPosition);
			this.m_worldMapIconTransform.parent = WorldMapUI.Instance.FadeOutGroup;
			TransparencyAnimator.Register(this.m_worldMapIconTransform);
		}
		if (this.m_areaMapIconGameObject)
		{
			this.m_areaMapIconGameObject.SetActive(true);
		}
		else
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(instance.TeleportPrefab);
			this.m_areaMapIconTransform = gameObject2.transform;
			this.m_areaMapIconGameObject = this.m_areaMapIconTransform.gameObject;
			this.m_areaMapIconHighlightAnimator = this.m_areaMapIconGameObject.transform.FindChild("highlight").GetComponentInChildren<TransparencyAnimator>();
			this.m_areaMapIconTransform.position = AreaMapUI.Instance.Navigation.WorldToMapPosition(this.WorldPosition + Vector3.up * 4f);
			this.m_areaMapIconTransform.parent = AreaMapUI.Instance.FadeOutGroup;
			TransparencyAnimator.Register(this.m_areaMapIconTransform);
		}
	}

	// Token: 0x06003112 RID: 12562 RVA: 0x000D1280 File Offset: 0x000CF480
	public void Update()
	{
		if (this.m_worldMapIconTransform)
		{
			this.m_worldMapIconTransform.position = WorldMapUI.Instance.WorldToUIPosition(this.WorldPosition);
		}
		if (this.m_areaMapIconTransform)
		{
			this.m_areaMapIconTransform.position = AreaMapUI.Instance.Navigation.WorldToMapPosition(this.WorldPosition + Vector3.up * 4f);
		}
	}

	// Token: 0x170007CF RID: 1999
	// (get) Token: 0x06003113 RID: 12563 RVA: 0x000D1301 File Offset: 0x000CF501
	public Vector2 WorldMapIconPosition
	{
		get
		{
			return this.m_worldMapIconTransform.position;
		}
	}

	// Token: 0x170007D0 RID: 2000
	// (get) Token: 0x06003114 RID: 12564 RVA: 0x000D1313 File Offset: 0x000CF513
	public Vector2 AreaMapIconPosition
	{
		get
		{
			return this.m_areaMapIconTransform.position;
		}
	}

	// Token: 0x170007D1 RID: 2001
	// (get) Token: 0x06003115 RID: 12565 RVA: 0x000D1325 File Offset: 0x000CF525
	public Vector2 WorldProjectedPositon
	{
		get
		{
			return WorldMapUI.Instance.WorldToProjectedPosition(this.WorldPosition);
		}
	}

	// Token: 0x170007D2 RID: 2002
	// (get) Token: 0x06003116 RID: 12566 RVA: 0x000D133C File Offset: 0x000CF53C
	public RuntimeGameWorldArea Area
	{
		get
		{
			return GameWorld.Instance.FindRuntimeArea(GameWorld.Instance.FindAreaFromPosition(this.WorldPosition));
		}
	}

	// Token: 0x06003117 RID: 12567 RVA: 0x000D1358 File Offset: 0x000CF558
	public void Hide()
	{
		if (this.m_worldMapIconGameObject)
		{
			this.m_worldMapIconGameObject.SetActive(false);
		}
		if (this.m_areaMapIconGameObject)
		{
			this.m_areaMapIconGameObject.SetActive(false);
		}
	}

	// Token: 0x06003118 RID: 12568 RVA: 0x000D13A0 File Offset: 0x000CF5A0
	public void Highlight()
	{
		if (this.m_worldMapIconHighlightAnimator)
		{
			this.m_worldMapIconHighlightAnimator.AnimatorDriver.ContinueForward();
		}
		if (this.m_areaMapIconHighlightAnimator)
		{
			this.m_areaMapIconHighlightAnimator.AnimatorDriver.ContinueForward();
		}
	}

	// Token: 0x06003119 RID: 12569 RVA: 0x000D13F0 File Offset: 0x000CF5F0
	public void Dehighlight()
	{
		if (this.m_worldMapIconHighlightAnimator)
		{
			this.m_worldMapIconHighlightAnimator.AnimatorDriver.ContinueBackwards();
		}
		if (this.m_areaMapIconHighlightAnimator)
		{
			this.m_areaMapIconHighlightAnimator.AnimatorDriver.ContinueBackwards();
		}
	}

	// Token: 0x04002C5F RID: 11359
	public string Identifier;

	// Token: 0x04002C60 RID: 11360
	public Vector3 WorldPosition;

	// Token: 0x04002C61 RID: 11361
	public bool Activated;

	// Token: 0x04002C62 RID: 11362
	public MessageProvider Name;

	// Token: 0x04002C63 RID: 11363
	private TransparencyAnimator m_worldMapIconHighlightAnimator;

	// Token: 0x04002C64 RID: 11364
	private Transform m_worldMapIconTransform;

	// Token: 0x04002C65 RID: 11365
	private GameObject m_worldMapIconGameObject;

	// Token: 0x04002C66 RID: 11366
	private TransparencyAnimator m_areaMapIconHighlightAnimator;

	// Token: 0x04002C67 RID: 11367
	private Transform m_areaMapIconTransform;

	// Token: 0x04002C68 RID: 11368
	private GameObject m_areaMapIconGameObject;
}
