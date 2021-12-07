using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006B4 RID: 1716
[Serializable]
public class SoundComposition : ScriptableObject
{
	// Token: 0x040024DC RID: 9436
	public int LoopCount;

	// Token: 0x040024DD RID: 9437
	public float LoopDuration;

	// Token: 0x040024DE RID: 9438
	public List<SoundComposition.SoundLoop> Loops = new List<SoundComposition.SoundLoop>();

	// Token: 0x040024DF RID: 9439
	public List<SoundComposition.SoundLayer> Layers = new List<SoundComposition.SoundLayer>();

	// Token: 0x020006B6 RID: 1718
	[Serializable]
	public class SoundLoop
	{
		// Token: 0x040024E2 RID: 9442
		public AudioClip Sound;

		// Token: 0x040024E3 RID: 9443
		public float Volume;

		// Token: 0x040024E4 RID: 9444
		public AnimationCurve VolumeOverTime;
	}

	// Token: 0x020006B7 RID: 1719
	[Serializable]
	public class SoundLayer
	{
		// Token: 0x040024E5 RID: 9445
		public AudioClip Sound;

		// Token: 0x040024E6 RID: 9446
		public List<bool> LoopsToPlay = new List<bool>();

		// Token: 0x040024E7 RID: 9447
		public float Volume;

		// Token: 0x040024E8 RID: 9448
		public AnimationCurve VolumeOverTime;
	}
}
