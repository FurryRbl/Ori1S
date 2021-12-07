using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x0200008A RID: 138
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AuthTicketHandle : IEquatable<AuthTicketHandle>
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x00007D9A File Offset: 0x00005F9A
		public AuthTicketHandle(uint value)
		{
			this.handle = value;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00007DA3 File Offset: 0x00005FA3
		public uint AsUInt32
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00007DAB File Offset: 0x00005FAB
		public static bool operator ==(AuthTicketHandle x, AuthTicketHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00007DBD File Offset: 0x00005FBD
		public static bool operator !=(AuthTicketHandle x, AuthTicketHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00007DD2 File Offset: 0x00005FD2
		public bool Equals(AuthTicketHandle other)
		{
			return this == other;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00007DE0 File Offset: 0x00005FE0
		public override bool Equals(object obj)
		{
			return obj is AuthTicketHandle && this == (AuthTicketHandle)obj;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00007DFD File Offset: 0x00005FFD
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00007E0A File Offset: 0x0000600A
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x04000284 RID: 644
		private uint handle;
	}
}
