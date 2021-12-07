using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Mono.Audio
{
	// Token: 0x020002B7 RID: 695
	internal class Win32SoundPlayer : IDisposable
	{
		// Token: 0x06001819 RID: 6169 RVA: 0x00042390 File Offset: 0x00040590
		public Win32SoundPlayer(Stream s)
		{
			if (s != null)
			{
				this._buffer = new byte[s.Length];
				s.Read(this._buffer, 0, this._buffer.Length);
			}
			else
			{
				this._buffer = new byte[0];
			}
		}

		// Token: 0x0600181A RID: 6170
		[DllImport("winmm.dll", SetLastError = true)]
		private static extern bool PlaySound(byte[] ptrToSound, UIntPtr hmod, Win32SoundPlayer.SoundFlags flags);

		// Token: 0x170005A9 RID: 1449
		// (set) Token: 0x0600181B RID: 6171 RVA: 0x000423E4 File Offset: 0x000405E4
		public Stream Stream
		{
			set
			{
				this.Stop();
				if (value != null)
				{
					this._buffer = new byte[value.Length];
					value.Read(this._buffer, 0, this._buffer.Length);
				}
				else
				{
					this._buffer = new byte[0];
				}
			}
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00042438 File Offset: 0x00040638
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00042448 File Offset: 0x00040648
		~Win32SoundPlayer()
		{
			this.Dispose(false);
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00042484 File Offset: 0x00040684
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this.Stop();
				this._disposed = true;
			}
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000424A0 File Offset: 0x000406A0
		public void Play()
		{
			Win32SoundPlayer.PlaySound(this._buffer, UIntPtr.Zero, (Win32SoundPlayer.SoundFlags)5U);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000424B4 File Offset: 0x000406B4
		public void PlayLooping()
		{
			Win32SoundPlayer.PlaySound(this._buffer, UIntPtr.Zero, (Win32SoundPlayer.SoundFlags)13U);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x000424CC File Offset: 0x000406CC
		public void PlaySync()
		{
			Win32SoundPlayer.PlaySound(this._buffer, UIntPtr.Zero, (Win32SoundPlayer.SoundFlags)6U);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000424E0 File Offset: 0x000406E0
		public void Stop()
		{
			Win32SoundPlayer.PlaySound(null, UIntPtr.Zero, Win32SoundPlayer.SoundFlags.SND_SYNC);
		}

		// Token: 0x04000F60 RID: 3936
		private byte[] _buffer;

		// Token: 0x04000F61 RID: 3937
		private bool _disposed;

		// Token: 0x020002B8 RID: 696
		private enum SoundFlags : uint
		{
			// Token: 0x04000F63 RID: 3939
			SND_SYNC,
			// Token: 0x04000F64 RID: 3940
			SND_ASYNC,
			// Token: 0x04000F65 RID: 3941
			SND_NODEFAULT,
			// Token: 0x04000F66 RID: 3942
			SND_MEMORY = 4U,
			// Token: 0x04000F67 RID: 3943
			SND_LOOP = 8U,
			// Token: 0x04000F68 RID: 3944
			SND_FILENAME = 131072U
		}
	}
}
