using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200017C RID: 380
	public sealed class AudioSettings
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06001804 RID: 6148 RVA: 0x00018454 File Offset: 0x00016654
		// (remove) Token: 0x06001805 RID: 6149 RVA: 0x0001846C File Offset: 0x0001666C
		public static event AudioSettings.AudioConfigurationChangeHandler OnAudioConfigurationChanged;

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001806 RID: 6150
		public static extern AudioSpeakerMode driverCapabilities { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001807 RID: 6151
		// (set) Token: 0x06001808 RID: 6152
		public static extern AudioSpeakerMode speakerMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001809 RID: 6153
		public static extern double dspTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600180A RID: 6154
		// (set) Token: 0x0600180B RID: 6155
		public static extern int outputSampleRate { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600180C RID: 6156
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetDSPBufferSize(out int bufferLength, out int numBuffers);

		// Token: 0x0600180D RID: 6157
		[WrapperlessIcall]
		[Obsolete("AudioSettings.SetDSPBufferSize is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetDSPBufferSize(int bufferLength, int numBuffers);

		// Token: 0x0600180E RID: 6158 RVA: 0x00018484 File Offset: 0x00016684
		public static AudioConfiguration GetConfiguration()
		{
			AudioConfiguration result;
			AudioSettings.INTERNAL_CALL_GetConfiguration(out result);
			return result;
		}

		// Token: 0x0600180F RID: 6159
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetConfiguration(out AudioConfiguration value);

		// Token: 0x06001810 RID: 6160 RVA: 0x0001849C File Offset: 0x0001669C
		public static bool Reset(AudioConfiguration config)
		{
			return AudioSettings.INTERNAL_CALL_Reset(ref config);
		}

		// Token: 0x06001811 RID: 6161
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Reset(ref AudioConfiguration config);

		// Token: 0x06001812 RID: 6162 RVA: 0x000184A8 File Offset: 0x000166A8
		[RequiredByNativeCode]
		internal static void InvokeOnAudioConfigurationChanged(bool deviceWasChanged)
		{
			if (AudioSettings.OnAudioConfigurationChanged != null)
			{
				AudioSettings.OnAudioConfigurationChanged(deviceWasChanged);
			}
		}

		// Token: 0x02000345 RID: 837
		// (Invoke) Token: 0x0600286E RID: 10350
		public delegate void AudioConfigurationChangeHandler(bool deviceWasChanged);
	}
}
