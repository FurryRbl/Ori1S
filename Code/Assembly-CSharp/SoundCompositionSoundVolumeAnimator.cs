using System;
using Core;
using UnityEngine;

// Token: 0x020006BD RID: 1725
public class SoundCompositionSoundVolumeAnimator : LegacyAnimator
{
	// Token: 0x0600296C RID: 10604 RVA: 0x000B337F File Offset: 0x000B157F
	protected override void AnimateIt(float value)
	{
		Core.SoundComposition.SoundVolumes.Volumes[this.Sound] = value;
	}

	// Token: 0x0600296D RID: 10605 RVA: 0x000B3392 File Offset: 0x000B1592
	public override void RestoreToOriginalState()
	{
		Core.SoundComposition.SoundVolumes.Volumes[this.Sound] = 1f;
	}

	// Token: 0x040024FC RID: 9468
	public AudioClip Sound;
}
