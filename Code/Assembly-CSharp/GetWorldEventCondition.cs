using System;
using System.Collections.Generic;
using Game;

// Token: 0x02000760 RID: 1888
public class GetWorldEventCondition : Condition
{
	// Token: 0x06002C1B RID: 11291 RVA: 0x000BD130 File Offset: 0x000BB330
	public override bool Validate(IContext context)
	{
		WorldEventsRuntime worldEventsRuntime = World.Events.Find(this.WorldEvents);
		int value = worldEventsRuntime.Value;
		if (this.States.Count == 0)
		{
			return this.State == value;
		}
		for (int i = 0; i < this.States.Count; i++)
		{
			int num = this.States[i];
			if (value == num)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040027E2 RID: 10210
	public WorldEvents WorldEvents;

	// Token: 0x040027E3 RID: 10211
	public int State;

	// Token: 0x040027E4 RID: 10212
	public List<int> States = new List<int>();
}
