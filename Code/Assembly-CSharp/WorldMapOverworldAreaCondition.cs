using System;

// Token: 0x0200089F RID: 2207
public class WorldMapOverworldAreaCondition : Condition
{
	// Token: 0x0600316C RID: 12652 RVA: 0x000D2EE5 File Offset: 0x000D10E5
	public override bool Validate(IContext context)
	{
		return base.GetComponent<WorldMapOverworldArea>().IsDiscovered || AreaMapUI.Instance.DebugNavigation.UndiscoveredMapVisible;
	}
}
