using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	// Token: 0x020000BB RID: 187
	public static class Ambience
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00021BB8 File Offset: 0x0001FDB8
		public static Ambience.Layer CurrentAmbienceLayer
		{
			get
			{
				if (Ambience.m_ambienceLayers.Count == 0)
				{
					return null;
				}
				return Ambience.m_ambienceLayers[Ambience.m_ambienceLayers.Count - 1];
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00021BEC File Offset: 0x0001FDEC
		public static void AddAmbienceLayer(Ambience.Layer musicLayer)
		{
			Ambience.Layer currentAmbienceLayer = Ambience.CurrentAmbienceLayer;
			Ambience.m_ambienceLayers.Add(musicLayer);
			Ambience.SortLayers();
			if (currentAmbienceLayer != Ambience.CurrentAmbienceLayer)
			{
				if (currentAmbienceLayer != null)
				{
					currentAmbienceLayer.Exit();
				}
				if (Ambience.CurrentAmbienceLayer != null)
				{
					Ambience.CurrentAmbienceLayer.Enter();
				}
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00021C3C File Offset: 0x0001FE3C
		public static void RemoveAmbienceLayer(Ambience.Layer musicLayer)
		{
			if (GameController.IsClosing)
			{
				return;
			}
			Ambience.Layer currentAmbienceLayer = Ambience.CurrentAmbienceLayer;
			Ambience.m_ambienceLayers.Remove(musicLayer);
			Ambience.SortLayers();
			if (currentAmbienceLayer != Ambience.CurrentAmbienceLayer)
			{
				if (currentAmbienceLayer != null)
				{
					currentAmbienceLayer.Exit();
				}
				if (Ambience.CurrentAmbienceLayer != null)
				{
					Ambience.CurrentAmbienceLayer.Enter();
				}
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00021C96 File Offset: 0x0001FE96
		public static void SortLayers()
		{
			Ambience.m_ambienceLayers.Sort(new Comparison<Ambience.Layer>(Ambience.Layer.Sort));
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00021CB0 File Offset: 0x0001FEB0
		public static Ambience.AmbienceTrack PlayTrack(SoundProvider soundProvider, float fadeInDuration, float fadeOutDuration)
		{
			for (int i = 0; i < Ambience.CurrentTracks.Count; i++)
			{
				Ambience.AmbienceTrack ambienceTrack = Ambience.CurrentTracks[i];
				if (ambienceTrack.SoundProvider == soundProvider)
				{
					ambienceTrack.Play(fadeInDuration, fadeOutDuration);
					return ambienceTrack;
				}
			}
			Ambience.AmbienceTrack ambienceTrack2 = new Ambience.AmbienceTrack();
			ambienceTrack2.SoundProvider = soundProvider;
			ambienceTrack2.FadeInDuration = fadeInDuration;
			ambienceTrack2.FadeOutDuration = fadeOutDuration;
			ambienceTrack2.Play(ambienceTrack2.FadeInDuration, ambienceTrack2.FadeOutDuration);
			Ambience.CurrentTracks.Add(ambienceTrack2);
			return ambienceTrack2;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00021D38 File Offset: 0x0001FF38
		public static void StopTrack(Ambience.AmbienceTrack track)
		{
			track.Stop();
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00021D40 File Offset: 0x0001FF40
		public static void UpdateAmbience()
		{
			for (int i = 0; i < Ambience.CurrentTracks.Count; i++)
			{
				Ambience.AmbienceTrack ambienceTrack = Ambience.CurrentTracks[i];
				if (!(ambienceTrack.SoundPlayer != null))
				{
					if (ambienceTrack.ReferenceCount == 0)
					{
						i--;
						Ambience.CurrentTracks.Remove(ambienceTrack);
					}
					else
					{
						SoundDescriptor sound = ambienceTrack.SoundProvider.GetSound(null);
						sound.ShouldBePanned = false;
						sound.SoundSize.Radius = 0f;
						sound.SoundSize.FalloffMargin = 0f;
						ambienceTrack.SoundPlayer = Sound.PlayLooping(sound, Vector3.zero, ambienceTrack.SoundNullify);
						if (ambienceTrack.SoundPlayer)
						{
							ambienceTrack.SoundPlayer.SoundType = SoundType.Ambience;
						}
					}
				}
			}
		}

		// Token: 0x04000652 RID: 1618
		private static readonly List<Ambience.Layer> m_ambienceLayers = new List<Ambience.Layer>();

		// Token: 0x04000653 RID: 1619
		public static List<Ambience.AmbienceTrack> CurrentTracks = new List<Ambience.AmbienceTrack>();

		// Token: 0x04000654 RID: 1620
		public static bool Mute;

		// Token: 0x020000BC RID: 188
		public class Layer
		{
			// Token: 0x060007E1 RID: 2017 RVA: 0x00021E14 File Offset: 0x00020014
			public Layer(SoundProvider soundProvider, float fadeInDuration, float fadeOutDuration, int priority)
			{
				Ambience.Layer.Track item = new Ambience.Layer.Track(soundProvider, fadeInDuration, fadeOutDuration);
				this.m_tracks.Add(item);
				this.m_priority = priority;
			}

			// Token: 0x060007E2 RID: 2018 RVA: 0x00021E50 File Offset: 0x00020050
			public static int Sort(Ambience.Layer layerA, Ambience.Layer layerB)
			{
				return layerA.m_priority.CompareTo(layerB.m_priority);
			}

			// Token: 0x060007E3 RID: 2019 RVA: 0x00021E74 File Offset: 0x00020074
			public void Enter()
			{
				foreach (Ambience.Layer.Track track in this.m_tracks)
				{
					track.Enter();
				}
			}

			// Token: 0x060007E4 RID: 2020 RVA: 0x00021ED0 File Offset: 0x000200D0
			public void Exit()
			{
				foreach (Ambience.Layer.Track track in this.m_tracks)
				{
					track.Exit();
				}
			}

			// Token: 0x04000655 RID: 1621
			private readonly List<Ambience.Layer.Track> m_tracks = new List<Ambience.Layer.Track>();

			// Token: 0x04000656 RID: 1622
			private readonly int m_priority;

			// Token: 0x020006AE RID: 1710
			public class Track
			{
				// Token: 0x06002939 RID: 10553 RVA: 0x000B1F6B File Offset: 0x000B016B
				public Track(SoundProvider soundProvider, float fadeInDuration, float fadeOutDuration)
				{
					this.SoundProvider = soundProvider;
					this.FadeInDuration = fadeInDuration;
					this.FadeOutDuration = fadeOutDuration;
				}

				// Token: 0x0600293A RID: 10554 RVA: 0x000B1F88 File Offset: 0x000B0188
				public void Enter()
				{
					if (this.SoundProvider == null)
					{
						return;
					}
					this.m_ambienceTrack = Ambience.PlayTrack(this.SoundProvider, this.FadeInDuration, this.FadeOutDuration);
				}

				// Token: 0x0600293B RID: 10555 RVA: 0x000B1FBC File Offset: 0x000B01BC
				public void Exit()
				{
					if (this.SoundProvider == null)
					{
						return;
					}
					if (this.m_ambienceTrack != null)
					{
						Ambience.StopTrack(this.m_ambienceTrack);
					}
				}

				// Token: 0x040024BC RID: 9404
				public SoundProvider SoundProvider;

				// Token: 0x040024BD RID: 9405
				private Ambience.AmbienceTrack m_ambienceTrack;

				// Token: 0x040024BE RID: 9406
				public float FadeInDuration;

				// Token: 0x040024BF RID: 9407
				public float FadeOutDuration;
			}
		}

		// Token: 0x020006AF RID: 1711
		public class AmbienceTrack
		{
			// Token: 0x1700068E RID: 1678
			// (get) Token: 0x0600293D RID: 10557 RVA: 0x000B1FF9 File Offset: 0x000B01F9
			public Action SoundNullify
			{
				get
				{
					if (this.m_soundNullify == null)
					{
						this.m_soundNullify = delegate()
						{
							this.SoundPlayer = null;
						};
					}
					return this.m_soundNullify;
				}
			}

			// Token: 0x0600293E RID: 10558 RVA: 0x000B2020 File Offset: 0x000B0220
			public void Play(float fadeInDuration, float fadeOutDuration)
			{
				this.FadeInDuration = fadeInDuration;
				this.FadeOutDuration = fadeOutDuration;
				this.ReferenceCount++;
				if (this.ReferenceCount == 1)
				{
					if (this.SoundPlayer)
					{
						this.SoundPlayer.FadeIn(this.FadeInDuration, false);
					}
					else
					{
						SoundDescriptor sound = this.SoundProvider.GetSound(null);
						sound.ShouldBePanned = false;
						sound.SoundSize.Radius = 0f;
						sound.SoundSize.FalloffMargin = 0f;
						sound.Volume *= GameSettings.Instance.AmbienceVolume;
						this.SoundPlayer = Sound.PlayLooping(sound, Vector3.zero, this.SoundNullify);
						if (this.SoundPlayer)
						{
							this.SoundPlayer.SoundType = SoundType.Ambience;
							this.SoundPlayer.FadeIn(this.FadeInDuration, true);
						}
					}
				}
			}

			// Token: 0x0600293F RID: 10559 RVA: 0x000B210C File Offset: 0x000B030C
			public void Stop()
			{
				this.ReferenceCount--;
				if (this.ReferenceCount == 0 && this.SoundPlayer)
				{
					this.SoundPlayer.FadeOut(this.FadeOutDuration, true);
				}
			}

			// Token: 0x040024C0 RID: 9408
			public SoundProvider SoundProvider;

			// Token: 0x040024C1 RID: 9409
			public SoundPlayer SoundPlayer;

			// Token: 0x040024C2 RID: 9410
			public int ReferenceCount;

			// Token: 0x040024C3 RID: 9411
			public float FadeInDuration;

			// Token: 0x040024C4 RID: 9412
			public float FadeOutDuration;

			// Token: 0x040024C5 RID: 9413
			private Action m_soundNullify;
		}
	}
}
