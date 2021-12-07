using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class WorldEventsOnAwake : MonoBehaviour
{
	// Token: 0x06000795 RID: 1941 RVA: 0x0001FE38 File Offset: 0x0001E038
	public void Apply()
	{
		foreach (WorldEventsOnAwake.WorldEventState worldEventState in this.WorldEventStates)
		{
			World.Events.Find(worldEventState.WorldEvent).Value = worldEventState.State;
		}
	}

	// Token: 0x040005DF RID: 1503
	public List<WorldEventsOnAwake.WorldEventState> WorldEventStates = new List<WorldEventsOnAwake.WorldEventState>();

	// Token: 0x020008A8 RID: 2216
	[Serializable]
	public class WorldEventState
	{
		// Token: 0x04002CC3 RID: 11459
		public WorldEvents WorldEvent;

		// Token: 0x04002CC4 RID: 11460
		public int State;
	}
}
