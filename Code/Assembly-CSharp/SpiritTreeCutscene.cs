using System;
using Game;

// Token: 0x020000E4 RID: 228
public class SpiritTreeCutscene : CutsceneController
{
	// Token: 0x06000928 RID: 2344 RVA: 0x00027860 File Offset: 0x00025A60
	public new void Start()
	{
		base.Start();
		Characters.Sein.PlatformBehaviour.PlatformMovement.PlaceOnGround(0.5f, 0f);
	}
}
