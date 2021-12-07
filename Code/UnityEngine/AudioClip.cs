using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000180 RID: 384
	public sealed class AudioClip : Object
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06001814 RID: 6164 RVA: 0x000184C8 File Offset: 0x000166C8
		// (remove) Token: 0x06001815 RID: 6165 RVA: 0x000184E4 File Offset: 0x000166E4
		private event AudioClip.PCMReaderCallback m_PCMReaderCallback;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06001816 RID: 6166 RVA: 0x00018500 File Offset: 0x00016700
		// (remove) Token: 0x06001817 RID: 6167 RVA: 0x0001851C File Offset: 0x0001671C
		private event AudioClip.PCMSetPositionCallback m_PCMSetPositionCallback;

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001818 RID: 6168
		public extern float length { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001819 RID: 6169
		public extern int samples { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600181A RID: 6170
		public extern int channels { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x0600181B RID: 6171
		public extern int frequency { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x0600181C RID: 6172
		[Obsolete("Use AudioClip.loadState instead to get more detailed information about the loading process.")]
		public extern bool isReadyToPlay { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x0600181D RID: 6173
		public extern AudioClipLoadType loadType { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600181E RID: 6174
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool LoadAudioData();

		// Token: 0x0600181F RID: 6175
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool UnloadAudioData();

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001820 RID: 6176
		public extern bool preloadAudioData { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001821 RID: 6177
		public extern AudioDataLoadState loadState { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001822 RID: 6178
		public extern bool loadInBackground { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001823 RID: 6179
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetData(float[] data, int offsetSamples);

		// Token: 0x06001824 RID: 6180
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetData(float[] data, int offsetSamples);

		// Token: 0x06001825 RID: 6181 RVA: 0x00018538 File Offset: 0x00016738
		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00018548 File Offset: 0x00016748
		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00018558 File Offset: 0x00016758
		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, pcmsetpositioncallback);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0001856C File Offset: 0x0001676C
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, null, null);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00018588 File Offset: 0x00016788
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x000185A8 File Offset: 0x000167A8
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			if (name == null)
			{
				throw new NullReferenceException();
			}
			if (lengthSamples <= 0)
			{
				throw new ArgumentException("Length of created clip must be larger than 0");
			}
			if (channels <= 0)
			{
				throw new ArgumentException("Number of channels in created clip must be greater than 0");
			}
			if (frequency <= 0)
			{
				throw new ArgumentException("Frequency in created clip must be greater than 0");
			}
			AudioClip audioClip = AudioClip.Construct_Internal();
			if (pcmreadercallback != null)
			{
				AudioClip audioClip2 = audioClip;
				audioClip2.m_PCMReaderCallback = (AudioClip.PCMReaderCallback)Delegate.Combine(audioClip2.m_PCMReaderCallback, pcmreadercallback);
			}
			if (pcmsetpositioncallback != null)
			{
				AudioClip audioClip3 = audioClip;
				audioClip3.m_PCMSetPositionCallback = (AudioClip.PCMSetPositionCallback)Delegate.Combine(audioClip3.m_PCMSetPositionCallback, pcmsetpositioncallback);
			}
			audioClip.Init_Internal(name, lengthSamples, channels, frequency, stream);
			return audioClip;
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00018648 File Offset: 0x00016848
		[RequiredByNativeCode]
		private void InvokePCMReaderCallback_Internal(float[] data)
		{
			if (this.m_PCMReaderCallback != null)
			{
				this.m_PCMReaderCallback(data);
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00018664 File Offset: 0x00016864
		[RequiredByNativeCode]
		private void InvokePCMSetPositionCallback_Internal(int position)
		{
			if (this.m_PCMSetPositionCallback != null)
			{
				this.m_PCMSetPositionCallback(position);
			}
		}

		// Token: 0x0600182D RID: 6189
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioClip Construct_Internal();

		// Token: 0x0600182E RID: 6190
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init_Internal(string name, int lengthSamples, int channels, int frequency, bool stream);

		// Token: 0x02000346 RID: 838
		// (Invoke) Token: 0x06002872 RID: 10354
		public delegate void PCMReaderCallback(float[] data);

		// Token: 0x02000347 RID: 839
		// (Invoke) Token: 0x06002876 RID: 10358
		public delegate void PCMSetPositionCallback(int position);
	}
}
