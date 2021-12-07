using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000017 RID: 23
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UGCQueryHandle : IEquatable<UGCQueryHandle>
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00003457 File Offset: 0x00001657
		public UGCQueryHandle(ulong value)
		{
			this.handle = value;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003460 File Offset: 0x00001660
		public ulong AsUInt64
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003468 File Offset: 0x00001668
		public uint AsUInt32
		{
			get
			{
				return (uint)this.handle;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003471 File Offset: 0x00001671
		public static bool operator ==(UGCQueryHandle x, UGCQueryHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003483 File Offset: 0x00001683
		public static bool operator !=(UGCQueryHandle x, UGCQueryHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003498 File Offset: 0x00001698
		public bool Equals(UGCQueryHandle other)
		{
			return this == other;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000034A6 File Offset: 0x000016A6
		public override bool Equals(object obj)
		{
			return obj is UGCQueryHandle && this == (UGCQueryHandle)obj;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000034C3 File Offset: 0x000016C3
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000034D0 File Offset: 0x000016D0
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x0400005D RID: 93
		public static readonly UGCQueryHandle Invalid = new UGCQueryHandle(ulong.MaxValue);

		// Token: 0x0400005E RID: 94
		private ulong handle;
	}
}
