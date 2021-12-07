using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Game;
using UnityEngine;

// Token: 0x020000A5 RID: 165
public class AreaMapUI : MonoBehaviour, ISuspendable
{
	// Token: 0x17000196 RID: 406
	// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001CB34 File Offset: 0x0001AD34
	// (set) Token: 0x060006FD RID: 1789 RVA: 0x0001CB3C File Offset: 0x0001AD3C
	public GameObject PlayerPositionMarker { get; set; }

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001CB45 File Offset: 0x0001AD45
	// (set) Token: 0x060006FF RID: 1791 RVA: 0x0001CB4D File Offset: 0x0001AD4D
	public GameObject SoulFlamePositionMarker { get; set; }

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001CB56 File Offset: 0x0001AD56
	// (set) Token: 0x06000701 RID: 1793 RVA: 0x0001CB5E File Offset: 0x0001AD5E
	public AreaMapDebugNavigation DebugNavigation { get; set; }

	// Token: 0x17000199 RID: 409
	// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001CB67 File Offset: 0x0001AD67
	// (set) Token: 0x06000703 RID: 1795 RVA: 0x0001CB6F File Offset: 0x0001AD6F
	public AreaMapNavigation Navigation { get; set; }

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001CB78 File Offset: 0x0001AD78
	// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001CB80 File Offset: 0x0001AD80
	public AreaMapIconManager IconManager { get; set; }

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001CB89 File Offset: 0x0001AD89
	public Transform FadeOutGroup
	{
		get
		{
			return this.FadeOutAnimator.transform;
		}
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0001CB96 File Offset: 0x0001AD96
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0001CBA4 File Offset: 0x0001ADA4
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0001CBB4 File Offset: 0x0001ADB4
	public void ResetMaps()
	{
		foreach (AreaMapCanvas areaMapCanvas in this.Canvases)
		{
			areaMapCanvas.ResetMap();
		}
		foreach (AreaMapCanvasOverlay areaMapCanvasOverlay in base.GetComponentsInChildren<AreaMapCanvasOverlay>(true))
		{
			areaMapCanvasOverlay.ApplyMasks();
		}
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0001CC38 File Offset: 0x0001AE38
	public void Awake()
	{
		AreaMapUI.Instance = this;
		this.DebugNavigation = base.GetComponent<AreaMapDebugNavigation>();
		this.Navigation = base.GetComponent<AreaMapNavigation>();
		this.IconManager = base.GetComponent<AreaMapIconManager>();
		SuspensionManager.Register(this);
		this.AreaMapLegend.HideSilently();
		if (this.PlayerPositionMarker == null)
		{
			this.PlayerPositionMarker = UnityEngine.Object.Instantiate<GameObject>(this.PlayerPositionMarkerPrefab);
			this.PlayerPositionMarker.transform.parent = this.FadeOutGroup;
			TransparencyAnimator.Register(this.PlayerPositionMarker.transform);
		}
		if (this.SoulFlamePositionMarker == null)
		{
			this.SoulFlamePositionMarker = UnityEngine.Object.Instantiate<GameObject>(this.SoulFlamePositionMarkerPrefab);
			this.SoulFlamePositionMarker.transform.parent = this.FadeOutGroup;
			TransparencyAnimator.Register(this.SoulFlamePositionMarker.transform);
		}
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0001CD10 File Offset: 0x0001AF10
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		AreaMapUI.Instance = null;
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0001CD20 File Offset: 0x0001AF20
	public AreaMapCanvas FindCanvas(GameWorldArea area)
	{
		return this.Canvases.FirstOrDefault((AreaMapCanvas canvas) => canvas.Area == area);
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x0001CD54 File Offset: 0x0001AF54
	public void Init()
	{
		this.ResetMaps();
		this.IconManager.ShowAreaIcons();
		this.Navigation.Advance();
		this.Navigation.UpdateScrollLimits();
		this.PlayerPositionOffset = Vector2.zero;
		this.Navigation.Init();
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0001CDA4 File Offset: 0x0001AFA4
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (!GameMapUI.Instance.IsVisible)
		{
			return;
		}
		this.Navigation.Advance();
		this.DebugNavigation.Advance();
		this.UpdatePlayerPositionMarker();
		this.UpdateSoulFlamePositionMarker();
		this.UpdateCurrentArea();
		if (GameWorld.Instance.ObjectiveText && !GameMapUI.Instance.ShowingObjective)
		{
			this.ObjectiveText.SetMessage(new MessageDescriptor(string.Concat(new object[]
			{
				"#",
				this.ObjectiveMessageProvider,
				"#: ",
				GameWorld.Instance.ObjectiveText.GetMessages().First<MessageDescriptor>().Message
			})));
			this.ObjectiveText.gameObject.SetActive(true);
		}
		else
		{
			this.ObjectiveText.gameObject.SetActive(false);
		}
		if (GameMapTransitionManager.Instance.InAreaMapMode && Core.Input.Legend.OnPressed)
		{
			this.AreaMapLegend.Toggle();
		}
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x0001CEBC File Offset: 0x0001B0BC
	public void UpdateCurrentArea()
	{
		Vector2 scrollPosition = this.Navigation.ScrollPosition;
		foreach (RuntimeGameWorldArea runtimeGameWorldArea in GameWorld.Instance.RuntimeAreas)
		{
			if ((runtimeGameWorldArea.AreaDiscovered || this.DebugNavigation.UndiscoveredMapVisible) && runtimeGameWorldArea.Area.BoundaryCage.FindFaceAtPositionFaster(scrollPosition) != null)
			{
				if (GameMapUI.Instance.CurrentHighlightedArea != runtimeGameWorldArea && this.ChangeSelectedAreaSound)
				{
					Sound.Play(this.ChangeSelectedAreaSound.GetSound(null), base.transform.position, null);
				}
				GameMapUI.Instance.CurrentHighlightedArea = runtimeGameWorldArea;
				break;
			}
		}
	}

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001CFA4 File Offset: 0x0001B1A4
	public Vector3 PlayerMarkerWorldPosition
	{
		get
		{
			Transform target = UI.Cameras.Current.Target;
			return target.position + this.PlayerPositionOffset + Vector3.up;
		}
	}

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001CFD8 File Offset: 0x0001B1D8
	public Vector3 SoulFlameMarkerWorldPosition
	{
		get
		{
			return Characters.Sein.SoulFlame.SoulFlamePosition + this.PlayerPositionOffset + Vector3.up;
		}
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x0001D00C File Offset: 0x0001B20C
	private void UpdatePlayerPositionMarker()
	{
		if (this.PlayerPositionMarker)
		{
			this.PlayerPositionMarker.transform.localPosition = this.Navigation.WorldToMapPosition(this.PlayerMarkerWorldPosition);
		}
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x0001D050 File Offset: 0x0001B250
	private void UpdateSoulFlamePositionMarker()
	{
		if (this.SoulFlamePositionMarker == null)
		{
			return;
		}
		if (Characters.Sein)
		{
			if (Characters.Sein.SoulFlame.SoulFlameExists)
			{
				this.SoulFlamePositionMarker.SetActive(true);
				this.SoulFlamePositionMarker.transform.localPosition = this.Navigation.WorldToMapPosition(this.SoulFlameMarkerWorldPosition);
			}
			else
			{
				this.SoulFlamePositionMarker.SetActive(false);
			}
		}
	}

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001D0D5 File Offset: 0x0001B2D5
	// (set) Token: 0x06000715 RID: 1813 RVA: 0x0001D0DD File Offset: 0x0001B2DD
	public bool IsSuspended { get; set; }

	// Token: 0x04000530 RID: 1328
	public static AreaMapUI Instance;

	// Token: 0x04000531 RID: 1329
	public List<AreaMapCanvas> Canvases = new List<AreaMapCanvas>();

	// Token: 0x04000532 RID: 1330
	public GameObject PlayerPositionMarkerPrefab;

	// Token: 0x04000533 RID: 1331
	public GameObject SoulFlamePositionMarkerPrefab;

	// Token: 0x04000534 RID: 1332
	public GameObject TeleportPrefab;

	// Token: 0x04000535 RID: 1333
	public GameObject ObjectivePrefab;

	// Token: 0x04000536 RID: 1334
	public GameObject IconPrefab;

	// Token: 0x04000537 RID: 1335
	public SoundProvider OpenSound;

	// Token: 0x04000538 RID: 1336
	public SoundProvider CloseSound;

	// Token: 0x04000539 RID: 1337
	public SoundProvider ChangeSelectedAreaSound;

	// Token: 0x0400053A RID: 1338
	public MessageBox ObjectiveText;

	// Token: 0x0400053B RID: 1339
	public TransparencyAnimator FadeOutAnimator;

	// Token: 0x0400053C RID: 1340
	public AreaMapLegend AreaMapLegend;

	// Token: 0x0400053D RID: 1341
	public MessageProvider ObjectiveMessageProvider;

	// Token: 0x0400053E RID: 1342
	public MessageProvider CompletedMessageProvider;

	// Token: 0x0400053F RID: 1343
	public Vector3 PlayerPositionOffset;
}
