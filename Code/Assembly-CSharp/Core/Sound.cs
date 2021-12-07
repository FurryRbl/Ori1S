using System;
using Game;
using UnityEngine;

namespace Core
{
	// Token: 0x02000032 RID: 50
	public static class Sound
	{
		// Token: 0x06000233 RID: 563 RVA: 0x000097BF File Offset: 0x000079BF
		public static GameObject GetAudioObjectsParent()
		{
			Sound.LoadAudioParent();
			return Sound.s_audioObjectsParent;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000097CC File Offset: 0x000079CC
		public static void LoadAudioParent()
		{
			if (Sound.s_audioObjectsParent == null)
			{
				Sound.s_audioObjectsParent = new GameObject("audioObjectsParent");
				UnityEngine.Object.DontDestroyOnLoad(Sound.s_audioObjectsParent);
			}
			if (Sound.s_loopingPrefab == null)
			{
				Sound.s_loopingPrefab = Resources.Load<GameObject>("loopingAudio");
			}
			if (Sound.s_oneShotPrefab == null)
			{
				Sound.s_oneShotPrefab = Resources.Load<GameObject>("oneShotAudio");
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009840 File Offset: 0x00007A40
		public static SoundPlayer PlayLooping(SoundDescriptor soundDescriptor, Vector3 position, Action nullify)
		{
			return Sound.PlayLooping(soundDescriptor.AudioClip, position, soundDescriptor.Volume, soundDescriptor.SoundSize, soundDescriptor.ShouldBePanned, soundDescriptor.Pitch, soundDescriptor.LowPassFilterSettings, soundDescriptor, nullify, 0f);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009880 File Offset: 0x00007A80
		public static SoundPlayer PlayLooping(SoundDescriptor soundDescriptor, Vector3 position, float fadeInDuration, Action nullify)
		{
			return Sound.PlayLooping(soundDescriptor.AudioClip, position, soundDescriptor.Volume, soundDescriptor.SoundSize, soundDescriptor.ShouldBePanned, soundDescriptor.Pitch, soundDescriptor.LowPassFilterSettings, soundDescriptor, nullify, fadeInDuration);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000098BC File Offset: 0x00007ABC
		public static SoundPlayer PlayLooping(AudioClip audioClip, Vector3 position, float volume, SoundSize soundSize, bool shouldBePanned, float pitch, LowPassFilterSettings lowPassFilterSettings, SoundDescriptor soundDescriptor, Action nullify, float fadeIn = 0f)
		{
			if (Sound.AllSoundsDisabled)
			{
				return null;
			}
			if (audioClip == null)
			{
				if (soundDescriptor.SoundProvider)
				{
				}
				return null;
			}
			if (Sound.s_loopingPrefab == null)
			{
				Sound.s_loopingPrefab = Resources.Load<GameObject>("loopingAudio");
			}
			GameObject gameObject = InstantiateUtility.Instantiate(Sound.s_loopingPrefab) as GameObject;
			if (nullify != null)
			{
				InstantiateUtility.AddOnDestroy(gameObject, nullify);
			}
			gameObject.transform.SetParentMaintainingLocalTransform(Sound.GetAudioObjectsParent().transform);
			SoundPlayer component = gameObject.GetComponent<SoundPlayer>();
			if (fadeIn != 0f)
			{
				component.FadeIn(fadeIn, true);
			}
			component.SoundSize = soundSize;
			component.ShouldBePanned = shouldBePanned;
			component.Play(audioClip, position, volume, true, soundDescriptor.SyncToTime, soundDescriptor.MixerGroup);
			return component;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009990 File Offset: 0x00007B90
		public static SoundPlayer Play(SoundDescriptor soundDescriptor, Vector3 position, Action nullify)
		{
			if (soundDescriptor == null)
			{
				return null;
			}
			return Sound.Play(soundDescriptor.AudioClip, position, soundDescriptor.Volume, soundDescriptor.SoundSize, soundDescriptor.ShouldBePanned, soundDescriptor.Pitch, soundDescriptor.LowPassFilterSettings, soundDescriptor, nullify);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000099D4 File Offset: 0x00007BD4
		public static SoundPlayer Play(AudioClip audioClip, Vector3 position, Action nullify, float volume = 1f, SoundDescriptor soundDescriptor = null)
		{
			return Sound.Play(audioClip, position, volume, SoundSize.Everywhere, false, 1f, LowPassFilterSettings.StandardSetting, soundDescriptor, nullify);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000099FC File Offset: 0x00007BFC
		private static SoundPlayer GetPooledPlayer(AudioClip clip, Vector3 pos, Quaternion rotation, Action nullify)
		{
			if (Sound.s_oneShotPrefab == null)
			{
				Sound.s_oneShotPrefab = Resources.Load<GameObject>("oneShotAudio");
			}
			GameObject gameObject = InstantiateUtility.Instantiate(Sound.s_oneShotPrefab, pos, rotation) as GameObject;
			if (nullify != null)
			{
				InstantiateUtility.AddOnDestroy(gameObject, nullify);
			}
			gameObject.transform.parent = Sound.GetAudioObjectsParent().transform;
			SoundPlayer component = gameObject.GetComponent<SoundPlayer>();
			component.Position = pos;
			return component;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009A6C File Offset: 0x00007C6C
		public static SoundPlayer Play(AudioClip audioClip, Vector3 position, float volume, SoundSize soundSize, bool shouldBePanned, float pitch, LowPassFilterSettings lowPassFilterSettings, SoundDescriptor soundDescriptor, Action nullify)
		{
			if (Sound.AllSoundsDisabled)
			{
				return null;
			}
			if (audioClip == null)
			{
				if (soundDescriptor.SoundProvider)
				{
				}
				return null;
			}
			if (audioClip.length < 40f && (soundSize.Radius != 0f || soundSize.FalloffMargin != 0f))
			{
				float num = soundSize.Radius + soundSize.FalloffMargin;
				if (UI.Cameras.Current.Target && num * num < (position - UI.Cameras.Current.Target.position).sqrMagnitude)
				{
					return null;
				}
			}
			SoundPlayer pooledPlayer = Sound.GetPooledPlayer(audioClip, position, Quaternion.identity, nullify);
			pooledPlayer.SoundSize = soundSize;
			pooledPlayer.DestroyOnFinished = true;
			pooledPlayer.ShouldBePanned = shouldBePanned;
			pooledPlayer.Pitch = pitch;
			pooledPlayer.LowPassFilterSettings = lowPassFilterSettings;
			pooledPlayer.Play(audioClip, position, volume, false, soundDescriptor.SyncToTime, soundDescriptor.MixerGroup);
			if (Sound.IsSoundLogEnabled)
			{
				SoundLog.AddSoundCall(soundDescriptor.AudioClip.name, soundDescriptor.SoundProvider.name);
			}
			if (Sound.IsPinkBoxesEnabled && soundDescriptor.SoundProvider && soundDescriptor.SoundProvider.ShowMessageOnPlay && soundDescriptor.SoundProvider)
			{
				SoundMessages.ShowMessage(soundDescriptor.SoundProvider.name);
			}
			return pooledPlayer;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00009BE5 File Offset: 0x00007DE5
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00009BEC File Offset: 0x00007DEC
		public static bool IsSoundLogEnabled
		{
			get
			{
				return Sound.isSoundLogEnabled;
			}
			set
			{
				Sound.isSoundLogEnabled = value;
				if (value)
				{
					Sound.m_soundLog = new GameObject("soundLog");
					Sound.m_soundLog.AddComponent<SoundLog>();
				}
				else
				{
					InstantiateUtility.Destroy(Sound.m_soundLog);
					Sound.m_soundLog = null;
					SoundLog.ResetLog();
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009C39 File Offset: 0x00007E39
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00009C40 File Offset: 0x00007E40
		public static bool IsPinkBoxesEnabled
		{
			get
			{
				return Sound.isPinkBoxesEnabled;
			}
			set
			{
				Sound.isPinkBoxesEnabled = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00009C48 File Offset: 0x00007E48
		public static Vector3 SoundListenerPosition
		{
			get
			{
				if (Characters.Current == null || (Characters.Current != null && GameController.Instance.InputLocked))
				{
					return UI.Cameras.Current.CameraTarget.TargetPosition;
				}
				return Characters.Current.Position;
			}
		}

		// Token: 0x040001CE RID: 462
		public static bool AllSoundsDisabled;

		// Token: 0x040001CF RID: 463
		private static GameObject s_audioObjectsParent;

		// Token: 0x040001D0 RID: 464
		private static GameObject s_oneShotPrefab;

		// Token: 0x040001D1 RID: 465
		private static GameObject s_loopingPrefab;

		// Token: 0x040001D2 RID: 466
		private static bool isSoundLogEnabled;

		// Token: 0x040001D3 RID: 467
		private static bool isPinkBoxesEnabled;

		// Token: 0x040001D4 RID: 468
		private static GameObject m_soundLog;
	}
}
