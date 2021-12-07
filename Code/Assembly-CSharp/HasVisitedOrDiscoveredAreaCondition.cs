using System;

// Token: 0x02000886 RID: 2182
public class HasVisitedOrDiscoveredAreaCondition : Condition
{
	// Token: 0x06003125 RID: 12581 RVA: 0x000D1654 File Offset: 0x000CF854
	public override bool Validate(IContext context)
	{
		if (AreaMapUI.Instance && AreaMapUI.Instance.DebugNavigation.UndiscoveredMapVisible)
		{
			return this.Visited;
		}
		if (this.m_area == null)
		{
			this.m_area = GameWorld.Instance.FindRuntimeArea(this.Area);
		}
		return this.m_area != null && ((!this.Visited) ? (!this.m_area.AreaDiscovered) : this.m_area.AreaDiscovered);
	}

	// Token: 0x04002C77 RID: 11383
	public GameWorldArea Area;

	// Token: 0x04002C78 RID: 11384
	public bool Visited = true;

	// Token: 0x04002C79 RID: 11385
	private RuntimeGameWorldArea m_area;
}
