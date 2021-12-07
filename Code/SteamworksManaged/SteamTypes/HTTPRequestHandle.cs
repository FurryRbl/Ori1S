using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x0200007E RID: 126
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTTPRequestHandle : IEquatable<HTTPRequestHandle>
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x00007B1B File Offset: 0x00005D1B
		public HTTPRequestHandle(uint value)
		{
			this.handle = value;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00007B24 File Offset: 0x00005D24
		public uint AsUInt32
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00007B2C File Offset: 0x00005D2C
		public static bool operator ==(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00007B3E File Offset: 0x00005D3E
		public static bool operator !=(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00007B53 File Offset: 0x00005D53
		public bool Equals(HTTPRequestHandle other)
		{
			return this == other;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00007B61 File Offset: 0x00005D61
		public override bool Equals(object obj)
		{
			return obj is HTTPRequestHandle && this == (HTTPRequestHandle)obj;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00007B7E File Offset: 0x00005D7E
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00007B8B File Offset: 0x00005D8B
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x0400020D RID: 525
		private uint handle;
	}
}
