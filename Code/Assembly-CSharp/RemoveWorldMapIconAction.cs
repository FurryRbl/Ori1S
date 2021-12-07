using System;

// Token: 0x02000893 RID: 2195
public class RemoveWorldMapIconAction : ActionMethod
{
	// Token: 0x0600314C RID: 12620 RVA: 0x000D1DF0 File Offset: 0x000CFFF0
	public override void Perform(IContext context)
	{
		foreach (RuntimeGameWorldArea runtimeGameWorldArea in GameWorld.Instance.RuntimeAreas)
		{
			RuntimeWorldMapIcon runtimeWorldMapIcon = runtimeGameWorldArea.Icons.Find((RuntimeWorldMapIcon a) => a.Guid == this.Target.MoonGuid);
			if (runtimeWorldMapIcon != null)
			{
				runtimeWorldMapIcon.Hide();
				runtimeWorldMapIcon.Icon = WorldMapIconType.Invisible;
			}
		}
	}

	// Token: 0x04002C9C RID: 11420
	public VisibleOnWorldMap Target;
}
