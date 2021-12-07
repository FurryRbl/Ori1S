using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000883 RID: 2179
public class WorldEventsManager
{
	// Token: 0x0600311E RID: 12574 RVA: 0x000D1482 File Offset: 0x000CF682
	public void OnGameReset()
	{
		this.m_worldEvents.Clear();
		this.All.Clear();
	}

	// Token: 0x0600311F RID: 12575 RVA: 0x000D149C File Offset: 0x000CF69C
	public void Serialize(Archive ar)
	{
		int num = ar.Serialize(this.m_worldEvents.Count);
		if (ar.Writing)
		{
			foreach (KeyValuePair<MoonGuid, WorldEventsRuntime> keyValuePair in this.m_worldEvents)
			{
				new MoonGuid(keyValuePair.Key).Serialize(ar);
				keyValuePair.Value.Serialize(ar);
			}
		}
		else
		{
			this.m_worldEvents.Clear();
			for (int i = 0; i < num; i++)
			{
				MoonGuid moonGuid = new MoonGuid(MoonGuid.Empty);
				moonGuid.Serialize(ar);
				WorldEventsRuntime worldEventsRuntime = new WorldEventsRuntime(0);
				worldEventsRuntime.Serialize(ar);
				this.m_worldEvents[moonGuid] = worldEventsRuntime;
			}
		}
	}

	// Token: 0x06003120 RID: 12576 RVA: 0x000D1580 File Offset: 0x000CF780
	public WorldEventsRuntime Find(WorldEvents worldEvents)
	{
		WorldEventsRuntime worldEventsRuntime;
		if (!this.m_worldEvents.TryGetValue(worldEvents.GetGuid(), out worldEventsRuntime))
		{
			int defaultValue = worldEvents.DefaultValue;
			worldEventsRuntime = new WorldEventsRuntime(defaultValue);
			this.m_worldEvents.Add(worldEvents.GetGuid(), worldEventsRuntime);
			if (Application.isPlaying && DebugMenuB.Instance)
			{
				DebugMenuB.Instance.AddWorldEvent(worldEvents);
			}
		}
		return worldEventsRuntime;
	}

	// Token: 0x04002C6F RID: 11375
	public static WorldEventsManager Instance = new WorldEventsManager();

	// Token: 0x04002C70 RID: 11376
	private readonly Dictionary<MoonGuid, WorldEventsRuntime> m_worldEvents = new Dictionary<MoonGuid, WorldEventsRuntime>();

	// Token: 0x04002C71 RID: 11377
	public readonly List<WorldEventsRuntime> All = new List<WorldEventsRuntime>();
}
