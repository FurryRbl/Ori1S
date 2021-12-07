using System;
using Core;
using UnityEngine;

// Token: 0x02000872 RID: 2162
public class AreaMapDebugNavigation : MonoBehaviour
{
	// Token: 0x060030E0 RID: 12512 RVA: 0x000D008E File Offset: 0x000CE28E
	public void Awake()
	{
		this.m_areaMapUi = base.GetComponent<AreaMapUI>();
	}

	// Token: 0x060030E1 RID: 12513 RVA: 0x000D009C File Offset: 0x000CE29C
	public void Advance()
	{
		if (XboxLiveController.IsContentPackage)
		{
			return;
		}
		Core.Input.ChargeJump.Used = true;
		if (!DebugMenuB.DebugControlsEnabled)
		{
			return;
		}
		if (Core.Input.RightShoulder.OnPressed)
		{
			if (this.UndiscoveredMapVisible)
			{
				this.ToggleUndiscoveredMap(false);
			}
			else
			{
				this.ToggleUndiscoveredMap(true);
			}
		}
		if (!Core.Input.RightShoulder.IsPressed)
		{
			return;
		}
		if (!this.UndiscoveredMapVisible)
		{
		}
	}

	// Token: 0x060030E2 RID: 12514 RVA: 0x000D0114 File Offset: 0x000CE314
	public void ToggleUndiscoveredMap(bool show)
	{
		this.UndiscoveredMapVisible = show;
		this.m_areaMapUi.ResetMaps();
		this.m_areaMapUi.Navigation.UpdateScrollLimits();
	}

	// Token: 0x04002C23 RID: 11299
	public GameObject DebugSceneBoundsMarkerPrefab;

	// Token: 0x04002C24 RID: 11300
	public float HiddenColorAlpha;

	// Token: 0x04002C25 RID: 11301
	public float UndiscoveredColorAlpha = 0.2f;

	// Token: 0x04002C26 RID: 11302
	private AreaMapUI m_areaMapUi;

	// Token: 0x04002C27 RID: 11303
	public bool UndiscoveredMapVisible;
}
