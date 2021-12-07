using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000732 RID: 1842
[Serializable]
public struct MixerGroupSettings
{
	// Token: 0x06002B54 RID: 11092 RVA: 0x000B9D4C File Offset: 0x000B7F4C
	public void MultiplyBlendWith(MixerGroupSettings settings, float weight)
	{
		this.Music *= Mathf.Lerp(1f, settings.Music, weight);
		this.MusicLoops *= Mathf.Lerp(1f, settings.MusicLoops, weight);
		this.MusicStingers *= Mathf.Lerp(1f, settings.MusicStingers, weight);
		this.SoundEffects *= Mathf.Lerp(1f, settings.SoundEffects, weight);
		this.AmbienceQuad *= Mathf.Lerp(1f, settings.AmbienceQuad, weight);
		this.AmbiencePoint *= Mathf.Lerp(1f, settings.AmbiencePoint, weight);
		this.EnemiesAttack *= Mathf.Lerp(1f, settings.EnemiesAttack, weight);
		this.EnemiesFoley *= Mathf.Lerp(1f, settings.EnemiesFoley, weight);
		this.Foley *= Mathf.Lerp(1f, settings.Foley, weight);
		this.Footsteps *= Mathf.Lerp(1f, settings.Footsteps, weight);
		this.Attacks *= Mathf.Lerp(1f, settings.Attacks, weight);
		this.Destruction *= Mathf.Lerp(1f, settings.Destruction, weight);
		this.UI *= Mathf.Lerp(1f, settings.UI, weight);
		this.SpiritTree *= Mathf.Lerp(1f, settings.SpiritTree, weight);
		this.Sein *= Mathf.Lerp(1f, settings.Sein, weight);
		this.Doors *= Mathf.Lerp(1f, settings.Doors, weight);
		this.Cutscenes *= Mathf.Lerp(1f, settings.Cutscenes, weight);
		this.Props *= Mathf.Lerp(1f, settings.Props, weight);
		this.Collectibles *= Mathf.Lerp(1f, settings.Collectibles, weight);
		this.Underwater = Mathf.Max(this.Underwater, settings.Underwater * weight);
		this.Reverb *= Mathf.Lerp(1f, settings.Reverb, weight);
	}

	// Token: 0x06002B55 RID: 11093 RVA: 0x000B9FE0 File Offset: 0x000B81E0
	public void ApplyGroupSettingsToMixer(AudioMixer mixer)
	{
		mixer.SetFloat("music", (this.Music - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("loops", (this.MusicLoops - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("stingers", (this.MusicStingers - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("soundEffects", (this.SoundEffects - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("ambienceQuad", (this.AmbienceQuad - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("ambiencePoint", (this.AmbiencePoint - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("foley", (this.Foley - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("footsteps", (this.Footsteps - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("enemiesAttack", (this.EnemiesAttack - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("enemiesFoley", (this.EnemiesFoley - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("attacks", (this.Attacks - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("destruction", (this.Destruction - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("ui", (this.UI - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("spiritTree", (this.SpiritTree - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("sein", (this.Sein - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("doors", (this.Doors - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("cutscenes", (this.Cutscenes - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("props", (this.Props - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("collectibles", (this.Collectibles - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("underwater", (this.Underwater - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
		mixer.SetFloat("reverb", (this.Reverb - 1f) * MixerGroupSettings.NEGATIVE_DECIBEL_RANGE_SIZE);
	}

	// Token: 0x06002B56 RID: 11094 RVA: 0x000BA264 File Offset: 0x000B8464
	public void Reset()
	{
		this.Music = 1f;
		this.MusicLoops = 1f;
		this.MusicStingers = 1f;
		this.SoundEffects = 1f;
		this.AmbienceQuad = 1f;
		this.AmbiencePoint = 1f;
		this.Foley = 1f;
		this.Footsteps = 1f;
		this.EnemiesAttack = 1f;
		this.EnemiesFoley = 1f;
		this.Attacks = 1f;
		this.Destruction = 1f;
		this.UI = 1f;
		this.SpiritTree = 1f;
		this.Sein = 1f;
		this.Doors = 1f;
		this.Cutscenes = 1f;
		this.Props = 1f;
		this.Collectibles = 1f;
		this.Underwater = 0f;
		this.Reverb = 1f;
	}

	// Token: 0x04002712 RID: 10002
	public float Music;

	// Token: 0x04002713 RID: 10003
	public float MusicLoops;

	// Token: 0x04002714 RID: 10004
	public float MusicStingers;

	// Token: 0x04002715 RID: 10005
	public float SoundEffects;

	// Token: 0x04002716 RID: 10006
	public float AmbienceQuad;

	// Token: 0x04002717 RID: 10007
	public float AmbiencePoint;

	// Token: 0x04002718 RID: 10008
	public float EnemiesAttack;

	// Token: 0x04002719 RID: 10009
	public float EnemiesFoley;

	// Token: 0x0400271A RID: 10010
	public float Foley;

	// Token: 0x0400271B RID: 10011
	public float Footsteps;

	// Token: 0x0400271C RID: 10012
	public float Attacks;

	// Token: 0x0400271D RID: 10013
	public float Destruction;

	// Token: 0x0400271E RID: 10014
	public float UI;

	// Token: 0x0400271F RID: 10015
	public float SpiritTree;

	// Token: 0x04002720 RID: 10016
	public float Sein;

	// Token: 0x04002721 RID: 10017
	public float Doors;

	// Token: 0x04002722 RID: 10018
	public float Cutscenes;

	// Token: 0x04002723 RID: 10019
	public float Props;

	// Token: 0x04002724 RID: 10020
	public float Collectibles;

	// Token: 0x04002725 RID: 10021
	public float Underwater;

	// Token: 0x04002726 RID: 10022
	public float Reverb;

	// Token: 0x04002727 RID: 10023
	private static readonly float NEGATIVE_DECIBEL_RANGE_SIZE = 70f;
}
