using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000F4 RID: 244
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct NetSocketHandle : IEquatable<NetSocketHandle>
	{
		// Token: 0x0600067D RID: 1661 RVA: 0x00009B80 File Offset: 0x00007D80
		public NetSocketHandle(uint value)
		{
			this.handle = value;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00009B89 File Offset: 0x00007D89
		public uint AsUInt32
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00009B91 File Offset: 0x00007D91
		public static bool operator ==(NetSocketHandle x, NetSocketHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00009BA3 File Offset: 0x00007DA3
		public static bool operator !=(NetSocketHandle x, NetSocketHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00009BB8 File Offset: 0x00007DB8
		public bool Equals(NetSocketHandle other)
		{
			return this == other;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00009BC6 File Offset: 0x00007DC6
		public override bool Equals(object obj)
		{
			return obj is NetSocketHandle && this == (NetSocketHandle)obj;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00009BE3 File Offset: 0x00007DE3
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00009BF0 File Offset: 0x00007DF0
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x0400040C RID: 1036
		private uint handle;
	}
}
