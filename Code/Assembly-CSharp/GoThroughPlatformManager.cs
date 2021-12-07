using System;

// Token: 0x020008EA RID: 2282
public static class GoThroughPlatformManager
{
	// Token: 0x060032E6 RID: 13030 RVA: 0x000D7114 File Offset: 0x000D5314
	public static void Register(GoThroughPlatform platform)
	{
		GoThroughPlatformManager.GoThroughPlatforms.Add(platform);
	}

	// Token: 0x060032E7 RID: 13031 RVA: 0x000D7121 File Offset: 0x000D5321
	public static void Unregister(GoThroughPlatform platform)
	{
		GoThroughPlatformManager.GoThroughPlatforms.Remove(platform);
	}

	// Token: 0x04002DE3 RID: 11747
	public static AllContainer<GoThroughPlatform> GoThroughPlatforms = new AllContainer<GoThroughPlatform>();
}
