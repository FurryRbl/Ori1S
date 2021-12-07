using System;

// Token: 0x02000631 RID: 1585
public class SpawnOrbsAction : ActionMethod
{
	// Token: 0x06002701 RID: 9985 RVA: 0x000AA737 File Offset: 0x000A8937
	public override void Perform(IContext context)
	{
		if (this.OrbSpawner)
		{
			this.OrbSpawner.SpawnOrbs(context);
		}
	}

	// Token: 0x17000628 RID: 1576
	// (get) Token: 0x06002702 RID: 9986 RVA: 0x000AA758 File Offset: 0x000A8958
	private string TargetName
	{
		get
		{
			return (!(this.OrbSpawner != null)) ? "unkown" : this.OrbSpawner.name;
		}
	}

	// Token: 0x06002703 RID: 9987 RVA: 0x000AA78B File Offset: 0x000A898B
	public override string GetNiceName()
	{
		return "Spawn orbs from " + this.TargetName;
	}

	// Token: 0x040021A5 RID: 8613
	public OrbSpawner OrbSpawner;
}
