using System;
using Game;
using UnityEngine;

// Token: 0x02000876 RID: 2166
public class RuntimeWorldMapIcon
{
	// Token: 0x060030ED RID: 12525 RVA: 0x000D07B3 File Offset: 0x000CE9B3
	public RuntimeWorldMapIcon(GameWorldArea.WorldMapIcon icon, RuntimeGameWorldArea area)
	{
		this.Icon = icon.Icon;
		this.Guid = icon.Guid;
		this.Position = icon.Position;
		this.Area = area;
		this.IsSecret = icon.IsSecret;
	}

	// Token: 0x060030EE RID: 12526 RVA: 0x000D07F4 File Offset: 0x000CE9F4
	public bool IsVisible(AreaMapUI areaMap)
	{
		return (!this.Area.IsHidden(this.Position) || GameWorld.Instance.IconRevealed(this.Guid) || areaMap.DebugNavigation.UndiscoveredMapVisible) && (this.Icon != WorldMapIconType.EnergyUpgrade || Characters.Sein.PlayerAbilities.EnergyMarkers.HasAbility) && (this.Icon != WorldMapIconType.HealthUpgrade || Characters.Sein.PlayerAbilities.HealthMarkers.HasAbility) && (this.Icon != WorldMapIconType.AbilityPoint || Characters.Sein.PlayerAbilities.AbilityMarkers.HasAbility) && Characters.Sein.PlayerAbilities.MapMarkers.HasAbility && (!this.IsSecret || Characters.Sein.PlayerAbilities.Sense.HasAbility);
	}

	// Token: 0x060030EF RID: 12527 RVA: 0x000D0900 File Offset: 0x000CEB00
	public void Show()
	{
		AreaMapUI instance = AreaMapUI.Instance;
		if (this.Icon == WorldMapIconType.Invisible)
		{
			return;
		}
		if (!this.IsVisible(instance))
		{
			return;
		}
		if (this.m_iconGameObject)
		{
			this.m_iconGameObject.SetActive(true);
		}
		else
		{
			GameObject icon = instance.IconManager.GetIcon(this.Icon);
			this.m_iconGameObject = (GameObject)InstantiateUtility.Instantiate(icon);
			Transform transform = this.m_iconGameObject.transform;
			transform.parent = instance.Navigation.MapPivot.transform;
			transform.localPosition = this.Position;
			transform.localRotation = Quaternion.identity;
			transform.localScale = icon.transform.localScale;
			TransparencyAnimator.Register(transform);
		}
	}

	// Token: 0x060030F0 RID: 12528 RVA: 0x000D09C7 File Offset: 0x000CEBC7
	public void Hide()
	{
		if (this.m_iconGameObject)
		{
			this.m_iconGameObject.SetActive(false);
		}
	}

	// Token: 0x060030F1 RID: 12529 RVA: 0x000D09E8 File Offset: 0x000CEBE8
	public void SetIcon(WorldMapIconType icon)
	{
		if (this.m_iconGameObject)
		{
			InstantiateUtility.Destroy(this.m_iconGameObject);
		}
		this.Icon = icon;
	}

	// Token: 0x04002C46 RID: 11334
	public MoonGuid Guid;

	// Token: 0x04002C47 RID: 11335
	public WorldMapIconType Icon;

	// Token: 0x04002C48 RID: 11336
	public Vector2 Position;

	// Token: 0x04002C49 RID: 11337
	private RuntimeGameWorldArea Area;

	// Token: 0x04002C4A RID: 11338
	public bool IsSecret;

	// Token: 0x04002C4B RID: 11339
	private GameObject m_iconGameObject;
}
