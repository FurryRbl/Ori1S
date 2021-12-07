using System;
using UnityEngine;

// Token: 0x02000684 RID: 1668
public class OriSpeechSounds : MonoBehaviour
{
	// Token: 0x06002885 RID: 10373 RVA: 0x000B01B8 File Offset: 0x000AE3B8
	public void Awake()
	{
		this.MessageBox.OnNextMessage += this.OnNextMessage;
		this.MessageBox.OnMessageScreenHide += this.StopOriSpeech;
	}

	// Token: 0x06002886 RID: 10374 RVA: 0x000B01F3 File Offset: 0x000AE3F3
	public void Start()
	{
		this.PlayOriSpeech();
	}

	// Token: 0x06002887 RID: 10375 RVA: 0x000B01FB File Offset: 0x000AE3FB
	public void OnNextMessage()
	{
		this.PlayOriSpeech();
	}

	// Token: 0x06002888 RID: 10376 RVA: 0x000B0204 File Offset: 0x000AE404
	public void PlayOriSpeech()
	{
		this.StopOriSpeech();
		if (this.MessageBox.CurrentMessageSound && this.ExtraSoundSource)
		{
			this.ExtraSoundSource.Sound = this.MessageBox.CurrentMessageSound;
			this.ExtraSoundSource.Play();
			return;
		}
		this.m_emotionSounds = this.GetSoundSetForEmotion(this.MessageBox.CurrentEmotion);
		int num = TextBoxExtended.CountLetters(this.MessageBox.TextBox);
		if (num <= this.MaxTextLengthShortSpeech)
		{
			if (this.m_emotionSounds.OriSpeechShortSound)
			{
				this.m_emotionSounds.OriSpeechShortSound.Play();
			}
		}
		else if (num <= this.MaxTextLengthMedSpeech)
		{
			if (this.m_emotionSounds.OriSpeechMedSound)
			{
				this.m_emotionSounds.OriSpeechMedSound.Play();
			}
		}
		else if (this.m_emotionSounds.OriSpeechLongSound)
		{
			this.m_emotionSounds.OriSpeechLongSound.Play();
		}
	}

	// Token: 0x06002889 RID: 10377 RVA: 0x000B0318 File Offset: 0x000AE518
	private void StopOriSpeech()
	{
		if (this.ExtraSoundSource)
		{
			this.ExtraSoundSource.Stop();
		}
		if (this.m_emotionSounds == null)
		{
			return;
		}
		if (this.m_emotionSounds.OriSpeechShortSound)
		{
			this.m_emotionSounds.OriSpeechShortSound.StopAndFadeOut(0.5f);
		}
		if (this.m_emotionSounds.OriSpeechMedSound)
		{
			this.m_emotionSounds.OriSpeechMedSound.StopAndFadeOut(0.5f);
		}
		if (this.m_emotionSounds.OriSpeechLongSound)
		{
			this.m_emotionSounds.OriSpeechLongSound.StopAndFadeOut(0.5f);
		}
	}

	// Token: 0x0600288A RID: 10378 RVA: 0x000B03CC File Offset: 0x000AE5CC
	private OriSpeechSounds.EmotionSounds GetSoundSetForEmotion(EmotionType emotion)
	{
		switch (emotion)
		{
		case EmotionType.Neutral:
			return this.NeutralSoundSet;
		case EmotionType.Happy:
			return this.HappySoundSet;
		case EmotionType.Sad:
			return this.SadSoundSet;
		case EmotionType.Scared:
			return this.ScaredSoundSet;
		case EmotionType.Urgent:
			return this.UrgentSoundSet;
		default:
			return this.NeutralSoundSet;
		}
	}

	// Token: 0x04002404 RID: 9220
	public MessageBox MessageBox;

	// Token: 0x04002405 RID: 9221
	public OriSpeechSounds.EmotionSounds HappySoundSet;

	// Token: 0x04002406 RID: 9222
	public OriSpeechSounds.EmotionSounds NeutralSoundSet;

	// Token: 0x04002407 RID: 9223
	public OriSpeechSounds.EmotionSounds SadSoundSet;

	// Token: 0x04002408 RID: 9224
	public OriSpeechSounds.EmotionSounds ScaredSoundSet;

	// Token: 0x04002409 RID: 9225
	public OriSpeechSounds.EmotionSounds UrgentSoundSet;

	// Token: 0x0400240A RID: 9226
	private OriSpeechSounds.EmotionSounds m_emotionSounds;

	// Token: 0x0400240B RID: 9227
	public int MaxTextLengthShortSpeech;

	// Token: 0x0400240C RID: 9228
	public int MaxTextLengthMedSpeech;

	// Token: 0x0400240D RID: 9229
	public SoundSource ExtraSoundSource;

	// Token: 0x02000685 RID: 1669
	[Serializable]
	public class EmotionSounds
	{
		// Token: 0x0400240E RID: 9230
		public SoundSource OriSpeechShortSound;

		// Token: 0x0400240F RID: 9231
		public SoundSource OriSpeechMedSound;

		// Token: 0x04002410 RID: 9232
		public SoundSource OriSpeechLongSound;
	}
}
