using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class SoundPlayer : MonoBehaviour, ISuspendable
{
	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06000206 RID: 518 RVA: 0x000088BF File Offset: 0x00006ABF
	// (set) Token: 0x06000207 RID: 519 RVA: 0x000088CC File Offset: 0x00006ACC
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
		set
		{
			base.transform.position = value;
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x06000208 RID: 520 RVA: 0x000088DA File Offset: 0x00006ADA
	// (set) Token: 0x06000209 RID: 521 RVA: 0x000088E7 File Offset: 0x00006AE7
	public AudioClip Clip
	{
		get
		{
			return this.m_audioSource.clip;
		}
		set
		{
			this.m_audioSource.clip = value;
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x0600020A RID: 522 RVA: 0x000088F5 File Offset: 0x00006AF5
	// (set) Token: 0x0600020B RID: 523 RVA: 0x00008902 File Offset: 0x00006B02
	public float Pitch
	{
		get
		{
			return this.m_audioSource.pitch;
		}
		set
		{
			this.m_audioSource.pitch = value;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x0600020C RID: 524 RVA: 0x00008910 File Offset: 0x00006B10
	// (set) Token: 0x0600020D RID: 525 RVA: 0x00008918 File Offset: 0x00006B18
	public float Volume
	{
		get
		{
			return this.m_volume;
		}
		set
		{
			this.m_volume = value;
			this.UpdateFadeTime();
			this.UpdateVolumeProperties();
		}
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00008930 File Offset: 0x00006B30
	public void Init()
	{
		this.m_audioSource = base.gameObject.GetComponent<AudioSource>();
		if (this.m_audioSource == null)
		{
			this.m_audioSource = base.gameObject.AddComponent<AudioSource>();
		}
		this.SoundSize = null;
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00008977 File Offset: 0x00006B77
	public void Awake()
	{
		this.Register();
		this.Init();
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06000210 RID: 528 RVA: 0x00008990 File Offset: 0x00006B90
	private void Register()
	{
		SuspensionManager.Register(this);
		SoundPlayer.All.Add(this);
	}

	// Token: 0x06000211 RID: 529 RVA: 0x000089A4 File Offset: 0x00006BA4
	private void OnPoolDespawned()
	{
		this.m_audioSource.clip = null;
		if (this.m_lowPassFilter)
		{
			UnityEngine.Object.Destroy(this.m_lowPassFilter);
			this.m_lowPassFilter = null;
		}
		this.RemoveRegisters();
	}

	// Token: 0x06000212 RID: 530 RVA: 0x000089E8 File Offset: 0x00006BE8
	private void OnPoolSpawned()
	{
		this.m_previousIsHighResources = true;
		this.m_isPlaying = false;
		this.m_isSuspended = false;
		this.m_volume = 0f;
		this.m_playTime = 0f;
		this.m_lastRealTime = 0f;
		this.m_fadeSpeed = 0f;
		this.m_fadeAmount = 0f;
		this.m_distance = 0f;
		this.m_timeToSet = 0f;
		this.m_frame = 0;
		if (this.m_lowPassFilter != null)
		{
			UnityEngine.Object.Destroy(this.m_lowPassFilter);
			this.m_lowPassFilter = null;
		}
		this.m_timeToSet = 0f;
		this.m_timeScaleChanged = false;
		this.m_keepInSync = false;
		this.m_falloffVolumeModifier = 1f;
		this.m_checkedLowPass = false;
		this.m_mixerGroup = MixerGroupType.Unspecified;
		this.SoundSize = null;
		this.SoundType = SoundType.SoundEffect;
		this.LowPassFilterSettings = null;
		this.AttachTo = null;
		this.DestroyOnFinished = false;
		this.DestroyOnFadeOut = false;
		this.DestroyOnRestart = false;
		this.Length = 0f;
		this.ShouldBePanned = false;
		this.PauseOnSuspend = false;
		this.m_audioSource.panStereo = 0f;
		this.m_audioSource.volume = 1f;
		this.m_audioSource.pitch = 1f;
		this.m_audioSource.priority = 128;
		this.m_audioSource.time = 0f;
		this.m_audioSource.mute = false;
		this.m_audioSource.bypassEffects = false;
		this.m_audioSource.bypassListenerEffects = false;
		this.m_audioSource.bypassReverbZones = false;
		this.m_audioSource.playOnAwake = true;
		this.m_audioSource.loop = false;
		this.m_audioSource.spatialBlend = 0f;
		this.m_audioSource.panStereo = 0f;
		this.m_audioSource.outputAudioMixerGroup = null;
		this.Register();
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00008BC7 File Offset: 0x00006DC7
	private void Start()
	{
		this.UpdateSoundProperties(0f);
	}

	// Token: 0x06000214 RID: 532 RVA: 0x00008BD4 File Offset: 0x00006DD4
	public void OnDestroy()
	{
		this.RemoveRegisters();
	}

	// Token: 0x06000215 RID: 533 RVA: 0x00008BDC File Offset: 0x00006DDC
	private void RemoveRegisters()
	{
		SuspensionManager.Unregister(this);
		SoundPlayer.All.Remove(this);
	}

	// Token: 0x06000216 RID: 534 RVA: 0x00008BF0 File Offset: 0x00006DF0
	public void Play(AudioClip clip, Vector3 position, float volume, bool loop, bool keepInSync, MixerGroupType mixerGroup)
	{
		base.transform.position = position;
		this.m_audioSource.clip = clip;
		this.m_audioSource.loop = false;
		this.Length = this.m_audioSource.clip.length;
		this.Volume = Mathf.Pow(volume, 4f);
		this.m_frame = 0;
		this.UpdateSoundProperties(0f);
		this.m_audioSource.time = 0f;
		this.m_audioSource.spread = 180f;
		this.m_isPlaying = true;
		this.Loop = loop;
		this.m_keepInSync = keepInSync;
		this.m_mixerGroup = mixerGroup;
		this.m_audioSource.outputAudioMixerGroup = MixerManager.GetMixerGroup(this.m_mixerGroup);
		this.m_audioSource.Play();
		this.m_lastRealTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06000217 RID: 535 RVA: 0x00008CC5 File Offset: 0x00006EC5
	public void Play()
	{
		if (!this.m_audioSource)
		{
			return;
		}
		this.m_audioSource.Play();
		this.m_isPlaying = true;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x00008CEA File Offset: 0x00006EEA
	public void Pause()
	{
		this.m_isPlaying = false;
		this.m_audioSource.Pause();
	}

	// Token: 0x06000219 RID: 537 RVA: 0x00008CFE File Offset: 0x00006EFE
	public void Stop()
	{
		if (this.m_audioSource && base.gameObject.activeInHierarchy)
		{
			this.m_audioSource.Stop();
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x0600021A RID: 538 RVA: 0x00008D2B File Offset: 0x00006F2B
	// (set) Token: 0x0600021B RID: 539 RVA: 0x00008D38 File Offset: 0x00006F38
	public bool Loop
	{
		get
		{
			return this.m_audioSource.loop;
		}
		set
		{
			this.m_audioSource.loop = value;
		}
	}

	// Token: 0x0600021C RID: 540 RVA: 0x00008D46 File Offset: 0x00006F46
	public void SetTime(float time)
	{
		this.m_audioSource.time = time;
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x0600021D RID: 541 RVA: 0x00008D54 File Offset: 0x00006F54
	public float AudioSourceTime
	{
		get
		{
			return this.m_audioSource.time;
		}
	}

	// Token: 0x0600021E RID: 542 RVA: 0x00008D61 File Offset: 0x00006F61
	public void SetSoundSize(SoundSize soundSize)
	{
		this.SoundSize = soundSize;
	}

	// Token: 0x0600021F RID: 543 RVA: 0x00008D6A File Offset: 0x00006F6A
	public bool IsPlaying()
	{
		return this.m_isPlaying;
	}

	// Token: 0x06000220 RID: 544 RVA: 0x00008D74 File Offset: 0x00006F74
	public void FadeOut(float time, bool shouldDestroyOnFadeOut)
	{
		if (this.m_audioSource == null || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.DestroyOnFadeOut = shouldDestroyOnFadeOut;
		this.m_fadeSpeed = 1f / time;
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00008DB7 File Offset: 0x00006FB7
	public void FadeIn(float time, bool reset = false)
	{
		this.m_fadeSpeed = -1f / time;
		if (reset)
		{
			this.m_fadeAmount = 1f;
		}
	}

	// Token: 0x06000222 RID: 546 RVA: 0x00008DD8 File Offset: 0x00006FD8
	private bool SoundShouldFreeze()
	{
		return this.IsSuspended && (this.PauseOnSuspend || DebugMenuB.Active || ((UI.MainMenuVisible || ResumeGameController.IsGameSuspended) && this.m_mixerGroup != MixerGroupType.MusicLoops));
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000223 RID: 547 RVA: 0x00008E32 File Offset: 0x00007032
	public bool AllSoundsCanPlay
	{
		get
		{
			return XboxOneSession.IsHighResources && GameController.IsFocused;
		}
	}

	// Token: 0x06000224 RID: 548 RVA: 0x00008E48 File Offset: 0x00007048
	public void Update()
	{
		if (this.SoundShouldFreeze())
		{
			if (this.m_audioSource.isPlaying)
			{
				this.m_audioSource.Pause();
			}
			return;
		}
		if (!this.m_audioSource.isPlaying && this.m_audioSource.time == 0f)
		{
			this.m_isPlaying = false;
		}
		if (this.AllSoundsCanPlay)
		{
			if (!this.m_audioSource.isPlaying && this.m_isPlaying && !this.m_previousIsHighResources)
			{
				this.m_audioSource.Play();
			}
		}
		else if (this.m_audioSource.isPlaying)
		{
			this.m_audioSource.Pause();
		}
		this.m_previousIsHighResources = this.AllSoundsCanPlay;
		if (this.m_timeScaleChanged)
		{
			if (!this.TimeIsScaled)
			{
				this.m_timeScaleChanged = false;
				if (this.m_audioSource.clip && this.m_audioSource.clip.length > this.m_timeToSet + Time.deltaTime)
				{
					this.m_audioSource.time = this.m_timeToSet + Time.deltaTime;
				}
			}
		}
		else if (this.TimeIsScaled)
		{
			this.m_audioSource.volume = 0f;
			this.m_timeToSet = this.m_audioSource.time;
			this.m_timeScaleChanged = true;
		}
		this.m_timeToSet += Time.deltaTime;
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x06000225 RID: 549 RVA: 0x00008FC4 File Offset: 0x000071C4
	public bool TimeIsScaled
	{
		get
		{
			return Time.timeScale < 0.5f || Time.timeScale > 2f;
		}
	}

	// Token: 0x06000226 RID: 550 RVA: 0x00008FE4 File Offset: 0x000071E4
	public void FixedUpdate()
	{
		if (this.TimeIsScaled)
		{
			return;
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.m_lastRealTime;
		this.m_lastRealTime = realtimeSinceStartup;
		if (num > 0.5f)
		{
			num = 0f;
		}
		if (!this.AllSoundsCanPlay || this.SoundShouldFreeze())
		{
			this.UpdateSoundProperties(0f);
		}
		else
		{
			this.UpdateSoundProperties(num);
		}
		this.m_frame++;
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00009060 File Offset: 0x00007260
	private void UpdateSoundProperties(float dt)
	{
		if (UI.Cameras.Current == null)
		{
			return;
		}
		if (this.AttachTo)
		{
			this.Position = this.AttachTo.position;
		}
		this.UpdateFadeTime();
		if (this.m_frame % 3 == 0)
		{
			this.UpdateVolumeProperties();
		}
		this.m_playTime += dt;
		if (this.DestroyOnFinished && this.m_playTime >= this.Length && !this.m_audioSource.isPlaying)
		{
			this.DestroySound();
			return;
		}
		if (this.m_fadeAmount >= 1f)
		{
			this.m_fadeSpeed = 0f;
			if (this.DestroyOnFadeOut)
			{
				this.DestroySound();
			}
		}
		if (this.m_frame % 3 == 0)
		{
			int priority;
			if (!this.Loop)
			{
				priority = Mathf.Clamp(128 - (int)this.Length + (int)(this.m_playTime / this.Length * 64f) + (int)((1f - this.m_audioSource.volume) * 64f), 0, 255);
			}
			else
			{
				priority = Mathf.Clamp((int)((1f - this.m_audioSource.volume) * 200f), 0, 255);
			}
			if (this.m_mixerGroup == MixerGroupType.MusicLoops || this.m_mixerGroup == MixerGroupType.MusicStingers)
			{
				priority = 0;
			}
			if (this.m_audioSource.volume < 0.01f)
			{
				priority = 255;
			}
			this.m_audioSource.priority = priority;
		}
		if (this.m_audioSource.volume <= 0.01f)
		{
			return;
		}
		if (this.m_frame % 5 == 0)
		{
			this.UpdatePanProperties();
			this.UpdateLowPassFilterProperties();
		}
	}

	// Token: 0x06000228 RID: 552 RVA: 0x00009228 File Offset: 0x00007428
	public void UpdateVolumeProperties()
	{
		Vector3 soundListenerPosition = Sound.SoundListenerPosition;
		this.m_distance = MoonMath.Vector.Distance(base.transform.position, soundListenerPosition);
		float num = this.Volume * (1f - this.m_fadeAmount);
		if (this.SoundSize == null)
		{
			return;
		}
		if (!Mathf.Approximately(this.SoundSize.Radius, 0f) || !Mathf.Approximately(this.SoundSize.FalloffMargin, 0f))
		{
			float num2 = this.m_distance - this.SoundSize.Radius;
			float num3;
			if (!Mathf.Approximately(this.SoundSize.FalloffMargin, 0f))
			{
				num2 /= this.SoundSize.FalloffMargin;
				num3 = this.SoundSize.FalloffCurve.Evaluate(Mathf.Clamp01(num2));
			}
			else
			{
				num3 = ((num2 > 0f) ? 0f : 1f);
			}
			this.m_falloffVolumeModifier = ((!Mathf.Approximately(this.m_audioSource.time, 0f)) ? Mathf.Lerp(num3, this.m_falloffVolumeModifier, 0.5f) : num3);
			num *= this.m_falloffVolumeModifier;
		}
		switch (this.SoundType)
		{
		case SoundType.Music:
			if (DebugMenuB.MuteMusic)
			{
				num = 0f;
			}
			break;
		case SoundType.SoundEffect:
			if (DebugMenuB.MuteSoundEffects)
			{
				num = 0f;
			}
			break;
		case SoundType.Ambience:
			if (DebugMenuB.MuteAmbience)
			{
				num = 0f;
			}
			break;
		}
		if (this.m_audioSource.volume != num)
		{
			this.m_audioSource.volume = num;
		}
	}

	// Token: 0x06000229 RID: 553 RVA: 0x000093DA File Offset: 0x000075DA
	private void UpdateFadeTime()
	{
		this.m_fadeAmount = Mathf.Clamp01(this.m_fadeAmount + this.m_fadeSpeed * Time.deltaTime);
	}

	// Token: 0x0600022A RID: 554 RVA: 0x000093FC File Offset: 0x000075FC
	public void UpdatePanProperties()
	{
		if (this.ShouldBePanned && UI.Cameras.Current.Target)
		{
			if (UI.Cameras.Current.OffsetController.Offset != SoundPlayer.m_previousCameraOffset)
			{
				SoundPlayer.m_previousCameraOffset = UI.Cameras.Current.OffsetController.Offset;
				SoundPlayer.m_cameraWidth = UI.Cameras.Current.CameraBoundingBox.size.x;
			}
			float num = base.transform.position.x - UI.Cameras.Current.Target.position.x;
			if (SoundPlayer.m_cameraWidth != 0f)
			{
				this.m_audioSource.panStereo = num / (SoundPlayer.m_cameraWidth / 2f);
			}
		}
	}

	// Token: 0x0600022B RID: 555 RVA: 0x000094D0 File Offset: 0x000076D0
	public void UpdateLowPassFilterProperties()
	{
		if (this.LowPassFilterSettings != null && this.LowPassFilterSettings.Active)
		{
			if (this.m_lowPassFilter == null)
			{
				this.m_lowPassFilter = base.gameObject.GetComponent<AudioLowPassFilter>();
			}
			if (this.m_lowPassFilter == null)
			{
				this.m_lowPassFilter = base.gameObject.AddComponent<AudioLowPassFilter>();
			}
			if (this.m_lowPassFilter)
			{
				this.m_lowPassFilter.lowpassResonanceQ = Mathf.Lerp(this.m_lowPassFilter.lowpassResonanceQ, this.LowPassFilterSettings.LowpassResonance.Evaluate(this.m_distance), 0.5f);
				this.m_lowPassFilter.cutoffFrequency = Mathf.Lerp(this.m_lowPassFilter.cutoffFrequency, this.LowPassFilterSettings.CutoffFrequency.Evaluate(this.m_distance), 0.5f);
			}
		}
		else if (!this.m_checkedLowPass)
		{
			if (this.m_lowPassFilter != null)
			{
				UnityEngine.Object.Destroy(this.m_lowPassFilter);
				this.m_lowPassFilter = null;
			}
			this.m_checkedLowPass = true;
		}
	}

	// Token: 0x0600022C RID: 556 RVA: 0x000095F2 File Offset: 0x000077F2
	public void OnDrawGizmosSelected()
	{
		this.DrawGizmos(true);
	}

	// Token: 0x0600022D RID: 557 RVA: 0x000095FC File Offset: 0x000077FC
	private void DrawGizmos(bool selected)
	{
		if (this.SoundSize != null)
		{
			Color color = Gizmos.color;
			Color color2;
			Color color3;
			if (selected)
			{
				color2 = new Color(0.5f, 1f, 1f, 1f);
				color3 = new Color(0f, 0f, 1f, 1f);
			}
			else
			{
				color2 = new Color(0.5f, 1f, 1f, 0.3f);
				color3 = new Color(0f, 0f, 1f, 0.3f);
			}
			Gizmos.color = color2;
			Gizmos.DrawWireSphere(base.transform.position, this.SoundSize.Radius + this.SoundSize.FalloffMargin);
			Gizmos.color = color3;
			Gizmos.DrawWireSphere(base.transform.position, this.SoundSize.Radius);
			Gizmos.color = color;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x0600022E RID: 558 RVA: 0x000096E5 File Offset: 0x000078E5
	// (set) Token: 0x0600022F RID: 559 RVA: 0x000096F0 File Offset: 0x000078F0
	public bool IsSuspended
	{
		get
		{
			return this.m_isSuspended;
		}
		set
		{
			this.m_isSuspended = value;
			if (!value)
			{
				if (!this.m_audioSource.isPlaying && this.m_isPlaying && !InstantiateUtility.IsDestroyed(base.gameObject))
				{
					this.m_audioSource.Play();
				}
			}
		}
	}

	// Token: 0x06000230 RID: 560 RVA: 0x00009745 File Offset: 0x00007945
	public void DestroySound()
	{
		InstantiateUtility.Destroy(base.gameObject);
	}

	// Token: 0x06000231 RID: 561 RVA: 0x00009754 File Offset: 0x00007954
	public static void DestroyAll()
	{
		List<SoundPlayer> list = new List<SoundPlayer>(SoundPlayer.All);
		for (int i = 0; i < list.Count; i++)
		{
			SoundPlayer soundPlayer = list[i];
			if (soundPlayer.DestroyOnRestart)
			{
				if (soundPlayer.m_audioSource.isPlaying)
				{
					soundPlayer.FadeOut(0.5f, true);
				}
				else
				{
					soundPlayer.DestroySound();
				}
			}
		}
	}

	// Token: 0x040001AE RID: 430
	public static HashSet<SoundPlayer> All = new HashSet<SoundPlayer>();

	// Token: 0x040001AF RID: 431
	[PooledSafe]
	public SoundDescriptor SoundDescriptor;

	// Token: 0x040001B0 RID: 432
	public SoundSize SoundSize = new SoundSize();

	// Token: 0x040001B1 RID: 433
	[PooledSafe]
	public LowPassFilterSettings LowPassFilterSettings;

	// Token: 0x040001B2 RID: 434
	public bool DestroyOnFinished;

	// Token: 0x040001B3 RID: 435
	public bool DestroyOnFadeOut;

	// Token: 0x040001B4 RID: 436
	public bool DestroyOnRestart;

	// Token: 0x040001B5 RID: 437
	public Transform AttachTo;

	// Token: 0x040001B6 RID: 438
	public float Length;

	// Token: 0x040001B7 RID: 439
	public SoundType SoundType = SoundType.SoundEffect;

	// Token: 0x040001B8 RID: 440
	public bool ShouldBePanned;

	// Token: 0x040001B9 RID: 441
	public bool PauseOnSuspend;

	// Token: 0x040001BA RID: 442
	private static Vector3 m_previousCameraOffset;

	// Token: 0x040001BB RID: 443
	private static float m_cameraWidth;

	// Token: 0x040001BC RID: 444
	private AudioSource m_audioSource;

	// Token: 0x040001BD RID: 445
	private bool m_previousIsHighResources = true;

	// Token: 0x040001BE RID: 446
	private bool m_isPlaying;

	// Token: 0x040001BF RID: 447
	private AudioLowPassFilter m_lowPassFilter;

	// Token: 0x040001C0 RID: 448
	private bool m_isSuspended;

	// Token: 0x040001C1 RID: 449
	private float m_volume;

	// Token: 0x040001C2 RID: 450
	private float m_playTime;

	// Token: 0x040001C3 RID: 451
	private float m_fadeSpeed;

	// Token: 0x040001C4 RID: 452
	private float m_fadeAmount;

	// Token: 0x040001C5 RID: 453
	private float m_distance;

	// Token: 0x040001C6 RID: 454
	private float m_timeToSet;

	// Token: 0x040001C7 RID: 455
	private bool m_timeScaleChanged;

	// Token: 0x040001C8 RID: 456
	private bool m_keepInSync;

	// Token: 0x040001C9 RID: 457
	private float m_falloffVolumeModifier = 1f;

	// Token: 0x040001CA RID: 458
	private bool m_checkedLowPass;

	// Token: 0x040001CB RID: 459
	private MixerGroupType m_mixerGroup;

	// Token: 0x040001CC RID: 460
	private int m_frame;

	// Token: 0x040001CD RID: 461
	private float m_lastRealTime;
}
