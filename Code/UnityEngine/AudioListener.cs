using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000182 RID: 386
	public sealed class AudioListener : Behaviour
	{
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001830 RID: 6192
		// (set) Token: 0x06001831 RID: 6193
		public static extern float volume { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001832 RID: 6194
		// (set) Token: 0x06001833 RID: 6195
		public static extern bool pause { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001834 RID: 6196
		// (set) Token: 0x06001835 RID: 6197
		public extern AudioVelocityUpdateMode velocityUpdateMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001836 RID: 6198
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputDataHelper(float[] samples, int channel);

		// Token: 0x06001837 RID: 6199
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSpectrumDataHelper(float[] samples, int channel, FFTWindow window);

		// Token: 0x06001838 RID: 6200 RVA: 0x00018688 File Offset: 0x00016888
		[Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public static float[] GetOutputData(int numSamples, int channel)
		{
			float[] array = new float[numSamples];
			AudioListener.GetOutputDataHelper(array, channel);
			return array;
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x000186A4 File Offset: 0x000168A4
		public static void GetOutputData(float[] samples, int channel)
		{
			AudioListener.GetOutputDataHelper(samples, channel);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000186B0 File Offset: 0x000168B0
		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public static float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] array = new float[numSamples];
			AudioListener.GetSpectrumDataHelper(array, channel, window);
			return array;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x000186D0 File Offset: 0x000168D0
		public static void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			AudioListener.GetSpectrumDataHelper(samples, channel, window);
		}
	}
}
