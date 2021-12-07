using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000100 RID: 256
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AccountID : IEquatable<AccountID>
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x0000B492 File Offset: 0x00009692
		public AccountID(uint value)
		{
			this.handle = value;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0000B49B File Offset: 0x0000969B
		public uint AsUInt32
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000B4A3 File Offset: 0x000096A3
		public static bool operator ==(AccountID x, AccountID y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0000B4B5 File Offset: 0x000096B5
		public static bool operator !=(AccountID x, AccountID y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000B4CA File Offset: 0x000096CA
		public bool Equals(AccountID other)
		{
			return this == other;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000B4D8 File Offset: 0x000096D8
		public override bool Equals(object obj)
		{
			return obj is AccountID && this == (AccountID)obj;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0000B4F5 File Offset: 0x000096F5
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0000B502 File Offset: 0x00009702
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x04000493 RID: 1171
		private uint handle;
	}
}
