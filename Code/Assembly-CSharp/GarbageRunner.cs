using System;

// Token: 0x020004AC RID: 1196
public class GarbageRunner : ActionDebugMenuItem
{
	// Token: 0x060020A0 RID: 8352 RVA: 0x0008E941 File Offset: 0x0008CB41
	public GarbageRunner() : base("Run garbage Test", new Func<bool>(GarbageRunner.GarbageTest))
	{
	}

	// Token: 0x060020A1 RID: 8353 RVA: 0x0008E95C File Offset: 0x0008CB5C
	private static bool GarbageTest()
	{
		DebugMenuB.ToggleDebugMenu();
		for (int i = 0; i < 100; i++)
		{
			GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
		}
		return true;
	}
}
