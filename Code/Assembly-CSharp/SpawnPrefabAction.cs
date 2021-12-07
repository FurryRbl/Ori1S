using System;

// Token: 0x0200055D RID: 1373
public class SpawnPrefabAction : ActionMethod
{
	// Token: 0x060023CD RID: 9165 RVA: 0x0009C9B1 File Offset: 0x0009ABB1
	public override void Perform(IContext context)
	{
		this.PrefabSource.Spawn(context);
	}

	// Token: 0x060023CE RID: 9166 RVA: 0x0009C9C0 File Offset: 0x0009ABC0
	public override string GetNiceName()
	{
		return "Spawn prefab for " + ActionHelper.GetName(this.PrefabSource);
	}

	// Token: 0x04001DFD RID: 7677
	public PrefabSpawner PrefabSource;
}
