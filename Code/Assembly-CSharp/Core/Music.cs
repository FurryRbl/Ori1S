using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	// Token: 0x020001A0 RID: 416
	public static class Music
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x000491C0 File Offset: 0x000473C0
		public static Music.Layer CurrentMusicLayer
		{
			get
			{
				if (Music.m_musicLayers.Count == 0)
				{
					return null;
				}
				return Music.m_musicLayers[Music.m_musicLayers.Count - 1];
			}
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x000491F4 File Offset: 0x000473F4
		public static void AddMusicLayer(Music.Layer musicLayer)
		{
			if (Music.CurrentMusicLayer != null)
			{
				Music.CurrentMusicLayer.Exit();
			}
			Music.m_musicLayers.Add(musicLayer);
			if (Music.CurrentMusicLayer != null)
			{
				Music.CurrentMusicLayer.Enter();
			}
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00049234 File Offset: 0x00047434
		public static void RemoveMusicLayer(Music.Layer musicLayer)
		{
			if (GameController.IsClosing)
			{
				return;
			}
			Music.Layer currentMusicLayer = Music.CurrentMusicLayer;
			Music.m_musicLayers.Remove(musicLayer);
			if (currentMusicLayer != Music.CurrentMusicLayer)
			{
				if (currentMusicLayer != null)
				{
					currentMusicLayer.Exit();
				}
				if (Music.CurrentMusicLayer != null)
				{
					Music.CurrentMusicLayer.Enter();
				}
			}
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0004928C File Offset: 0x0004748C
		public static Music.MusicTrack PlayTrack(SoundDescriptor soundDescriptor, float fadeInDuration, float fadeOutDuration)
		{
			for (int i = 0; i < Music.CurrentTracks.Count; i++)
			{
				Music.MusicTrack musicTrack = Music.CurrentTracks[i];
				if (musicTrack.SoundDescriptor.AudioClip == soundDescriptor.AudioClip)
				{
					musicTrack.Play(fadeInDuration, fadeOutDuration);
					return musicTrack;
				}
			}
			Music.MusicTrack musicTrack2 = new Music.MusicTrack();
			musicTrack2.SoundDescriptor = soundDescriptor;
			musicTrack2.FadeInDuration = fadeInDuration;
			musicTrack2.FadeOutDuration = fadeOutDuration;
			musicTrack2.Play(musicTrack2.FadeInDuration, musicTrack2.FadeOutDuration);
			Music.CurrentTracks.Add(musicTrack2);
			return musicTrack2;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0004931E File Offset: 0x0004751E
		public static void StopTrack(Music.MusicTrack track)
		{
			track.Stop();
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00049326 File Offset: 0x00047526
		public static void OnRestoreCheckpoint()
		{
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00049328 File Offset: 0x00047528
		public static void UpdateMusic()
		{
			Music.Layer currentMusicLayer = Music.CurrentMusicLayer;
			if (currentMusicLayer != null)
			{
				currentMusicLayer.Update();
			}
			for (int i = 0; i < Music.CurrentTracks.Count; i++)
			{
				Music.MusicTrack musicTrack = Music.CurrentTracks[i];
				if (musicTrack.SoundPlayer == null)
				{
					if (musicTrack.ReferenceCount == 0)
					{
						i--;
						Music.CurrentTracks.Remove(musicTrack);
					}
					else
					{
						SoundDescriptor soundDescriptor = musicTrack.SoundDescriptor;
						soundDescriptor.ShouldBePanned = false;
						soundDescriptor.SoundSize.Radius = 0f;
						soundDescriptor.SoundSize.FalloffMargin = 0f;
						musicTrack.PlayDescriptor(soundDescriptor);
					}
				}
			}
		}

		// Token: 0x04000D20 RID: 3360
		private static readonly List<Music.Layer> m_musicLayers = new List<Music.Layer>();

		// Token: 0x04000D21 RID: 3361
		public static List<Music.MusicTrack> CurrentTracks = new List<Music.MusicTrack>();

		// Token: 0x04000D22 RID: 3362
		public static bool Mute;

		// Token: 0x020006A8 RID: 1704
		public class Layer
		{
			// Token: 0x0600292A RID: 10538 RVA: 0x000B1CD8 File Offset: 0x000AFED8
			public Layer(SoundProvider soundProvider, float fadeInDuration, float fadeOutDuration)
			{
				Music.Layer.Track item = new Music.Layer.Track(soundProvider, fadeInDuration, fadeOutDuration);
				this.m_tracks.Add(item);
			}

			// Token: 0x0600292B RID: 10539 RVA: 0x000B1D0C File Offset: 0x000AFF0C
			public void Enter()
			{
				for (int i = 0; i < this.m_tracks.Count; i++)
				{
					Music.Layer.Track track = this.m_tracks[i];
					track.Enter();
				}
			}

			// Token: 0x0600292C RID: 10540 RVA: 0x000B1D48 File Offset: 0x000AFF48
			public void Exit()
			{
				for (int i = 0; i < this.m_tracks.Count; i++)
				{
					Music.Layer.Track track = this.m_tracks[i];
					track.Exit();
				}
			}

			// Token: 0x0600292D RID: 10541 RVA: 0x000B1D84 File Offset: 0x000AFF84
			public void Update()
			{
				for (int i = 0; i < this.m_tracks.Count; i++)
				{
					Music.Layer.Track track = this.m_tracks[i];
					track.Update();
				}
			}

			// Token: 0x040024B5 RID: 9397
			private readonly List<Music.Layer.Track> m_tracks = new List<Music.Layer.Track>();

			// Token: 0x020006B0 RID: 1712
			public class Track
			{
				// Token: 0x06002941 RID: 10561 RVA: 0x000B2152 File Offset: 0x000B0352
				public Track(SoundProvider soundProvider, float fadeInDuration, float fadeOutDuration)
				{
					this.SoundProvider = soundProvider;
					this.FadeInDuration = fadeInDuration;
					this.FadeOutDuration = fadeOutDuration;
				}

				// Token: 0x06002942 RID: 10562 RVA: 0x000B2170 File Offset: 0x000B0370
				public void Enter()
				{
					if (this.SoundProvider == null)
					{
						return;
					}
					SoundDescriptor sound = this.SoundProvider.GetSound(null);
					this.m_musicTrack = Music.PlayTrack(sound, this.FadeInDuration, this.FadeOutDuration);
				}

				// Token: 0x06002943 RID: 10563 RVA: 0x000B21B4 File Offset: 0x000B03B4
				public void Exit()
				{
					if (this.SoundProvider == null)
					{
						return;
					}
					if (this.m_musicTrack != null)
					{
						this.m_musicTrack.Stop();
					}
				}

				// Token: 0x06002944 RID: 10564 RVA: 0x000B21EC File Offset: 0x000B03EC
				public void Update()
				{
					if (this.SoundProvider == null)
					{
						return;
					}
					SoundDescriptor sound = this.SoundProvider.GetSound(null);
					if (sound.AudioClip != this.m_musicTrack.SoundDescriptor.AudioClip)
					{
						if (this.m_musicTrack != null)
						{
							this.m_musicTrack.Stop();
							this.m_musicTrack = null;
						}
						this.m_musicTrack = Music.PlayTrack(sound, this.FadeInDuration, this.FadeOutDuration);
					}
				}

				// Token: 0x040024C6 RID: 9414
				public SoundProvider SoundProvider;

				// Token: 0x040024C7 RID: 9415
				private Music.MusicTrack m_musicTrack;

				// Token: 0x040024C8 RID: 9416
				public float FadeInDuration;

				// Token: 0x040024C9 RID: 9417
				public float FadeOutDuration;
			}
		}

		// Token: 0x020006B1 RID: 1713
		public class MusicTrack
		{
			// Token: 0x06002946 RID: 10566 RVA: 0x000B2278 File Offset: 0x000B0478
			public void Play(float fadeInDuration, float fadeOutDuration)
			{
				this.FadeInDuration = fadeInDuration;
				this.FadeOutDuration = fadeOutDuration;
				this.ReferenceCount++;
				if (this.ReferenceCount == 1)
				{
					this.Play();
				}
			}

			// Token: 0x06002947 RID: 10567 RVA: 0x000B22B4 File Offset: 0x000B04B4
			public void Play()
			{
				if (this.SoundPlayer)
				{
					this.SoundPlayer.FadeIn(this.FadeInDuration, false);
				}
				else
				{
					if (this.m_soundToNull == null)
					{
						this.m_soundToNull = delegate()
						{
							this.SoundPlayer = null;
						};
					}
					this.SoundDescriptor.ShouldBePanned = false;
					this.SoundDescriptor.SoundSize.Radius = 0f;
					this.SoundDescriptor.SoundSize.FalloffMargin = 0f;
					this.SoundPlayer = Sound.PlayLooping(this.SoundDescriptor, Vector3.zero, this.FadeInDuration, this.m_soundToNull);
					if (this.SoundPlayer)
					{
						this.SoundPlayer.SoundType = SoundType.Music;
					}
				}
			}

			// Token: 0x06002948 RID: 10568 RVA: 0x000B2379 File Offset: 0x000B0579
			public void Stop()
			{
				this.ReferenceCount--;
				if (this.ReferenceCount == 0 && this.SoundPlayer)
				{
					this.SoundPlayer.FadeOut(this.FadeOutDuration, true);
				}
			}

			// Token: 0x06002949 RID: 10569 RVA: 0x000B23B8 File Offset: 0x000B05B8
			public void PlayDescriptor(SoundDescriptor soundDescriptor)
			{
				if (this.m_soundToNull == null)
				{
					this.m_soundToNull = delegate()
					{
						this.SoundPlayer = null;
					};
				}
				this.SoundPlayer = Sound.PlayLooping(soundDescriptor, Vector3.zero, this.m_soundToNull);
				if (this.SoundPlayer)
				{
					this.SoundPlayer.SoundType = SoundType.Music;
				}
			}

			// Token: 0x040024CA RID: 9418
			public SoundDescriptor SoundDescriptor;

			// Token: 0x040024CB RID: 9419
			public SoundPlayer SoundPlayer;

			// Token: 0x040024CC RID: 9420
			public int ReferenceCount;

			// Token: 0x040024CD RID: 9421
			public float FadeInDuration;

			// Token: 0x040024CE RID: 9422
			public float FadeOutDuration;

			// Token: 0x040024CF RID: 9423
			private Action m_soundToNull;
		}
	}
}
