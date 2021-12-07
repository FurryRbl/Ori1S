using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x0200004D RID: 77
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AppID : IEquatable<AppID>
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00003E54 File Offset: 0x00002054
		public AppID(uint value)
		{
			this.handle = value;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00003E5D File Offset: 0x0000205D
		public ulong AsUInt64
		{
			get
			{
				return (ulong)this.handle;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00003E66 File Offset: 0x00002066
		public uint AsUInt32
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00003E6E File Offset: 0x0000206E
		public static bool operator ==(AppID x, AppID y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00003E80 File Offset: 0x00002080
		public static bool operator !=(AppID x, AppID y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00003E95 File Offset: 0x00002095
		public bool Equals(AppID other)
		{
			return this == other;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00003EA3 File Offset: 0x000020A3
		public override bool Equals(object obj)
		{
			return obj is AppID && this == (AppID)obj;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00003EC0 File Offset: 0x000020C0
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00003ECD File Offset: 0x000020CD
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x0400016B RID: 363
		private uint handle;
	}
}
