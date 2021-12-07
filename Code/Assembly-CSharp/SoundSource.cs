using System;
using Core;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class SoundSource : MonoBehaviour, IPooled, ISuspendable
{
	// Token: 0x17000134 RID: 308
	// (get) Token: 0x060004DE RID: 1246 RVA: 0x00013A36 File Offset: 0x00011C36
	// (set) Token: 0x060004DF RID: 1247 RVA: 0x00013A3E File Offset: 0x00011C3E
	public bool IsPlaying { get; set; }

	// Token: 0x060004E0 RID: 1248 RVA: 0x00013A48 File Offset: 0x00011C48
	private void ReleaseSoundPlayer()
	{
		if (this.m_soundPlayer)
		{
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_soundPlayer.gameObject);
			this.m_soundPlayer = null;
		}
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x00013A81 File Offset: 0x00011C81
	public void OnPoolSpawned()
	{
		this.m_originalVolume = 0f;
		this.m_lastPlayedTime = 0f;
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x00013A9C File Offset: 0x00011C9C
	private void OnPoolDespawned()
	{
		if (!InstantiateUtility.IsDestroyed(this.m_soundPlayer))
		{
			this.m_soundPlayer.FadeOut(this.FadeOutTimeOnDestroy, true);
			this.ReleaseSoundPlayer();
		}
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x00013AD1 File Offset: 0x00011CD1
	public void Awake()
	{
		SuspensionManager.Register(this);
		if (this.MinimumTimeBetweenPlays == 0f)
		{
			this.MinimumTimeBetweenPlays = 0.0666f;
		}
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x00013AF4 File Offset: 0x00011CF4
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		if (!InstantiateUtility.IsDestroyed(this.m_soundPlayer))
		{
			this.m_soundPlayer.FadeOut(this.FadeOutTimeOnDestroy, true);
			this.ReleaseSoundPlayer();
		}
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x00013B2F File Offset: 0x00011D2F
	private void Nullify()
	{
		this.m_soundPlayer = null;
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00013B38 File Offset: 0x00011D38
	public void Start()
	{
		if ((this.loop || (this.LoopOneSound && !this.DropLoopWhenOutOfRange)) && base.name.EndsWith("SoundSpot"))
		{
			this.loop = false;
			this.LoopOneSound = true;
			this.DropLoopWhenOutOfRange = true;
		}
		if (this.PlayAtStart)
		{
			this.Play();
			if (this.FadeInFirstSoundDuration > 0f && !InstantiateUtility.IsDestroyed(this.m_soundPlayer))
			{
				this.m_soundPlayer.FadeIn(this.FadeInFirstSoundDuration, false);
			}
		}
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00013BD4 File Offset: 0x00011DD4
	public void OnDisable()
	{
		if ((this.loop || this.LoopOneSound) && !InstantiateUtility.IsDestroyed(this.m_soundPlayer))
		{
			this.m_soundPlayer.FadeOut(this.FadeOutTimeOnDestroy, true);
			this.ReleaseSoundPlayer();
		}
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x00013C20 File Offset: 0x00011E20
	public void FixedUpdate()
	{
		bool flag = InstantiateUtility.IsDestroyed(this.m_soundPlayer);
		if (!flag)
		{
			this.m_soundPlayer.transform.position = base.transform.position;
		}
		if (this.IsSuspended)
		{
			return;
		}
		if (flag)
		{
			if ((this.loop && this.LoopInterval == 0f && this.IsPlaying) || (this.LoopOneSound && this.IsPlaying) || this.TempJustAlwaysLoop)
			{
				if (!this.DropLoopWhenOutOfRange)
				{
					this.Play();
				}
				else
				{
					SoundDescriptor sound = this.Sound.GetSound(null);
					if (sound.SoundSize.Radius != 0f || sound.SoundSize.FalloffMargin != 0f)
					{
						float num = sound.SoundSize.Radius + sound.SoundSize.FalloffMargin;
						if (num * num >= (base.transform.position - Core.Sound.SoundListenerPosition).sqrMagnitude)
						{
							this.Play();
						}
					}
				}
			}
			else if (this.DestroyOnSoundEnd)
			{
				InstantiateUtility.Destroy(base.gameObject);
			}
		}
		else if (this.DropLoopWhenOutOfRange && (this.m_soundPlayer.SoundSize.Radius != 0f || this.m_soundPlayer.SoundSize.FalloffMargin != 0f))
		{
			float num2 = this.m_soundPlayer.SoundSize.Radius + this.m_soundPlayer.SoundSize.FalloffMargin;
			if (num2 * num2 < (base.transform.position - Core.Sound.SoundListenerPosition).sqrMagnitude)
			{
				this.Stop();
				this.IsPlaying = true;
			}
		}
		if (this.IsPlaying)
		{
			if (this.loop && this.LoopInterval != 0f && this.m_lastPlayedTime + this.LoopInterval < Time.time)
			{
				this.Play();
			}
			if (flag && !this.loop && !this.LoopOneSound)
			{
				this.IsPlaying = false;
			}
		}
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x00013E60 File Offset: 0x00012060
	public void Play()
	{
		if (Time.time - this.m_lastPlayedTime < this.MinimumTimeBetweenPlays)
		{
			return;
		}
		this.IsPlaying = true;
		this.m_lastPlayedTime = Time.time;
		if (this.Sound)
		{
			SoundDescriptor sound = this.Sound.GetSound(null);
			if (sound == null)
			{
				return;
			}
			this.m_originalVolume = sound.Volume;
			sound.Volume = this.m_originalVolume * this.VolumeMultiplier;
			if (this.m_soundPlayer != null)
			{
				if (this.m_soundPlayer.Loop)
				{
					this.m_soundPlayer.FadeOut(0.5f, true);
				}
				this.ReleaseSoundPlayer();
			}
			if (this.m_nullify == null)
			{
				this.m_nullify = new Action(this.Nullify);
			}
			if (this.LoopOneSound)
			{
				this.m_soundPlayer = Core.Sound.PlayLooping(sound, base.transform.position, this.FadeInFirstSoundDuration, this.m_nullify);
			}
			else
			{
				this.m_soundPlayer = Core.Sound.Play(sound, base.transform.position, this.m_nullify);
			}
			if (!InstantiateUtility.IsDestroyed(this.m_soundPlayer))
			{
				this.m_soundPlayer.PauseOnSuspend = this.PauseOnSuspend;
			}
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00013FA0 File Offset: 0x000121A0
	public void Pause()
	{
		if (!InstantiateUtility.IsDestroyed(this.m_soundPlayer))
		{
			this.m_soundPlayer.Pause();
		}
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x00013FC0 File Offset: 0x000121C0
	public void Stop()
	{
		this.IsPlaying = false;
		if (!InstantiateUtility.IsDestroyed(this.m_soundPlayer))
		{
			this.m_soundPlayer.FadeOut(0.3f, true);
			this.ReleaseSoundPlayer();
		}
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x00013FFC File Offset: 0x000121FC
	public void StopAndFadeOut(float duration)
	{
		this.IsPlaying = false;
		if (!InstantiateUtility.IsDestroyed(this.m_soundPlayer))
		{
			this.m_soundPlayer.FadeOut(duration, true);
			this.ReleaseSoundPlayer();
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00014033 File Offset: 0x00012233
	public void SetVolumeMultiplier(float multiplier)
	{
		this.VolumeMultiplier = multiplier;
		if (!InstantiateUtility.IsDestroyed(this.m_soundPlayer))
		{
			this.m_soundPlayer.Volume = this.m_originalVolume * this.VolumeMultiplier;
		}
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x060004EE RID: 1262 RVA: 0x00014064 File Offset: 0x00012264
	// (set) Token: 0x060004EF RID: 1263 RVA: 0x0001406C File Offset: 0x0001226C
	public bool IsSuspended { get; set; }

	// Token: 0x040003ED RID: 1005
	public SoundProvider Sound;

	// Token: 0x040003EE RID: 1006
	private SoundPlayer m_soundPlayer;

	// Token: 0x040003EF RID: 1007
	public bool loop;

	// Token: 0x040003F0 RID: 1008
	public bool LoopOneSound;

	// Token: 0x040003F1 RID: 1009
	public bool DropLoopWhenOutOfRange;

	// Token: 0x040003F2 RID: 1010
	public bool DestroyOnSoundEnd;

	// Token: 0x040003F3 RID: 1011
	public float VolumeMultiplier = 1f;

	// Token: 0x040003F4 RID: 1012
	private float m_originalVolume;

	// Token: 0x040003F5 RID: 1013
	public float FadeInFirstSoundDuration;

	// Token: 0x040003F6 RID: 1014
	public float MinimumTimeBetweenPlays;

	// Token: 0x040003F7 RID: 1015
	public float LoopInterval;

	// Token: 0x040003F8 RID: 1016
	private float m_lastPlayedTime = -10f;

	// Token: 0x040003F9 RID: 1017
	public bool PlayAtStart;

	// Token: 0x040003FA RID: 1018
	public bool PauseOnSuspend;

	// Token: 0x040003FB RID: 1019
	public float FadeOutTimeOnDestroy = 0.4f;

	// Token: 0x040003FC RID: 1020
	public bool TempJustAlwaysLoop;

	// Token: 0x040003FD RID: 1021
	private Action m_nullify;
}
