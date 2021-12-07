using System;

// Token: 0x02000043 RID: 67
public interface IUberAtlasExternal
{
	// Token: 0x060002E8 RID: 744
	UberScreenMode GetExternalUberScreenMode();

	// Token: 0x060002E9 RID: 745
	float GetUberTweakValue();

	// Token: 0x060002EA RID: 746
	bool DoesProvideAtlas();

	// Token: 0x060002EB RID: 747
	void Update();
}
