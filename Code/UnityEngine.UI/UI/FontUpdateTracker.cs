﻿using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000046 RID: 70
	public static class FontUpdateTracker
	{
		// Token: 0x06000202 RID: 514 RVA: 0x000087B8 File Offset: 0x000069B8
		public static void TrackText(Text t)
		{
			if (t.font == null)
			{
				return;
			}
			List<Text> list;
			FontUpdateTracker.m_Tracked.TryGetValue(t.font, out list);
			if (list == null)
			{
				if (FontUpdateTracker.m_Tracked.Count == 0)
				{
					Font.textureRebuilt += FontUpdateTracker.RebuildForFont;
				}
				list = new List<Text>();
				FontUpdateTracker.m_Tracked.Add(t.font, list);
			}
			if (!list.Contains(t))
			{
				list.Add(t);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000883C File Offset: 0x00006A3C
		private static void RebuildForFont(Font f)
		{
			List<Text> list;
			FontUpdateTracker.m_Tracked.TryGetValue(f, out list);
			if (list == null)
			{
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				list[i].FontTextureChanged();
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008884 File Offset: 0x00006A84
		public static void UntrackText(Text t)
		{
			if (t.font == null)
			{
				return;
			}
			List<Text> list;
			FontUpdateTracker.m_Tracked.TryGetValue(t.font, out list);
			if (list == null)
			{
				return;
			}
			list.Remove(t);
			if (list.Count == 0)
			{
				FontUpdateTracker.m_Tracked.Remove(t.font);
				if (FontUpdateTracker.m_Tracked.Count == 0)
				{
					Font.textureRebuilt -= FontUpdateTracker.RebuildForFont;
				}
			}
		}

		// Token: 0x040000EE RID: 238
		private static Dictionary<Font, List<Text>> m_Tracked = new Dictionary<Font, List<Text>>();
	}
}
