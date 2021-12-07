using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006B3 RID: 1715
public class SoundCompositionTransition : ScriptableObject
{
	// Token: 0x17000690 RID: 1680
	// (get) Token: 0x06002953 RID: 10579 RVA: 0x000B262F File Offset: 0x000B082F
	public float EndPreviousSoundTime
	{
		get
		{
			return this.DefaultFadeOut.FadeStartTime + this.DefaultFadeOut.FadeDuration;
		}
	}

	// Token: 0x17000691 RID: 1681
	// (get) Token: 0x06002954 RID: 10580 RVA: 0x000B2648 File Offset: 0x000B0848
	public float EndTransitionTime
	{
		get
		{
			return Mathf.Max(this.EndPreviousSoundTime, this.NextSoundDelay + this.DefaultFadeIn.FadeStartTime + this.DefaultFadeIn.FadeDuration);
		}
	}

	// Token: 0x040024D4 RID: 9428
	public List<SoundCompositionTransition.SoundFadeInformation> FadeOut = new List<SoundCompositionTransition.SoundFadeInformation>();

	// Token: 0x040024D5 RID: 9429
	public List<SoundCompositionTransition.SoundFadeInformation> FadeIn = new List<SoundCompositionTransition.SoundFadeInformation>();

	// Token: 0x040024D6 RID: 9430
	public SoundCompositionTransition.FadeInformation DefaultFadeIn = new SoundCompositionTransition.FadeInformation();

	// Token: 0x040024D7 RID: 9431
	public SoundCompositionTransition.FadeInformation DefaultFadeOut = new SoundCompositionTransition.FadeInformation();

	// Token: 0x040024D8 RID: 9432
	public float TransitionDelay;

	// Token: 0x040024D9 RID: 9433
	public float NextSoundDelay;

	// Token: 0x040024DA RID: 9434
	public AudioClip Sound;

	// Token: 0x040024DB RID: 9435
	public float Volume;

	// Token: 0x020006BE RID: 1726
	[Serializable]
	public class FadeInformation
	{
		// Token: 0x040024FD RID: 9469
		public AnimationCurve FadeCurve;

		// Token: 0x040024FE RID: 9470
		public float FadeDuration;

		// Token: 0x040024FF RID: 9471
		public float FadeStartTime;
	}

	// Token: 0x020006BF RID: 1727
	[Serializable]
	public class SoundFadeInformation
	{
		// Token: 0x04002500 RID: 9472
		public AudioClip Sound;

		// Token: 0x04002501 RID: 9473
		public AnimationCurve FadeCurve;

		// Token: 0x04002502 RID: 9474
		public float FadeDuration;

		// Token: 0x04002503 RID: 9475
		public float FadeStartTime;
	}
}
