using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000019 RID: 25
public static class SuspensionManager
{
	// Token: 0x0600014E RID: 334 RVA: 0x00006588 File Offset: 0x00004788
	public static void Register(ISuspendable suspendable)
	{
		if (!SuspensionManager.s_suspended.ContainsKey(suspendable))
		{
			SuspensionManager.s_suspended.Add(suspendable, 0);
			SuspensionManager.s_suspendedIterate.Add(suspendable);
		}
	}

	// Token: 0x0600014F RID: 335 RVA: 0x000065BD File Offset: 0x000047BD
	public static void Unregister(ISuspendable suspendable)
	{
		SuspensionManager.s_suspended.Remove(suspendable);
		SuspensionManager.s_suspendedIterate.Remove(suspendable);
	}

	// Token: 0x06000150 RID: 336 RVA: 0x000065D8 File Offset: 0x000047D8
	private static bool FindOrAddToSuspended(ISuspendable suspendable, out int count)
	{
		if (suspendable as Component == null)
		{
			count = 0;
			return false;
		}
		return SuspensionManager.s_suspended.TryGetValue(suspendable, out count);
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00006610 File Offset: 0x00004810
	public static void SuspendAll()
	{
		foreach (ISuspendable suspendable in SuspensionManager.s_suspendedIterate)
		{
			SuspensionManager.Suspend(suspendable);
		}
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00006668 File Offset: 0x00004868
	public static void GetSuspendables(HashSet<ISuspendable> suspendables, GameObject go)
	{
		suspendables.Clear();
		foreach (Component component in go.FindComponentsInChildren<ISuspendable>())
		{
			suspendables.Add((ISuspendable)component);
		}
	}

	// Token: 0x06000153 RID: 339 RVA: 0x000066A8 File Offset: 0x000048A8
	public static HashSet<ISuspendable> GetSuspendables(HashSet<ISuspendable> suspendables, params GameObject[] gos)
	{
		suspendables.Clear();
		foreach (GameObject gameObject in gos)
		{
			foreach (Component component in gameObject.FindComponentsInChildren<ISuspendable>())
			{
				ISuspendable item = (ISuspendable)component;
				if (!suspendables.Contains(item))
				{
					suspendables.Add(item);
				}
			}
		}
		return suspendables;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x0000671C File Offset: 0x0000491C
	public static HashSet<ISuspendable> GetSuspendables(HashSet<ISuspendable> suspendables, bool includeInactive, GameObject go)
	{
		suspendables.Clear();
		go.GetComponentsInChildren<ISuspendable>(includeInactive, SuspensionManager.s_suspenableList);
		int count = SuspensionManager.s_suspenableList.Count;
		for (int i = 0; i < count; i++)
		{
			ISuspendable item = SuspensionManager.s_suspenableList[i];
			if (!suspendables.Contains(item))
			{
				suspendables.Add(item);
			}
		}
		SuspensionManager.s_suspenableList.Clear();
		return suspendables;
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00006784 File Offset: 0x00004984
	public static void Suspend(ISuspendable suspendable)
	{
		int num;
		bool flag = SuspensionManager.FindOrAddToSuspended(suspendable, out num);
		if (flag)
		{
			try
			{
				num++;
				if (num == 1)
				{
					suspendable.IsSuspended = true;
				}
			}
			catch (Exception ex)
			{
			}
			SuspensionManager.s_suspended[suspendable] = num;
		}
	}

	// Token: 0x06000156 RID: 342 RVA: 0x000067DC File Offset: 0x000049DC
	public static void Resume(ISuspendable suspendable)
	{
		int num;
		bool flag = SuspensionManager.FindOrAddToSuspended(suspendable, out num);
		if (flag)
		{
			try
			{
				if (num == 0)
				{
					return;
				}
				num--;
				if (num == 0)
				{
					suspendable.IsSuspended = false;
				}
			}
			catch (Exception ex)
			{
			}
			SuspensionManager.s_suspended[suspendable] = num;
		}
	}

	// Token: 0x06000157 RID: 343 RVA: 0x0000683C File Offset: 0x00004A3C
	public static void Suspend(List<ISuspendable> suspendables)
	{
		for (int i = 0; i < suspendables.Count; i++)
		{
			ISuspendable suspendable = suspendables[i];
			SuspensionManager.Suspend(suspendable);
		}
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00006870 File Offset: 0x00004A70
	public static void Suspend(HashSet<ISuspendable> suspendables)
	{
		foreach (ISuspendable suspendable in suspendables)
		{
			SuspensionManager.Suspend(suspendable);
		}
	}

	// Token: 0x06000159 RID: 345 RVA: 0x000068C4 File Offset: 0x00004AC4
	public static void CleanupSuspendables()
	{
		SuspensionManager.s_suspendRemove.Clear();
		foreach (ISuspendable suspendable in SuspensionManager.s_suspended.Keys)
		{
			if (suspendable as Component == null)
			{
				SuspensionManager.s_suspendRemove.Add(suspendable);
				if (suspendable != null)
				{
				}
			}
		}
		for (int i = 0; i < SuspensionManager.s_suspendRemove.Count; i++)
		{
			ISuspendable suspendable2 = SuspensionManager.s_suspendRemove[i];
			SuspensionManager.s_suspended.Remove(suspendable2);
			SuspensionManager.s_suspendedIterate.Remove(suspendable2);
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00006988 File Offset: 0x00004B88
	public static void ResumeAll()
	{
		foreach (ISuspendable suspendable in SuspensionManager.s_suspendedIterate)
		{
			SuspensionManager.Resume(suspendable);
		}
	}

	// Token: 0x0600015B RID: 347 RVA: 0x000069E0 File Offset: 0x00004BE0
	public static void ResumeExcluding(HashSet<ISuspendable> exclude)
	{
		foreach (ISuspendable suspendable in SuspensionManager.s_suspendedIterate)
		{
			if (!exclude.Contains(suspendable))
			{
				SuspensionManager.Resume(suspendable);
			}
		}
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00006A44 File Offset: 0x00004C44
	public static void SuspendExcluding(HashSet<ISuspendable> exclude)
	{
		foreach (ISuspendable suspendable in SuspensionManager.s_suspendedIterate)
		{
			if (!exclude.Contains(suspendable))
			{
				SuspensionManager.Suspend(suspendable);
			}
		}
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00006AA8 File Offset: 0x00004CA8
	public static void Resume(HashSet<ISuspendable> suspendables)
	{
		foreach (ISuspendable suspendable in suspendables)
		{
			SuspensionManager.Resume(suspendable);
		}
	}

	// Token: 0x0600015E RID: 350 RVA: 0x00006AFC File Offset: 0x00004CFC
	public static void Resume(List<ISuspendable> suspendables)
	{
		for (int i = 0; i < suspendables.Count; i++)
		{
			ISuspendable suspendable = suspendables[i];
			SuspensionManager.Resume(suspendable);
		}
	}

	// Token: 0x0600015F RID: 351 RVA: 0x00006B30 File Offset: 0x00004D30
	public static void Resume(IEnumerable<ISuspendable> suspendables)
	{
		foreach (ISuspendable suspendable in suspendables)
		{
			SuspensionManager.Resume(suspendable);
		}
	}

	// Token: 0x04000114 RID: 276
	private static readonly Dictionary<ISuspendable, int> s_suspended = new Dictionary<ISuspendable, int>();

	// Token: 0x04000115 RID: 277
	private static readonly HashSet<ISuspendable> s_suspendedIterate = new HashSet<ISuspendable>();

	// Token: 0x04000116 RID: 278
	private static readonly List<ISuspendable> s_suspenableList = new List<ISuspendable>();

	// Token: 0x04000117 RID: 279
	private static List<ISuspendable> s_suspendRemove = new List<ISuspendable>();

	// Token: 0x02000741 RID: 1857
	private class SuspendableInfo
	{
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x000BB2CE File Offset: 0x000B94CE
		public int Counter
		{
			get
			{
				return this.m_counter;
			}
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000BB2D8 File Offset: 0x000B94D8
		public void SuspendObj(ISuspendable suspendable)
		{
			try
			{
				this.m_counter++;
				if (this.m_counter == 1)
				{
					suspendable.IsSuspended = true;
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000BB324 File Offset: 0x000B9524
		public void ResumeObj(ISuspendable suspendable)
		{
			try
			{
				if (this.m_counter != 0)
				{
					this.m_counter--;
					if (this.m_counter == 0)
					{
						suspendable.IsSuspended = false;
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0400275D RID: 10077
		private int m_counter;
	}
}
