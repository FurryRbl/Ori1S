using System;

namespace Mono.Audio
{
	// Token: 0x020002B2 RID: 690
	internal class AudioDevice
	{
		// Token: 0x060017DD RID: 6109 RVA: 0x00041ACC File Offset: 0x0003FCCC
		private static AudioDevice TryAlsa(string name)
		{
			AudioDevice result;
			try
			{
				AudioDevice audioDevice = new AlsaDevice(name);
				result = audioDevice;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00041B18 File Offset: 0x0003FD18
		public static AudioDevice CreateDevice(string name)
		{
			AudioDevice audioDevice = AudioDevice.TryAlsa(name);
			if (audioDevice == null)
			{
				audioDevice = new AudioDevice();
			}
			return audioDevice;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00041B3C File Offset: 0x0003FD3C
		public virtual bool SetFormat(AudioFormat format, int channels, int rate)
		{
			return true;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00041B40 File Offset: 0x0003FD40
		public virtual int PlaySample(byte[] buffer, int num_frames)
		{
			return num_frames;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00041B44 File Offset: 0x0003FD44
		public virtual void Wait()
		{
		}
	}
}
