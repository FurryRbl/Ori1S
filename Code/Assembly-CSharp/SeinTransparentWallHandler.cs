using System;
using UnityEngine;

// Token: 0x02000924 RID: 2340
public class SeinTransparentWallHandler : MonoBehaviour
{
	// Token: 0x060033DE RID: 13278 RVA: 0x000DA428 File Offset: 0x000D8628
	public void Awake()
	{
		SeinTransparentWallHandler.Instance = this;
	}

	// Token: 0x04002ECF RID: 11983
	public static SeinTransparentWallHandler Instance;

	// Token: 0x04002ED0 RID: 11984
	public SoundProvider EnterTransparentWallSoundProvider;

	// Token: 0x04002ED1 RID: 11985
	public SoundProvider EnterTransparentWallFirstTimeSoundProvider;

	// Token: 0x04002ED2 RID: 11986
	public SoundProvider LeaveTransparentWallSoundProvider;
}
