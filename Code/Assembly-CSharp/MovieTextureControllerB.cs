using System;
using System.Collections;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200086C RID: 2156
public class MovieTextureControllerB : MonoBehaviour
{
	// Token: 0x060030AD RID: 12461 RVA: 0x000CEF31 File Offset: 0x000CD131
	public void OnDestroy()
	{
	}

	// Token: 0x060030AE RID: 12462 RVA: 0x000CEF33 File Offset: 0x000CD133
	public void Start()
	{
		if (this.VideoDescriptor.MovieTexture == null)
		{
			this.Stop();
		}
		if (this.PlayAtStart)
		{
			this.Play();
		}
	}

	// Token: 0x060030AF RID: 12463 RVA: 0x000CEF64 File Offset: 0x000CD164
	public void Play()
	{
		if (this.VideoDescriptor.MovieTexture == null)
		{
			return;
		}
		this.m_started = true;
		this.m_finished = false;
		this.m_pausedByPlayer = false;
		this.m_messageScreen = null;
		this.m_movieTexturePlayingDuration = 0f;
		Material material = this.MovieTextureRenderer.sharedMaterial;
		if (Application.isPlaying)
		{
			material = this.MovieTextureRenderer.material;
		}
		material.mainTexture = this.VideoDescriptor.MovieTexture;
		this.VideoDescriptor.MovieTexture.loop = this.Loop;
		this.VideoDescriptor.MovieTexture.Play();
		if (this.UseAudio && this.MovieAudioSource && this.VideoDescriptor.MovieTexture.audioClip)
		{
			this.MovieAudioSource.clip = this.VideoDescriptor.MovieTexture.audioClip;
			this.MovieAudioSource.Play();
		}
		if (this.PauseDimmer)
		{
			if (this.m_dimmerAnimators == null)
			{
				this.m_dimmerAnimators = this.PauseDimmer.GetComponentsInChildren<LegacyAnimator>();
			}
			this.HideDimmer();
		}
		this.UpdateMainTexture();
	}

	// Token: 0x060030B0 RID: 12464 RVA: 0x000CF09C File Offset: 0x000CD29C
	public void Pause()
	{
		if (this.VideoDescriptor.MovieTexture)
		{
			this.VideoDescriptor.MovieTexture.Pause();
		}
		if (this.UseAudio && this.MovieAudioSource)
		{
			this.MovieAudioSource.Pause();
		}
		if (this.PauseDimmer)
		{
			this.ShowDimmer();
		}
	}

	// Token: 0x060030B1 RID: 12465 RVA: 0x000CF10C File Offset: 0x000CD30C
	public void Resume()
	{
		this.VideoDescriptor.MovieTexture.Play();
		this.UpdateMainTexture();
		if (this.PauseDimmer)
		{
			this.HideDimmer();
		}
	}

	// Token: 0x060030B2 RID: 12466 RVA: 0x000CF145 File Offset: 0x000CD345
	public void UpdateMainTexture()
	{
	}

	// Token: 0x060030B3 RID: 12467 RVA: 0x000CF148 File Offset: 0x000CD348
	public void Stop()
	{
		this.m_finished = true;
		if (this.VideoDescriptor.MovieTexture)
		{
			this.VideoDescriptor.MovieTexture.Stop();
		}
		if (this.UseAudio && this.MovieAudioSource)
		{
			this.MovieAudioSource.Stop();
		}
		if (this.OnFinishedAction)
		{
			this.OnFinishedAction.Perform(null);
		}
		if (this.DestroyOnFinish)
		{
			base.StartCoroutine(this.DestroyRoutine());
		}
	}

	// Token: 0x060030B4 RID: 12468 RVA: 0x000CF1DC File Offset: 0x000CD3DC
	public IEnumerator DestroyRoutine()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		InstantiateUtility.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x060030B5 RID: 12469 RVA: 0x000CF1F8 File Offset: 0x000CD3F8
	private void HideDimmer()
	{
		for (int i = 0; i < this.m_dimmerAnimators.Length; i++)
		{
			LegacyAnimator legacyAnimator = this.m_dimmerAnimators[i];
			legacyAnimator.ContinueBackward();
		}
	}

	// Token: 0x060030B6 RID: 12470 RVA: 0x000CF230 File Offset: 0x000CD430
	private void ShowDimmer()
	{
		for (int i = 0; i < this.m_dimmerAnimators.Length; i++)
		{
			LegacyAnimator legacyAnimator = this.m_dimmerAnimators[i];
			legacyAnimator.ContinueForward();
		}
	}

