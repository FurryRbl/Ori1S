using System;
using Game;
using UnityEngine;

// Token: 0x020000A7 RID: 167
public class WorldMapUI : MonoBehaviour
{
	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001DBB3 File Offset: 0x0001BDB3
	public static bool IsReady
	{
		get
		{
			return WorldMapUI.Instance != null;
		}
	}

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001DBC0 File Offset: 0x0001BDC0
	public static bool UseCameraSettings
	{
		get
		{
			return !(WorldMapUI.Instance == null) && WorldMapUI.Instance.m_enabled;
		}
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x0001DBDE File Offset: 0x0001BDDE
	public void OnEnable()
	{
		this.m_enabled = true;
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x0001DBE7 File Offset: 0x0001BDE7
	public void OnDisable()
	{
		this.m_enabled = false;
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001DBF0 File Offset: 0x0001BDF0
	public static CameraSettings CameraSettings
	{
		get
		{
			if (WorldMapUI.Instance == null)
			{
				return null;
			}
			if (WorldMapUI.Instance.m_cameraSettings == null)
			{
				WorldMapUI.Instance.m_cameraSettings = new CameraSettings(WorldMapUI.Instance.CameraSettingsAsset, WorldMapUI.Instance.Fog);
			}
			return WorldMapUI.Instance.m_cameraSettings;
		}
	}

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x06000741 RID: 1857 RVA: 0x0001DC4B File Offset: 0x0001BE4B
	public Transform FadeOutGroup
	{
		get
		{
			return this.CrossFade.transform;
		}
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x0001DC58 File Offset: 0x0001BE58
	public void Awake()
	{
		if (WorldMapUI.Instance != null)
		{
		}
		WorldMapUI.Instance = this;
		base.transform.parent = GameMapUI.Instance.Group;
		base.transform.position = Vector3.zero;
		CleverMenuItemSelectionManager navigationManager = this.NavigationManager;
		navigationManager.OptionChangeCallback = (Action)Delegate.Combine(navigationManager.OptionChangeCallback, new Action(this.OnMenuItemChange));
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x0001DCC8 File Offset: 0x0001BEC8
	public void OnMenuItemChange()
	{
		if (this.m_ignoreNavigationMenuItemChange)
		{
			return;
		}
		WorldMapOverworldArea currentArea = this.CurrentArea;
		AreaMapUI.Instance.Navigation.ScrollPosition = currentArea.ScrollPosition;
		GameMapUI.Instance.CurrentHighlightedArea = GameWorld.Instance.FindRuntimeArea(currentArea.Area);
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x0001DD1C File Offset: 0x0001BF1C
	public void OnDestroy()
	{
		if (WorldMapUI.Instance == this)
		{
			UnityEngine.Object.DestroyObject(WorldMapUI.Instance);
		}
		CleverMenuItemSelectionManager navigationManager = this.NavigationManager;
		navigationManager.OptionChangeCallback = (Action)Delegate.Remove(navigationManager.OptionChangeCallback, new Action(this.OnMenuItemChange));
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x0001DD6C File Offset: 0x0001BF6C
	public void Activate()
	{
		base.gameObject.SetActive(true);
		if (!GameMapUI.Instance.ShowingTeleporters)
		{
			this.ShowAreaSelection();
		}
		this.m_ignoreNavigationMenuItemChange = true;
		foreach (WorldMapOverworldArea worldMapOverworldArea in this.NavigationManager.GetComponentsInChildren<WorldMapOverworldArea>())
		{
			if (worldMapOverworldArea.Area == GameMapUI.Instance.CurrentHighlightedArea.Area)
			{
				this.NavigationManager.SetCurrentMenuItem(worldMapOverworldArea.GetComponent<CleverMenuItem>());
			}
		}
		this.m_ignoreNavigationMenuItemChange = false;
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x0001DDFC File Offset: 0x0001BFFC
	public void Deactivate()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001DE0A File Offset: 0x0001C00A
	public float ZoomTime
	{
		get
		{
			return AreaMapUI.Instance.Navigation.ZoomTime;
		}
	}

	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001DE1B File Offset: 0x0001C01B
	public Vector3 ScrollPosition
	{
		get
		{
			return AreaMapUI.Instance.Navigation.ScrollPosition;
		}
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x0001DE31 File Offset: 0x0001C031
	public Vector3 WorldToProjectedPosition(Vector3 position)
	{
		return WorldMapOverworldLogic.Instance.WorldToOverworld(position);
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x0001DE3E File Offset: 0x0001C03E
	public Vector3 WorldToUIPosition(Vector3 position)
	{
		return this.WorldToScreenToUI(this.WorldToProjectedPosition(position));
	}

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001DE50 File Offset: 0x0001C050
	public Vector3 ClosePosition
	{
		get
		{
			return this.WorldToProjectedPosition(this.ScrollPosition) + Vector3.back * this.CloseZoom;
		}
	}

	// Token: 0x170001AA RID: 426
	// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001DE7E File Offset: 0x0001C07E
	public Vector3 FarPosition
	{
		get
		{
			return Vector3.back * this.FullZoom + this.CameraOffset;
		}
	}

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001DE9B File Offset: 0x0001C09B
	public WorldMapOverworldArea CurrentArea
	{
		get
		{
			return this.NavigationManager.CurrentMenuItem.GetComponent<WorldMapOverworldArea>();
		}
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
	public void UpdateCameraPosition()
	{
		if (!GameMapUI.Instance.ShowingObjective)
		{
			if (!GameMapUI.Instance.RevealingMap)
			{
				if (GameMapUI.Instance.ShowingTeleporters)
				{
					Vector3 b = GameMapUI.Instance.Teleporters.SelectedTeleporter.WorldProjectedPositon;
					b.z = -2f;
					b.x *= 0.3f;
					b.y *= 0.1f;
					b.y -= 3f;
					this.CameraOffset = Vector3.Lerp(this.CameraOffset, b, 0.03f);
				}
				else if (this.NavigationManager.gameObject.activeSelf)
				{
					Vector3 position = this.CurrentArea.transform.position;
					position.z = 0f;
					Vector3 b2 = position * 0.2f;
					this.CameraOffset = Vector3.Lerp(this.CameraOffset, b2, 0.03f);
				}
			}
		}
		Vector3 vector = this.FarPosition + Vector3.right * 0.3f * Mathf.Sin(6.2831855f * Time.time / 4.2f) + Vector3.up * 0.2f * Mathf.Cos(6.2831855f * Time.time / 7.3f);
		Vector3 closePosition = this.ClosePosition;
		float zoomTime = this.ZoomTime;
		Vector3 position2;
		position2.x = Mathf.Lerp(vector.x, closePosition.x, this.ZoomXYCurve.Evaluate(zoomTime));
		position2.y = Mathf.Lerp(vector.y, closePosition.y, this.ZoomXYCurve.Evaluate(zoomTime));
		position2.z = Mathf.Lerp(vector.z, closePosition.z, this.ZoomZCurve.Evaluate(zoomTime));
		this.Camera.transform.position = position2;
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0001E0C0 File Offset: 0x0001C2C0
	public void FixedUpdate()
	{
		if (!GameMapUI.Instance.IsVisible)
		{
			return;
		}
		this.NavigationManager.IsActive = GameMapTransitionManager.Instance.InWorldMapMode;
		this.UpdateCameraPosition();
		if (Characters.Sein)
		{
			this.PlayerMarker.position = this.WorldToUIPosition(Characters.Sein.Position);
		}
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x0001E124 File Offset: 0x0001C324
	public Vector3 WorldToScreenToUI(Vector3 position)
	{
		Vector2 v = this.Camera.WorldToScreenPoint(position);
		Camera camera = UI.Cameras.System.GUICamera.Camera;
		Vector3 result = camera.ScreenToWorldPoint(v);
		result.z = 0f;
		return result;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x0001E16D File Offset: 0x0001C36D
	public void ShowAreaSelection()
	{
		this.NavigationManager.gameObject.SetActive(true);
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0001E180 File Offset: 0x0001C380
	public void HideAreaSelection()
	{
		this.NavigationManager.gameObject.SetActive(false);
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x0001E194 File Offset: 0x0001C394
	public static void Initialize()
	{
		if (WorldMapUI.m_isLoadingWorldMapScene)
		{
			WorldMapUI.m_cancelLoading = false;
		}
		else
		{
			WorldMapUI.m_isLoadingWorldMapScene = true;
			Application.LoadLevelAdditiveAsync("worldMapScene");
		}
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0001E1C8 File Offset: 0x0001C3C8
	public static void OnFinishedLoading(SceneRoot sceneRoot)
	{
		if (WorldMapUI.m_cancelLoading)
		{
			UnityEngine.Object.DestroyObject(sceneRoot.gameObject);
		}
		else
		{
			sceneRoot.EarlyStart();
			UnityEngine.Object.DestroyObject(sceneRoot.GetComponent<SaveSceneManager>());
			UnityEngine.Object.DestroyObject(sceneRoot.GetComponent<SceneSettingsComponent>());
			UnityEngine.Object.DestroyObject(sceneRoot);
			sceneRoot.gameObject.SetActive(true);
		}
		WorldMapUI.m_isLoadingWorldMapScene = false;
		WorldMapUI.m_cancelLoading = false;
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x0001E229 File Offset: 0x0001C429
	public static void CancelLoading()
	{
		if (WorldMapUI.m_isLoadingWorldMapScene)
		{
			WorldMapUI.m_cancelLoading = true;
		}
	}

	// Token: 0x04000551 RID: 1361
	public static WorldMapUI Instance;

	// Token: 0x04000552 RID: 1362
	public Transform ProjectionPlane;

	// Token: 0x04000553 RID: 1363
	public Transform PlayerMarker;

	// Token: 0x04000554 RID: 1364
	public bool Activated;

	// Token: 0x04000555 RID: 1365
	public float FullZoom = 20f;

	// Token: 0x04000556 RID: 1366
	public float CloseZoom = 10f;

	// Token: 0x04000557 RID: 1367
	public Camera Camera;

	// Token: 0x04000558 RID: 1368
	public TransparencyAnimator CrossFade;

	// Token: 0x04000559 RID: 1369
	public CleverMenuItemSelectionManager NavigationManager;

	// Token: 0x0400055A RID: 1370
	public CameraSettingsAsset CameraSettingsAsset;

	// Token: 0x0400055B RID: 1371
	public FogGradientController Fog;

	// Token: 0x0400055C RID: 1372
	public AnimationCurve ZoomXYCurve;

	// Token: 0x0400055D RID: 1373
	public AnimationCurve ZoomZCurve;

	// Token: 0x0400055E RID: 1374
	public Vector3 CameraOffset;

	// Token: 0x0400055F RID: 1375
	private CameraSettings m_cameraSettings;

	// Token: 0x04000560 RID: 1376
	private bool m_enabled;

	// Token: 0x04000561 RID: 1377
	private bool m_ignoreNavigationMenuItemChange;

	// Token: 0x04000562 RID: 1378
	private static bool m_isLoadingWorldMapScene;

	// Token: 0x04000563 RID: 1379
	private static bool m_cancelLoading;
}
