using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x0200010F RID: 271
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ServerListRequestHandle : IEquatable<ServerListRequestHandle>
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x0000B7DE File Offset: 0x000099DE
		public ServerListRequestHandle(uint value)
		{
			this.handle = value;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0000B7E7 File Offset: 0x000099E7
		public uint AsUInt32
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0000B7EF File Offset: 0x000099EF
		public static bool operator ==(ServerListRequestHandle x, ServerListRequestHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0000B801 File Offset: 0x00009A01
		public static bool operator !=(ServerListRequestHandle x, ServerListRequestHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0000B816 File Offset: 0x00009A16
		public bool Equals(ServerListRequestHandle other)
		{
			return this == other;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0000B824 File Offset: 0x00009A24
		public override bool Equals(object obj)
		{
			return obj is ServerListRequestHandle && this == (ServerListRequestHandle)obj;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0000B841 File Offset: 0x00009A41
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0000B84E File Offset: 0x00009A4E
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x040004B7 RID: 1207
		public static readonly ServerListRequestHandle Invalid = new ServerListRequestHandle(0U);

		// Token: 0x040004B8 RID: 1208
		private uint handle;
	}
}
