using System;

// Token: 0x0200087C RID: 2172
public class ChangeWorldMapIconAction : ActionMethod
{
	// Token: 0x06003108 RID: 12552 RVA: 0x000D0F30 File Offset: 0x000CF130
	public override void Perform(IContext context)
	{
		foreach (RuntimeGameWorldArea runtimeGameWorldArea in GameWorld.Instance.RuntimeAreas)
		{
			foreach (RuntimeWorldMapIcon runtimeWorldMapIcon in runtimeGameWorldArea.Icons)
			{
				if (runtimeWorldMapIcon.Guid == this.Target.MoonGuid)
				{
					runtimeWorldMapIcon.SetIcon(this.Icon);
				}
			}
		}
	}

	// Token: 0x04002C55 RID: 11349
	public WorldMapIconType Icon;

	// Token: 0x04002C56 RID: 11350
	[NotNull]
	public VisibleOnWorldMap Target;
}
