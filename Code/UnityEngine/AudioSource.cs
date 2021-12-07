using System;
using System.Runtime.CompilerServices;
using UnityEngine.Audio;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000186 RID: 390
	public sealed class AudioSource : Behaviour
	{
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600183D RID: 6205
		// (set) Token: 0x0600183E RID: 6206
		public extern float volume { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600183F RID: 6207
		// (set) Token: 0x06001840 RID: 6208
		public extern float pitch { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001841 RID: 6209
		// (set) Token: 0x06001842 RID: 6210
		public extern float time { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001843 RID: 6211
		// (set) Token: 0x06001844 RID: 6212
		public extern int timeSamples { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001845 RID: 6213
		// (set) Token: 0x06001846 RID: 6214
		public extern AudioClip clip { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001847 RID: 6215
		// (set) Token: 0x06001848 RID: 6216
		public extern AudioMixerGroup outputAudioMixerGroup { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001849 RID: 6217
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play([DefaultValue("0")] ulong delay);

		// Token: 0x0600184A RID: 6218 RVA: 0x000186E4 File Offset: 0x000168E4
		[ExcludeFromDocs]
		public void Play()
		{
			ulong delay = 0UL;
			this.Play(delay);
		}

		// Token: 0x0600184B RID: 6219
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PlayDelayed(float delay);

		// Token: 0x0600184C RID: 6220
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PlayScheduled(double time);

		// Token: 0x0600184D RID: 6221
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetScheduledStartTime(double time);

		// Token: 0x0600184E RID: 6222
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetScheduledEndTime(double time);

		// Token: 0x0600184F RID: 6223
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		// Token: 0x06001850 RID: 6224 RVA: 0x000186FC File Offset: 0x000168FC
		public void Pause()
		{
			AudioSource.INTERNAL_CALL_Pause(this);
		}

		// Token: 0x06001851 RID: 6225
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Pause(AudioSource self);

		// Token: 0x06001852 RID: 6226 RVA: 0x00018704 File Offset: 0x00016904
		public void UnPause()
		{
			AudioSource.INTERNAL_CALL_UnPause(this);
		}

		// Token: 0x06001853 RID: 6227
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_UnPause(AudioSource self);

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001854 RID: 6228
		public extern bool isPlaying { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001855 RID: 6229
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PlayOneShot(AudioClip clip, [DefaultValue("1.0F")] float volumeScale);

		// Token: 0x06001856 RID: 6230 RVA: 0x0001870C File Offset: 0x0001690C
		[ExcludeFromDocs]
		public void PlayOneShot(AudioClip clip)
		{
			float volumeScale = 1f;
			this.PlayOneShot(clip, volumeScale);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00018728 File Offset: 0x00016928
		[ExcludeFromDocs]
		public static void PlayClipAtPoint(AudioClip clip, Vector3 position)
		{
			float volume = 1f;
			AudioSource.PlayClipAtPoint(clip, position, volume);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00018744 File Offset: 0x00016944
		public static void PlayClipAtPoint(AudioClip clip, Vector3 position, [DefaultValue("1.0F")] float volume)
		{
			GameObject gameObject = new GameObject("One shot audio");
			gameObject.transform.position = position;
			AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
			audioSource.clip = clip;
			audioSource.spatialBlend = 1f;
			audioSource.volume = volume;
			audioSource.Play();
			Object.Destroy(gameObject, clip.length * ((Time.timeScale >= 0.01f) ? Time.timeScale : 0.01f));
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001859 RID: 6233
		// (set) Token: 0x0600185A RID: 6234
		public extern bool loop { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600185B RID: 6235
		// (set) Token: 0x0600185C RID: 6236
		public extern bool ignoreListenerVolume { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600185D RID: 6237
		// (set) Token: 0x0600185E RID: 6238
		public extern bool playOnAwake { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x0600185F RID: 6239
		// (set) Token: 0x06001860 RID: 6240
		public extern bool ignoreListenerPause { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001861 RID: 6241
		// (set) Token: 0x06001862 RID: 6242
		public extern AudioVelocityUpdateMode velocityUpdateMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001863 RID: 6243
		// (set) Token: 0x06001864 RID: 6244
		public extern float panStereo { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001865 RID: 6245
		// (set) Token: 0x06001866 RID: 6246
		public extern float spatialBlend { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001867 RID: 6247
		// (set) Token: 0x06001868 RID: 6248
		public extern bool spatialize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001869 RID: 6249
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetCustomCurve(AudioSourceCurveType type, AnimationCurve curve);

		// Token: 0x0600186A RID: 6250
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimationCurve GetCustomCurve(AudioSourceCurveType type);

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x0600186B RID: 6251
		// (set) Token: 0x0600186C RID: 6252
		public extern float reverbZoneMix { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600186D RID: 6253
		// (set) Token: 0x0600186E RID: 6254
		public extern bool bypassEffects { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600186F RID: 6255
		// (set) Token: 0x06001870 RID: 6256
		public extern bool bypassListenerEffects { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001871 RID: 6257
		// (set) Token: 0x06001872 RID: 6258
		public extern bool bypassReverbZones { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001873 RID: 6259
		// (set) Token: 0x06001874 RID: 6260
		public extern float dopplerLevel { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001875 RID: 6261
		// (set) Token: 0x06001876 RID: 6262
		public extern float spread { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001877 RID: 6263
		// (set) Token: 0x06001878 RID: 6264
		public extern int priority { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001879 RID: 6265
		// (set) Token: 0x0600187A RID: 6266
		public extern bool mute { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600187B RID: 6267
		// (set) Token: 0x0600187C RID: 6268
		public extern float minDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600187D RID: 6269
		// (set) Token: 0x0600187E RID: 6270
		public extern float maxDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600187F RID: 6271
		// (set) Token: 0x06001880 RID: 6272
		public extern AudioRolloffMode rolloffMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001881 RID: 6273
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetOutputDataHelper(float[] samples, int channel);

		// Token: 0x06001882 RID: 6274 RVA: 0x000187C8 File Offset: 0x000169C8
		[Obsolete("GetOutputData return a float[] is deprecated, use GetOutputData passing a pre allocated array instead.")]
		public float[] GetOutputData(int numSamples, int channel)
		{
			float[] array = new float[numSamples];
			this.GetOutputDataHelper(array, channel);
			return array;
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x000187E8 File Offset: 0x000169E8
		public void GetOutputData(float[] samples, int channel)
		{
			this.GetOutputDataHelper(samples, channel);
		}

		// Token: 0x06001884 RID: 6276
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSpectrumDataHelper(float[] samples, int channel, FFTWindow window);

		// Token: 0x06001885 RID: 6277 RVA: 0x000187F4 File Offset: 0x000169F4
		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData passing a pre allocated array instead.")]
		public float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] array = new float[numSamples];
			this.GetSpectrumDataHelper(array, channel, window);
			return array;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00018814 File Offset: 0x00016A14
		public void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			this.GetSpectrumDataHelper(samples, channel, window);
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001887 RID: 6279
		// (set) Token: 0x06001888 RID: 6280
		[Obsolete("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public extern float minVolume { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001889 RID: 6281
		// (set) Token: 0x0600188A RID: 6282
		[Obsolete("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public extern float maxVolume { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600188B RID: 6283
		// (set) Token: 0x0600188C RID: 6284
		[Obsolete("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public extern float rolloffFactor { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600188D RID: 6285
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetSpatializerFloat(int index, float value);

		// Token: 0x0600188E RID: 6286
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetSpatializerFloat(int index, out float value);
	}
}