	// Token: 0x060030B7 RID: 12471 RVA: 0x000CF268 File Offset: 0x000CD468
	public bool IsPlaying()
	{
		return !(this.VideoDescriptor.MovieTexture == null) && !this.m_pausedByPlayer && this.VideoDescriptor.MovieTexture.isPlaying;
	}

	// Token: 0x060030B8 RID: 12472 RVA: 0x000CF2AC File Offset: 0x000CD4AC
	public void FixedUpdate()
	{
		if (this.m_finished)
		{
			return;
		}
		if (this.m_started)
		{
			if (this.IsPlaying())
			{
				this.m_movieTexturePlayingDuration += Time.fixedDeltaTime;
				if (this.SkippedWithButtonPress && (Core.Input.SpiritFlame.OnPressed || Core.Input.Jump.OnPressed || Core.Input.Start.OnPressed))
				{
					this.Stop();
				}
			}
			else if (Core.Input.SoulFlame.OnPressed)
			{
				if (this.CanBeSkipped)
				{
					Core.Input.SoulFlame.Used = true;
					if (this.m_messageScreen != null)
					{
						this.m_messageScreen.HideMessageScreen();
					}
					this.Stop();
				}
			}
			else if (this.m_pausedByPlayer)
			{
				if (this.m_messageScreen == null)
				{
					this.m_messageScreen = UI.MessageController.ShowHintMessage(this.PressToSkipVideoMessage, OnScreenPositions.MiddleCenter, 3f);
				}
				else
				{
					this.m_messageScreen.Visibility.ResetWaitDuration();
				}
			}
		}
		if (this.CanBePaused && Core.Input.Start.OnPressed)
		{
			if (this.IsPlaying())
			{
				this.m_pausedByPlayer = true;
				this.Pause();
			}
			else if (this.m_started)
			{
				this.m_pausedByPlayer = false;
				if (this.m_messageScreen)
				{
					this.m_messageScreen.HideMessageScreen();
				}
				this.Resume();
			}
		}
		if (this.m_started && !this.IsPlaying() && !this.m_pausedByPlayer)
		{
			this.Stop();
		}
	}

	// Token: 0x060030B9 RID: 12473 RVA: 0x000CF45E File Offset: 0x000CD65E
	public void Update()
	{
		if (Time.timeScale != this.m_prevTimeScale)
		{
			this.MovieAudioSource.pitch = Time.timeScale;
			this.m_prevTimeScale = Time.timeScale;
		}
	}

	// Token: 0x060030BA RID: 12474 RVA: 0x000CF48B File Offset: 0x000CD68B
	public bool IsFinished()
	{
		return this.m_finished;
	}

	// Token: 0x04002BF1 RID: 11249
	public VideoDescriptor VideoDescriptor;

	// Token: 0x04002BF2 RID: 11250
	public Renderer MovieTextureRenderer;

	// Token: 0x04002BF3 RID: 11251
	public AudioSource MovieAudioSource;

	// Token: 0x04002BF4 RID: 11252
	public ActionMethod OnFinishedAction;

	// Token: 0x04002BF5 RID: 11253
	public MessageProvider PressToSkipVideoMessage;

	// Token: 0x04002BF6 RID: 11254
	public GameObject PauseDimmer;

	// Token: 0x04002BF7 RID: 11255
	private LegacyAnimator[] m_dimmerAnimators;

	// Token: 0x04002BF8 RID: 11256
	private bool m_started;

	// Token: 0x04002BF9 RID: 11257
	public bool DestroyOnFinish;

	// Token: 0x04002BFA RID: 11258
	public bool PlayAtStart;

	// Token: 0x04002BFB RID: 11259
	public bool CanBePaused = true;

	// Token: 0x04002BFC RID: 11260
	public bool CanBeSkipped = true;

	// Token: 0x04002BFD RID: 11261
	public bool SkippedWithButtonPress;

	// Token: 0x04002BFE RID: 11262
	public bool Loop;

	// Token: 0x04002BFF RID: 11263
	public bool UseAudio = true;

	// Token: 0x04002C00 RID: 11264
	private float m_movieTexturePlayingDuration;

	// Token: 0x04002C01 RID: 11265
	private MessageBox m_messageScreen;

	// Token: 0x04002C02 RID: 11266
	private bool m_pausedByPlayer;

	// Token: 0x04002C03 RID: 11267
	private bool m_finished;

	// Token: 0x04002C04 RID: 11268
	private float m_prevTimeScale = -1f;

	// Token: 0x04002C05 RID: 11269
	public GUISkin Skin;

	// Token: 0x04002C06 RID: 11270
	[HideInInspector]
	public static GUIStyle style;
}
