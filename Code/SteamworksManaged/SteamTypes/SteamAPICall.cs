using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000128 RID: 296
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamAPICall : IEquatable<SteamAPICall>
	{
		// Token: 0x06000A1C RID: 2588 RVA: 0x0000BE2B File Offset: 0x0000A02B
		public SteamAPICall(ulong value)
		{
			this.handle = value;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0000BE34 File Offset: 0x0000A034
		public ulong AsUInt64
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0000BE3C File Offset: 0x0000A03C
		public uint AsUInt32
		{
			get
			{
				return (uint)this.handle;
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0000BE45 File Offset: 0x0000A045
		public static bool operator ==(SteamAPICall x, SteamAPICall y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0000BE57 File Offset: 0x0000A057
		public static bool operator !=(SteamAPICall x, SteamAPICall y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0000BE6C File Offset: 0x0000A06C
		public bool Equals(SteamAPICall other)
		{
			return this == other;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0000BE7A File Offset: 0x0000A07A
		public override bool Equals(object obj)
		{
			return obj is AppID && this == (SteamAPICall)obj;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0000BE97 File Offset: 0x0000A097
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x0400050F RID: 1295
		private ulong handle;
	}
}
