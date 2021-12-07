using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x0200009C RID: 156
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ScreenshotHandle : IEquatable<ScreenshotHandle>
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x00008F9D File Offset: 0x0000719D
		public ScreenshotHandle(uint value)
		{
			this.handle = value;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00008FA6 File Offset: 0x000071A6
		public uint AsUInt32
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00008FAE File Offset: 0x000071AE
		public static bool operator ==(ScreenshotHandle x, ScreenshotHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00008FC0 File Offset: 0x000071C0
		public static bool operator !=(ScreenshotHandle x, ScreenshotHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00008FD5 File Offset: 0x000071D5
		public bool Equals(ScreenshotHandle other)
		{
			return this == other;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00008FE3 File Offset: 0x000071E3
		public override bool Equals(object obj)
		{
			return obj is ScreenshotHandle && this == (ScreenshotHandle)obj;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00009000 File Offset: 0x00007200
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000900D File Offset: 0x0000720D
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x040002C9 RID: 713
		private uint handle;
	}
}
